// Módulo que gestiona la tabla rol
// Módulos invocados
var conexion = require("./conexion");

// Función para listar todos los roles ordenados alfabéticamente
exports.list = function (req, res) {
    conexion.query("SELECT * FROM rol ORDER BY nombre ASC", function (error, rows, fields) {
        if (error) {
            console.log(error);
        }
        else {
            return res.json(rows);
        }
    });
}

// Función para obtener un rol según su codigo
exports.get = function (req, res) {
    var codigo = req.params.codigo;
    conexion.query("SELECT * FROM rol WHERE codigo=?", [codigo], function (error, rows) {
        if (error) {
            console.log(error);
        }
        else {
            return res.json(rows)
        }
    });
}