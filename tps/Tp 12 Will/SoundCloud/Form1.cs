using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SoundCloud
{
    public partial class SoundCloud : Form
    {
        // =========================================================================
        // 1. ESTRUCTURA DE DATOS: NODO CANCIÓN (LISTA ENLAZADA)
        // =========================================================================
        public class Cancion
        {
            public string Nombre { get; set; }
            public int Duracion { get; set; } // Duración en segundos
            public Cancion Siguiente { get; set; } // Puntero al siguiente nodo

            public Cancion(string nombre, int duracion)
            {
                Nombre = nombre;
                Duracion = duracion;
                Siguiente = null;
            }

            public override string ToString()
            {
                return $"{Nombre} ({Duracion} seg)";
            }
        }

        // Punteros principales de la lista y variables del reproductor
        Cancion cabeza = null;        // Primer nodo de la lista
        Cancion cancionActual = null; // Canción que está sonando
        int segundosRestantes = 0;    // Tiempo que le queda a la canción actual

        public SoundCloud()
        {
            InitializeComponent();
        }

        // =========================================================================
        // 2. FUNCIONES RECURSIVAS (LISTAR Y CONTAR)
        // =========================================================================

        // Función recursiva para listar las canciones en el ListBox
        private void ListarRecursivo(Cancion nodo, int indice)
        {
            // Caso base: llegamos al final de la lista
            if (nodo == null) return;

            // Agregamos la canción actual al ListBox
            lstCanciones.Items.Add($"{indice}. {nodo.ToString()}");

            // Llamada recursiva con el siguiente nodo
            ListarRecursivo(nodo.Siguiente, indice + 1);
        }

        // Función recursiva para contar la cantidad total de canciones
        private int ContarRecursivo(Cancion nodo)
        {
            // Caso base
            if (nodo == null) return 0;

            // Caso recursivo: 1 + el resto de la lista
            return 1 + ContarRecursivo(nodo.Siguiente);
        }

        // Procedimiento para actualizar la lista y el label lblTotalCanciones
        private void ActualizarInterfaz()
        {
            lstCanciones.Items.Clear();
            ListarRecursivo(cabeza, 1);

            int total = ContarRecursivo(cabeza);
            lblTotalCanciones.Text = $"Total de canciones: {total}";
        }

        // =========================================================================
        // 3. BOTONES DE GESTIÓN DE LA LISTA
        // =========================================================================

        private bool ValidarEntradas(out string nombre, out int duracion)
        {
            nombre = txtNombre.Text.Trim();
            if (string.IsNullOrEmpty(nombre) || !int.TryParse(txtDuracion.Text, out duracion) || duracion <= 0)
            {
                MessageBox.Show("Ingrese un nombre válido y una duración en segundos mayor a 0.", "Datos inválidos");
                duracion = 0;
                return false;
            }
            return true;
        }

        private void LimpiarCampos()
        {
            txtNombre.Clear();
            txtDuracion.Clear();
            txtNombre.Focus();
        }

        // Agregar canción al principio
        private void btnAgregarInicio_Click(object sender, EventArgs e)
        {
            if (!ValidarEntradas(out string nombre, out int duracion)) return;

            Cancion nueva = new Cancion(nombre, duracion);
            nueva.Siguiente = cabeza;
            cabeza = nueva;

            LimpiarCampos();
            ActualizarInterfaz();
        }

        // Agregar canción al final
        private void btnAgregarFinal_Click(object sender, EventArgs e)
        {
            if (!ValidarEntradas(out string nombre, out int duracion)) return;

            Cancion nueva = new Cancion(nombre, duracion);

            if (cabeza == null)
            {
                cabeza = nueva;
            }
            else
            {
                Cancion aux = cabeza;
                while (aux.Siguiente != null)
                {
                    aux = aux.Siguiente;
                }
                aux.Siguiente = nueva;
            }

            LimpiarCampos();
            ActualizarInterfaz();
        }

        // PUNTO II: Agregar una canción en la posición "n" (tu botón btnPos)
        private void btnPos_Click(object sender, EventArgs e)
        {
            if (!ValidarEntradas(out string nombre, out int duracion)) return;

            if (!int.TryParse(txtPosicion.Text, out int pos) || pos < 1)
            {
                MessageBox.Show("Ingrese una posición 'n' válida (1 o mayor).", "Posición inválida");
                return;
            }

            int total = ContarRecursivo(cabeza);
            if (pos > total + 1)
            {
                MessageBox.Show($"La posición máxima permitida actualmente es {total + 1}.", "Fuera de rango");
                return;
            }

            Cancion nueva = new Cancion(nombre, duracion);

            if (pos == 1)
            {
                nueva.Siguiente = cabeza;
                cabeza = nueva;
            }
            else
            {
                Cancion aux = cabeza;
                for (int i = 1; i < pos - 1; i++)
                {
                    aux = aux.Siguiente;
                }
                nueva.Siguiente = aux.Siguiente;
                aux.Siguiente = nueva;
            }

            LimpiarCampos();
            txtPosicion.Clear();
            ActualizarInterfaz();
        }

        // Eliminar canción seleccionada
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            int indiceSeleccionado = lstCanciones.SelectedIndex;
            if (indiceSeleccionado == -1)
            {
                MessageBox.Show("Seleccione una canción de la lista para eliminar.", "Aviso");
                return;
            }

            tmrReproductor.Stop();
            cancionActual = null;
            lblReproduciendo.Text = "Reproducción detenida";

            if (indiceSeleccionado == 0)
            {
                cabeza = cabeza.Siguiente;
            }
            else
            {
                Cancion aux = cabeza;
                for (int i = 0; i < indiceSeleccionado - 1; i++)
                {
                    aux = aux.Siguiente;
                }
                aux.Siguiente = aux.Siguiente.Siguiente;
            }

            ActualizarInterfaz();
        }

        // PUNTO I: Alterar el orden de cualquier posición con el de su derecha
        private void btnMoverDerecha_Click(object sender, EventArgs e)
        {
            int idx = lstCanciones.SelectedIndex;
            int total = ContarRecursivo(cabeza);

            if (idx == -1)
            {
                MessageBox.Show("Seleccione una canción de la lista para intercambiar con la siguiente.", "Aviso");
                return;
            }

            if (idx >= total - 1)
            {
                MessageBox.Show("La última canción no tiene otra a su derecha para intercambiar.", "Aviso");
                return;
            }

            Cancion actual = cabeza;
            for (int i = 0; i < idx; i++)
            {
                actual = actual.Siguiente;
            }

            Cancion derecha = actual.Siguiente;

            string tempNombre = actual.Nombre;
            int tempDuracion = actual.Duracion;

            actual.Nombre = derecha.Nombre;
            actual.Duracion = derecha.Duracion;

            derecha.Nombre = tempNombre;
            derecha.Duracion = tempDuracion;

            ActualizarInterfaz();
            lstCanciones.SelectedIndex = idx + 1;
        }

        // =========================================================================
        // 4. REPRODUCCIÓN AUTOMÁTICA Y EN BUCLE (PLAY, STOP Y TIMER)
        // =========================================================================

        private void btnPlay_Click(object sender, EventArgs e)
        {
            if (cabeza == null)
            {
                MessageBox.Show("La lista de reproducción está vacía.", "Sin canciones");
                return;
            }

            if (cancionActual == null)
            {
                cancionActual = cabeza;
                segundosRestantes = cancionActual.Duracion;
            }

            lblReproduciendo.Text = $"Reproduciendo: {cancionActual.Nombre} ({segundosRestantes}s)";
            tmrReproductor.Start();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            tmrReproductor.Stop();
            cancionActual = null;
            lblReproduciendo.Text = "Reproductor detenido";
        }

        // Evento del Timer: se ejecuta cada 1 segundo
        private void tmrReproductor_Tick(object sender, EventArgs e)
        {
            segundosRestantes--;

            if (segundosRestantes > 0)
            {
                lblReproduciendo.Text = $"Reproduciendo: {cancionActual.Nombre} ({segundosRestantes}s)";
            }
            else
            {
                // Pasamos a la siguiente canción, o volvemos al principio si era la última
                if (cancionActual.Siguiente != null)
                {
                    cancionActual = cancionActual.Siguiente;
                }
                else
                {
                    cancionActual = cabeza;
                }

                segundosRestantes = cancionActual.Duracion;
                lblReproduciendo.Text = $"Reproduciendo: {cancionActual.Nombre} ({segundosRestantes}s)";
            }
        }

        // Eventos TextChanged vacíos que ya tenías creados (se dejan para que no dé error el diseñador)
        private void txtNombre_TextChanged(object sender, EventArgs e) { }
        private void txtPosicion_TextChanged(object sender, EventArgs e) { }
        private void txtDuracion_TextChanged(object sender, EventArgs e) { }
    }
}