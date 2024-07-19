import { Given, When, Then } from "@badeball/cypress-cucumber-preprocessor";

Given('I am on the homepage', () => {
  cy.visit('/products');
});

When('I select {string} from the category selector', (category) => {
  cy.get('#categories').select(category);
});

Then('I should see the product {string} listed', (productName) => {
  cy.get('.product .name').contains(productName);
});