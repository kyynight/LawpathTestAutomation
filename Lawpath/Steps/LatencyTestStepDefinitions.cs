using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;
using Lawpath_signup_tests.Drivers;
using Lawpath_signup_tests.Pages;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace LawpathTestAutomation.Steps
{
    [Binding]
    public sealed class LatencyTestStepDefinitions
    {
        //create a chromedriver and simulate latency via setting up network conditions
        ChromeDriver driver = new ChromeDriver("..\\Lawpath\\Drivers\\");
        bool isTimeout = false;

        [Given(@"John has a bad internet connectivity")]
        public void JohnHasBadInternet()
        {
            driver.NetworkConditions = new ChromeNetworkConditions()
            { DownloadThroughput = 3, UploadThroughput = 3, Latency = TimeSpan.FromMilliseconds(1) };
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }

        [When(@"John locates to Lawpath registration page")]
        public void WhenJohnLocatesToLawpathRegistrationPage()
        {
            try
            {
                driver.Navigate().GoToUrl("https://staging.lawpath.com.au/register");
            }
            catch (Exception e)
            {
                //if we end up in this block then it means there's a successful timeout exception thrown
                isTimeout = true;
            }
        }

        [Then(@"he should get a timeout error")]
        public void ThenHeShouldGetATimeoutError()
        {
            if (isTimeout == false)
            {
                //fail if there wasn't a timeout
                Assert.Fail();
                driver.Quit();
            }
            else
            {
                driver.Quit();
            }
        }


        


    }
}
