using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Background : MonoBehaviour {

    public float layer1_y;
    public float layer2_y;
    public float layer3_y;
    public float speed1 = 15f;
    public float speed2 = 15f;
    public float speed3 = 15f;
    public int layer1_buffer = 3;
    public int layer2_buffer = 3;
    public int layer3_buffer = 3;
    //backward parallax layer
    public GameObject layer1;
    private List<GameObject> layer1List;
    //middle parallax layer
    public GameObject layer2;
    private List<GameObject> layer2List;
    //forward parallax layer
    public GameObject layer3;
    private List<GameObject> layer3List;

    private float layer1UnityWidth;
    private float layer2UnityWidth;
    private float layer3UnityWidth;

    // Use this for initialization
    void Start () {
        layer1List = new List<GameObject>();
        layer2List = new List<GameObject>();
        //layer3List = new List<GameObject>();

        layer1UnityWidth = layer1.GetComponent<SpriteRenderer>().sprite.texture.width / layer1.GetComponent<SpriteRenderer>().sprite.pixelsPerUnit;
        layer2UnityWidth = layer2.GetComponent<SpriteRenderer>().sprite.texture.width / layer2.GetComponent<SpriteRenderer>().sprite.pixelsPerUnit;
        //layer3UnityWidth = layer3.GetComponent<SpriteRenderer>().sprite.texture.width / layer3.GetComponent<SpriteRenderer>().sprite.pixelsPerUnit;

        //Load in the parallax background images
        for (int i = 0; i < layer1_buffer; i++)
        {
            layer1List.Add((GameObject)Instantiate(layer1, new Vector3(layer1UnityWidth * i, layer1_y, 50  ), Quaternion.identity));
        }
        for (int i = 0; i < layer2_buffer; i++)
        {
            layer2List.Add((GameObject)Instantiate(layer2, new Vector3(layer2UnityWidth * i, layer2_y, 49 ), Quaternion.identity));
        }
        //for (int i = 0; i < layer3_buffer; i++)
        //{
            //layer3List.Add((GameObject)Instantiate(layer3, new Vector3(layer3UnityWidth * i, layer3_y, 3 + i * 0.001f), Quaternion.identity));
        //}
    }

    // Update is called once per frame
    void Update () {
        //Move the background left along the screen
        Vector3 scrollSpeed1 = Time.deltaTime * new Vector3(speed1, 0, 0);
        Vector3 scrollSpeed2 = Time.deltaTime * new Vector3(speed2, 0, 0);
        Vector3 scrollSpeed3 = Time.deltaTime * new Vector3(speed3, 0, 0);

        foreach (GameObject frame in layer1List)
        {
            frame.transform.position -= scrollSpeed1;
        }
        foreach (GameObject frame in layer2List)
        {
            frame.transform.position -= scrollSpeed2;
        }
        //foreach (GameObject frame in layer3List)
        //{
           // frame.transform.position -= scrollSpeed3;
        //}

        float resetPos = -20.0f;
        float moveTo = resetPos + (layer1UnityWidth * layer1_buffer);
        //if parallax layer 1 moves too far left, move it back around to the right
        if (layer1List[0].transform.position.x < resetPos)
        {
            GameObject temp = layer1List[0];
            layer1List.RemoveAt(0);
            temp.transform.position = new Vector3(moveTo, layer1_y, temp.transform.position.z);
            layer1List.Add(temp);
        }
        moveTo = resetPos + (layer2UnityWidth * layer2_buffer);
        //if parallax layer 2 moves too far left, move it back around to the right
        if (layer2List[0].transform.position.x < resetPos)
        {
            GameObject temp = layer2List[0];
            layer2List.RemoveAt(0);
            temp.transform.position = new Vector3(moveTo, layer2_y, temp.transform.position.z);
            layer2List.Add(temp);
        }
        //moveTo = resetPos + (layer3UnityWidth * layer3_buffer);
        //if parallax layer 3 moves too far left, move it back around to the right
        //if (layer3List[0].transform.position.x < resetPos)
        //{
           // GameObject temp = layer3List[0];
            //layer3List.RemoveAt(0);
            //temp.transform.position = new Vector3(moveTo, layer3_y, temp.transform.position.z);
            //layer3List.Add(temp);
       // }
    }
}
