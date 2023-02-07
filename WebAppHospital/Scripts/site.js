const eliminarDoctor = (doctor) => {
    console.log(doctor.hash.replace('#', ''));
    alertify.confirm('Eliminar doctor', 'A continuación se eliminará este doctor, desea continuar?',
        function () {
            $.ajax({
                url: location.origin + "/Doctor/EliminarDoctor", // Url
                data: {
                    idDoctor: doctor.hash.replace('#', '')
                    // ...
                },
                async: false,
                type: "post"  // Verbo HTTP
            })
                // Se ejecuta si todo fue bien.
                .done(function (result) {

                    alertify.success('El doctor se eliminó correctamente');


                })
                // Se ejecuta si se produjo un error.
                .fail(function (xhr, status, error) {
                    console.log(error);
                    alertify.error('El doctor no se eliminó, ya que cuenta con citas relacionadas.');
                })
                // Hacer algo siempre, haya sido exitosa o no.
                .always(function () {
                    setTimeout(() => {
                        window.location.reload();
                    }, "3000")
                    
                });


        }
        , function () {
            /*alertify.error('Cancel') */
        }
    ).set('labels', { ok: 'Sí', cancel: 'No' });
}

const eliminarPaciente = (paciente) => {
    console.log(paciente.hash.replace('#', ''));
    alertify.confirm('Eliminar paciente', 'A continuación se eliminará este paciente, desea continuar?',
        function () {
            $.ajax({
                url: location.origin + "/Paciente/EliminarPaciente", // Url
                data: {
                    idPaciente: paciente.hash.replace('#', '')
                    // ...
                },
                async: false,
                type: "post"  // Verbo HTTP
            })
                // Se ejecuta si todo fue bien.
                .done(function (result) {

                    alertify.success('El paciente se eliminó correctamente');


                })
                // Se ejecuta si se produjo un error.
                .fail(function (xhr, status, error) {
                    console.log(error);
                    alertify.error('El paciente no se eliminó, ya que cuenta con citas relacionadas.');
                })
                // Hacer algo siempre, haya sido exitosa o no.
                .always(function () {
                    setTimeout(() => {
                        window.location.reload();
                    }, "3000")

                });


        }
        , function () {
            /*alertify.error('Cancel') */
        }
    ).set('labels', { ok: 'Sí', cancel: 'No' });
}
const eliminarCita  = (cita) => {
    console.log(cita.hash.replace('#', ''));
    alertify.confirm('Eliminar cita', 'A continuación se eliminará esta cita, desea continuar?',
        function () {
            $.ajax({
                url: location.origin + "/Cita/EliminarCita", // Url
                data: {
                    idCita: cita.hash.replace('#', '')
                    // ...
                },
                async: false,
                type: "post"  // Verbo HTTP
            })
                // Se ejecuta si todo fue bien.
                .done(function (result) {

                    alertify.success('La cita se eliminó correctamente');


                })
                // Se ejecuta si se produjo un error.
                .fail(function (xhr, status, error) {
                    console.log(error);
                    alertify.error('LSuucedio un erro, favor de conmsultar a sistemas.');
                })
                // Hacer algo siempre, haya sido exitosa o no.
                .always(function () {
                    setTimeout(() => {
                        window.location.reload();
                    }, "3000")

                });


        }
        , function () {
            /*alertify.error('Cancel') */
        }
    ).set('labels', { ok: 'Sí', cancel: 'No' });
}