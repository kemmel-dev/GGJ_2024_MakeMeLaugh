using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using JoinScene;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MiniGamePreviewPanel : MonoBehaviour
{
    public Color _highlightColor;
    public Color _normalColor;
    public List<MiniGamePreview> miniGamePreviews;

    private Transform child;
    
    private void Awake()
    {
        child = transform.GetChild(0);
        child.gameObject.SetActive(false);
    }

    public async void StartMiniGamePicker(int pickedMiniGame)//37
    {
        child.gameObject.SetActive(true);
        var numMiniGame = pickedMiniGame - 3;//04
        var currentIndex = 0;
        var delay = 100;
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                miniGamePreviews[currentIndex % 5].highlight.color = _highlightColor;
                miniGamePreviews[currentIndex - 1 < 0 ? 4 : (currentIndex - 1) % 5].highlight.color = _normalColor;
                if (i == 2 && (currentIndex % 5 ) == numMiniGame)
                {
                    await Task.Delay(2000);
                    SceneManager.LoadScene(pickedMiniGame);
                    break;
                }
                
                await Task.Delay((delay += 50));
                currentIndex++;
            }
        }
    }
}
