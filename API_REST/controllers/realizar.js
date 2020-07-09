// Módulo que gestiona la tabla realizar
// Módulos invocados
var conexion = require("./conexion");

// Función para listar todas las rutinas de un alumno en un rango de fechas
exports.listaRutinasAlumnoRango = function (req, res) {
    var dni_usuario = req.params.dni_usuario;
    var fecha1 = req.params.fecha1;
    var fecha2 = req.params.fecha2;
    conexion.query("SELECT * FROM realizar WHERE dni_usuario = ? AND fecha BETWEEN ? AND ? ORDER BY fecha ", [dni_usuario, fecha1, fecha2], function (error, rows, fields) {
        if (error) {
            console.log(error);
        }
        else {
            return res.json(rows);
        }
    });
}

// Función para obtener las rutinas de un usuario en una fecha concreta
exports.get = function (req, res) {
    var dni_usuario = req.params.dni_usuario;
    var fecha = req.params.fecha;
    conexion.query("SELECT * FROM realizar WHERE dni_usuario = ? AND fecha = ? ", [dni_usuario,fecha], function (error, rows) {
        if (error) {
            console.log(error);
        }
        else {
            return res.json(rows)
        }
    });
}

// Función para obtener una rutina de un usuario en una fecha concreta
exports.rutinaFecha = function (req, res) {
    var dni_usuario = req.params.dni_usuario;
    var cod_rutina = req.params.codigo_rutina
    var fecha = req.params.fecha;
    conexion.query("SELECT * FROM realizar WHERE dni_usuario = ? AND codigo_rutina = ? AND fecha = ? ", [dni_usuario, cod_rutina, fecha], function (error, rows) {
        if (error) {
            console.log(error);
        }
        else {
            return res.json(rows)
        }
    });
}

// Función para crear una rutina para un usuario en una fecha concreta en la base de datos
exports.add = function (req, res) {
    var json = JSON.parse(JSON.stringify(req.body));
    conexion.query("INSERT INTO realizar set ?", json, function (error, rows) {
        if (error) {
            console.log(error)
        }
        else {
            console.log("Actividad insertada en la base de datos")
        }
    });
    res.end();
}

// Función para eliminar una rutina de un usuario en una fecha concreta
exports.delete = function (req, res) {
    var dni_usuario = req.params.dni_usuario;
    var codigo_rutina = req.params.codigo_rutina;
    var fecha = req.params.fecha;
    conexion.query("DELETE FROM realizar WHERE dni_usuario = ? AND codigo_rutina = ? AND fecha = ? AND ejecutada = 0 ", [dni_usuario, codigo_rutina, fecha], function (error, rows) {
        if (error) {
            console.log(error);
        }
        else {
            console.log("rutina eliminada")
        }
    });
    res.end();
}

// Función para actualizar el estado de una rutina de un usuario en una fecha concreta con valor true 
exports.rutinaEjecutada = function (req, res) {
    var dni_usuario = req.params.dni_usuario;
    var codigo_rutina = req.params.codigo_rutina;
    var fecha = req.params.fecha;
    conexion.query("UPDATE realizar SET ejecutada = 1 WHERE dni_usuario = ? AND codigo_rutina = ? AND fecha = ? ", [dni_usuario, codigo_rutina, fecha], function (error, rows) {
        if (error) {
            console.log(error);
        }
        else {
            console.log("estado de la rutina actualizado")
        }
    });
    res.end();
}

// Función para actualizar el estado de una rutina de un usuario en una fecha concreta con valor false
exports.rutinaNoEjecutada = function (req, res) {
    var dni_usuario = req.params.dni_usuario;
    var codigo_rutina = req.params.codigo_rutina;
    var fecha = req.params.fecha;
    conexion.query("UPDATE realizar SET ejecutada = 0 WHERE dni_usuario = ? AND codigo_rutina = ? AND fecha = ? ", [dni_usuario, codigo_rutina, fecha], function (error, rows) {
        if (error) {
            console.log(error);
        }
        else {
            console.log("estado de la rutina actualizado")
        }
    });
    res.end();
}

// Función para saber si una rutina ha sido ejecutada
exports.estadoRutina = function (req, res) {
    var cod_rutina = req.params.codigo_rutina
    conexion.query("SELECT * FROM realizar WHERE codigo_rutina = ? ORDER BY (ejecutada) DESC LIMIT 1 ", [cod_rutina], function (error, rows) {
        if (error) {
            console.log(error);
        }
        else {
            return res.json(rows)
        }
    });
}

