using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] Button playOneShotSound_btn;
    private void Start() {
        playOneShotSound_btn.onClick.AddListener(AudioManager.Instance.PlaySFX_Clap);
    }
}
