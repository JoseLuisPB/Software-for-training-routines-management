// Módulo que gestiona la tabla usuario
// Módulos invocados
var conexion = require("./conexion");

// Función para listar todos los usuarios
exports.list = function (req, res) {
    conexion.query("SELECT * FROM usuario", function (error, rows, fields) {
        if (error) {
            console.log(error);
        }
        else {
            return res.json(rows);
        }
    });
}

// Función para obtener la lista de usuarios activos asignados a un entrenador.
exports.listaUsuarioActivosAsignados = function (req, res) {
    var entrenador_asignado = req.params.entrenador_asignado;
    conexion.query("SELECT dni, nombre, apellidos FROM alumno, usuario WHERE alumno.dni_usuario = usuario.dni AND alumno.entrenador_asignado= ? AND usuario.activo = 1", [entrenador_asignado], function (error, rows) {
        if (error) {
            console.log(error);
        }
        else {
            return res.json(rows)
        }
    });
}
// Función para obtener un usuario utilizando su DNI
exports.get = function (req, res) {
    var dni = req.params.dni;
    conexion.query("SELECT * FROM usuario WHERE dni=?", [dni], function (error, rows) {
        if (error) {
            console.log(error);
        }
        else {
            return res.json(rows)
        }
    });
}

// Función para crear un usuario en la base de datos
exports.add = function (req, res) {
    var json = JSON.parse(JSON.stringify(req.body));
    conexion.query("INSERT INTO usuario set ?", json, function (error, rows) {
        if (error) {
            console.log(error)
        }
        else {
            console.log("Usuario insertado en la base de datos")
        }
    });
    res.end();
}

// Función para actualizar un usuario en la base de datos
exports.update = function (req, res) {
    var json = JSON.parse(JSON.stringify(req.body));
    var dni = json.Dni;
    conexion.query("UPDATE usuario set ? where dni= ?", [json, dni], function (error, rows) {
        if (error) {
            console.log(error);
        }
        else {
            console.log("Usuario actualizado en la base de datos");
        }
    });
    res.end();
}

// Función para dar de alta a un usuario
exports.altaUsuario = function (req, res) {
    var dni = req.params.dni;
    conexion.query("UPDATE usuario SET activo = 1, cambiar_password = 1 WHERE dni=? ", [dni], function (error, rows) {
        if (error) {
            console.log(error);
        }
        else {
            console.log("Usuario dado de alta");
        }
    });
    res.end();
}

// Función para dar de baja a un usuario
exports.bajaUsuario = function (req, res) {
    var dni = req.params.dni;
    conexion.query("UPDATE usuario set activo = 0, cambiar_password = 0 where dni= ?", [dni], function (error, rows) {
        if (error) {
            console.log(error);
        }
        else {
            console.log("Usuario dado de baja");
        }
    });
    res.end();
}

// Función para resetear el password
exports.resetPassword = function (req, res) {
    var dni = req.params.dni;
    conexion.query("UPDATE usuario set cambiar_password = 1 where dni= ?", [dni], function (error, rows) {
        if (error) {
            console.log(error);
        }
        else {
            console.log("Contraseña reseteada");
        }
    });
    res.end();
}

// Función para actualizar el password
exports.actualizarPassword = function (req, res) {
    var contrasenya = req.params.contrasenya;
    var dni = req.params.dni;
    conexion.query("UPDATE usuario set contrasenya = ?, cambiar_password = 0 where dni= ?", [contrasenya, dni], function (error, rows) {
        if (error) {
            console.log(error);
        }
        else {
            console.log("Contraseña actualizada");
        }
    });
    res.end();
}

// Función para mostrar los alumnos asignados al entrenador --> No se usa
exports.usuariosEntrenador = function (req, res) {
    var entrenador = req.params.entrenador;
    conexion.query("SELECT * FROM usuario WHERE entrenador_asignado=?", [entrenador], function (error, rows) {
        if (error) {
            console.log(error);
        }
        else {
            return res.json(rows)
        }
    });
}

// Filtros

// Función para listar todos los usuarios activos
exports.listaActivos = function (req, res) {
    conexion.query("SELECT * FROM usuario WHERE activo = 1", function (error, rows, fields) {
        if (error) {
            console.log(error);
        }
        else {
            return res.json(rows);
        }
    });
}

// Función para listar todos los usuarios inactivos
exports.listaInactivos = function (req, res) {
    conexion.query("SELECT * FROM usuario WHERE activo = 0", function (error, rows, fields) {
        if (error) {
            console.log(error);
        }
        else {
            return res.json(rows);
        }
    });
}

// Función para filtrar por nombre
exports.busquedaNombre = function (req, res) {
    var nombre = req.params.nombre;
    conexion.query("SELECT * FROM usuario WHERE nombre LIKE ? ",nombre, function (error, rows, fields) {
        if (error) {
            console.log(error);
        }
        else {
            return res.json(rows);
        }
    });
}

