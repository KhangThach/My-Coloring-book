using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementGalleryGroup : MonoBehaviour
{
    [SerializeField] private DataPicture dataPicture;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Setup(DataPicture _dataPicture)
    {
        dataPicture = _dataPicture;
    }
}
