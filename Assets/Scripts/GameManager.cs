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
	public InputField newNameField;
	
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
		showInput();
	}

	public void EditAnim()
	{
		Button leftButton = (Button)Instantiate(leftButtonPrefab);
		Button rightButton = (Button)Instantiate(rightButtonPrefab);
		leftButton.transform.SetParent(Canvas.transform,false);
		rightButton.transform.SetParent(Canvas.transform,false);

		leftButton.onClick.AddListener(LeftArrow);
		rightButton.onClick.AddListener(RightArrow);
		Debug.Log("AFTER BUTTONS");
	}

	public void LeftArrow() //select the previous element
	{
		controller.anims[controller.activated].SetActive(false);
		if(controller.activated == 0)
			controller.activated = controller.anims.Count - 1;
		else
			controller.activated -= 1;
		
		controller.anims[controller.activated].SetActive(true);
	}

	public void RightArrow() //select the next element
	{
		controller.anims[controller.activated].SetActive(false);
		if(controller.activated == controller.anims.Count - 1)
			controller.activated = 0;
		else
			controller.activated += 1;
		
		controller.anims[controller.activated].SetActive(true);
	}

	public void showInput()
	{
		newNameField.gameObject.SetActive(true);
		newNameField.onEndEdit.AddListener(delegate{changeName(newNameField);});
	}

	public void changeName(InputField newNameField)
	{
		string newName = newNameField.GetComponentInChildren<Text>().text;
		newNameField.GetComponentInChildren<Text>().text = "";
		newNameField.gameObject.SetActive(false);
		controller.changeName(newName);
	}
}