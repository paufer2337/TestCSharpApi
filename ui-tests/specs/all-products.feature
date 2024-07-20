Feature: Display all products

  Scenario Outline: Check that the product "<product>" is shown when the "Alla" category is selected
    Given I am on the homepage
    When I select "Alla" from the category selector
    Then I should see the product "<product>" listed

  Examples:
    | product               |
    | Belgisk Malinois      |
    | Tysk Schäfer          |
    | Beauceron             |
    | Chodský pes           |
    | Australian Shepherd   |
    | Labrador Retriever    |
    | American Bully        |
    | Staffordshire Terrier |
    | Fransk Bulldog        |