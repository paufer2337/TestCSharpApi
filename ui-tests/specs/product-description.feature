Feature: As a user I want to be able to see the correct description for all the products when I have chosen a category for all catagories or a specific category. 

Scenario Outline: When I choose the category "<category>" the product "<productName>" should be shown with the description "<description>".
Given that I want to visit the product page
When I choose a specific category "<category>"
Then the product "<productName>" should be displayed with the description "<description>"

Examples:
    | category  | productName           | description |
    | Alla      | Belgisk Malinois      | Passar bra till brukssportens alla grenar. Det är en mångsidig tävlings- och tjänstehund. |
    | Alla      | Tysk Schäfer          | Skarp och förarvek brukshund för tävling. Används även som tjänstehund av bland annat polisen. Rakryggade linjer. |
    | Alla      | Beauceron             | Krävande mångsysslare. Lämpar sig väl för bruksgrenar och vallning men också för hundsporter som agility och lydnad. |
    | Alla      | Chodský pes           | Alert, lättlärd och mångsidig träningskompis. Duktiga tjänstehundar i mindre format. |
    | Alla      | Australian Kelpie     | Lyhörd, mångsidig och passar utmärkt för vallning, agility och i tjänst. |
    | Alla      | Labrador Retriever    | Social, lättsam och stark apportör med jakt-linjer i blodet. |
    | Alla      | American Bully        | Stor XL Bully, trevlig barn-nanny och bra hund för drag-sporter. |
    | Alla      | Staffordshire Terrier | Energisk, följsam och sprallig glädjespridare! Passar Allround. |
    | Alla      | Fransk Bulldog        | Trevlig sällskapshund som även är arbetsvillig. |
    | Brukshund | Belgisk Malinois      | Passar bra till brukssportens alla grenar. Det är en mångsidig tävlings- och tjänstehund. |
    | Brukshund | Tysk Schäfer          | Skarp och förarvek brukshund för tävling. Används även som tjänstehund av bland annat polisen. Rakryggade linjer. |
    | Brukshund | Beauceron             | Krävande mångsysslare. Lämpar sig väl för bruksgrenar och vallning men också för hundsporter som agility och lydnad. |
    | Brukshund | Chodský pes           | Alert, lättlärd och mångsidig träningskompis. Duktiga tjänstehundar i mindre format. |
    | Brukshund | Australian Kelpie     | Lyhörd, mångsidig och passar utmärkt för vallning, agility och i tjänst. |
    | Brukshund | Labrador Retriever    | Social, lättsam och stark apportör med jakt-linjer i blodet. |
    | Kamphund  | American Bully        | Stor XL Bully, trevlig barn-nanny och bra hund för drag-sporter. |
    | Kamphund  | Staffordshire Terrier | Energisk, följsam och sprallig glädjespridare! Passar Allround. |
    | Sällskap  | Fransk Bulldog        | Trevlig sällskapshund som även är arbetsvillig. |