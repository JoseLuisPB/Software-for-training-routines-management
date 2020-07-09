// Módulo que gestiona la tabla rutina
// Módulos invocados
var conexion = require("./conexion");

// Función para obtener los roles de un usuario según su dni
exports.get = function (req, res) {
    var dni_usuario = req.params.dni_usuario;
    conexion.query("SELECT codigo_rol FROM tener WHERE dni_usuario=?", [dni_usuario], function (error, rows) {
        if (error) {
            console.log(error);
        }
        else {
            return res.json(rows)
        }
    });
}
// Función para listar todos los roles que tiene un usuario.
exports.numRolesUsuario = function (req, res) {
    var dni_usuario = req.params.dni_usuario;
    conexion.query("SELECT COUNT(codigo_rol) AS total FROM tener WHERE dni_usuario = ?", [dni_usuario], function (error, rows, fields) {
        if (error) {
            console.log(error);
        }
        else {
            return res.json(rows);
        }
    });
}
// Función para añadir un rol
exports.add = function (req, res) {
    var json = JSON.parse(JSON.stringify(req.body));
    var datos = {
        dni_usuario: json.Dni_usuario,
        codigo_rol: json.Codigo_rol
    }
    conexion.query("INSERT INTO tener set ? ", datos, function (error, rows) {
        if (error) {
            console.log(error);
        }
        else {
            console.log("Rol insertado");
        }
    });
    res.end();
}

// Función eliminar un rol
exports.delete = function (req, res) {
    var dni_usuario = req.params.dni_usuario;
    var codigo_rol = req.params.codigo_rol;
    conexion.query("DELETE FROM tener WHERE dni_usuario = ? AND codigo_rol = ?  ", [dni_usuario, codigo_rol], function (error, rows) {
        if (error) {
            console.log(error);
        }
        else {
            console.log("Rol eliminado")
        }
    });
    res.end();
}