using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class OSDManager : MonoBehaviour {
    public List<Sprite> osdSprites;

    public List<OSDElement> elements = new List<OSDElement>();

    private ConfigDataManager config;
    private int resXCache;
    private int resYCache;

    void Start() {
        GameObject go = GameObject.Find("dataManager");
		if (go == null) {
			Debug.LogError("FATAL: dataManager object not found!");
		}
		else {
			config = go.GetComponent<ConfigDataManager>();
			config.Reload();
		}
    }

    void Rebuild() {
        foreach (OSDElement elem in elements) {
            Destroy(elem.gameObject);
        }

        elements.Clear();
        foreach (string s in config.osdElements) {
            switch (s) {
                case "input":
                    elements.Add(new OSDInput());
                    elements.Last().Build(osdSprites);
                    break;
            }
        }
    }

    void Update() {
        if (config.uiRebuild || Screen.width != resXCache || Screen.height != resYCache) {
            Rebuild();
            config.uiRebuild = false;
            resXCache = Screen.width;
            resYCache = Screen.height;
        }

        foreach (OSDElement elem in elements) {
            elem.Update();
        }
    }
}
