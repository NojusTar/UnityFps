using System.Collections.Generic;
using UnityEngine;

public class GunInventory : MonoBehaviour
{
    [Header("Gun inventory")]
    [SerializeField]
    public List<Gun> gunArray;
    [SerializeField]
    private Transform gunPlace;
    [SerializeField]
    private GunPlay gunPlay;

    public Gun currentGun { get; private set; }

    
    public GameObject gunVisual;
    private int gunIndex;
    private int lastGunIndex = -1;


    // Start is called before the first frame update
    void Start()
    {
        gunArray.Clear();
        if (LevelVarTransfer.Instance != null)
        {
            gunArray.AddRange(LevelVarTransfer.Instance.staticGunsArray);
        }

        if (gunArray.Count - 1 >= 0) 
        { 
            gunIndex = 0;
            SwitchGuns(true);
        }
            
    }

    // Update is called once per frame
    void Update()
    {
        SwitchGuns(GunSelectInput());

    }

    #region my functions

    private bool GunSelectInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            gunIndex = 0;
            return true;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2)) 
        {
            gunIndex = 1;
            return true;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            gunIndex = 2;
            return true;
        }
        return false;
        
    }

    private void SwitchGuns(bool wasSelected)
    {
        if (!wasSelected) { return; } //jeigu nepasirinko tai iseinam is funkcijos
        
        if (gunArray.Count - 1 >= gunIndex)
        {

            if (lastGunIndex != gunIndex)
            {

                if (gunVisual != null) //jei jau laiko ginkla tai sunaikina
                {
                    Destroy(gunVisual);
                    currentGun = null;
                }

                // pakeicia ginklus
                lastGunIndex = gunIndex;
                gunVisual = Instantiate(gunArray[gunIndex].gunPrefab, gunPlace.transform);
                currentGun = gunArray[gunIndex];
                gunPlay.hasFired = false;

            }

        }

        
    }

    public void PickUpGun(Gun gun)
    {
        gunArray.Add(gun);
    }

    #endregion
}
