using MainScene;
using UnityEngine;

public class MainGameManager : MonoBehaviour
{
    private void Awake()
    {
        // GlobalGameState 프리팹이 없으면 Resources에서 로드
        if (GlobalGameState.Instance == null)
        {
            GlobalGameState existing = FindObjectOfType<GlobalGameState>();
            if (existing == null)
            {
                var prefab = Resources.Load<GameObject>("GlobalGameState");
                if (prefab != null)
                {
                    GameObject obj = Instantiate(prefab);
                    obj.transform.position = prefab.transform.position;
                    obj.transform.rotation = prefab.transform.rotation;
                }
            }
        }
    }

    private void Start()
    {
        // 씬 복귀 시 플레이어 위치 복원
        if (GlobalGameState.Instance != null && GlobalGameState.Instance.returnToSelection)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                player.transform.position = GlobalGameState.Instance.savedPlayerPosition;

                Camera mainCam = Camera.main;
                if (mainCam != null)
                {
                    FollowCamera follow = mainCam.GetComponent<FollowCamera>();
                    if (follow != null)
                    {
                        follow.target = player.transform;
                    }
                }
            }

            GlobalGameState.Instance.returnToSelection = false;
        }
    }

}
