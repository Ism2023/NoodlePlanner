export var config = {
    baseUrl: 'http://localhost:62306/home',
    // NOTE: Before running for the first time download chromedriver binaries via:  > webdriver-manager update
    // See http://www.protractortest.org/#/tutorial
    // with standalone server there is no need to start a server instance manually from command prompt
    seleniumServerJar: 'selenium/selenium-server-standalone-2.53.0.jar',
    framework: 'jasmine2',

    onPrepare: () => {

        browser.manage().timeouts().implicitlyWait(5000)    // set implicit wait times in ms
        browser.driver.manage().window().maximize();        // set browser size

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
        showColors: true,               // Use colors in the command line report
        displayStacktrace: true,
        displaySpecDuration: true,
        print() { },                    // overrides jasmine's print method to report dot syntax for custom reports
        defaultTimeoutInterval: 120000  // set high to allow debugging
    },

    //specs: ['./specs/referral/worklist.spec.js']
    //specs: ['./specs/accountManagement/updateUserDetails.spec.js']     
    //specs: ['./specs/administration/users/userManagement.spec.js']
    //specs: ['./specs/administration/roles/roleManagement.spec.js']
};