using System;
using TechTalk.SpecFlow;
using Lawpath_signup_tests.Drivers;
using Lawpath_signup_tests.Pages;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Lawpath_signup_tests.Steps
{
    [Binding]
    public class LawPathTestsStepDefinitions
    {
        private readonly SignUpPageObject _signUpPageObject;
        public ChromeDriver driver;
        public WebDriverWait wait;

        public LawPathTestsStepDefinitions(BrowserDriver browserDriver)
        {
            _signUpPageObject = new SignUpPageObject(browserDriver.Current);
        }

        [Given(@"John is on Lawpath registration page")]       
        public void GivenJohnIsOnLawpathRegistrationPage()
        {
            _signUpPageObject.LocateToSignUp();
        }

        [When(@"he enters all the required registration information ""(.*)"" , ""(.*)"", ""(.*)"", ""(.*)"" and ""(.*)""")]
        [When(@"he enters registration information with a badly formatted email ""(.*)"" , ""(.*)"", ""(.*)"", ""(.*)"" and ""(.*)""")]
        [When(@"he enters registration information of an existing account ""(.*)"" , ""(.*)"", ""(.*)"", ""(.*)"" and ""(.*)""")]
        public void WhenHeEntersAllTheRequiredRegistrationInformationAnd(string firstName, string lastName, string phone, string email, string password)
        {
            //Enter all required information
            _signUpPageObject.EnterFirstName(firstName);
            _signUpPageObject.EnterLastName(lastName);
            _signUpPageObject.EnterPhone(phone);
            _signUpPageObject.EnterEmail(email);
            _signUpPageObject.EnterPassword(password);
        }


        [When(@"he enters all the required registration information for new user ""(.*)"" , ""(.*)"", ""(.*)"" and ""(.*)""")]
        public void WhenHeEntersAllTheRequiredRegistrationInformationForNewUserAnd(string firstName, string lastName, string phone, string password)
        {
            //Registering new user needs a seperate method for randomising the email variable so that it can autogenerate a new user without the need of hard coding everytime
            //randomising email with datetime
            string email = DateTime.Now.ToFileTime() + "@hightable.com";
            _signUpPageObject.EnterFirstName(firstName);
            _signUpPageObject.EnterLastName(lastName);
            _signUpPageObject.EnterPhone(phone);           
            _signUpPageObject.EnterEmail(email);
            _signUpPageObject.EnterPassword(password);
        }





        [When(@"he hits Sign Up")]
        public void WhenHeHitsSignUp()
        {
            _signUpPageObject.ClickSignUp();
        }


        [Then(@"his Lawpath account is created and is redirected to the welcome page")]
        public void ThenHisLawpathAccountIsCreated()
        {
            try
            {
                //Both methods below contains validation of whether the user is on the welcome page
                _signUpPageObject.WaitForSuccessSignUpPage();
                //Assert.AreEqual(_signUpPageObject.GetUrl(), "https://staging-my.lawpath.com/");
            }
            catch
            {
                Assert.Fail();
            }
        }

        [Then(@"an error of account duplication should be thrown")]
        public void ThenAnErrorOfAccountDuplicationShouldBeThrown()
        {
            try
            {
                _signUpPageObject.WaitForDuplicateEmailErrorMsg();
                //Depends how often the text for the error is changed, if it's changed frequently we can simply check whether an error exist and skip the step below
                Assert.AreEqual(_signUpPageObject.GetDuplicaationErrorText(), "Email address already registered in our system. Please try again.");
            }
            catch
            {
                Assert.Fail();
            }
        }

        [Then(@"an error of badly formatted email should be thrown")]
        public void ThenAnErrorOfBadlyFormattedEmailShouldBeThrown()
        {
            try
            {
                _signUpPageObject.WaitForBadlyFormattedEMailErrorMsg();

                //Wait for 2secs
                System.Threading.Thread.Sleep(2000);
                //Check if url remains on the register page which suggests an error
                Assert.AreEqual(_signUpPageObject.GetUrl(), "https://staging.lawpath.com.au/register");
            }
            catch
            {
                Assert.Fail();
            }
        }

    }
}
