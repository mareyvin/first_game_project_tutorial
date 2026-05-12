using UnityEngine;
using UnityEngine.AI;
using TestGameTutorial.Utils;

public class EnemyAll : MonoBehaviour {

    [SerializeField] private State startingState;
    [SerializeField] private float roamingDistanceMax = 7f;
    [SerializeField] private float roamingDistanceMin = 3f;
    [SerializeField] private float roamingTimerMax = 2f;


    private NavMeshAgent navMeshAgent;
    private State state;
    private float roamingTime;
    private Vector3 roamPosition;
    private Vector3 startingPosition;

    private enum State {
        Roaming
    }
    private void Awake() {

        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.updateRotation = false; //чтобы объект не вращался при движении
        navMeshAgent.updateUpAxis = false; // чтобы ориентация навмеша не влияла на ориентацию объекта
        state = startingState;

    }

    private void Update() { //в каком состоянии агент
        switch (state) {
            default: // по дефорту обычное состояние
            case State.Roaming: // пока идет таймер обнуляется, еогда обнулится - идет к новой точке
                roamingTime -= Time.deltaTime;
                if (roamingTime < 0) {
                    Roaming();
                    roamingTime = roamingTimerMax;
                }
                break;
        }
    }

    private void Roaming() { //путь к точке
        startingPosition = transform.position; // обновление начальной (самой старт точки) чтобы иоб мог бешать не по одной области а по всей карте
        roamPosition = GetRoamingPosition();
        ChangeFacingDirection(startingPosition, roamPosition);
        navMeshAgent.SetDestination(roamPosition);
    }

    private Vector3 GetRoamingPosition() { // новая точка
        return startingPosition + Utils.GetRandomDir() * UnityEngine.Random.Range(roamingDistanceMin, roamingDistanceMax);
    }

    private void ChangeFacingDirection(Vector3 sourcePosition, Vector3 targetPosition) {
        if (sourcePosition.x > targetPosition.x) {
            transform.rotation = Quaternion.Euler(0, -180, 0);
        }
        else {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
