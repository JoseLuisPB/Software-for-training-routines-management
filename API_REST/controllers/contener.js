// Módulo que gestiona la tabla contener
// Módulos invocados
var conexion = require("./conexion");


// Función para listar todas las actividades contenidas en una rutina
exports.listaActividadesRutina = function (req, res) {
    var codigo_rutina = req.params.codigo_rutina;
    conexion.query("SELECT * FROM contener where codigo_rutina = ?", [codigo_rutina], function (error, rows, fields) {
        if (error) {
            console.log(error);
        }
        else {
            return res.json(rows);
        }
    });
}


// Función para insertar una actividad en una rutina
exports.add = function (req, res) {
    var json = JSON.parse(JSON.stringify(req.body));
    conexion.query("INSERT INTO contener set ? ", json, function (error, rows) {
        if (error) {
            console.log(error)
        }
        else {
            console.log("Actividad añadida a la rutina")
        }
    });
    res.end();
}

// Función para actualizar una rutina en la base de datos
exports.delete = function (req, res) {
    var codigo_rutina = req.params.codigo_rutina;
    conexion.query("DELETE FROM contener WHERE codigo_rutina = ?", [codigo_rutina], function (error, rows) {
        if (error) {
            console.log(error);
        }
        else {
            console.log("Actividades eliminadas de la rutina");
        }
    });
    res.end();
}

// ESTADÍSTICAS
// Función para obtener la lista de actividades realizadas en un rango de fechas determinado
exports.actividadesEjecutadasRango = function (req, res) {
    var dni = req.params.dni;
    var fechaInicio = req.params.fechaInicio;
    var fechaFin = req.params.fechaFin;
    conexion.query("SELECT DISTINCT contener.codigo_actividad  FROM realizar, contener  WHERE contener.codigo_rutina = realizar.codigo_rutina AND realizar.dni_usuario = ? AND realizar.ejecutada = 1 AND realizar.fecha BETWEEN ? AND ?",[dni, fechaInicio, fechaFin], function (error, rows) {
        if (error) {
            console.log(error);
        }
        else {
            return res.json(rows);
        }
    });
}
// Función para obtener la suma del total de repeticiones ejecutadas en un rango de fechas
exports.totalRepeticiones = function (req, res) {
    var dni = req.params.dni;
    var codigoActividad = req.params.codigoActividad;
    var fechaInicio = req.params.fechaInicio;
    var fechaFin = req.params.fechaFin
    conexion.query("SELECT SUM(total) AS total FROM realizar, contener WHERE contener.codigo_rutina = realizar.codigo_rutina AND realizar.dni_usuario = ? AND realizar.ejecutada = 1 AND contener.codigo_actividad = ? AND realizar.fecha BETWEEN ? AND ? ", [dni, codigoActividad, fechaInicio, fechaFin], function (error, rows, fields) {
        if (error) {
            console.log(error);
        }
        else {
            return res.json(rows);
        }
    });
}