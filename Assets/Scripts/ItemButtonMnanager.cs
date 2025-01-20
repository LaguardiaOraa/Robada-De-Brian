using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class ItemButtonMnanager : MonoBehaviour
{
    private string ItemName;
    private Sprite ItemImage;
    private string ItemDescripcion;
    private GameObject Item3DModel;
    [SerializeField] private GameObject sueloSombra;
    private GameObject sueloSombraEscena;
    private ArInteractionManager arInteractiveManager;

    public string ItemName1 { get => ItemName; set => ItemName = value; }
    public Sprite ItemImage1 { get => ItemImage; set => ItemImage = value; }
    public string ItemDescripcion1 { get => ItemDescripcion; set => ItemDescripcion = value; }
    public GameObject Item3DModel1 { get => Item3DModel; set => Item3DModel = value; }

    // Start is called before the first frame update
    void Start()
    {
        transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = ItemName1;
        transform.GetChild(1).GetComponent<RawImage>().texture = ItemImage1.texture;
        transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = ItemDescripcion1;
        var button = GetComponent<Button>();
        button.onClick.AddListener(GameManager.manager.ARPosition);
        button.onClick.AddListener(Create3DModel);
        arInteractiveManager = FindObjectOfType<ArInteractionManager>();
    }

    private void Create3DModel()
    {
        sueloSombraEscena = GameObject.FindGameObjectWithTag("Suelo");
        if (sueloSombraEscena == null)
        {
            arInteractiveManager.SueloSombra = Instantiate(sueloSombra);
            sueloSombra.SetActive(true);
        }
        arInteractiveManager.Item3DModel = Instantiate(Item3DModel1);
    }
}
