# SocialBrothers-Assessment

## Features
- Basic CRUD operations
- OData query functionality
- Third-party Geolocation API connection

## Getting Started 🚀
1. Clone this repo
2. Get a API key from [mapquest](https://developer.mapquest.com/)
3. Place the API key in both `appsettings.json` and `appsettings.Development.json` under 'Key' 
4. Run the project
5. Navigate to [`localhost:44396/swagger/index.html`](https://localhost:44396/swagger/index.html) for API documentation
6. Enjoy 🎉

7. (optional) Check out [OData](https://www.odata.org/getting-started/basic-tutorial/) for information on OData query language

## The Good
1. Seperate geolocation API project, with self-contained entities making it easily reusable for other projects.
2. Generic repository class with default implementation for crud operations, which can easily be used for other entities as well if needed.
3. OData search implementation, allowing for extremely flexible queries. This includes lazy loading through the use of IQueryable, which also ensures that only the requested data is fetched from the database.

## The bad
1. API credentials in appsettings - It would be better to include them in a web.config or some other non-git file.
2. Logic inside controller classes - It would be better to place this logic inside the controller classes in a separate project, but as the API is very small I didn't find this useful enough here.
3. Entity/DTO seperation - Instead of returning the entity directly, having separate DTO's would give better control over what is returned when, but because of the size of the project I decided not to add it here.