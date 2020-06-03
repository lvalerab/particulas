using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;
using System.Xml;


namespace Particulas2Dc
{
    public partial class Form1 : Form
    {
        private Device _pantalla;
        private PresentParameters _parametros;
        private bool _salirBucle = false;
        private double _delta = 1;
        private double[] _campmag = new double[] { 0, 0, 0 };
        private string _rutapart = @"c:\particulas\";
        private string _rutaenvi = @"c:\particulas\enviroment.env";

        private void InicializarGraph()
        {
            this._parametros = new PresentParameters();
            this._parametros.SwapEffect = SwapEffect.Copy;
            this._parametros.BackBufferWidth = this.Width;
            this._parametros.BackBufferHeight = this.Height;
             this._parametros.Windowed =true;

            this._pantalla = new Microsoft.DirectX.Direct3D.Device(0,DeviceType.Hardware,this.Handle,CreateFlags.SoftwareVertexProcessing,this._parametros);

            //Habilitar transparencias
            this._pantalla.RenderState.AlphaBlendEnable = true;
            this._pantalla.RenderState.SourceBlend = Blend.SourceAlpha;
            this._pantalla.RenderState.DestinationBlend = Blend.InvSourceAlpha;            
        }

        private double[] CoorPan(double[] c)
        {
            double[] r = new double[c.Length];
            r[0] = c[0] + this.Width / 2;
            r[1] = c[1] + this.Height / 2;
            return r;
        }

        private void CargaEntorno(string ruta)
        {
            if (!System.IO.File.Exists(ruta))
            {
                XmlWriter xW = XmlWriter.Create(ruta);
                xW.WriteStartDocument(true);
                xW.WriteStartElement("MEDIO");
                xW.WriteStartElement("PASO_TIEMPO");
                xW.WriteString("1");
                xW.WriteEndElement();
                xW.WriteStartElement("CAMPO_MAGNETICO");
                xW.WriteString("0/0/0");
                xW.WriteEndElement();
                xW.WriteEndElement();
                xW.WriteEndDocument();
                xW.Flush();
                xW.Close();
                xW = null;
            }
            string aux = "";
            XmlReader xR = XmlReader.Create(ruta);
            xR.ReadStartElement("MEDIO");
            xR.ReadStartElement("PASO_TIEMPO");
            aux = xR.ReadString();
            this._delta = Convert.ToDouble(aux);
            xR.ReadEndElement();
            xR.ReadStartElement("CAMPO_MAGNETICO");
            aux = xR.ReadString();
            string[] aux2 = aux.Split(new char[] { '/' });
            this._campmag = new double[aux2.Length];
            for (int i = 0; i < this._campmag.Length; i++)
            {
                this._campmag[i]=Convert.ToDouble(aux2[i]);
            }
            xR.ReadEndElement();
            xR.ReadEndElement();
            xR.Close();
        }

        private void InicializaBucle(string RutaPart)
        {
            List<Particula> particulas=new List<Particula>();
            foreach (string f in System.IO.Directory.GetFiles(RutaPart, "*.xml"))
            {
                Particula aux = new Particula(f, new double[] { this.Width / 2, this.Height / 2 }, this._pantalla);
                particulas.Add(aux);
            }
            while (!this._salirBucle)
            {
                this._pantalla.BeginScene();
                this._pantalla.Clear(ClearFlags.Target, Color.Black, 0, 0);
                //hago la interacion entre particulas
                for (int i = 0; i < particulas.Count; i++)
                {
                    for (int j = 0; j < particulas.Count; j++)
                    {
                        if (particulas[i] != null)
                        {
                            if (i != j)
                            {
                                //Particula aux = particulas[i].Interactuar(particulas[j], this._delta);
                                Particula aux = Interaccion.Interacturar(particulas[i], particulas[j], this._campmag, this._delta);
                                if (aux != null)
                                {
                                    particulas[i].Dispose();
                                    particulas[i] = null;
                                    particulas[j].Dispose();
                                    particulas[j] = null;
                                    particulas.Add(aux);                                    
                                }
                            }
                        }
                    }
                }
                GC.Collect();
                for (int i = 0; i < particulas.Count; i++)
                {
                    if (particulas[i] != null)
                    {
                        particulas[i].Dibuja();
                    }
                }
                this._pantalla.EndScene();
                this._pantalla.Present();
                Application.DoEvents();
            }
            for (int i = 0; i < particulas.Count; i++)
            {
                if (particulas[i] != null)
                {
                    particulas[i].Dispose();
                    particulas[i] = null;
                }
            }
            if (this._pantalla != null)
            {
                this._pantalla.Dispose();
                this._pantalla = null;
                GC.Collect();
            }
        }

        public Form1()
        {
            InitializeComponent();
            this.InicializarGraph();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this._salirBucle = true;
            }
            else
            {
                switch (e.KeyCode)
                {
                    case Keys.Up:
                        this._delta += 0.05;
                        break;
                    case Keys.Down:
                        this._delta -= 0.05;
                        break;
                }
            }
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
//            this.InicializaBucle();
        }

        private void Form1_Leave(object sender, EventArgs e)
        {
            this._salirBucle = true;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            this._salirBucle = true;
        }

        private void BtnPlay_Click(object sender, EventArgs e)
        {
            this._salirBucle = false;
            this.InicializarGraph();
            this.CargaEntorno(this._rutaenvi);
            this.InicializaBucle(this._rutapart);
        }

        private void BtnStop_Click(object sender, EventArgs e)
        {
            this._salirBucle = true;
        }

        private void cBoton1_Click(object sender, EventArgs e)
        {
            this.FrmBuscaDir.SelectedPath=this._rutapart;
            this.FrmBuscaDir.ShowDialog();
            this._rutapart = this.FrmBuscaDir.SelectedPath;
        }

        private void cBoton2_Click(object sender, EventArgs e)
        {
            this.FrmBuscaFich.ShowDialog();            
        }

        private void FrmBuscaFich_FileOk(object sender, CancelEventArgs e)
        {
            this._rutaenvi = this.FrmBuscaFich.FileName;
        }
    }
}