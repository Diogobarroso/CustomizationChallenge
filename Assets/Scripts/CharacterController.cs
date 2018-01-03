using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterController : MonoBehaviour {

	// Use this for initialization
	private string name;
	private bool edit = false;
	private Animation anim;
	private float interval;
	public float maxInterval = 4f;
	public int activated = 0;
	public List<GameObject> anims; //storing the different animations we have
	public string[] animations = {"Jumping","Looking Around","Yelling"}; //the animations included in the project
	void Start () {
		name = transform.GetComponentInChildren<TextMesh>().text;
		anim = GetComponentInChildren<Animation>();

		LoadAnimations();
	}

	void Awake()
	{
		DontDestroyOnLoad(transform.gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		interval += Time.deltaTime;

		if(interval >= maxInterval)
		{
			anim.Play();
			interval = 0;
		}
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

	void LoadAnimations()
	{
		for(int i = 0 ; i < animations.Length; i++)
		{
			anims.Add(Instantiate(Resources.Load(name + "@" + animations[i])) as GameObject);
			Debug.Log("loaded " + name + " " + animations[i]);
			anims[i].transform.SetParent(transform);
			anims[i].transform.localPosition = new Vector3(0,0,0);
			anims[i].SetActive(false);
		}

		anims[0].SetActive(true);
		anim = anims[0].GetComponentInChildren<Animation>();
	}
}
