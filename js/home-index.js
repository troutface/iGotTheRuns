(function () {
	var module = angular.module("homeIndex", ['ngRoute']);

	//define routes
	module.config(function ($routeProvider) {
		$routeProvider
			.when("/", {
				controller: "topicsController",
				templateUrl: "/templates/topicsView.html"  //external template downloaded from server
			})
			.when("/newmessage", {
				controller: "newTopicController",
				templateUrl: "/templates/newTopicView.html"  //external template downloaded from server
			})
			.when("/message/:id", {
				controller: "singleTopicController",
				templateUrl: "/templates/singleTopicView.html"
			})
			.otherwise({ redirectTo: "/" });

	});

	module.factory("dataService", function ($http, $q) {
		var _topics = [];
		var _isInit = false;

		var _isReady = function () {
			return _isInit;
		};

		var _getTopics = function () {

			var deferred = $q.defer();

			$http.get("/api/v1/topics?includeReplies=true")
			.then(function (result) {
				// Successful
				angular.copy(result.data, _topics);
				_isInit = true;
				deferred.resolve();
			},
			function () {
				// Error. Callback to whoever called this service.
				deferred.reject();
			});

			return deferred.promise;
		};

		var _addTopic = function (newTopic) {
			var deferred = $q.defer();

			$http.post("/api/v1/topics", newTopic)
				.then(function (result) {
					// success
					var newlyCreatedTopic = result.data;
					// merge with existing list of topics
					_topics.splice(0, 0, newlyCreatedTopic);
					deferred.resolve(newlyCreatedTopic);
				},
				function () {
					// error
					deferred.reject();
				});

			return deferred.promise;
		};

		// private function. not exposed outside of the service.
		function _findTopic(id) {
			var found = null;

			// using jQuery bcz it allows us to break from the loop when we find our item. Angular does not.
			// if expensive, maybe create a dictionary object.
			$.each(_topics, function (i, item) {
				if (item.id == id) {
					found = item;
					return false;
				}
			});

			return found;
		}

		var _getTopicById = function (id) {
			var deferred = $q.defer();

			if (_isReady()) {
				var topic = _findTopic(id);
				if (topic) {
					deferred.resolve(topic);
				} else {
					deferred.reject();
				}
			} else {
				_getTopics()
					.then(function (result) {
						// success
						var topic = _findTopic(id);
						if (topic) {
							deferred.resolve(topic);
						} else {
							deferred.reject();
						}
					},
					function () {
						// Error
						deferred.reject();
					});
			}
			return deferred.promise;
		};

		var _saveReply = function (topic, newReply) {
			var deferred = $q.defer();

			$http.post("/api/v1/topics/" + topic.id + "/replies", newReply)
			  .then(function (result) {
			  	// success
			  	// if first time, topic may be null
			  	if (topic.replies == null) {
			  		topic.replies = [];
			  	}
			  	topic.replies.push(result.data);
			  	deferred.resolve(result.data);
			  },
			  function () {
			  	// error
			  	deferred.reject();
			  });
			return deferred.promise;
		};

		return {
			// exposed properties & objects from this service
			// public interface
			topics: _topics,
			getTopics: _getTopics,
			addTopic: _addTopic,
			isReady: _isReady,
			getTopicById: _getTopicById,
			saveReply: _saveReply
		};
	});

	module.controller('topicsController', function ($scope, $http, dataService) {
		$scope.data = dataService;
		$scope.isBusy = false;

		//avoid calling service & hitting server each time 
		if (dataService.isReady() === false) {
			$scope.isBusy = true;
			dataService.getTopics()
				.then(function (result) {
					// success
				},
				function () {
					// Error
					alert("could not load topics");
				})
				.then(function () {
					$scope.isBusy = false;
				});
		}
	});

	module.controller('newTopicController', function ($scope, $http, $window, dataService) {
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

		$scope.newTopic = {};

		$scope.save = function () {
			dataService.addTopic($scope.newTopic)
				.then(function () {
					// success
					$window.location = "#/";
				},
				function () {
					// error
					alert('could not save the new topic');
				});
		};
	});

	module.controller('singleTopicController', function ($scope, $http, $window, dataService, $routeParams) {
		$scope.topic = null;
		$scope.newReply = {};

		dataService.getTopicById($routeParams.id)
			.then(function (topic) {
				// success
				$scope.topic = topic;
			},
			function () {
				// error
				$window.location = "#/";  // returns to the main view
			});

		$scope.addReply = function () {
			dataService.saveReply($scope.topic, $scope.newReply)
			  .then(function () {
			  	// success
			  	$scope.newReply.body =  "";
			  },
			  function () {
			  	// error
			  	alert("Could not save the new reply");
			  });
		};
	});
}());
