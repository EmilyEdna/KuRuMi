using KuRuMi.Mio.DoMain.MongoDbCache.MongoDbCommon;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace KuRuMi.Mio.DoMain.MongoDbCache.MongoDbCache
{
    /// <summary>
    /// MongoDb缓存
    /// </summary>
    public sealed class MongoDbCacheService : MongoDbBase
    {
        #region 同步
        #region 增加
        /// <summary>
        /// 保存单个对象
        /// </summary>
        /// <param name="Root"></param>
        /// <returns></returns>
        public bool AddSignleObject<TAggregateRoot>(TAggregateRoot Root)
        {
            try
            {
                var collection = Db.GetCollection<TAggregateRoot>(typeof(TAggregateRoot).Name);
                collection.InsertOne(Root);
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        /// <summary>
        /// 批量保存多个对象
        /// </summary>
        /// <param name="Root"></param>
        /// <returns></returns>
        public bool AddManyObject<TAggregateRoot>(List<TAggregateRoot> Root)
        {
            try
            {
                var collection = Db.GetCollection<TAggregateRoot>(typeof(TAggregateRoot).Name);
                collection.InsertMany(Root);
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        #endregion

        #region 替换
        public void ReplaceByFilter<TAggregateRoot>(Expression<Func<TAggregateRoot, bool>> filter, TAggregateRoot Root)
        {
            try
            {
                var collection = Db.GetCollection<TAggregateRoot>(typeof(TAggregateRoot).Name);
                var entity = collection.Find(filter).FirstOrDefault();
                foreach (var prop in entity.GetType().GetProperties())
                {
                    if (prop.Name.Equals("Id"))
                        continue;
                    var newValue = prop.GetValue(entity);
                    var oldValue = entity.GetType().GetProperty(prop.Name).GetValue(entity);
                    if (newValue != null)
                    {
                        if (!newValue.ToString().Equals(oldValue.ToString()))
                        {
                            entity.GetType().GetProperty(prop.Name).SetValue(entity, newValue);
                        }
                    }
                }
                collection.ReplaceOne(filter, entity);
            }
            catch (Exception e)
            {

                throw e;
            }
        }
        #endregion

        #region 删除
        /// <summary>
        /// 删除单个记录
        /// </summary>
        /// <typeparam name="TAggregateRoot"></typeparam>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public int DeleteSingleIndex<TAggregateRoot>(Expression<Func<TAggregateRoot, bool>> filter)
        {
            try
            {
                var collection = Db.GetCollection<TAggregateRoot>(typeof(TAggregateRoot).Name);
                return (int)collection.DeleteOne(filter).DeletedCount;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #endregion

        #region 查询
        /// <summary>
        /// 查询单条记录
        /// </summary>
        /// <typeparam name="TAggregateRoot"></typeparam>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public TAggregateRoot FindSingleIndex<TAggregateRoot>(Expression<Func<TAggregateRoot, bool>> filter)
        {
            try
            {
                var collection = Db.GetCollection<TAggregateRoot>(typeof(TAggregateRoot).Name);
                return collection.Find(filter).FirstOrDefault();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// <summary>
        /// 查询整个集合
        /// </summary>
        /// <typeparam name="TAggregateRoot"></typeparam>
        /// <param name="filter"></param>
        /// <returns></returns>
        public List<TAggregateRoot> FindMany<TAggregateRoot>(Expression<Func<TAggregateRoot, bool>> filter)
        {
            try
            {
                var collection = Db.GetCollection<TAggregateRoot>(typeof(TAggregateRoot).Name);
                return collection.Find(filter).ToList();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #endregion

        #region 更新
        /// <summary>
        /// 更新单个值
        /// </summary>
        /// <typeparam name="TAggregateRoot"></typeparam>
        /// <param name="filter"></param>
        /// <param name="field"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public int UpdateSingle<TAggregateRoot>(Expression<Func<TAggregateRoot, bool>> filter, string name, string parameter)
        {
            try
            {
                var collection = Db.GetCollection<TAggregateRoot>(typeof(TAggregateRoot).Name);
                var set = Builders<TAggregateRoot>.Update.Set(name, parameter);
                return (int)collection.UpdateOne(filter, set).ModifiedCount;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// <summary>
        /// 更新多个值
        /// </summary>
        /// <typeparam name="TAggregateRoot"></typeparam>
        /// <param name="filter"></param>
        /// <param name="Root"></param>
        /// <param name="property"></param>
        /// <param name="replace"></param>
        public int UpdateMany<TAggregateRoot>(Expression<Func<TAggregateRoot, bool>> filter, TAggregateRoot Root, List<string> property = null, bool replace = false)
        {
            try
            {
                var collection = Db.GetCollection<TAggregateRoot>(typeof(TAggregateRoot).Name);
                var type = Root.GetType();
                //修改集合
                var list = new List<UpdateDefinition<TAggregateRoot>>();
                foreach (var propert in type.GetProperties())
                {
                    if (propert.Name.ToLower() != "id")
                    {
                        try
                        {
                            if (property == null && property.Count < 1 || property.Any(o => o.ToLower() == propert.Name.ToLower()))
                            {
                                var replaceValue = propert.GetValue(Root);
                                if (replaceValue != null)
                                    list.Add(Builders<TAggregateRoot>.Update.Set(propert.Name, replaceValue));
                                else if (replace)
                                    list.Add(Builders<TAggregateRoot>.Update.Set(propert.Name, replaceValue));
                            }
                        }
                        catch (Exception)
                        {
                            if (property == null)
                            {
                                var replaceValue = propert.GetValue(Root);
                                if (replaceValue != null)
                                    list.Add(Builders<TAggregateRoot>.Update.Set(propert.Name, replaceValue));
                                else if (replace)
                                    list.Add(Builders<TAggregateRoot>.Update.Set(propert.Name, replaceValue));
                            }
                        }

                    }
                }
                if (list.Count > 0)
                {
                    var builders = Builders<TAggregateRoot>.Update.Combine(list);
                    return (int)collection.UpdateOne(filter, builders).ModifiedCount;
                }
                return 0;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #endregion
        #endregion

        #region 异步
        #region 增加
        /// <summary>
        /// 异步保存单个对象
        /// </summary>
        /// <param name="Root"></param>
        /// <returns></returns>
        public bool AddSignleObjectAsync<TAggregateRoot>(TAggregateRoot Root)
        {
            try
            {
                var collection = Db.GetCollection<TAggregateRoot>(typeof(TAggregateRoot).Name);
                collection.InsertOneAsync(Root);
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        /// <summary>
        /// 批量保存多个对象
        /// </summary>
        /// <param name="Root"></param>
        /// <returns></returns>
        public bool AddManyObjectAsync<TAggregateRoot>(List<TAggregateRoot> Root)
        {
            try
            {
                var collection = Db.GetCollection<TAggregateRoot>(typeof(TAggregateRoot).Name);
                collection.InsertManyAsync(Root);
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        #endregion

        #region 替换
        public void ReplaceByFilterAsync<TAggregateRoot>(Expression<Func<TAggregateRoot, bool>> filter, TAggregateRoot Root)
        {
            try
            {
                var collection = Db.GetCollection<TAggregateRoot>(typeof(TAggregateRoot).Name);
                var entity = collection.Find(filter).FirstOrDefaultAsync().Result;
                foreach (var prop in entity.GetType().GetProperties())
                {
                    if (prop.Name.Equals("Id"))
                        continue;
                    var newValue = prop.GetValue(entity);
                    var oldValue = entity.GetType().GetProperty(prop.Name).GetValue(entity);
                    if (newValue != null)
                    {
                        if (!newValue.ToString().Equals(oldValue.ToString()))
                        {
                            entity.GetType().GetProperty(prop.Name).SetValue(entity, newValue);
                        }
                    }
                }
                collection.ReplaceOneAsync(filter, entity);
            }
            catch (Exception e)
            {

                throw e;
            }
        }
        #endregion

        #region 删除
        /// <summary>
        /// 异步删除单个记录
        /// </summary>
        /// <typeparam name="TAggregateRoot"></typeparam>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public int DeleteSingleIndexAsync<TAggregateRoot>(Expression<Func<TAggregateRoot, bool>> filter)
        {
            try
            {
                var collection = Db.GetCollection<TAggregateRoot>(typeof(TAggregateRoot).Name);
                return (int)collection.DeleteOneAsync(filter).Result.DeletedCount;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #endregion

        #region 查询
        /// <summary>
        /// 异步查询单条记录
        /// </summary>
        /// <typeparam name="TAggregateRoot"></typeparam>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public TAggregateRoot FindSingleIndexAsync<TAggregateRoot>(Expression<Func<TAggregateRoot, bool>> filter)
        {
            try
            {
                var collection = Db.GetCollection<TAggregateRoot>(typeof(TAggregateRoot).Name);
                return collection.FindAsync(filter).Result.FirstOrDefault();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// <summary>
        /// 异步查询整个集合
        /// </summary>
        /// <typeparam name="TAggregateRoot"></typeparam>
        /// <param name="filter"></param>
        /// <returns></returns>
        public List<TAggregateRoot> FindManyAsync<TAggregateRoot>(Expression<Func<TAggregateRoot, bool>> filter)
        {
            try
            {
                var collection = Db.GetCollection<TAggregateRoot>(typeof(TAggregateRoot).Name);
                return collection.FindAsync(filter).Result.ToList();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #endregion

        #region 更新
        /// <summary>
        /// 异步更新单个值
        /// </summary>
        /// <typeparam name="TAggregateRoot"></typeparam>
        /// <param name="filter"></param>
        /// <param name="field"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public int UpdateSingleAsync<TAggregateRoot>(Expression<Func<TAggregateRoot, bool>> filter, string name, string parameter)
        {
            try
            {
                var collection = Db.GetCollection<TAggregateRoot>(typeof(TAggregateRoot).Name);
                var set = Builders<TAggregateRoot>.Update.Set(name, parameter);
                return (int)collection.UpdateOneAsync(filter, set).Result.ModifiedCount;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// <summary>
        ///异步更新多个值
        /// </summary>
        /// <typeparam name="TAggregateRoot"></typeparam>
        /// <param name="filter"></param>
        /// <param name="Root"></param>
        /// <param name="property"></param>
        /// <param name="replace"></param>
        public int UpdateManyAsync<TAggregateRoot>(Expression<Func<TAggregateRoot, bool>> filter, TAggregateRoot Root, List<string> property = null, bool replace = false)
        {
            try
            {
                var collection = Db.GetCollection<TAggregateRoot>(typeof(TAggregateRoot).Name);
                var type = Root.GetType();
                //修改集合
                var list = new List<UpdateDefinition<TAggregateRoot>>();
                foreach (var propert in type.GetProperties())
                {
                    if (propert.Name.ToLower() != "id")
                    {
                        try
                        {
                            if (property == null && property.Count < 1 || property.Any(o => o.ToLower() == propert.Name.ToLower()))
                            {
                                var replaceValue = propert.GetValue(Root);
                                if (replaceValue != null)
                                    list.Add(Builders<TAggregateRoot>.Update.Set(propert.Name, replaceValue));
                                else if (replace)
                                    list.Add(Builders<TAggregateRoot>.Update.Set(propert.Name, replaceValue));
                            }
                        }
                        catch (Exception)
                        {
                            if (property == null)
                            {
                                var replaceValue = propert.GetValue(Root);
                                if (replaceValue != null)
                                    list.Add(Builders<TAggregateRoot>.Update.Set(propert.Name, replaceValue));
                                else if (replace)
                                    list.Add(Builders<TAggregateRoot>.Update.Set(propert.Name, replaceValue));
                            }
                        }

                    }
                }
                if (list.Count > 0)
                {
                    var builders = Builders<TAggregateRoot>.Update.Combine(list);
                    return (int)collection.UpdateOneAsync(filter, builders).Result.ModifiedCount;
                }
                return 0;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #endregion
        #endregion
    }
}
