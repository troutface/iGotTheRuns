/// <reference path="c:\users\g2harri\documents\toolbox\igottheruns\igottheruns.tests\scripts\jasmine.js" />
/// <reference path="../../js/myapp.js" />

describe("myapp tests->", function () {

	it("isDebug", function () {
		expect(app.isDebug).toEqual(true);
	});

	it("log", function () {
		expect(app.log).toBeDefined();
		app.log("testing");
	});


});