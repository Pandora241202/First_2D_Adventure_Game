using UnityEngine;

public class CamManager
{
    private Camera cam;

    public CamManager(Camera cam)
    {
        this.cam = cam;
    }

    public void MyUpdate()
    {
        MapManager mapManager = AllManager.Instance().mapManager;
        Vector3 playerPos = AllManager.Instance().playerManager.GetPos();

        float viewHeight = 2f * cam.orthographicSize;
        float viewWidth = viewHeight * cam.aspect;

        Vector3 newCamPos = cam.gameObject.transform.position;

        if (playerPos.x - mapManager.LeftEdge < viewWidth / 2)
        {
            newCamPos.x = mapManager.LeftEdge + viewWidth / 2;
        }
        else if (mapManager.RightEdge - playerPos.x < viewWidth / 2)
        {
            newCamPos.x = mapManager.RightEdge - viewWidth / 2;
        } else
        {
            newCamPos.x = playerPos.x;
        }

        if (playerPos.y - mapManager.BotEdge < viewHeight / 2)
        {
            newCamPos.y = mapManager.BotEdge + viewHeight / 2;
        }
        else if (mapManager.TopEdge - playerPos.y < viewHeight / 2)
        {
            newCamPos.y = mapManager.TopEdge - viewHeight / 2;
        }
        else
        {
            newCamPos.y = playerPos.y;
        }

        cam.gameObject.transform.position = newCamPos;
    }
}
