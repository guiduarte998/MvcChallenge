# MvcChallenge

A few things to mention.

I created two ways to do this challenge, one of them was using the API provided by the company that provided me with the challenge. And the other was through Entity Framework Core, using migrations.

I tried and implemented Delete and Details methods, to make a complete CRUD. Delete does not work on API Controllers because the Endpoint does not exist.

In the Create method, in the Authors, there is an error in the JSON, the Email field is as Eamil.

{
     "id": "65aed5589914ede866ccf89c",
     "firstName": "Barack",
     "lastName": "Obama",
     "eamil": "barack@obama.com",
     "phone": "555-123456",
     "address1": "123 Main Avenue",
     "address2": "",
     "city": "New York",
     "state": "NY",
     "zip": "01234",
     "country": "USA"
}

This has virtually no impact, but it can cause an error when trying to convert the JSON object to Authors and vice versa.

And lastly, in the API I put the URL in all the methods, this is not the best or most practical way to do it, but I was running out of time to deliver the challenge. (If I have already corrected the URLs, skip this part).

I thank the company for the challenge, it was fun.
