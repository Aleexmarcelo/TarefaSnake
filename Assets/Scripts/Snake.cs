using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Snake : MonoBehaviour
{  
    //Criada a lista usando transform para formação da cobra.
    private List<Transform> _segments = new List<Transform>();
    //Criando a variável transform para os prefabs.
    public Transform segmentPrefab;
    //Criando a variável cujo determina a direção que a cobra vai seguir.
    public Vector2 direction = Vector2.right;
    //Criando a variável com o número de quadrados da cobra no início.
    public int initialSize = 4;

    //Determina tudo que deve ser executado quando o jogo é iniciado
    private void Start()
    {
        //O jogo começa já no modo ResetState
        ResetState();
    }

    //Determina o que deve ser executado a todo tempo durante o jogo.
    private void Update()
    {
        //Identifica a tecla pressionada pelo jogador, e move a cobra para cima ou para baixo.
        if (this.direction.x != 0f)
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) {
                this.direction = Vector2.up;
            } else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) {
                this.direction = Vector2.down;
            }
        }
        //Identifica a tecla pressionada pelo jogador, e move a cobra para direita ou para a esquerda.
        else if (this.direction.y != 0f)
        {
            if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) {
                this.direction = Vector2.right;
            } else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) {
                this.direction = Vector2.left;
            }
        }
    }

    private void FixedUpdate()
    {
        // Criando os segmentos "atrás" do rabo da cobra.
        for (int i = _segments.Count - 1; i > 0; i--) {
            _segments[i].position = _segments[i - 1].position;
        }

        // Criada as variáveis responsáveis pela direção do novo segmento.
        float x = Mathf.Round(this.transform.position.x) + this.direction.x;
        float y = Mathf.Round(this.transform.position.y) + this.direction.y;

        this.transform.position = new Vector2(x, y);
    }

    //Método que cria um novo segmento na posição correta.
    public void Grow()
    {
        Transform segment = Instantiate(this.segmentPrefab);
        segment.position = _segments[_segments.Count - 1].position;

        _segments.Add(segment);
    }

    //Método que define as regras de inicio de jogo, usado em Start para sempre iniciar o jogo do zero.
    public void ResetState()
    {
        //Define a direção que a cobra deve iniciar no início do jogo.
        this.direction = Vector2.right;
        this.transform.position = Vector3.zero;

        //Destrói o último objeto da cena, para dar sensasão de movimento na cobra.
        for (int i = 1; i < _segments.Count; i++) {
            Destroy(_segments[i].gameObject);
        }

        //Limpa os segmentos e adiciona um novo usando a varíável.
        _segments.Clear();
        _segments.Add(this.transform);

        //Se o tamanho da cobra for menor que a variável, adiciona mais um segmento.
        for (int i = 0; i < this.initialSize - 1; i++) {
            Grow();
        }
    }

    //Método que trabalha junto com o Collider2D para detectar colisões entre "tags"
    private void OnTriggerEnter2D(Collider2D other)
    {
        //Se a cobra colidir com a tag "food", o método Grow é ativado, fazendo a cobra crescer.
        if (other.tag == "Food") {
            Grow();
        }
        //Se a cobra colidir com a tag "Obstacle", o método ResetState é ativado, fazendo o jogo começar do zero.
        else if (other.tag == "Obstacle") {
            ResetState();
        }
    }

}
