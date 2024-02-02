using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatCode : MonoBehaviour
{
    [SerializeField] BossParent bossParent;
    [SerializeField] BootUpPortal portal;
    [SerializeField] RespawnManager respawnManager;

    public List<Transform> checkpointPositions = new List<Transform>();

    bool toggleBossAtk;

    private void Start()
    {
        respawnManager.SpawnPlayerAtSpecificLocation(checkpointPositions[0]);
    }

    [ContextMenu("SkipToPhaseTwo")]
    public void SkipToPhaseTwo()
    {
        bossParent.ForcePhaseTwo();
        //bossParent.StopBossAttack();
    }

    [ContextMenu("SkipToEND")]
    public void SkipToEND()
    {
        portal.TurnOn();
    }

    public void SkipCheckpoint(int checkpoint)
    {
        respawnManager.SpawnPlayerAtSpecificLocation(checkpointPositions[checkpoint]);
    }

    public void ToggleBossAttack()
    {
        toggleBossAtk = !toggleBossAtk;
        if (toggleBossAtk)
        {
            bossParent.StopBossAttack();
        }
        else
        {
            bossParent.StartBossAttack();
        }
    }
}
