using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorHandler : MonoBehaviour
{
    private const string COLOR_NAME_KEY = "COLOR_NAME_KEY";
    public Slider slider;
    public SpriteRenderer birdSprite;

    private void LoadColor()
    {
        slider.value = PlayerPrefs.GetFloat(COLOR_NAME_KEY);

        birdSprite.color = Color.HSVToRGB(slider.value, 0.85f, 0.85f); //Update color
    }

    public void OnSliderChanged()
    {
        birdSprite.color = Color.HSVToRGB(slider.value, 0.85f, 0.85f); //Update color

        CancelInvoke(nameof(SaveColor));

        Invoke(nameof(SaveColor), 0.5f);
    }

    private void SaveColor()
    {
        PlayerPrefs.SetFloat(COLOR_NAME_KEY, slider.value); //Save color to player prefs
    }
}
