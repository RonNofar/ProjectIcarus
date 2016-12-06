using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PauseOverlayManager : MonoBehaviour {

    // UI elements of Start Panel
    public GameObject panel1;
    public Button btn1; // Let's Go!
    public Image img1; // Computer Mouse
    public float img1_speed, img1_range;
    Vector3 img1_Position;

    // UI elements of Death Panel
    public GameObject panel2;
    public Color[] color_panel2 = { new Color(0.8627f, 0.0784f, 0.0784f), new Color(0.9019f, 0.1176f, 0.1176f) };

    public float flashSpeed2 = 8;

    private Image image_panel2;
    private float startTime2 = 0;

    // UI elements of Pause Panel
    public GameObject panel3;

    // UI elements of In-Game Panel
    public GameObject panel4;
    public Text txt4;
    public Color[] color_txt4 = { Color.yellow, Color.black };
    public float flashSpeed4 = 8;

    private string[] zeros4 = { "000", "00", "0", "", "9999" };
    private int prvSize = 0;
    private float temp4; // used to store temp flash value
    private float prev_temp4;
    private bool check_temp4 = true;
    private int count4 = -1; // used to store amounts of current flashes
    private float startTime4 = 0;


    void Start () {
        SetInitialReferences();
    }
    
    void SetInitialReferences () {
        btn1 = btn1.GetComponent<Button>();
        img1_Position = img1.transform.localPosition;

        image_panel2 = panel2.GetComponent<Image>();

        txt4 = txt4.GetComponent<Text>();
    }
	
	void Update () {
	    if (!GameMaster.isGameStarted) {
            panel1.SetActive(true);
            panel2.SetActive(false);
            panel3.SetActive(false);
            panel4.SetActive(false);
            Panel1();
        }
        else {
            panel1.SetActive(false);
            if (UFOController.isMovementStarted) { // turn on In-Game overlay
                panel4.SetActive(true);
                Panel4();
            } else {
                panel4.SetActive(false);
            }
            if (GameMaster.isGamePaused) { // turn on pause menu
                panel3.SetActive(true);
            } else {
                panel3.SetActive(false);
            }
            if (GameMaster.isGameOver) { // turn on death sceen
                panel2.SetActive(true);
                Panel2();
            } else {
                panel2.SetActive(false);
            }
        }
	}

    #region Start Panel - 1
    void Panel1 () {
        btn1.onClick.AddListener(Button1);
        Image1();
    }

    void Button1 () {
        GameMaster.isGameStarted = true;
    }

    void Image1 () {
        img1.transform.localPosition = new Vector3(
            Mathf.Sin(Time.time * img1_speed) * img1_range,
            img1_Position.y,
            img1_Position.z
            );
    }
    #endregion
    #region Death Panel - 2
    void Panel2 () {
        PanelImage2();
    }

    void PanelImage2 () {
        image_panel2.color = Color.Lerp(color_panel2[0], color_panel2[1], (Mathf.Sin(Time.time) * flashSpeed2 + 1)/2);
    }

    #endregion
    #region In-Game Panel - 4
    void Panel4 () {
        Text4();
    }

    void Text4() {
        //UFOController.FixNegativeScore();
        int size = Mathf.Clamp(UFOController.score.ToString().Length, 0,6);
        //Debug.Log("size: " + size);
        if (size < 5) txt4.text = zeros4[size - 1] + UFOController.score.ToString();
        else if (size >= 5) txt4.text = zeros4[size - 1];

        if (size > prvSize) {
            prvSize = size;
            startTime4 = Time.time;
            count4 = 0;
        }
        if (count4 >= 0) {
            prev_temp4 = temp4;
            temp4 = (Mathf.Sin((Time.time-startTime4) * flashSpeed4) + 1) / 2;
            //Debug.Log("temp4: "+temp4);
            if (prev_temp4 > temp4 && check_temp4) count4++; check_temp4 = false;
            if (prev_temp4 < temp4 && !check_temp4) check_temp4 = true;
            txt4.color = Color.Lerp(color_txt4[0], color_txt4[1], temp4);
            if (count4 > size) count4 = -1;
        }
    }
    #endregion
    
}
