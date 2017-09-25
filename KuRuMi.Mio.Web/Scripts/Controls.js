var ctrl = ctrl || {};
var host = "http://localhost:13292/";
//请求头
var head = { appid: "2xl7w0Doqog=", sign: "MwOMp5bATVf1N2qNmAxW1GL1mduieOjsLHe45frBuISIpRE9OWncZ569sZraRQnwmWuQHNmJZJjCT/FaWsSiZw==" };

//请求API
ctrl.ajaxWebApi = function (option) {
    var defaultOption = {
        url: undefined,
        data: undefined,
        type: 'GET',
        dataType: 'json',
        headers: head,
        cache: true,
        anync: true,
        success: null
    };
    var options = $.extend(defaultOption, option);
    return $.ajax({
        url: host + options.url,
        data: option.data,
        type: option.type,
        cache: option.cache,
        async: option.async,
        dataType: option.dataType,
        headers: head,
        success: function (response)
        {
            option.success(response);
        }
    });
}
//请求Controller
ctrl.ajaxController = function (option) {
    var defaultOption = {
        url: undefined,
        data: undefined,
        type: 'GET',
        dataType: 'json',
        headers: head,
        cache: true,
        anync: true,
        success: null
    };
    var options = $.extend(defaultOption, option);
    return $.ajax({
        url:options.url,
        data: option.data,
        type: option.type,
        cache: option.cache,
        async: option.async,
        dataType: option.dataType,
        success: function (response) {
            option.success(response);
        }
    });
}
//提示层
ctrl.msg = function (data) {
    layer.msg(data);
}
//弹出层
ctrl.alert = function (data) {
    layer.alert(data);
}
//对话框
ctrl.open = function (data) {
    layer.open({
        type: 1,
        title: data.info,
        content: "<div class='container-fluid'><span class='btn btn-success glyphicon glyphicon-warning-sign'><text class='text-warning'>" + data.content + "</text></span></div>",
        anim: 1,
        time: 3000,
        btn: ['确认']
    });
}
//确认框
ctrl.confirm = function (data)
{
    layer.confirm(data, {
        icon: 7,
        title: '系统提示',
        btn: ['确认', '取消']
    }, function (index) {
        callBack(true, index);
        layer.close(index);
        window.location.reload();
    }, function (index) {
        callBack(false, index);
        layer.close(index);
    });
}
//提示
ctrl.tips = function (data, i) {
    layer.tips(data, this, { tips: i })
}
//初始化导航
ctrl.Initnav = function () {
    //转换
    $(".panel-heading").click(function (e) {
        /*切换折叠指示图标*/
        $(this).find("span").toggleClass("glyphicon-chevron-down");
        $(this).find("span").toggleClass("glyphicon-chevron-up");
    });
    //用户配置
    $('#user-config').click(function (e) {
        $('#user').css("display", "block");
        $('#img').css("display", "none");
        $('#blog').css("display", "none");
        $('#atricle').css("display", "none");
        $('#role').css("display", "none");
    });
    //图片配置
    $('#img-config').click(function (e) {
        $('#user').css("display", "none");
        $('#img').css("display", "block");
        $('#blog').css("display", "none");
        $('#atricle').css("display", "none");
        $('#role').css("display", "none");
    });
    //博客配置
    $('#blog-config').click(function (e) {
        $('#user').css("display", "none");
        $('#img').css("display", "none");
        $('#blog').css("display", "block");
        $('#atricle').css("display", "none");
        $('#role').css("display", "none");
    });
    //文章配置
    $('#atricle-config').click(function (e) {
        $('#user').css("display", "none");
        $('#img').css("display", "none");
        $('#blog').css("display", "none");
        $('#atricle').css("display", "block");
        $('#role').css("display", "none");
    });
    //博客管理权限配置
    $('#role-config-one').click(function (e) {
        $('#user').css("display", "none");
        $('#img').css("display", "none");
        $('#blog').css("display", "none");
        $('#atricle').css("display", "none");
        $('#role').css("display", "block");
    });
    //文章管理权限配置
    $('#role-config-two').click(function (e) {
        $('#user').css("display", "none");
        $('#img').css("display", "none");
        $('#blog').css("display", "none");
        $('#atricle').css("display", "none");
        $('#role').css("display", "block");
    });
    //初始化编辑器
    $(".summernote").summernote({
        lang: "zh-CN",
        placeholder: "请在此处编辑博客内容",
        height: 200,
        dialogsInBody: true,
        dialogsFade: true
    });
}
//文件上传
ctrl.fileinput = function (selector, ApiUrl) {
    var control = $("#" + selector);
    control.fileinput({
        language: 'zh', //设置语言
        uploadUrl: ApiUrl,//开启ajax上传
        allowedFileExtensions: ['jpg', 'png', 'gif'],//支持上传的文件格式
        showUpload: true,//是否显示上传按钮
        showCaption: false,//是否显示标题
        browseClass: "btn btn-primary", //按钮样式
        //dropZoneEnabled: false,//是否显示拖拽区域
        //minImageWidth: 50, //图片的最小宽度
        //minImageHeight: 50,//图片的最小高度
        //maxImageWidth: 1000,//图片的最大宽度
        //maxImageHeight: 1000,//图片的最大高度
        maxFileSize: 0,//单位为kb，如果为0表示不限制文件大小
        //minFileCount: 0,//最小上传单位
        maxFileCount: 2, //表示允许同时上传的最大文件个数
        enctype: 'multipart/form-data',
        //validateInitialCount: true,
        previewFileIcon: "<i class='glyphicon glyphicon-king'></i>", //图标
        msgFilesTooMany: "选择上传的文件数量({n}) 超过允许的最大数值{m}！",
        uploadExtraData:
        {
            "UserId": localStorage.userinfo
        },
    }
    ).on("fileuploaded", function (event, data) {
        var result = data.response;
    });
}
//时间选择
ctrl.date = function (selector) {
    var $item = $("#" + selector);
    $item.datetimepicker({
        lang: 'ch',
        format: 'Y-m-d H:i:s'
    });
}
//序列化表单
$.fn.serializeObject = function () {
    var o = {};
    var a = this.serializeArray();
    $.each(a, function () {
        if (o[this.name]) {
            if (!o[this.name].push) {
                o[this.name] = [o[this.name]];
            }
            o[this.name].push(this.value || '');
        } else {
            o[this.name] = this.value || '';
        }
    });
    return o;
};