using UnityEngine;

public class Food : MonoBehaviour
{
    public Collider2D gridArea;

    private void Start()
    {
        //O jogo sempre começa chamando o método..
        RandomizePosition();
    }

    public void RandomizePosition()
    {
        //Lê as "bordas" do jogo.
        Bounds bounds = this.gridArea.bounds;

        //Randomiza um número para criar a comida no jogo, dentro das bordas.
        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);

        //Arredonda o valor gerado.
        x = Mathf.Round(x);
        y = Mathf.Round(y);

        this.transform.position = new Vector2(x, y);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Ao cilidir com outro objeto, aciona o método.
        RandomizePosition();
    }

}
