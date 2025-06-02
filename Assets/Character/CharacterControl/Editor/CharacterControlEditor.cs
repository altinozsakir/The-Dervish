using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


namespace the_dervish{


    [CustomEditor(typeof(CharacterControl))]
    public class CharacterControlEditor : Editor
    {
        public override void OnInspectorGUI()
        {

            DrawDefaultInspector();

            CharacterControl control = (CharacterControl)target;

            if (GUILayout.Button("Setup RagdollParts (BodyParts)"))
            {
                control.SetRagdollParts();
            }
            
        }
       
        
    }


}

