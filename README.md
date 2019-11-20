# Pizza API

Aplicación CRUD para un restaurante de pizzas con conexión a MongoDB. La API posee puntos finales para crear, obtener, remplazar y eliminar elementos.

## Crear pizza

El punto final /api/mongo con un verbo `POST` espera un JSON con la siguiente estructura:

```javascript
{
    "Name": "Pizza name",
    "Description": "Pizza description",
    "Ingredients": [
        "ingredient 1",
        "ingredient 2"
    ],
    "Size": "pizza size (small, medium, large)",
    "Slices": 4,
    "HasExtraCheese": false
}
```

## Obtener pizza

Existen dos formas de obtener una pizza, las dos utilizan verbo `GET`:

* /api/mongo, retorna todas las pizzas disponibles.
* /api/mongo/{name}, retorna una pizza especifica para ello se necesita cambiar `{name}` por el nombre de la pizza deseada.

## Actualizar pizza

El punto final /api/mongo/{name} con un verbo `PUT` espera un JSON con la siguiente estructura:

```javascript
{
    "Name": "Pizza name",
    "Description": "Pizza description",
    "Ingredients": [
        "ingredient 1",
        "ingredient 2"
    ],
    "Size": "pizza size (small, medium, large)",
    "Slices": 4,
    "HasExtraCheese": false
}
```

Al momento de realizar la petición se debe de cambiar `{name}` por el nombre de la pizza la cual se desea actualizar.

## Eliminar pizza

Para eliminar una pizza solo se necesita realizar una petición de tipo `DELETE` hacia el punto final /api/mongo/{name} y cambiar la `{name}` por el nombre de la pizza que se desea eliminar.
