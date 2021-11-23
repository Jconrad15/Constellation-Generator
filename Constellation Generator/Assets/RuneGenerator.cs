using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuneGenerator : MonoBehaviour
{
    [SerializeField]
    private GameObject pixel;

    [SerializeField]
    private GameObject line;

    private List<GameObject> pixels = new List<GameObject>();
    private List<GameObject> lines = new List<GameObject>();


    // Start is called before the first frame update
    void Start()
    {
        GenerateRunes();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ClearAll();

            GenerateRunes();
        }
    }

    private void GenerateRunes()
    {
        PlaceCircles();

        AlterLocations();

        CreateLines();

    }

    private void CreateLines()
    {
        int numLines = Random.Range(2, pixels.Count);

        for (int i = 0; i < numLines; i++)
        {
            // Get two points
            int pointIndex1 = Random.Range(0, pixels.Count);
            int pointIndex2 = Random.Range(0, pixels.Count);
            while (pointIndex2 == pointIndex1)
            {
                pointIndex2 = Random.Range(0, pixels.Count);
            }

            GameObject newLine = line;
            lines.Add(Instantiate(newLine));
            
            LineRenderer lr = lines[i].GetComponent<LineRenderer>();
            lr.useWorldSpace = true;

            Vector3[] positions = new Vector3[2];
            positions[0] = pixels[pointIndex1].transform.position;
            positions[1] = pixels[pointIndex2].transform.position;
            lr.SetPositions(positions);

        }
    }

    private void AlterLocations()
    {
        foreach (GameObject pix in pixels)
        {
            Vector3 position = pix.transform.position;
            float varianceX;
            varianceX = Random.Range(0, 0.05f);
            float varianceY;
            varianceY = Random.Range(0, 0.05f);

            position.x += varianceX;
            position.y += varianceY;
            pix.transform.position = position;
        }
    }

    private void PlaceCircles()
    {
        int numberPixels = Random.Range(3, 6);

        for (int i = 0; i < numberPixels; i++)
        {
            Vector3 position = CreatePosition();
            foreach (GameObject pix in pixels)
            {
                if (position == pix.transform.position)
                {
                    position = CreatePosition();
                }
            }
            position *= 2f;

            pixels.Add(Instantiate(pixel, position, Quaternion.identity));
            float scale = Random.Range(0.5f, 1);
            Vector3 newScale = new Vector3(scale, scale, scale);
            pixels[i].transform.localScale = newScale;
        }
    }

    private static Vector3 CreatePosition()
    {
        int x = Random.Range(0, 4);
        int y = Random.Range(0, 4);
        Vector3 position = new Vector3(x, y, 0);
        return position;
    }

    private void ClearAll()
    {
        for (int i = 0; i < pixels.Count; i++)
        {
            Destroy(pixels[i]);
        }
        pixels.Clear();

        for (int i = 0; i < lines.Count; i++)
        {
            Destroy(lines[i]);
        }
        lines.Clear();
    }

    private Color DarkenColor(Color color)
    {
        float colorChange = 0.8f;

        Color darkerColor = color;
        darkerColor.r *= colorChange;
        darkerColor.g *= colorChange;
        darkerColor.b *= colorChange;

        return darkerColor;
    }

    private Color RandomColor()
    {
        Color color = new Color();
        color.r = Random.value;
        color.g = Random.value;
        color.b = Random.value;
        color.a = 1;

        return color;
    }
}
