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

    private void Start()
    {
        StartMiniGamePicker(3);
    }

    public async void StartMiniGamePicker(int pickedMiniGame)
    {
        var currentIndex = 0;
        var delay = 100;
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                miniGamePreviews[currentIndex % 5].highlight.color = _highlightColor;
                miniGamePreviews[currentIndex - 1 < 0 ? 4 : (currentIndex - 1) % 5].highlight.color = _normalColor;
                if (i == 2 && (currentIndex % 5 ) == pickedMiniGame)
                {
                    await Task.Delay(2000);
                    // Switch to index of scene + 3 
                    SceneManager.LoadScene(currentIndex % 5 + 3);
                    break;
                }
                
                await Task.Delay((delay += 50));
                currentIndex++;
            }
        }
    }
}
