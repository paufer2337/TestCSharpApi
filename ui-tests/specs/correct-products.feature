Feature: As user I want to be able to see the correct products listed when I have chosen a category so that I can easily filter the product list by category.

  Scenario: Check that each category shows the right products.
    Given I am on the product page
    When I choose a <category> from the selection
    Then I should see the <product>

    Examples:
    | category  | product               | price  |
    | Brukshund | Belgisk Malinois      | 25000  |
    | Brukshund | Dobermann             | 27000  |
    | Brukshund | Beauceron             | 21000  |
    | Brukshund | Chodský pes           | 18000  |
    | Brukshund | Australian Shepherd   | 22000  |
    | Brukshund | Labrador Retriever    | 19000  |
    | Kamphund  | American Bully        | 20000  |
    | Kamphund  | Staffordshire Terrier | 15000  |
    | Sällskap  | Fransk Bulldog        | 17000  |