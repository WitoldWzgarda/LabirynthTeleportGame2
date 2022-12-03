using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public Texture2D map;
    public ColorToPrefarb[] colorMappings;
    public float offset = 5f;

    public Material material01;
    public Material material02;
    // 2 losowe materialy scian ^

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GenerateTile(int x, int z)
    {
        //pobieramy kolor piksela w pozycji x, z
        Color pixelColor = map.GetPixel(x, z);

        if (pixelColor.a == 0)
        {
            return;
        }

        foreach (ColorToPrefarb colorMapping in colorMappings)
        {
            //Je¿eli któryœ z kolorów który odpowiada to ustaw Prefarb
            if (colorMapping.color.Equals(pixelColor))
            {
                // wylicz pozycje na podstawie piksela 
                Vector3 position = new Vector3(x, 0, z) * offset;

                Instantiate(colorMapping.prefarb, position, Quaternion.identity, transform);
            }
        }
    }

    public void GenerateLabirynth()
    {
        for (int x = 0; x < map.width; x++)
        {
            for (int z = 0; z < map.height; z++)
            {
                GenerateTile(x, z);
            }
        }
        ColorTheChildren();
    }

    public void ColorTheChildren()
    {
        foreach (Transform child in transform)
        {
            if (child.tag == "Wall")
            {
                if (Random.Range(1, 100) % 3 == 0)
                {
                    child.gameObject.GetComponent<Renderer>().material = material02;
                }
                else
                {
                    child.gameObject.GetComponent<Renderer>().material = material01;
                }
            }

            // W tym miejscu mo¿na dodaæ kawa³ek, kodu, który bêdzie sprawdza³ czy œciana ma jeszcze jakieœ dzieci
            // i by im te¿ nada³a materia³y w ten sposób 
            if (child.childCount > 0)
            {
                foreach (Transform grandchild in child.transform)
                {
                    if (grandchild.tag == "Wall")
                    {
                        if (Random.Range(1, 100) % 3 == 0)
                        {
                            child.gameObject.GetComponent<Renderer>().material = material02;
                        }
                        else
                        {
                            child.gameObject.GetComponent<Renderer>().material = material01;
                        }
                    }
                }
            }
        }
    }
}
