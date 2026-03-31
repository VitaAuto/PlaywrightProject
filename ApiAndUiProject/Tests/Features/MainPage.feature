@ui
Feature: Main page user actions

  Scenario Outline: Hamburger menu transitions and options on Main page
    Given user opens 'Main' page
    Then "Cookie Consent popup" should be present
    
    When user clicks "Accept All Button"
    Then "Cookie Consent popup" should be hidden

    When user clicks "Hamburger Menu Button"
    Then "Hamburger Menu" contains 5 options
    Then following options should be present:
    | Hamburger Menu Services Link   |
    | Hamburger Menu Industries Link |
    | Hamburger Menu Insights Link   |
    | Hamburger Menu About Link      |
    | Hamburger Menu Careers Link    |
    
    When user hovers over "Hamburger Menu Services Link"
    Then hand pointer should appear over "Hamburger Menu Services Link"
    When user clicks "Hamburger Menu Services Link"
    Then user should be navigated to 'Services' page
    
    Given user opens 'Main' page
    When user clicks "Hamburger Menu Button"
    When user hovers over "Hamburger Menu Industries Link"
    Then hand pointer should not appear over "Hamburger Menu Industries Link"
    
    Given user opens 'Main' page
    When user clicks "Hamburger Menu Button"
    And user hovers over "Hamburger Menu Insights Link"
    Then hand pointer should appear over "Hamburger Menu Insights Link"
    When user clicks "Hamburger Menu Insights Link"
    Then user should be navigated to 'Insights' page

    Given user opens 'Main' page
    When user clicks "Hamburger Menu Button"
    And user hovers over "Hamburger Menu About Link"
    Then hand pointer should appear over "Hamburger Menu About Link"
    When user clicks "Hamburger Menu About Link"
    Then user should be navigated to 'About' page

    Given user opens 'Main' page
    When user clicks "Hamburger Menu Button"
    And user hovers over "Hamburger Menu Careers Link"
    Then hand pointer should appear over "Hamburger Menu Careers Link"
    When user clicks "Hamburger Menu Careers Link"
    Then user should be navigated to 'Careers' page

    
  Scenario Outline: User performs search from every page
    Given user opens 'Main' page
    Then "Cookie Consent popup" should be present
    
    When user clicks "Accept All Button"
    Then "Cookie Consent popup" should be hidden

    Given user opens '<page>' page
    Then user should be navigated to '<page>' page

    When user clicks "Search Button"
    And user enters '<first_search>' text in "Search Input"
    And user clicks "Find Button"
    Then search results should be present in "Search Results"

    When user clicks "Search Button"
    And user enters '<second_search>' text in "Search Input"
    And user clicks "Find Button"
    Then search results should not be present in "Search Results"

  Examples:
    |  page  | first_search | second_search |
    |Main    | Automation   | udf8dfgdfg123 |
    |Services| AI           | qwertyu000    |
    |Insights| AI           | 0129834765    |
    |About   | RPA          | QaWsDFDFDD    |
    |Careers | DevOps       | Q555RRqwer    |


Scenario: Default Country Dropdown Placeholder on Careers_Jobs page
    Given user opens 'Careers' page
    Then "Cookie Consent popup" should be present
    
    When user clicks "Accept All Button"
    Then "Cookie Consent popup" should be hidden

    When user hovers over "Start Your Search Here Button"
    Then hand pointer should appear over "Start Your Search Here Button"

    When user clicks "Start Your Search Here Button"
    Then user should be navigated to 'Careers_Jobs' page

    When user clicks "Country Dropdown Clear"
    Then "Country Dropdown Default Placeholder" should be present


    