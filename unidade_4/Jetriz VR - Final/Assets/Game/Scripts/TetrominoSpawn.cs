using UnityEngine;

public class TetrominoSpawn : MonoBehaviour
{
    public GameObject[] tetriminoPrefabs; // Prefabs para as diferentes formas Tetris
    public Transform pontoDeSpawn; // Ponto onde os Tetriminos serão gerados
    public float intervaloDeGeracao = 2.0f; // Intervalo entre a geração de Tetriminos

    private float tempoDaUltimaGeracao;

    public static TetrominoSpawn instance;

    public bool canSpawn = true;

    private void Awake() 
    {
        instance = this;
    }

    private void Start()
    {
        tempoDaUltimaGeracao = Time.time;
    }

    private void Update()
    {
        if (Time.time - tempoDaUltimaGeracao > intervaloDeGeracao)
        {
            GerarTetriminoAleatorio();
            tempoDaUltimaGeracao = Time.time;
        }
    }

    private void GerarTetriminoAleatorio()
    {
        if (canSpawn)
        {
            int indiceTetrimino = Random.Range(0, tetriminoPrefabs.Length);
        
            Vector3 posicaoAleatoria = new Vector3(pontoDeSpawn.position.x, pontoDeSpawn.position.y, pontoDeSpawn.position.z);

            GameObject tetrimino = Instantiate(tetriminoPrefabs[indiceTetrimino], posicaoAleatoria, Quaternion.identity);
        }

        canSpawn = false;
    }
}