// myapp.js
// self-executing anonymous function
// global object: app
(function (app) {
	app.isDebug = true;

	app.log = function (msg) {
		if (app.isDebug) {
			console.log(msg);
		}
	};



})(window.app = window.app || {});