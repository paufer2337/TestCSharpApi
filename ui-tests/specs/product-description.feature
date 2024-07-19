Feature: As a user I want to be able to see the correct description for all the products when I have chosen a category for all catagories or a specific category. 

Scenario Outline: When I choose the category "<category>" the product "<productName>" should be shown with the description "<description>".
Given that I am on the product page
When I get the category "<category>"
Then the product "<productName>" should be shown with the description "<description>"

Examples:
    | category  | productName           | description |
    | Brukshund | Belgisk Malinois      | Passar bra till brukssportens alla grenar. Det är en mångsidig  tävlings- och tjänstehund. |
    | Brukshund | Dobermann             | Lekfull, arbetsglad och livlig brukshund. Den används även som tjänstehund av bland annat polis och räddningstjänst.|
    | Brukshund | Beauceron             | Krävande mångsysslare. Lämpar sig väl för bruksgrenar och vallning men också för hundsporter som agility och lydnad. |
    | Brukshund | Chodský pes           | Alert, lättlärd och mångsidig träningskompis  |
    | Brukshund | Australian Shepherd   | Lyhörd, mångsidig och förekommer med stubbsvans!  |
    | Brukshund | Labrador Retriever    | Social och stark apportör som är duktig på det mesta.  |
    | Kamphund  | American Bully        | Stor XL Bully, trevlig barn-nanny och bra hund för drag-sporter.  |
    | Kamphund  | Staffordshire Terrier | Energisk, följsam och sprallig glädjespridare! Passar Allround.  |
    | Sällskap  | Fransk Bulldog        | Trevlig sällskapshund som även är arbetsvillig.  |