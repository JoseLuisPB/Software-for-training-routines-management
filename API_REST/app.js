'use strict';
var http = require('http');
var debug = require('debug');
var express = require('express');
var path = require('path');
var favicon = require('serve-favicon');
var logger = require('morgan');
var cookieParser = require('cookie-parser');
var bodyParser = require('body-parser');

// Importamos los módulos que contienen los manejadores de ruta para cada fichero 
var usuario = require('./routes/usuario');
var entrenador = require('./routes/entrenador');
var actividad = require('./routes/actividad');
var rutina = require('./routes/rutina');
var alumno = require('./routes/alumno');
var contener = require('./routes/contener');
var realizar = require('./routes/realizar');
var rol = require('./routes/rol');
var tener = require('./routes/tener');
var extra = require('./routes/extra');

// Se guarda el objeto de la aplicación express para acceder a las funciones y propiedades del objeto de la aplicación
var app = express();

// view engine setup
// Le damos el valor 3000 a la variable port
app.set('port', 3000);
app.set('views', path.join(__dirname, 'views'));
app.set('view engine', 'pug');

// uncomment after placing your favicon in /public
//app.use(favicon(__dirname + '/public/favicon.ico'));
app.use(logger('dev'));
app.use(bodyParser.json());
app.use(bodyParser.urlencoded({ extended: false }));
app.use(cookieParser());
app.use(express.static(path.join(__dirname, 'public')));

// Uso de los módulos
// La función use se usa para cargar funciones del middleware
app.use('/', usuario);
app.use('/', entrenador);
app.use('/', rutina);
app.use('/', actividad);
app.use('/', alumno);
app.use('/', contener);
app.use('/', realizar);
app.use('/', rol);
app.use('/', tener);
app.use('/', extra);


// catch 404 and forward to error handler
app.use(function (req, res, next) {
    var err = new Error('Not Found');
    err.status = 404;
    next(err);
});

// error handlers

// development error handler
// will print stacktrace
if (app.get('env') === 'development') {
    app.use(function (err, req, res, next) {
        res.status(err.status || 500);
        res.render('error', {
            message: err.message,
            error: err
        });
    });
}

// production error handler
// no stacktraces leaked to user
app.use(function (err, req, res, next) {
    res.status(err.status || 500);
    res.render('error', {
        message: err.message,
        error: {}
    });
});

// Creamos el servidor http para variable app que contiene el objeto Express()
// Recuperamos el valor de la variable port(el número de puerto) para ponerlo a la escucha.
http.createServer(app).listen(app.get('port'), function () {
    // Mostramos por consola que el servidor se ha iniciado y el puerto que está a la escucha
    console.log('Servidor del gimnasio iniciado, escuchando en el puerto ' + app.get('port'));
});
