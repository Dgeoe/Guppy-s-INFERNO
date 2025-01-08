using System.Collections;
using UnityEngine;

public class FinalRoomPrinter : MonoBehaviour
{
    private GameObject parentGrid;

    // Used to spawn boss entrance room which will spawn the boss transition room
    // Scans the grid until rooms stop generating and then proceeds to replace the final instantiated room with the entrance

    public GameObject bossRoomPrefab;  // Reference to the boss room prefab

    void Start()
    {
        // Get the grid object in the scene
        parentGrid = GameObject.Find("Grid");

        // Start the coroutine to wait and print the last room name
        StartCoroutine(PrintFinalRoomName());
    }

    private IEnumerator PrintFinalRoomName()
    {
        // Wait for 5 seconds to guarantee the final room is instantiated
        yield return new WaitForSeconds(5f);

        // Get all room objects in the grid
        GameObject[] rooms = GameObject.FindGameObjectsWithTag("Room");

        // If rooms are found, get the last one in the array
        if (rooms.Length > 0)
        {
            GameObject lastRoom = rooms[rooms.Length - 1];
            Debug.Log("The final room instantiated is: " + lastRoom.name);

            // Replace the final room with the boss room entrance
            Vector3 lastRoomPosition = lastRoom.transform.position;  // Save the position
            Quaternion lastRoomRotation = lastRoom.transform.rotation;  // Save the rotation

            // Instantiate the boss room entrance at the same position and rotation as the final room
            GameObject bossRoom = Instantiate(bossRoomPrefab, lastRoomPosition, lastRoomRotation, parentGrid.transform);

            // Destroy the final room to replace it
            Destroy(lastRoom);

            Debug.Log("The final room has been replaced with the boss room.");
        }
        else
        {
            Debug.Log("No rooms found in the grid.");
        }
    }
}
