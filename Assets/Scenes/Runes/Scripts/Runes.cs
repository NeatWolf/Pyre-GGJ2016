﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Runes : MonoBehaviour {

    public Collider[] runesSources;
    public Collider[] runeSlots;
    public RuneData gameData;

    List<Transform> usedRunes = new List<Transform>();
    List<int> usedRuneIndexes = new List<int>();

	void Start () {
	    
	}
	
	void Update () {
        RaycastHit hit;
        if( Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 2f ) ){
            
            int source = System.Array.IndexOf( runesSources, hit.collider );
            int dest = System.Array.IndexOf( runeSlots, hit.collider );
            if( source != -1 ){
                Add( runesSources[source], source );
            }
               /*
            else {
                Remove( runeSlots[] );
            }
            */
        }
	}

    void Add(Collider runeSlot, int index){
        if( runeSlots.Length == usedRunes.Count ){
            return;
        }

        if( runeSlot.transform.childCount == 0 ){
            return;
        }

        Transform rune = runeSlot.transform.GetChild(0);
        usedRunes.Add( rune );
        usedRuneIndexes.Add( index );

        rune.parent = runeSlots[usedRunes.Count-1].transform;
        rune.localPosition = Vector3.zero;

        if( runeSlots.Length == usedRunes.Count ){
            Check();
        }
    }

    void Remove(){

    }

    void Check(){
        
        int rightNumberRightPlace = 0;
        int rightNumber = 0;
        for(int i=0;i<gameData.runeIndexes.Length;i++){
            int usedIndex = usedRuneIndexes.IndexOf( gameData.runeIndexes[i] );
            if( usedIndex == i ){
                rightNumberRightPlace++;
            }
            else if( usedIndex != -1 ){
                rightNumber++;
            } 
        }
        if( rightNumberRightPlace == runeSlots.Length ){
            Invoke("OnComplete",2f);
        }
        else {
            Invoke("Reset",2f);
        }

        StartCoroutine( PlayResult(rightNumberRightPlace,rightNumber) );

        // ( , usedRuneIndexes.OrderBy(r=>r) );
    }

    IEnumerator PlayResult(int rightNumber, int rightNumberRightPlace){
           yield break;
    }

    void OnComplete(){
        SendMessage("RitualComplete");
    }

    void Reset(){
        for(int i=0;i<usedRuneIndexes.Count;i++){
            usedRunes[i].parent = runesSources[i].transform;
            usedRunes[i].localPosition = Vector3.zero;
        }

        usedRuneIndexes.Clear();
        usedRunes.Clear();
    }
}
