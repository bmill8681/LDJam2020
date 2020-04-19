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

            ForTestingPurposes();
        }

        void ForTestingPurposes()
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                this.PlantSize = PlantSizes.XLarge;
                this.HP = this.MaxHP;
            }
            if (Input.GetKeyDown(KeyCode.W))
            {
                this.PlantSize = PlantSizes.Large;
                this.HP = this.MaxHP;
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                this.PlantSize = PlantSizes.Medium;
                this.HP = this.MaxHP;
            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                this.PlantSize = PlantSizes.Small;
                this.HP = this.MaxHP;
            }
            if (Input.GetKeyDown(KeyCode.K))
            {
                this.RemoveHealth();
            }
        }

        void AdjustCollider()
        {
            if (this.IsDragging && !ColliderAdjusted)
            {
                this.ColliderAdjusted = true;
                BoxCollider collider = GetComponent<BoxCollider>();
                collider.size = new Vector3(collider.size.x, collider.size.y + 0.2f, collider.size.z);
                collider.center = new Vector3(collider.center.x, collider.center.y + 0.1f, collider.center.z);
            } else if (!this.IsDragging && ColliderAdjusted)
            {
                this.ColliderAdjusted = false;
                BoxCollider collider = GetComponent<BoxCollider>();
                Transform colTransform = collider.GetComponent<Transform>();

                collider.size = new Vector3(collider.size.x, collider.size.y - 0.2f, collider.size.z);
                collider.center = new Vector3(collider.center.x, collider.center.y - 0.1f, collider.center.z);
            }
        }

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
            Debug.Log(string.Format("Dead?: {0}, HP: {1}, Roots: {2}, Growth: {3}", this.IsDead, this.HP, this.RootDepth, this.PlantGrowth));
        }

        public bool Sheer()
        {
            Debug.Log("Attemplting to sheer");
            bool plantSheerSuccess = false;
            switch (this.PlantSize)
            {
                case PlantSizes.XLarge:
                    plantSheerSuccess = true;
                    this.PlantSize = PlantSizes.Large;
                    break;
                case PlantSizes.Large:
                    plantSheerSuccess = true;
                    this.PlantSize = PlantSizes.Medium;
                    break;
                case PlantSizes.Medium:
                    plantSheerSuccess = true;
                    this.PlantSize = PlantSizes.Small;
                    break;
            }
            Debug.Log("Sheer Success: " + plantSheerSuccess);
            return plantSheerSuccess;
        }
    }
}
