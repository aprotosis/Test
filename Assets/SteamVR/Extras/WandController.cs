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


    private AudioSource bladeHum;
    public AudioClip bladeExtendSound;
    public AudioClip bladeRetractSound;


    public Transform hiltTransform;
    public Transform bladeTransform;

    public MeshRenderer bladeRenderer;
    public float bladeExtendSpeed = 1;
    public float bladeRetractSpeed = 1;
    private bool bladeOut = false;
    private bool igniting = false;
    private float bladeExtensionTimer = 0.0f;
    private Color bladeGlow;

    private float bladeCenterOffset = 0.4f;
    private Vector3 bladeCenterDistanceFromHilt;

    // Use this for initialization
    protected override void Start ()
    {
        base.Start();

        //lineRenderer = gameObject.AddComponent<LineRenderer>();
        //lineRenderer = gameObject.GetComponent<LineRenderer>();
        //lineRenderer.material = new Material(Shader.Find("Particles/Additive"));
        //lineRenderer.SetWidth(0.01f, 0.01f);
        //lineRenderer.SetVertexCount(2);
        bladeHum = gameObject.GetComponentInChildren<AudioSource>();
        bladeGlow = new Color();
        bladeGlow = bladeRenderer.material.GetColor("_EmissionColor");
        bladeRenderer.material.SetColor("_EmissionColor", Color.black);
        bladeTransform.localScale = new Vector3(bladeTransform.localScale.x, 0, bladeTransform.localScale.z);
        bladeCenterDistanceFromHilt = Vector3.zero;


    }
	
	// Update is called once per frame
	protected override void Update ()
    {
        base.Update();


        // test if the blade is extending in or out and make it behave properly
        if (igniting)
        {
            if(bladeOut)
            {
                bladeExtensionTimer -= Time.deltaTime * bladeRetractSpeed;
                if(bladeExtensionTimer < 0)
                {
                    bladeExtensionTimer = 0;
                    igniting = false;
                    bladeOut = false;
                    bladeRenderer.material.SetColor("_EmissionColor", Color.black);
                    if(bladeHum != null)
                    {
                        bladeHum.Stop();
                    }
                }
            }
            else
            {
                bladeExtensionTimer += Time.deltaTime * bladeExtendSpeed;
                if(bladeExtensionTimer > 1)
                {
                    bladeExtensionTimer = 1;
                    igniting = false;
                    bladeOut = true;
                    if(bladeHum != null)
                        bladeHum.Play();
                }
            }
            bladeTransform.localScale = Vector3.Lerp(new Vector3(bladeTransform.localScale.x, 0, bladeTransform.localScale.z), new Vector3(bladeTransform.localScale.x, 0.4f, bladeTransform.localScale.z), bladeExtensionTimer);
            bladeCenterDistanceFromHilt = Vector3.Lerp(Vector3.zero, new Vector3(0, bladeCenterOffset, 0), bladeExtensionTimer);
        }

        bladeTransform.position = hiltTransform.position;
        bladeTransform.rotation = hiltTransform.rotation;
        bladeTransform.Translate(bladeCenterDistanceFromHilt);


    }

    public override void OnTriggerClicked(ClickedEventArgs e)
    {
        base.OnTriggerClicked(e);

        if (transform.parent == null)
            return;

        // make the blade go in or out
        igniting = true;
        if (bladeHum != null)
        {
            if (bladeOut)
            {
                bladeHum.PlayOneShot(bladeRetractSound, 1);
            }
            else
            {
                bladeHum.PlayOneShot(bladeExtendSound, 1);
                bladeRenderer.material.SetColor("_EmissionColor", bladeGlow);
            }
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
