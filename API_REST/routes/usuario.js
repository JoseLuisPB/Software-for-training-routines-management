// Gestión de peticiones a usuario
// Imports
// Invocación al módulo express
var express = require("express");
// Objeto router del módulo express, se utiliza para agrupar manejadores de rutas.
var router = express.Router();
// Fichero con las funciones a ejecutar
var user = require("../controllers/usuario");

//Manejadores de rutas
router.get("/usuario", user.list);
router.get("/usuario/:dni", user.get);
router.get("/listaUsuarioActivosAsignados/:entrenador_asignado", user.listaUsuarioActivosAsignados)
router.get("/listaActivos", user.listaActivos);
router.get("/listaInactivos", user.listaInactivos);
router.get("/busquedaNombre/:nombre", user.busquedaNombre);
router.post("/usuario", user.add);
router.put("/usuario", user.update);
router.put("/resetPassword/:dni", user.resetPassword);
router.put("/actualizarPassword/:contrasenya/:dni", user.actualizarPassword);

// Export del objeto router para poder usarlo en otros archivos
module.exports = router;