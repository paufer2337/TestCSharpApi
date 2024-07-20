Feature: As a user I want to be able to see the correct price for all the products when I have chosen a category for all catagories or a specific category. 

Scenario Outline: When I choose the category "<category>" the product "<productName>" should be shown with the price "<price>".
Given I click on the product page
When I click on category "<category>"
Then the product "<productName>" should be shown with the price "<price>"

Examples:
    | category  | productName           | price  |
    | Brukshund | Belgisk Malinois      | 25000  |
    | Brukshund | Tysk Schäfer          | 27000  |
    | Brukshund | Beauceron             | 21000  |
    | Brukshund | Chodský pes           | 18000  |
    | Brukshund | Australian Kelpie     | 22000  |
    | Brukshund | Labrador Retriever    | 19000  |
    | Kamphund  | American Bully        | 20000  |
    | Kamphund  | Staffordshire Terrier | 15000  |
    | Sällskap  | Fransk Bulldog        | 17000  |