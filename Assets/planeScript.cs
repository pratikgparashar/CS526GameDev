using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class planeScript : MonoBehaviour {
    private Rigidbody2D rb;
    public GameObject pl;
    float randX = 0.0f;
    float randY = 0.0f;
    Vector3 directionF = new Vector3(0.02f,0.05f,0);
    bool moveSingle = true;
    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        randX = Random.Range(-0.04f, 0.0f);
        randY = Random.Range(-0.04f, 0.02f);

    }

    // Update is called once per frame
    void Update()
    {
        if (moveSingle)
            //pl.transform.Translate(directionF.x*0.001f,directionF.y*0.002f,0);
            //pl.transform.position = pl.transform.TransformDirection(directionF);
            //transform.Translate()
            //transform.Translate()
            pl.transform.Translate(pl.transform.forward * Time.deltaTime * 5, Space.Self);

        //transform.Tra
    }
    public void setMoveSingle(bool toogle){
        moveSingle = toogle;
    }
    public void setDirection(Vector3 direction){
        moveSingle = true;
        directionF = direction;
        //transform.Translate(direction);
    }
}
