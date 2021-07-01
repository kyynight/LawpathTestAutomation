
# Lawpath Test Automation

A project with four automated test cases for the sign-up page

## Prerequisite

[Visual Studio 19](https://visualstudio.microsoft.com/downloads/)

Make sure Chrome is up to date (Version 91 for the time being)

If this is the first time using specflow+runner you'll need to register and activate it before you can run any tests
[Activation Steps](https://docs.specflow.org/en/latest/specflowaccount.html#specflow-runner)


## Run Tests

Tests can be run by opening this project in Visual Studio. On the top bar click on Test->Run All Tests

The automatically generated test report can be found in the ./TestResults/ directory


## Test Cases
```
Case 1
Scenario: User successfully creates a Lawpath account
	Given John locates to Lawpath registration page
	When he enters all the required registration information for new user
	And he hits Sign Up
	Then his Lawpath account is created and is redirected to the welcome page
```
```
Case 2
Scenario: User failed to sign up using a duplicated account
	Given John locates to Lawpath registration page
	When he enters registration information of an existing account
	And he hits Sign Up
	Then an error of account duplication should be thrown
```
```
Case 3
Scenario: User signs up using a badly formatted email
	Given John locates to Lawpath registration page
	When he enters registration information using a badly formatted email
	And he hits Sign Up
	Then an error of badly formatted email should be thrown
```
```
Case 4
Scenario: User Gets a timeout error
	Given John has a bad internet connectivity
	When John locates to Lawpath registration page 
	Then he should get a timeout error
```

## Acceptance criteria
```xml
Case 1
* Users can only submit a form that has all required fields filled out
* Users can only submit a form when all of the fields are filled out correctly
* Users can only register with an email that has never been used
```
```xml
Case 2
* Users that register by using an existing email should fail
* An indication of the duplicated email should be provided on screen
```
```xml
Case 3
* Users that register with a badly formatted email should fail
* Form should never be submitted when input fields are incorrect
* There should be indications letting users know what went wrong
```
```xml
Case 4
* Test case should be failing when a user takes too long to load a page

```

## Important Notes
There are two tricky parts that I have bumped into and the resolutions are as follow:

* For Case 3 the pop-up error can't be inspected and I think that box is created via some javascript and it never exists in the DOM element. My approach for this test case is to test whether the user remains on the same page after submitting (which suggests there's an error), and also take a screenshot. If you have run the test already you'll be able to find the screenshot with the badly formatted email pop-up in the TestResults/testresults/ directory (it is automatically captured when you run that test case).

* For Case 4 since I have to simulate a timeout error, I achieve that via creating a chrome driver with very high latency. Since the web driver can't be closed before the test is completed, case 4 may take a while before the success indication (took me 2.6 minutes to run Case 4) so please be patient.