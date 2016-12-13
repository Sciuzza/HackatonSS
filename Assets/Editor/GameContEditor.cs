using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
/// <summary>
/// CI SONO PROBLEMI DI IDENTAZIONE ATTUALMENTE
/// 
/// Attualmente sono presenti anche i valori della lunghezza degli array per debuggare
/// da implementare ancora il nome delle News, Scene e dei Clues in base non all'index (int) ma al Name (string). Lo farà in automatico
/// </summary>
[CustomEditor(typeof(GameContN))]



public class GameContEditor : Editor
{
    public SerializedProperty playerData_Prop;

    // Prendiamo la propietà principale nella classe GameContN ovvero playerDatas
    void OnEnable()
    {

    }

    public override void OnInspectorGUI()
    {
        playerData_Prop = serializedObject.FindProperty("playerDatas");
        serializedObject.Update();

        #region PlayerDataVisulizator

        // Visualiziamo la propietà in inspector di playerData
        EditorGUILayout.PropertyField(playerData_Prop);
        if (playerData_Prop.isExpanded)
        {
            EditorGUI.indentLevel += 1;

            // Prendiamo ogni singola propietà presente all'interno di sensibleGeneralData
            SerializedProperty lastSceneVisited_Prop = playerData_Prop.FindPropertyRelative("lastSceneVisited");
            SerializedProperty lastNewsVisited_Prop = playerData_Prop.FindPropertyRelative("lastNewsVisited");
            SerializedProperty lastCityVisited_Prop = playerData_Prop.FindPropertyRelative("lastCityVisited");
            SerializedProperty sceneCreation_Prop = playerData_Prop.FindPropertyRelative("runtimeSceneCreationMode");
            SerializedProperty mapData_Prop = playerData_Prop.FindPropertyRelative("mapData");

            // E le visualizziamo
            EditorGUILayout.PropertyField(lastSceneVisited_Prop);
            EditorGUILayout.PropertyField(lastNewsVisited_Prop);
            EditorGUILayout.PropertyField(lastCityVisited_Prop);
            EditorGUILayout.PropertyField(sceneCreation_Prop);

            // Array size di fianco a Map Data
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PropertyField(mapData_Prop);
            EditorGUILayout.PropertyField(mapData_Prop.FindPropertyRelative("Array.size"));
            EditorGUILayout.EndHorizontal();

            #region MapData_Visualizator
            if (mapData_Prop.isExpanded)
            {
                //EditorGUI.indentLevel += 1;
                // Per ogni elemento dell'array all'interno di mapData
                for (int i = 0; i < mapData_Prop.arraySize; i++)
                {
                    // Questo mi serve per non rendere il codice troppo morboso   
                    SerializedProperty arrayMapNameRef = mapData_Prop.GetArrayElementAtIndex(i);

                    // Prendiamo ogni singola propietà presente all'interno di sensibleMapData
                    SerializedProperty mapName_Prop = arrayMapNameRef.FindPropertyRelative("mapName");
                    SerializedProperty newsData = arrayMapNameRef.FindPropertyRelative("newsData");

                    // Creo una variabile cities per estrapolare il valore contenente a mapName
                    // in modo tale da dare all'elemento dell'array il valore contenente la variabile (fa figo)

                    // Dato che fede e cri sono dei bastardi e non mi dicono niente ora estrapolo il valore
                    // dalla variabile mapName che è una stringa e la metto al nome dell'elemeno dell'array
                                   
                    EditorGUILayout.PropertyField(arrayMapNameRef,new GUIContent (mapName_Prop.stringValue));


                    if (arrayMapNameRef.isExpanded)
                    {
                        //EditorGUI.indentLevel += 1;
                        // Ed ora visualizziamo le propietà di sensibleMapData
                        EditorGUILayout.PropertyField(mapName_Prop);

                        EditorGUILayout.BeginHorizontal();
                        EditorGUILayout.PropertyField(newsData);
                        EditorGUILayout.PropertyField(newsData.FindPropertyRelative("Array.size"));
                        EditorGUILayout.EndHorizontal();
                        #region NewsData_Visualizator
                        if (newsData.isExpanded)
                        {
                            for (int j = 0; j < newsData.arraySize; j++)
                            {
                                // Vedi riga 50
                                SerializedProperty arrayNewsDataRef = newsData.GetArrayElementAtIndex(j);

                                // Prendiamo le propietà di sensibleNewsData
                                SerializedProperty newsIndex_Prop = arrayNewsDataRef.FindPropertyRelative("newsName");
                                SerializedProperty newsInfoText_Prop = arrayNewsDataRef.FindPropertyRelative("newsInfoText");
                                SerializedProperty playerCurrentScore_Prop = arrayNewsDataRef.FindPropertyRelative("playerCurrentScore");
                                SerializedProperty scenesData_Prop = arrayNewsDataRef.FindPropertyRelative("scenesData");

                                // E le visualizziamo
                                //EditorGUI.indentLevel += 1;
                                EditorGUILayout.PropertyField(newsIndex_Prop);
                                EditorGUILayout.PropertyField(newsInfoText_Prop);
                                EditorGUILayout.PropertyField(playerCurrentScore_Prop);

                                EditorGUILayout.BeginHorizontal();
                                EditorGUILayout.PropertyField(scenesData_Prop);
                                EditorGUILayout.PropertyField(scenesData_Prop.FindPropertyRelative("Array.size"));
                                EditorGUILayout.EndHorizontal();
                                #region SceneData_Visualizator
                                if (scenesData_Prop.isExpanded)
                                {
                                    for (int k = 0; k < scenesData_Prop.arraySize; k++)
                                    {
                                        // Vedi riga 50
                                        SerializedProperty arrayScenesDataRef = scenesData_Prop.GetArrayElementAtIndex(k);

                                        // Prendiamo le propietà di sensibleSceneData
                                        SerializedProperty sceneIndex_Prop = arrayScenesDataRef.FindPropertyRelative("sceneIndex");
                                        SerializedProperty cluesData_Prop = arrayScenesDataRef.FindPropertyRelative("cluesData");

                                        // E le visualizziamo
                                        //EditorGUI.indentLevel += 1;
                                        EditorGUILayout.PropertyField(sceneIndex_Prop);

                                        EditorGUILayout.BeginHorizontal();
                                        EditorGUILayout.PropertyField(cluesData_Prop);
                                        EditorGUILayout.PropertyField(cluesData_Prop.FindPropertyRelative("Array.size"));
                                        EditorGUILayout.EndHorizontal();
                                        #region ClueData_Visualizator
                                        if (cluesData_Prop.isExpanded)
                                        {
                                            EditorGUI.indentLevel += 1;
                                            for (int h = 0; h < cluesData_Prop.arraySize; h++)
                                            {
                                                // Vedi riga 50
                                                SerializedProperty arraycluesData = cluesData_Prop.GetArrayElementAtIndex(h);

                                                // Prendiamo le propietà di sensibleClueData
                                                SerializedProperty clueName_Prop = arraycluesData.FindPropertyRelative("clueName");
                                                SerializedProperty clueInfoText_Prop = arraycluesData.FindPropertyRelative("clueInfoText");
                                                SerializedProperty hasBeenFound_Prop = arraycluesData.FindPropertyRelative("hasBeenFound");

                                                string nameClue = clueName_Prop.stringValue;
                                                if (nameClue == "")
                                                {
                                                    EditorGUILayout.PropertyField(arraycluesData, new GUIContent("Clue " + h));
                                                }
                                                else
                                                {
                                                    EditorGUILayout.PropertyField(arraycluesData, new GUIContent(nameClue));
                                                }


                                                //EditorGUI.indentLevel += 1;
                                                if (arraycluesData.isExpanded)
                                                {
                                                    // E le visualizziamo

                                                    EditorGUILayout.PropertyField(clueName_Prop);
                                                    EditorGUILayout.PropertyField(clueInfoText_Prop);
                                                    EditorGUILayout.PropertyField(hasBeenFound_Prop);
                                                }

                                                //EditorGUI.indentLevel -= 1;
                                            }
                                            EditorGUI.indentLevel -= 1;
                                        }
                                        EditorGUILayout.Separator();
                                        //EditorGUI.indentLevel -= 1;
                                        #endregion                                        
                                    }
                                    //EditorGUI.indentLevel -= 1;
                                }
                                #endregion                                
                            }
                        }
                        //EditorGUI.indentLevel -= 1;
                        #endregion
                        // Metto un pò di spazi tra una città e l'altra
                        EditorGUILayout.Space();
                        EditorGUILayout.Space();
                        EditorGUILayout.Space();
                        EditorGUILayout.Space();
                        EditorGUILayout.Space();
                        EditorGUILayout.Space();
                    }


                }

            }
            #endregion

        }
        #endregion
        serializedObject.ApplyModifiedProperties();

    }



}

