using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI.Table;

public class Spawner : MonoBehaviour
{
    public GameObject circleObstacle;
    public Transform spawnPoint;
    private int rowAmount = 15;
    private float rowSpacing = 0.55f;
    private float colSpacing = 0.55f;

    public GameObject squareScore;
    public int squareCount = 16;

    float firstPos = 0;
    float secondPos = 0;
    float diff = 0;

    // Start is called before the first frame update
    void Start()
    {
        SpawnPyramid();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SpawnPyramid()
    {

        for(int row = 0; row < rowAmount; row++)
        {
            int circlesInRow = 3 + row;

            float rowStartX = spawnPoint.position.x + rowSpacing/2 - (circlesInRow / 2.0f) * rowSpacing;
            float squareStartX = spawnPoint.position.x - diff/2 + rowSpacing / 2 - (circlesInRow / 2.0f) * rowSpacing;

            for (int col = 0; col < circlesInRow; col++)
            {
                Vector3 position = new Vector3(
                    rowStartX + col * rowSpacing,
                    spawnPoint.position.y - row * colSpacing
                );

                if (col == 2) firstPos = position.x;
                if (col == 3) secondPos = position.x;

                Instantiate(circleObstacle, position, Quaternion.identity, transform);
            }
            diff = Mathf.Abs(firstPos - secondPos);
            float[] multies = new float[8] { 0.4f, 0.7f, 1.4f, 3f, 5f, 10f, 41f, 99f };

            if (row == 14)
            {

                for (int col = 1; col < circlesInRow; col++)
                {
                    Vector3 squarePosition = new Vector3(
                        squareStartX + col * rowSpacing,
                        spawnPoint.position.y - row * colSpacing - diff
                    );

                    GameObject square = Instantiate(squareScore, squarePosition, Quaternion.identity, transform);
                    square.transform.localScale = new Vector3(diff, 0.5f);

                    SlotMultiply slotMultiply = square.GetComponent<SlotMultiply>();

                    if (col == 8 || col == 9)
                    {
                        slotMultiply.multiplier = 0.4f; // Dla dwóch œrodkowych kwadratów
                    }
                    else
                    {
                        int index;
                        int half = multies.Length / 2;
                        if(col < 10) // lewa
                        {
                            index = Mathf.Clamp(8 - col, 0, multies.Length - 1);

                        }
                        else  // Prawa
                        {
                            index = Mathf.Clamp(col - 9, 0, multies.Length - 1);
                        }
                        slotMultiply.multiplier = multies[index];
                    }

                    //kolory
                    float centerX = squareStartX + (circlesInRow/2) * rowSpacing;
                    float maxDistance = (circlesInRow / 2) * rowSpacing;
                    float distanceFromCenter = Mathf.Abs(squarePosition.x - centerX);
                    float t = distanceFromCenter / maxDistance;

                    Color midColor = new Color(1f, 1f, 0f);
                    Color endColor = new Color(1f, 0f, 0.2f);

                    Color color = Color.Lerp(midColor, endColor, t);
                    SpriteRenderer renderer = square.GetComponent<SpriteRenderer>();

                    if (renderer != null)
                    {
                        renderer.color = color;
                    }
                }
            }
        }
    }


}
