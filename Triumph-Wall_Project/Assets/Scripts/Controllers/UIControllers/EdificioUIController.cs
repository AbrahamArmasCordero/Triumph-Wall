﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Sirenix.OdinInspector;
using UnityEngine.Events;
using UIDataTypes.Buildings;

public class EdificioUIController : MonoBehaviour
{

	[SceneObjectsOnly] [FoldoutGroup( "Common UI" )]
	public GameObject canvas;
	[SceneObjectsOnly][FoldoutGroup( "Common UI" )]
	public GameObject nameTextObj;
	[SceneObjectsOnly][FoldoutGroup( "Common UI" )]
	public TextMeshProUGUI nameText;
	[FoldoutGroup( "Common UI/Durability" )]
	[SceneObjectsOnly]
	public GameObject durabilityObj;
	[FoldoutGroup( "Common UI/Durability" )]
	[SceneObjectsOnly]
	public Slider durabilitySlider;

	[FoldoutGroup( "Common UI/Upgrades" )]
	[SceneObjectsOnly]
	public GameObject upgradesObject;
	[SceneObjectsOnly]
	[FoldoutGroup( "Common UI/Upgrades" )]
	public TextMeshProUGUI upgradesText;

	[FoldoutGroup( "Common UI/Process" )]
	[SceneObjectsOnly]
	public GameObject progresObj;
	[FoldoutGroup( "Common UI/Process" )]
	[SceneObjectsOnly]
	public Slider progresSlider;
	[SceneObjectsOnly]
	[FoldoutGroup( "Common UI/Process" )]
	public GameObject speedObject;
	[SceneObjectsOnly]
	[FoldoutGroup( "Common UI/Process" )]
	public TextMeshProUGUI speedText;

	[FoldoutGroup( "Common UI/Employees" )]
	[SceneObjectsOnly]
	public GameObject emplyeeObject;
	[FoldoutGroup( "Common UI/Employees" )]
	[SceneObjectsOnly]
	public Image emplyeeImg;
	[SceneObjectsOnly]
	[FoldoutGroup( "Common UI/Employees" )]
	public TextMeshProUGUI employeeText;

	[FoldoutGroup( "Common UI/Inmigrants" )]
	[SceneObjectsOnly]
	public GameObject inmigrantsObject;
	[SceneObjectsOnly]
	[FoldoutGroup( "Common UI/Inmigrants" )]
	public TextMeshProUGUI inmigrantsText;

	[FoldoutGroup( "Common UI/Buttons" )]
	public GameObject buttonWrapperObj;
	[FoldoutGroup( "Common UI/Buttons" )]
	public GameObject upgradeBtnObj;
	[FoldoutGroup( "Common UI/Buttons" )]
	public Button upgradeBtn;
	[FoldoutGroup( "Common UI/Buttons" )]
	public GameObject repairBtnObj;
	[FoldoutGroup( "Common UI/Buttons" )]
	public Button repairBtn;
	[FoldoutGroup( "Common UI/Buttons" )]
	public GameObject buyBtnObj;
	[FoldoutGroup( "Common UI/Buttons" )]
	public Button buyBtn;
	[FoldoutGroup( "Common UI/Buttons" )]
	public GameObject fireBtnObj;
	[FoldoutGroup( "Common UI/Buttons" )]
	public Button fireBtn;
	
	[FoldoutGroup( "NOTCommon UI")]
	[FoldoutGroup( "NOTCommon UI/Parcela" )]
	[SceneObjectsOnly]
	public GameObject salubrityObj;
	[FoldoutGroup( "NOTCommon UI/Parcela" )]
	[SceneObjectsOnly]
	public Slider salubritySlider;
	[SceneObjectsOnly]
	[FoldoutGroup( "NOTCommon UI/Parcela" )]
	public GameObject controlObj;
	[SceneObjectsOnly]
	[FoldoutGroup( "NOTCommon UI/Parcela" )]
	public Slider controlSlider;

	[FoldoutGroup( "NOTCommon UI/Cocina" )]
	[SceneObjectsOnly][ShowInInspector]
	public GameObject foodAmountObj;
	[FoldoutGroup( "NOTCommon UI/Cocina" )]
	[SceneObjectsOnly][ShowInInspector]
	public TextMeshProUGUI foodAmountText;
	[FoldoutGroup( "NOTCommon UI/Cocina" )]
	[SceneObjectsOnly][ShowInInspector]
	public TMP_Dropdown portionDropDown;

	private UnityEvent hideEvent = new UnityEvent();
	private CentroDeRetencion crFacility = null;

	public void SetUp ( )
	{
		crFacility = FindObjectOfType<CentroDeRetencion>();
	}

	//Ui Basica de TODOS los edificios
	//tiene en cuenta las booleanas de UIB_Data para enseñar diferentes elementos
	public void ShowBaseUI (UIB_Data baseData)
	{
		nameText.text = baseData.name;
		nameTextObj.SetActive( true );
		if (baseData.showDurabilityBar)
		{
			durabilityObj.SetActive( true );
			durabilitySlider.value = baseData.currentDurability / baseData.maxDurability;
		}
		else
		{
			durabilityObj.SetActive( false );
		}

		if (baseData.showUpgradeNum)
		{
			upgradesObject.SetActive( true );
			upgradesText.text = string.Format( "{0:00}/{1:00}", baseData.currentUpgrade, baseData.maxOfUpgrades );
		}
		else
		{
			upgradesObject.SetActive( false );
		}

		if (baseData.showEmployeeNum)
		{
			emplyeeImg.sprite = baseData.employeeSprite;
			emplyeeObject.SetActive( true );
			employeeText.text = string.Format( "{0:00}/{1:00}", baseData.currentEmployeeNum, baseData.maxEmployeeNum );
		}
		else
		{
			emplyeeObject.SetActive( false );
		}

		if (baseData.showInmigrantNum)
		{
			inmigrantsObject.SetActive( true );
			inmigrantsText.text = string.Format( "{0:00}/{1:00}", baseData.currentInmigrantNum, baseData.maxInmigrantNum );
		}
		else
		{
			inmigrantsObject.SetActive( false );
		}

		if (baseData.showProgress)
		{
			speedObject.SetActive( true );
			progresObj.SetActive( true );
			progresSlider.value = baseData.currentProgress;
			speedText.text = string.Format( "{0:00}", baseData.processSpeed );
		}
		else
		{
			speedObject.SetActive( false );
			progresObj.SetActive( false );
		}

		if (baseData.showUpgradeBtn ||baseData.showRepairBtn ||baseData.showFireEmployeeBtn || baseData.showBuyEmployeeBtn)
		{
			if (baseData.showUpgradeBtn)
			{
				upgradeBtn.onClick.RemoveAllListeners();
				upgradeBtn.onClick.AddListener(delegate {
					crFacility.DoBuildingAction( Edificio.B_Actions.UPGRADE, baseData.managerID );
				} );
				upgradeBtnObj.SetActive( true );
			}
			else
			{
				upgradeBtnObj.SetActive( false );
			}

			if (baseData.showRepairBtn)
			{
				repairBtn.onClick.RemoveAllListeners();
				repairBtn.onClick.AddListener(delegate {
					crFacility.DoBuildingAction( Edificio.B_Actions.REPAIR, baseData.managerID );
				} );
				repairBtnObj.SetActive( true );
			}
			else
			{
				repairBtnObj.SetActive( false );
			}

			if (baseData.showBuyEmployeeBtn)
			{
				buyBtn.onClick.RemoveAllListeners();
				buyBtn.onClick.AddListener(delegate {
					crFacility.DoBuildingAction( Edificio.B_Actions.BUY_EMPLOYEE, baseData.managerID );
				} );
				buyBtnObj.SetActive( true );
			}
			else
			{
				buyBtnObj.SetActive( false );
			}

			if (baseData.showFireEmployeeBtn)
			{
				fireBtn.onClick.RemoveAllListeners();
				fireBtn.onClick.AddListener(delegate {
					crFacility.DoBuildingAction( Edificio.B_Actions.FIRE_EMPLOYEE, baseData.managerID );
				} );
				fireBtnObj.SetActive( true );
			}
			else
			{
				fireBtnObj.SetActive( false );
			}
			buttonWrapperObj.SetActive( true );
		}
		else
		{
			buttonWrapperObj.SetActive( false );
		}
	}

	//SetUp de la UI de edificio concreto
	public void StartShowUI(SO_UICR_Data data)
	{
		ShowBaseUI( data );
		//enseñar Ui especifica
		salubrityObj.SetActive( true );
		salubritySlider.value = data.salubrity;

		controlObj.SetActive( true );
		controlSlider.value = data.control;

		//eventos de actualizaciond e la UI
		data.updatedValuesEvent.RemoveAllListeners();
		UnityAction action = delegate { UpdateUI( data ); };
		data.updatedValuesEvent.AddListener( action );
		//evento  al esconderse esta UI
		UnityAction action2 = delegate 
		{
			data.updatedValuesEvent.RemoveListener( action );
			salubrityObj.SetActive( false );
			controlObj.SetActive( false );
		};
		hideEvent.AddListener( action2 );
		hideEvent.AddListener( delegate { hideEvent.RemoveListener( action2 ); } );
	}
	private void UpdateUI (SO_UICR_Data data)
	{
		ShowBaseUI( data );
		//enseñar Ui especifica
		salubritySlider.value = data.salubrity;
		controlSlider.value = data.control;
	}

	public void StartShowUI(SO_UICocina_Data data)
	{
		ShowBaseUI( data );
		foodAmountText.text = data.foodStorage.ToString();
		foodAmountObj.SetActive( true );
		portionDropDown.value = data.currentPortion;
		portionDropDown.RefreshShownValue();
		portionDropDown.onValueChanged.AddListener( data.notifyDorpdownChange.Invoke );
		portionDropDown.gameObject.SetActive( true );

		//eventos de actualizaciond e la UI
		//data.updatedValuesEvent.RemoveAllListeners();
		UnityAction action = delegate { UpdateUI( data ); };
		data.updatedValuesEvent.AddListener( action );
		//evento  al esconderse esta UI
		UnityAction action2 = delegate
		{
			foodAmountObj.SetActive( false );
			portionDropDown.gameObject.SetActive( false );
			portionDropDown.onValueChanged.RemoveAllListeners();
			data.updatedValuesEvent.RemoveListener( action );
		};
		hideEvent.AddListener( action2 );
		hideEvent.AddListener( delegate { hideEvent.RemoveListener( action2 ); } );
	}
	private void UpdateUI (SO_UICocina_Data data)
	{
		ShowBaseUI( data );
		//enseñar Ui especifica
		if (data.alertFood)
		{
			foodAmountText.color = Color.red;
		}
		else
		{
			foodAmountText.color = Color.white;
		}
		foodAmountText.text = data.foodStorage.ToString();
	}

	public void StartShowUI(SO_UIDorm_Data data)
    {
        ShowBaseUI(data);
        //enseñar Ui especifica

        //eventos de actualizaciond e la UI
        UnityAction action = delegate { UpdateUI(data); };
        data.updatedValuesEvent.AddListener(action);
        //evento  al esconderse esta UI
        UnityAction action2 = delegate
        {
            data.updatedValuesEvent.RemoveListener(action);

        };
        hideEvent.AddListener(action2);
        hideEvent.AddListener(delegate { hideEvent.RemoveListener(action2); });
    }
	private void UpdateUI (SO_UIDorm_Data data)
	{
		ShowBaseUI( data );
		//enseñar Ui especifica
	}

	public void StartShowUI(SO_UIODI_Data data)
    {
        ShowBaseUI(data);
		//enseñar Ui especifica

		//eventos de actualizaciond e la UI
		data.updatedValuesEvent.RemoveAllListeners();
		UnityAction action = delegate { UpdateUI(data); };
        data.updatedValuesEvent.AddListener(action);
        //evento  al esconderse esta UI
        UnityAction action2 = delegate
        {
            data.updatedValuesEvent.RemoveListener(action);
            
        };
        hideEvent.AddListener(action2);
        hideEvent.AddListener(delegate { hideEvent.RemoveListener(action2); });
    }
	private void UpdateUI (SO_UIODI_Data data)
	{
		ShowBaseUI( data );
		//enseñar Ui especifica

	}

	public void StartShowUI(SO_UIENF_Data data)
    {
        ShowBaseUI(data);
        //enseñar Ui especifica

        //eventos de actualizaciond e la UI
        UnityAction action = delegate { UpdateUI(data); };
        data.updatedValuesEvent.AddListener(action);
        //evento  al esconderse esta UI
        UnityAction action2 = delegate
        {
            data.updatedValuesEvent.RemoveListener(action);

        };
        hideEvent.AddListener(action2);
        hideEvent.AddListener(delegate { hideEvent.RemoveListener(action2); });
    }
	private void UpdateUI (SO_UIENF_Data data)
	{
		ShowBaseUI( data );
		//enseñar Ui especifica
	}
    //se llama desde el input controller
    public void Hide ( )
	{
		canvas.SetActive( false );
		hideEvent.Invoke();
	}
}
