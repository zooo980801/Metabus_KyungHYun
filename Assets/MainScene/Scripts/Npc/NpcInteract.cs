using UnityEngine;

namespace MainScene
{
    public enum NpcType
    {
        GameSelect,
        Score,
        Reset
    }
    public class NpcInteract : MonoBehaviour
    {
        [SerializeField] private NpcType npcType;
        [SerializeField] private GameSelectUIManager gameSelectUIManager;
        [SerializeField] private ResetUI resetUI;
        [SerializeField] private ScoreBoardUI scoreBoardUI;
        [SerializeField] private AudioClip interactSFX;

        private AudioSource audioSource;

        private bool isPlayerNear = false;

        private void Update()
        {
            if (isPlayerNear && Input.GetKeyDown(KeyCode.Space))
            {
                if (interactSFX != null)
                    audioSource.PlayOneShot(interactSFX);

                switch (npcType)
                {
                    case NpcType.GameSelect:
                        gameSelectUIManager?.OpenSelectionPanel();
                        break;
                    case NpcType.Score:
                        scoreBoardUI?.Open();
                        break;
                    case NpcType.Reset:
                        resetUI?.OpenResetConfirmPanel();
                        break;
                }
            }
        }

        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();
            if (audioSource == null)
                audioSource = gameObject.AddComponent<AudioSource>();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                isPlayerNear = true;
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                isPlayerNear = false;
            }
        }
    }

}
