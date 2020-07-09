// Módulo que gestiona las funcionalidades extra

// Función que al ejecutarse apagará el servidor
exports.detener = function (req, res) {
    process.exit();
}