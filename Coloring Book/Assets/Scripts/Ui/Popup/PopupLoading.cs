using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PopupLoading : UiCanvas
{
    [SerializeField] private Image imgFill;

    [SerializeField] private float timeFill;

    [SerializeField] private Transform parentInstance;

    [SerializeField] private PictureManager pictureManager;

    [SerializeField] private float indexScale;

    private Action actionDone;

    // Start is called before the first frame update
    protected override void Start()
    {
        actionDone += () => { Show(false); AdsManager.Instance.RefreshBanner(); };    
    }

    public override void Show(bool _isShow)
    {
        if (_isShow)
        {
            if (pictureManager != null)
            {
                Destroy(pictureManager.gameObject);
            }

            imgFill.fillAmount = 0;

            TypeGallery typeGallery = LevelManager.Instance.GamePlayManager.GetCurrentTypeGallery();

            TypeId typeId = LevelManager.Instance.GamePlayManager.GetCurrentTypeId();

            DataPicture dataPicture = GameManager.Instance.DataManager.GetDataPicture(typeGallery, typeId);

            GameObject objInstance = Instantiate<GameObject>(dataPicture.prefabUiPicture, parentInstance);

            objInstance.transform.localScale = objInstance.transform.localScale * indexScale;

            pictureManager = objInstance.GetComponent<PictureManager>();

            pictureManager.Init(typeGallery, typeId);
        }



        base.Show(_isShow);

        if (_isShow)
        {
            imgFill.DOFillAmount(1, timeFill).OnComplete(() =>
            {
                actionDone?.Invoke();
            });
        }
    }
}