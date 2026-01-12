using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public enum GameState { Init, Play, Pause, Exit }
    public GameState CurrentState { get; private set; }
    public CodeTest_InventoryManager inventoryManager;
    
  
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != null)
        {
            Destroy(gameObject);
        }

        inventoryManager = GetComponent<CodeTest_InventoryManager>();
    }
    void Start()
    {
        CurrentState = GameState.Init;
        
    }

    // Update is called once per frame
    public void UpdateStateUI(GameState newstate)
    {
        newstate = CurrentState;
       // UIManager.Instance.UpdateStateText($"Now State : {CurrentState}");
    }

    void Update()
    {
        
        if(Keyboard.current.spaceKey.isPressed)
        {
            CurrentState = GameState.Play;
            UpdateStateUI(CurrentState);
        }
    }

    
    
}
