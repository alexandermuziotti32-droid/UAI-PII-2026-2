using System;
using System.Collections.Generic;
using System;
using System.Collections.Generic; // Necesario para usar la estructura Cola (Queue)
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ServicioDeCombis
{
    public partial class SerivicioDeCombis : Form
    {
        // Declaramos la Cola (FIFO: primero en entrar, primero en salir) para los pasajeros en espera
        Queue<string> colaEspera = new Queue<string>();

        // Contador para llevar la cuenta de cuántos pasajeros ya subieron a la combi
        int pasajerosAbordo = 0;

        // Constante con la capacidad máxima de la combi (19 lugares según la consigna)
        const int CAPACIDAD_MAXIMA = 19;

        public SerivicioDeCombis()
        {
            InitializeComponent();
        }

        // PUNTO E: Procedimiento para actualizar el ListBox ante cada modificación en la cola
        private void ActualizarListaEspera()
        {
            // Limpiamos el ListBox para volver a cargarlo actualizado sin repetir nombres
            lstEnEspera.Items.Clear();

            // Recorremos cada pasajero que está en la cola y lo agregamos al ListBox
            foreach (string pasajero in colaEspera)
            {
                lstEnEspera.Items.Add(pasajero);
            }
        }

        // PUNTO C: Botón ANOTAR para agregar elementos a la estructura (Encolar)
        private void btnAnotar_Click(object sender, EventArgs e)
        {
            // Verificamos que la caja de texto no esté vacía antes de agregar
            if (string.IsNullOrWhiteSpace(txtPasajero.Text))
            {
                MessageBox.Show("Por favor, ingrese el nombre del pasajero.", "Dato faltante", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPasajero.Focus();
                return;
            }

            // Agregamos (Enqueue) el pasajero al final de la cola de espera
            colaEspera.Enqueue(txtPasajero.Text.Trim());

            // Llamamos al procedimiento para mostrar la lista actualizada en pantalla
            ActualizarListaEspera();

            // Limpiamos el cuadro de texto y dejamos el cursor listo para el siguiente
            txtPasajero.Clear();
            txtPasajero.Focus();
        }

        // PUNTOS D, G y H: Botón SUBIR A LA COMBI para quitar elementos de la estructura (Desencolar)
        private void btnSubir_Click(object sender, EventArgs e)
        {
            // PUNTO G: Si la combi ya llegó a los 19 pasajeros, no dejamos subir a nadie más
            if (pasajerosAbordo >= CAPACIDAD_MAXIMA)
            {
                MessageBox.Show("La combi ya está llena (19 pasajeros). No se pueden agregar más.", "Combi llena", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            // Validamos que haya al menos una persona esperando en la cola
            if (colaEspera.Count == 0)
            {
                MessageBox.Show("No hay pasajeros en espera para subir.", "Lista vacía", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // PUNTO D: Quitamos (Dequeue) al primer pasajero que llegó a la cola
            string pasajeroSubiendo = colaEspera.Dequeue();

            // Sumamos 1 al contador de pasajeros arriba de la combi
            pasajerosAbordo++;

            // Actualizamos el ListBox porque salió una persona de la espera
            ActualizarListaEspera();

            // PUNTO H: Cuando se completa el lugar 19, mostramos el mensaje de que puede emprender viaje
            if (pasajerosAbordo == CAPACIDAD_MAXIMA)
            {
                MessageBox.Show($"Subió {pasajeroSubiendo}. ¡La combi está llena (19/19) y ya puede emprender viaje!", "Viaje listo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnSubir.Enabled = false; // Desactivamos el botón para evitar que suban más
            }
        }

        // Eventos vacíos que se generaron al hacer doble clic en el diseñador (los dejamos para evitar errores)
        private void TerminalObelisco_Enter(object sender, EventArgs e) { }
        private void txtPasajero_TextChanged(object sender, EventArgs e) { }
        private void lblPasajero_Click(object sender, EventArgs e) { }
        private void lblEspera_Click(object sender, EventArgs e) { }
    }
}
