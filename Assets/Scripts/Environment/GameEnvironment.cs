using Infrastructure.Services;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Environment
{
    public class GameEnvironment : MonoBehaviour
    {
        [SerializeField] private GameObject _leftObj;
        [SerializeField] private GameObject _rightObj;
        [SerializeField] private MeshRenderer _backMashRender;

        private GameObject _leftGhostObj;
        private GameObject _rightGhostObj;

        private void Start()
        {
            _leftGhostObj = CreateGhost(_leftObj);
            _rightGhostObj = CreateGhost(_rightObj);
        }

        private void Update()
        {
            _leftGhostObj.transform.position = _leftObj.transform.position;
            _rightGhostObj.transform.position = _rightObj.transform.position;
        }

        public void SetBackgroundColor(Color color)
        {
            _backMashRender.material.color = color;
        }

        private GameObject CreateGhost(GameObject obj)
        {
            GameObject newObj = Instantiate(obj, obj.transform.position, obj.transform.rotation);
            newObj.GetComponent<Renderer>().enabled = false;
            SceneManager.MoveGameObjectToScene(newObj, 
                SceneManager.GetSceneByName(SceneLoader.SimulationSceneName));
            return newObj;
        }
    }
}
