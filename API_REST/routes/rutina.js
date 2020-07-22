// Gestión de peticiones a usuario
// Imports
var express = require("express");
var router = express.Router();
var routine = require("../controllers/rutina");

//Manejadores de rutas
router.get("/rutina", routine.list);
router.get("/listaRutinasEntrenador/:dni_usuario", routine.listaRutinasEntrenador);
router.get("/listaRutinasActivas/:dni_usuario", routine.listaRutinasActivas);
router.get("/rutina/:codigo", routine.get);
router.get("/busquedaNombreRutina/:nombre/:dni_usuario", routine.busquedaNombreRutina);
router.post("/rutina", routine.add);
router.put("/rutina", routine.update);
router.put("/activarRutina/:codigo", routine.activarRutina);
router.put("/desactivarRutina/:codigo", routine.desactivarRutina);
router.get("/ultimaRutina", routine.ultimaRutina);

module.exports = router;
