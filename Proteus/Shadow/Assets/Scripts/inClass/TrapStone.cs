using UnityEngine;

public class TrapStone : MonoBehaviour
{

    public Rigidbody Rb;

    public bool Triggering = false;


	void Update () {
	    if (Triggering)
	    {
	        Triggering = false;
	        ReleaseStone();
	    }
	}

    void ReleaseStone()
    {
        Rb.constraints = RigidbodyConstraints.None;
    }
}
