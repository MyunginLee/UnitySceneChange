using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Author: Myungin Lee. 
// This codes shows how to specify, clone, and manipulate the game objects using rigidbody components

public class Chaser : MonoBehaviour
{
    GameObject player;
    GameObject monkey;
    GameObject[] clones;
    int numberOfMonkeys = 10;
    const float sponRange = 20;

    void Awake()
    {
        clones = new GameObject[numberOfMonkeys];
        // find and link the gameobject in the scene to this script's gameobject
        player = GameObject.Find("player");
        monkey = GameObject.Find("monkey");
    }
    void Start()
    {
        for (int i = 0; i < numberOfMonkeys; i++)
        {
            // make clones of the original monkey
            clones[i] = GameObject.Instantiate(monkey);
            // Initially randomize the position of cloned monkeys
            clones[i].transform.position = new Vector3(Random.Range(-sponRange, sponRange), 0.5f, Random.Range(-sponRange, sponRange));
            clones[i].transform.rotation = Quaternion.Euler(0, Random.Range(0, 360f), 0);
        }
    }

    void Update()
    {
        // Let's make the monkeys chase the target
        for (int i = 0; i < numberOfMonkeys; i++)
        {
            // let the monkeys(clones) find the player(target)
            clones[i].transform.LookAt(player.transform.position);
            // Derive the vector between the player and monkeys
            Vector3 target = player.transform.position - clones[i].transform.position;
            // Decide velocity of monkeys based on the normalized vector (a vector with the direction but the size is always 1)
            clones[i].GetComponent<Rigidbody>().linearVelocity = target.normalized * AudioSpectrum.audioAmp * 2f; // Of course you can try something else.. 
                                                                                    // monkeys react to the music
        }

    }
}
