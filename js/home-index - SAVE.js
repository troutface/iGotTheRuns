(function homeIndexController() {
	angular.module('iGotTheRuns', [])
	.controller('homeIndexController', function ($scope, $http) {
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
