using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallManager : MonoBehaviour
{

    private void OnEnable()
    {
        PlayerMovement.TurnOnWall += TurnOffWall;
    }

    private void OnDisable()
    {
        PlayerMovement.TurnOnWall -= TurnOffWall;
    }

     void TurnOffWall(BoxCollider2D wall)
    {
        wall.enabled = false;
        StartCoroutine(TurnWallOn(wall));
    }

    IEnumerator TurnWallOn(BoxCollider2D wall)
    {
        yield return new WaitForSeconds(0.5f);
        wall.enabled = true;
    }
}
