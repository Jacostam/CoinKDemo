# COINK Demo

- Se realizo el projecto para manejo de base de datos MYSQL.

- Para la conexion a la base de datos se debe agregar en el archivo appsettings.json
```
    "MySqlConnection": "Server={server};Database={Database};User={useer};Password={Password};"
```
# SCRIP DE BASE DE DATOS
## CREACIONDE TABLAS 

###  Creación de la tabla de países

```
CREATE TABLE countries (
    id INT AUTO_INCREMENT PRIMARY KEY,
    name VARCHAR(100) NOT NULL
);
```
**Insertar registro** 
```
    INSERT INTO `countries` (`name`) VALUES ('Colombia'); 
```

###  Creación de la tabla de departamentos
```
CREATE TABLE departments (
    id INT AUTO_INCREMENT PRIMARY KEY,
    name VARCHAR(100) NOT NULL,
    country_id INT NOT NULL,
    FOREIGN KEY (country_id) REFERENCES countries(id)
);
```
**Insertar registro** 
```
    INSERT INTO `departments` (`name`, `country_id`) VALUES ('Bogota', '1'); 
```

###  Creación de la tabla de ciudades
```
CREATE TABLE cities (
    id INT AUTO_INCREMENT PRIMARY KEY,
    name VARCHAR(100) NOT NULL,
    departments_id INT NOT NULL,,
    FOREIGN KEY (departments_id) REFERENCES departments(id)
);
```
**Insertar registro** 
```
    INSERT INTO `cities` (`name`, `departments_id`) VALUES ('Bogota D.C', '1');
```

###  Creación de la tabla de Usuarios
```
CREATE TABLE users (
    id INT AUTO_INCREMENT PRIMARY KEY,
    name VARCHAR(100) NOT NULL,
    phone VARCHAR(100) NOT NULL,
    address VARCHAR(100) NOT NULL,
    status INT NOT NULL  DEFAULT 1 ,
    city_id INT,
    FOREIGN KEY (city_id) REFERENCES cities(id)        
);
```
**En la tabla users, solo se dejó la relación con cities, ya que con el ID de la ciudad podemos conocer el departamento y el país.**

## CREACIONDE VISTAS 
###  Creación Vista de Usuarios
```
CREATE VIEW `data_user` AS
    SELECT 
        `U`.`id` AS `id`,
        `U`.`name` AS `name`,
        `U`.`phone` AS `phone`,
        `U`.`address` AS `address`,
        `U`.`city_id` AS `city_id`,
        `C`.`name` AS `city`,
        `D`.`id` AS `department_id`,
        `D`.`name` AS `department`,
        `P`.`id` AS `country_id`,
        `P`.`name` AS `country`
    FROM
        (((`users` `U`
        JOIN `cities` `C` ON ((`C`.`id` = `U`.`city_id`)))
        JOIN `departments` `D` ON ((`D`.`id` = `C`.`departments_id`)))
        JOIN `countries` `P` ON ((`P`.`id` = `D`.`country_id`)))
    WHERE
        (`U`.`status` = 1)
```
## PROCEDIMIENTOS ALMACENADOS 

### Listar usuarios activos
```
CREATE PROCEDURE list_users()
BEGIN
    SELECT * FROM data_user ;
END 
```
### Crear Usuarios
```
CREATE PROCEDURE `create_user`(IN name VARCHAR(100), IN phone VARCHAR(100), IN city_id INT, IN address VARCHAR(100))
BEGIN
    IF NOT EXISTS (SELECT 1 FROM cities WHERE id = city_id) THEN
        SIGNAL SQLSTATE '45000' 
            SET MESSAGE_TEXT = 'El CityId no existe en la tabla Cities.';
    ELSE
        -- Insertar el pedido si CustomerID es válido
         INSERT INTO `users` (`name`, `phone`, `city_id`, `address`) VALUES (name, phone,city_id, address);
    END IF;           
END
```
### Eliminar Usuarios
```
CREATE PROCEDURE `delete_user`(IN user_id INT)
BEGIN
    UPDATE `users` SET `status` = '0' WHERE `id` = user_id;
END 
```

### Listar Paises
```
CREATE PROCEDURE list_country()
BEGIN
    SELECT * FROM countries;
END
```
### Listar Departamentos
```
CREATE PROCEDURE list_departments()
BEGIN
    SELECT * FROM departments;
END 
```
### Listar Departamentos por pais
```
CREATE PROCEDURE `list_departments_country` (IN id_country INT)
BEGIN
	  SELECT * FROM departments where country_id=id_country;
END 
```

### Listar Ciudades
```
CREATE PROCEDURE list_cities()
BEGIN
    SELECT * FROM cities;
END 
```
### Listar Ciudades por departamento
```
CREATE PROCEDURE `list_cities_department` (in department_id INT)
BEGIN
    SELECT * FROM cities where departments_id=department_id;
END 
```

# Api

### Listar Paises
```
[GET] http://localhost:5191/countries

```
### Listar Departamentos
```
[GET] http://localhost:5191/departments

```
### Listar Departamentos por pais 
```
[GET] http://localhost:5191/departments/id_country

```

### Listar Ciudades
```
[GET] http://localhost:5191/cities
```
### Listar Ciudades por departamentos
```
[GET] http://localhost:5191/cities/id_department
```

### Listar Usuarios
```
[GET] http://localhost:5191/users
```
### Crear Usuarios
```
[POST] http://localhost:5191/users
    - Crea un nuevo usuario en la plataforma.Se envia el siguiente JSon por bodyram.
        {
            "Name": "Juan pruebas",
            "Phone": "3000",
            "CityId": 1,
            "Address": "cra 40 390 30"
        }
```

### Eliminar Usuario
```
[DELETE] http://localhost:5191/users/{{id}}
```