using UnityEngine;
using System.Collections;

public class WandController : SteamVR_TrackedController {

	public SteamVR_Controller.Device controller
    {
        get
        {
            return SteamVR_Controller.Input((int)controllerIndex);
        }
    }
    public Vector3 velocity
    {
        get
        {
            return controller.velocity;
        }
    }
    public Vector3 angularVelocity
    {
        get
        {
            return controller.angularVelocity;
        }
    }

    protected LineRenderer lineRenderer;
    public LineRenderer innerGlowRenderer;
    protected Vector3[] lineRendererVertices;
    protected Vector3[] innerGlowVertices;

    public Transform hiltTransform;
    private float bladeEffectOffset = 0.0f;
    // Use this for initialization
	protected override void Start ()
    {
        base.Start();

        //lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer = gameObject.GetComponent<LineRenderer>();
        //lineRenderer.material = new Material(Shader.Find("Particles/Additive"));
        //lineRenderer.SetWidth(0.01f, 0.01f);
        //lineRenderer.SetVertexCount(2);

        lineRendererVertices = new Vector3[2];
        innerGlowVertices = new Vector3[2];

	}
	
	// Update is called once per frame
	protected override void Update ()
    {
        base.Update();

        if(lineRenderer && lineRenderer.enabled && hiltTransform && innerGlowRenderer && innerGlowRenderer.enabled)
        {
            // create length and position of blade
            Vector3 startPos = hiltTransform.position;
            lineRendererVertices[0] = startPos;
            lineRendererVertices[1] = startPos + (hiltTransform.up*0.75f);
            lineRenderer.SetPositions(lineRendererVertices);

            // vibrate effect texture
            bladeEffectOffset -= Time.deltaTime * 2f;
            if(bladeEffectOffset < -10f)
            {
                bladeEffectOffset += 10f;
            }
            lineRenderer.sharedMaterials[1].SetTextureOffset("_MainTex",new Vector2(bladeEffectOffset, 0.0f));

            //create length and position of inner glow
            innerGlowVertices[0] = startPos;
            innerGlowVertices[1] = startPos + (hiltTransform.up * 0.75f);
            innerGlowRenderer.SetPositions(innerGlowVertices);

        }
	}

    public override void OnTriggerClicked(ClickedEventArgs e)
    {
        base.OnTriggerClicked(e);

        if (transform.parent == null)
            return;
    }

    public override void OnTriggerUnclicked(ClickedEventArgs e)
    {
        base.OnTriggerUnclicked(e);
    }

    public override void OnMenuClicked(ClickedEventArgs e)
    {
        base.OnMenuClicked(e);
    }

    public override void OnMenuUnclicked(ClickedEventArgs e)
    {
        base.OnMenuUnclicked(e);
    }

    public override void OnSteamClicked(ClickedEventArgs e)
    {
        base.OnSteamClicked(e);
    }

    public override void OnPadClicked(ClickedEventArgs e)
    {
        base.OnPadClicked(e);
    }

    public override void OnPadUnclicked(ClickedEventArgs e)
    {
        base.OnPadUnclicked(e);
    }

    public override void OnPadTouched(ClickedEventArgs e)
    {
        base.OnPadTouched(e);
    }

    public override void OnPadUntouched(ClickedEventArgs e)
    {
        base.OnPadUntouched(e);
    }

    public override void OnGripped(ClickedEventArgs e)
    {
        base.OnGripped(e);
    }

    public override void OnUngripped(ClickedEventArgs e)
    {
        base.OnUngripped(e);
    }

    public float GetTriggerAxis()
    {
        if (controller == null)
            return 0;

        return controller.GetAxis(Valve.VR.EVRButtonId.k_EButton_Axis1).x;
    }

    public Vector2 GetTouchpadAxis()
    {
        if (controller == null)
            return new Vector2();

        return controller.GetAxis(Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad);
    }

}
