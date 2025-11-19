Feature: Main page user actions

  Scenario Outline: User performs some actions from Main page
    Given user is on 'Main' page

    When user clicks "Search Button"
    And user enters '<first_search>' text
    And user clicks "Find Button"
    Then search results should be present

    When user clicks "Search Button"
    And user enters '<second_search>' text
    And user clicks "Find Button"
    Then search results should not be present

    When user hovers over "Contact Us Button"
    Then hand pointer appears over "Contact Us Button"

    When user clicks "Contact Us Button"
    Then user should be navigated to "<contact_url>"

    When user clicks "Hamburger Menu Button"
    Then "Hamburger Menu Services Link" should be present

  Examples:
    | first_search | second_search    | contact_url                                               |
    | Automation   | gdfgdfgdfgdfg    | https://www.epam.com/about/who-we-are/contact             |
    | AI           | qwertyuiop       | https://www.epam.com/about/who-we-are/contact             |

