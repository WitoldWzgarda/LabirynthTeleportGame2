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

    List<Vector3> BialeKafelki = new List<Vector3>();
    List<Vector3> ZieloneKafelki = new List<Vector3>();
    List<Vector3> CzerwoneKafelki = new List<Vector3>();
    public float procenty = 3;
    public GameObject[] key;

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

                if (colorMapping.color == Color.white)
                {
                    BialeKafelki.Add(position);
                }

                if (colorMapping.color == Color.green)
                {
                    ZieloneKafelki.Add(position);
                }

                if (colorMapping.color == Color.red)
                {
                    CzerwoneKafelki.Add(position);
                }

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

        for (int i = 0; i < procenty; i++)
        {
            int losowa = Random.Range(0, BialeKafelki.Count - 1);
            Instantiate(key[0], BialeKafelki[losowa], Quaternion.identity, transform);

            losowa = Random.Range(0, ZieloneKafelki.Count - 1);
            Instantiate(key[1], ZieloneKafelki[losowa], Quaternion.identity, transform);

            losowa = Random.Range(0, CzerwoneKafelki.Count - 1);
            Instantiate(key[2], CzerwoneKafelki[losowa], Quaternion.identity, transform);
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
