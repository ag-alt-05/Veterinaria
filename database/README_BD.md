# Base de datos Veterinaria

## Orden de ejecución

1. `00_crear_bd.sql`
2. `01_datos_prueba.sql`
3. `02_pruebas.sql`

## Importante

La base de datos local NO se sincroniza automáticamente con GitHub. GitHub conserva los scripts SQL para que cada integrante reconstruya la misma base.

**ADVERTENCIA:** `00_crear_bd.sql` contiene `DROP DATABASE IF EXISTS Veterinaria`; al ejecutarlo se elimina la base local Veterinaria y se crea nuevamente. No ejecutarlo sobre datos que deban conservarse.

El esquema sigue las nueve tablas oficiales de la Práctica 5 — Parte 2. Los formatos A-D incluyen campos adicionales que requerirán coordinación con Backend antes de ampliar el esquema.

## Práctica 7

Archivo agregado:

- 04_verificacion_final_p7.sql

Finalidad:
comprobar el estado final de la base
durante las pruebas de aceptación,
incluyendo las 9 tablas, sus PK, sus FK
y las reglas de integridad referencial.

Orden recomendado de ejecucion:

1. 00_crear_bd.sql
2. 01_datos_prueba.sql
3. 02_pruebas.sql
4. 03_verificacion_p6.sql
5. 04_verificacion_final_p7.sql
   
### Advertencia imporrtante

El archivo `00_crear_bd.sql` elimina y reconstruye
la base de datos local.

No ejecutarlo sobre una base con información
importante sin realizar un respaldo previamente.