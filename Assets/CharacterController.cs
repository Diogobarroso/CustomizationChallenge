using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterController : MonoBehaviour {

	// Use this for initialization
	private string name;
	private bool edit = false;
	void Start () {
		name = transform.GetComponentInChildren<TextMesh>().text;
	}

	void Awake()
	{
		DontDestroyOnLoad(transform.gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void changeName(string newName)
	{
		name = newName;
	}

	public void OnMouseDown()
	{
		if(SceneManager.GetActiveScene().buildIndex == 0) //if this is the display scene
		{
			edit = true;
			SceneManager.LoadScene(1);
		}
	}

	public bool getEdit()
	{
		return edit;
	}

	public void setEdit(bool state)
	{
		edit = state;
	}
}
