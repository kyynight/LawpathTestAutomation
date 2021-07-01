Feature: TimeOut
	In order to maintain a healthy connection
	As a user with bad internet connectivity
	I should be timed out by the website

@mytag
Scenario: User Gets a timeout error
	Given John has a bad internet connectivity
	When John locates to Lawpath registration page 
	Then he should get a timeout error