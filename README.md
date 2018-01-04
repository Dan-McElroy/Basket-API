# Checkout.com Technical Test

## Assumptions

Strings for Item IDs.

All Item IDs are valid - not performing lookup on a store.

## Decisions

Parameters can be added in request body or query string - but eschews the use of a more specific DTO.

While Add, Edit and Remove could all theoretically be handled by one endpoint, having one for each seemed the most user-friendly option.

Not throwing Exception for delete if Item does not already exist in the basket.