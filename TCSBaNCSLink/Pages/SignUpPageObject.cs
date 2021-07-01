using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.IO;

namespace Lawpath_signup_tests.Pages
{
    public class SignUpPageObject
    {
        //This project could have been done in simply steps definition + feature file, however setting up a class for page object and another class for creating the driver instance
        //is a much cleaner approach, and this is the better practice for autoamtion. Actual ways of user interacting with the webpage will be simulated with public methods, and
        //for things that a user shouldn't be doing (such as things like finding the First Name field with the name attribute "firstName", should be private methods)
        
        //The URL of the calculator to be opened in the browser
        private const string SignUpUrl = "https://staging.lawpath.com.au/register";

        //The Selenium web driver to automate the browser
        private readonly IWebDriver _webDriver;

        //The default wait time in seconds for wait.Until
        public const int DefaultWaitInSeconds = 15;

        public SignUpPageObject(IWebDriver webDriver)
        {
            _webDriver = webDriver;
        }

        //Finding elements by name
        private IWebElement firstName => _webDriver.FindElement(By.Name("firstName"));
        private IWebElement lastName => _webDriver.FindElement(By.Name("lastName"));
        private IWebElement phone => _webDriver.FindElement(By.Name("phone"));
        private IWebElement email => _webDriver.FindElement(By.Name("email"));
        private IWebElement password => _webDriver.FindElement(By.Name("password"));
        private IWebElement signUpBtn => _webDriver.FindElement(By.Id("signup-submit"));

        private IWebElement emailDuplicateError;


        private IWebElement FirstNumberElement => _webDriver.FindElement(By.Id("first-number"));
        private IWebElement SecondNumberElement => _webDriver.FindElement(By.Id("second-number"));
        private IWebElement AddButtonElement => _webDriver.FindElement(By.Id("add-button"));
        private IWebElement ResultElement => _webDriver.FindElement(By.Id("result"));
        private IWebElement ResetButtonElement => _webDriver.FindElement(By.Id("reset-button"));

        public void EnterFirstName(string s)
        {
            //Clear text box
            firstName.Clear();
            //Enter text
            firstName.SendKeys(s);
        }

        public void EnterLastName(string s)
        {
            lastName.Clear();
            lastName.SendKeys(s);
        }

        public void EnterPhone(string s)
        {
            phone.Clear();
            phone.SendKeys(s);
        }

        public void EnterEmail(string s)
        {
            email.Clear();
            email.SendKeys(s);
        }

        public void EnterPassword(string s)
        {
            password.Clear();
            password.SendKeys(s);
        }

        public void ClickSignUp()
        {
            signUpBtn.Click();
        }

        public void LocateToSignUp()
        {
            //change url to the register page
            if (_webDriver.Url != SignUpUrl)
            {
                _webDriver.Url = SignUpUrl;
            }
        }

        public void WaitForSuccessSignUpPage()
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(DefaultWaitInSeconds));
                //Upon successful sign up there should be a pop-up with an h3 header saying Welcome to Lawpath
                IWebElement welcomeHeader = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("/html/body/div[9]/div/div/div/span/div/div/div[1]/h3[1]")));
                if (welcomeHeader.Text != "Welcome to Lawpath!")
                    throw new Exception();
            }
            catch
            { 
                throw new ElementNotVisibleException();
            }
        }

        public void WaitForDuplicateEmailErrorMsg()
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(DefaultWaitInSeconds));
                //If duplicate email there'll be an error presented on screen
                emailDuplicateError = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//small[@class='color-wild-passion']")));
            }
            catch
            {
                throw new ElementNotVisibleException();
            }
        }

        public void WaitForBadlyFormattedEMailErrorMsg()
        {
            try
            {
                //Since the error pop-up doesn't exist in the DOM (it's probably shown via some js code)
                //This part is abit tricky and I chose the approach of taking a screenshot and verifying in report
                //the screenshot can be found in .\TestResults\testresults, this screenshot will be updated everytime the test is run
                //also if url hasn't been changed after sign up button is clicked then it suggests that there's an error

                //Timeout for 2secs for error pop-up to appear
                System.Threading.Thread.Sleep(2000);

                //Take screenshot
                TakeScreenshot(_webDriver);                

            }
            catch
            {
                throw new ElementNotVisibleException();
            }
        }

        public string GetDuplicaationErrorText()
        {
            return emailDuplicateError.Text;
        }


        public string GetUrl()
        {
            return _webDriver.Url;
        }



        private void TakeScreenshot(IWebDriver driver)
        {
            //method to takescreenshot of the current test
            try
            {
 

                var artifactDirectory = Path.Combine(Directory.GetCurrentDirectory(), "testresults");
                if (!Directory.Exists(artifactDirectory))
                    Directory.CreateDirectory(artifactDirectory);


                ITakesScreenshot takesScreenshot = driver as ITakesScreenshot;

                if (takesScreenshot != null)
                {
                    var screenshot = takesScreenshot.GetScreenshot();

                    string screenshotFilePath = Path.Combine(artifactDirectory, "BadlyFormattedEmail_screenshot.png");

                    screenshot.SaveAsFile(screenshotFilePath);

                    Console.WriteLine("Screenshot: {0}", new Uri(screenshotFilePath));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while taking screenshot: {0}", ex);
            }
        }
    }
}
