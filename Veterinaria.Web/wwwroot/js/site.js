// =========================================
// CONFIGURACIÓN DEL TEMA OSCURO O CLASRO
// =========================================

document.addEventListener("DOMContentLoaded", function () {

    const botonTema = document.getElementById("boton-tema");
    const iconoTema = document.getElementById("icono-tema");

    // Recuperar el tema guardado o utilizar el modo claro.
    const temaGuardado = localStorage.getItem("tema-veterinaria") || "claro";

    aplicarTema(temaGuardado);

    // Cambiar el tema al pulsar el botón.
    if (botonTema) {
        botonTema.addEventListener("click", function () {

            const temaActual = document.documentElement.getAttribute("data-tema");

            const nuevoTema = temaActual === "oscuro" ? "claro" : "oscuro";

            aplicarTema(nuevoTema);

            // Guardar la preferencia del usuario.
            localStorage.setItem("tema-veterinaria", nuevoTema);
        });
    }

    // Aplicar el tema seleccionado.
    function aplicarTema(tema) {

        document.documentElement.setAttribute("data-tema", tema);

        if (iconoTema) {

            if (tema === "oscuro") {
                iconoTema.className = "bi bi-sun-fill";
                botonTema.setAttribute("aria-label", "Cambiar a modo claro");
                botonTema.setAttribute("title", "Cambiar a modo claro");
            }
            else {
                iconoTema.className = "bi bi-moon-fill";
                botonTema.setAttribute("aria-label", "Cambiar a modo oscuro");
                botonTema.setAttribute("title", "Cambiar a modo oscuro");
            }
        }
    }
});