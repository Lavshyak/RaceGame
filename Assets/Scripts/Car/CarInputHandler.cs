using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CarInputHandler : MonoBehaviour
{
    public int playerNumber = 1;
    public bool allowUIInput = true;
    public bool allowCommonInput = true;

    

    private Vector2 inputVectorFromUI = Vector2.zero;

    //Components
    TopDownCarController topDownCarController;

    //Awake is called when the script instance is being loaded.
    void Awake()
    {
        topDownCarController = GetComponent<TopDownCarController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame and is frame dependent
    void Update()
    {
        Vector2 inputVector = Vector2.zero;

        if (allowCommonInput)
        {
            var playerNumberStr = playerNumber.ToString();
            
            inputVector.x += Input.GetAxis($"Horizontal_P{playerNumberStr}");
            inputVector.y += Input.GetAxis($"Vertical_P{playerNumberStr}");
        }
        if (allowUIInput)
        {
            inputVector += inputVectorFromUI;
        }
        
        //Send the input to the car controller.
        topDownCarController.SetInputVector(inputVector.normalized);
    }

    public void SetInput(Vector2 newInput)
    {
        inputVectorFromUI = newInput;
    }
}
