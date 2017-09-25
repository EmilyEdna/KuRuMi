app.factory('RequestHttp', RequestHttp);

function RequestHttp($http, $q, $rootScope) {
    var ApiData = {};
    ApiData.Myhttp = function (option) {
        debugger;     
        var deferred = $q.defer();
        var request =
            {
                method: 'post',
                url: option.url,
                data: option.data,
                headers: { appid: "2xl7w0Doqog=", sign: "MwOMp5bATVf1N2qNmAxW1GL1mduieOjsLHe45frBuISIpRE9OWncZ569sZraRQnwmWuQHNmJZJjCT/FaWsSiZw==" }
            };
        $http(request)
            .then(function (response) {
                debugger;
                $rootScope.data = response.data;
                deferred.resolve(response);
            }
            , function (response) {
                deferred.reject(response);
            });

        return deferred.promise;
    }
    return ApiData;
};

app.factory('commandData', commandData);

function commandData(RequestHttp, $rootScope) {
    var service = {};
    service.registUser = function (inData) {
        debugger;
        var params = {
            data: inData,
            url: 'http://localhost:13292/Api/User/DataForAjax'
        };
        return RequestHttp.Myhttp(params);
    };
    service.getUserInfo = function () {
        debugger;
        var params = {
            data: null,
            url: 'http://localhost:13292/Api/User/GetAllTheUser'
        };
        return RequestHttp.Myhttp(params);
    }
    return service;
};

app.controller('userCtrl', userCtrl);

function userCtrl($scope, commandData) {
    var init = function () {
        $scope.userinfo = commandData.getUserInfo();
    }
    $scope.post = function () {
        debugger;
        var userName = $scope.userName;
        var email = $scope.email;
        var passWord = $scope.passWord;
        var json = { "userName": userName, "email": email, "passWord": passWord };
        commandData.registUser(json);
    };
    init();
};