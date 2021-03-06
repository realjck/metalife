using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class WorldMenuManager : MonoBehaviour
{
    [SerializeField] private Vector3 worldPosition;
    [SerializeField] private TextMeshProUGUI worldTitleText;
    [SerializeField] private GameObject lightObject;
    private GameObject world;
    // Start is called before the first frame update
    void Start()
    {
        ShowCurrentWorld();

        // apply current sky if available
        if (GameManager.Instance.isWorldRezzed){
            RenderSettings.skybox = GameManager.Instance.skyMaterials[GameManager.Instance.selectedSkyIndex];
            DynamicGI.UpdateEnvironment();
            Vector3 lightPos = GameManager.Instance.lightPositions[GameManager.Instance.selectedSkyIndex];
            if (lightPos == Vector3.zero){
                lightObject.GetComponent<Light>().type = LightType.Point;
                lightObject.GetComponent<Light>().intensity = 1;
            } else {
                lightObject.GetComponent<Light>().type = LightType.Directional;
                lightObject.transform.rotation = Quaternion.Euler(lightPos);
                lightObject.GetComponent<Light>().intensity = 0.3f;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow)){
            ClickLeft();
        } else if (Input.GetKeyDown(KeyCode.RightArrow)){
            ClickRight();
        } else if (Input.GetKeyDown(KeyCode.Space)){
            ClickRez();
        }
    }

    void ShowCurrentWorld(){
        if (world != null){
            Destroy(world);
        }
        world = Instantiate(GameManager.Instance.worlds[GameManager.Instance.selectedWorldIndex]);
        world.transform.position = worldPosition;
        GameObject.Find("Gems").SetActive(false);

        worldTitleText.text = GameManager.Instance.worldTitles[GameManager.Instance.selectedWorldIndex];
    }

    public void ClickLeft(){
        AudioManager.Instance.ClickUISound();
        GameManager.Instance.selectedWorldIndex--;
        if (GameManager.Instance.selectedWorldIndex < 0){
            GameManager.Instance.selectedWorldIndex = GameManager.Instance.worlds.Length -1;
        }
        ShowCurrentWorld();
    }
    public void ClickRight(){
        AudioManager.Instance.ClickUISound();
        GameManager.Instance.selectedWorldIndex++;
        if (GameManager.Instance.selectedWorldIndex > GameManager.Instance.worlds.Length -1){
            GameManager.Instance.selectedWorldIndex = 0;
        }
        ShowCurrentWorld();
    }

    public void ClickRez(){
        AudioManager.Instance.ClickUISound();
        if (GameManager.Instance.isWorldRezzed){
            SceneManager.LoadScene(2);
        } else {
            SceneManager.LoadScene(3);
        }
    }
}
