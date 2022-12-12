using System;
using Infrastructure;
using Infrastructure.Services;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Baskets
{
    public class TrajectoryRenderer : MonoBehaviour
    {
        [SerializeField] private LineRenderer _line;
        
        private const int LinePositionCount = 25;
        private const float SimulatedStep = 0.05f;
        
        private Scene _simulationScene;
        private PhysicsScene _physicsScene;

        private void Start()
        {
            CreateScene();
        }

        public void EnableLine(bool value)
        {
            _line.gameObject.SetActive(value);
        }
        
        public void SimulateTrajectory(GameObject prefab, Vector3 origin, Vector3 velocity)
        {
            var obj = Instantiate(prefab, origin, Quaternion.identity);
            SceneManager.MoveGameObjectToScene(obj, _simulationScene);

            var rb = obj.GetComponent<Rigidbody>();
            rb.isKinematic = false;
            rb.AddForce(velocity, ForceMode.Impulse);
            _line.positionCount = LinePositionCount;

            for (int i = 0; i < LinePositionCount; i++)
            {
                _physicsScene.Simulate(SimulatedStep);
                _line.SetPosition(i, obj.transform.position);
            }
            Destroy(obj);
        }

        private void CreateScene()
        {
            _simulationScene = SceneManager.GetSceneByName(SceneLoader.SimulationSceneName);
            _physicsScene = _simulationScene.GetPhysicsScene();
        }
    }
}
