using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class MyResources : MonoBehaviour{

    //##f13a66ff
    public static Color PM_COLOR = new Color32(0xF1, 0x3A, 0x66,0xFF);
    //#4ccd14ff
    public static Color IT_COLOR = new Color32(0x4c, 0xcd, 0x14, 0xFF);
    //b4a7d6ff
    public static Color GENERIC_COLOR = new Color32(0xb4, 0xa7, 0xd6, 0xFF);
    //#1155ccff
    public static Color SI_COLOR = new Color32(0x11, 0x55, 0xcc, 0xFF);
    //1C3733
    public static Color ACTIONABLE_PANEL_COLOR = new Color32(0x00, 0x00, 0x00, 0xFF);

    //FFFFFF
    public static Color NORMAL_PANEL_COLOR = new Color32(0xFF, 0xFF, 0xFF, 0x0F);

    public static int CARDS_BY_HAND = 3;
    public static int TARGET_DP_NUMBER = 3;

    //Complete string is VSEName + "has accomplished all of the Deploy Ment Packages".
    public static string WINNER_TEXT_SUFIX = "has accomplished all of the Deploy Ment Packages";

    public static string NO_PLAYERS_SELECTED = "You have not setted the players";
    public static string NO_PLAYERS_SELECTED_INFO = "You should set at least two VSE's.\n\nCheck the VSE that you want to select and write their names.";

    public static string GAME_WILL_START = "Game will start";
    public static string GAME_WILL_START_SUB = "The game is about to start";

    public static string DP_TEXT = "Deployment Package";
    public static string GP_TEXT = "Good Practice";
    public static string BP_TEXT = "Bad Practice";
    public static string EVENT_TEXT = "Event";


    public static string DP_PM_TEXT = "Project Management Process";
    public static string DP_IT_TEXT = "Highly Iterative Software Process";
    public static string DP_GENERIC_TEXT = "Generic DP";
    public static string DP_SI_TEXT = "Software Implementation Process";

    public static string GP_PM_TEXT = 
        
@"- Planeación.
- Ejecución de la planeación.
- Evaluación y control.
- Cierre del proyecto.
";
    public static string GP_IT_TEXT = 
@"- Fase de prueba de concepto
- Fase de producción
- Fase de posproducción
";

    public static string GP_SI_TEXT = 
@"Inicio de implementación
Análisis de requerimientos.
Identificación de componentes.
Construcción de software
Tests de integración
Entrega del producto
";

    public static string GP_GENERIC_TEXT = 
@"El proceso está bien";

    public static string BP_PM_TEXT = 
@"Mala planeación del proyecto.
Mal control de control de cambios";

    public static string BP_SI_TEXT = 
@"No se hizo documento de requisitos.
No se guardaron registros de tests.
La arquitectura no responde a todos los requisitos.
No se entregó manual de producto.
";
    public static string BP_IT_TEXT = 
@"La iteración fue caótica.
No se realizó prueba de concepto de planeación
No se hizo análisis inicial de requerimientos.
";


    public static string BP_GENERIC_TEXT = @"Algo no está bien";

    public static string EVENT_SW_QUALITY_EVENTS = "Software Quality Events";
    public static string EVENT_SW_QUALITY_EVENTS_SUB = "Interchage DP";

    public static string EVENT_ATTRACT_GOOD_EMPLOYEES = "Attract Good Employees";
    public static string EVENT_ATTRACT_GOOD_EMPLOYEES_SUB = "Get DP from the others VSE";

    public static string EVENT_COMPETITION_BAD_PRACTICES = "Competitions Adopts Bad Practices";
    public static string EVENT_COMPETITION_BAD_PRACTICES_SUB = "Bad practices for the others.";

    public static string EVENT_SOFTWARE_PROCESS_IMPROVEMENT = "Software process Improvemet";
    public static string EVENT_SOFTWARE_PROCESS_IMPROVEMENT_SUB = "You are one step ahead. Other VSE may change discart their cards";


    //static object[] sprites = AssetDatabase.LoadAllAssetsAtPath("Assets/Images/ICONS.png"); Does not compile.

    public static int PM_SPRITE_ICON = 0; //(Sprite)sprites[86];
    public static int SI_SPRITE_ICON = 1;//(Sprite)sprites[44];
    public static int IT_SPRITE_ICON = 2;//(Sprite)sprites[54];
    public static int GENERIC_SPRITE_ICON = 3; //(Sprite)sprites[55];

    public static int GP_SPRITE_ICON = 4;//sprites[60];
    public static int BP_SPRITE_ICON =5;//(Sprite)sprites[57];
    public static int EVENT_ICON = 6;//(Sprite)sprites[85];

    public static int TYPETEXT_FONT = 0;
    public static int DP_TEXT_FONT = 1;
    public static int GP_TEXT_FONT = 2;
    public static int BP_TEXT_FONT = 2;
    public static int EV_TEXT_FONT = 0;
    public static int EV_SUBTEXT_FONT = 3;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
