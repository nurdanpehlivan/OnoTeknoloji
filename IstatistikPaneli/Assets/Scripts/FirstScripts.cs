using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class FirstScripts : MonoBehaviour
{
    public int SiparisKodu = 0;
    public int MalzemeKodu = 0;
    public int SiparisMiktari = 0;

    public float OEE = 75;

    public float OcakSicakligi = 500f;
    public float OcakAgirligi = 800f;
    public float UstKalipSicakligi = 25f;
    public float AltKalipSicakligi = 15f;
    public float ivme = 0.15f;
    public float basınc = 0.250f;

    public TextMeshProUGUI OcakSicakligiTxt;
    public TextMeshProUGUI OcakAgirligiTxt;
    public TextMeshProUGUI UstKalipSicakligiTxt;
    public TextMeshProUGUI AltKalipSicakligiTxt;
    public TextMeshProUGUI IvmeTxt;
    public TextMeshProUGUI BasincTxt;
    public TextMeshProUGUI OEETxt;
    public TextMeshProUGUI SiparisKoduTxt;
    public TextMeshProUGUI MalzemeKoduTxt;
    public TextMeshProUGUI SiparisMiktariTxt;

    public Canvas AnaPanel;
    public Button Button;
    public Image OEEDaire;
    public Image ProgressBarBackGround;
    public GameObject Background;

    private bool isWhite = false;
    private Color originalColor;
    private Animator animator;

    void Start()
    {
        // TMP bileşenlerinin atanıp atanmadığını kontrol edin
        ValidateReferences();

        // AnaPanel'i başlangıçta gizli yap
        AnaPanel.gameObject.SetActive(false);

        // Buton için onClick olayına Background rengini beyaz yapma fonksiyonunu ekleyin
        Button.onClick.AddListener(ChangeBackgroundColor);

        // Animator bileşenini al
        animator = AnaPanel.GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("Animator component is missing on the AnaPanel GameObject.");
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            UpdateOrderDetails();
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            TriggerAnimation();
        }
    }

    private void OnMouseDown()
    {
        Debug.Log("Küp'e tıklandı.");
        bool isActive = !AnaPanel.gameObject.activeSelf;
        AnaPanel.gameObject.SetActive(isActive);

        if (isActive)
        {
            // AnaPanel aktif hale geldiğinde Coroutine'leri başlat
            StartCoroutine(ChangeOtherValues());
        }
        else
        {
            // AnaPanel pasif hale geldiğinde Coroutine'leri durdur
            StopAllCoroutines();
        }
    }

    private IEnumerator ChangeOtherValues()
    {
        while (true)
        {
            UpdateDynamicValues();
            yield return new WaitForSeconds(1f);
        }
    }

    private void ChangeBackgroundColor()
    {
        if (Background != null)
        {
            Image bgImage = Background.GetComponent<Image>();
            if (bgImage != null)
            {
                bgImage.color = isWhite ? originalColor : Color.white;
                isWhite = !isWhite;
            }
        }
    }

    private void ValidateReferences()
    {
        if (OcakSicakligiTxt == null || OcakAgirligiTxt == null || UstKalipSicakligiTxt == null ||
            AltKalipSicakligiTxt == null || IvmeTxt == null || BasincTxt == null || OEETxt == null ||
            SiparisKoduTxt == null || MalzemeKoduTxt == null || SiparisMiktariTxt == null)
        {
            Debug.LogError("One or more TextMeshProUGUI components are not assigned in the Inspector.");
        }

        if (AnaPanel == null)
        {
            Debug.LogError("AnaPanel is not assigned in the Inspector.");
        }

        if (Button == null)
        {
            Debug.LogError("Button is not assigned in the Inspector.");
        }

        if (OEEDaire == null)
        {
            Debug.LogError("OEEDaire is not assigned in the Inspector.");
        }

        if (ProgressBarBackGround == null)
        {
            Debug.LogError("ProgressBarBackGround is not assigned in the Inspector.");
        }

        if (Background == null)
        {
            Debug.LogError("Background is not assigned in the Inspector.");
        }
    }

    private void UpdateOrderDetails()
    {
        SiparisKodu++;
        MalzemeKodu++;
        SiparisMiktari = Mathf.Min(SiparisMiktari + 50, 800);

        if (ProgressBarBackGround != null)
        {
            float normalizedAmount = Mathf.InverseLerp(0f, 800f, SiparisMiktari);
            ProgressBarBackGround.fillAmount = normalizedAmount;
        }

        SiparisKoduTxt.text = $"Sipariş Kodu {SiparisKodu}";
        MalzemeKoduTxt.text = $"Malzeme Kodu \n{MalzemeKodu}";
        SiparisMiktariTxt.text = $"Sipariş Miktarı \n{SiparisMiktari} adet";
    }

    private void TriggerAnimation()
    {
        if (animator != null)
        {
            animator.SetTrigger("Rotate");
        }
    }

    private void UpdateDynamicValues()
    {
        OEE = Random.Range(75f, 86f);
        OEEDaire.fillAmount = OEE / 100f;
        OEETxt.text = $"{OEE:F2}%";

        OcakSicakligiTxt.text = $"Ocak Sıcaklığı \n{Random.Range(OcakSicakligi * 0.9f, OcakSicakligi * 1.1f):F2} °C";
        OcakAgirligiTxt.text = $"Ocak Ağırlığı \n{Random.Range(OcakAgirligi * 0.9f, OcakAgirligi * 1.1f):F2} kg";
        UstKalipSicakligiTxt.text = $"Üst Kalıp Sıcaklığı \n{Random.Range(UstKalipSicakligi * 0.9f, UstKalipSicakligi * 1.1f):F2} °C";
        AltKalipSicakligiTxt.text = $"Alt Kalıp Sıcaklığı \n{Random.Range(AltKalipSicakligi * 0.9f, AltKalipSicakligi * 1.1f):F2} °C";
        IvmeTxt.text = $"İvme \n{Random.Range(ivme * 0.9f, ivme * 1.1f):F2} m/s²";
        BasincTxt.text = $"Basınç \n{Random.Range(basınc * 0.9f, basınc * 1.1f):F2} bar";
    }
}
