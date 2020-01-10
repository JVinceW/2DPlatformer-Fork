using UnityEngine;

public class LevelCreator : MonoBehaviour
{
    [Header("Level Creator")]
    public Texture2D m_maps;
    public ColorToPrefabs[] m_ColorMappings;
    // Start is called before the first frame update
    void Start()
    {
        //Tạo map
        GenerateMap();

        //Set max và min cho camera
        CameraFollow camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraFollow>();
        if (camera == null)
            return;
        Logs.LogD("Width:" + m_maps.width + " - Height :" + m_maps.height);
        camera.SetMaxY(m_maps.height);
        camera.SetMaxX(m_maps.width);
    }

    private void GenerateMap()
    {
        for (int x = 0; x < m_maps.width ; x++)
        {
            for (int y = 0; y < m_maps.height; y++)
            {
                GenerateTiled(x, y);
            }
        }
    }

    private void GenerateTiled(int x, int y)
    {
        Color pixelColor = m_maps.GetPixel(x, y);
        if(pixelColor.a == 0)
        {
            //a = 0 thì bỏ qua -Độ trong suốt của hình-
            return;
        }

        foreach (ColorToPrefabs colorMaping in m_ColorMappings)
        {
            //Nhận diện màu sắc mà tạo vật thể
            if(colorMaping.color.Equals(pixelColor))
            {
                Vector2 position = new Vector2(x, y);
                GameObject tile = Instantiate(colorMaping.prefabs, position, Quaternion.identity, transform);
                tile.GetComponent<SpriteRenderer>().sortingLayerName = "BackGround";
            }
        }
    }
}
