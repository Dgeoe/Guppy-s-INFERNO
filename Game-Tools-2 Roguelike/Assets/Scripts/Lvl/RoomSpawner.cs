using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    [Header("1= needs bottom Hall")]
    [Header("2= needs top Hall")]
    [Header("3= needs left Hall")]
    [Header("4= needs right Hall")]
    [Header("5= needs bottom Room")]
    [Header("6= needs top Room")]
    [Header("7= needs left Room")]
    [Header("8= needs right Room")]
    [Header("9= needs boss Room")]

   public int openingDirection;
   //int meanings: 1= needs bottom door/entrance, 2= needs top door/entrance, 3= needs left door/entrance, 4= needs right door/entrance, 5= boss

   private RoomTemplates templates;
   private int rand;
   private bool spawned = false;

   private GameObject parentGrid;

   void Start()
   {
    templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
    Invoke("Spawn", 0.1f);

    // This returns the Grid from the current scene so the instantiated tilemap spawns correctly and is not jumbled.
    parentGrid = GameObject.Find("Grid");

   }

   void Spawn()
   {
    if (spawned == false)
    {
        if (openingDirection == 1)
        {
            //will spawn a hallway with a BOTTOM facing door
            rand = Random.Range(0, templates.bottomRooms.Length);
            Instantiate(templates.bottomRooms[rand], transform.position, templates.bottomRooms[rand].transform.rotation, parentGrid.transform);
        }
        else if(openingDirection == 2)
        {
            //will spawn a hallway with a TOP facing door
            rand = Random.Range(0, templates.topRooms.Length);
            Instantiate(templates.topRooms[rand], transform.position, templates.topRooms[rand].transform.rotation, parentGrid.transform);
        }
        else if(openingDirection == 3)
        {
            //will spawn a hallway with a LEFT facing door
            rand = Random.Range(0, templates.leftRooms.Length);
            Instantiate(templates.leftRooms[rand], transform.position, templates.leftRooms[rand].transform.rotation, parentGrid.transform);
        }
        else if(openingDirection == 4)
        {
            //will spawn a hallway with a RIGHT facing door
            rand = Random.Range(0, templates.rightRooms.Length);
            Instantiate(templates.rightRooms[rand], transform.position, templates.rightRooms[rand].transform.rotation, parentGrid.transform);
        }
        else if(openingDirection == 5)
        {
            //will spawn a room with a BOTTOM facing door
            rand = Random.Range(0, templates.bottomRealRooms.Length);
            Instantiate(templates.bottomRealRooms[rand], transform.position, templates.rightRooms[rand].transform.rotation, parentGrid.transform);
        }
        else if(openingDirection == 6)
        {
            //will spawn a room with a TOP facing door
            rand = Random.Range(0, templates.topRealRooms.Length);
            Instantiate(templates.topRealRooms[rand], transform.position, templates.rightRooms[rand].transform.rotation, parentGrid.transform);
        }
        else if(openingDirection == 7)
        {
            //will spawn a room with a LEFT facing door
            rand = Random.Range(0, templates.leftRealRooms.Length);
            Instantiate(templates.leftRealRooms[rand], transform.position, templates.rightRooms[rand].transform.rotation, parentGrid.transform);
        }
        else if(openingDirection == 8)
        {
            //will spawn a room with a RIGHT facing door
            rand = Random.Range(0, templates.rightRealRooms.Length);
            Instantiate(templates.rightRealRooms[rand], transform.position, templates.rightRooms[rand].transform.rotation, parentGrid.transform);
        }
        else if(openingDirection == 9)
        {
            //will spawn a room with a BOSS 
            rand = Random.Range(0, templates.bossRooms.Length);
            Instantiate(templates.bossRooms[rand], transform.position, templates.rightRooms[rand].transform.rotation, parentGrid.transform);
        }
        else if(openingDirection == 0)
        {
            Debug.Log("DESTROYED");
        }
        else
        {
            Debug.Log("Error: No viable room set");
        }
        spawned = true;
    }

   }
   
   //guarentees rooms wont spawn on top of each other 
   void OnTriggerEnter2D(Collider2D other)
   {
    if(other.CompareTag("SpawnPoint")) 
    {
        if(other.GetComponent<RoomSpawner>().spawned == false && spawned == false)
        {
            //spawn walls blocking off any openings 
            Instantiate(templates.closedRoom[rand], transform.position, templates.closedRoom[rand].transform.rotation, parentGrid.transform);
            Destroy(gameObject);
        }
        spawned = true; 
    }
   }

}
