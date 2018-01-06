# Checkout.com Technical Test

## Notes

Methods can use request body, route parameters and query string for parameters, in that priority (i.e. if you have request body and URL, request body will be used.

Considered using an IBasketItem interface, but BasketItem contains the bare minimum data and functionality, and can be easily subclassed for more detailed basket items.

EditItemQuantity in Basket relies on an exception for some non-error logic - not thrilled about this, but it was the cleanest way I could think of to enforce the > 1 constraint everywhere.

## Assumptions

Strings for Item IDs.

All Item IDs are valid - not performing lookup on a store.

By "client", I'm assuming a helper class to call the methods in the API - given that there are a small amount, any more than 1 class seems like over-engineering.

## Decisions

While Add, Edit and Remove could all theoretically be handled by one endpoint, having one for each seemed the most user-friendly option.

Not throwing Exception for delete if Item does not already exist in the basket.

Endpoints will only send around string ID and int quantity, as for this small amount of information I feel it makes more sense than passing a serialised DTO around.

Split the models into a separate library to allow internal methods and properties to be unit tested without exposing to the API.

Internalised the collection itself in IBasket as though exposing it would keep the class simple, would also expose undesirable operations to the client.

Took a simplistic route to thread-safety, locking on list of items for each Basket operation.

Added baskets for multiple users - each user creates a basket, is returned a token, and uses that token for each subsequent call.

## TODO

Make basket endpoints (strings) configurable for client.