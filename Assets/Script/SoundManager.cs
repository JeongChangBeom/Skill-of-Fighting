using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    public AudioSource BGM;
    public AudioClip Stage01BGM;
    public AudioClip Stage02BGM;
    public AudioClip Stage03BGM;

    public AudioSource ButtonMouseOn_Audio;
    public AudioClip ButtonMouseOn;

    public AudioSource Click_Audio;
    public AudioClip Click;

    public AudioSource SceneChange_Audio;
    public AudioClip SceneChange;

    public AudioSource ExclamationMarkOn_Audio;
    public AudioClip ExclamationMarkOn;

    public AudioSource Key_Audio;
    public AudioClip Key;

    public AudioSource Shake_Audio;
    public AudioClip Shake;

    public AudioSource Arrow_Audio;
    public AudioClip Arrow;

    public AudioSource ParryArrow_Audio;
    public AudioClip ParryArrow;

    public AudioSource HunterWalk_Audio;
    public AudioClip HunterWalk;

    public AudioSource StageExclamationMarkOn_Audio;
    public AudioClip StageExclamationMarkOn;

    public AudioSource Smoke_Audio;
    public AudioClip Smoke;

    public AudioSource SnipeLoad_Audio;
    public AudioClip SnipeLoad;

    public AudioSource SnipeShoot_Audio;
    public AudioClip SnipeShoot;

    public AudioSource Land_Audio;
    public AudioClip Land;

    public AudioSource GunnerJump_Audio;
    public AudioClip GunnerJump;

    public AudioSource Skid_Audio;
    public AudioClip Skid;

    public AudioSource RandomShoot_Audio;
    public AudioClip RandomShoot;

    public AudioSource Shoot_Audio;
    public AudioClip Shoot;

    public AudioSource Bomb_Audio;
    public AudioClip Bomb;

    public AudioSource BombDrop_Audio;
    public AudioClip BombDrop;

    public AudioSource Grenade_Audio;
    public AudioClip Grenade;

    public AudioSource Smash_Audio;
    public AudioClip Smash;

    public AudioSource Punch01_Audio;
    public AudioClip Punch01;

    public AudioSource Punch02_Audio;
    public AudioClip Punch02;

    public AudioSource DestroyArm_Audio;
    public AudioClip DestroyArm;

    public AudioSource ImmortalDamage_Audio;
    public AudioClip ImmortalDamage;

    public AudioSource ImmortalDie1_Audio;
    public AudioClip ImmortalDie1;

    public AudioSource ImmortalDie2_Audio;
    public AudioClip ImmortalDie2;

    public AudioSource RaserBefore_Audio;
    public AudioClip RaserBefore;

    public AudioSource Raser_Audio;
    public AudioClip Raser;

    public AudioSource MissileBefore_Audio;
    public AudioClip MissileBefore;

    public AudioSource Missile_Audio;
    public AudioClip Missile;

    public AudioSource GuardianBackStep_Audio;
    public AudioClip GuardianBackStep;

    public AudioSource GuardianJump_Audio;
    public AudioClip GuardianJump;

    public AudioSource GuardianParry_Audio;
    public AudioClip GuardianParry;

    public AudioSource GuardianMove_Audio;
    public AudioClip GuardianMove;

    public static float BGMvolume = 1.0f;
    public static float SFXvolume = 1.0f;
    public static SoundManager instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = FindObjectOfType<SoundManager>();
            }
            return m_instance;
        }
    }

    private static SoundManager m_instance;

    private void Awake()
    {
        {
            if (instance != this)
            {
                Destroy(gameObject);
            }
        }
    }

    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "Stage01")
        {
            BGM.loop = true;
            BGM.clip = Stage01BGM;
            BGM.Play();
        }
        else if (SceneManager.GetActiveScene().name == "Stage02")
        {
            BGM.loop = true;
            BGM.clip = Stage02BGM;
            BGM.Play();
        }
        else if (SceneManager.GetActiveScene().name == "Stage03")
        {
            BGM.loop = true;
            BGM.clip = Stage03BGM;
            BGM.Play();
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Click_Sound();
        }
    }

    public void ButtonMouseOn_Sound()
    {
        ButtonMouseOn_Audio.PlayOneShot(ButtonMouseOn);
    }

    public void Click_Sound()
    {
        Click_Audio.PlayOneShot(Click);
    }

    public void SceneChange_Sound()
    {
        SceneChange_Audio.PlayOneShot(SceneChange);
    }

    public void ExclamationMarkOn_Sound()
    {
        ExclamationMarkOn_Audio.PlayOneShot(ExclamationMarkOn);
    }

    public void Key_Sound()
    {
        Key_Audio.PlayOneShot(Key);
    }

    public void Shake_Sound()
    {
        Shake_Audio.PlayOneShot(Shake);
    }

    public void Arrow_Sound()
    {
        Arrow_Audio.PlayOneShot(Arrow);
    }

    public void ParryArrow_Sound()
    {
        ParryArrow_Audio.PlayOneShot(ParryArrow);
    }

    public void HunterWalk_Sound()
    {
        HunterWalk_Audio.PlayOneShot(HunterWalk);
    }


    public void StageExclamationMarkOn_Sound()
    {
        StageExclamationMarkOn_Audio.PlayOneShot(StageExclamationMarkOn);
    }

    public void Smoke_Sound()
    {
        Smoke_Audio.PlayOneShot(Smoke);
    }

    public void SnipeLoad_Sound()
    {
        SnipeLoad_Audio.PlayOneShot(SnipeLoad);
    }

    public void SnipeShoot_Sound()
    {
        SnipeShoot_Audio.PlayOneShot(SnipeShoot);
    }

    public void Land_Sound()
    {
        Land_Audio.PlayOneShot(Land);
    }

    public void GunnerJump_Sound()
    {
        GunnerJump_Audio.PlayOneShot(GunnerJump);
    }

    public void Skid_Sound()
    {
        Skid_Audio.PlayOneShot(Skid);
    }

    public void RandomShoot_Sound()
    {
        RandomShoot_Audio.PlayOneShot(RandomShoot);
    }

    public void Shoot_Sound()
    {
        Shoot_Audio.PlayOneShot(Shoot);
    }

    public void Bomb_Sound()
    {
        Bomb_Audio.PlayOneShot(Bomb);
    }

    public void Grenade_Sound()
    {
        Grenade_Audio.PlayOneShot(Grenade);
    }

    public void Smash_Sound()
    {
        Smash_Audio.PlayOneShot(Smash);
    }

    public void Punch01_Sound()
    {
        Punch01_Audio.PlayOneShot(Punch01);
    }

    public void Punch02_Sound()
    {
        Punch02_Audio.PlayOneShot(Punch02);
    }

    public void DestroyArm_Sound()
    {
        DestroyArm_Audio.PlayOneShot(DestroyArm);
    }

    public void ImmortalDamage_Sound()
    {
        ImmortalDamage_Audio.PlayOneShot(ImmortalDamage);
    }

    public void RaserBefore_Sound()
    {
        RaserBefore_Audio.PlayOneShot(RaserBefore);
    }

    public void Raser_Sound()
    {
        Raser_Audio.PlayOneShot(Raser);
    }

    public void MissileBefore_Sound()
    {
        MissileBefore_Audio.PlayOneShot(MissileBefore);
    }

    public void Missile_Sound()
    {
        Missile_Audio.PlayOneShot(Missile);
    }

    public void BombDrop_Sound()
    {
        BombDrop_Audio.PlayOneShot(BombDrop);
    }

    public void GuardianBackStep_Sound()
    {
        GuardianBackStep_Audio.PlayOneShot(GuardianBackStep);
    }

    public void GuardianJump_Sound()
    {
        GuardianJump_Audio.PlayOneShot(GuardianJump);
    }

    public void GuardianParry_Sound()
    {
        GuardianParry_Audio.PlayOneShot(GuardianParry);
    }

    public void GuardianMove_Sound()
    {
        GuardianMove_Audio.PlayOneShot(GuardianMove);
    }

    public void ImmortalDie1_Sound()
    {
        ImmortalDie1_Audio.PlayOneShot(ImmortalDie1);
    }

    public void ImmortalDie2_Sound()
    {
        ImmortalDie2_Audio.PlayOneShot(ImmortalDie2);
    }
}
