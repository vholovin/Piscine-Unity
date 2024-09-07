using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Sprites;

	public class TowerInstantiateUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
	{
		private GameObject BridgeAPI;

		public GameObject Tower;
	    [HideInInspector] public bool TowerStatus = false;
		
		public GameObject Drop;

		public Text TextDamage;
		public Text TextEnergy;
		public Text TextRange;
		public Text TextWait;

		private void Awake()
		{
			BridgeAPI = GameObject.Find("BridgeAPI");
		}

	    private void Start()
		{
			Drop.SetActive(false);
		}

		private void Update()
		{
			UpdateInfo();
			SetStatus();
		}

		private void UpdateInfo()
		{
			TextDamage.text = Tower.GetComponent<towerScript>().damage.ToString();
			TextEnergy.text = Tower.GetComponent<towerScript>().energy.ToString();
			TextRange.text = Tower.GetComponent<towerScript>().range.ToString();
			TextWait.text = Tower.GetComponent<towerScript>().fireRate.ToString();
		}

		private void SetStatus()
		{
			if (BridgeAPI.GetComponent<BridgeAPI>().GetEnergy() < Tower.GetComponent<towerScript>().energy)
			{
				TowerStatus = false;
				GetComponent<Image> ().color = Color.gray;
			}
			else
			{
				TowerStatus = true;
	            GetComponent<Image> ().color = Color.white;
			}
		}

		public void OnBeginDrag(PointerEventData eventData)
		{
			if (TowerStatus)
			{
	            Drop.SetActive(true);
		    }
		}

		public void OnDrag(PointerEventData eventData)
		{
			if (TowerStatus)
			{
				Drop.transform.position = Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
            }
		}

		public void OnEndDrag(PointerEventData eventData)
		{
			if (TowerStatus)
			{
				if (Drop.activeSelf == true)
				{
	                Drop.SetActive(false);
		            
					RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
					if (hit && hit.collider.transform.tag == "empty")
					{
						BridgeAPI.GetComponent<BridgeAPI>().SetEnergy(-1 * Tower.GetComponent<towerScript>().energy);
						Instantiate(Tower, hit.collider.gameObject.transform.position, Quaternion.identity);
					}
					else
					{
						Debug.Log("space is not empty");
					}
				}
			}
		}
	}