**Catalog**

 - Try to add a new category table and populate it with some data
 - Get products by categories (Send a list of categories instead onf one)
    - ../products/categories"
 - Add attributes property to product table => **Attributes = new Dictionary<string, object>**

 - Mimoun: Separate read and write database => Steps descriptions
 - Validations
    - Add some custom validations: 
       - Should Have Description If Price IsGreater Than100
       - Be a valid image format
    - Fix exceptions Hanlder


**Basket**
 - Add decorator strategy : InMemory, Redis or Test

**Discount**
 - Resolve the N + 1 problem in Discount and Basket  

**Ordering**
 - Using repositories inside the ordering module .

- Implement : Event sourcing

- Create Sql server container with a default user