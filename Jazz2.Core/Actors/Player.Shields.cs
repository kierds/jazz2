﻿using Duality;

namespace Jazz2.Actors
{
    partial class Player
    {
        public enum ShieldType : byte
        {
            None,

            Fire,
            Water,
            Lightning,
            Laser
        }

        private ActorBase shieldComponent, shieldComponentFront;
        private float shieldTime;

        public void SetShield(ShieldType shieldType, float secs)
        {
            if (shieldComponent != null) {
                ParentScene.RemoveObject(shieldComponent);
                shieldComponent = null;
            }

            if (shieldComponentFront != null) {
                ParentScene.RemoveObject(shieldComponentFront);
                shieldComponentFront = null;
            }

            if (shieldType == ShieldType.None) {
                shieldTime = 0f;
                return;
            }

            shieldTime = secs * Time.FramesPerSecond;

            switch (shieldType) {
                case ShieldType.Fire:
                    shieldComponent = new ShieldComponent(shieldType, false);
                    shieldComponent.OnAttach(new ActorInstantiationDetails {
                        Api = api
                    });
                    shieldComponent.Parent = this;

                    shieldComponentFront = new ShieldComponent(shieldType, true);
                    shieldComponentFront.OnAttach(new ActorInstantiationDetails {
                        Api = api
                    });
                    shieldComponentFront.Parent = this;
                    break;

                case ShieldType.Water:
                    shieldComponentFront = new ShieldComponent(shieldType, true);
                    shieldComponentFront.OnAttach(new ActorInstantiationDetails {
                        Api = api
                    });
                    shieldComponentFront.Parent = this;
                    break;

                case ShieldType.Lightning:
                    // ToDo
                    break;

                case ShieldType.Laser:
                    // ToDo
                    break;
            }
        }

        public bool IncreaseShieldTime(float secs)
        {
            if (shieldTime <= 0f) {
                return false;
            }

            shieldTime += secs * Time.FramesPerSecond;
            PlaySound("PickupGem");

            return true;
        }

        private class ShieldComponent : ActorBase
        {
            private readonly ShieldType shieldType;
            private readonly bool front;

            public ShieldComponent(ShieldType shieldType, bool front)
            {
                this.shieldType = shieldType;
                this.front = front;
            }

            public override void OnAttach(ActorInstantiationDetails details)
            {
                base.OnAttach(details);

                RequestMetadata("Interactive/Shields");

                switch (shieldType) {
                    case ShieldType.Fire: SetAnimation(front ? "FireFront": "Fire"); break;
                    case ShieldType.Water: SetAnimation("Water"); break;
                }
            }

            protected override void OnUpdate()
            {
                Transform.RelativePos = new Vector3(0f, 0f, front ? -2f : 2f);
            }
        }
    }
}