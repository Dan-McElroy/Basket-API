# Checkout.com Technical Test

## Notes

Methods can use request body, route parameters and query string for parameters, in that priority (i.e. if you have request body and URL, request body will be used.

Considered using an IBasketItem interface, but BasketItem contains the bare minimum data and functionality, and can be easily subclassed for more detailed basket items.

## Assumptions

Strings for Item IDs.

All Item IDs are valid - not performing lookup on a store.

## Decisions

While Add, Edit and Remove could all theoretically be handled by one endpoint, having one for each seemed the most user-friendly option.

Not throwing Exception for delete if Item does not already exist in the basket.

Split the models into a separate library to allow internal methods and properties to be unit tested without exposing to the API.

Internalised the collection itself in IBasket as though exposing it would keep the class simple, would also expose undesirable operations to the client.