using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectBaka
{
    public class ExitController : MonoBehaviour
    {
        /////////////////////////////////////////////////////////////////////////
        // 定数宣言
        /////////////////////////////////////////////////////////////////////////
        [SerializeField] PanelController kExitPanel = null;

        public void Resume()
        {
            kExitPanel.Disable();
            Time.timeScale = 1f;
            OverrideEventSystem.Instance.SetSelectedObject(OverrideEventSystem.Instance.GetLastSelected());
            Input.ResetInputAxes();
        }

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                if (kExitPanel.gameObject.activeInHierarchy)
                {
                    Resume();
                }
                else
                {
                    kExitPanel.gameObject.SetActive(true);
                    Time.timeScale = 0f;
                    OverrideEventSystem.Instance.SetSelectedObject(kExitPanel.transform.Find("Yes").gameObject);
                }
            }
        }
    }
}