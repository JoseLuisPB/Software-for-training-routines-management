-- --------------------------------------------------------
-- Host:                         127.0.0.1
-- Versión del servidor:         10.4.8-MariaDB - mariadb.org binary distribution
-- SO del servidor:              Win64
-- HeidiSQL Versión:             10.2.0.5599
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;


-- Volcando estructura de base de datos para gimnasio
CREATE DATABASE IF NOT EXISTS `gimnasio` /*!40100 DEFAULT CHARACTER SET latin1 COLLATE latin1_spanish_ci */;
USE `gimnasio`;

-- Volcando estructura para tabla gimnasio.actividad
CREATE TABLE IF NOT EXISTS `actividad` (
  `codigo` int(11) NOT NULL DEFAULT 0,
  `nombre` varchar(45) COLLATE latin1_spanish_ci NOT NULL,
  `tipo` varchar(30) COLLATE latin1_spanish_ci NOT NULL,
  `nivel` varchar(30) COLLATE latin1_spanish_ci NOT NULL,
  `imagen` text COLLATE latin1_spanish_ci DEFAULT NULL,
  `activa` tinyint(1) NOT NULL,
  `dni_usuario` varchar(9) COLLATE latin1_spanish_ci NOT NULL,
  PRIMARY KEY (`codigo`),
  KEY `FK_actividad_entrenador` (`dni_usuario`),
  CONSTRAINT `FK_actividad_entrenador` FOREIGN KEY (`dni_usuario`) REFERENCES `entrenador` (`dni_usuario`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_spanish_ci;

-- Volcando datos para la tabla gimnasio.actividad: ~37 rows (aproximadamente)
/*!40000 ALTER TABLE `actividad` DISABLE KEYS */;
INSERT INTO `actividad` (`codigo`, `nombre`, `tipo`, `nivel`, `imagen`, `activa`, `dni_usuario`) VALUES
	(1, 'Press de banca con barra', 'pecho', 'Baja', '', 1, '12345671f'),
	(2, 'Curl de biceps concentrado', 'biceps', 'Baja', '', 1, '12345671f'),
	(3, 'Pullover', 'pecho', 'Media', '', 1, '12345671f'),
	(4, 'Curl de biceps Scott en banco', 'biceps', 'Media', '', 1, '12345671f'),
	(5, 'Press Arnold', 'hombros', 'Elevada', '', 1, '12345671f'),
	(6, 'Press militar', 'hombros', 'Media', '', 1, '12345671f'),
	(7, 'Encogimiento de hombros', 'hombros', 'Baja', '', 1, '12345671f'),
	(8, 'Copa con mancuerna', 'triceps', 'Media', '', 1, '12345671f'),
	(9, 'Peso muerto con barra', 'espalda', 'Elevada', '', 1, '12345671f'),
	(10, 'Crunches', 'abdominales', 'Baja', '', 1, '12345671f'),
	(11, 'Zancada hacia atrás con barra', 'isquiotibiales', 'Media', '', 1, '12345671f'),
	(12, 'Zancadas', 'cuadriceps', 'Elevada', '', 1, '12345671f'),
	(13, 'Patada trasera', 'gluteos', 'Baja', '', 1, '12345671f'),
	(14, 'Plancha', 'abdominales', 'Media', '', 1, '12345671f'),
	(15, 'Giros rusos', 'abdominales', 'Elevada', '', 1, '12345671f'),
	(16, 'Elevación piernas', 'abdominales', 'Media', '', 1, '12345671f'),
	(17, 'Curl de biceps de tipo martillo', 'biceps', 'Media', '', 1, '12345671f'),
	(18, 'Curl Zottman', 'biceps', 'Elevada', '', 1, '12345671f'),
	(19, 'Sentadillas', 'cuadriceps', 'Baja', '', 1, '12345671f'),
	(20, 'Sentadilla búlgara', 'cuadriceps', 'Baja', '', 1, '12345671f'),
	(21, 'Press de piernas', 'cuadriceps', 'Media', '', 1, '12345671f'),
	(22, 'Polea al pecho', 'espalda', 'Baja', '', 1, '12345671f'),
	(23, 'Polea tras nuca', 'espalda', 'Baja', '', 1, '12345671f'),
	(24, 'Remo con mancuerna', 'espalda', 'Media', '', 1, '12345671f'),
	(25, 'Step', 'gluteos', 'Baja', '', 1, '12345671f'),
	(26, 'Lunge', 'gluteos', 'Media', '', 1, '12345671f'),
	(27, 'Saltos en banco', 'gluteos', 'Elevada', '', 1, '12345671f'),
	(28, 'Remo al mentón', 'hombros', 'Baja', '', 1, '12345671f'),
	(29, 'Femoral de una sola pierna', 'isquiotibiales', 'Baja', '', 1, '12345671f'),
	(30, 'Prensa vertical', 'isquiotibiales', 'Elevada', '', 1, '12345671f'),
	(31, 'Peso muerto rumano', 'isquiotibiales', 'Elevada', '', 1, '12345671f'),
	(32, 'Aperturas con mancuernas', 'pecho', 'Elevada', '', 1, '12345671f'),
	(33, 'Flexiones', 'pecho', 'Baja', '', 1, '12345671f'),
	(34, 'Rompe craneo', 'triceps', 'Elevada', '', 1, '12345671f'),
	(35, 'Fondo de triceps', 'triceps', 'Baja', '', 1, '12345671f'),
	(36, 'Extensión de triceps en polea', 'triceps', 'Baja', '', 1, '12345671f'),
	(37, 'Abdominales oblicuos', 'abdominales', 'Media', '', 1, '12345671f');
/*!40000 ALTER TABLE `actividad` ENABLE KEYS */;

-- Volcando estructura para tabla gimnasio.alumno
CREATE TABLE IF NOT EXISTS `alumno` (
  `dni_usuario` varchar(9) COLLATE latin1_spanish_ci NOT NULL,
  `entrenador_asignado` varchar(9) COLLATE latin1_spanish_ci DEFAULT NULL,
  `dolencias` text COLLATE latin1_spanish_ci DEFAULT NULL,
  `objetivo` text COLLATE latin1_spanish_ci DEFAULT NULL,
  PRIMARY KEY (`dni_usuario`),
  CONSTRAINT `FK_alumno_usuario` FOREIGN KEY (`dni_usuario`) REFERENCES `usuario` (`dni`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_spanish_ci;

-- Volcando datos para la tabla gimnasio.alumno: ~9 rows (aproximadamente)
/*!40000 ALTER TABLE `alumno` DISABLE KEYS */;
INSERT INTO `alumno` (`dni_usuario`, `entrenador_asignado`, `dolencias`, `objetivo`) VALUES
	('12345672p', '12345671f', '', '');
/*!40000 ALTER TABLE `alumno` ENABLE KEYS */;

-- Volcando estructura para tabla gimnasio.contener
CREATE TABLE IF NOT EXISTS `contener` (
  `codigo_rutina` int(11) NOT NULL,
  `codigo_actividad` int(11) NOT NULL,
  `series` int(2) DEFAULT NULL,
  `repeticiones` int(3) DEFAULT NULL,
  `total` int(11) DEFAULT NULL,
  PRIMARY KEY (`codigo_rutina`,`codigo_actividad`),
  KEY `FK_contener_actividad` (`codigo_actividad`),
  CONSTRAINT `FK_contener_actividad` FOREIGN KEY (`codigo_actividad`) REFERENCES `actividad` (`codigo`),
  CONSTRAINT `FK_contener_rutina` FOREIGN KEY (`codigo_rutina`) REFERENCES `rutina` (`codigo`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_spanish_ci;

-- Volcando datos para la tabla gimnasio.contener: ~52 rows (aproximadamente)
/*!40000 ALTER TABLE `contener` DISABLE KEYS */;
/*!40000 ALTER TABLE `contener` ENABLE KEYS */;

-- Volcando estructura para tabla gimnasio.entrenador
CREATE TABLE IF NOT EXISTS `entrenador` (
  `dni_usuario` varchar(9) COLLATE latin1_spanish_ci NOT NULL,
  `biografia` text COLLATE latin1_spanish_ci DEFAULT NULL,
  PRIMARY KEY (`dni_usuario`),
  CONSTRAINT `FK_entrenador_usuario` FOREIGN KEY (`dni_usuario`) REFERENCES `usuario` (`dni`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_spanish_ci;

-- Volcando datos para la tabla gimnasio.entrenador: ~10 rows (aproximadamente)
/*!40000 ALTER TABLE `entrenador` DISABLE KEYS */;
INSERT INTO `entrenador` (`dni_usuario`, `biografia`) VALUES
	('12345671f', '');
/*!40000 ALTER TABLE `entrenador` ENABLE KEYS */;

-- Volcando estructura para tabla gimnasio.realizar
CREATE TABLE IF NOT EXISTS `realizar` (
  `dni_usuario` varchar(9) COLLATE latin1_spanish_ci NOT NULL,
  `codigo_rutina` int(11) NOT NULL,
  `fecha` date NOT NULL,
  `ejecutada` tinyint(1) NOT NULL,
  PRIMARY KEY (`dni_usuario`,`codigo_rutina`,`fecha`),
  KEY `FK_realizar_rutina` (`codigo_rutina`),
  CONSTRAINT `FK_realizar_rutina` FOREIGN KEY (`codigo_rutina`) REFERENCES `rutina` (`codigo`),
  CONSTRAINT `FK_realizar_usuario` FOREIGN KEY (`dni_usuario`) REFERENCES `usuario` (`dni`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_spanish_ci;

-- Volcando datos para la tabla gimnasio.realizar: ~70 rows (aproximadamente)
/*!40000 ALTER TABLE `realizar` DISABLE KEYS */;
/*!40000 ALTER TABLE `realizar` ENABLE KEYS */;

-- Volcando estructura para tabla gimnasio.rol
CREATE TABLE IF NOT EXISTS `rol` (
  `codigo` int(11) NOT NULL DEFAULT 0,
  `nombre` varchar(50) COLLATE latin1_spanish_ci DEFAULT NULL,
  PRIMARY KEY (`codigo`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_spanish_ci;

-- Volcando datos para la tabla gimnasio.rol: ~3 rows (aproximadamente)
/*!40000 ALTER TABLE `rol` DISABLE KEYS */;
INSERT INTO `rol` (`codigo`, `nombre`) VALUES
	(0, 'administrador'),
	(1, 'entrenador'),
	(2, 'alumno');
/*!40000 ALTER TABLE `rol` ENABLE KEYS */;

-- Volcando estructura para tabla gimnasio.rutina
CREATE TABLE IF NOT EXISTS `rutina` (
  `codigo` int(11) NOT NULL DEFAULT 0,
  `nombre` varchar(45) COLLATE latin1_spanish_ci NOT NULL,
  `activa` tinyint(1) NOT NULL,
  `dni_usuario` varchar(9) COLLATE latin1_spanish_ci NOT NULL,
  PRIMARY KEY (`codigo`),
  KEY `FK_rutina_usuario` (`dni_usuario`),
  CONSTRAINT `FK_rutina_usuario` FOREIGN KEY (`dni_usuario`) REFERENCES `usuario` (`dni`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_spanish_ci;

-- Volcando datos para la tabla gimnasio.rutina: ~14 rows (aproximadamente)
/*!40000 ALTER TABLE `rutina` DISABLE KEYS */;
/*!40000 ALTER TABLE `rutina` ENABLE KEYS */;

-- Volcando estructura para tabla gimnasio.tener
CREATE TABLE IF NOT EXISTS `tener` (
  `dni_usuario` varchar(9) COLLATE latin1_spanish_ci NOT NULL,
  `codigo_rol` int(11) NOT NULL,
  PRIMARY KEY (`dni_usuario`,`codigo_rol`),
  KEY `FK_tener_rol` (`codigo_rol`),
  CONSTRAINT `FK_tener_rol` FOREIGN KEY (`codigo_rol`) REFERENCES `rol` (`codigo`),
  CONSTRAINT `FK_tener_usuario` FOREIGN KEY (`dni_usuario`) REFERENCES `usuario` (`dni`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_spanish_ci;

-- Volcando datos para la tabla gimnasio.tener: ~15 rows (aproximadamente)
/*!40000 ALTER TABLE `tener` DISABLE KEYS */;
INSERT INTO `tener` (`dni_usuario`, `codigo_rol`) VALUES
	('12345670y', 0),
	('12345671f', 1),
	('12345672p', 2);
/*!40000 ALTER TABLE `tener` ENABLE KEYS */;

-- Volcando estructura para tabla gimnasio.usuario
CREATE TABLE IF NOT EXISTS `usuario` (
  `dni` varchar(9) COLLATE latin1_spanish_ci NOT NULL,
  `nombre` varchar(15) COLLATE latin1_spanish_ci NOT NULL,
  `apellidos` varchar(40) COLLATE latin1_spanish_ci NOT NULL,
  `contrasenya` varchar(100) COLLATE latin1_spanish_ci DEFAULT NULL,
  `direccion` varchar(60) COLLATE latin1_spanish_ci DEFAULT NULL,
  `localidad` varchar(30) COLLATE latin1_spanish_ci DEFAULT NULL,
  `cp` varchar(5) COLLATE latin1_spanish_ci DEFAULT NULL,
  `pais` varchar(30) COLLATE latin1_spanish_ci DEFAULT NULL,
  `telefono` varchar(9) COLLATE latin1_spanish_ci DEFAULT NULL,
  `email` varchar(50) COLLATE latin1_spanish_ci DEFAULT NULL,
  `fecha_nacimiento` date DEFAULT NULL,
  `fecha_alta` date DEFAULT NULL,
  `activo` tinyint(1) NOT NULL,
  `cambiar_password` tinyint(1) NOT NULL,
  PRIMARY KEY (`dni`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_spanish_ci;

-- Volcando datos para la tabla gimnasio.usuario: ~13 rows (aproximadamente)
/*!40000 ALTER TABLE `usuario` DISABLE KEYS */;
INSERT INTO `usuario` (`dni`, `nombre`, `apellidos`, `contrasenya`, `direccion`, `localidad`, `cp`, `pais`, `telefono`, `email`, `fecha_nacimiento`, `fecha_alta`, `activo`, `cambiar_password`) VALUES
	('12345670y', 'Admin', 'Administrador', 'dc483e80a7a0bd9ef71d8cf973673924', 'Avenida Maisonave', 'Alicante', '03002', 'España', '666000666', 'administrador@gmail.com', '1900-01-01', '2020-07-01', 1, 0),
	('12345671f', 'Entrenador', 'Entrenador', 'dc483e80a7a0bd9ef71d8cf973673924', 'Avenida Maisonave', 'Alicante', '03002', 'España', '660066006', 'entrenador@gmail.com', '1900-01-01', '2020-07-01', 1, 0),
	('12345672p', 'Alumno', 'Alumno', 'dc483e80a7a0bd9ef71d8cf973673924', 'Avenida Maisonave', 'Alicante', '03002', 'España', '660606066', 'alumno@gmail.com', '1900-01-01', '2020-07-01', 1, 0);
/*!40000 ALTER TABLE `usuario` ENABLE KEYS */;

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
