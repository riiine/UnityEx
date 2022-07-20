using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowGenerator : MonoBehaviour
{
    public GameObject arrowPrefab;
    float spawn = 2.0f;
    float delta = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameMgr.Inst.gameState != GameState.GameIng)
        {
            spawn = 2.0f;
            delta = 0;
            return;
        }

        delta += Time.deltaTime;
        if (delta > this.spawn)
        {
            this.delta = 0;

            GameObject go = Instantiate(arrowPrefab) as GameObject;

            int dropPos = Random.Range(-2, 3);
            go.GetComponent<ArrowCtrl>().SetInit(dropPos);

            spawn = 2.0f - GameMgr.Inst.DiffLevel * 0.2f;
            if (spawn < 0.5f)
                spawn = 0.5f;
        } //if (delta > this.spawn)
    }// void Update()
}
