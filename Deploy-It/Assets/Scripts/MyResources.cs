using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class MyResources : MonoBehaviour{

    //##f13a66ff
    public static Color PM_COLOR = new Color(0xF1, 0x3A, 0x66);
    //#4ccd14ff
    public static Color IT_COLOR = new Color(0x4c, 0xcd, 0x14);
    //b4a7d6ff
    public static Color GENERIC_COLOR = new Color(0xb4, 0xa7, 0xd6);
    //#1155ccff
    public static Color SI_COLOR = new Color(0x11, 0x55, 0xcc);

    public static string DP_PM_TEXT = "Project Management Process";
    public static string DP_IT_TEXT = "Highly Iterative Software Process";
    public static string DP_GENERIC_TEXT = "Highly Iterative Software Process";
    public static string DP_SI_TEXT = "Software Implementation Process";

    public static string GP_PM_TEXT = @"- Planeación.
                                        - Ejecución de la planeación.
                                        - Evaluación y control.
                                        - Cierre del proyecto.
                                        ";
    public static string GP_IT_TEXT = @"- Fase de prueba de concepto
                                        - Fase de producción
                                        - Fase de posproducción
                                        ";

    public static string GP_SI_TEXT = @"Inicio de implementación
                                        Análisis de requerimientos.
                                        Identificación de componentes.
                                        Construcción de software
                                        Tests de integración
                                        Entrega del producto
                                        ";

    public static string GP_GENERIC_TEXT = @"El proceso está bien";

    public static string BP_PM_TEXT = @"Mala planeación del proyecto.
                                        Mal control de control de cambios";

    public static string BP_SI_TEXT = @"No se hizo documento de requisitos.
                                        No se guardaron registros de tests.
                                        La arquitectura no responde a todos los requisitos.
                                        No se entregó manual de producto.
                                        ";
    public static string BP_IT_TEXT = @"La iteración fue caótica.
                                        No se realizó prueba de concepto de planeación
                                        No se hizo análisis inicial de requerimientos.
                                        ";





    static Sprite[] sprites = (Sprite[])AssetDatabase.LoadAllAssetsAtPath("Assets/Images/ICONS.png");

    public static Sprite PM_SPRITE_ICON = sprites[86];
    public static Sprite SI_SPRITE_ICON = sprites[44];
    public static Sprite IT_SPRITE_ICON = sprites[54];
    public static Sprite GENERIC_SPRITE_ICON = sprites[55];

    public static Sprite GP_SPRITE_ICON = sprites[60];
    public static Sprite BP_SPRITE_ICON = sprites[57];
    public static Sprite EVENT_ICON = sprites[85];





	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
