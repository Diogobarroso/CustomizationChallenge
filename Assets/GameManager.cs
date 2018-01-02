using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	GameObject currentModel; //The model we're editing
	// Use this for initialization
	void Start ()
	{
		GameObject[] characters = GameObject.FindGameObjectsWithTag("Player");
		foreach(GameObject character in characters)
		{
			if(character.GetComponent<CharacterController>().getEdit()) //check if this is the model we want to edit
			{
				currentModel = character;
				currentModel.transform.position = new Vector3(0,0,0);//place the model in the origin
			}
			else
				character.SetActive(false);
		}
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
		string newName = "";
		newName = GUI.TextField(new Rect(10, 10, 200, 20), newName, 25);
	}

	public void EditAnim()
	{
		//TODO: edit the animation of the model
	}
}