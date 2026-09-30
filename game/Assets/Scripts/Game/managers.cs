using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ExtendedNumerics;

public class managers : MonoBehaviour
{
    constants constantsScript;
    Color clr_unavailable, clr_available;
    BigDecimal dec_currentScore;
    string str_managerTag;
    public GameObject gmo_managersCanvas;
    GameObject gmo_menuItems;
    RectTransform tsf_managersContent;
    GameObject[] arr_managerObjects;
    List<Vector3> arr_managerStartingPositions = new List<Vector3>();
    scoreTracking scoreTrackingScript;
    AudioSource aud_purchaseSound, aud_buttonSound;  
    stage1 stage1Script;
    stage2 stage2Script;
    stage3 stage3Script;
    stage4 stage4Script;
    stage5 stage5Script;
    stage6 stage6Script;
    stage7 stage7Script;
    stage8 stage8Script;
    stage9 stage9Script;
    stage10 stage10Script;


    BigDecimal flt_costManager1, flt_costManager2, flt_costManager3, flt_costManager4,
        flt_costManager5, flt_costManager6, flt_costManager7, flt_costManager8,
        flt_costManager9, flt_costManager10, flt_costManager11, flt_costManager12,
        flt_costManager13, flt_costManager14, flt_costManager15, flt_costManager16,
        flt_costManager17, flt_costManager18, flt_costManager19, flt_costManager20,
        flt_costManager21, flt_costManager22, flt_costManager23, flt_costManager24, 
        flt_costManager25, flt_costManager26, flt_costManager27, flt_costManager28, 
        flt_costManager29, flt_costManager30;
    GameObject gmo_manager1, gmo_manager2, gmo_manager3, gmo_manager4, gmo_manager5,
        gmo_manager6, gmo_manager7, gmo_manager8, gmo_manager9, gmo_manager10, gmo_manager11,
        gmo_manager12, gmo_manager13, gmo_manager14, gmo_manager15, gmo_manager16, gmo_manager17,
        gmo_manager18, gmo_manager19, gmo_manager20, gmo_manager21, gmo_manager22, gmo_manager23, 
        gmo_manager24, gmo_manager25, gmo_manager26, gmo_manager27, gmo_manager28, gmo_manager29, 
        gmo_manager30;
    Button btn_buyManager1, btn_buyManager2, btn_buyManager3, btn_buyManager4,
        btn_buyManager5, btn_buyManager6, btn_buyManager7, btn_buyManager8,
        btn_buyManager9, btn_buyManager10, btn_buyManager11, btn_buyManager12,
        btn_buyManager13, btn_buyManager14, btn_buyManager15, btn_buyManager16,
        btn_buyManager17, btn_buyManager18, btn_buyManager19, btn_buyManager20,
        btn_buyManager21, btn_buyManager22, btn_buyManager23, btn_buyManager24, 
        btn_buyManager25, btn_buyManager26, btn_buyManager27, btn_buyManager28, 
        btn_buyManager29, btn_buyManager30;
    Text txt_costManager1, txt_buyManager1, txt_costManager2, txt_buyManager2, 
        txt_costManager3, txt_buyManager3, txt_costManager4, txt_buyManager4,
        txt_costManager5, txt_buyManager5, txt_costManager6, txt_buyManager6, 
        txt_costManager7, txt_buyManager7, txt_costManager8, txt_buyManager8,
        txt_costManager9, txt_buyManager9, txt_costManager10, txt_buyManager10,
        txt_costManager11, txt_buyManager11, txt_costManager12, txt_buyManager12, 
        txt_costManager13, txt_buyManager13, txt_costManager14, txt_buyManager14,
        txt_costManager15, txt_buyManager15, txt_costManager16, txt_buyManager16, 
        txt_costManager17, txt_buyManager17, txt_costManager18, txt_buyManager18,
        txt_costManager19, txt_buyManager19, txt_costManager20, txt_buyManager20,
        txt_costManager21, txt_buyManager21, txt_costManager22, txt_buyManager22, 
        txt_costManager23, txt_buyManager23, txt_costManager24, txt_buyManager24,
        txt_costManager25, txt_buyManager25, txt_costManager26, txt_buyManager26, 
        txt_costManager27, txt_buyManager27, txt_costManager28, txt_buyManager28,
        txt_costManager29, txt_buyManager29, txt_costManager30, txt_buyManager30;

    // Start is called before the first frame update
    void Start()
    {
        // yield return new WaitForSeconds(0.02f);

        constantsScript = GameObject.Find("Score").GetComponent<constants>();
        scoreTrackingScript = GameObject.Find("Score").GetComponent<scoreTracking>();
        tsf_managersContent = GameObject.Find("Managers Content").GetComponent<RectTransform>();
        aud_purchaseSound = GameObject.Find("Purchase Sound").GetComponent<AudioSource>();
        aud_buttonSound = GameObject.Find("Button Sound").GetComponent<AudioSource>();
        gmo_menuItems = GameObject.Find("Hamburger Menu Items");

        clr_unavailable = scoreTrackingScript.HexToColor("767676");
        clr_available = Color.white;

        gmo_managersCanvas = GameObject.Find("Managers Canvas");
        stage1Script = GameObject.Find("Stage1").GetComponent<stage1>();
        stage2Script = GameObject.Find("Stage2").GetComponent<stage2>();
        stage3Script = GameObject.Find("Stage3").GetComponent<stage3>();
        stage4Script = GameObject.Find("Stage4").GetComponent<stage4>();
        stage5Script = GameObject.Find("Stage5").GetComponent<stage5>();
        stage6Script = GameObject.Find("Stage6").GetComponent<stage6>();
        stage7Script = GameObject.Find("Stage7").GetComponent<stage7>();
        stage8Script = GameObject.Find("Stage8").GetComponent<stage8>();
        stage9Script = GameObject.Find("Stage9").GetComponent<stage9>();
        stage10Script = GameObject.Find("Stage10").GetComponent<stage10>();

        flt_costManager1 = constantsScript.flt_initialM1Cost;
        gmo_manager1 = GameObject.Find("Manager1");
        btn_buyManager1 = GameObject.Find("btn_m1buy").GetComponent<Button>();
        txt_buyManager1 = GameObject.Find("txt_m1buy").GetComponent<Text>();
        txt_costManager1 = GameObject.Find("txt_m1cost").GetComponent<Text>();
        txt_costManager1.text = "$" + scoreTrackingScript.SimplifyScore(flt_costManager1);

        flt_costManager2 = constantsScript.flt_initialM2Cost;
        gmo_manager2 = GameObject.Find("Manager2");
        btn_buyManager2 = GameObject.Find("btn_m2buy").GetComponent<Button>();
        txt_buyManager2 = GameObject.Find("txt_m2buy").GetComponent<Text>();
        txt_costManager2 = GameObject.Find("txt_m2cost").GetComponent<Text>();
        txt_costManager2.text = "$" + scoreTrackingScript.SimplifyScore(flt_costManager2);

        flt_costManager3 = constantsScript.flt_initialM3Cost;
        gmo_manager3 = GameObject.Find("Manager3");
        btn_buyManager3 = GameObject.Find("btn_m3buy").GetComponent<Button>();
        txt_buyManager3 = GameObject.Find("txt_m3buy").GetComponent<Text>();
        txt_costManager3 = GameObject.Find("txt_m3cost").GetComponent<Text>();
        txt_costManager3.text = "$" + scoreTrackingScript.SimplifyScore(flt_costManager3);

        flt_costManager4 = constantsScript.flt_initialM4Cost;
        gmo_manager4 = GameObject.Find("Manager4");
        btn_buyManager4 = GameObject.Find("btn_m4buy").GetComponent<Button>();
        txt_buyManager4 = GameObject.Find("txt_m4buy").GetComponent<Text>();
        txt_costManager4 = GameObject.Find("txt_m4cost").GetComponent<Text>();
        txt_costManager4.text = "$" + scoreTrackingScript.SimplifyScore(flt_costManager4);

        flt_costManager5 = constantsScript.flt_initialM5Cost;
        gmo_manager5 = GameObject.Find("Manager5");
        btn_buyManager5 = GameObject.Find("btn_m5buy").GetComponent<Button>();
        txt_buyManager5 = GameObject.Find("txt_m5buy").GetComponent<Text>();
        txt_costManager5 = GameObject.Find("txt_m5cost").GetComponent<Text>();
        txt_costManager5.text = "$" + scoreTrackingScript.SimplifyScore(flt_costManager5);

        flt_costManager6 = constantsScript.flt_initialM6Cost;
        gmo_manager6 = GameObject.Find("Manager6");
        btn_buyManager6 = GameObject.Find("btn_m6buy").GetComponent<Button>();
        txt_buyManager6 = GameObject.Find("txt_m6buy").GetComponent<Text>();
        txt_costManager6 = GameObject.Find("txt_m6cost").GetComponent<Text>();
        txt_costManager6.text = "$" + scoreTrackingScript.SimplifyScore(flt_costManager6);

        flt_costManager7 = constantsScript.flt_initialM7Cost;
        gmo_manager7 = GameObject.Find("Manager7");
        btn_buyManager7 = GameObject.Find("btn_m7buy").GetComponent<Button>();
        txt_buyManager7 = GameObject.Find("txt_m7buy").GetComponent<Text>();
        txt_costManager7 = GameObject.Find("txt_m7cost").GetComponent<Text>();
        txt_costManager7.text = "$" + scoreTrackingScript.SimplifyScore(flt_costManager7);

        flt_costManager8 = constantsScript.flt_initialM8Cost;
        gmo_manager8 = GameObject.Find("Manager8");
        btn_buyManager8 = GameObject.Find("btn_m8buy").GetComponent<Button>();
        txt_buyManager8 = GameObject.Find("txt_m8buy").GetComponent<Text>();
        txt_costManager8 = GameObject.Find("txt_m8cost").GetComponent<Text>();
        txt_costManager8.text = "$" + scoreTrackingScript.SimplifyScore(flt_costManager8);

        flt_costManager9 = constantsScript.flt_initialM9Cost;
        gmo_manager9 = GameObject.Find("Manager9");
        btn_buyManager9 = GameObject.Find("btn_m9buy").GetComponent<Button>();
        txt_buyManager9 = GameObject.Find("txt_m9buy").GetComponent<Text>();
        txt_costManager9 = GameObject.Find("txt_m9cost").GetComponent<Text>();
        txt_costManager9.text = "$" + scoreTrackingScript.SimplifyScore(flt_costManager9);

        flt_costManager10 = constantsScript.flt_initialM10Cost;
        gmo_manager10 = GameObject.Find("Manager10");
        btn_buyManager10 = GameObject.Find("btn_m10buy").GetComponent<Button>();
        txt_buyManager10 = GameObject.Find("txt_m10buy").GetComponent<Text>();
        txt_costManager10 = GameObject.Find("txt_m10cost").GetComponent<Text>();
        txt_costManager10.text = "$" + scoreTrackingScript.SimplifyScore(flt_costManager10);

        flt_costManager11 = constantsScript.flt_initialM11Cost;
        gmo_manager11 = GameObject.Find("Manager11");
        btn_buyManager11 = GameObject.Find("btn_m11buy").GetComponent<Button>();
        txt_buyManager11 = GameObject.Find("txt_m11buy").GetComponent<Text>();
        txt_costManager11 = GameObject.Find("txt_m11cost").GetComponent<Text>();
        txt_costManager11.text = "$" + scoreTrackingScript.SimplifyScore(flt_costManager11);

        flt_costManager12 = constantsScript.flt_initialM12Cost;
        gmo_manager12 = GameObject.Find("Manager12");
        btn_buyManager12 = GameObject.Find("btn_m12buy").GetComponent<Button>();
        txt_buyManager12 = GameObject.Find("txt_m12buy").GetComponent<Text>();
        txt_costManager12 = GameObject.Find("txt_m12cost").GetComponent<Text>();
        txt_costManager12.text = "$" + scoreTrackingScript.SimplifyScore(flt_costManager12);

        flt_costManager13 = constantsScript.flt_initialM13Cost;
        gmo_manager13 = GameObject.Find("Manager13");
        btn_buyManager13 = GameObject.Find("btn_m13buy").GetComponent<Button>();
        txt_buyManager13 = GameObject.Find("txt_m13buy").GetComponent<Text>();
        txt_costManager13 = GameObject.Find("txt_m13cost").GetComponent<Text>();
        txt_costManager13.text = "$" + scoreTrackingScript.SimplifyScore(flt_costManager13);

        flt_costManager14 = constantsScript.flt_initialM14Cost;
        gmo_manager14 = GameObject.Find("Manager14");
        btn_buyManager14 = GameObject.Find("btn_m14buy").GetComponent<Button>();
        txt_buyManager14 = GameObject.Find("txt_m14buy").GetComponent<Text>();
        txt_costManager14 = GameObject.Find("txt_m14cost").GetComponent<Text>();
        txt_costManager14.text = "$" + scoreTrackingScript.SimplifyScore(flt_costManager14);

        flt_costManager15 = constantsScript.flt_initialM15Cost;
        gmo_manager15 = GameObject.Find("Manager15");
        btn_buyManager15 = GameObject.Find("btn_m15buy").GetComponent<Button>();
        txt_buyManager15 = GameObject.Find("txt_m15buy").GetComponent<Text>();
        txt_costManager15 = GameObject.Find("txt_m15cost").GetComponent<Text>();
        txt_costManager15.text = "$" + scoreTrackingScript.SimplifyScore(flt_costManager15);

        flt_costManager16 = constantsScript.flt_initialM16Cost;
        gmo_manager16 = GameObject.Find("Manager16");
        btn_buyManager16 = GameObject.Find("btn_m16buy").GetComponent<Button>();
        txt_buyManager16 = GameObject.Find("txt_m16buy").GetComponent<Text>();
        txt_costManager16 = GameObject.Find("txt_m16cost").GetComponent<Text>();
        txt_costManager16.text = "$" + scoreTrackingScript.SimplifyScore(flt_costManager16);

        flt_costManager17 = constantsScript.flt_initialM17Cost;
        gmo_manager17 = GameObject.Find("Manager17");
        btn_buyManager17 = GameObject.Find("btn_m17buy").GetComponent<Button>();
        txt_buyManager17 = GameObject.Find("txt_m17buy").GetComponent<Text>();
        txt_costManager17 = GameObject.Find("txt_m17cost").GetComponent<Text>();
        txt_costManager17.text = "$" + scoreTrackingScript.SimplifyScore(flt_costManager17);

        flt_costManager18 = constantsScript.flt_initialM18Cost;
        gmo_manager18 = GameObject.Find("Manager18");
        btn_buyManager18 = GameObject.Find("btn_m18buy").GetComponent<Button>();
        txt_buyManager18 = GameObject.Find("txt_m18buy").GetComponent<Text>();
        txt_costManager18 = GameObject.Find("txt_m18cost").GetComponent<Text>();
        txt_costManager18.text = "$" + scoreTrackingScript.SimplifyScore(flt_costManager18);

        flt_costManager19 = constantsScript.flt_initialM19Cost;
        gmo_manager19 = GameObject.Find("Manager19");
        btn_buyManager19 = GameObject.Find("btn_m19buy").GetComponent<Button>();
        txt_buyManager19 = GameObject.Find("txt_m19buy").GetComponent<Text>();
        txt_costManager19 = GameObject.Find("txt_m19cost").GetComponent<Text>();
        txt_costManager19.text = "$" + scoreTrackingScript.SimplifyScore(flt_costManager19);

        flt_costManager20 = constantsScript.flt_initialM20Cost;
        gmo_manager20 = GameObject.Find("Manager20");
        btn_buyManager20 = GameObject.Find("btn_m20buy").GetComponent<Button>();
        txt_buyManager20 = GameObject.Find("txt_m20buy").GetComponent<Text>();
        txt_costManager20 = GameObject.Find("txt_m20cost").GetComponent<Text>();
        txt_costManager20.text = "$" + scoreTrackingScript.SimplifyScore(flt_costManager20);

        flt_costManager21 = constantsScript.flt_initialM21Cost;
        gmo_manager21 = GameObject.Find("Manager21");
        btn_buyManager21 = GameObject.Find("btn_m21buy").GetComponent<Button>();
        txt_buyManager21 = GameObject.Find("txt_m21buy").GetComponent<Text>();
        txt_costManager21 = GameObject.Find("txt_m21cost").GetComponent<Text>();
        txt_costManager21.text = "$" + scoreTrackingScript.SimplifyScore(flt_costManager21);

        flt_costManager22 = constantsScript.flt_initialM22Cost;
        gmo_manager22 = GameObject.Find("Manager22");
        btn_buyManager22 = GameObject.Find("btn_m22buy").GetComponent<Button>();
        txt_buyManager22 = GameObject.Find("txt_m22buy").GetComponent<Text>();
        txt_costManager22 = GameObject.Find("txt_m22cost").GetComponent<Text>();
        txt_costManager22.text = "$" + scoreTrackingScript.SimplifyScore(flt_costManager22);

        flt_costManager23 = constantsScript.flt_initialM23Cost;
        gmo_manager23 = GameObject.Find("Manager23");
        btn_buyManager23 = GameObject.Find("btn_m23buy").GetComponent<Button>();
        txt_buyManager23 = GameObject.Find("txt_m23buy").GetComponent<Text>();
        txt_costManager23 = GameObject.Find("txt_m23cost").GetComponent<Text>();
        txt_costManager23.text = "$" + scoreTrackingScript.SimplifyScore(flt_costManager23);

        flt_costManager24 = constantsScript.flt_initialM24Cost;
        gmo_manager24 = GameObject.Find("Manager24");
        btn_buyManager24 = GameObject.Find("btn_m24buy").GetComponent<Button>();
        txt_buyManager24 = GameObject.Find("txt_m24buy").GetComponent<Text>();
        txt_costManager24 = GameObject.Find("txt_m24cost").GetComponent<Text>();
        txt_costManager24.text = "$" + scoreTrackingScript.SimplifyScore(flt_costManager24);

        flt_costManager25 = constantsScript.flt_initialM25Cost;
        gmo_manager25 = GameObject.Find("Manager25");
        btn_buyManager25 = GameObject.Find("btn_m25buy").GetComponent<Button>();
        txt_buyManager25 = GameObject.Find("txt_m25buy").GetComponent<Text>();
        txt_costManager25 = GameObject.Find("txt_m25cost").GetComponent<Text>();
        txt_costManager25.text = "$" + scoreTrackingScript.SimplifyScore(flt_costManager25);

        flt_costManager26 = constantsScript.flt_initialM26Cost;
        gmo_manager26 = GameObject.Find("Manager26");
        btn_buyManager26 = GameObject.Find("btn_m26buy").GetComponent<Button>();
        txt_buyManager26 = GameObject.Find("txt_m26buy").GetComponent<Text>();
        txt_costManager26 = GameObject.Find("txt_m26cost").GetComponent<Text>();
        txt_costManager26.text = "$" + scoreTrackingScript.SimplifyScore(flt_costManager26);

        flt_costManager27 = constantsScript.flt_initialM27Cost;
        gmo_manager27 = GameObject.Find("Manager27");
        btn_buyManager27 = GameObject.Find("btn_m27buy").GetComponent<Button>();
        txt_buyManager27 = GameObject.Find("txt_m27buy").GetComponent<Text>();
        txt_costManager27 = GameObject.Find("txt_m27cost").GetComponent<Text>();
        txt_costManager27.text = "$" + scoreTrackingScript.SimplifyScore(flt_costManager27);

        flt_costManager28 = constantsScript.flt_initialM28Cost;
        gmo_manager28 = GameObject.Find("Manager28");
        btn_buyManager28 = GameObject.Find("btn_m28buy").GetComponent<Button>();
        txt_buyManager28 = GameObject.Find("txt_m28buy").GetComponent<Text>();
        txt_costManager28 = GameObject.Find("txt_m28cost").GetComponent<Text>();
        txt_costManager28.text = "$" + scoreTrackingScript.SimplifyScore(flt_costManager28);

        flt_costManager29 = constantsScript.flt_initialM29Cost;
        gmo_manager29 = GameObject.Find("Manager29");
        btn_buyManager29 = GameObject.Find("btn_m29buy").GetComponent<Button>();
        txt_buyManager29 = GameObject.Find("txt_m29buy").GetComponent<Text>();
        txt_costManager29 = GameObject.Find("txt_m29cost").GetComponent<Text>();
        txt_costManager29.text = "$" + scoreTrackingScript.SimplifyScore(flt_costManager29);

        flt_costManager30 = constantsScript.flt_initialM30Cost;
        gmo_manager30 = GameObject.Find("Manager30");
        btn_buyManager30 = GameObject.Find("btn_m30buy").GetComponent<Button>();
        txt_buyManager30 = GameObject.Find("txt_m30buy").GetComponent<Text>();
        txt_costManager30 = GameObject.Find("txt_m30cost").GetComponent<Text>();
        txt_costManager30.text = "$" + scoreTrackingScript.SimplifyScore(flt_costManager30);

        arr_managerObjects = new GameObject[] {gmo_manager1, gmo_manager2, gmo_manager3, gmo_manager4, gmo_manager5, gmo_manager6,
            gmo_manager7, gmo_manager8, gmo_manager9, gmo_manager10, gmo_manager11, gmo_manager12, gmo_manager13, gmo_manager14,
            gmo_manager15, gmo_manager16, gmo_manager17, gmo_manager18, gmo_manager19, gmo_manager20, gmo_manager21, gmo_manager22,
            gmo_manager23, gmo_manager24, gmo_manager25, gmo_manager26, gmo_manager27, gmo_manager28, gmo_manager29, gmo_manager30};
        
        foreach (GameObject gmo_manager in arr_managerObjects) {
            arr_managerStartingPositions.Add(gmo_manager.transform.position);
        }

        gmo_managersCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (scoreTrackingScript != null)
        {
            dec_currentScore = scoreTrackingScript.GetScore();

            if (dec_currentScore >= flt_costManager1)
            {
                btn_buyManager1.interactable = true;
                txt_buyManager1.color = clr_available;
            }
            else
            {
                btn_buyManager1.interactable = false;
                txt_buyManager1.color = clr_unavailable;
            }

            if (dec_currentScore >= flt_costManager2)
            {
                btn_buyManager2.interactable = true;
                txt_buyManager2.color = clr_available;
            }
            else
            {
                btn_buyManager2.interactable = false;
                txt_buyManager2.color = clr_unavailable;
            }

            if (dec_currentScore >= flt_costManager3)
            {
                btn_buyManager3.interactable = true;
                txt_buyManager3.color = clr_available;
            }
            else
            {
                btn_buyManager3.interactable = false;
                txt_buyManager3.color = clr_unavailable;
            }

            if (dec_currentScore >= flt_costManager4)
            {
                btn_buyManager4.interactable = true;
                txt_buyManager4.color = clr_available;
            }
            else
            {
                btn_buyManager4.interactable = false;
                txt_buyManager4.color = clr_unavailable;
            }

            if (dec_currentScore >= flt_costManager5)
            {
                btn_buyManager5.interactable = true;
                txt_buyManager5.color = clr_available;
            }
            else
            {
                btn_buyManager5.interactable = false;
                txt_buyManager5.color = clr_unavailable;
            }

            if (dec_currentScore >= flt_costManager6)
            {
                btn_buyManager6.interactable = true;
                txt_buyManager6.color = clr_available;
            }
            else
            {
                btn_buyManager6.interactable = false;
                txt_buyManager6.color = clr_unavailable;
            }

            if (dec_currentScore >= flt_costManager7)
            {
                btn_buyManager7.interactable = true;
                txt_buyManager7.color = clr_available;
            }
            else
            {
                btn_buyManager7.interactable = false;
                txt_buyManager7.color = clr_unavailable;
            }

            if (dec_currentScore >= flt_costManager8)
            {
                btn_buyManager8.interactable = true;
                txt_buyManager8.color = clr_available;
            }
            else
            {
                btn_buyManager8.interactable = false;
                txt_buyManager8.color = clr_unavailable;
            }

            if (dec_currentScore >= flt_costManager9)
            {
                btn_buyManager9.interactable = true;
                txt_buyManager9.color = clr_available;
            }
            else
            {
                btn_buyManager9.interactable = false;
                txt_buyManager9.color = clr_unavailable;
            }

            if (dec_currentScore >= flt_costManager10)
            {
                btn_buyManager10.interactable = true;
                txt_buyManager10.color = clr_available;
            }
            else
            {
                btn_buyManager10.interactable = false;
                txt_buyManager10.color = clr_unavailable;
            }

            if (dec_currentScore >= flt_costManager11)
            {
                btn_buyManager11.interactable = true;
                txt_buyManager11.color = clr_available;
            }
            else
            {
                btn_buyManager11.interactable = false;
                txt_buyManager11.color = clr_unavailable;
            }

            if (dec_currentScore >= flt_costManager12)
            {
                btn_buyManager12.interactable = true;
                txt_buyManager12.color = clr_available;
            }
            else
            {
                btn_buyManager12.interactable = false;
                txt_buyManager12.color = clr_unavailable;
            }

            if (dec_currentScore >= flt_costManager13)
            {
                btn_buyManager13.interactable = true;
                txt_buyManager13.color = clr_available;
            }
            else
            {
                btn_buyManager13.interactable = false;
                txt_buyManager13.color = clr_unavailable;
            }

            if (dec_currentScore >= flt_costManager14)
            {
                btn_buyManager14.interactable = true;
                txt_buyManager14.color = clr_available;
            }
            else
            {
                btn_buyManager14.interactable = false;
                txt_buyManager14.color = clr_unavailable;
            }

            if (dec_currentScore >= flt_costManager15)
            {
                btn_buyManager15.interactable = true;
                txt_buyManager15.color = clr_available;
            }
            else
            {
                btn_buyManager15.interactable = false;
                txt_buyManager15.color = clr_unavailable;
            }

            if (dec_currentScore >= flt_costManager16)
            {
                btn_buyManager16.interactable = true;
                txt_buyManager16.color = clr_available;
            }
            else
            {
                btn_buyManager16.interactable = false;
                txt_buyManager16.color = clr_unavailable;
            }

            if (dec_currentScore >= flt_costManager17)
            {
                btn_buyManager17.interactable = true;
                txt_buyManager17.color = clr_available;
            }
            else
            {
                btn_buyManager17.interactable = false;
                txt_buyManager17.color = clr_unavailable;
            }

            if (dec_currentScore >= flt_costManager18)
            {
                btn_buyManager18.interactable = true;
                txt_buyManager18.color = clr_available;
            }
            else
            {
                btn_buyManager18.interactable = false;
                txt_buyManager18.color = clr_unavailable;
            }

            if (dec_currentScore >= flt_costManager19)
            {
                btn_buyManager19.interactable = true;
                txt_buyManager19.color = clr_available;
            }
            else
            {
                btn_buyManager19.interactable = false;
                txt_buyManager19.color = clr_unavailable;
            }

            if (dec_currentScore >= flt_costManager20)
            {
                btn_buyManager20.interactable = true;
                txt_buyManager20.color = clr_available;
            }
            else
            {
                btn_buyManager20.interactable = false;
                txt_buyManager20.color = clr_unavailable;
            }

            if (dec_currentScore >= flt_costManager21)
            {
                btn_buyManager21.interactable = true;
                txt_buyManager21.color = clr_available;
            }
            else
            {
                btn_buyManager21.interactable = false;
                txt_buyManager21.color = clr_unavailable;
            }

            if (dec_currentScore >= flt_costManager22)
            {
                btn_buyManager22.interactable = true;
                txt_buyManager22.color = clr_available;
            }
            else
            {
                btn_buyManager22.interactable = false;
                txt_buyManager22.color = clr_unavailable;
            }

            if (dec_currentScore >= flt_costManager23)
            {
                btn_buyManager23.interactable = true;
                txt_buyManager23.color = clr_available;
            }
            else
            {
                btn_buyManager23.interactable = false;
                txt_buyManager23.color = clr_unavailable;
            }

            if (dec_currentScore >= flt_costManager24)
            {
                btn_buyManager24.interactable = true;
                txt_buyManager24.color = clr_available;
            }
            else
            {
                btn_buyManager24.interactable = false;
                txt_buyManager24.color = clr_unavailable;
            }

            if (dec_currentScore >= flt_costManager25)
            {
                btn_buyManager25.interactable = true;
                txt_buyManager25.color = clr_available;
            }
            else
            {
                btn_buyManager25.interactable = false;
                txt_buyManager25.color = clr_unavailable;
            }

            if (dec_currentScore >= flt_costManager26)
            {
                btn_buyManager26.interactable = true;
                txt_buyManager26.color = clr_available;
            }
            else
            {
                btn_buyManager26.interactable = false;
                txt_buyManager26.color = clr_unavailable;
            }

            if (dec_currentScore >= flt_costManager27)
            {
                btn_buyManager27.interactable = true;
                txt_buyManager27.color = clr_available;
            }
            else
            {
                btn_buyManager27.interactable = false;
                txt_buyManager27.color = clr_unavailable;
            }

            if (dec_currentScore >= flt_costManager28)
            {
                btn_buyManager28.interactable = true;
                txt_buyManager28.color = clr_available;
            }
            else
            {
                btn_buyManager28.interactable = false;
                txt_buyManager28.color = clr_unavailable;
            }

            if (dec_currentScore >= flt_costManager29)
            {
                btn_buyManager29.interactable = true;
                txt_buyManager29.color = clr_available;
            }
            else
            {
                btn_buyManager29.interactable = false;
                txt_buyManager29.color = clr_unavailable;
            }

            if (dec_currentScore >= flt_costManager30)
            {
                btn_buyManager30.interactable = true;
                txt_buyManager30.color = clr_available;
            }
            else
            {
                btn_buyManager30.interactable = false;
                txt_buyManager30.color = clr_unavailable;
            }
        }
    }

    public void ManagerOpenClicked()
    {
        gmo_managersCanvas.SetActive(true);
        gmo_menuItems.SetActive(false);

        // Play button sound effect
        if ((PlayerPrefs.GetInt("Loaded") == 1) && (PlayerPrefs.GetInt("EffectsAudio") == 1)) {
            aud_buttonSound.Play();
        }
    }

    public void ManagerCloseClicked()
    {
        gmo_managersCanvas.SetActive(false);

        // Play button sound effect
        if ((PlayerPrefs.GetInt("Loaded") == 1) && (PlayerPrefs.GetInt("EffectsAudio") == 1)) {
            aud_buttonSound.Play();
        }
    }

    void ManagerPurchased(int int_managerPurchased)
    {
        float yOffset = constantsScript.flt_managerOffset;

        for (int i = int_managerPurchased - 1; i < arr_managerObjects.Length; i++)
        {
            GameObject gmo_manager = arr_managerObjects[i];
            gmo_manager.transform.localPosition = new Vector3(gmo_manager.transform.localPosition.x,
                gmo_manager.transform.localPosition.y + yOffset, gmo_manager.transform.localPosition.z);
        }

        tsf_managersContent.sizeDelta -= new Vector2(0, yOffset);

        // Play purchase sound effect
        if ((PlayerPrefs.GetInt("Loaded") == 1) && (PlayerPrefs.GetInt("EffectsAudio") == 1)) {
            aud_purchaseSound.Play();
        }
    }

    public void Prestige()
    {
        for (int i = 0; i < arr_managerObjects.Length; i++)
        {
            GameObject gmo_manager = arr_managerObjects[i];
            gmo_manager.transform.position = arr_managerStartingPositions[i];
            gmo_manager.SetActive(true);
        }
    }

    public void BuyManager1Clicked()
    {
        dec_currentScore = scoreTrackingScript.GetScore();
        if (dec_currentScore >= flt_costManager1)
        {
            scoreTrackingScript.MinusScore(flt_costManager1);
            stage1Script.PurchaseManager();
            ManagerPurchased(1);
            gmo_manager1.SetActive(false);
        }
    }

    public void BuyManager2Clicked()
    {
        dec_currentScore = scoreTrackingScript.GetScore();
        if (dec_currentScore >= flt_costManager2)
        {
            scoreTrackingScript.MinusScore(flt_costManager2);
            stage2Script.PurchaseManager();
            ManagerPurchased(2);
            gmo_manager2.SetActive(false);
        }
    }

    public void BuyManager3Clicked()
    {
        dec_currentScore = scoreTrackingScript.GetScore();
        if (dec_currentScore >= flt_costManager3)
        {
            scoreTrackingScript.MinusScore(flt_costManager3);
            stage3Script.PurchaseManager();
            ManagerPurchased(3);
            gmo_manager3.SetActive(false);
        }
    }

    public void BuyManager4Clicked()
    {
        dec_currentScore = scoreTrackingScript.GetScore();
        if (dec_currentScore >= flt_costManager4)
        {
            scoreTrackingScript.MinusScore(flt_costManager4);
            stage4Script.PurchaseManager();
            ManagerPurchased(4);
            gmo_manager4.SetActive(false);
        }
    }

    public void BuyManager5Clicked()
    {
        dec_currentScore = scoreTrackingScript.GetScore();
        if (dec_currentScore >= flt_costManager5)
        {
            scoreTrackingScript.MinusScore(flt_costManager5);
            stage5Script.PurchaseManager();
            ManagerPurchased(5);
            gmo_manager5.SetActive(false);
        }
    }

    public void BuyManager6Clicked()
    {
        dec_currentScore = scoreTrackingScript.GetScore();
        if (dec_currentScore >= flt_costManager6)
        {
            scoreTrackingScript.MinusScore(flt_costManager6);
            stage6Script.PurchaseManager();
            ManagerPurchased(6);
            gmo_manager6.SetActive(false);
        }
    }

    public void BuyManager7Clicked()
    {
        dec_currentScore = scoreTrackingScript.GetScore();
        if (dec_currentScore >= flt_costManager7)
        {
            scoreTrackingScript.MinusScore(flt_costManager7);
            stage7Script.PurchaseManager();
            ManagerPurchased(7);
            gmo_manager7.SetActive(false);
        }
    }

    public void BuyManager8Clicked()
    {
        dec_currentScore = scoreTrackingScript.GetScore();
        if (dec_currentScore >= flt_costManager8)
        {
            scoreTrackingScript.MinusScore(flt_costManager8);
            stage8Script.PurchaseManager();
            ManagerPurchased(8);
            gmo_manager8.SetActive(false);
        }
    }

    public void BuyManager9Clicked()
    {
        dec_currentScore = scoreTrackingScript.GetScore();
        if (dec_currentScore >= flt_costManager9)
        {
            scoreTrackingScript.MinusScore(flt_costManager9);
            stage9Script.PurchaseManager();
            ManagerPurchased(9);
            gmo_manager9.SetActive(false);
        }
    }

    public void BuyManager10Clicked()
    {
        dec_currentScore = scoreTrackingScript.GetScore();
        if (dec_currentScore >= flt_costManager10)
        {
            scoreTrackingScript.MinusScore(flt_costManager10);
            stage10Script.PurchaseManager();
            ManagerPurchased(10);
            gmo_manager10.SetActive(false);
        }
    }

    public void BuyManager11Clicked()
    {
        dec_currentScore = scoreTrackingScript.GetScore();
        if (dec_currentScore >= flt_costManager11)
        {
            scoreTrackingScript.MinusScore(flt_costManager11);
            stage1Script.PurchaseManagerReward();
            ManagerPurchased(11);
            gmo_manager11.SetActive(false);
            PlayerPrefs.SetInt("Stage1ManagerReward1", 1);
        }
    }

    public void BuyManager12Clicked()
    {
        dec_currentScore = scoreTrackingScript.GetScore();
        if (dec_currentScore >= flt_costManager12)
        {
            scoreTrackingScript.MinusScore(flt_costManager12);
            stage2Script.PurchaseManagerReward();
            ManagerPurchased(12);
            gmo_manager12.SetActive(false);
            PlayerPrefs.SetInt("Stage2ManagerReward1", 1);
        }
    }

    public void BuyManager13Clicked()
    {
        dec_currentScore = scoreTrackingScript.GetScore();
        if (dec_currentScore >= flt_costManager13)
        {
            scoreTrackingScript.MinusScore(flt_costManager13);
            stage3Script.PurchaseManagerReward();
            ManagerPurchased(13);
            gmo_manager13.SetActive(false);
            PlayerPrefs.SetInt("Stage3ManagerReward1", 1);
        }
    }

    public void BuyManager14Clicked()
    {
        dec_currentScore = scoreTrackingScript.GetScore();
        if (dec_currentScore >= flt_costManager14)
        {
            scoreTrackingScript.MinusScore(flt_costManager14);
            stage4Script.PurchaseManagerReward();
            ManagerPurchased(14);
            gmo_manager14.SetActive(false);
            PlayerPrefs.SetInt("Stage4ManagerReward1", 1);
        }
    }

    public void BuyManager15Clicked()
    {
        dec_currentScore = scoreTrackingScript.GetScore();
        if (dec_currentScore >= flt_costManager15)
        {
            scoreTrackingScript.MinusScore(flt_costManager15);
            stage5Script.PurchaseManagerReward();
            ManagerPurchased(15);
            gmo_manager15.SetActive(false);
            PlayerPrefs.SetInt("Stage5ManagerReward1", 1);
        }
    }

    public void BuyManager16Clicked()
    {
        dec_currentScore = scoreTrackingScript.GetScore();
        if (dec_currentScore >= flt_costManager16)
        {
            scoreTrackingScript.MinusScore(flt_costManager16);
            stage6Script.PurchaseManagerReward();
            ManagerPurchased(16);
            gmo_manager16.SetActive(false);
            PlayerPrefs.SetInt("Stage6ManagerReward1", 1);
        }
    }

    public void BuyManager17Clicked()
    {
        dec_currentScore = scoreTrackingScript.GetScore();
        if (dec_currentScore >= flt_costManager17)
        {
            scoreTrackingScript.MinusScore(flt_costManager17);
            stage7Script.PurchaseManagerReward();
            ManagerPurchased(17);
            gmo_manager17.SetActive(false);
            PlayerPrefs.SetInt("Stage7ManagerReward1", 1);
        }
    }

    public void BuyManager18Clicked()
    {
        dec_currentScore = scoreTrackingScript.GetScore();
        if (dec_currentScore >= flt_costManager18)
        {
            scoreTrackingScript.MinusScore(flt_costManager18);
            stage8Script.PurchaseManagerReward();
            ManagerPurchased(18);
            gmo_manager18.SetActive(false);
            PlayerPrefs.SetInt("Stage8ManagerReward1", 1);
        }
    }

    public void BuyManager19Clicked()
    {
        dec_currentScore = scoreTrackingScript.GetScore();
        if (dec_currentScore >= flt_costManager19)
        {
            scoreTrackingScript.MinusScore(flt_costManager19);
            stage9Script.PurchaseManagerReward();
            ManagerPurchased(19);
            gmo_manager19.SetActive(false);
            PlayerPrefs.SetInt("Stage9ManagerReward1", 1);
        }
    }

    public void BuyManager20Clicked()
    {
        dec_currentScore = scoreTrackingScript.GetScore();
        if (dec_currentScore >= flt_costManager20)
        {
            scoreTrackingScript.MinusScore(flt_costManager20);
            stage10Script.PurchaseManagerReward();
            ManagerPurchased(20);
            gmo_manager20.SetActive(false);
            PlayerPrefs.SetInt("Stage10ManagerReward1", 1);
        }
    }

    public void BuyManager21Clicked()
    {
        dec_currentScore = scoreTrackingScript.GetScore();
        if (dec_currentScore >= flt_costManager21)
        {
            scoreTrackingScript.MinusScore(flt_costManager21);
            stage1Script.PurchaseManagerTime();
            ManagerPurchased(21);
            gmo_manager21.SetActive(false);
            PlayerPrefs.SetInt("Stage1ManagerTime1", 1);
        }
    }

    public void BuyManager22Clicked()
    {
        dec_currentScore = scoreTrackingScript.GetScore();
        if (dec_currentScore >= flt_costManager22)
        {
            scoreTrackingScript.MinusScore(flt_costManager22);
            stage2Script.PurchaseManagerTime();
            ManagerPurchased(22);
            gmo_manager22.SetActive(false);
            PlayerPrefs.SetInt("Stage2ManagerTime1", 1);
        }
    }

    public void BuyManager23Clicked()
    {
        dec_currentScore = scoreTrackingScript.GetScore();
        if (dec_currentScore >= flt_costManager23)
        {
            scoreTrackingScript.MinusScore(flt_costManager23);
            stage3Script.PurchaseManagerTime();
            ManagerPurchased(23);
            gmo_manager23.SetActive(false);
            PlayerPrefs.SetInt("Stage3ManagerTime1", 1);
        }
    }

    public void BuyManager24Clicked()
    {
        dec_currentScore = scoreTrackingScript.GetScore();
        if (dec_currentScore >= flt_costManager24)
        {
            scoreTrackingScript.MinusScore(flt_costManager24);
            stage4Script.PurchaseManagerTime();
            ManagerPurchased(24);
            gmo_manager24.SetActive(false);
            PlayerPrefs.SetInt("Stage4ManagerTime1", 1);
        }
    }

    public void BuyManager25Clicked()
    {
        dec_currentScore = scoreTrackingScript.GetScore();
        if (dec_currentScore >= flt_costManager25)
        {
            scoreTrackingScript.MinusScore(flt_costManager25);
            stage5Script.PurchaseManagerTime();
            ManagerPurchased(25);
            gmo_manager25.SetActive(false);
            PlayerPrefs.SetInt("Stage5ManagerTime1", 1);
        }
    }

    public void BuyManager26Clicked()
    {
        dec_currentScore = scoreTrackingScript.GetScore();
        if (dec_currentScore >= flt_costManager26)
        {
            scoreTrackingScript.MinusScore(flt_costManager26);
            stage6Script.PurchaseManagerTime();
            ManagerPurchased(26);
            gmo_manager26.SetActive(false);
            PlayerPrefs.SetInt("Stage6ManagerTime1", 1);
        }
    }

    public void BuyManager27Clicked()
    {
        dec_currentScore = scoreTrackingScript.GetScore();
        if (dec_currentScore >= flt_costManager27)
        {
            scoreTrackingScript.MinusScore(flt_costManager27);
            stage7Script.PurchaseManagerTime();
            ManagerPurchased(27);
            gmo_manager27.SetActive(false);
            PlayerPrefs.SetInt("Stage7ManagerTime1", 1);
        }
    }

    public void BuyManager28Clicked()
    {
        dec_currentScore = scoreTrackingScript.GetScore();
        if (dec_currentScore >= flt_costManager28)
        {
            scoreTrackingScript.MinusScore(flt_costManager28);
            stage8Script.PurchaseManagerTime();
            ManagerPurchased(28);
            gmo_manager28.SetActive(false);
            PlayerPrefs.SetInt("Stage8ManagerTime1", 1);
        }
    }

    public void BuyManager29Clicked()
    {
        dec_currentScore = scoreTrackingScript.GetScore();
        if (dec_currentScore >= flt_costManager29)
        {
            scoreTrackingScript.MinusScore(flt_costManager29);
            stage9Script.PurchaseManagerTime();
            ManagerPurchased(29);
            gmo_manager29.SetActive(false);
            PlayerPrefs.SetInt("Stage9ManagerTime1", 1);
        }
    }

    public void BuyManager30Clicked()
    {
        dec_currentScore = scoreTrackingScript.GetScore();
        if (dec_currentScore >= flt_costManager30)
        {
            scoreTrackingScript.MinusScore(flt_costManager30);
            stage10Script.PurchaseManagerTime();
            ManagerPurchased(30);
            gmo_manager30.SetActive(false);
            PlayerPrefs.SetInt("Stage10ManagerTime1", 1);
        }
    }
}
