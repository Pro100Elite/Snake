using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviour
{

    public GameObject block;
    public float speedSnake = 0.5f;
    struct Body
    {

        public int indexX, indexY;

    }

    int sizeSnake = 0;
    int dX = 0, dY = 1;

    float renderTime, currentTime;

    GameObject[,] mapArea = new GameObject[20, 20];
    Body[] snake = new Body[10];

    void Start()
    {
        GenMap();

        for (int i = 0; i < 2; i++)
        {
            GrowSnake();
        }
        StartCoroutine(CreateItems());
    }

    void Update()
    {
        DrawSnake();
        currentTime = Time.time;
        float alpha = currentTime - renderTime;

        if (alpha > speedSnake)
        {
            MoveSnake();
            renderTime = Time.time;
        }
        TestStep();
        Victory();
    }

    void GenMap()
    {
        Vector3 pointStart = new Vector3(0, 0, 0);
        for (int y = 0; y < 20; y++)
        {
            for (int x = 0; x < 20; x++)
            {
                pointStart = new Vector3(x, y, 0);
                mapArea[x, y] = Instantiate(block, pointStart, Quaternion.identity);
                if (y == 0 || x == 0)
                {
                    SetColor(mapArea[x, y], 4);
                } else if (y == 19 || x == 19)
                {
                    SetColor(mapArea[x, y], 4);
                }
            }
        }
    }

    int GetColor(GameObject obj)
    {
        MyMap comonentObject = obj.GetComponent<MyMap>();
        int imgcolor = comonentObject.indexImg;

        return imgcolor;
    }

    void SetColor(GameObject obj, int setColor)
    {
        MyMap comonentObject = obj.GetComponent<MyMap>();
        comonentObject.indexImg = setColor;
    }

    void GrowSnake()
    {
        if (sizeSnake == 0)
        {
            snake[sizeSnake].indexX = 10;
            snake[sizeSnake].indexY = 10;
        }
        else
        {
            speedSnake += 0.1f;
            snake[sizeSnake].indexX = snake[sizeSnake - 1].indexX;
            snake[sizeSnake].indexY = snake[sizeSnake - 1].indexY;
        }

        sizeSnake++;
    }

    void DrawSnake()
    {
        gameObject.GetComponent<Camera>().PositionCamera(snake[0].indexX, snake[0].indexY);
        for (int i = 1; i < sizeSnake; i++)
        {
            SetColor(mapArea[snake[i].indexX, snake[i].indexY], 2);
        }

        if (sizeSnake > 0)
        {
            SetColor(mapArea[snake[0].indexX, snake[0].indexY], 1);
        }
    }


    public void LeftButton()
    {
        if (dX != 1 & dY == 1)
        {
            dX = -1; dY = 0;
        } else if (dX == -1 & dY == 0)
        {
            dX = 0; dY = -1;
        } else if (dX == 0 & dY == -1)
        {
            dX = 1; dY = 0;
        } else if (dX == 1 & dY == 0)
        {
            dX = 0; dY = 1;
        }
    }

    public void RightButton()
    {
        if (dX != -1 && dY == 1)
        {
            dX = 1; dY = 0;
        }
        else if (dX == 1 && dY == 0)
        {
            dX = 0; dY = -1;
        }
        else if (dX == 0 & dY == -1)
        {
            dX = -1; dY = 0;
        }
        else if (dX == -1 & dY == 0)
        {
            dX = 0; dY = 1;
        }
    }

    void MoveSnake()
    {
        if ((dX != 0) || (dY != 0))

        {
            int last = sizeSnake - 1;

            if (last > 0)
            {
                SetColor(mapArea[snake[last].indexX, snake[last].indexY], 0);
            }

            for (int i = sizeSnake - 1; i > 0; i--) snake[i] = snake[i - 1];

            snake[0].indexX += dX;
            snake[0].indexY += dY;
        }
    }

    void TestStep()
    {
        int headX = snake[0].indexX, headY = snake[0].indexY;

        int step = GetColor(mapArea[headX, headY]);

        switch (step)

        {
            case 3:
                GrowSnake();
                break;
            case 4:
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                break;
            case 2:
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                break;
        }
    }

    void Victory()
    {
        if (sizeSnake >= 10)
        {
            SceneManager.LoadScene(2);
        }
    }

    IEnumerator CreateItems()
    {
        while (true)
        {
            int random = Random.Range(3, 5);
            int randomX = Random.Range(1, 18);
            int randomY = Random.Range(1, 18);
            SetColor(mapArea[randomX, randomY], random);
            yield return new WaitForSeconds(2.5f);
        }
    }
}
