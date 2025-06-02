using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditorInternal;
using UnityEngine;


namespace the_dervish
{

    public enum TransitionParameter
    {
        Move,
        Jump,
        ForceTransition,
        Grounded,
        Attack,
    }

    public class CharacterControl : MonoBehaviour
    {

        [SerializeField] public Animator SkinnedMeshAnimator;
        [SerializeField] public bool MoveRight;
        [SerializeField] public bool MoveLeft;
        [SerializeField] public bool Jump;

        [SerializeField] public bool Attack;

        public Material material;

        public GameObject ColliderEdgePrefab;
        public List<GameObject> BottomSpheres = new List<GameObject>();
        public List<GameObject> FrontSpheres = new List<GameObject>();

        public List<Collider> RagdollParts = new List<Collider>();

        private List<TriggerDetector> TriggerDetectors = new List<TriggerDetector>();

        //public List<Collider> CollidingParts = new List<Collider>();

        public float GravityMultiplier;
        public float PullMultiplier;

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
            bool SwitchBack = false;

            if (!IsFacingForward())
            {
                SwitchBack = true;
            }
            FaceForward(true);
            SetColliderSpheres();

            if (SwitchBack)
            {
                FaceForward(false);
            }

        }

        /*         private IEnumerator Start(){
                    yield return new WaitForSeconds(5f);
                    //RIGID_BODY.AddForce(200f * Vector3.up);
                    yield return new WaitForSeconds(0.5f);
                    TurnOnRagdoll();
                } */



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

        public void CreateMiddleSpheres(GameObject start, Vector3 direction, float sec, int iterations, List<GameObject> spheresList)
        {
            // spheresList.Add(CreateEdgeSphere(start.transform.position));
            for (int i = 0; i < iterations; i++)
            {
                Vector3 pos = start.transform.position + (direction * sec * (i + 1));

                spheresList.Add(CreateEdgeSphere(pos));
            }

        }


        public GameObject CreateEdgeSphere(Vector3 pos)
        {
            GameObject obj = Instantiate(ColliderEdgePrefab, pos, Quaternion.identity);
            obj.transform.parent = this.transform;
            return obj;
        }


        public void MoveForward(float Speed, float SpeedGraph)
        {
            transform.Translate(Vector3.forward * Speed * SpeedGraph * Time.deltaTime);
        }


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

        public bool IsFacingForward()
        {
            if (transform.forward.z > 0f)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


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


        public void ChangeMaterial()
        {

            if (material == null)
            {
                Debug.Log("No Material Spesified");
            }
            Renderer[] arrMaterials = this.gameObject.GetComponentsInChildren<Renderer>();


            foreach (Renderer r in arrMaterials)
            {
                if (r.gameObject != this.gameObject)
                {
                    r.material = material;
                }
            }

        }
    }



}