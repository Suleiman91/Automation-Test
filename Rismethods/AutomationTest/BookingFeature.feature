Feature: BookingFeature
	Validate funnctionality of booking feature
	on a travel website




Background: User is logged in
    Given A user navigate to login page
    When Enter credentials
	| UserName            | Password |
	| user@phptravels.com | demouser |
    And and submit the login
    Then user should be logged in then return home
@Booking

Scenario: Check that search result for the flight resrvation system are sorted from lowest to highest, 
          check also that starting point and destination are the same as user input
	Given I am on the home page
	When I select flight type
	And I enter the required information
	| StartPoint | Destination | DepartureAfterSpecificDays | AdultPassengerNumber | ChildsPassngerNumber |
	| New York   | Munich      | 14                         | 2                    | 2                    |
	And I click search and wait to search result to be loaded
	Then The list of available flights should be ascendingly ordered
	And flights directions must match user input
	| StartPoint | Destination |
	| New York   | Munich      |

Scenario: Make sure that booking summary informatoin when user choose a flight are 
	          correct and match user choice
	Given I am on the home page
	When I select flight type
	And I enter the required information
	| StartPoint | Destination | DepartureAfterSpecificDays | AdultPassengerNumber | ChildsPassngerNumber |
	| New York   | Munich      | 14                         | 2                    | 2                    |
	And I click search and wait to search result to be loaded
	Then The list of available flights should be ascendingly ordered
	When I select the desired flight and click book now, for example number 1
	Then I should be directed to the summary page
	And All information must match user choice including the below
	| DepartureDate    | ArrivalDate      | From     | To     | TotalAmount |
	| 2020-04-15 14:30 | 2020-04-17 07:00 | New York | Munich | 1562        |

Scenario: Apply my own alghorithm on flights result to make use of it and 
          reserve the flight with the shortest time, and reserve for this flight
	Given I am on the home page
	When I select flight type
	And I enter the required information
	| StartPoint | Destination | DepartureAfterSpecificDays | AdultPassengerNumber | ChildsPassngerNumber |
	| New York   | Munich      | 14                         | 2                    | 2                    |
	And I click search and wait to search result to be loaded
	Then The list of available flights should be ascendingly ordered
	When Now i will select my ticket based own my algorithm, so i'll choose shortest one
	Then I should be directed to the summary page
	When I fill billing infomation and payment information as below
	| Title | Name     | Surname     | Email           | Phone         | Birthday   | ExpirationDate | Nationality | CardType | CardNumber | CardExpiryYear | CVV | PassportNumber |
	| Mr    | testName | testSurname | test.sdf@df.com | 0172384332732 | 2020-01-01 | 2021           | AFGHANISTAN | AX       | 3sddr3sdr  | 2022           | 123 | 3322135436543  |
	When check about accepting the rules
	And submitting the form
	Then result will be printed out about amount of ours