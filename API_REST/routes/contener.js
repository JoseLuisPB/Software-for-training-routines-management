// Gestión de peticiones a usuario
// Imports
var express = require("express");
var router = express.Router();
var contener = require("../controllers/contener");

//Manejadores de rutas
router.get("/listaActividadesRutina/:codigo_rutina", contener.listaActividadesRutina);
router.get("/actividadesEjecutadasRango/:dni/:fechaInicio/:fechaFin", contener.actividadesEjecutadasRango);
router.get("/totalRepeticiones/:dni/:codigoActividad/:fechaInicio/:fechaFin", contener.totalRepeticiones);
router.post("/contener/", contener.add);
router.delete("/contener/:codigo_rutina", contener.delete);

module.exports = router;