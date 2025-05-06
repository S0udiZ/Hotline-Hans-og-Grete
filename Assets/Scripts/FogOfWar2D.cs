using UnityEngine;
public class FogOfWar2D : MonoBehaviour
{
    public int textureSize = 256;
    public float worldSize = 20f;
    public Transform player;
    public float revealRadius = 2f;

    private Texture2D fogTexture;
    private SpriteRenderer sr;
    private Color32[] pixels;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        fogTexture = new Texture2D(textureSize, textureSize, TextureFormat.ARGB32, false);
        pixels = new Color32[textureSize * textureSize];

        // Fill fog with opaque black
        for (int i = 0; i < pixels.Length; i++)
            pixels[i] = new Color32(0, 0, 0, 255);

        fogTexture.SetPixels32(pixels);
        fogTexture.Apply();

        sr.sprite = Sprite.Create(fogTexture, new Rect(0, 0, textureSize, textureSize), new Vector2(0.5f, 0.5f));
        transform.localScale = new Vector3(worldSize, worldSize, 1);
    }

    void Update()
    {
        if (player == null) return;

        float pxPerUnit = textureSize / worldSize;
        Vector2 playerPos = player.position;
        int centerX = Mathf.FloorToInt((playerPos.x + worldSize / 2f) * pxPerUnit);
        int centerY = Mathf.FloorToInt((playerPos.y + worldSize / 2f) * pxPerUnit);

        int radius = Mathf.CeilToInt(revealRadius * pxPerUnit);

        for (int y = -radius; y <= radius; y++)
        {
            for (int x = -radius; x <= radius; x++)
            {
                int fx = centerX + x;
                int fy = centerY + y;
                if (fx >= 0 && fx < textureSize && fy >= 0 && fy < textureSize)
                {
                    float dst = Mathf.Sqrt(x * x + y * y);
                    if (dst < radius)
                    {
                        int index = fy * textureSize + fx;
                        pixels[index].a = 0; // make transparent
                    }
                }
            }
        }

        fogTexture.SetPixels32(pixels);
        fogTexture.Apply();
    }
}
