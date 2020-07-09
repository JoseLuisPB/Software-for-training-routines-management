// Gestión de peticiones a alumno
// Imports
var express = require("express");
var router = express.Router();
var alumno = require("../controllers/alumno");

//Manejadores de rutas
router.get("/alumno/:dni", alumno.get);
router.post("/alumno", alumno.add);
router.put("/alumno", alumno.update);

module.exports = router;