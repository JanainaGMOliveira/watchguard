# Watchguard
## To build the project
### Running in Docker

```bash
make build 
make run-sqlserver
make run-warehouse
```
### Urls
The API listens on http://localhost:5010

These are the endpoints:

#### Brand
1. GetById: GET http://localhost:5010/api/brand/{id} 

The endpoint returns the brand object with the id, name and description.


2. Create: POST http://localhost:5010/api/brand/

#### body: 
{
  "name": "Brand Name",
  "description": "Brand Descrition"
}

The endpoint inserts the new brand in database and returns the status.


3. Update: PUT http://localhost:5010/api/brand/

#### body: 
{
  "id": 1,
  "name": "Brand Name",
  "description": "Brand Descrition"
}

The endpoint updates a brand and returns the status.


4. Delete: DELETE http://localhost:5010/api/brand/{id}

The endpoint deletes the brand with the specified id and returns the status.


5. ListAll: GET http://localhost:5010/api/brand/

The endpoint returns all the brands in the database with the id, name and description.


#### Product
1. GetById: GET http://localhost:5010/api/product/{id} 

The endpoint returns the brand object with the id, name, unit, quantity, price, active status and brand name.


2. Create: POST http://localhost:5010/api/product/

#### body: 
{
  "name": "Product Name",
  "unit": "Product Unity",
  "quantity": 1,
  "price": 1.0,
  "active": true,
  "brandid": 1
}

The endpoint inserts a new brand in database and returns the status. To insert a product, there must exits a brand.

3. Update: PUT http://localhost:5010/api/product/

#### body: 
{
  "id": 1,
  "name": "Product Name",
  "unit": "Product Unity",
  "quantity": 1,
  "price": 1.0,
  "active": true,
  "brandid": 1
}

The endpoint updates the product and returns the status.


4. Delete: DELETE http://localhost:5010/api/product/{id}

The endpoint deletes the product with the specified id and returns the status.


5. ListAll: GET http://localhost:5010/api/product/

The endpoint returns all the products in the database with the id, name, unit, quantity, price, active status and brand name.


6. FileWithActiveProducts: GET http://localhost:5010/api/product/QuantityActiveProducts

The endpoint returns the sum of active products in the database in a txt file.


7. FileWithProductsByBrand: GET http://localhost:5010/api/product/ProductsByBrand

The endpoint returns the count of products by brand in the database in a xml file.
