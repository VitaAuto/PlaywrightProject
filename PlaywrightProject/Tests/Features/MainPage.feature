Feature: Main page user actions

  Scenario Outline: User performs transitions from Hamburger menu on Main page
    Given user opens 'Main' page
    Then "Cookie Consent popup" should be present
    
    When user clicks "Accept All Button"
    Then "Cookie Consent popup" should be hidden

    When user clicks "Hamburger Menu Button"
    Then following options should be present:
    | Hamburger Menu Services Link   |
    | Hamburger Menu Industries Link |
    | Hamburger Menu Insights Link   |
    | Hamburger Menu About Link      |
    | Hamburger Menu Careers Link    |
    
    When user clicks "Hamburger Menu Services Link"
    Then user should be navigated to 'Services' page

    Given user opens 'Main' page
    When user clicks "Hamburger Menu Button"
    And user clicks "Hamburger Menu Insights Link"
    Then user should be navigated to 'Insights' page

    Given user opens 'Main' page
    When user clicks "Hamburger Menu Button"
    And user clicks "Hamburger Menu About Link"
    Then user should be navigated to 'About' page

    Given user opens 'Main' page
    When user clicks "Hamburger Menu Button"
    And user clicks "Hamburger Menu Careers Link"
    Then user should be navigated to 'Careers' page

    
  Scenario Outline: User performs search from every page
    Given user opens 'Main' page
    Then "Cookie Consent popup" should be present
    
    When user clicks "Accept All Button"
    Then "Cookie Consent popup" should be hidden

    Given user opens '<page>' page
    Then user should be navigated to '<page>' page

    When user clicks "Search Button"
    And user enters '<first_search>' text
    And user clicks "Find Button"
    Then search results should be present

    When user clicks "Search Button"
    And user enters '<second_search>' text
    And user clicks "Find Button"
    Then search results should not be present

  Examples:
    |  page  | first_search | second_search |
    |Main    | Automation   | udf8dfgdfg123 |
    |Services| AI           | qwertyu000    |
    |Insights| AI           | 0129834765    |
    |About   | RPA          | QaWsDFDFDD    |
    |Careers | DevOps       | Q555RRqwer    |