using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	GameObject currentModel; //The model we're editing
	public GameObject Canvas;
	public Button rightButtonPrefab;
	public Button leftButtonPrefab;
	CharacterController controller; //the controller of the character we're editing
	void Start ()
	{
		GameObject[] characters = GameObject.FindGameObjectsWithTag("Player");
		foreach(GameObject character in characters)
		{
			if(character.GetComponent<CharacterController>().getEdit()) //check if this is the model we want to edit
			{
				currentModel = character;
				currentModel.transform.position = new Vector3(0,0,0); //place the model in the origin
			}
			else
				character.SetActive(false);
		}
		controller = currentModel.GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	public void BackButton()
	{
		currentModel.GetComponent<CharacterController>().setEdit(false);
		SceneManager.LoadScene(0);
	}

	public void EditName()
	{
		/*string newName = "";
		InputField newNameField = 
		newNameField.text = "New name";
		newNameField.transform.position = new Vector3(0,0,0);
		*/
	}

	public void EditAnim()
	{
		//TODO: edit the animation of the model
		Button leftButton = (Button)Instantiate(leftButtonPrefab);
		Button rightButton = (Button)Instantiate(rightButtonPrefab);
		leftButton.transform.SetParent(Canvas.transform,false);
		rightButton.transform.SetParent(Canvas.transform,false);

		leftButton.onClick.AddListener(LeftArrow);
		rightButton.onClick.AddListener(RightArrow);
		Debug.Log("AFTER BUTTONS");
	}

	public void LeftArrow()
	{
		controller.anims[controller.activated].SetActive(false);
		if(controller.activated == 0)
			controller.activated = controller.anims.Count - 1;
		else
			controller.activated -= 1;
		
		controller.anims[controller.activated].SetActive(true);
	}

	public void RightArrow()
	{
		controller.anims[controller.activated].SetActive(false);
		if(controller.activated == controller.anims.Count - 1)
			controller.activated = 0;
		else
			controller.activated += 1;
		
		controller.anims[controller.activated].SetActive(true);
	}
}