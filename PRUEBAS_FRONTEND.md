# Universidad Autónoma de Baja California

**Facultad de Ingeniería, Arquitectura y Diseño**

**Integrantes:**
- Luis Angel Martinez Zamaniego
- Carlos Alberto Guzman Ramirez
- Jareth Izhar Aparicio Lopez

**Actividad:** Actividad 7  
**Grupo:** 952  
**Asignatura:** Ambientes de programación visual  

**Lugar y fecha:** Ensenada, Baja California, 16 de noviembre de 2026.

---

# Actividad 7

## ETAPA 2A — FRONTEND

## Tabla de pruebas

| ID | Prueba | Resultado esperado | Resultado obtenido | Estado |
|---|---|---|---|---|
| P01 | Navegación principal | Acceder a Inicio, Clientes y Mascotas. | Correcto. | Aprobado |
| P02 | Gestión de clientes | Listar, agregar, editar y eliminar. | Correcto. | Aprobado |
| P03 | Validación de clientes | Rechazar campos obligatorios vacíos. | Correcto. Se puede acceder sin correo. | Aprobado |
| P04 | Correo electrónico | Validar formato del correo. | Correcto. No se acepta un formato incompleto. | Aprobado |
| P05 | Gestión de mascotas | Listar, agregar, editar y eliminar. | Correcto. | Aprobado |
| P06 | Fecha de nacimiento | Rechazar fechas futuras. | Correcto. | Aprobado |
| P07 | Detalle de mascota | Mostrar datos y propietario correctamente. | Correcto. Se muestra la fecha de nacimiento. | Aprobado |
| P08 | Diseño responsivo móvil | Adaptar interfaz a 390 px. | Correcto. | Aprobado |
| P09 | Diseño responsivo tablet | Adaptar interfaz a 768 px. | Correcto. | Aprobado |
| P10 | Diseño escritorio | Mostrar correctamente a 1366 px. | Correcto. | Aprobado |

## Correcciones realizadas

Durante la revisión y las pruebas del frontend se realizaron las siguientes correcciones:

- Se ajustaron las consultas y operaciones de clientes y mascotas para utilizar las columnas existentes en la base de datos.
- Se incorporó el campo de fecha de nacimiento en los formularios de registro y edición de mascotas.
- Se agregó una validación para impedir el registro de fechas de nacimiento futuras.
- Se incorporó la visualización de la fecha de nacimiento en el detalle de las mascotas.
- Se mejoró el manejo de excepciones durante las operaciones de eliminación de clientes y mascotas.
- Se agregaron mensajes amigables para evitar exponer información técnica de la aplicación.
- Se verificó el funcionamiento de la navegación y de las operaciones principales.
- Se realizaron pruebas de diseño responsivo en dispositivos móviles, tabletas y escritorio.

## Conclusión

Las pruebas realizadas permitieron verificar el funcionamiento de las principales funcionalidades del frontend de la aplicación Veterinaria. Se comprobó la navegación, la gestión de clientes y mascotas, las validaciones de formularios, la visualización de información y la adaptación de la interfaz a diferentes tamaños de pantalla.

Después de realizar las correcciones necesarias, las pruebas documentadas fueron aprobadas y el frontend quedó preparado para su entrega.