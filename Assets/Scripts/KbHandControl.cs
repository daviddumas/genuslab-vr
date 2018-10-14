﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KbHandControl : MonoBehaviour {

    public OVRInput.Controller Controller;

    public GameObject camera;
    public GameObject stickHolder;
    public GameObject surface;
    public GameObject helpScreenParent;
    public GameObject h2view;
    public float h2speed = 3f;
    private StickBehavior sb;
    private h2viewcontrol h2c;
    private Quaternion stickInitQ, cameraInitQ, surfaceInitQ;
    private PaintableTexture pt = null;
    private HelpScreen helpScreen;
    private Vector3 surfaceDelta;



    void Start()
    {
        pt = PaintableTexture.Instance;
        // Todo: replace below with singletons to avoid need for linking in the editor
        sb = stickHolder.GetComponent<StickBehavior>();
        helpScreen = helpScreenParent.GetComponent<HelpScreen>();
        h2c = h2view.GetComponent<h2viewcontrol>();
        stickInitQ = stickHolder.transform.localRotation;
        cameraInitQ = camera.transform.localRotation;
        surfaceInitQ = surface.transform.localRotation;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void doQuit()
    {
        // Exit the application (builds) or stop the player (editor)
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }

    void OnApplicationQuit()
    {
        Cursor.lockState = CursorLockMode.None;
    }



    void Update()
    {
        float dt = Time.deltaTime;

        if (!sb.visible())
        {
            sb.makeVisible();
        }

        if (OVRInput.GetDown(OVRInput.RawButton.A))
        {
            // Clear all drawing on the PaintableTexture
            pt.Clear();
        }

        if (OVRInput.GetDown(OVRInput.RawButton.B))
        {
            // Klein-Poincare toggle
            h2c.Toggle();
            h2c.ExportMode();
        }

        if (OVRInput.GetDown(OVRInput.RawButton.RIndexTrigger))
        {
            sb.startDrawing();
        }
        else if (OVRInput.GetUp(OVRInput.RawButton.RIndexTrigger))
        {
            sb.stopDrawing();
        }
    }
}
