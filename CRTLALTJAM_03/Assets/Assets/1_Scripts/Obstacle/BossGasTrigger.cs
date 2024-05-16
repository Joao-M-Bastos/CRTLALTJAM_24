using UnityEngine;

public class BossGasTrigger : MonoBehaviour
{
    [SerializeField] GameObject bossGas;
    [SerializeField] Transform gasSpawnerTransform;

    [SerializeField] int bossGasSize;
    GameObject bossGasInstance;
    // Start is called before the first frame update
    void Start()
    {
            
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GenerateGas()
    {
        bossGasInstance = Instantiate(bossGas, gasSpawnerTransform.position, gasSpawnerTransform.rotation);

        bossGasInstance.transform.localScale += Vector3.up * bossGasSize;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player") && bossGasInstance ==  null)
            GenerateGas();
    }
}
