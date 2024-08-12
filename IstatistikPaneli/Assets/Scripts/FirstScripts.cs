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

    void Start()
    {
        // TMP bileşenlerinin atanıp atanmadığını kontrol edin
        if (OcakSicakligiTxt == null || OcakAgirligiTxt == null || UstKalipSicakligiTxt == null ||
            AltKalipSicakligiTxt == null || IvmeTxt == null || BasincTxt == null || OEETxt == null ||
            SiparisKoduTxt == null || MalzemeKoduTxt == null || SiparisMiktariTxt == null)
        {
            Debug.LogError("One or more TextMeshProUGUI components are not assigned in the Inspector.");
            return;
        }

        if (AnaPanel == null)
        {
            Debug.LogError("AnaPanel is not assigned in the Inspector.");
            return;
        }

        if (Button == null)
        {
            Debug.LogError("Button is not assigned in the Inspector.");
            return;
        }

        if (OEEDaire == null)
        {
            Debug.LogError("OEEDaire is not assigned in the Inspector.");
            return;
        }

        if (ProgressBarBackGround == null)
        {
            Debug.LogError("ProgressBarBackGround is not assigned in the Inspector.");
            return;
        }

        if (Background == null)
        {
            Debug.LogError("Background is not assigned in the Inspector.");
            return;
        }

        // AnaPanel'i başlangıçta gizli yap
        AnaPanel.gameObject.SetActive(false);

        // Buton için onClick olayına Background rengini beyaz yapma fonksiyonunu ekleyin
        Button.onClick.AddListener(ChangeBackgroundColor);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            SiparisKodu++;
            MalzemeKodu++;

            // Sipariş miktarını güncelle
            if (SiparisMiktari < 800) // Miktar 750'den küçükse 50 artır
            {
                SiparisMiktari += 50;
            }
            else
            {
                SiparisMiktari = 800; // Maksimum değer 800
            }

            // ProgressBarBackground'ın fillAmount değerini güncelle
            float normalizedAmount = Mathf.InverseLerp(0f, 800f, SiparisMiktari);
            if (ProgressBarBackGround != null)
            {
                ProgressBarBackGround.fillAmount = normalizedAmount;
            }

            // Metinleri güncelle
            SiparisKoduTxt.text = "Sipariş Kodu " + SiparisKodu;
            MalzemeKoduTxt.text = "Malzeme Kodu \n" + MalzemeKodu;
            SiparisMiktariTxt.text = "Sipariş Miktarı \n" + SiparisMiktari + " adet";
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

    // Coroutine fonksiyonu diğer değerler için
    private IEnumerator ChangeOtherValues()
    {
        while (true)
        {
            OEE = Random.Range(75f, 86f);
            OEEDaire.fillAmount = OEE / 100f;
            if (OEETxt != null)
            {
                OEETxt.text = OEE.ToString("F2") + "%";
            }

            float randomOcakSicakligi = Random.Range(OcakSicakligi * 0.9f, OcakSicakligi * 1.1f);
            float randomOcakAgirligi = Random.Range(OcakAgirligi * 0.9f, OcakAgirligi * 1.1f);
            float randomUstKalipSicakligi = Random.Range(UstKalipSicakligi * 0.9f, UstKalipSicakligi * 1.1f);
            float randomAltKalipSicakligi = Random.Range(AltKalipSicakligi * 0.9f, AltKalipSicakligi * 1.1f);
            float randomIvme = Random.Range(ivme * 0.9f, ivme * 1.1f);
            float randomBasinc = Random.Range(basınc * 0.9f, basınc * 1.1f);

            Debug.Log($"Ocak Sıcaklığı: {randomOcakSicakligi}, Ocak Ağırlığı: {randomOcakAgirligi}, Üst Kalıp Sıcaklığı: {randomUstKalipSicakligi}, Alt Kalıp Sıcaklığı: {randomAltKalipSicakligi}, İvme: {randomIvme}, Basınç: {randomBasinc}");

            OcakSicakligiTxt.text = "Ocak Sıcaklığı \n" + randomOcakSicakligi.ToString("F2") + " °C";
            OcakAgirligiTxt.text = "Ocak Ağırlığı \n" + randomOcakAgirligi.ToString("F2") + " kg";
            UstKalipSicakligiTxt.text = "Üst Kalıp Sıcaklığı \n" + randomUstKalipSicakligi.ToString("F2") + " °C";
            AltKalipSicakligiTxt.text = "Alt Kalıp Sıcaklığı \n" + randomAltKalipSicakligi.ToString("F2") + " °C";
            IvmeTxt.text = "İvme \n" + randomIvme.ToString("F2") + " m/s²";
            BasincTxt.text = "Basınç\n " + randomBasinc.ToString("F3") + " bar";

            yield return new WaitForSeconds(1);
        }
    }

    // Background rengini beyaz yapacak fonksiyon
    public void ChangeBackgroundColor()
    {
        Debug.Log("ChangeBackgroundColor fonksiyonu çağrıldı.");

        // Background GameObject'inin rengini beyaz yap
        if (Background != null)
        {
            Image backgroundImage = Background.GetComponent<Image>();
            if (backgroundImage != null)
            {
                // Renk değişimini kontrol et ve eski rengi geri getir
                if (isWhite)
                {
                    backgroundImage.color = originalColor;
                    isWhite = false;
                }
                else
                {
                    originalColor = backgroundImage.color;
                    backgroundImage.color = Color.white;
                    isWhite = true;
                }
            }
            else
            {
                Debug.LogError("Background GameObject'inde Image bileşeni bulunamadı.");
            }
        }
    }
}
