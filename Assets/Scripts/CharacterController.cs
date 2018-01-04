using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class CharacterController : MonoBehaviour {
	public Vector3 position;
	public float height = 1;
	private Vector3 originalScale;
	public string name;
	private bool edit = false; //True when this is the character we're going to customize
	private float interval; //The time that has passed since the last animation
	public float maxInterval = 4f; //The time between 2 animations (the loop)
	public List<GameObject> anims; //storing the different animations we have
	public int activated = 0; //The index of the animation being displayed (from anims)
	public string[] animations = {"Jumping","Looking Around","Yelling"}; //the animations included in the project
	void Start () {

		LoadAnimations();
	}

	void Awake()
	{
		string tag = gameObject.tag;
		if(GameObject.FindGameObjectsWithTag(tag).Length <= 2) //if there are no more 2 objects with my tag (myself and the other party member)
			DontDestroyOnLoad(gameObject);
		else
			Destroy(gameObject);

		if(gameObject.name == "Remy")
		{
			position = new Vector3(3,0,0);
			name = "Remy";
		}
		else if (gameObject.name == "Malcolm")
		{
			position = new Vector3(-1,0,0);
			name = "Malcolm";
		}

		transform.localPosition = position;
		originalScale = transform.localScale;
	}
	
	// Update is called once per frame
	void Update () {
		interval += Time.deltaTime;

		if(interval >= maxInterval)
		{
			Debug.Log(anims.Count);
			anims[activated].GetComponent<Animation>().Play();
			interval = 0;
		}
	}

	//When the user taps one of the characters
	public void OnMouseDown()
	{
		if(SceneManager.GetActiveScene().buildIndex == 0) //if this is the display scene
		{
			edit = true;
			SceneManager.LoadScene(1);
		}
	}

	//Is this the character we're editing?
	public bool getEdit()
	{
		return edit;
	}

	public void setEdit(bool state)
	{
		edit = state;
	}

	public void LoadAnimations()
	{
		anims = new List<GameObject>(3);
		foreach(GameObject tmpChar in GameObject.FindGameObjectsWithTag(name))
		{
			anims.Add(tmpChar);
			tmpChar.SetActive(false); //Disable all the animations
		}
		anims[activated].SetActive(true); //We only need one animation at a time
	}

	public void changeName(string newName)
	{
		gameObject.GetComponentInChildren<TextMesh>().text = newName;
	}

	public void changeHeight(float newHeight)
	{
		transform.localScale = newHeight * originalScale;
		height = newHeight;
	}
}
