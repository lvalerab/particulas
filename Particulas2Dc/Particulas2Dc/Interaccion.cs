using System;
using System.Collections.Generic;
using System.Text;

namespace Particulas2Dc
{
    public class Interaccion
    {
        private static double DistanciaCartesiana(Particula p1, Particula p2)
        {
            double aux = 0;
            for (int i = 0; i < p1.posicion.Length; i++)
            {
                aux += Math.Pow(p1.posicion[i] - p2.posicion[i], 2);
            }
            aux = Math.Sqrt(aux);
            return aux;
        }

        private static double[] VectorUnitario(Particula p1, Particula p2)
        {
            double[] aux = new double[p1.posicion.Length];
            double d=Interaccion.DistanciaCartesiana(p1,p2);
            for (int i = 0; i < aux.Length; i++)
            {
                aux[i] = (p1.posicion[i] - p2.posicion[i]) / d;
            }
            return aux;
        }

        public static double[] Gravitatoria(Particula p1, Particula p2)
        {
            double[] f = new double[p1.aceleracion.Length];
            double dist = Interaccion.DistanciaCartesiana(p1, p2);
            double[] u = Interaccion.VectorUnitario(p1,p2);
            for (int i = 0; i < f.Length; i++)
            {
                f[i] = -1 * (Particula.G * (p1.masa * p2.masa) / (dist*dist)) * u[i];
            }
            return f;
        }

        public static double[] Electroestatica(Particula p1, Particula p2)
        {
            double[] f = new double[p1.aceleracion.Length];
            double dist = Interaccion.DistanciaCartesiana(p1, p2);
            double[] u = Interaccion.VectorUnitario(p1, p2);
            for (int i = 0; i < f.Length; i++)
            {
                f[i] =  (Particula.K * (p1.carga * p2.carga) / (dist * dist)) * u[i];
            }
            return f;
        }

        public static double[] Magnetica(Particula p, double[] campo)
        {
            double[] v=new double[3] {p.velocidad[0],p.velocidad[1],(p.velocidad.Length<3?0:p.velocidad[2])};
            double[] f=new double[3] {p.carga*(v[1]*campo[2]-v[2]*campo[1]),p.carga*(v[2]*campo[0]-v[0]*campo[2]),p.carga*(v[0]*campo[1]-v[1]*campo[0])};
            v=null;
            return f;
        }

        private static void Int(ref Particula p1, Particula p2, double[] campo, double delta)
        {                    
            double[] g = (p2!=null?Interaccion.Gravitatoria(p1, p2):null);
            double[] e = (p2 != null ? Interaccion.Electroestatica(p1, p2) :null);
            double[] m = Interaccion.Magnetica(p1, campo);
            for (int i = 0; i < p1.aceleracion.Length; i++)
            {
                if (p2 != null)
                {
                    p1.aceleracion[i] = (g[i] + e[i] + m[i]) / p1.masa;
                }
                else
                {
                    p1.aceleracion[i] = m[i] / p1.masa;
                }
                p1.velocidad[i] = p1.velocidad[i] + p1.aceleracion[i] * delta;
                p1.posicion[i] = p1.posicion[i] + p1.velocidad[i] * delta;
            }            
            g = null;
            e = null;
            m = null;
            GC.Collect();
        }

        public static Particula Interacturar(Particula p1, Particula p2, double[] campo, double delta)
        {
            Particula p = null;
            if (p2!=null && p1.distancia(p2) < 10)
            {
                p = p1 + p2;
                p1 = null;
                p2 = null;
                Interaccion.Int(ref p, null, campo, delta);
            }
            else
            {
                Interaccion.Int(ref p1, p2, campo, delta);
                if(p2!=null) 
                    Interaccion.Int(ref p2, p1, campo, delta);
            }
            return p;
        }
    }
}
