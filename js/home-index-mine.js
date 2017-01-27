(function () {
	var module = angular.module("homeIndex", ['ngRoute']);

	//define routes
	module.config(function ($routeProvider) {
		$routeProvider.when("/", {
			controller: "topicsController",
			templateUrl: "/templates/topicsView.html"  //external template downloaded from server
		});

		$routeProvider.otherwise("/");

	});

	module.controller('topicsController', function ($scope, $http) {
		$scope.dataCount = 0;
		$scope.data = [];
		$scope.isBusy = true;

		$http.get("/api/v1/topics?includeReplies=true")
			.then(function (result) {
				// Successful
				angular.copy(result.data, $scope.data);
				$scope.dataCount = $scope.data.length;
			},
			function () {
				// Error
				alert("could not load topics");
			})
			.then(function () {
				$scope.isBusy = false;
			});
	});
}());
