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

'use strict'
interface IBasePage {
    goTo: () => void;
    isLoaded: () => webdriver.promise.Promise<{}>;
    pageUrl: (url: string) => void;
    pageLoaded: (locator: protractor.ElementFinder) => void;
}

export default class BasePage implements IBasePage {
     /*
    * Wrappers for expected conditions.
    * ECs are generally poorly named, so we wrap them in methods that are 9% more sexy, and allow us to add logging etc.
    *
    * @returns {ExpectedCondition}
    */
    private ec = protractor.ExpectedConditions;
    // wrap timeout (ms) in t-shirt sizes
    private timeout = {
        'xs': 420,
        's': 1000,
        'm': 2000,
        'l': 5000,
        'xl': 9000,
        'xxl': 15000
    };

    private url: string;
    private pageLoadedLocator: protractor.ElementFinder;
    pageUrl(url: string): void {
        this.url = url;
    }
    pageLoaded(locator: protractor.ElementFinder): void {
        this.pageLoadedLocator = locator;
    }
    goTo(): void {
        browser.get(this.url, this.timeout.xl);
    }

    isLoaded(): webdriver.promise.Promise<{}> {
        return browser.wait(this.inDom(this.pageLoadedLocator), this.timeout.xl);
    }

    inDom(locator: protractor.ElementFinder): webdriver.until.Condition<{}> {
        return this.ec.presenceOf(locator);
    }

    notInDom(locator: protractor.ElementFinder): webdriver.until.Condition<{}> {
        return this.ec.stalenessOf(locator);
    }

    // helpers
    isVisible(locator: protractor.ElementFinder): webdriver.until.Condition<{}> {
        return this.ec.visibilityOf(locator);
    }

    isNotVisible(locator: protractor.ElementFinder): webdriver.until.Condition<{}> {
        return this.ec.invisibilityOf(locator);
    }

    isClickable(locator: protractor.ElementFinder): webdriver.until.Condition<{}> {
        return this.ec.elementToBeClickable(locator);
    }

    hasText(locator: protractor.ElementFinder, text: string): webdriver.until.Condition<{}> {
        return this.ec.textToBePresentInElement(locator, text);
    }

    and(arrayOfFunctions: any): webdriver.until.Condition<{}> {
        return this.ec.and(arrayOfFunctions);
    }

    titleIs(title: string): webdriver.until.Condition<{}> {
        return this.ec.titleIs(title);
    }
}
