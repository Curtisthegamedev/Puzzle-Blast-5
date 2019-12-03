using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//this is current script.
public class FoodCell : MonoBehaviour
{
    public SpriteRenderer foodSprite;
    public Vector2 gridPos;
    public TokenType typeOfToken = TokenType.Undefined;
    private static float foodGrow;
    [SerializeField] ParticleSystem foodBurst;
    [SerializeField] Transform spawnGemsOne, spawnGemsTwo, spawnGemsThree;
    [SerializeField] GameObject gemOne, gemTwo, gemThree; 
    private float fruitScaleUpSize = 1.25f;
    public Transform spriteObjectChildStartPos;
    public GameObject spriteObjectChild; 
    public enum TokenType
    {
        Undefined = -1,
        Apple = 0,
        Cherry = 1,
        Sword = 2,
        Armour = 3, 
        InvisibilityPotion = 4
    }

    private TokenType tokenType;
    //private IEnumerator DoFruitSolving()
    //{
    //   Vector3 scale = foodSprite.transform.localScale; 
    //  while (scale.x < 1.2)
    //}

    private void Start()
    {
        spriteObjectChildStartPos = this.gameObject.GetComponentInChildren<Transform>(); 
    }

    public void ChangeFruit(Board board)
    {
        int whatTypeOfFood = Random.Range(0, board.foodSprites.Length);
        typeOfToken = (FoodCell.TokenType)whatTypeOfFood;
        foodSprite.sprite = board.foodSprites[whatTypeOfFood];
    }

    public void ChangeFruitLevelTwo(BoardLeveTwo board)
    {
        int whatTypeOfFood = Random.Range(0, board.foodSprites.Length);
        typeOfToken = (FoodCell.TokenType)whatTypeOfFood;
        foodSprite.sprite = board.foodSprites[whatTypeOfFood];
    }



    public IEnumerator DoSolveFruit(Board board)
    {
        //gets the current size a the sprite being solved. 
        float currentScale = foodSprite.transform.localScale.x;
        while (currentScale < fruitScaleUpSize)
        {
            //wait one frame. 
            yield return null;
            //this is how fast the sprite will grow. 
            currentScale += 0.01f;
            foodSprite.transform.localScale = Vector3.one * currentScale;
        }
        //sets the foodBurst particle effect active 
        foodBurst.gameObject.SetActive(true);
        //spawns the gems that the player can pick up.
        //Instantiate(gemOne, spawnGemsOne.position, spawnGemsOne.rotation);
        //Instantiate(gemTwo, spawnGemsTwo.position, spawnGemsTwo.rotation);
        //Instantiate(gemThree, spawnGemsThree.position, spawnGemsThree.rotation);
        //sets the new sprite to be normal size
        foodSprite.transform.localScale = Vector3.one;
        ChangeFruit(board); 
        //waits for half a second before stoping the particle effect and setting it inactive
        yield return new WaitForSeconds(0.5f);
        foodBurst.Stop();
        foodBurst.gameObject.SetActive(false);
    }

    public IEnumerator doSolveFruitTwo(BoardLeveTwo board)
    {
        //gets the current size a the sprite being solved. 
        float currentScale = foodSprite.transform.localScale.x;
        while (currentScale < fruitScaleUpSize)
        {
            //wait one frame. 
            yield return null;
            //this is how fast the sprite will grow. 
            currentScale += 0.01f;
            foodSprite.transform.localScale = Vector3.one * currentScale;
        }
        //sets the foodBurst particle effect active 
        foodBurst.gameObject.SetActive(true);
        //sets the new sprite to be normal size
        foodSprite.transform.localScale = Vector3.one;
        ChangeFruitLevelTwo(board); 
        //waits for half a second before stoping the particle effect and setting it inactive
        yield return new WaitForSeconds(0.5f);
        //foodBurst.Stop();
        foodBurst.gameObject.SetActive(false);
    }

    public bool IsNeiborCell(Vector2 _gridPos)
    {
        if (gridPos.x == _gridPos.x || gridPos.y == _gridPos.y)
        {
            return true;
        }
        return false;
    }

    //Here is where i cheak the food that is contained in the nieboring cells. 
    public FoodCell getNeiborRight(FoodCell[,] grid)
    {
        //find the grid position to the right by incrementing x if it is less then the width of the board.
        if (gridPos.x + 1 < Board.widthOfBoard)
        {
            return grid[(int)gridPos.x + 1, (int)gridPos.y];
        }
        else
        {
            return null;
        }

    }

    public FoodCell getNeiborUp(FoodCell[,] grid)
    {
        //find the grid position up by incrementing y if it is less then the height of the board.
        if (gridPos.y + 1 < Board.heightOfBoard)
        {
            return grid[(int)gridPos.x, (int)gridPos.y + 1];
        }
        else
        {
            return null;
        }
    }


}
