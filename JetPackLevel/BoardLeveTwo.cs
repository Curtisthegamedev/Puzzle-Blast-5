using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardLeveTwo : MonoBehaviour
{
    public static int widthOfBoard = 30, heightOfBoard = 3;
    [SerializeField] float cellWidth = 1, cellHight = 1;
    private int gridSolvedTimesInARow = 0;
    [SerializeField]
    GameObject myTile;
    public Sprite[] foodSprites;
    [SerializeField] float DelayBeforeSolveing = 0.5f;
    private FoodCell fruitCellDown, fruitCellUp;
    private bool FirstSolveGridHasPassed = false, thereWasSomethingToSolveInTheGrid = false;
    protected FoodCell[,] tiles;
    [SerializeField] Fuel myShipFuel;
    public void Start()
    {
        tiles = new FoodCell[widthOfBoard, heightOfBoard];
        LevelLoader();
        SolveGrid();
    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            onMouseUp();
        }
        if (Input.GetMouseButtonDown(0))
        {
            onMouseDown();
        }

    }

    private void onMouseUp()
    {
        if(Time.timeScale != 0)
        {
            fruitCellUp = GetFruiteCellFromMousePos(Input.mousePosition);
            Debug.Log(fruitCellUp);
            //ScoreScript.myMovesAmout++; script to increase the player score. 
            if (fruitCellUp == null)
            {
                //Prevents the player from droping powerUp tokens out of bounds. 
                fruitCellDown.foodSprite.gameObject.transform.localPosition = Vector3.zero;
            }
            if (fruitCellUp.IsNeiborCell(fruitCellDown.gridPos))
            {
                if (fruitCellDown == null)
                {
                    return;
                }
                //swaps the foodtype. 
                FoodCell.TokenType temp = fruitCellDown.typeOfToken;
                fruitCellDown.typeOfToken = fruitCellUp.typeOfToken;
                fruitCellUp.typeOfToken = temp;
                //swaps the sprites. 
                Sprite tempSprite = fruitCellDown.foodSprite.sprite;
                fruitCellDown.foodSprite.sprite = fruitCellUp.foodSprite.sprite;
                fruitCellUp.foodSprite.sprite = tempSprite;
                //sets the picked up fruit sprite back into it's cell. 
                fruitCellDown.foodSprite.gameObject.transform.localPosition = Vector3.zero;
                if (FirstSolveGridHasPassed)
                {
                    //Instantiate(gemOne, gemSpawnOne.position, gemSpawnOne.rotation);
                    //Instantiate(gemTwo, gemSpawnTwo.position, gemSpawnTwo.rotation);
                    //Instantiate(gemThree, gemSpawnThree.position, gemSpawnThree.rotation);
                }
            }
            else
            {
                fruitCellDown.foodSprite.gameObject.transform.localPosition = Vector3.zero;
                //Health.life -= 1;
            }
            gridSolvedTimesInARow = 0;
            StartCoroutine(WaitAndSolveGrid());
            StartCoroutine(WaitAndDamagePlayer());
        }
    }

    //This IEnumerator will damage the Player if they Move a token without a match. 
    private IEnumerator WaitAndDamagePlayer()
    {
        yield return new WaitForSeconds(1.5f);
        if (gridSolvedTimesInARow < 2 && !thereWasSomethingToSolveInTheGrid)
        {
            if (!Player.hasArmour)
            {
                Health.life -= 1;
            }
            else if (Player.hasArmour)
            {
                ArmourBar.armourAmount -= 1;
            }

        }

    }

    private void onMouseDown()
    {
        if(Time.timeScale != 0)
        {
            fruitCellDown = GetFruiteCellFromMousePos(Input.mousePosition);
        }
        //Debug.Log(fruitCellDown); 
    }

    private void LevelLoader()
    {
        for (int i = 0; i < widthOfBoard; i++)
        {
            for (int j = 0; j < heightOfBoard; j++)
            {
                //(Vector2)transform.position casts transform.position as a vector2 instead of vector3. 
                Vector2 position = (Vector2)transform.position + new Vector2(i * cellWidth, j * cellHight);
                GameObject tile = Instantiate(myTile, position, Quaternion.identity) as GameObject;
                tile.transform.parent = this.transform;
                FoodCell fruit = tile.GetComponent<FoodCell>();
                tiles[i, j] = fruit;
                fruit.gridPos = new Vector2(i, j);
                tile.name = "(" + i + "," + j + ")";
                int whatTypeOfFood = Random.Range(0, foodSprites.Length);
                tiles[i, j].typeOfToken = (FoodCell.TokenType)whatTypeOfFood;
                tiles[i, j].foodSprite.sprite = foodSprites[whatTypeOfFood];
            }
        }
    }
    //this function solves the grid but looping using for loops to cheak every cell and cheak weather the cells beside or on the bottom or top of the cell match the cell. 
    private void SolveGrid()
    {
        gridSolvedTimesInARow += 1;
        //Debug.Log("solveGrid");
        //two for loops used to cheak through every grid cell
        for (int i = 0; i < widthOfBoard; i++)
        {
            for (int j = 0; j < heightOfBoard; j++)
            {
                //Debug.Log("cheaking cell " + i + "," + j);
                FoodCell.TokenType tempFood = tiles[i, j].typeOfToken;
                FoodCell currentFruitCell = tiles[i, j];
                List<FoodCell> matchingCellsToTheRight = new List<FoodCell>();
                List<FoodCell> matchingCellsUp = new List<FoodCell>();
                FoodCell neiborFruitCell = currentFruitCell.getNeiborRight(tiles);
                //uses a while loop to cheak keep cheaking for matches as long as their is a neiboring Cell to the one being cheaked. 
                while (neiborFruitCell != null)
                {
                    //if statment used to cheak if cell being cheaked has a match with a horizontal cell. 
                    if (currentFruitCell.typeOfToken == neiborFruitCell.typeOfToken)
                    {
                        //Debug.Log("H-match- " + neiborFruitCell.gridPos);
                        matchingCellsToTheRight.Add(neiborFruitCell);
                        neiborFruitCell = neiborFruitCell.getNeiborRight(tiles);
                    }
                    //breaks out of the stament if their is no match. 
                    else
                    {
                        break;
                    }
                }
                neiborFruitCell = currentFruitCell.getNeiborUp(tiles);
                while (neiborFruitCell != null)
                {
                    if (currentFruitCell.typeOfToken == neiborFruitCell.typeOfToken)
                    {

                        //if statment used to cheak if cell being cheaked has a match with a vertical cell. 
                        //Debug.Log("V-match- " + neiborFruitCell.gridPos);
                        matchingCellsUp.Add(neiborFruitCell);
                        neiborFruitCell = neiborFruitCell.getNeiborUp(tiles);
                    }
                    else
                    {
                        break;
                    }
                }
                //uses bools to tell game if the food in a cell needs to be replaced by another food when a match is found
                bool horizontalSolved = false;
                bool verticalSolved = false;
                if (matchingCellsToTheRight.Count >= 2)
                {
                    horizontalSolved = true;
                    //iterate over this list and replace the food type with a new food type. 
                    for (int b = 0; b < matchingCellsToTheRight.Count; b++)
                    {
                        FoodCell fruitCellToSolve = matchingCellsToTheRight[b];
                        //Debug.Log("cells to the right match");
                        StartCoroutine(fruitCellToSolve.doSolveFruitTwo(this));
                    }
                }
                if (matchingCellsUp.Count >= 2)
                {
                    verticalSolved = true;
                    //iterate over this list and replace the food type with a new food type. 
                    for (int b = 0; b < matchingCellsUp.Count; b++)
                    {
                        FoodCell fruitCellToSolve = matchingCellsUp[b];
                        //Debug.Log("cells up match");
                        StartCoroutine(fruitCellToSolve.doSolveFruitTwo(this));
                    }
                }
                //if we solved any fruit cells above or to the right thne break out of this 
                if (horizontalSolved || verticalSolved)
                {
                    thereWasSomethingToSolveInTheGrid = true;
                    //randomFoodSelection called to change food that matches.
                    //this uses a coroutine from FruitCell.
                    StartCoroutine(currentFruitCell.doSolveFruitTwo(this));
                    //restarts the solving prosses.
                    myShipFuel.FuelAmount += 0.2f; 
                    StartCoroutine(WaitAndSolveGrid());
                }
                else
                {
                    thereWasSomethingToSolveInTheGrid = false;
                }
            }
        }
    }

    //this function randomizes the typeOfFood and sprite that appears in the Fruitcell. 
    private void randomFoodSelection(FoodCell myCell)
    {

        int whatTypeOfFood = Random.Range(0, foodSprites.Length);
        myCell.typeOfToken = (FoodCell.TokenType)whatTypeOfFood;
        myCell.foodSprite.sprite = foodSprites[whatTypeOfFood];
    }

    private IEnumerator WaitAndSolveGrid()
    {
        yield return new WaitForSeconds(DelayBeforeSolveing);
        SolveGrid();
    }
    private FoodCell GetFruiteCellFromMousePos(Vector2 _mousePos)
    {
        //Debug.Log("mousex - " + _mousePos.x + " mouse y - " + _mousePos.y);
        Vector2 startPos = transform.position;
        float cellXhalf = cellWidth / 2f;
        float CellYhalf = cellHight / 2f;
        //this converts my mouse from screen position to world position. 
        Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(_mousePos);
        Vector2 nomalizedMousePos = mouseWorldPos - startPos;
        //normalizes the mouse position in unity to my grid position. 
        int mouseXGridPos = (int)Mathf.Ceil((nomalizedMousePos.x - cellXhalf) / cellWidth);
        int mouseYGridPos = (int)Mathf.Ceil((nomalizedMousePos.y - CellYhalf) / cellHight);
        // Debug.Log("mouseGridPos" + " x " + mouseXGridPos + " y " + mouseYGridPos);
        //Change the mouseGridPos for a bigger grid.  
        if (mouseXGridPos > 29 || mouseXGridPos < 0 || mouseYGridPos > 2 || mouseYGridPos < 0)
        {
            return null;
        }
        else
        {
            FirstSolveGridHasPassed = true;
            return tiles[mouseXGridPos, mouseYGridPos];
        }
    }

}
