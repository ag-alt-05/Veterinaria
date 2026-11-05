USE Veterinaria;

-- FORMATO A: CLIENTE
ALTER TABLE cliente
ADD COLUMN telefono_alternativo VARCHAR(20),
ADD COLUMN colonia VARCHAR(100),
ADD COLUMN ciudad VARCHAR(100),
ADD COLUMN estado VARCHAR(100),
ADD COLUMN codigo_postal VARCHAR(10),
ADD COLUMN contacto_emergencia VARCHAR(120),
ADD COLUMN parentesco_emergencia VARCHAR(50),
ADD COLUMN telefono_emergencia VARCHAR(20),
ADD COLUMN observaciones TEXT;

-- FORMATO B: MASCOTA
ALTER TABLE mascota
ADD COLUMN edad_aproximada VARCHAR(30),
ADD COLUMN tamano VARCHAR(20),
ADD COLUMN esterilizado VARCHAR(20),
ADD COLUMN vacunas VARCHAR(20),
ADD COLUMN alergias TEXT,
ADD COLUMN observaciones TEXT;

-- FORMATO C: CONSULTA
ALTER TABLE consulta
ADD COLUMN duracion_sintomas VARCHAR(100),
ADD COLUMN frecuencia_cardiaca INT,
ADD COLUMN frecuencia_respiratoria INT,
ADD COLUMN estado_general TEXT,
ADD COLUMN antecedentes_clinicos TEXT,
ADD COLUMN exploracion_fisica TEXT,
ADD COLUMN proxima_revision DATE,
ADD COLUMN notas_adicionales TEXT;

-- FORMATO D: RECETA
ALTER TABLE receta
ADD COLUMN proxima_revision DATE,
ADD COLUMN firma_veterinario VARCHAR(150);