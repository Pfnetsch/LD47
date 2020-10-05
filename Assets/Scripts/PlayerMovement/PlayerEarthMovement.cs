using Bolt;
using MiscUtil.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEarthMovement : MonoBehaviour, IPlayerPlanetMovement
{
    private float _movementSpeed = 0.1F;
    private bool _isTeleportComplete = false;
    private bool _isRocketSpawned = false;

    SpriteRenderer _spriteRenderer;
    Transform _teleportTransform;

    Transform _BaikonurRocket;
    Transform _BaikonurText;

    public void PlayerSetup(GameObject rootGameObject)
    {
        GlobalInformation.currentCollectibles = 0;

        Variables.Application.Set("laserTransition", false);

        _BaikonurText = transform.Find("Baikonur").Find("BaikonurText");
        _BaikonurRocket = transform.Find("Baikonur").Find("Rocket");

        //setup
        rootGameObject.GetComponentInChildren<Camera>().orthographicSize = 5;
        rootGameObject.GetComponent<Rigidbody2D>().gravityScale = 1.0f;

        Transform laserTrans = rootGameObject.transform.Find("LaserTransition");
        laserTrans.gameObject.SetActive(false);

        Transform spriteTrans = rootGameObject.transform.Find("Sprite");
        spriteTrans.gameObject.SetActive(true);
        _spriteRenderer = spriteTrans.GetComponent<SpriteRenderer>(); 
        _spriteRenderer.color = new Color(_spriteRenderer.color.r, _spriteRenderer.color.g, _spriteRenderer.color.b, 0F);   // Invisible at the beginning

        _teleportTransform = rootGameObject.transform.Find("TeleportAnimation");
        _teleportTransform.gameObject.SetActive(true);

        _teleportTransform.GetComponent<Animator>().SetTrigger("startTP");
    }

    public void PlayerUpdate(Rigidbody2D playerBody)
    {
        //Debug.Log("Earth Movement");

        if (!_isTeleportComplete)
        {
            float animationPercentage = _teleportTransform.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime;
            if (animationPercentage < 1)
            {
                _spriteRenderer.color += new Color(0, 0, 0, 0.3F * Time.deltaTime);
            }
            else
            {
                _isTeleportComplete = true;
                _teleportTransform.gameObject.SetActive(false);
                _spriteRenderer.color = new Color(_spriteRenderer.color.r, _spriteRenderer.color.g, _spriteRenderer.color.b, 1F);   // Invisible at the beginning
            }
        }
        else
        {
            if (playerBody.GetComponent<Player>().isUnderWater)
            {
                if (Input.GetKey(KeyCode.LeftArrow))
                {
                    playerBody.transform.Rotate(new Vector3(0F, 0F, 0.2F));
                }
                if (Input.GetKey(KeyCode.RightArrow))
                {
                    playerBody.transform.Rotate(new Vector3(0F, 0F, -0.2F));
                }
                if (Input.GetKey(KeyCode.UpArrow))
                {
                    playerBody.AddRelativeForce(new Vector2(0.0F, 0.8F), ForceMode2D.Force);
                }
                if (Input.GetKey(KeyCode.DownArrow))
                {
                    playerBody.AddRelativeForce(new Vector2(0.0F, -0.8F), ForceMode2D.Force);
                }
            }
            else
            {
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
                    playerBody.AddRelativeForce(new Vector2(0.0F, 5.0F), ForceMode2D.Impulse);
                }
            }

            if (!_isRocketSpawned && playerBody.GetComponent<Player>().isAtLocation)
            {
                _BaikonurText.gameObject.SetActive(true);

                if (GlobalInformation.currentCollectibles == 3)
                {
                    _BaikonurRocket.gameObject.SetActive(true);
                    _isRocketSpawned = true;
                }
            }
            else
                _BaikonurText.gameObject.SetActive(false);

            if (_isRocketSpawned && playerBody.GetComponent<Player>().isAtRocket)
            {
                playerBody.transform.position = _BaikonurRocket.position;
                //playerBody.transform.Find("Sprite").gameObject.SetActive(false);
                playerBody.transform.Find("RocketSprite").gameObject.SetActive(true);
                _BaikonurRocket.gameObject.SetActive(false);
            }
        }
    }
}
