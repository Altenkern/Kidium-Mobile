using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace PaintCraft.Demo.ColoringBook
{
    public class LoadSceneButtonController : MonoBehaviour
    {
        private void Start()
        {
            GetComponent<Button>().onClick.AddListener(
                () =>
                {
                    SceneManager.LoadScene("PageSelect");
                });
        }
    }
}