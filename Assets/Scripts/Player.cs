
using Interfaces;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{
   
    public static Player Instance { get; private set; }
    
    void Start()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

 
    void Update()
    {
        
    }

    public void TakeDamage(int damage)
    {
        throw new System.NotImplementedException();
    }

    public void Kill()
    {
        throw new System.NotImplementedException();
    }


}
