using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MapUI : MonoBehaviour
{
    [Header("Minimap")]
    public RawImage minimapImage;
    public Image mapCursor;
    public Rect minimapBounds;
    public int minimapResolution = 256;

    [Header("Map Info")]
    public TextMeshProUGUI selectedTileInfoText;
    public TextMeshProUGUI playerPositionText;
    public TextMeshProUGUI mapCoordinatesText;

    [Header("Controls")]
    public Button zoomInButton;
    public Button zoomOutButton;
    public Button centerMapButton;
    public Slider zoomSlider;

    private Texture2D minimapTexture;
    private Vector2 selectedTilePosition;
    private float currentZoom = 1f;

    private void Start()
    {
        InitializeMinimap();
        zoomInButton.onClick.AddListener(ZoomIn);
        zoomOutButton.onClick.AddListener(ZoomOut);
        centerMapButton.onClick.AddListener(CenterMap);
        zoomSlider.onValueChanged.AddListener(OnZoomSliderChanged);
    }

    private void InitializeMinimap()
    {
        MapManager mapManager = GameManager.Instance.mapManager;
        minimapTexture = new Texture2D(minimapResolution, minimapResolution, TextureFormat.RGB24, false);

        // Generate minimap from map data
        for (int x = 0; x < minimapResolution; x++)
        {
            for (int y = 0; y < minimapResolution; y++)
            {
                Color pixelColor = GetTileColor(x, y, mapManager);
                minimapTexture.SetPixel(x, y, pixelColor);
            }
        }

        minimapTexture.Apply();
        minimapImage.texture = minimapTexture;
    }

    private Color GetTileColor(int x, int y, MapManager mapManager)
    {
        // Map resolution to actual map size
        int actualX = (x * mapManager.mapWidth) / minimapResolution;
        int actualY = (y * mapManager.mapHeight) / minimapResolution;

        MapManager.Tile tile = mapManager.GetTile(new Vector2Int(actualX, actualY));
        if (tile == null)
            return Color.gray;

        switch (tile.tileType)
        {
            case MapManager.TileType.Grass:
                return Color.green;
            case MapManager.TileType.Forest:
                return new Color(0, 0.5f, 0);
            case MapManager.TileType.Mountain:
                return Color.gray;
            case MapManager.TileType.Water:
                return Color.blue;
            case MapManager.TileType.Desert:
                return Color.yellow;
            default:
                return Color.white;
        }
    }

    private void Update()
    {
        UpdateMapDisplay();
    }

    private void UpdateMapDisplay()
    {
        // Update player position on map
        PlayerData playerData = GameManager.Instance.playerData;
        playerPositionText.text = "Position: " + playerData.playerInfo.villagePosition.ToString();
    }

    public void ZoomIn()
    {
        currentZoom = Mathf.Min(currentZoom + 0.1f, 3f);
        zoomSlider.value = currentZoom;
    }

    public void ZoomOut()
    {
        currentZoom = Mathf.Max(currentZoom - 0.1f, 0.5f);
        zoomSlider.value = currentZoom;
    }

    public void CenterMap()
    {
        // Center the map on player position
        PlayerData playerData = GameManager.Instance.playerData;
        selectedTilePosition = new Vector2(playerData.playerInfo.villagePosition.x, 
                                          playerData.playerInfo.villagePosition.y);
    }

    private void OnZoomSliderChanged(float value)
    {
        currentZoom = value;
    }
}
