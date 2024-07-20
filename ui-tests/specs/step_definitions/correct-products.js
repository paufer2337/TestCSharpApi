import { Given, When, Then } from "@badeball/cypress-cucumber-preprocessor";

Given('that I am currently on the homepage', () => {
  cy.visit('/products');
});

When('I select {string} from the category list', (category) => {
  cy.get('#categories').select(category);
});

Then('I should only see {string} of each category', (productName) => {
  cy.get('.product .name').contains(productName);
});
