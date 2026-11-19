# Base de datos Veterinaria

## Orden de ejecución

1. `00_crear_bd.sql`
2. `01_datos_prueba.sql`
3. `02_pruebas.sql`

## Importante

La base de datos local NO se sincroniza automáticamente con GitHub. GitHub conserva los scripts SQL para que cada integrante reconstruya la misma base.

**ADVERTENCIA:** `00_crear_bd.sql` contiene `DROP DATABASE IF EXISTS Veterinaria`; al ejecutarlo se elimina la base local Veterinaria y se crea nuevamente. No ejecutarlo sobre datos que deban conservarse.

El esquema sigue las nueve tablas oficiales de la Práctica 5 — Parte 2. Los formatos A-D incluyen campos adicionales que requerirán coordinación con Backend antes de ampliar el esquema.
