# Checkout.com Technical Test

## Notes

Methods can use request body, route parameters and query string for parameters, in that priority (i.e. if you have request body and URL, request body will be used.

## Assumptions

Strings for Item IDs.

All Item IDs are valid - not performing lookup on a store.

## Decisions

While Add, Edit and Remove could all theoretically be handled by one endpoint, having one for each seemed the most user-friendly option.

Not throwing Exception for delete if Item does not already exist in the basket.