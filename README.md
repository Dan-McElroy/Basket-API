# Basket API

This solution contains both an API which allows users to create and modify a basket of shopping items, and a class library which allows for easy access to this API in client applications, as well as a supporting model class library and unit test suites.

## BasketSystem.API

This project contains the API itself, which works by having the client create a basket, and then add, edit, or remove items from that basket.

### User Guide

The Basket API has the following endpoints:

`POST: /api/basket/new`

_Parameters_: **None**

This endpoint is used to create a new basket in the repository, and returns a unique user token to access that basket.

`POST: /api/basket` _or_

`POST: /api/basket/user-token/$TOKEN/item-id/$ID/quantity/$QUANTITY`

_Parameters_ (in request body, URL or query string): **Guid** token, **string** id, _(Optional)_ **int** quantity

Adds a new item to a basket specified by the token. Providing a quantity is optional - the default value is 1.

`PUT: /api/basket` _or_

`PUT: /api/basket/user-token/$TOKEN/item-id/$ID/quantity/$QUANTITY`

_Parameters_ (request body, URL or query string): **Guid** token, **string** id, **int** quantity

Edits the quantity of an item in the basket specified by the token. Note: if quantity given is below 1, the item will be removed from the basket.

`DELETE: /api/basket/user-token/$TOKEN/item-id/$ID`

_Parameters_ (URL): **Guid** token, **string** id

Removes an item from the basket specified by the token.

`DELETE: /api/basket/user-token/$TOKEN/all-items`

_Parameters_ (URL): **Guid** token

Clears the basket specified by the token of all items.

### Implementation Notes

Versioning is implemented via an `api-version` header. Currently, only version `1` of the API is implemented, but this allows for modifications and extensions of the API in the future without forcing clients to update.

Dependency Injection is used in the `BasketController` to create a singleton `IBasketRepository` which keeps track of all active baskets. This interface is simple, exposing three methods to create a basket, find a basket and remove a basket. In lieu of a database, this seemed like the easiest, cleanest way to store basket data in memory.

Regarding `IBasketRepository`'s `RemoveBasket` method - this is currently unused, but would be necessary for use by the supporting payment API.

I have assumed from the specification that items can be specified via a unique product ID, in the form of a `string`. I have also assumed that all such strings are valid - I have not mocked performing a lookup on a store.

Parameters can be supplied to endpoints via the request body (where permitted by the HTTP method), the URL route specified above and the query string, in that order of recognition; i.e. if you specify a `quantity` of `3` in the request body and `2` in the query string, `3` is the value that will be read.

---

## BasketSystem.Client

This project contains a library which can be used by client applications to easily access the Basket API.

It consists of one main class, `BasketClient`, as well as a `Settings` object which describes its related settings, `JsonObject` which is a helper class for creating JSON-related HTTP requests easily, and a `BasketRequest` DTO for sending requests to the Basket API.

### User Guide

Before the Basket API Client can be used, a section needs to be added to your application's `appsettings.json`. See `appsettings.example.json` within the `BasketSystem.Client` project for the layout of this section. Before use of the client, it should then be deserialized into a `BasketSystem.Client.Settings` object.

The Basket API can then be accessed by instantiating a `BasketClient` object within a `using` block, so it can be disposed:

```cs
using BasketSystem.Client;
....
using (var client = new BasketClient(clientSettings))
{
    ...
}
```
By default, creating a client will create a new basket for you via the Basket API. However, if you wish to access an existing basket and have its token, this can be provided as the second parameter of the `BasketClient` constructor.

`BasketClient` exposes four operations on the API, via the methods `AddItemAsync`, `EditItemQuantityAsync`, `RemoveItemAsync` and `ClearBasketAsync`. Each of these follows the parameters and behaviour of the corresponding API endpoints as described above, and run asynchronously.

### Implementation Notes

Where possible, the client sends request parameters via the request body, in order to be as secure as possible.

The client implements `IDisposable`, and should be disposed in order to close connections made in the underlying `HttpClient`.

---

## BasketSystem.Models

This project contains the `IBasket` interface, and the `Basket` & `BasketItem` classes which hold the data and much of the logic associated with the API. These models were split from the API itself to hide implementation details from the API while exposing them to unit testing frameworks, and it gives the client library access to these models as well.

`IBasket` is the interface exposed to the `BasketController` in `BasketSystem.API`, and holds the add, edit, remove and clear methods made clear in the project specification. It does not expose a collection of BasketItems - this is to allow flexibility in implementation, and prevent client code from being able to manipulate the basket outside of the four operations outlined above.

`Basket` is the concrete implementation of `IBasket`, and uses an `ICollection<BasketItem>` to store items, which is instantiated as a `List` in the default constructor.

`BasketItem` is a simple class which models an entry in the basket, and contains a `string Id` and an `int Quantity`.

### Implementation Notes

I considered creating an `IBasketItem` interface, but `BasketItem` contains the minimum amount of properties that an `IBasketItem` would need to expose, and the setters in `BasketItem` are implemented to perform necessary checks on any basket item (a non-null ID and above-zero quantity).

With respect to thread safety, I took the simplistic approach of locking on the collection for each basket operation. This should suffice as while it locks other threads out of concurrent execution, the amount of work within each operation is small enough that this delay should be minimal, and rules out any chance of side-effects.

The `EditItemQuantity` method in `Basket` relies on an exception to handle some cases which may not be an error - if the quantity is set below zero, `BasketItem` throws an exception which is caught by `Basket` to remove the item from the list. I'm slightly unhappy with using exceptions in this way, but it provided the most easy, logically-understandable way to handle this case.

---
