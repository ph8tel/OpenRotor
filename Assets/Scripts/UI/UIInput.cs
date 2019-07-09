﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIInput : MonoBehaviour {
	private ConfigDataManager config;
	private StandaloneInputModule input;

	void Start() {
		input = GetComponent<StandaloneInputModule>();
		GameObject go = GameObject.Find("dataManager");
		if (go == null) {
			Debug.LogError("FATAL: dataManager object not found!");
		}
		else {
			config = go.GetComponent<ConfigDataManager>();
			config.Reload();
		}
	}
    /*
    0: Throttle
    1: Roll
    2: Pitch
    3: Yaw
    4: Tilt
    5: FoV
    6: Exit
    7: Submit
    8: Reset
    9: Flip
    */

	void Update() {
		CustomInputAxis cancel = ((CustomInput)config.input).axis[6];
		CustomInputAxis submit = ((CustomInput)config.input).axis[7];
		CustomInputAxis horizontal = ((CustomInput)config.input).axis[1];
		CustomInputAxis vertical = ((CustomInput)config.input).axis[2];

		string cancelStr = "empty";
		string submitStr = "empty";
		string horizontalStr = "empty";
		string verticalStr = "empty";

		if (cancel is ButtonInputAxis) {
			cancelStr = ((ButtonInputAxis)cancel).btnHigh;
		}
		if (cancel is SimpleInputAxis) {
			cancelStr = ((SimpleInputAxis)cancel).axisName;
		}
		if (submit is ButtonInputAxis) {
			submitStr = ((ButtonInputAxis)submit).btnHigh;
		}
		if (submit is SimpleInputAxis) {
			submitStr = ((SimpleInputAxis)submit).axisName;
		}
		if (horizontal is SimpleInputAxis) {
			horizontalStr = ((SimpleInputAxis)horizontal).axisName;
		}
		if (vertical is SimpleInputAxis) {
			verticalStr = ((SimpleInputAxis)vertical).axisName;
		}

		input.cancelButton = cancelStr;
		input.submitButton = submitStr;
		input.horizontalAxis = horizontalStr;
		input.verticalAxis = verticalStr;
	}
}