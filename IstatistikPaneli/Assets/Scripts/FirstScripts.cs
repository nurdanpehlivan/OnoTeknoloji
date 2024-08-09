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
    public float basýnc = 0.250f;

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
        // TMP bileþenlerinin atanýp atanmadýðýný kontrol edin
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

        // Orijinal rengi sakla
        if (Background != null)
        {
            Image backgroundImage = Background.GetComponent<Image>();
            if (backgroundImage != null)
            {
                originalColor = backgroundImage.color;
            }
        }

        // AnaPanel'i baþlangýçta gizli yap
        AnaPanel.gameObject.SetActive(false);

        // ProgressBarBackGround'ý baþlangýçta gizli yap
        ProgressBarBackGround.gameObject.SetActive(false);

        // Buton için onClick olayýna Background rengini deðiþtirme fonksiyonunu ekleyin
        Button.onClick.AddListener(ToggleBackgroundColor);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            SiparisKodu++;
            MalzemeKodu++;

            // Sipariþ miktarýný güncelle
            if (SiparisMiktari < 800) // Miktar 750'den küçükse 50 artýr
            {
                SiparisMiktari += 50;
            }
            else
            {
                SiparisMiktari = 800; // Maksimum deðer 800
            }

            // ProgressBarBackground'ýn fillAmount deðerini güncelle
            float normalizedAmount = Mathf.InverseLerp(0f, 800f, SiparisMiktari);
            if (ProgressBarBackGround != null)
            {
                ProgressBarBackGround.fillAmount = normalizedAmount;
                // ProgressBarBackGround'ý görünür hale getir
                ProgressBarBackGround.gameObject.SetActive(true);
            }

            // Metinleri güncelle
            SiparisKoduTxt.text = "Sipariþ Kodu " + SiparisKodu;
            MalzemeKoduTxt.text = "Malzeme Kodu \n" + MalzemeKodu;
            SiparisMiktariTxt.text = "Sipariþ Miktarý \n" + SiparisMiktari + " adet";
        }
    }

    // AnaPanel'in görünürlüðünü deðiþtirir
    public void ToggleAnaPanel()
    {
        bool isActive = !AnaPanel.gameObject.activeSelf;
        AnaPanel.gameObject.SetActive(isActive);

        if (isActive)
        {
            // AnaPanel aktif hale geldiðinde Coroutine'leri baþlat
            StartCoroutine(ChangeOtherValues());
        }
        else
        {
            // AnaPanel pasif hale geldiðinde Coroutine'leri durdur
            StopAllCoroutines();
        }
    }

    // Coroutine fonksiyonu diðer deðerler için
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
            float randomBasinc = Random.Range(basýnc * 0.9f, basýnc * 1.1f);

            OcakSicakligiTxt.text = "Ocak Sýcaklýðý \n" + randomOcakSicakligi.ToString("F2") + " °C";
            OcakAgirligiTxt.text = "Ocak Aðýrlýðý \n" + randomOcakAgirligi.ToString("F2") + " kg";
            UstKalipSicakligiTxt.text = "Üst Kalýp Sýcaklýðý \n" + randomUstKalipSicakligi.ToString("F2") + " °C";
            AltKalipSicakligiTxt.text = "Alt Kalýp Sýcaklýðý \n" + randomAltKalipSicakligi.ToString("F2") + " °C";
            IvmeTxt.text = "Ývme \n" + randomIvme.ToString("F2") + " m/s²";
            BasincTxt.text = "Basýnç\n " + randomBasinc.ToString("F3") + " bar";

            yield return new WaitForSeconds(1);
        }
    }

    // Background rengini deðiþtiren fonksiyon
    public void ToggleBackgroundColor()
    {
        if (Background != null)
        {
            Image backgroundImage = Background.GetComponent<Image>();
            if (backgroundImage != null)
            {
                if (isWhite)
                {
                    backgroundImage.color = originalColor;
                }
                else
                {
                    backgroundImage.color = Color.white;
                }

                isWhite = !isWhite; // Durumu tersine çevir
            }
            else
            {
                Debug.LogError("Background GameObject'inde Image bileþeni bulunamadý.");
            }
        }
    }
}
