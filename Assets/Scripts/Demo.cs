using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine.UI;



public class Demo : MonoBehaviour {

	public Transform gestureOnScreenPrefab;


	private int strokeId = -1;

	private Vector3 virtualKeyPosition = Vector2.zero;
	private RuntimePlatform platform;
	private int vertexCount = 0;

	public List<LineRenderer> gestureLinesRenderer = new List<LineRenderer>();
	public LineRenderer currentGestureLineRenderer;

	//GUI
	private string message;
	private bool recognized;
	private string newGestureName = "";

	public Text textDebug;

	void Start () 
	{

		this.platform = Application.platform;
		// drawArea = new Rect(0, 0, Screen.width - Screen.width / 3, Screen.height);

		//Load pre-made gestures
		// TextAsset[] gesturesXml = Resources.LoadAll<TextAsset>("GestureSet/10-stylus-MEDIUM/");
		// foreach (TextAsset gestureXml in gesturesXml)
		// 	trainingSet.Add(GestureIO.ReadGestureFromXML(gestureXml.text));

		//Load user custom gestures
		// string[] filePaths = Directory.GetFiles(Application.persistentDataPath, "*.xml");
		// foreach (string filePath in filePaths)
		// 	trainingSet.Add(GestureIO.ReadGestureFromFile(filePath));
	}

	void Update () {

		if (platform == RuntimePlatform.Android || platform == RuntimePlatform.IPhonePlayer) 
		{
			if (Input.touchCount > 0) 
			{
				virtualKeyPosition = new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y);
			}
		} 
		//For Desktop
		else 
		{
			if (Input.GetMouseButton(0)) 
			{
				virtualKeyPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y);
			}
		}

		

		if (Input.GetMouseButtonDown(0)) 
		{

			if (recognized) 
			{

				recognized = false;
				strokeId = -1;

			

				foreach (LineRenderer lineRenderer in gestureLinesRenderer)
				{
					lineRenderer.SetVertexCount(0);
					Destroy(lineRenderer.gameObject);
				}

				gestureLinesRenderer.Clear();
			}

			++strokeId;
			
			Transform tmpGesture = Instantiate(gestureOnScreenPrefab, transform.position, transform.rotation) as Transform;
			currentGestureLineRenderer = tmpGesture.GetComponent<LineRenderer>();
			
			gestureLinesRenderer.Add(currentGestureLineRenderer);
			
			vertexCount = 0;
		}
		
		if (Input.GetMouseButton(0)) {
			
			currentGestureLineRenderer.SetVertexCount(++vertexCount);
			currentGestureLineRenderer.SetPosition(vertexCount - 1, Camera.main.ScreenToWorldPoint(new Vector3(virtualKeyPosition.x, virtualKeyPosition.y, 10)));
		}
		
	}

	public void recognizeGesture()
	{
		bool isTick = checkTick();
		if(isTick)
			textDebug.text = "Es un tick ossas";

		else	
			textDebug.text = "Eso no es un tick sablaza";
				}

	public bool checkTick()
	{
		float positionX1=0;
		float positionX2=0;
		int direcciónInicialCuadrante = 0;
		int changeCount = 0;
		bool pendiente= false;
		bool primerPuntoInterseccionEncontrado= false;
		int primerPuntoInterseccion = 0;

		for(int i = 0 ; i < gestureLinesRenderer[0].positionCount-1;i++)
		{
			positionX1 = gestureLinesRenderer[0].GetPosition(i).x;
			positionX2 = gestureLinesRenderer[0].GetPosition(i+1).x;
			if(positionX1 != positionX2)
			{
				break;
			}
		}

		//<First Barrier> Analizando cambios de direccion en y
		
		float positionY1 = gestureLinesRenderer[0].GetPosition(0).y;
		float positionY2 = gestureLinesRenderer[0].GetPosition(4).y;
		

		if(positionY1 < positionY2)
		{	
			
			return false;
		}

		else
		{
			print("Pasó la primera barrera");
		}

		//</First Barrier>

		//<Second Barrier> Analizando cambios de dirección en y

		for(int i = 0; i< gestureLinesRenderer[0].positionCount-1; i++)
		{
			positionY1 = gestureLinesRenderer[0].GetPosition(i).y;
			positionY2 = gestureLinesRenderer[0].GetPosition(i+1).y;
			if(positionY2>positionY1 && !pendiente)
			{
				changeCount++;
				pendiente = true;
				if(!primerPuntoInterseccionEncontrado)
				{
					primerPuntoInterseccionEncontrado = true;
					primerPuntoInterseccion = i+1;
					
				}
			}

			else if(positionY1>positionY2 && pendiente)
			{
				changeCount++;
				pendiente = false;
				if(!primerPuntoInterseccionEncontrado)
				{
					primerPuntoInterseccionEncontrado = true;
					primerPuntoInterseccion = i+1;
					
				}
			}
		}

		//<Second Barrier> Analizando cambios de dirección en x
		//hacia el tercer cuadrante
		if(positionX1< positionX2)
		{
			for(int i = 0; i< gestureLinesRenderer[0].positionCount-1; i++)
			{
				float positionX1Aux = gestureLinesRenderer[0].GetPosition(i).x;
				float positionX2Aux = gestureLinesRenderer[0].GetPosition(i+1).x;
				if(positionX2Aux<positionX1Aux)
				{
					return false;
					// if(!primerPuntoInterseccionEncontrado)
					// {
					// 	primerPuntoInterseccionEncontrado = true;
					// 	primerPuntoInterseccion = i+1;
					// 	if(gestureLinesRenderer[0].GetPosition(i).x  < gestureLinesRenderer[0].GetPosition(gestureLinesRenderer[0].positionCount).x)
					// 	{
					// 		print("Se retorna falso porque la bifurcacion se hizo para la izquierda y debia ser para la derecha");
					// 		return false;
					// 	}
					// }
				}

				// else if(positionX2>positionX1 && pendiente)
				// {
				// 	changeCount++;
				// 	pendiente = false;
				// 	if(!primerPuntoInterseccionEncontrado)
				// 	{
				// 		primerPuntoInterseccionEncontrado = true;
				// 		primerPuntoInterseccion = i+1;
				// 		if(gestureLinesRenderer[0].GetPosition(i).x  > gestureLinesRenderer[0].GetPosition(gestureLinesRenderer[0].positionCount).x)
				// 		{
				// 			print("Se retorna falso porque la bifurcacion se hizo para la derecha y debia ser para la derecha");
				// 			return false;
				// 		}
						
				// 	}
				// }
			}
		}

		
		
		if(positionX1 > positionX2)
		{
			for(int i = 0; i< gestureLinesRenderer[0].positionCount-1; i++)
			{
				float positionX1Aux = gestureLinesRenderer[0].GetPosition(i).x;
				float positionX2Aux = gestureLinesRenderer[0].GetPosition(i+1).x;
				if(positionX1Aux < positionX2Aux)
				{
					return false;
					// if(!primerPuntoInterseccionEncontrado)
					// {
					// 	primerPuntoInterseccionEncontrado = true;
					// 	primerPuntoInterseccion = i+1;
					// 	if(gestureLinesRenderer[0].GetPosition(i).x  < gestureLinesRenderer[0].GetPosition(gestureLinesRenderer[0].positionCount).x)
					// 	{
					// 		print("Se retorna falso porque la bifurcacion se hizo para la izquierda y debia ser para la derecha");
					// 		return false;
					// 	}
					// }
				}

				// else if(positionX2>positionX1 && pendiente)
				// {
				// 	changeCount++;
				// 	pendiente = false;
				// 	if(!primerPuntoInterseccionEncontrado)
				// 	{
				// 		primerPuntoInterseccionEncontrado = true;
				// 		primerPuntoInterseccion = i+1;
				// 		if(gestureLinesRenderer[0].GetPosition(i).x  > gestureLinesRenderer[0].GetPosition(gestureLinesRenderer[0].positionCount).x)
				// 		{
				// 			print("Se retorna falso porque la bifurcacion se hizo para la derecha y debia ser para la derecha");
				// 			return false;
				// 		}
						
				// 	}
				// }
			}
		}

		//</second>

		print("El primer Punto encontrado que provoca un cambio en la interseccion es el punto: ");
		print("x: " + gestureLinesRenderer[0].GetPosition(primerPuntoInterseccion).x);
		print("y: " + gestureLinesRenderer[0].GetPosition(primerPuntoInterseccion).y);
		print("El numero de intersecciones encontradas es: " + changeCount);

		if(changeCount>1)
		{
			
			return false;
		}

		else
		{
			print("Pasó la segunda barrera");
		}

		//</Second Barrier>

		//<Third Barrier> Analizando tamaños de los dos trazos

		

		

		
		float primeraDistancia;
		float segundaDistancia;
		
	

		if(positionX1 > positionX2)
		{
			// direcciónInicialCuadrante = 3;
			primeraDistancia = Math.Abs(Vector3.Distance(gestureLinesRenderer[0].GetPosition(0), gestureLinesRenderer[0].GetPosition(primerPuntoInterseccion-1)));
			segundaDistancia = Math.Abs(Vector3.Distance(gestureLinesRenderer[0].GetPosition(primerPuntoInterseccion), gestureLinesRenderer[0].GetPosition(gestureLinesRenderer[0].positionCount-1)));

			print("primeraDistancia: "+ primeraDistancia);
			print("Distancia menor: " + segundaDistancia);
			print("Posicion X1: " +positionX1);
			print("Posicion X2: " +positionX2);
			
			if(primeraDistancia < segundaDistancia)
			{
				return false;	
			}
		}

		else if(positionX1 < positionX2)
		{
			primeraDistancia = Math.Abs(Vector3.Distance(gestureLinesRenderer[0].GetPosition(0), gestureLinesRenderer[0].GetPosition(primerPuntoInterseccion-1)));
			segundaDistancia = Math.Abs(Vector3.Distance(gestureLinesRenderer[0].GetPosition(primerPuntoInterseccion), gestureLinesRenderer[0].GetPosition(gestureLinesRenderer[0].positionCount-1)));

			print("primeraDistancia: "+ primeraDistancia);
			print("segundaDistancia: " + segundaDistancia);
			print("Posicion X1: " +positionX1);
			print("Posicion X2: " +positionX2);
			// direcciónInicialCuadrante = 4;
			
			if(primeraDistancia > segundaDistancia)
			{
				return false;	
			}
		}



		
		//</Third Barrier>

		
		return true;
		

		
	}

	//Erase the current Gesture
	public void eraseGesture()
	{
		this.strokeId = -1;

	

		foreach (LineRenderer lineRenderer in this.gestureLinesRenderer)
		{
			// lineRenderer.SetVertexCount(0);
			Destroy(lineRenderer.gameObject);
		}

		this.gestureLinesRenderer.Clear();
	}

}
