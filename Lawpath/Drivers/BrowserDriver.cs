using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Lawpath_signup_tests.Drivers
{
    public class BrowserDriver : IDisposable
    {
        private readonly Lazy<IWebDriver> _currentWebDriverLazy;

        public BrowserDriver()
        {
            _currentWebDriverLazy = new Lazy<IWebDriver>(CreateWebDriver);
        }

        /// The Selenium IWebDriver instance
        public IWebDriver Current => _currentWebDriverLazy.Value;

        /// Creates the Selenium web driver (opens a browser)
        private IWebDriver CreateWebDriver()
        {
            var chromeDriver = new ChromeDriver("..\\Lawpath\\Drivers\\");

            return chromeDriver;
        }

        /// Disposes the Selenium web driver (closing the browser) after the Scenario completed
        public void Dispose()
        {
            Current.Quit();
        }
    }
}
