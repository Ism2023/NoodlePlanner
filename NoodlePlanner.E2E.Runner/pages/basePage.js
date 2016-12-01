/*
https://www.typescriptlang.org/docs/handbook/namespaces-and-modules.html
Just like namespaces, modules can contain both code and declarations. The main difference is that modules declare their dependencies.
Modules also have a dependency on a module loader (such as CommonJs/Require.js)

It is also worth noting that, for Node.js applications, modules are the default and the recommended approach to structure your code.

A key feature of modules in TypeScript is that two different modules will never contribute names to the same scope.
Because the consumer of a module decides what name to assign it, there’s no need to proactively wrap up the exported symbols in a namespace.

To reiterate why you shouldn’t try to namespace your module contents, the general idea of namespacing is to provide logical grouping
of constructs and to prevent name collisions. Because the module file itself is already a logical grouping, and its top-level name is
defined by the code that imports it, it’s unnecessary to use an additional module layer for exported objects.

Just as there is a one-to-one correspondence between JS files and modules, TypeScript has a one-to-one correspondence between module
source files and their emitted JS files. One effect of this is that it’s not possible to use the --outFile compiler switch to
concatenate multiple module source files into a single JavaScript file
*/
'use strict';
var BasePage = (function () {
    function BasePage() {
        /*
       * Wrappers for expected conditions.
       * ECs are generally poorly named, so we wrap them in methods that are 9% more sexy, and allow us to add logging etc.
       *
       * @returns {ExpectedCondition}
       */
        this.ec = protractor.ExpectedConditions;
        // wrap timeout (ms) in t-shirt sizes
        this.timeout = {
            'xs': 420,
            's': 1000,
            'm': 2000,
            'l': 5000,
            'xl': 9000,
            'xxl': 15000
        };
    }
    BasePage.prototype.pageUrl = function (url) {
        this.url = url;
    };
    BasePage.prototype.pageLoaded = function (locator) {
        this.pageLoadedLocator = locator;
    };
    BasePage.prototype.goTo = function () {
        browser.get(this.url, this.timeout.xl);
    };
    BasePage.prototype.isLoaded = function () {
        return browser.wait(this.inDom(this.pageLoadedLocator), this.timeout.xl);
    };
    BasePage.prototype.inDom = function (locator) {
        return this.ec.presenceOf(locator);
    };
    BasePage.prototype.notInDom = function (locator) {
        return this.ec.stalenessOf(locator);
    };
    // helpers
    BasePage.prototype.isVisible = function (locator) {
        return this.ec.visibilityOf(locator);
    };
    BasePage.prototype.isNotVisible = function (locator) {
        return this.ec.invisibilityOf(locator);
    };
    BasePage.prototype.isClickable = function (locator) {
        return this.ec.elementToBeClickable(locator);
    };
    BasePage.prototype.hasText = function (locator, text) {
        return this.ec.textToBePresentInElement(locator, text);
    };
    BasePage.prototype.and = function (arrayOfFunctions) {
        return this.ec.and(arrayOfFunctions);
    };
    BasePage.prototype.titleIs = function (title) {
        return this.ec.titleIs(title);
    };
    return BasePage;
}());
Object.defineProperty(exports, "__esModule", { value: true });
exports.default = BasePage;
//# sourceMappingURL=basePage.js.map