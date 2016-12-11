using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Arrangement : MonoBehaviour {
    public abstract bool evaluate();
    public Furniture furnitureParent;
}
