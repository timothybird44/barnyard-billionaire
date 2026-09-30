using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ExtendedNumerics;

public class constants : MonoBehaviour
{
    // gerneral constants
    public float flt_loadTime;

    // scoreTracking constants
    public string str_initialScore;
    public BigDecimal int_initialPrestigeCost;
    public float flt_initialPrestigeMultiplier, flt_prestigeCostMultiplier,
        flt_prestigeBonus, flt_goldBonusMultiplier, flt_prestigeBonusMultiplier, flt_initialOtherMultiplier;
    public int int_initialGold, int_initialGoldBonus;

    // score submission constants
    public float flt_walletSaveCooldown, flt_scoreSaveCooldown;

    // earn more constants
    public float flt_earnTimeReset, flt_earnRewardMultiplier;

    // managers constants
    public float flt_managerOffset;
    public BigDecimal flt_initialM1Cost, flt_initialM2Cost, flt_initialM3Cost, flt_initialM4Cost,
        flt_initialM5Cost, flt_initialM6Cost, flt_initialM7Cost, flt_initialM8Cost, flt_initialM9Cost, flt_initialM10Cost,
        flt_initialM11Cost, flt_initialM12Cost, flt_initialM13Cost, flt_initialM14Cost, flt_initialM15Cost, flt_initialM16Cost,
        flt_initialM17Cost, flt_initialM18Cost, flt_initialM19Cost, flt_initialM20Cost, flt_initialM21Cost, flt_initialM22Cost, 
        flt_initialM23Cost, flt_initialM24Cost, flt_initialM25Cost, flt_initialM26Cost, flt_initialM27Cost, flt_initialM28Cost, 
        flt_initialM29Cost, flt_initialM30Cost;

    // upgrades constants
    public int int_upgradeStageMultiplier, int_initialU1Cost, int_initialU2Cost, int_initialU3Cost, int_initialU4Cost,
        int_initialU5Cost, int_initialU6Cost, int_initialU7Cost, int_initialU8Cost, int_initialU9Cost, int_initialU10Cost,
        int_initialU11Cost, int_initialU12Cost;
    public float flt_upgradesCostMultiplier;

    // stages constants
    public float flt_initialRewardTimeMultiplier,
        flt_initialS1UnlockCost, flt_initialS1RewardTime, flt_initialS1CostMultiplier, flt_initialS1RewardMultiplier,
        flt_initialS2UnlockCost, flt_initialS2RewardTime, flt_initialS2CostMultiplier, flt_initialS2RewardMultiplier,
        flt_initialS3UnlockCost, flt_initialS3RewardTime, flt_initialS3CostMultiplier, flt_initialS3RewardMultiplier,
        flt_initialS4UnlockCost, flt_initialS4RewardTime, flt_initialS4CostMultiplier, flt_initialS4RewardMultiplier,
        flt_initialS5UnlockCost, flt_initialS5RewardTime, flt_initialS5CostMultiplier, flt_initialS5RewardMultiplier,
        flt_initialS6UnlockCost, flt_initialS6RewardTime, flt_initialS6CostMultiplier, flt_initialS6RewardMultiplier,
        flt_initialS7UnlockCost, flt_initialS7RewardTime, flt_initialS7CostMultiplier, flt_initialS7RewardMultiplier,
        flt_initialS8UnlockCost, flt_initialS8RewardTime, flt_initialS8CostMultiplier, flt_initialS8RewardMultiplier,
        flt_initialS9UnlockCost, flt_initialS9RewardTime, flt_initialS9CostMultiplier, flt_initialS9RewardMultiplier,
        flt_initialS10UnlockCost, flt_initialS10RewardTime, flt_initialS10CostMultiplier, flt_initialS10RewardMultiplier;
    public int int_initialLevel, int_intialSubLevel, int_initialMaxSubLevel, int_initialBuyAmount;
    public string str_initialBuyAmount;

    // Start is called before the first frame update
    private void Awake()
    {
        // general constants
        flt_loadTime = 0.035f;
            
        // scoreTracking constants
        str_initialScore = "5";
        int_initialGold = 0;
        int_initialGoldBonus = 10;
        flt_goldBonusMultiplier = 1.5f;
        int_initialPrestigeCost = BigDecimal.Parse("5000000000");
        flt_initialPrestigeMultiplier = 1f;
        flt_prestigeCostMultiplier = 2.5f;
        flt_prestigeBonus = 0.25f;
        flt_initialOtherMultiplier = 1f;

        // score submission constants
        flt_walletSaveCooldown = 1209600; // 1209600 seconds = 14 days
        flt_scoreSaveCooldown = 86400; // 86400 seconds = 24 hours

        // earn constants
        flt_earnTimeReset = 3600f; // 3600 seconds = 1 hour
        flt_earnRewardMultiplier = 2f;

        // managers constants
        flt_managerOffset = 300f;
        flt_initialM1Cost = BigDecimal.Parse("1000");
        flt_initialM2Cost = BigDecimal.Parse("5500");
        flt_initialM3Cost = BigDecimal.Parse("30250");
        flt_initialM4Cost = BigDecimal.Parse("166375");
        flt_initialM5Cost = BigDecimal.Parse("915062");
        flt_initialM6Cost = BigDecimal.Parse("5032843");
        flt_initialM7Cost = BigDecimal.Parse("27680640");
        flt_initialM8Cost = BigDecimal.Parse("152243523");
        flt_initialM9Cost = BigDecimal.Parse("837339378");
        flt_initialM10Cost = BigDecimal.Parse("4605366583");
        flt_initialM11Cost = BigDecimal.Parse("25329516211");
        flt_initialM12Cost = BigDecimal.Parse("139312339165");
        flt_initialM13Cost = BigDecimal.Parse("766217865410");
        flt_initialM14Cost = BigDecimal.Parse("4214198259757");
        flt_initialM15Cost = BigDecimal.Parse("23178090428664");
        flt_initialM16Cost = BigDecimal.Parse("127479497357655");
        flt_initialM17Cost = BigDecimal.Parse("701137235467104");
        flt_initialM18Cost = BigDecimal.Parse("3856254795069074");
        flt_initialM19Cost = BigDecimal.Parse("21209401372879911");
        flt_initialM20Cost = BigDecimal.Parse("116651707550839512");
        flt_initialM21Cost = BigDecimal.Parse("641584391529617318");
        flt_initialM22Cost = BigDecimal.Parse("3528714153412895250");
        flt_initialM23Cost = BigDecimal.Parse("19407927843770923879");
        flt_initialM24Cost = BigDecimal.Parse("106743603141000000000");
        flt_initialM25Cost = BigDecimal.Parse("587089817270000000000");
        flt_initialM26Cost = BigDecimal.Parse("3228993995000000000000");
        flt_initialM27Cost = BigDecimal.Parse("17759466973000000000000");
        flt_initialM28Cost = BigDecimal.Parse("97677068349000000000000");
        flt_initialM29Cost = BigDecimal.Parse("537223875920000000000000");
        flt_initialM30Cost = BigDecimal.Parse("2954731317600000000000000");


        //upgrades constants
        flt_upgradesCostMultiplier = 2f;
        int_upgradeStageMultiplier = 2;
        int_initialU1Cost = 5;
        int_initialU2Cost = 10;
        int_initialU3Cost = 4;
        int_initialU4Cost = 8;
        int_initialU5Cost = 12;
        int_initialU6Cost = 16;
        int_initialU7Cost = 20;
        int_initialU8Cost = 24;
        int_initialU9Cost = 28;
        int_initialU10Cost = 32;
        int_initialU11Cost = 36;
        int_initialU12Cost = 40;

        // stages constants
        int_initialLevel = 0;
        int_intialSubLevel = 0;
        int_initialMaxSubLevel = 25;
        int_initialBuyAmount = 1;
        str_initialBuyAmount = "x1";
        flt_initialRewardTimeMultiplier = 0.5f;

        flt_initialS1UnlockCost = 1f;
        flt_initialS1RewardTime = 1f;
        flt_initialS1CostMultiplier = 1.25f;
        flt_initialS1RewardMultiplier = 1f;

        flt_initialS2UnlockCost = 60f;
        flt_initialS2RewardTime = 2f;
        flt_initialS2CostMultiplier = 1.25f;
        flt_initialS2RewardMultiplier = 1f;

        flt_initialS3UnlockCost = 480f;
        flt_initialS3RewardTime = 5f;
        flt_initialS3CostMultiplier = 1.25f;
        flt_initialS3RewardMultiplier = 1f;

        flt_initialS4UnlockCost = 2400f;
        flt_initialS4RewardTime = 15f;
        flt_initialS4CostMultiplier = 1.25f;
        flt_initialS4RewardMultiplier = 1f;

        flt_initialS5UnlockCost = 10500f;
        flt_initialS5RewardTime = 90f;
        flt_initialS5CostMultiplier = 1.25f;
        flt_initialS5RewardMultiplier = 1f;

        flt_initialS6UnlockCost = 312000f;
        flt_initialS6RewardTime = 300f;
        flt_initialS6CostMultiplier = 1.25f;
        flt_initialS6RewardMultiplier = 1f;

        flt_initialS7UnlockCost = 1250000f;
        flt_initialS7RewardTime = 2700f;
        flt_initialS7CostMultiplier = 1.25f;
        flt_initialS7RewardMultiplier = 1f;

        flt_initialS8UnlockCost = 120000000f;
        flt_initialS8RewardTime = 9000f;
        flt_initialS8CostMultiplier = 1.25f;
        flt_initialS8RewardMultiplier = 1f;

        flt_initialS9UnlockCost = 750000000f;
        flt_initialS9RewardTime = 36000f;
        flt_initialS9CostMultiplier = 1.25f;
        flt_initialS9RewardMultiplier = 1f;

        flt_initialS10UnlockCost = 2400000000f;
        flt_initialS10RewardTime = 79200f;
        flt_initialS10CostMultiplier = 1.25f;
        flt_initialS10RewardMultiplier = 1f;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
