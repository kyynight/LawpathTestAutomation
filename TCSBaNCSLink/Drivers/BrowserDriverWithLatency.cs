using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace LawpathTestAutomation.Drivers
{
    public class BrowserDriverWithLatency:IDisposable
    {
        private readonly Lazy<IWebDriver> _currentWebDriverLazy;
        private bool _isDisposed;

        public BrowserDriverWithLatency()
        {
            _currentWebDriverLazy = new Lazy<IWebDriver>(CreateWebDriver);
        }

        /// The Selenium IWebDriver instance
        public IWebDriver Current => _currentWebDriverLazy.Value;

        /// Creates the Selenium web driver (opens a browser)
        private IWebDriver CreateWebDriver()
        {

            var chromeDriver = new ChromeDriver("..\\TCSBaNCSLink\\Drivers\\");
            chromeDriver.NetworkConditions = new ChromeNetworkConditions()
            { DownloadThroughput = 5000, UploadThroughput = 5000, Latency = TimeSpan.FromMilliseconds(500) };

            return chromeDriver;
        }


        /// Disposes the Selenium web driver (closing the browser) after the Scenario completed
        public void Dispose()
        {
            if (_isDisposed)
            {
                return;
            }

            if (_currentWebDriverLazy.IsValueCreated)
            {
                Current.Quit();
            }

            _isDisposed = true;
        }
    }
}
