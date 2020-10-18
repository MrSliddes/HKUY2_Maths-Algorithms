using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Player
{
    private Texture2D visual;
    public DevMath.Vector2 Position
    {
        get { return Circle.Position; }
        set { Circle.Position = value; }
    }

    public DevMath.Vector2 Direction => DevMath.Vector2.DirectionFromAngle(Rotation);

    public DevMath.Circle Circle
    {
        get; private set;
    }

    private Texture2D pixel;
    
    public float Rotation
    {
        get; private set;
    }

    private readonly float moveSpeed = 0.9f;

    private const float MAX_CHARGE_TIME = 1.0f;

    private const float MIN_PROJECTILE_START_VELOCITY = .0f;
    private const float MAX_PROJECTILE_START_VELOCITY = 10.0f;
    private const float MIN_PROJECTILE_START_ACCELERATION = 10.0f;
    private const float MAX_PROJECTILE_START_ACCELERATION = 20.0f;

    private float chargeTime;

    private DevMath.Rigidbody rigidbody;

    public Player()
    {
        visual = Resources.Load<Texture2D>("pacman");

        Circle = new DevMath.Circle();
        Circle.Radius = visual.width * .5f;

        pixel = new Texture2D(1, 1, TextureFormat.RGBA32, false);
        pixel.SetPixel(0, 0, Color.white);
        pixel.Apply();

        Position = new DevMath.Vector2(Screen.width * .5f - visual.width * .5f, Screen.height * .5f - visual.height * .5f);

        rigidbody = new DevMath.Rigidbody()
        {
            mass = 1.0f,
            force = 150.0f,
            dragCoefficient = .47f
        };
    }

    public void Render()
    {
        GUIUtility.RotateAroundPivot(Rotation, Position.ToUnity());

        GUI.DrawTexture(new Rect(Position.x - Circle.Radius, Position.y - Circle.Radius, visual.width, visual.height), visual);

        float p = DevMath.DevMath.Clamp(chargeTime, .0f, MAX_CHARGE_TIME) / MAX_CHARGE_TIME;
        float fireVelocity = DevMath.DevMath.Lerp(MIN_PROJECTILE_START_VELOCITY, MAX_PROJECTILE_START_VELOCITY, p);
        float fireAcceleration = DevMath.DevMath.Lerp(MIN_PROJECTILE_START_ACCELERATION, MAX_PROJECTILE_START_ACCELERATION, p);

        float distanceTraveled = DevMath.DevMath.DistanceTraveled(fireVelocity, fireAcceleration, Projectile.LIFETIME);

        //Implementeer de Line class met de IntersectsWith(Circle) functie en gebruik deze om de lijn rood te kleuren wanneer je een enemy zou raken
        DevMath.Circle lineC = new DevMath.Circle();
        lineC.Position = new DevMath.Vector2(Position.x + distanceTraveled, Position.y); lineC.Radius = 32;
        DevMath.Circle otherC = new DevMath.Circle(); otherC.Radius = 32;
        if(Game.Instance.enemies[0] != null) otherC.Position = new DevMath.Vector2(Game.Instance.enemies[0].Position.x, Game.Instance.enemies[0].Position.y);
        //Debug
        GUI.color = Color.magenta;
        //Debug GUI.DrawTexture(new Rect(new Vector2(lineC.Position.x, lineC.Position.y), new Vector2(32, 32)), pixel);
        GUIUtility.RotateAroundPivot(-Rotation, Position.ToUnity()); // Ow shit dit werkt
        //Debug GUI.DrawTexture(new Rect(new Vector2(otherC.Position.x, otherC.Position.y), new Vector2(32, 32)), pixel);
        GUIUtility.RotateAroundPivot(Rotation, Position.ToUnity()); // Reset weer
        // Super leuk dat als je wilt testen dat de enemy op je player spawned waardoor je gelijk dood bent.
        if(lineC.CollidesWith(otherC)) // Werkt soms en soms niet 
        {
            GUI.color = Color.red;
        }
        else
        {
            GUI.color = Color.black;
        }
        GUI.DrawTexture(new Rect(Position.x, Position.y, distanceTraveled, 1.0f), pixel);

        GUI.matrix = Matrix4x4.identity;
    }

    private void UpdatePhysics()
    {
        DevMath.Vector2 forceDirection = new DevMath.Vector2(-Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        //Debug.Log(forceDirection.x + " " + forceDirection.y);
        //Debug.Log(Time.deltaTime);
        rigidbody.AddForce(forceDirection, Time.deltaTime); // Ik heb geen idee waarom, maar de rb beweegt met lichtsnelheid. Nu niet meer, waarom weet ik niet

        Position += rigidbody.Velocity;
    }

    public void Update()
    {
        UpdatePhysics();

        var mousePos = new DevMath.Vector2(Input.mousePosition.x, Screen.height - Input.mousePosition.y);
        var mouseDir = (mousePos - Position).Normalized;

        Rotation = DevMath.DevMath.RadToDeg(DevMath.Vector2.Angle(new DevMath.Vector2(.0f, .0f), mouseDir));

        if(Input.GetMouseButton(0))
        {
            chargeTime += Time.deltaTime;
        }
        else if(Input.GetMouseButtonUp(0))
        {
            float p = DevMath.DevMath.Clamp(chargeTime, .0f, MAX_CHARGE_TIME) / MAX_CHARGE_TIME;

            Game.Instance.CreateProjectile(Position, Direction, DevMath.DevMath.Lerp(MIN_PROJECTILE_START_VELOCITY, MAX_PROJECTILE_START_VELOCITY, p), DevMath.DevMath.Lerp(MIN_PROJECTILE_START_ACCELERATION, MAX_PROJECTILE_START_ACCELERATION, p));

            chargeTime = .0f;
        }
    }
}
