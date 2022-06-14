# Cocktail Maker (backend)

Backend for service that provides cocktail recipes (based on [The Cocktail DB API](https://www.thecocktaildb.com/api.php))

## Overview

Service has two main components:

* Grabber - migrates data from Cocktail DB API to a more user-friendly model
* API - basically an HTTP API service to use for frontend (coming soon)

Both services are built with .NET 6 and PostgreSQL as a database

## Local development

To set up the environment you need to execute from root folder:

```
docker compose -f ./scripts/docker-compose.local.yml up -d
```

This will run database container. After that you are free to use anything you like to develop .NET 6 apps
