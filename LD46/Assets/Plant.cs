using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace PlantStuff
{
    public class Plant : MonoBehaviour
    {
        public enum PlantSizes
        {
            XLarge = 16,
            Large = 12, 
            Medium = 8, 
            Small = 4
        }

        public DragDrop DragController;

        public int RootDepth { get; set; } = 4;
        [SerializeField]
        private int HP;
        private int MaxHP = 6;
        [SerializeField]
        private int PlantGrowth;
        public int SpawnLocation;

        public bool IsPlanted;
        public bool IsDead;
        public bool IsDragging = false;
        public bool IsOffset = false;
        public bool ColliderAdjusted = false;
        bool CanAttachPlant = false;

        public PlantSizes PlantSize;

        public GameObject PlantSprite;
        public PlantSpriteUpdater PlantSpriteUpdateHandler;
        BoxCollider PlanterCollider = null;
        public GameManagerScript GameController;
        private List<string> ActionQueue = new List<string>();

        private void Start()
        {
            this.GameController = FindObjectOfType<GameManagerScript>();
            if(this.GameController == null)
            {
                throw new Exception("No game controller found in scene");
            }
            this.PlantSize = PlantSizes.Small;
            this.GameController.AddPlantTolist(this);
            this.DragController = GetComponent<DragDrop>();
            this.HP = this.MaxHP;
            this.PlantGrowth = 4;
            this.IsPlanted = false;
            this.IsDead = false;
            PlantSpriteUpdateHandler.SetPlantSprite(this.PlantSize, this.HP);
            SetColliderSize();
        }

        private void Update()
        {
            if(!this.IsDragging && DragController.IsDragging)
            {
                this.IsDragging = true;
                AdjustCollider();
                SetPositionOffset();
            } else if (this.IsDragging && !DragController.IsDragging)
            {
                this.IsDragging = false;
                AdjustCollider();
                SetPositionOffset();
            }

            if(!this.IsDragging && this.ActionQueue.Count > 0)
            {
                RunActionQueue();
            }

            // For testing purposes
            if (Input.GetKeyDown(KeyCode.Q)) //XL
            {
                this.PlantSize = PlantSizes.XLarge;
                this.HP = this.MaxHP;
                PlantSpriteUpdateHandler.SetPlantSprite(this.PlantSize, this.HP);
                SetColliderSize();
            }
            if (Input.GetKeyDown(KeyCode.W)) //L
            {
                this.PlantSize = PlantSizes.Large;
                this.HP = this.MaxHP;
                PlantSpriteUpdateHandler.SetPlantSprite(this.PlantSize, this.HP);
                SetColliderSize();
            }
            if (Input.GetKeyDown(KeyCode.E)) //M
            {
                this.PlantSize = PlantSizes.Medium;
                this.HP = this.MaxHP;
                PlantSpriteUpdateHandler.SetPlantSprite(this.PlantSize, this.HP);
                SetColliderSize();
            }
            if (Input.GetKeyDown(KeyCode.R)) //S
            {
                this.PlantSize = PlantSizes.Small;
                this.HP = this.MaxHP;
                PlantSpriteUpdateHandler.SetPlantSprite(this.PlantSize, this.HP);
                SetColliderSize();
            }
            if (Input.GetKeyDown(KeyCode.K))
            {
                this.RemoveHealth();
                PlantSpriteUpdateHandler.SetPlantSprite(this.PlantSize, this.HP);
            }
        }

        void RunActionQueue()
        {
            List<string> UpdateQueue = new List<string>();
            foreach(string item in this.ActionQueue)
            {
                UpdateQueue.Add(item);
            }
            ActionQueue = new List<string>();

            while(UpdateQueue.Count > 0)
            {
                string item = UpdateQueue[UpdateQueue.Count - 1];
                UpdateQueue.RemoveAt(UpdateQueue.Count - 1);
                RunAction(item);
            }
        }

        void RunAction(string item)
        {
            switch (item)
            {
                case "SETCOLLIDERSIZE":
                    this.SetColliderSize();
                    break;
                default:
                    Debug.Log(string.Format("'{0}' - is not an action in RunAction method", item));
                    break;
            }
        }



        #region Collider Adjustment
        /* Refactor these - should be in their own class */

        void SetColliderSize()
        {
            if(IsDragging)
            {
                ActionQueue.Add("SETCOLLIDERSIZE");                
            }
            else
            {
                if (this.PlantSize == PlantSizes.XLarge)
                {
                    SetColliderSizeXL();
                }
                else if (this.PlantSize == PlantSizes.Large)
                {
                    SetColliderSizeL();
                }
                else if (this.PlantSize == PlantSizes.Medium)
                {
                    SetColliderSizeM();
                }
                else if (this.PlantSize == PlantSizes.Small)
                {
                    SetColliderSizeS();
                }
            }
        }

        void SetColliderSizeXL()
        {
            BoxCollider collider = GetComponent<BoxCollider>();
            collider.size = new Vector3(0.15f, 0.24f, 0);
            collider.center = new Vector3(0, -0.04f, 0);
        }
        void SetColliderSizeL()
        {
            BoxCollider collider = GetComponent<BoxCollider>();
            collider.size = new Vector3(0.15f, 0.22f, 0);
            collider.center = new Vector3(0, -0.05f, 0);
        }
        void SetColliderSizeM()
        {
            BoxCollider collider = GetComponent<BoxCollider>();
            collider.size = new Vector3(0.15f, 0.15f, 0.02f);
            collider.center = new Vector3(0, -.085f, 0);
        }
        void SetColliderSizeS()
        {
            BoxCollider collider = GetComponent<BoxCollider>();
            collider.size = new Vector3(0.15f, 0.08f, 0);
            collider.center = new Vector3(0, -0.12f, 0);
        }
        void AdjustCollider()
        {
            if(this.PlantSize == PlantSizes.XLarge)
            {
                AdjustColliderXL();
            }
            else if (this.PlantSize == PlantSizes.Large)
            {
                AdjustColliderL();
            }
            else if(this.PlantSize == PlantSizes.Medium)
            {
                AdjustColliderM();
            }
            else if(this.PlantSize == PlantSizes.Small)
            {
                AdjustColliderS();
            }
        }

        void AdjustColliderXL()
        {
            if (this.IsDragging && !ColliderAdjusted)
            {
                this.ColliderAdjusted = true;
                BoxCollider collider = GetComponent<BoxCollider>();
                collider.size = new Vector3(collider.size.x, collider.size.y + 0.2f, collider.size.z);
                collider.center = new Vector3(collider.center.x, collider.center.y + 0.1f, collider.center.z);
            }
            else if (!this.IsDragging && ColliderAdjusted)
            {
                this.ColliderAdjusted = false;
                BoxCollider collider = GetComponent<BoxCollider>();
                Transform colTransform = collider.GetComponent<Transform>();
                collider.size = new Vector3(collider.size.x, collider.size.y - 0.2f, collider.size.z);
                collider.center = new Vector3(collider.center.x, collider.center.y - 0.1f, collider.center.z);
            }
        }

        void AdjustColliderL()
        {
            if (this.IsDragging && !ColliderAdjusted)
            {
                this.ColliderAdjusted = true;
                BoxCollider collider = GetComponent<BoxCollider>();
                collider.size = new Vector3(collider.size.x, collider.size.y + 0.2f, collider.size.z);
                collider.center = new Vector3(collider.center.x, collider.center.y + 0.1f, collider.center.z);
            }
            else if (!this.IsDragging && ColliderAdjusted)
            {
                this.ColliderAdjusted = false;
                BoxCollider collider = GetComponent<BoxCollider>();
                Transform colTransform = collider.GetComponent<Transform>();
                collider.size = new Vector3(collider.size.x, collider.size.y - 0.2f, collider.size.z);
                collider.center = new Vector3(collider.center.x, collider.center.y - 0.1f, collider.center.z);
            }
        }

        void AdjustColliderM()
        {
            if (this.IsDragging && !ColliderAdjusted)
            {
                this.ColliderAdjusted = true;
                BoxCollider collider = GetComponent<BoxCollider>();
                collider.size = new Vector3(collider.size.x, collider.size.y + 0.2f, collider.size.z);
                collider.center = new Vector3(collider.center.x, collider.center.y + 0.1f, collider.center.z);
            }
            else if (!this.IsDragging && ColliderAdjusted)
            {
                this.ColliderAdjusted = false;
                BoxCollider collider = GetComponent<BoxCollider>();
                Transform colTransform = collider.GetComponent<Transform>();
                collider.size = new Vector3(collider.size.x, collider.size.y - 0.2f, collider.size.z);
                collider.center = new Vector3(collider.center.x, collider.center.y - 0.1f, collider.center.z);
            }
        }

        void AdjustColliderS()
        {
            if (this.IsDragging && !ColliderAdjusted)
            {
                this.ColliderAdjusted = true;
                BoxCollider collider = GetComponent<BoxCollider>();
                collider.size = new Vector3(collider.size.x, collider.size.y + 0.2f, collider.size.z);
                collider.center = new Vector3(collider.center.x, collider.center.y + 0.1f, collider.center.z);
            }
            else if (!this.IsDragging && ColliderAdjusted)
            {
                this.ColliderAdjusted = false;
                BoxCollider collider = GetComponent<BoxCollider>();
                Transform colTransform = collider.GetComponent<Transform>();
                collider.size = new Vector3(collider.size.x, collider.size.y - 0.2f, collider.size.z);
                collider.center = new Vector3(collider.center.x, collider.center.y - 0.1f, collider.center.z);
            }
        }

        #endregion
        void SetPositionOffset()
        {

            Transform transform = PlantSprite.GetComponent<Transform>();
            if (!this.IsOffset && DragController.IsDragging)
            {
                this.IsOffset = true;
                transform.position = new Vector3(transform.position.x, transform.position.y + 0.2f, transform.position.z);
            } else if (this.IsOffset && !DragController.IsDragging)
            {
                this.IsOffset = false;
                transform.position = new Vector3(transform.position.x, transform.position.y - 0.2f, transform.position.z);
            }
        }

        public void AddRootGrowth()
        {
            bool growPlantSize = false;

            // If the PlantGrowth is smaller than the maximum PlantSize value 
            // and the PlantGrowth is smaller than the root system supports
            // grow the plant
            // otherwise, grow the root system.
            if (this.PlantGrowth < (int)PlantSizes.XLarge)
            {
                if (this.PlantGrowth <= this.RootDepth)
                {
                    growPlantSize = true;
                } 
            }

            if (growPlantSize)
            {
                this.PlantGrowth++;
                AdjustPlantSize();
            }
            else
            {
                this.RootDepth++;
            }
        }

        private void AdjustPlantSize()
        {
            if(this.PlantGrowth <= 4)
            {
                this.PlantSize = PlantSizes.Small;
            }
            else if (this.PlantGrowth > 4 && this.PlantGrowth <= 8)
            {
                this.PlantSize = PlantSizes.Medium;
            }
            else if (this.PlantGrowth > 8 && this.PlantGrowth <= 12)
            {
                this.PlantSize = PlantSizes.Large;
            }
            else if (this.PlantGrowth > 12)
            {
                this.PlantSize = PlantSizes.XLarge;
            }
            PlantSpriteUpdateHandler.SetPlantSprite(this.PlantSize, this.HP);
            SetColliderSize();
        }

        public void AddHeath()
        {
            this.HP++;
            if(this.HP > MaxHP)
            {
                this.HP = MaxHP;
                AddRootGrowth();
            }
            PlantSpriteUpdateHandler.SetPlantSprite(this.PlantSize, this.HP);
            SetColliderSize();
        }

        public void RemoveHealth()
        {
            this.HP--;
            if (HP <= 0)
            {
                HP = 0;
                IsDead = true;
            }
            PlantSpriteUpdateHandler.SetPlantSprite(this.PlantSize, this.HP);
            SetColliderSize();
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("planter"))
            {
                if (DragController.IsDragging)
                {
                    this.CanAttachPlant = true;
                    ShowDropIndicator();
                    PlanterCollider = other.GetComponent<BoxCollider>();
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag("planter"))
            {
                HideDropIndicator();
                this.CanAttachPlant = false;
                this.PlanterCollider = null;
            }
        }

        private void OnMouseUp()
        {
            if (this.CanAttachPlant)
            {
                AttachPlant();
            }
        }

        void AttachPlant()
        {
            if (PlanterCollider == null) return;
            PlantControllerScript PC = PlanterCollider.GetComponent<PlantControllerScript>();
            HideDropIndicator();
            PC.AttachPlant(this);
            IsPlanted = true;
            
            GetComponent<DepthManager>().enabled = false;
        }

        public void RemoveFromPlanter()
        {
            this.gameObject.transform.SetParent(null);
            this.IsPlanted = false;
            this.CanAttachPlant = false;
            GetComponent<DepthManager>().enabled = true;
        }

        public void DetachNewPlant()
        {
            this.gameObject.transform.SetParent(null);
            GetComponent<DragDrop>().enabled = true;
        }

        void ShowDropIndicator()
        {
            PlantSprite.GetComponent<SpriteRenderer>().color = new Color(0.7f, 0.8f, 0.5f, 1);
        }
        void HideDropIndicator()
        {
            PlantSprite.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        }

        public void PrintPlantStatus()
        {
            Debug.Log(string.Format("Dead: {0}, HP: {1}, Roots: {2}, Growth: {3}", this.IsDead, this.HP, this.RootDepth, this.PlantGrowth));
        }

        public bool Sheer()
        {
            bool plantSheerSuccess = false;
            switch (this.PlantSize)
            {
                case PlantSizes.XLarge:
                    plantSheerSuccess = true;
                    this.PlantSize = PlantSizes.Large;
                    this.PlantGrowth = 11;
                    break;
                case PlantSizes.Large:
                    plantSheerSuccess = true;
                    this.PlantSize = PlantSizes.Medium;
                    this.PlantGrowth = 7;
                    break;
                case PlantSizes.Medium:
                    plantSheerSuccess = true;
                    this.PlantSize = PlantSizes.Small;
                    this.PlantGrowth = 3;
                    break;
            }
            AdjustCollider();
            AdjustPlantSize();
            return plantSheerSuccess;
        }
    }
}
