// Gestión de peticiones a entrenador
// Imports
var express = require("express");
var router = express.Router();
var trainer = require("../controllers/entrenador");

//Manejadores de rutas
router.get("/entrenador/:dni", trainer.get);
router.get("/listaEntrenadoresActivos", trainer.listaEntrenadoresActivos);
router.get("/numeroAlumnosAsignados/:dni", trainer.numeroAlumnosAsignados);
router.post("/entrenador", trainer.add);
router.put("/entrenador", trainer.update);

module.exports = router;