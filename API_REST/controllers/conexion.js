//Módulo que permite la conexión con la base de datos

//Imports
var mysql = require("mysql");

//Variable donde guardamos la conexión
var conexion = mysql.createConnection({
    user: "root",
    password: "ad1920",
    host: "localhost",
    database: "gimnasio",
    dateStrings: true
})

module.exports = conexion;