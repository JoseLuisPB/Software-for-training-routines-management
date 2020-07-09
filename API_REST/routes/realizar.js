// Gestión de peticiones a realizar
// Imports
var express = require("express");
var router = express.Router();
var realizar = require("../controllers/realizar");

//Manejadores de rutas
router.get("/listaRutinasAlumnoRango/:dni_usuario/:fecha1/:fecha2", realizar.listaRutinasAlumnoRango)
router.get("/realizar/:dni_usuario/:fecha", realizar.get);
router.get("/rutinaFecha/:dni_usuario/:codigo_rutina/:fecha", realizar.rutinaFecha);
router.get("/estadoRutina/:codigo_rutina", realizar.estadoRutina);
router.post("/realizar", realizar.add);
router.put("/rutinaEjecutada/:dni_usuario/:codigo_rutina/:fecha", realizar.rutinaEjecutada);
router.put("/rutinaNoEjecutada/:dni_usuario/:codigo_rutina/:fecha", realizar.rutinaNoEjecutada);
router.delete("/realizar/:dni_usuario/:codigo_rutina/:fecha", realizar.delete)


module.exports = router;