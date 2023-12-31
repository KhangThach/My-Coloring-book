using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyworkGroupManager : MonoBehaviour
{
    [SerializeField] private GameObject prefabElementGroup;

    [SerializeField] private List<ElementMyworkGroup> elementMyworkGroups;

    [SerializeField] private Transform parentElement;

    [SerializeField] private GameObject canvasRender;

    private bool isShow;

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Show(bool isTrue)
    {
        canvasRender.SetActive(isTrue);

        if (isTrue)
        {
            Debug.Log("Khang");


        }

        if (isShow != isTrue)
        {
            isShow = isTrue;
        }


    }

    public void Init()
    {
        List<DataPicture> _dataPictures = GameManager.Instance.DataManager.GetSaveDataPicture();



        for(int i = 0; i < elementMyworkGroups.Count; i++)
        {
            Destroy(elementMyworkGroups[i].gameObject);
        }

        elementMyworkGroups = new List<ElementMyworkGroup>();


        for (int i = 0; i < _dataPictures.Count; i++)
        {
            GameObject objElement = Instantiate<GameObject>(prefabElementGroup, parentElement);

            ElementMyworkGroup elementMyworkGroup = objElement.GetComponent<ElementMyworkGroup>();

            elementMyworkGroup.Setup(_dataPictures[i]);

            elementMyworkGroups.Add(elementMyworkGroup);
        }

        for (int i = 0; i < elementMyworkGroups.Count; i++)
        {
            elementMyworkGroups[i].Init();
        }
    }

    public void Setup(List<DataPicture> _dataPictures)
    {

    }
}
