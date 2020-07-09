// Gestión de peticiones a tener
// Imports
var express = require("express");
var router = express.Router();
var tener = require("../controllers/tener");

//Manejadores de rutas
router.get("/tener/:dni_usuario", tener.get);
router.get("/numRolesUsuario/:dni_usuario", tener.numRolesUsuario);
router.post("/tener", tener.add);
router.delete("/tener/:dni_usuario/:codigo_rol", tener.delete);
module.exports = router;