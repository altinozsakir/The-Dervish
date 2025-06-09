using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace the_dervish
{
    // Enum to define the different transition parameters for the Animator
    public enum TransitionParameter
    {
        Move,
        Jump,
        ForceTransition,
        Grounded,
        Attack,
        TransitionIndex,
    }

    // This class handles character movement, animation states, collision detection, and ragdoll physics
    public class CharacterControl : MonoBehaviour
    {
        [SerializeField] public Animator SkinnedMeshAnimator;
        [SerializeField] public bool MoveRight;
        [SerializeField] public bool MoveLeft;
        [SerializeField] public bool MoveUp;
        [SerializeField] public bool MoveDown;
        [SerializeField] public bool Jump;
        [SerializeField] public bool Attack;

        [SerializeField] public LedgeChecker ledgeChecker;

        // Prefab to visualize or use for collision edge detection
        public GameObject ColliderEdgePrefab;
        public List<GameObject> BottomSpheres = new List<GameObject>();
        public List<GameObject> FrontSpheres = new List<GameObject>();

        // Parts of the body that become active when ragdoll is triggered
        public List<Collider> RagdollParts = new List<Collider>();
        private List<TriggerDetector> TriggerDetectors = new List<TriggerDetector>();

        public float GravityMultiplier;
        public float PullMultiplier;

        // Cached reference to the Rigidbody
        private new Rigidbody rigidbody;

        public Rigidbody RIGID_BODY
        {
            get
            {
                if (rigidbody == null)
                {
                    rigidbody = GetComponent<Rigidbody>();
                }
                return rigidbody;
            }
        }

        private void Awake()
        {
            // Temporarily face the character forward to align colliders
            bool SwitchBack = false;
            if (!IsFacingForward())
            {
                SwitchBack = true;
            }

            FaceForward(true);
            SetColliderSpheres();

            // Return to original facing direction if needed
            if (SwitchBack)
            {
                FaceForward(false);
            }

            ledgeChecker = GetComponentInChildren<LedgeChecker>();
        }

        // Enables ragdoll mode by disabling animator and enabling physical colliders
        public void TurnOnRagdoll()
        {
            RIGID_BODY.useGravity = false;
            RIGID_BODY.linearVelocity = Vector3.zero;

            this.gameObject.GetComponent<BoxCollider>().enabled = false;
            SkinnedMeshAnimator.enabled = false;
            SkinnedMeshAnimator.avatar = null;

            foreach (Collider c in RagdollParts)
            {
                c.isTrigger = false;
                c.attachedRigidbody.linearVelocity = Vector3.zero;
            }
        }

        // Prepares colliders for ragdoll by marking them as triggers and tracking them
        public void SetRagdollParts()
        {
            RagdollParts.Clear();
            Collider[] colliders = this.gameObject.GetComponentsInChildren<Collider>();

            foreach (Collider c in colliders)
            {
                if (c.gameObject != this.gameObject)
                {
                    c.isTrigger = true;
                    RagdollParts.Add(c);

                    if (c.GetComponent<TriggerDetector>() == null)
                    {
                        c.gameObject.AddComponent<TriggerDetector>();
                    }
                }
            }
        }

        // Sets up reference spheres at the character's base and front to be used for collision detection
        private void SetColliderSpheres()
        {
            BoxCollider box = GetComponent<BoxCollider>();

            float bottom = box.bounds.center.y - box.bounds.extents.y;
            float top = box.bounds.center.y + box.bounds.extents.y;
            float front = box.bounds.center.z + box.bounds.extents.z;
            float back = box.bounds.center.z - box.bounds.extents.z;

            GameObject bottomFront = CreateEdgeSphere(new Vector3(0f, bottom, front));
            GameObject bottomBack = CreateEdgeSphere(new Vector3(0f, bottom, back));
            GameObject topFront = CreateEdgeSphere(new Vector3(0f, top, front));

            BottomSpheres.Add(bottomFront);
            BottomSpheres.Add(bottomBack);
            FrontSpheres.Add(topFront);
            FrontSpheres.Add(bottomFront);

            float horSec = (bottomFront.transform.position - bottomBack.transform.position).magnitude / 5f;
            CreateMiddleSpheres(bottomBack, this.transform.forward, horSec, 4, BottomSpheres);

            float verSec = (bottomFront.transform.position - topFront.transform.position).magnitude / 10f;
            CreateMiddleSpheres(bottomFront, this.transform.up, verSec, 9, FrontSpheres);
        }

        // Handles gravity and jump pull effects in physics step
        private void FixedUpdate()
        {
            if (RIGID_BODY.linearVelocity.y < 0f)
            {
                RIGID_BODY.linearVelocity += -Vector3.up * GravityMultiplier;
            }

            if (RIGID_BODY.linearVelocity.y > 0f && !Jump)
            {
                RIGID_BODY.linearVelocity += (-Vector3.up * PullMultiplier);
            }
        }

        // Helper to create evenly spaced spheres between two points
        public void CreateMiddleSpheres(GameObject start, Vector3 direction, float sec, int iterations, List<GameObject> spheresList)
        {
            for (int i = 0; i < iterations; i++)
            {
                Vector3 pos = start.transform.position + (direction * sec * (i + 1));
                spheresList.Add(CreateEdgeSphere(pos));
            }
        }

        // Instantiates a collision sphere and parents it to the character
        public GameObject CreateEdgeSphere(Vector3 pos)
        {
            GameObject obj = Instantiate(ColliderEdgePrefab, pos, Quaternion.identity);
            obj.transform.parent = this.transform;
            return obj;
        }

        // Moves the character forward based on speed and animation speed curve
        public void MoveForward(float Speed, float SpeedGraph)
        {
            transform.Translate(Vector3.forward * Speed * SpeedGraph * Time.deltaTime);
        }

        // Rotates the character to face forward or backward (used for flipping direction)
        public void FaceForward(bool forward)
        {
            if (forward)
            {
                transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            }
            else
            {
                transform.rotation = Quaternion.Euler(0f, 180f, 0f);
            }
        }

        // Checks if the character is currently facing forward
        public bool IsFacingForward()
        {
            return transform.forward.z > 0f;
        }

        // Returns all child colliders with TriggerDetector, caching for performance
        public List<TriggerDetector> GetAllTriggers()
        {
            if (TriggerDetectors.Count == 0)
            {
                TriggerDetector[] arr = this.gameObject.GetComponentsInChildren<TriggerDetector>();

                foreach (TriggerDetector d in arr)
                {
                    TriggerDetectors.Add(d);
                }
            }

            return TriggerDetectors;
        }
    }
}
