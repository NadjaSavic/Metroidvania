using UnityEngine;

namespace Manager
{
    public class DeathManager : MonoBehaviour
    {

        public static DeathManager Instance; 
        
        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return; 
            }

            Instance = this; 
        }
    }
}
