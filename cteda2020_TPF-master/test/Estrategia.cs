
using System;
using System.Collections.Generic;
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

			int distancia=1;

			//mientras la cola no esta vacia 
			while (!c.esVacia())
			{
				
				arbolaux = c.desencolar();
				if (arbolaux != null)
				{
					foreach (var hijo in arbolaux.getHijos())
					{
						if (!(hijo.getDatoRaiz()).EsPlanetaDeLaIA())
						{
							c.encolar(hijo); //
							c.encolar(null);
						}
						else
						{
							return "La distancia que existe entre la raiz y el planeta perteneciente al BOT es : " + distancia;
						}
					}
				}
				else
				{
					distancia++;
				}	
			}
			return "No se encontro ningun planeta";
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

			int nivel = 1;

			string cantDePlanetas="";

			while(!c.esVacia())
			{
				arbolaux = c.desencolar();

				if (arbolaux!=null)
				{
					foreach( var hijo in arbol.getHijos())
					{
						if (hijo.getDatoRaiz().Poblacion() > 10)
						{
							contarPlanetas++;
						}
					}
				}
				else
				{
					nivel++;
					cantDePlanetas = "la cant de planetas con poblacion mayor a 10 en el nivel " + nivel + "es: " + contarPlanetas ;
				}
				return cantDePlanetas+= cantDePlanetas;
			}

			return "la cant ";
		}


		public String Consulta3( ArbolGeneral<Planeta> arbol)
		{
			return "Implementar";
		}
		
		public Movimiento CalcularMovimiento(ArbolGeneral<Planeta> arbol)
		{
			//Implementar
			
			return null;
		}
	}
}
