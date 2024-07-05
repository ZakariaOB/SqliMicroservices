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
 - TODO Justify the choice of the decorator pattern over inheritance 
 - Add decorator strategy : InMemory, Redis or Test
 - Siham : Add more cases to showcase some addtional functionnalities at Basket service

**Discount**
 - Resolve the N + 1 problem in Discount and Basket 
 - Create a discount table in Basket API 
 - Try to refresh this table usng async communication from Basket service 
   using rabbitMQ .

**Ordering**
 - Using repositories inside the ordering module .

- Implement : Event sourcing

- Create Sql server container with a default user