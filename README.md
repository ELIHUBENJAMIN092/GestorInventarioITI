Gestor de Inventario ITI.

Este proyecto es una API construida con ASP.NET Core para gestionar un inventario. Está conectada a una base de datos SQL Server y expone endpoints para realizar operaciones CRUD sobre las entidades User, Role, Product, y Category.

Requisitos

Visual Studio 2022 o superior
.NET SDK 8.0 o superior
SQL Server (puede ser local o en la nube)
Conocimiento básico de C# y Entity Framework Core
Organización del Proyecto

1. Creación del Proyecto La API se construyó utilizando el tipo de proyecto ASP.NET Core Web API en Visual Studio 2022, con el marco de trabajo .NET 8.0 o superior. El objetivo es ofrecer una plataforma para gestionar un inventario de productos mediante la interacción con las entidades de usuario, roles, productos y categorías.

2. Configuración de la Conexión a la Base de Datos Entity Framework Core se utiliza para interactuar con la base de datos. Se configura mediante el paquete Microsoft.EntityFrameworkCore.SqlServer para facilitar la comunicación con SQL Server. La cadena de conexión se almacena en el archivo appsettings.json, donde se especifica el servidor y la base de datos a utilizar. Un archivo DbContext gestiona la interacción con la base de datos, exponiendo las entidades como conjuntos de datos (DbSet), tales como Users, Roles, Products y Categories.

3. Controladores de la API Los controladores son responsables de manejar las solicitudes HTTP y realizar las operaciones CRUD sobre las entidades:

UsersController: Permite gestionar los usuarios del sistema. Implementa los métodos GET para obtener una lista de usuarios y POST para crear nuevos usuarios.
RolesController, ProductsController, CategoriesController: Similar a UsersController, estos controladores permiten gestionar roles, productos y categorías, respectivamente, proporcionando endpoints para realizar operaciones CRUD.
4. CORS (Cross-Origin Resource Sharing) Para consumir la API desde un navegador en un dominio diferente, es necesario habilitar CORS (Cross-Origin Resource Sharing). Esto se realiza configurando una política de CORS en Program.cs, permitiendo que cualquier origen acceda a los recursos de la API.

5. Ejecución de la API Al ejecutar el proyecto en Visual Studio, la API estará disponible en una URL local, como http://localhost:5000 (esta URL puede variar dependiendo de la configuración del proyecto). Desde allí, los clientes pueden interactuar con la API para realizar operaciones sobre el inventario.

Consumo de la API La API puede ser consumida mediante un cliente web. Para facilitar el acceso, se puede crear una página HTML con JavaScript que utilice el método fetch para realizar solicitudes HTTP a la API y mostrar los resultados en el navegador.

El cliente web puede hacer solicitudes GET a los endpoints de la API para obtener datos sobre usuarios, productos, roles, etc. Los datos obtenidos desde la API pueden ser presentados dinámicamente en la página web, facilitando la interacción con el inventario.

Recursos:

Documentación oficial de ASP.NET Core
Entity Framework Core
CORS en ASP.NET Core
