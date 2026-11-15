// =====================================================================
//  TP 12 - Integración #1 - Ejercicio 2: Reproductor multimedia
//
//  Estructura de datos: LISTA SIMPLEMENTE ENLAZADA de canciones, porque
//  se agrega al principio, al final y en cualquier posición, y se elimina
//  e intercambia en cualquier lugar de la lista. La reproducción es
//  circular: al terminar la última canción vuelve a la primera.
//  Listar y contar se resuelven con funciones recursivas.
// =====================================================================
using System;
using System.Drawing;
using System.Windows.Forms;

namespace TP12_Reproductor
{
    // Datos de una canción
    public class Cancion
    {
        public string Nombre { get; set; }
        public int Duracion { get; set; }   // en segundos

        public Cancion(string nombre, int duracion)
        {
            Nombre = nombre;
            Duracion = duracion;
        }

        // Convierte segundos a formato m:ss
        public static string FormatoTiempo(int segundos)
        {
            return (segundos / 60) + ":" + (segundos % 60).ToString("00");
        }
    }

    // Nodo de la lista: guarda una canción y la referencia a la siguiente
    public class NodoCancion
    {
        public Cancion Dato { get; set; }
        public NodoCancion Siguiente { get; set; }

        public NodoCancion(Cancion dato)
        {
            Dato = dato;
            Siguiente = null;
        }
    }

    // Lista simplemente enlazada de canciones (posiciones desde 1)
    public class ListaCanciones
    {
        private NodoCancion primero;

        public ListaCanciones()
        {
            primero = null;
        }

        public NodoCancion Primero
        {
            get { return primero; }
        }

        public bool EstaVacia()
        {
            return primero == null;
        }

        // Agrega una canción al principio de la lista
        public void AgregarAlPrincipio(Cancion cancion)
        {
            NodoCancion nuevo = new NodoCancion(cancion);
            nuevo.Siguiente = primero;
            primero = nuevo;
        }

        // Agrega una canción al final, recorriendo hasta el último nodo
        public void AgregarAlFinal(Cancion cancion)
        {
            NodoCancion nuevo = new NodoCancion(cancion);

            if (EstaVacia())
            {
                primero = nuevo;
                return;
            }

            NodoCancion actual = primero;
            while (actual.Siguiente != null)
            {
                actual = actual.Siguiente;
            }
            actual.Siguiente = nuevo;
        }

        // Devuelve el nodo de la posición indicada, o null si no existe
        public NodoCancion ObtenerNodo(int pos)
        {
            if (pos < 1) return null;

            NodoCancion actual = primero;
            int i = 1;
            while (actual != null && i < pos)
            {
                actual = actual.Siguiente;
                i++;
            }
            return actual;
        }

        // Agrega una canción en la posición n (de 1 a cantidad + 1)
        public bool AgregarEnPosicion(Cancion cancion, int pos)
        {
            if (pos < 1 || pos > Contar() + 1)
            {
                return false;
            }

            if (pos == 1)
            {
                AgregarAlPrincipio(cancion);
                return true;
            }

            // Se inserta entre el nodo anterior y su siguiente
            NodoCancion anterior = ObtenerNodo(pos - 1);
            NodoCancion nuevo = new NodoCancion(cancion);
            nuevo.Siguiente = anterior.Siguiente;
            anterior.Siguiente = nuevo;
            return true;
        }

        // Elimina la canción de la posición indicada y devuelve su nodo
        public NodoCancion EliminarEnPosicion(int pos)
        {
            if (EstaVacia() || pos < 1)
            {
                return null;
            }

            if (pos == 1)
            {
                NodoCancion eliminadoPrimero = primero;
                primero = primero.Siguiente;
                return eliminadoPrimero;
            }

            NodoCancion anterior = ObtenerNodo(pos - 1);
            if (anterior == null || anterior.Siguiente == null)
            {
                return null;
            }

            // El anterior pasa a apuntar al siguiente del eliminado
            NodoCancion eliminado = anterior.Siguiente;
            anterior.Siguiente = eliminado.Siguiente;
            return eliminado;
        }

        // Intercambia la canción de la posición indicada con la de su derecha
        public bool IntercambiarConDerecha(int pos)
        {
            NodoCancion nodo = ObtenerNodo(pos);
            if (nodo == null || nodo.Siguiente == null)
            {
                return false;
            }

            // Se intercambian los datos de los dos nodos con un auxiliar
            Cancion aux = nodo.Dato;
            nodo.Dato = nodo.Siguiente.Dato;
            nodo.Siguiente.Dato = aux;
            return true;
        }

        // Cuenta las canciones (recursiva)
        public int Contar()
        {
            return ContarDesde(primero);
        }

        private int ContarDesde(NodoCancion nodo)
        {
            if (nodo == null)
            {
                return 0;                              // caso base: fin de la lista
            }
            return 1 + ContarDesde(nodo.Siguiente);    // esta canción + las restantes
        }

        // Lista las canciones en el ListBox (recursiva), marcando la que suena
        public void ListarEn(ListBox lista, NodoCancion sonando)
        {
            lista.Items.Clear();
            ListarDesde(primero, lista, 1, sonando);
        }

        private void ListarDesde(NodoCancion nodo, ListBox lista, int pos, NodoCancion sonando)
        {
            if (nodo == null)
            {
                return;                                // caso base: fin de la lista
            }

            string marca = (nodo == sonando) ? "▶ " : "    ";
            lista.Items.Add(marca + pos + ". " + nodo.Dato.Nombre +
                            " (" + Cancion.FormatoTiempo(nodo.Dato.Duracion) + ")");

            ListarDesde(nodo.Siguiente, lista, pos + 1, sonando);   // resto de la lista
        }
    }

    public partial class Form1 : Form
    {
        private ListaCanciones lista = new ListaCanciones();
        private NodoCancion sonando = null;              // canción actual (null = detenido)
        private int segundosRestantes = 0;               // tiempo que le falta a la canción actual
        private System.Windows.Forms.Timer timer;        // simula el paso del tiempo (1 segundo)

        private GroupBox grpAgregar, grpReproductor, grpLista;
        private Label lblNombre, lblDuracion, lblPosicion;
        private TextBox txtNombre;
        private NumericUpDown nudDuracion, nudPosicion;
        private Button btnAlFinal, btnAlPrincipio, btnEnPosicion;
        private Label lblReproduciendo;
        private Button btnPlay, btnPausa, btnDetener, btnSiguiente;
        private ListBox lstCanciones;
        private Button btnEliminar, btnIntercambiar;
        private Label lblTotalCanciones;

        public Form1()
        {
            InitializeComponent();
            CrearControles();

            // Canciones de ejemplo para probar
            lista.AgregarAlFinal(new Cancion("Intro", 5));
            lista.AgregarAlFinal(new Cancion("Tema A", 8));
            lista.AgregarAlFinal(new Cancion("Tema B", 6));

            ActualizarLista();
            ActualizarEstado();
        }

        // Crea los controles del formulario con su posición y tamaño
        private void CrearControles()
        {
            Text = "Reproductor multimedia";
            ClientSize = new Size(640, 360);

            // Recuadro para agregar canciones
            grpAgregar = new GroupBox { Left = 12, Top = 12, Width = 300, Height = 170, Text = "Agregar canción" };
            lblNombre = new Label { Left = 10, Top = 28, Width = 70, Text = "Nombre:" };
            txtNombre = new TextBox { Left = 85, Top = 25, Width = 200 };
            lblDuracion = new Label { Left = 10, Top = 60, Width = 75, Text = "Duración (s):" };
            nudDuracion = new NumericUpDown { Left = 85, Top = 57, Width = 80, Minimum = 1, Maximum = 3600, Value = 10 };
            btnAlFinal = new Button { Left = 10, Top = 90, Width = 135, Height = 26, Text = "Agregar al final" };
            btnAlPrincipio = new Button { Left = 150, Top = 90, Width = 135, Height = 26, Text = "Agregar al principio" };
            lblPosicion = new Label { Left = 10, Top = 133, Width = 70, Text = "Posición n:" };
            nudPosicion = new NumericUpDown { Left = 85, Top = 130, Width = 55, Minimum = 1, Maximum = 1000, Value = 1 };
            btnEnPosicion = new Button { Left = 150, Top = 127, Width = 135, Height = 26, Text = "Agregar en posición n" };
            grpAgregar.Controls.AddRange(new Control[] { lblNombre, txtNombre, lblDuracion, nudDuracion,
                                                         btnAlFinal, btnAlPrincipio, lblPosicion, nudPosicion, btnEnPosicion });

            // Recuadro con los controles de reproducción
            grpReproductor = new GroupBox { Left = 12, Top = 192, Width = 300, Height = 150, Text = "Reproductor" };
            lblReproduciendo = new Label { Left = 10, Top = 25, Width = 280, Height = 50 };
            btnPlay = new Button { Left = 10, Top = 90, Width = 65, Height = 30, Text = "Play" };
            btnPausa = new Button { Left = 80, Top = 90, Width = 65, Height = 30, Text = "Pausa" };
            btnDetener = new Button { Left = 150, Top = 90, Width = 65, Height = 30, Text = "Detener" };
            btnSiguiente = new Button { Left = 220, Top = 90, Width = 70, Height = 30, Text = "Siguiente" };
            grpReproductor.Controls.AddRange(new Control[] { lblReproduciendo, btnPlay, btnPausa, btnDetener, btnSiguiente });

            // Recuadro con la lista de reproducción
            grpLista = new GroupBox { Left = 325, Top = 12, Width = 300, Height = 330, Text = "Lista de reproducción" };
            lstCanciones = new ListBox { Left = 10, Top = 25, Width = 280, Height = 210 };
            btnEliminar = new Button { Left = 10, Top = 245, Width = 135, Height = 28, Text = "Eliminar" };
            btnIntercambiar = new Button { Left = 150, Top = 245, Width = 140, Height = 28, Text = "Mover a la derecha" };
            lblTotalCanciones = new Label { Left = 10, Top = 290, Width = 280, Text = "Total de canciones: 0" };
            grpLista.Controls.AddRange(new Control[] { lstCanciones, btnEliminar, btnIntercambiar, lblTotalCanciones });

            Controls.AddRange(new Control[] { grpAgregar, grpReproductor, grpLista });

            // Timer que se ejecuta cada 1 segundo
            timer = new System.Windows.Forms.Timer();
            timer.Interval = 1000;
            timer.Tick += Timer_Tick;

            // Eventos de los botones
            btnAlFinal.Click += BtnAlFinal_Click;
            btnAlPrincipio.Click += BtnAlPrincipio_Click;
            btnEnPosicion.Click += BtnEnPosicion_Click;
            btnPlay.Click += BtnPlay_Click;
            btnPausa.Click += BtnPausa_Click;
            btnDetener.Click += BtnDetener_Click;
            btnSiguiente.Click += BtnSiguiente_Click;
            btnEliminar.Click += BtnEliminar_Click;
            btnIntercambiar.Click += BtnIntercambiar_Click;
        }

        // Crea una canción con los datos ingresados (null si falta el nombre)
        private Cancion LeerCancionDelFormulario()
        {
            string nombre = txtNombre.Text.Trim();
            if (nombre == "")
            {
                MessageBox.Show("Ingrese el nombre de la canción.", "Falta el nombre",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNombre.Focus();
                return null;
            }
            int duracion = (int)nudDuracion.Value;
            return new Cancion(nombre, duracion);
        }

        // Agrega la canción al final de la lista
        private void BtnAlFinal_Click(object sender, EventArgs e)
        {
            Cancion nueva = LeerCancionDelFormulario();
            if (nueva == null) return;

            lista.AgregarAlFinal(nueva);
            txtNombre.Clear();
            ActualizarLista();
        }

        // Agrega la canción al principio de la lista
        private void BtnAlPrincipio_Click(object sender, EventArgs e)
        {
            Cancion nueva = LeerCancionDelFormulario();
            if (nueva == null) return;

            lista.AgregarAlPrincipio(nueva);
            txtNombre.Clear();
            ActualizarLista();
        }

        // Agrega la canción en la posición n indicada
        private void BtnEnPosicion_Click(object sender, EventArgs e)
        {
            Cancion nueva = LeerCancionDelFormulario();
            if (nueva == null) return;

            int pos = (int)nudPosicion.Value;
            if (!lista.AgregarEnPosicion(nueva, pos))
            {
                MessageBox.Show("Posición inválida. Debe estar entre 1 y " + (lista.Contar() + 1) + ".",
                                "Posición inválida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            txtNombre.Clear();
            ActualizarLista();
        }

        // Posición (desde 1) de la canción seleccionada; 0 si no hay selección
        private int PosicionSeleccionada()
        {
            return lstCanciones.SelectedIndex + 1;
        }

        // Elimina la canción seleccionada
        private void BtnEliminar_Click(object sender, EventArgs e)
        {
            int pos = PosicionSeleccionada();
            if (pos == 0)
            {
                MessageBox.Show("Seleccione la canción a eliminar.", "Sin selección",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            NodoCancion eliminado = lista.EliminarEnPosicion(pos);

            // Si se eliminó la canción que sonaba, pasa a la siguiente (o a la primera)
            if (eliminado != null && eliminado == sonando)
            {
                if (lista.EstaVacia())
                {
                    Detener();
                }
                else
                {
                    sonando = (eliminado.Siguiente != null) ? eliminado.Siguiente : lista.Primero;
                    segundosRestantes = sonando.Dato.Duracion;
                }
            }

            ActualizarLista();
            ActualizarEstado();
        }

        // Intercambia la canción seleccionada con la de su derecha
        private void BtnIntercambiar_Click(object sender, EventArgs e)
        {
            int pos = PosicionSeleccionada();
            if (pos == 0)
            {
                MessageBox.Show("Seleccione la canción a mover.", "Sin selección",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            NodoCancion izquierdo = lista.ObtenerNodo(pos);
            NodoCancion derecho = izquierdo.Siguiente;

            if (!lista.IntercambiarConDerecha(pos))
            {
                MessageBox.Show("La última canción no tiene otra a su derecha.", "No se puede mover",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // La canción que sonaba cambió de nodo: se actualiza la referencia
            if (sonando == izquierdo)
            {
                sonando = derecho;
            }
            else if (sonando == derecho)
            {
                sonando = izquierdo;
            }

            ActualizarLista();
            lstCanciones.SelectedIndex = pos;   // mantiene seleccionada la canción movida
            ActualizarEstado();
        }

        // Inicia la reproducción (o la reanuda si estaba en pausa)
        private void BtnPlay_Click(object sender, EventArgs e)
        {
            if (lista.EstaVacia())
            {
                MessageBox.Show("La lista está vacía. Agregue canciones.", "Sin canciones",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (sonando == null)
            {
                sonando = lista.Primero;
                segundosRestantes = sonando.Dato.Duracion;
            }

            timer.Start();
            ActualizarLista();
            ActualizarEstado();
        }

        // Pausa la reproducción sin perder la canción actual
        private void BtnPausa_Click(object sender, EventArgs e)
        {
            timer.Stop();
            ActualizarEstado();
        }

        // Detiene la reproducción
        private void BtnDetener_Click(object sender, EventArgs e)
        {
            Detener();
            ActualizarLista();
            ActualizarEstado();
        }

        // Pasa a la siguiente canción
        private void BtnSiguiente_Click(object sender, EventArgs e)
        {
            if (sonando == null) return;
            PasarALaSiguiente();
        }

        // Cada segundo descuenta tiempo; al terminar la canción pasa a la siguiente
        private void Timer_Tick(object sender, EventArgs e)
        {
            segundosRestantes--;

            if (segundosRestantes <= 0)
            {
                PasarALaSiguiente();
            }
            else
            {
                ActualizarEstado();
            }
        }

        // Avanza a la siguiente canción; después de la última vuelve a la primera
        private void PasarALaSiguiente()
        {
            if (lista.EstaVacia())
            {
                Detener();
            }
            else
            {
                sonando = (sonando != null && sonando.Siguiente != null) ? sonando.Siguiente : lista.Primero;
                segundosRestantes = sonando.Dato.Duracion;
            }
            ActualizarLista();
            ActualizarEstado();
        }

        // Frena el timer y deja el reproductor sin canción actual
        private void Detener()
        {
            timer.Stop();
            sonando = null;
            segundosRestantes = 0;
        }

        // Lista las canciones y muestra el total en lblTotalCanciones
        private void ActualizarLista()
        {
            int seleccion = lstCanciones.SelectedIndex;

            lista.ListarEn(lstCanciones, sonando);
            lblTotalCanciones.Text = "Total de canciones: " + lista.Contar();

            // Restaura la selección que se pierde al recargar el ListBox
            if (seleccion >= 0 && seleccion < lstCanciones.Items.Count)
            {
                lstCanciones.SelectedIndex = seleccion;
            }
        }

        // Muestra la canción actual y el tiempo restante
        private void ActualizarEstado()
        {
            if (sonando == null)
            {
                lblReproduciendo.Text = "Detenido";
            }
            else
            {
                string estado = timer.Enabled ? "Reproduciendo" : "En pausa";
                lblReproduciendo.Text = estado + ": " + sonando.Dato.Nombre +
                                        "\nFaltan " + Cancion.FormatoTiempo(segundosRestantes) +
                                        " de " + Cancion.FormatoTiempo(sonando.Dato.Duracion);
            }
        }
    }
}