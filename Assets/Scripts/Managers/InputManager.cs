using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {

    [SerializeField]
    private UIManager uiManager;

    private Player player;

    private KeyCode up, upAlt, upEng;
    private KeyCode left, leftAlt, leftEng;
    private KeyCode right, rightAlt;
    private KeyCode down, downAlt;
    private KeyCode upgradeSelecKey;

    private Vector2 movement;

	// Use this for initialization
	void Awake () {

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        up = KeyCode.Z;
        left = KeyCode.Q;
        right = KeyCode.D;
        down = KeyCode.S;

        upEng = KeyCode.W;
        leftEng = KeyCode.A;

        upAlt = KeyCode.UpArrow;
        leftAlt = KeyCode.LeftArrow;
        rightAlt = KeyCode.RightArrow;
        downAlt = KeyCode.DownArrow;

        upgradeSelecKey = KeyCode.Space;
    }
	
	// Update is called once per frame
	void Update () {

        if (uiManager.IsUpgradePopUpVisible())
        {
            MenuMovement();
        }
        else
        {
            movement = Vector2.zero;

            CheckMovement();

            player.Move(movement);
        }
	}

    private void CheckMovement()
    {
        if(Input.GetKey(up) || Input.GetKey(upAlt) || Input.GetKey(upEng))
        {
            movement.y += 1;
        }

        if (Input.GetKey(down) || Input.GetKey(downAlt))
        {
            movement.y -= 1;
        }

        if (Input.GetKey(left) || Input.GetKey(leftAlt) || Input.GetKey(leftEng))
        {
            movement.x -= 1;
        }

        if (Input.GetKey(right) || Input.GetKey(rightAlt))
        {
            movement.x += 1;
        }

        //Do touchscreen move if time
    }

    private void MenuMovement()
    {
        int selecMenu = 0;
        
        if (Input.GetKeyDown(upgradeSelecKey))
        {
            uiManager.BuyCurrentlySelectedOptionUpgradeMenu();
        }

        if (Input.GetKeyDown(up) || Input.GetKeyDown(upAlt) || Input.GetKeyDown(upEng))
        {
            selecMenu -= 1;
        }

        if (Input.GetKeyDown(down) || Input.GetKeyDown(downAlt))
        {
            selecMenu += 1;
        }

        uiManager.ChangeSelectedOptionUpgradeMenu(selecMenu);
        //Do touchscreen move if time
    }
}
