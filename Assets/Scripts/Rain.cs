using UnityEngine;

public class Rain : MonoBehaviour
{
    GameObject[] rain;
    int numRain = 50;
    public GameObject block;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        rain = new GameObject[numRain];
        for (int i = 0; i < numRain; i++)
        {
            rain[i] = GameObject.Instantiate(block);
            rain[i].transform.position = new Vector3(Random.Range(-10f, 10f), Random.Range(10f, 60f), Random.Range(-20f, 20f));
            rain[i].transform.Rotate(Random.Range(-4f, 4f), Random.Range(-4f, 4f), Random.Range(-4f, 4f));
        }

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
