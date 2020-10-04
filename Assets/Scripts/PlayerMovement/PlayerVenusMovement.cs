using Bolt;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerVenusMovement : MonoBehaviour, IPlayerPlanetMovement
{
    private float _movementSpeed = 0.2F;

    public void PlayerSetup(GameObject rootGameObject)
    {
        rootGameObject.GetComponentInChildren<Camera>().orthographicSize = 5;
        rootGameObject.GetComponent<Rigidbody2D>().gravityScale = 1.0f;
    }

    private int _movingToNextPlanet = 0;

    SpriteRenderer _spriteRenderer;
    Transform _teleportTransform;

    public void PlayerUpdate(Rigidbody2D playerBody)
    {
        //Debug.Log("Venus Movement");

        if (_movingToNextPlanet == 0)
        {
            // move left
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                gameObject.transform.RotateAround(gameObject.transform.position, Vector3.forward, _movementSpeed * -1F);
            }

            if (Input.GetKey(KeyCode.RightArrow))
            {
                gameObject.transform.RotateAround(gameObject.transform.position, Vector3.forward, _movementSpeed);
            }

            if (Input.GetKeyDown(KeyCode.Space) && playerBody.GetComponent<Player>().isGrounded)
            {
                playerBody.AddRelativeForce(new Vector2(0.0F, 7.0F), ForceMode2D.Impulse);
            }

            if (playerBody.GetComponent<Player>().isButtonPressed)
            {
                _movingToNextPlanet = 1;
            }
        }
        else if (_movingToNextPlanet == 1)
        {
            _spriteRenderer = playerBody.transform.Find("Sprite").GetComponent<SpriteRenderer>();
            _teleportTransform = playerBody.transform.Find("TeleportAnimation");
            _teleportTransform.gameObject.SetActive(true);

            _teleportTransform.GetComponent<Animator>().SetTrigger("startTP");

            _movingToNextPlanet = 2;
        }
        else if (_movingToNextPlanet == 2)
        {
            // The integer part is the number of time a state has been looped. The fractional part is the % (0-1) of progress in the current loop.
            float animationPercentage = _teleportTransform.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime;
            if (animationPercentage < 1)
            {
                _spriteRenderer.color -= new Color(0, 0, 0, 0.3F * Time.deltaTime);
            }
            else
            {
                GlobalInformation.currentScene++;
                Variables.Application.Set("laserTransition", true);
                SceneManager.LoadScene("Transition", LoadSceneMode.Single);
            }
        }
    }
}
