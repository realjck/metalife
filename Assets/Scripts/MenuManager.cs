using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private Vector3 characterPosition;
    private GameObject character;
    // Start is called before the first frame update
    void Start()
    {
        ShowCurrentCharacter();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow)){
            ClickLeft();
        } else if (Input.GetKeyDown(KeyCode.RightArrow)){
            ClickRight();
        }
    }

    void ShowCurrentCharacter(){
        if (character != null){
            Destroy(character);
        }
        character = Instantiate(GameManager.Instance.characters[GameManager.Instance.selectedCharacterIndex]);
        character.transform.position = characterPosition;
    }

    public void ClickLeft(){
        GameManager.Instance.selectedCharacterIndex--;
        if (GameManager.Instance.selectedCharacterIndex < 0){
            GameManager.Instance.selectedCharacterIndex = GameManager.Instance.characters.Length -1;
        }
        ShowCurrentCharacter();
    }
    public void ClickRight(){
        GameManager.Instance.selectedCharacterIndex++;
        if (GameManager.Instance.selectedCharacterIndex > GameManager.Instance.characters.Length -1){
            GameManager.Instance.selectedCharacterIndex = 0;
        }
        ShowCurrentCharacter();
    }

    public void ClickRez(){
        SceneManager.LoadScene(1);
    }
}