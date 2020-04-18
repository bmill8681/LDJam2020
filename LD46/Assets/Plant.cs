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
        enum PlantSizes
        {
            Large = 15, 
            Medium = 10, 
            Small = 5
        }

        public DragDrop DragController;

        public int RootDepth { get; set; } = 1;
        public int HP { get; set; } = 5;
        public int MaxHP = 5;

        public bool IsPlanted = false;
        public bool IsDragging = false;
        public bool IsOffset = false;
        public bool ColliderAdjusted = false;
        bool CanAttachPlant = false;

        PlantSizes PlantSize;

        public GameObject PlantSprite;
        BoxCollider PlanterCollider = null;

        private void Awake()
        {
            this.DragController = GetComponent<DragDrop>();
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
            this.RootDepth++;
        }

        public void AddHeath()
        {
            this.HP++;
            if(this.HP > MaxHP)
            {
                this.HP = MaxHP;
            }
        }

        public void RemoveHealth()
        {
            this.HP--;
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
            this.IsPlanted = true;
            
            GetComponent<DepthManager>().enabled = false;
        }

        void ShowDropIndicator()
        {
            PlantSprite.GetComponent<SpriteRenderer>().color = new Color(0.7f, 0.8f, 0.5f, 1);
        }
        void HideDropIndicator()
        {
            PlantSprite.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        }
    }
}
