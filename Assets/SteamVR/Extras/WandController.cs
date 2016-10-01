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
    protected Vector3[] lineRendererVertices;
    
    // Use this for initialization
	protected override void Start ()
    {
        base.Start();

        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.material = new Material(Shader.Find("Particles/Additive"));
        lineRenderer.SetWidth(0.01f, 0.01f);
        lineRenderer.SetVertexCount(2);

        lineRendererVertices = new Vector3[2];
	}
	
	// Update is called once per frame
	protected override void Update ()
    {
        base.Update();

        if(lineRenderer && lineRenderer.enabled)
        {
            RaycastHit hit;
            Vector3 startPos = transform.position;

            if (Physics.Raycast(startPos, transform.forward, out hit, 1000.0f))
            {
                lineRendererVertices[1] = hit.point;
                lineRenderer.SetColors(Color.green, Color.green);
            }
            else
            {
                lineRendererVertices[1] = startPos + transform.forward * 1000.0f;
                lineRenderer.SetColors(Color.red, Color.red);
            }

            lineRendererVertices[0] = transform.position;
            lineRenderer.SetPositions(lineRendererVertices);
        }
	}

    public override void OnTriggerClicked(ClickedEventArgs e)
    {
        base.OnTriggerClicked(e);

        if (transform.parent == null)
            return;

        RaycastHit hit;
        Vector3 startPos = transform.position;

        if(Physics.Raycast(startPos, transform.forward, out hit, 1000.0f))
        {
            transform.parent.position = hit.point;
        }
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
