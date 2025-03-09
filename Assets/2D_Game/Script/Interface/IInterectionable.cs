using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInterectionable
{
    void Interection(MonoBehaviour target);
    void Highlight(bool isHighlighted) { }
}
