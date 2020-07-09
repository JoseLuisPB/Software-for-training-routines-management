// Módulo que gestiona la tabla alumno
// Módulos invocados
var conexion = require("./conexion");


// Función para obtener los datos de un alumno utilizando su DNI
exports.get = function (req, res) {
    var dni = req.params.dni;
    conexion.query("SELECT * FROM alumno WHERE dni_usuario=?", [dni], function (error, rows) {
        if (error) {
            console.log(error);
        }
        else {
            return res.json(rows)
        }
    });
}

// Función para crear los datos de un alumno
exports.add = function (req, res) {
    var json = JSON.parse(JSON.stringify(req.body));
    conexion.query("INSERT INTO alumno set ?", json, function (error, rows) {
        if (error) {
            console.log(error)
        }
        else {
            console.log("Datos del alumno insertados en la base de datos")
        }
    });
    res.end();
}

// Función para actualizar los datos de un alumno en la base de datos
exports.update = function (req, res) {
    var json = JSON.parse(JSON.stringify(req.body));
    var dni = json.Dni_usuario;
    conexion.query("UPDATE alumno set ? where dni_usuario= ?", [json, dni], function (error, rows) {
        if (error) {
            console.log(error);
        }
        else {
            console.log("Datos del alumno actualizados en la base de datos");
        }
    });
    res.end();
}


