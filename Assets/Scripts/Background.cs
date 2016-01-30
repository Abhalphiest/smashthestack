using UnityEngine;
using System.Collections;

public class Background : MonoBehaviour {

    //backward parallax layer
    public GameObject layer1;
    private GameObject layer1A;
    private GameObject layer1B;
    private GameObject layer1C;
    //middle parallax layer
    public GameObject layer2;
    private GameObject layer2A;
    private GameObject layer2B;
    private GameObject layer2C;
    //forward parallax layer
    public GameObject layer3;
    private GameObject layer3A;
    private GameObject layer3B;
    private GameObject layer3C;

    // Use this for initialization
    void Start () {
        //Load in the parallax background images
        layer1A = (GameObject)Instantiate(layer1, new Vector3(0, 0, 5), Quaternion.identity);
        layer1B = (GameObject)Instantiate(layer1, new Vector3(14, 0, 5), Quaternion.identity);
        layer1C = (GameObject)Instantiate(layer1, new Vector3(28, 0, 5), Quaternion.identity);
        layer2A = (GameObject)Instantiate(layer2, new Vector3(0, 0, 4), Quaternion.identity);
        layer2B = (GameObject)Instantiate(layer2, new Vector3(14, 0, 4), Quaternion.identity);
        layer2C = (GameObject)Instantiate(layer2, new Vector3(28, 0, 4), Quaternion.identity);
        layer3A = (GameObject)Instantiate(layer3, new Vector3(0, 0, 3), Quaternion.identity);
        layer3B = (GameObject)Instantiate(layer3, new Vector3(14, 0, 3), Quaternion.identity);
        layer3C = (GameObject)Instantiate(layer3, new Vector3(28, 0, 3), Quaternion.identity);
    }

    // Update is called once per frame
    void Update () {
        //Move the background left along the screen
        Vector3 scrollSpeed1 = new Vector3(0.1f, 0, 0);
        layer1A.transform.position -= scrollSpeed1;
        layer1B.transform.position -= scrollSpeed1;
        layer1C.transform.position -= scrollSpeed1;
        Vector3 scrollSpeed2 = new Vector3(0.15f, 0, 0);
        layer2A.transform.position -= scrollSpeed2;
        layer2B.transform.position -= scrollSpeed2;
        layer2C.transform.position -= scrollSpeed2;
        Vector3 scrollSpeed3 = new Vector3(0.2f, 0, 0);
        layer3A.transform.position -= scrollSpeed3;
        layer3B.transform.position -= scrollSpeed3;
        layer3C.transform.position -= scrollSpeed3;

        int screensPerLayer = 3;
        float resetPos = -20.0f;
        float moveTo = resetPos + (14 * screensPerLayer);
        //if parallax layer 1A moves too far left, move it back around to the right
        if (layer1A.transform.position.x < resetPos)
        {
            layer1A.transform.position = new Vector3(moveTo, 0, 5);
        }
        //if parallax layer 1B moves too far left, move it back around to the right
        if (layer1B.transform.position.x < resetPos)
        {
            layer1B.transform.position = new Vector3(moveTo, 0, 5);
        }
        //if parallax layer 1C moves too far left, move it back around to the right
        if (layer1C.transform.position.x < resetPos)
        {
            layer1C.transform.position = new Vector3(moveTo, 0, 5);
        }
        //if parallax layer 2A moves too far left, move it back around to the right
        if (layer2A.transform.position.x < resetPos)
        {
            layer2A.transform.position = new Vector3(moveTo, 0, 4);
        }
        //if parallax layer 2B moves too far left, move it back around to the right
        if (layer2B.transform.position.x < resetPos)
        {
            layer2B.transform.position = new Vector3(moveTo, 0, 4);
        }
        //if parallax layer 2C moves too far left, move it back around to the right
        if (layer2C.transform.position.x < resetPos)
        {
            layer2C.transform.position = new Vector3(moveTo, 0, 4);
        }
        //if parallax layer 3A moves too far left, move it back around to the right
        if (layer3A.transform.position.x < resetPos)
        {
            layer3A.transform.position = new Vector3(moveTo, 0, 3);
        }
        //if parallax layer 3B moves too far left, move it back around to the right
        if (layer3B.transform.position.x < resetPos)
        {
            layer3B.transform.position = new Vector3(moveTo, 0, 3);
        }
        //if parallax layer 3C moves too far left, move it back around to the right
        if (layer3C.transform.position.x < resetPos)
        {
            layer3C.transform.position = new Vector3(moveTo, 0, 3);
        }
    }
}
