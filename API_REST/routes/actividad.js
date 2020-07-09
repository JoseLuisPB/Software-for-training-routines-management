// Gestión de peticiones a actividad
// Imports
var express = require("express");
var router = express.Router();
var actividad = require("../controllers/actividad");

//Manejadores de rutas
router.get("/actividad", actividad.list);
router.get("/actividad/:codigo", actividad.get);
router.get("/ultimaActividadCreada", actividad.ultimaActividadCreada)
router.post("/actividad", actividad.add);
router.put("/actividad", actividad.update);
router.put("/altaActividad/:codigo", actividad.altaActividad);
router.put("/bajaActividad/:codigo", actividad.bajaActividad);
router.get("/tipoActividad", actividad.tipoActividad);
router.get("/actividadesPorTipo/:tipoActividad", actividad.actividadesPorTipo);

module.exports = router;