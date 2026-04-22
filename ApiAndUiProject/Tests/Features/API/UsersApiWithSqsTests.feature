@api
Feature: Users API with SQS tests

@smoke
Scenario Outline: Create user with CorrelationId in SQS
    Given user is logged in
    And I have user with first name "<FirstName>", last name "<LastName>", email "<Email>", is active <IsActive>
    And user email "<Email>" is unique
    When I send POST request to create user
    Then response status should be <Status>
    And message with CorrelationId should be present in SQS
    And SQS message body should match user with first name "<FirstName>", last name "<LastName>", email "<Email>", is active <IsActive>
    And message with CorrelationId is cleared in SQS

    Examples:
      | FirstName | LastName | Email            | IsActive | Status |
      | Kerry     | Loman    | Kerry@mail.com   | true     | 201    |