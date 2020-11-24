
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace DeepSpace
{

	class Estrategia
	{

		public String Consulta1( ArbolGeneral<Planeta> arbol)
		{
			//instancio la clase cola
			Cola<ArbolGeneral<Planeta>> c = new Cola<ArbolGeneral<Planeta>>();

			//genero un objeto auxiliar para guardar lo que se desencola de cola
			ArbolGeneral<Planeta> arbolaux;


			//encolo el arbol
			c.encolar(arbol);
			//encolo el stop de niveles
			c.encolar(null);

			int distancia=0;

			//mientras la cola no esta vacia 
			while (!c.esVacia())
			{
				
				arbolaux = c.desencolar();


				if (arbolaux != null)
				{
					if (arbolaux.getDatoRaiz().EsPlanetaDeLaIA())
					{
						return "CONSULTA 1:"+"\n" + "La distancia de la raiz al planeta mas cercano de la IA es " + distancia;
					}
					else
					{
						foreach (var hijo in arbolaux.getHijos())
						{	
								c.encolar(hijo); 

						}
			
					}
			    }
				else
				{
					distancia++;
					c.encolar(null);
				}	
			}
			return "No se encontro ningun planeta" ;
		}


		public String Consulta2( ArbolGeneral<Planeta> arbol)
		{
			//instancio la clase cola
			Cola<ArbolGeneral<Planeta>> c = new Cola<ArbolGeneral<Planeta>>();

			//genero un objeto auxiliar para guardar lo que se desencola de cola
			ArbolGeneral<Planeta> arbolaux;

			//encolo el arbol
			c.encolar(arbol);
			//encolo el stop de niveles
			c.encolar(null);

			int contarPlanetas = 0;

			int nivel = 0;

			string consultaDePlanetas="";

			string consulta2 = "CONSULTA 2:" + "\n";

			while(!c.esVacia())
			{
				arbolaux = c.desencolar();

				if (arbolaux!=null)
				{
					if (arbolaux.getDatoRaiz().Poblacion()>10)
					{
						contarPlanetas++;
					}
					foreach( var hijo in arbolaux.getHijos())
					{
						
						c.encolar(hijo);
						
					}
				}				
				else
				{
					consultaDePlanetas += "La cant de planetas con poblacion mayor a 10 en el nivel " + nivel + " es: " + contarPlanetas + "\n";
					contarPlanetas = 0;
					nivel++;
					if (!c.esVacia())
					{
						c.encolar(null);
					}
				}
			}
			return consulta2 + consultaDePlanetas;
		}


		public String Consulta3( ArbolGeneral<Planeta> arbol)
		{
			//instancio la clase cola
			Cola<ArbolGeneral<Planeta>> c = new Cola<ArbolGeneral<Planeta>>();

			//genero un objeto auxiliar para guardar lo que se desencola de cola
			ArbolGeneral<Planeta> arbolaux;

			//encolo el arbol
			c.encolar(arbol);
			//encolo el stop de niveles
			c.encolar(null);

			int contarPlanetas = 0;

			int nivel = 0;

			int sumarPoblacion = 0;

			float promedioPorNivel=0;

			string consultaPromedioDePlanetas = "";

			string consulta3 = "CONSULTA 3:" + "\n";

			while (!c.esVacia())
			{
				arbolaux = c.desencolar();

				if (arbolaux != null)
				{
					if (arbolaux.getDatoRaiz().Poblacion() > 0)
					{
						contarPlanetas++;
						sumarPoblacion = sumarPoblacion+arbolaux.getDatoRaiz().Poblacion();
						promedioPorNivel = sumarPoblacion / contarPlanetas;
					}
					foreach (var hijo in arbolaux.getHijos())
					{

						c.encolar(hijo);

					}
				}
				else
				{
				
					consultaPromedioDePlanetas += "El promedio poblacional en el nivel " + nivel + " es: " + promedioPorNivel + "\n";
					contarPlanetas = 0;
					nivel++;
					promedioPorNivel = 0;
					sumarPoblacion = 0;
					if (!c.esVacia())
					{
						c.encolar(null);
					}
				}
			}
			return consulta3 +consultaPromedioDePlanetas;
		}

		public Movimiento CalcularMovimiento(ArbolGeneral<Planeta> arbol)
		{
		
			List<Planeta> caminoHaciaRaiz = null;
			List<Planeta> caminoHaciaHumano = null;

			caminoHaciaRaiz = CaminoaRaiz(arbol, new List<Planeta>());
			caminoHaciaRaiz.Reverse();
			//dar vuelta la lista TODO

			caminoHaciaHumano = CaminoRaizAHumano(arbol, new List<Planeta>());

			if (!arbol.getDatoRaiz().EsPlanetaDeLaIA())
			{
				Movimiento movARaiz = new Movimiento(caminoHaciaRaiz[0], caminoHaciaRaiz[1]);
				return movARaiz;
			}
			else
			{
				
				for(int index=0; index< caminoHaciaHumano.Count(); index++)
				{
					if (caminoHaciaHumano[index].EsPlanetaDeLaIA() && 
							(caminoHaciaHumano[index+1].EsPlanetaNeutral()|| 
								caminoHaciaHumano[index+1].EsPlanetaDelJugador()))
					{
						Movimiento movAhumano = new Movimiento(caminoHaciaHumano[index], caminoHaciaHumano[index+1]);
						return movAhumano;
					}
				}
	
			}
			return null;
		}

		public List<Planeta> CaminoaRaiz(ArbolGeneral<Planeta> arbol, List<Planeta> caminoDeLaIA)
		{
			
			//preorden 1ro raiz
			caminoDeLaIA.Add(arbol.getDatoRaiz());

			//si encontre camino
			if (arbol.getDatoRaiz().EsPlanetaDeLaIA())
			{
				return caminoDeLaIA;
			}
			else
			{
				//if(arbol.esHoja())
				//{
					foreach(var hijo in arbol.getHijos())
					{
					
						List<Planeta> caminoAux =CaminoaRaiz(hijo, caminoDeLaIA);
						if (caminoAux != null)
						{
							return caminoAux;
						}
					
					}
					//saco ultimo planeta
					caminoDeLaIA.RemoveAt(caminoDeLaIA.Count()-1);
				//}
			}
			return null;
		}

		public List<Planeta> CaminoRaizAHumano(ArbolGeneral<Planeta> arbol, List<Planeta> caminoDeRaizAHumano)
		{

			//preorden 1ro raiz
			caminoDeRaizAHumano.Add(arbol.getDatoRaiz());

			//si encontre camino
			if (arbol.getDatoRaiz().EsPlanetaDelJugador())
			{
				return caminoDeRaizAHumano;
			}
			else
			{
				
				foreach (var hijo in arbol.getHijos())
				{

					List<Planeta> caminoAux = CaminoRaizAHumano(hijo, caminoDeRaizAHumano);
					if (caminoAux != null)
					{
						return caminoAux;
					}

				}
				//saco ultimo planeta
				caminoDeRaizAHumano.RemoveAt(caminoDeRaizAHumano.Count() - 1);
				//}
			}
			return null;
		}
	}
}
