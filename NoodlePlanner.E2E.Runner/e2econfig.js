"use strict";
exports.config = {
    baseUrl: 'http://localhost:62306/home',
    // NOTE: Before running for the first time download chromedriver binaries via:  > webdriver-manager update
    // See http://www.protractortest.org/#/tutorial
    // with standalone server there is no need to start a server instance manually from command prompt
    seleniumServerJar: 'selenium/selenium-server-standalone-2.53.0.jar',
    framework: 'jasmine2',
    onPrepare: function () {
        browser.manage().timeouts().implicitlyWait(5000); // set implicit wait times in ms
        browser.driver.manage().window().maximize(); // set browser size
        var specReporter = require('jasmine-spec-reporter');
        jasmine.getEnv().addReporter(new specReporter({ displayStacktrace: 'spec' }));
        var jasmine2HtmlReporter = require('protractor-jasmine2-html-reporter');
    },
    capabilities: {
        browserName: 'chrome',
        sharedTestFiles: true,
        maxInstance: 4
    },
    // See https://github.com/jasmine/jasmine-npm/blob/master/lib/jasmine.js for the exact options available.
    jasmineNodeOpts: {
        isVerbose: true,
        showColors: true,
        displayStacktrace: true,
        displaySpecDuration: true,
        print: function () { },
        defaultTimeoutInterval: 120000 // set high to allow debugging
    },
};
//# sourceMappingURL=e2econfig.js.map