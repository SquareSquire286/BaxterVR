using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class WorldScaleModule : MonoBehaviour
{
    public GameObject scalingAnchor, player, console, baxterModel;
    public AudioClip human, giant, dog;
    private float normalScalar, giantScalar, dogScalar, fleaScalar;
    private Vector3 consoleBaseScale;

    // Start is called before the first frame update
    void Start()
    {
        normalScalar = 1.0f;
        giantScalar = 0.333f;
        dogScalar = 2.5f;
        fleaScalar = 10f;

        consoleBaseScale = console.transform.localScale;

        PlayerScaleSingleton.SetPlayerScale(PlayerScale.Human);
    }

    // Update is called once per frame
    void Update()
    {
        // OLD CODE THAT TESTED WORLD SCALE CHANGES
        /*if (Input.GetKeyDown(KeyCode.Z))
            ChangeWorldScale(PlayerScale.Human);

        if (Input.GetKeyDown(KeyCode.X))
            ChangeWorldScale(PlayerScale.Giant);

        if (Input.GetKeyDown(KeyCode.C))
            ChangeWorldScale(PlayerScale.Dog);

        if (Input.GetKeyDown(KeyCode.V))
            ChangeWorldScale(PlayerScale.Flea);*/
    }

    public void ChangeWorldScale(PlayerScale scale)
    {
        if (scale == PlayerScaleSingleton.GetPlayerScale())
            return;

        PlayerScaleSingleton.SetPlayerScale(scale);

        switch (scale)
        {
            case PlayerScale.Flea: 
                scalingAnchor.transform.localScale = new Vector3(fleaScalar, fleaScalar, fleaScalar); 
                player.GetComponent<Movement>().speed = 5f;
                player.GetComponent<Movement>().audioSource.clip = null;
                console.transform.localScale = new Vector3(consoleBaseScale.x * fleaScalar, consoleBaseScale.y, consoleBaseScale.z * fleaScalar);
                baxterModel.transform.localScale = new Vector3(0.2107325f, 3.805992f * fleaScalar, 0.2325053f);
                break;
            case PlayerScale.Giant: 
                scalingAnchor.transform.localScale = new Vector3(giantScalar, giantScalar, giantScalar);
                player.GetComponent<Movement>().speed = 1f;
                player.GetComponent<Movement>().audioSource.clip = giant;
                console.transform.localScale = new Vector3(consoleBaseScale.x * giantScalar, consoleBaseScale.y, consoleBaseScale.z * giantScalar);
                baxterModel.transform.localScale = new Vector3(0.2107325f, 3.805992f * giantScalar, 0.2325053f);
                break;
            case PlayerScale.Dog: 
                scalingAnchor.transform.localScale = new Vector3(dogScalar, dogScalar, dogScalar); 
                player.GetComponent<Movement>().speed = 3f;
                player.GetComponent<Movement>().audioSource.clip = dog;
                console.transform.localScale = new Vector3(consoleBaseScale.x * dogScalar, consoleBaseScale.y, consoleBaseScale.z * dogScalar);
                baxterModel.transform.localScale = new Vector3(0.2107325f, 3.805992f * dogScalar, 0.2325053f);
                break;
            default: 
                scalingAnchor.transform.localScale = new Vector3(normalScalar, normalScalar, normalScalar); 
                player.GetComponent<Movement>().speed = 2f;
                player.GetComponent<Movement>().audioSource.clip = human;
                console.transform.localScale = new Vector3(consoleBaseScale.x * normalScalar, consoleBaseScale.y, consoleBaseScale.z * normalScalar);
                baxterModel.transform.localScale = new Vector3(0.2107325f, 3.805992f * normalScalar, 0.2325053f);
                break;
        }
    }
}
