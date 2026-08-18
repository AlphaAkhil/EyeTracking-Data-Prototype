using UnityEngine;

namespace Spidy.XRDataShowcase{
    public class AOIObjectID : MonoBehaviour
    {
        [SerializeField]
        private string id;

        public string ID => id;

        private void Reset()
        {
            if (string.IsNullOrEmpty(id))
            {
                id = gameObject.name;
            }
        }
    }
}