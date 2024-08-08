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
    public float bas�nc = 0.250f;

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

    public Canvas Panel;
    public Button Button;
    public Image OEEDaire;
    public Image ProgressBarBackGround;
    public GameObject Background; // Background GameObject

    private bool isWhite = false; // Rengin beyaz olup olmad���n� kontrol edecek bayrak
    private Color originalColor; // Orijinal rengi saklamak i�in de�i�ken

    void Start()
    {
        // TMP bile�enlerinin atan�p atanmad���n� kontrol edin
        if (OcakSicakligiTxt == null || OcakAgirligiTxt == null || UstKalipSicakligiTxt == null ||
            AltKalipSicakligiTxt == null || IvmeTxt == null || BasincTxt == null || OEETxt == null ||
            SiparisKoduTxt == null || MalzemeKoduTxt == null || SiparisMiktariTxt == null)
        {
            Debug.LogError("One or more TextMeshProUGUI components are not assigned in the Inspector.");
            return;
        }

        if (Panel == null)
        {
            Debug.LogError("Panel is not assigned in the Inspector.");
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

        // AnaPanel'i ba�lang��ta gizli yapPanel.gameObject.SetActive(false);
        

        // Buton i�in onClick olay�na Background rengini de�i�tirme fonksiyonunu ekleyin
        Button.onClick.AddListener(ChangeBackgroundColor);

        // Orijinal rengi sakla
        Image backgroundImage = Background.GetComponent<Image>();
        if (backgroundImage != null)
        {
            originalColor = backgroundImage.color;
        }

        // Ba�lang��ta de�erleri g�ncelle ve Coroutine'i ba�lat
        UpdateValues();
        StartCoroutine(ChangeOtherValues());
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            SiparisKodu++;
            MalzemeKodu++;

            // Sipari� miktar�n� g�ncelle
            if (SiparisMiktari < 800) // Miktar 750'den k���kse 50 art�r
            {
                SiparisMiktari += 50;
            }
            else
            {
                SiparisMiktari = 800; // Maksimum de�er 800
            }

            // ProgressBarBackground'�n fillAmount de�erini g�ncelle
            float normalizedAmount = Mathf.InverseLerp(0f, 800f, SiparisMiktari);
            if (ProgressBarBackGround != null)
            {
                ProgressBarBackGround.fillAmount = normalizedAmount;
            }

            // Metinleri g�ncelle
            SiparisKoduTxt.text = "Sipari� Kodu\n" + SiparisKodu;
            MalzemeKoduTxt.text = "Malzeme Kodu \n" + MalzemeKodu;
            SiparisMiktariTxt.text = "Sipari� Miktar� \n" + SiparisMiktari + " adet";
        }
    }
    private void UpdateValues()
    {
        // Rastgele ba�lang�� de�erlerini ayarla
        OEE = Random.Range(75f, 86f);
        OcakSicakligi = Random.Range(400f, 600f);
        OcakAgirligi = Random.Range(700f, 900f);
        UstKalipSicakligi = Random.Range(20f, 30f);
        AltKalipSicakligi = Random.Range(10f, 20f);
        ivme = Random.Range(0.1f, 0.2f);
        bas�nc = Random.Range(0.2f, 0.3f);

        // TextMeshProUGUI bile�enlerine ba�lang�� de�erlerini ata
            OcakSicakligiTxt.text = "Ocak S�cakl��� \n" + OcakSicakligi.ToString("F2") + " �C";
            OcakAgirligiTxt.text = "Ocak A��rl��� \n" + OcakAgirligi.ToString("F2") + " kg";
            UstKalipSicakligiTxt.text = "�st Kal�p S�cakl��� \n" + UstKalipSicakligi.ToString("F2") + " �C";
            AltKalipSicakligiTxt.text = "Alt Kal�p S�cakl��� \n" + AltKalipSicakligi.ToString("F2") + " �C";
            IvmeTxt.text = "�vme \n" + ivme.ToString("F2") + " m/s�";
            BasincTxt.text = "Bas�n�\n " + bas�nc.ToString("F") + " bar";
            OEETxt.text = OEE.ToString("F2") + "%";
    }

    private IEnumerator ChangeOtherValues()
    {
        while (true)
        {
            // OEE de�erini rastgele g�ncelle
            OEE = Random.Range(75f, 86f);
            // OEEDaire'in fillAmount de�erini OEE'ye g�re g�ncelle
            float oeeNormalizedAmount = Mathf.InverseLerp(75f, 86f, OEE);
            if (OEEDaire != null)
            {
                OEEDaire.fillAmount = oeeNormalizedAmount;
            }

            // Di�er de�erleri rastgele g�ncelle
            float randomOcakSicakligi = Random.Range(OcakSicakligi * 0.9f, OcakSicakligi * 1.1f);
            float randomOcakAgirligi = Random.Range(OcakAgirligi * 0.9f, OcakAgirligi * 1.1f);
            float randomUstKalipSicakligi = Random.Range(UstKalipSicakligi * 0.9f, UstKalipSicakligi * 1.1f);
            float randomAltKalipSicakligi = Random.Range(AltKalipSicakligi * 0.9f, AltKalipSicakligi * 1.1f);
            float randomIvme = Random.Range(ivme * 0.9f, ivme * 1.1f);
            float randomBasinc = Random.Range(bas�nc * 0.9f, bas�nc * 1.1f);

            // TextMeshProUGUI bile�enlerine de�erleri ata
            if (OcakSicakligiTxt != null)
            {
                OcakSicakligiTxt.text = "Ocak S�cakl��� \n" + randomOcakSicakligi.ToString("F2") + " �C";
            }

            if (OcakAgirligiTxt != null)
            {
                OcakAgirligiTxt.text = "Ocak A��rl��� \n" + randomOcakAgirligi.ToString("F2") + " kg";
            }

            if (UstKalipSicakligiTxt != null)
            {
                UstKalipSicakligiTxt.text = "�st Kal�p S�cakl��� \n" + randomUstKalipSicakligi.ToString("F2") + " �C";
            }

            if (AltKalipSicakligiTxt != null)
            {
                AltKalipSicakligiTxt.text = "Alt Kal�p S�cakl��� \n" + randomAltKalipSicakligi.ToString("F2") + " �C";
            }

            if (IvmeTxt != null)
            {
                IvmeTxt.text = "�vme \n" + randomIvme.ToString("F2") + " m/s�";
            }

            if (BasincTxt != null)
            {
                BasincTxt.text = "Bas�n�\n " + randomBasinc.ToString("F3") + " bar";
            }

            yield return new WaitForSeconds(1);
        }
    }

    private void ChangeBackgroundColor()
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
            isWhite = !isWhite;
        }
        else
        {
            Debug.LogError("Background Image bile�eni atanm�� de�il.");
        }
    }
}
