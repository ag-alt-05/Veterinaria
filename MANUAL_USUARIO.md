# Manual de Usuario
## Sistema de Gestión Veterinaria

### Introducción

El Sistema de Gestión Veterinaria permite administrar la información de los clientes y sus mascotas de manera sencilla y organizada.

Mediante este sistema, el usuario puede consultar, buscar, registrar, editar y visualizar información de clientes y mascotas. También cuenta con restricciones para evitar la eliminación de registros que tengan información relacionada.

---

# 1. Inicio

Al ingresar al sistema, se muestra la página principal. Desde el menú de navegación superior se puede acceder a los diferentes apartados disponibles.

Para utilizar las funciones de clientes y mascotas, se deben seleccionar las opciones correspondientes en el menú.

![Pantalla principal del sistema](img/manual/inicio.png)


---

# 2. Consultar Clientes

Para consultar los clientes registrados:

1. Seleccionar la opción **Clientes** en el menú de navegación.
2. El sistema mostrará el listado de clientes registrados.
3. En la tabla se puede consultar la información principal de cada cliente.
4. Desde las acciones disponibles se puede acceder a sus detalles, editar su información o intentar eliminarlo.

![Listado de clientes](img/manual/clientes.png)

---

# 3. Buscar Cliente

El sistema cuenta con un buscador para localizar clientes registrados.

Para realizar una búsqueda:

1. Ingresar al apartado **Clientes**.
2. Escribir el nombre o algún dato del cliente en el campo de búsqueda.
3. Presionar el botón **Buscar**.
4. El sistema mostrará los registros que coincidan con el texto ingresado.

Para volver a consultar todos los clientes, se puede limpiar el campo de búsqueda y realizar nuevamente la consulta.

![Búsqueda de clientes](img/manual/buscar_cliente.png)

---

# 4. Registrar Cliente

Para registrar un nuevo cliente:

1. Ingresar al apartado **Clientes**.
2. Seleccionar el botón **Nuevo cliente** o la opción equivalente para agregar un registro.
3. Completar los campos solicitados, como nombre, apellidos y datos de contacto.
4. Verificar que la información sea correcta.
5. Presionar el botón **Guardar**.

Si el registro se realiza correctamente, el sistema mostrará un mensaje de confirmación.

![Formulario para registrar un cliente](img/manual/registrar_cliente.png)

---

# 5. Editar Cliente

Para modificar la información de un cliente:

1. Ingresar al apartado **Clientes**.
2. Localizar al cliente que se desea modificar.
3. Seleccionar el botón **Editar** correspondiente a su registro.
4. Modificar los campos necesarios.
5. Presionar **Guardar cambios**.

El sistema actualizará la información y mostrará un mensaje de confirmación.

![Formulario para editar un cliente](img/manual/editar_cliente.png)

---

# 6. Ver Detalle Cliente

Para consultar toda la información de un cliente:

1. Ingresar al apartado **Clientes**.
2. Localizar el registro correspondiente.
3. Seleccionar el botón **Detalles** o el icono de visualización.
4. El sistema mostrará la información completa del cliente.

En esta sección se pueden consultar sus datos personales y de contacto.

![Detalle de un cliente](img/manual/detalle_cliente.png)

---

# 7. Ver Mascotas de un Cliente

El sistema permite consultar las mascotas que pertenecen a un cliente.

Para hacerlo:

1. Ingresar al apartado **Clientes**.
2. Localizar al cliente correspondiente.
3. Abrir su información o seleccionar la opción para consultar sus mascotas.
4. El sistema mostrará las mascotas asociadas a ese propietario.

Esta función permite identificar rápidamente qué animales están registrados a nombre de cada cliente.

![Mascotas asociadas a un cliente](img/manual/mascotas_cliente.png)

---

# 8. Consultar / Filtrar Mascotas

Para consultar las mascotas registradas:

1. Seleccionar la opción **Mascotas** en el menú de navegación.
2. El sistema mostrará el listado de mascotas.
3. Se puede utilizar el campo de búsqueda para localizar registros.
4. Ingresar el texto que se desea buscar y presionar **Buscar**.
5. El sistema mostrará las mascotas que coincidan con la búsqueda.

En el listado también se encuentran disponibles las acciones para consultar detalles, editar y eliminar registros.

![Listado y búsqueda de mascotas](img/manual/mascotas.png)

---

# 9. Registrar Mascota

Para registrar una nueva mascota:

1. Ingresar al apartado **Mascotas**.
2. Seleccionar el botón para agregar una nueva mascota.
3. Completar la información solicitada, como nombre, especie, raza, sexo y demás datos disponibles.
4. Seleccionar al cliente que será su propietario.
5. Verificar que la información sea correcta.
6. Presionar **Guardar**.

Si el registro se realiza correctamente, el sistema mostrará un mensaje de confirmación.

![Formulario para registrar una mascota](img/manual/registrar_mascota.png)

---

# 10. Editar Mascota

Para modificar la información de una mascota:

1. Ingresar al apartado **Mascotas**.
2. Localizar la mascota que se desea modificar.
3. Seleccionar el botón **Editar**.
4. Actualizar los campos necesarios.
5. Presionar **Guardar cambios**.

El sistema actualizará la información de la mascota.

![Formulario para editar una mascota](img/manual/editar_mascota.png)

---

# 11. Ver Detalle Mascota

Para consultar la información completa de una mascota:

1. Ingresar al apartado **Mascotas**.
2. Localizar el registro correspondiente.
3. Seleccionar el botón **Detalles**.
4. El sistema mostrará la información registrada de la mascota.

En esta sección se pueden consultar sus características y los datos relacionados con su propietario.

![Detalle de una mascota](img/manual/detalle_mascota.png)

---

# 12. Reglas de eliminación

El sistema cuenta con restricciones para proteger la información relacionada entre clientes, mascotas y consultas.

Las siguientes reglas deben tomarse en cuenta:

- **Clientes:** no se puede eliminar un cliente que tenga mascotas registradas.
- **Mascotas:** no se puede eliminar una mascota que tenga consultas médicas registradas.

Cuando se intenta eliminar un registro que tiene información relacionada, el sistema muestra un mensaje indicando que no es posible realizar la operación.

Estas restricciones ayudan a conservar la integridad de la información almacenada.

---

## Fin del manual

El sistema permite administrar la información de clientes y mascotas mediante operaciones sencillas de consulta, búsqueda, registro, edición y visualización de detalles.