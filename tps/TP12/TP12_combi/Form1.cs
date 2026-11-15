// =====================================================================
//  TP 12 - Integración #1 - Ejercicio 1: Servicio de Combis
//
//  1) Estructura de datos: COLA (FIFO). Los pasajeros suben a la combi
//     en el mismo orden en que se anotaron: se agrega por el final
//     (Anotar) y se saca por el frente (Subir a la combi).
//
//  2) Componentes: Form, PictureBox (foto de la combi), GroupBox
//     ("Terminal Obelisco"), Label ("Pasajero", "En Espera" y estado de
//     la combi), TextBox (nombre del pasajero), Button ("Anotar" y
//     "Subir a la combi!") y ListBox (lista "En Espera").
// =====================================================================
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace TP12_combi
{
    // Nodo de la cola: guarda un pasajero y la referencia al siguiente
    public class NodoPasajero
    {
        public string Nombre { get; set; }
        public NodoPasajero Siguiente { get; set; }

        public NodoPasajero(string nombre)
        {
            Nombre = nombre;
            Siguiente = null;
        }
    }

    // Cola de pasajeros implementada con nodos (frente = próximo en subir)
    public class ColaPasajeros
    {
        private NodoPasajero frente;
        private NodoPasajero fin;
        private int cantidad;

        public ColaPasajeros()
        {
            frente = null;
            fin = null;
            cantidad = 0;
        }

        public bool EstaVacia()
        {
            return frente == null;
        }

        public int Count()
        {
            return cantidad;
        }

        // Agrega un pasajero al final de la cola
        public void Encolar(string nombre)
        {
            NodoPasajero nuevo = new NodoPasajero(nombre);

            if (EstaVacia())
            {
                // Cola vacía: el nuevo es el primero y el último
                frente = nuevo;
                fin = nuevo;
            }
            else
            {
                // Se engancha detrás del último y pasa a ser el nuevo fin
                fin.Siguiente = nuevo;
                fin = nuevo;
            }
            cantidad++;
        }

        // Saca y devuelve el pasajero del frente
        public string Desencolar()
        {
            if (EstaVacia())
            {
                throw new InvalidOperationException("No hay pasajeros en espera.");
            }

            string nombre = frente.Nombre;
            frente = frente.Siguiente;

            // Si la cola quedó vacía, el fin también se anula
            if (frente == null)
            {
                fin = null;
            }

            cantidad--;
            return nombre;
        }

        // Recorre la cola del frente al fin y carga los nombres en el ListBox
        public void ListarEn(ListBox lista)
        {
            lista.Items.Clear();
            NodoPasajero actual = frente;
            int posicion = 1;

            while (actual != null)
            {
                lista.Items.Add(posicion + ". " + actual.Nombre);
                actual = actual.Siguiente;
                posicion++;
            }
        }
    }

    public partial class Form1 : Form
    {
        private const int CAPACIDAD = 19;                         // lugares de la combi
        private ColaPasajeros colaEspera = new ColaPasajeros();   // fila de la terminal
        private int pasajerosEnCombi = 0;                         // pasajeros ya subidos

        private PictureBox picCombi;
        private GroupBox grpTerminal;
        private Label lblPasajero;
        private TextBox txtPasajero;
        private Button btnAnotar;
        private Label lblEnEspera;
        private ListBox lstEnEspera;
        private Button btnSubir;
        private Label lblEstadoCombi;

        public Form1()
        {
            InitializeComponent();
            CrearControles();
            ActualizarPantalla();
        }

        // Crea los controles del formulario con su posición y tamaño
        private void CrearControles()
        {
            Text = "Servicio de Combis";
            ClientSize = new Size(680, 360);

            // Foto de la combi (se carga combi.png si está junto al ejecutable)
            picCombi = new PictureBox { Left = 20, Top = 50, Width = 310, Height = 240 };
            picCombi.SizeMode = PictureBoxSizeMode.Zoom;
            string rutaImagen = Path.Combine(Application.StartupPath, "combi.png");
            if (File.Exists(rutaImagen))
            {
                picCombi.Image = Image.FromFile(rutaImagen);
            }

            lblEstadoCombi = new Label { Left = 20, Top = 300, Width = 310, Height = 40 };

            grpTerminal = new GroupBox { Left = 345, Top = 20, Width = 315, Height = 320, Text = "Terminal Obelisco" };
            lblPasajero = new Label { Left = 15, Top = 32, Width = 60, Text = "Pasajero" };
            txtPasajero = new TextBox { Left = 80, Top = 29, Width = 215 };
            btnAnotar = new Button { Left = 205, Top = 60, Width = 90, Height = 25, Text = "Anotar" };
            lblEnEspera = new Label { Left = 15, Top = 85, Width = 100, Text = "En Espera" };
            lstEnEspera = new ListBox { Left = 15, Top = 108, Width = 280, Height = 160 };
            btnSubir = new Button { Left = 165, Top = 280, Width = 130, Height = 28, Text = "Subir a la combi!" };

            // Eventos de los botones
            btnAnotar.Click += BtnAnotar_Click;
            btnSubir.Click += BtnSubir_Click;
            AcceptButton = btnAnotar;   // Enter = Anotar

            grpTerminal.Controls.AddRange(new Control[] { lblPasajero, txtPasajero, btnAnotar, lblEnEspera, lstEnEspera, btnSubir });
            Controls.AddRange(new Control[] { picCombi, lblEstadoCombi, grpTerminal });
        }

        // ANOTAR: agrega el pasajero al final de la cola de espera
        private void BtnAnotar_Click(object sender, EventArgs e)
        {
            string nombre = txtPasajero.Text.Trim();

            // Validación: el nombre no puede estar vacío
            if (nombre == "")
            {
                MessageBox.Show("Ingrese el nombre del pasajero.", "Falta el nombre",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPasajero.Focus();
                return;
            }

            colaEspera.Encolar(nombre);

            txtPasajero.Clear();
            txtPasajero.Focus();
            ActualizarPantalla();
        }

        // SUBIR A LA COMBI: saca al primero de la cola y lo sube a la combi
        private void BtnSubir_Click(object sender, EventArgs e)
        {
            // Sin pasajeros en espera
            if (colaEspera.EstaVacia())
            {
                if (pasajerosEnCombi == 0)
                {
                    // Combi vacía: se dirige a Puerto Madero
                    MessageBox.Show("No hay pasajeros en espera y la combi está vacía.\n" +
                                    "La combi debe ir a la terminal de Puerto Madero.",
                                    "Sin pasajeros", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    // Combi con pasajeros: espera a que lleguen más
                    MessageBox.Show("No hay pasajeros en espera.\n" +
                                    "La combi se queda esperando más pasajeros (" +
                                    pasajerosEnCombi + "/" + CAPACIDAD + ").",
                                    "Esperando pasajeros", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                return;
            }

            // Combi llena: no se permite subir más pasajeros
            if (pasajerosEnCombi >= CAPACIDAD)
            {
                MessageBox.Show("La combi está llena. No pueden subir más pasajeros.",
                                "Combi llena", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Sube el primer pasajero de la cola
            string pasajero = colaEspera.Desencolar();
            pasajerosEnCombi++;
            ActualizarPantalla();

            // Se completaron los 19 lugares: la combi emprende viaje
            if (pasajerosEnCombi == CAPACIDAD)
            {
                MessageBox.Show("Subió " + pasajero + ".\n" +
                                "¡La combi está llena (" + CAPACIDAD + "/" + CAPACIDAD + ")! " +
                                "Puede emprender viaje.",
                                "Combi llena", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // La combi parte y llega una vacía para los que siguen esperando
                pasajerosEnCombi = 0;
                ActualizarPantalla();
            }
        }

        // Lista la cola en "En Espera" y muestra el estado de la combi
        private void ActualizarPantalla()
        {
            colaEspera.ListarEn(lstEnEspera);

            lblEstadoCombi.Text = "Pasajeros en la combi: " + pasajerosEnCombi + "/" + CAPACIDAD +
                                  "\nPasajeros en espera: " + colaEspera.Count();
        }
    }
}