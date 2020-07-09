// Gestión de peticiones a rol
// Imports
var express = require("express");
var router = express.Router();
var rol = require("../controllers/rol");

//Manejadores de rutas
router.get("/rol", rol.list);
router.get("/rol/:codigo", rol.get);


module.exports = router;