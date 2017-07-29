using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;      

public class Player : MovingObject
{
	public int totalAmmo;
	public int totalDamage;
	public int totalHealth;

	private Animator animator;
	private int currentAmmo;
	private int currentDamage;
	private int currentHealth;

	protected override void Start ()
	{
		animator = GetComponent<Animator>();
		base.Start ();
	}
		
	private void OnDisable ()
	{
		
	}

	private void Update ()
	{
		int horizontal = (int) (Input.GetAxisRaw ("Horizontal"));
		int vertical = (int) (Input.GetAxisRaw ("Vertical"));

		if(horizontal != 0)
			vertical = 0;
			
		if(horizontal != 0 || vertical != 0)
			AttemptMove<Wall> (horizontal, vertical); // TO DO : wall class
	}

	protected override void AttemptMove <T> (int xDir, int yDir)
	{
		base.AttemptMove <T> (xDir, yDir);

		RaycastHit2D hit;

		if (Move (xDir, yDir, out hit)) 
		{
			// TO DO : audio
		}
			
		CheckIfGameOver ();
	}
		
	protected override void OnCantMove <T> (T component)
	{
		
	}
		
	private void OnTriggerEnter2D (Collider2D other)
	{
		if(other.tag == "Exit")
			ChangeLevel();
		else if(other.tag == "stuff")
		{
			// TO DO : item
		}
	}
		
	private void ChangeLevel ()
	{
		SceneManager.LoadScene (0);
	}
		
	private void CheckIfGameOver ()
	{
		if (currentHealth <= 0) 
		{
			GameManager.instance.GameOver (); // TO DO : GameManager class
		}
	}
}