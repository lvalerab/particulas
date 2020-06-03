using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;
using System.Xml;
namespace Particulas2Dc
{
    public class Particula:IDisposable
    {
        public const double G = 6.67384e-11;
        public const double K = 8.99e9;
        private double _masa;
        private double _carga;
        private double[] _posicion;
        private double[] _velocidad;
        private double[] _aceleracion;
        private string _fText;
        private Device _dv;
        private Sprite _sp;
        private Microsoft.DirectX.Direct3D.Font _ft;
        private string _nombre;

        public string nombre
        {
            get
            {
                return this._nombre;
            }
            set
            {
                this._nombre = value;
            }
        }

        public string textura
        {
            get
            {
                return this._fText;
            }
            set
            {
                this._fText = value;
            }
        }

        public double masa
        {
            set
            {
                this._masa = value;
            }
            get
            {
                return this._masa;
            }
        }
        public double carga
        {
            set
            {
                this._carga = value;
            }
            get
            {
                return this._carga;
            }
        }
        public double[] posicion
        {
            set
            {
                this._posicion = value;
            }
            get
            {
                return this._posicion;
            }
        }
        public double[] velocidad
        {
            set
            {
                this._velocidad = value;
            }
            get
            {
                return this._velocidad;
            }
        }
        public double[] aceleracion
        {
            set
            {
                this._aceleracion = value;
            }
            get
            {
                return this._aceleracion;
            }
        }
        public double[] cmovimiento
        {
            get
            {
                double[] devo = new double[this.velocidad.Length];
                for (int i = 0; i < this.velocidad.Length; i++)
                {
                    devo[i] = this.masa * this.velocidad[i];
                }
                return devo;
            }
        }

        public Particula(double m, double c, double[] p, double[] v, double[] a, Device d)
        {
            this._nombre = "";
            this._masa = m;
            this._carga = c;
            this._posicion = p;
            this._velocidad = v;
            this._aceleracion = a;
            //this.textura = AppDomain.CurrentDomain.BaseDirectory + @"\particula.png";
            this.textura = @"c:\PIELES\particula.png";
            this._dv = d;            
        }

        public Particula(string fich, double[] bcord, Device d)
        {
            if (!System.IO.File.Exists(fich))
            {
                //Creamos un fichero de ejemplo
                XmlWriter xW = XmlWriter.Create(fich);
                xW.WriteStartDocument(true);
                xW.WriteStartElement("PARTICULA");
                xW.WriteStartElement("NOMBRE");
                xW.WriteString(@"PARTICULA");
                xW.WriteEndElement();
                xW.WriteStartElement("IMAGEN");
                xW.WriteString(@"c:\PIELES\particula.png");
                xW.WriteEndElement();
                xW.WriteStartElement("MASA");
                xW.WriteString("1");
                xW.WriteEndElement();
                xW.WriteStartElement("CARGA");
                xW.WriteString("1");
                xW.WriteEndElement();
                xW.WriteStartElement("POSICION");
                xW.WriteString("0/0");
                xW.WriteEndElement();
                xW.WriteStartElement("VELOCIDAD");
                xW.WriteString("0/0");
                xW.WriteEndElement();
                xW.WriteStartElement("ACELERACION");
                xW.WriteString("0/0");
                xW.WriteEndElement();
                xW.WriteEndElement();
                xW.WriteEndDocument();                
                xW.Flush();
                xW.Close();
            }
            XmlReader xR = XmlReader.Create(fich);
            string aux = "";
            string n = "";
            string[] sp;
            string f = "";
            double m = 0;
            double c = 0;
            double[] p = new double[] { 0, 0 };
            double[] v = new double[] { 0, 0 };
            double[] a = new double[] { 0, 0 };
            xR.ReadStartElement("PARTICULA");
            xR.ReadStartElement("NOMBRE");
            n = xR.ReadString();
            xR.ReadEndElement();
            xR.ReadStartElement("IMAGEN");
            f = xR.ReadString();
            xR.ReadEndElement();
            xR.ReadStartElement("MASA");
            aux = xR.ReadString();
            m = Convert.ToDouble(aux);
            xR.ReadEndElement();
            xR.ReadStartElement("CARGA");
            aux = xR.ReadString();
            c = Convert.ToDouble(aux);
            xR.ReadEndElement();
            xR.ReadStartElement("POSICION");
            aux = xR.ReadString();
            sp=aux.Split(new char[] {'/'});
            p[0] = Convert.ToDouble(sp[0]) + bcord[0];
            p[1] = Convert.ToDouble(sp[1]) + bcord[1];
            xR.ReadEndElement();
            xR.ReadStartElement("VELOCIDAD");
            aux = xR.ReadString();
            sp = aux.Split(new char[] { '/' });
            v[0] = Convert.ToDouble(sp[0]);
            v[1] = Convert.ToDouble(sp[1]);
            xR.ReadEndElement();
            xR.ReadStartElement("ACELERACION");
            aux = xR.ReadString();
            sp = aux.Split(new char[] { '/' });
            a[0] = Convert.ToDouble(sp[0]);
            a[1] = Convert.ToDouble(sp[1]);
            xR.ReadEndElement();
            xR.ReadEndElement();
            xR.Close();
            this._nombre = n;
            this._masa = m;
            this._carga = c;
            this._posicion = p;
            this._velocidad = v;
            this._aceleracion = a;
            //this.textura = AppDomain.CurrentDomain.BaseDirectory + @"\particula.png";
            this.textura = f;
            this._dv = d;
        }

        ~Particula()
        {
            //this.Dispose();
        }

        public double distancia(Particula p)
        {
            double dist = 0;
            for (int i = 0; i < this.posicion.Length; i++)
            {
                dist += Math.Pow(this.posicion[i] - p.posicion[i], 2);
            }
            dist = Math.Sqrt(dist);
            return dist;
        }

        public double[] VUnitario(Particula p)
        {
            double dist = this.distancia(p);
            double[] u = new double[this.posicion.Length];
            for (int i = 0; i < u.Length; i++)
            {
                u[i] = (this.posicion[i] - p.posicion[i]) / dist;
            }
            return u;
        }

        public double[] IntGravitacion(Particula p)
        {
            double[] f = new double[this.aceleracion.Length];
            double dist=this.distancia(p);
            double[] u=this.VUnitario(p);
            for (int i = 0; i < f.Length; i++)
            {
                f[i] = -1*(G * (this.masa * p.masa) / (dist*dist)) * u[i];                
            }
            u = null;
            return f;
        }

        public double[] IntElectronica(Particula p)
        {
            double[] f = new double[this.aceleracion.Length];
            double dist = this.distancia(p);
            double[] u = this.VUnitario(p);
            for (int i = 0; i < f.Length; i++)
            {
                f[i] = (K* (this.carga * p.carga) / (dist*dist)) * u[i];
            }
            u = null;
            return f;
        }

        public Device dev
        {
            get
            {
                return this._dv;
            }
        }

        public Particula Interactuar(Particula p, double delta)
        {
            Particula resultante = null;
            
                if (p != null)
                {
                    if (this.distancia(p) < 10)
                    {
                        resultante = this + p;
                    }
                    else
                    {
                        double[] ftot = new double[p.aceleracion.Length];
                        double[] fg = this.IntGravitacion(p);
                        double[] fe = this.IntElectronica(p);
                        for (int i = 0; i < ftot.Length; i++)
                        {
                            ftot[i] = fg[i] + fe[i];
                            this.aceleracion[i] = ftot[i] / this.masa;
                        }
                        ftot =null;
                        fg =null;
                        fe = null;
                        //Velocidad
                        for (int i = 0; i < this.aceleracion.Length; i++)
                        {
                            this.velocidad[i] = this.velocidad[i] + this.aceleracion[i] * delta;
                        }
                        //Posicion
                        for (int i = 0; i < this.velocidad.Length; i++)
                        {
                            this.posicion[i] = this.posicion[i] + this.velocidad[i] * delta;
                        }
                     }
                }
                else
                {
                    for (int i = 0; i < this.aceleracion.Length; i++)
                    {
                        this.velocidad[i] = this.velocidad[i] + this.aceleracion[i] * delta;
                    }
                    //Posicion
                    for (int i = 0; i < this.velocidad.Length; i++)
                    {
                        this.posicion[i] = this.posicion[i] + this.velocidad[i] * delta;
                    }
                }
                return resultante;
        }

        public static Particula operator +(Particula a, Particula b)
        {
            //Suponemos que es un choque inelastico, y que no hay transformacion de masa en energia
            double[] p = new double[a.posicion.Length];
            for (int i = 0; i < p.Length; i++)
            {
                p[i] = (a.posicion[i] + b.posicion[i]) / 2;
            }

            double[] velocidad=new double[a.velocidad.Length];
            for(int i=0;i<velocidad.Length;i++) {
                velocidad[i]=(a.cmovimiento[i]+b.cmovimiento[i])/(a.masa+b.masa);
            }
            double[] aceleracion = new double[a.aceleracion.Length];
            for (int i = 0; i < aceleracion.Length; i++)
            {
                //aceleracion[i] = ((a.masa * a.aceleracion[i]) + (b.masa * b.aceleracion[i])) / (a.masa + b.masa);
                aceleracion[i] = 0;
            }
            Particula res = new Particula(a.masa + b.masa, a.carga + b.carga, p, velocidad, aceleracion,a.dev);
            res.nombre = a.nombre + " + " + b.nombre;
            p = null;
            velocidad = null;
            aceleracion = null;            
            return res;
        }

        public void Dibuja()
        {
            this._sp = new Sprite(this._dv);
            this._ft = new Microsoft.DirectX.Direct3D.Font(this._dv, new System.Drawing.Font(FontFamily.GenericSerif, 10));
            this._sp.Begin(SpriteFlags.None);
            this._sp.Draw2D(TextureLoader.FromFile(this._sp.Device, this.textura), new Point(0, 0), 0, new Point((int)this.posicion[0], (int)this.posicion[1]), Color.White);
            string cadena = this.nombre + "( M: " + this.masa.ToString("e3") + ") (Q: " + this.carga.ToString("e3") + ")\n";
            cadena+= "P: " + this.posicion[0].ToString("e3") + "," + this.posicion[1].ToString("e3") + '\n';
            cadena += "V: " + this.velocidad[0].ToString("e3") + "," + this.velocidad[1].ToString("e3");
            this._ft.DrawText(this._sp, cadena, new Point((int)this.posicion[0] + 10, (int)this.posicion[1] + 10), Color.Red);
            this._sp.Flush();
            this._sp.End();
            this._sp.Dispose();
            this._ft.Dispose();
            this._ft = null;
            this._sp = null;
            GC.Collect();
        }

        #region Miembros de IDisposable

        public void Dispose()
        {
            //this._sp.Dispose();
            this._masa = 0;
            this._carga = 0;
            this._posicion = null;
            this._velocidad = null;
            this._aceleracion = null;
            this._fText = null;
            this._sp = null;
            GC.Collect();
        }

        #endregion
    }
}
