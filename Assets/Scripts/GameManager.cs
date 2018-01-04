using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	GameObject[] characters; //all characters in the party
	GameObject currentModel; //The model we're editing
	CharacterController controller; //the controller of the currentModel

	//These 3 variables store the characteristics that are passive of change in the Customization, in case the player discards the modifications
	int previousActivated;
	string previousName;
	float previousHeight;

	public GameObject Canvas;
	public Button rightButtonPrefab;
	public Button leftButtonPrefab;
	public InputField newNameField;
	public Slider heightSlider;
	
	void Start ()
	{
		characters = GameObject.FindGameObjectsWithTag("Player");
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

		previousActivated = controller.activated;
		previousName = currentModel.GetComponentInChildren<TextMesh>().text;
		previousHeight = controller.height;
	}

	public void ConfirmButton()
	{
		LoadScene();
	}

	public void CancelButton()
	{
		controller.anims[controller.activated].SetActive(false);
		controller.activated = previousActivated;	
		controller.anims[controller.activated].SetActive(true);
		currentModel.GetComponentInChildren<TextMesh>().text = previousName;
		controller.changeHeight(previousHeight);

		LoadScene();
	}

	//Prepares to go back to the Display Scene
	void LoadScene()
	{
		controller.setEdit(false);
		foreach(GameObject cha in characters) //not checking which one is the currentModel, easier to enable both, not much processing time lost
		{
			cha.SetActive(true);
			cha.transform.localPosition = cha.GetComponent<CharacterController>().position;
		}
		SceneManager.LoadScene(0);
	}

	public void EditName()
	{
		showInput();
	}

	//Prepares to select the animations
	public void EditAnim()
	{
		Button leftButton = (Button)Instantiate(leftButtonPrefab);
		Button rightButton = (Button)Instantiate(rightButtonPrefab);
		leftButton.transform.SetParent(Canvas.transform,false);
		rightButton.transform.SetParent(Canvas.transform,false);

		leftButton.onClick.AddListener(LeftAnim);
		rightButton.onClick.AddListener(RightAnim);
	}

	//select the previous animation
	public void LeftAnim() 
	{
		controller.anims[controller.activated].SetActive(false);
		if(controller.activated == 0)
			controller.activated = controller.anims.Count - 1;
		else
			controller.activated -= 1;
		
		controller.anims[controller.activated].SetActive(true);
	}
 
 	//select the next animation
	public void RightAnim()
	{
		controller.anims[controller.activated].SetActive(false);
		if(controller.activated == controller.anims.Count - 1)
			controller.activated = 0;
		else
			controller.activated += 1;
		
		controller.anims[controller.activated].SetActive(true);
	}

	//Shows the InputField in which the User chooses the new name
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

	//Shows the Height slider
	public void showHeight()
	{
			heightSlider.gameObject.SetActive(!heightSlider.IsActive());
			heightSlider.value = previousHeight;
	}

	public void changeHeight(Slider slider)
	{
		controller.changeHeight(slider.value);
	}
}