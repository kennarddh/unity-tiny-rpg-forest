using UnityEngine;

namespace Game.Achivement
{
    public class AchivementSystem : MonoBehaviour
    {
        public static AchivementSystem Instance { get; private set; }

        public Achivements Achivements;

        private void Start()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this);
            }
            else
            {
                DontDestroyOnLoad(this);
                Instance = this;
            }
        }
    }
}
