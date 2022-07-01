using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisappearingWall : MonoBehaviour
{
    List<GameObject> listOfTargets = new List<GameObject>();
    public string tagName = "TagName";
 
    void Start()
    {
        listOfTargets.AddRange(GameObject.FindGameObjectsWithTag(tagName));
        print(listOfTargets.Count);
    }
 
    public void KilledOpponent(GameObject opponent)
    {
            print("Target destroyed");
            if (listOfTargets.Contains(opponent))
            {
                listOfTargets.Remove(opponent);
                AreOpponentsDead();
            }
        
    }
 
    void AreOpponentsDead()
    {
        if(listOfTargets.Count <= 0)
        {
            // They are dead!
            Destroy(this.gameObject);
        }
    }
}
