using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KuRuMi.Mio.DoMain.Model.BaseModel
{
    /// <summary>
    /// 聚合根
    /// </summary>
    public abstract class AggregateRoot : IAggregateRoot
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)] //SqlServerID
        [BsonId(IdGenerator = typeof(GuidGenerator)), BsonRepresentation(BsonType.String)] //MongoDBId
        public Guid Id{ get;set;}
    }
}
