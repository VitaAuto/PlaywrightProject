@smoke
Feature: Smoke tests

  Scenario Outline: User can open Main page
    Given user opens 'Main' page
    Then user should be navigated to 'Main' page

  Scenario Outline: User can open Services page
    Given user opens 'Services' page
    Then user should be navigated to 'Services' page

  Scenario Outline: User can open Insights page
    Given user opens 'Insights' page
    Then user should be navigated to 'Insights' page

  Scenario Outline: User can open About page
    Given user opens 'About' page
    Then user should be navigated to 'About' page
  
  Scenario Outline: User can open Careers page
    Given user opens 'Careers' page
    Then user should be navigated to 'Careers' page

