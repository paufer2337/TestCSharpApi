import { Given, When, Then } from "@badeball/cypress-cucumber-preprocessor";

Given('that I want to visit the product page', () => {
    cy.visit("/products");
});

When('I choose a specific category {string}', (category) => {
    cy.get('#categories').select(category);
});

Then('the product {string} should be displayed with the description {string}', (productName) => {
    cy.get('.product .name').contains(productName);

});