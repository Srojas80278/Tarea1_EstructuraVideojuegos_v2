using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using System.Collections;

public class WheelController : MonoBehaviour
{
    [SerializeField]
    float rotatePower = 650.0F;

    [SerializeField]
    float stopPower = 200.0F;

    [SerializeField]
    TextMeshProUGUI nameText;

    [SerializeField]
    TextMeshProUGUI moneyText;

    [SerializeField]
    TextMeshProUGUI tryText;

    Rigidbody2D _rb;

    bool _rotate;

    float _endingTime;

    float _currentTime;

    float _prize = 0.0F;

    int spinsRemaining = 3;

    void Awake()
    {
        nameText.text = StateManager.Instance.getName();
        moneyText.text = "$0.00";
        tryText.text = spinsRemaining + " tries";
        _rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        // Primer frame del componente
    }

    void Update()
    {
        // Se ejecuta cada frame
        // Transcurre en un tiempo delta (del frame anterior al frame actual X cantidad de milisegundos)
        if (_rb.angularVelocity > 0)
        {
            _rb.angularVelocity -= stopPower * Time.deltaTime;
            _rb.angularVelocity = Mathf.Clamp(_rb.angularVelocity, 0, 1440);
        }

        if (_rb.angularVelocity == 0 && _rotate)
        {
            _currentTime += Time.deltaTime;
            if (_currentTime > _endingTime)
            {
                AudioManager.Instance.StopSfx();
                GetReward();
                _rotate = false;
                _currentTime = 0.0F;
                _endingTime = 0.0F;
            }
        }
    }

    void GetReward()
    {
        float rotation = transform.eulerAngles.z;

        if (rotation >= 22 && rotation < 67)
        {
            SetWheelRotation(45.0F);
            Win(400);
        }
        else if (rotation >= 67 && rotation < 112)
        {
            SetWheelRotation(90.0F);
            Win(100);
        }
        else if (rotation >= 112 && rotation < 157)
        {
            AudioManager.Instance.PlaySFX("Jackpot", false);
            SetWheelRotation(135.0F);
            Win(3000);
        }
        else if (rotation >= 157 && rotation < 202)
        {
            SetWheelRotation(180.0F);
            Win(600);
        }
        else if (rotation >= 202 && rotation < 247)
        {
            SetWheelRotation(225.0F);
            Win(100);
        }
        else if (rotation >= 247 && rotation < 292)
        {
            SetWheelRotation(270.0F);
            Win(400);
        }
        else if (rotation >= 292 && rotation < 337)
        {
            SetWheelRotation(315.0F);
            Win(600);
        }
        else if (rotation >= 337 || rotation < 22)
        {
            SetWheelRotation(0.0F);
            Win(1000);
        }
    }

    void SetWheelRotation(float z)
    {
        GetComponent<RectTransform>().eulerAngles = new Vector3(0.0F, 0.0F, z);
    }

    void Win(float prize)
    {
        _prize += prize;
        moneyText.text = "$" + _prize.ToString("#,##0.00");
        StateManager.Instance.setPrize(moneyText.text);
    }

    public void Rotate()
    {
        if (!_rotate && spinsRemaining > 0)
        {
            _endingTime = Random.Range(0.5F, 2.0F);
            _rotate = true;
            _rb.AddTorque(Random.Range(rotatePower / 1.50F, rotatePower * 1.50F));
            // Reduzca el contador de giros restantes.
            spinsRemaining--;
            tryText.text = spinsRemaining + " tries";
            if (spinsRemaining == 0)
            {

                tryText.text ="No more tries";
                StartCoroutine(WaitAndLoadScene(6.0f));
            }
        }


    }
    public IEnumerator WaitAndLoadScene(float waitTime)
    {
        // Espera durante el tiempo especificado.
        yield return new WaitForSeconds(waitTime);
        // Usar LevelManager para cambiar a la siguiente escena.
        LevelManager.Instance.NextScene();
    }
}
