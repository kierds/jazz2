﻿using Duality;

namespace Jazz2.Actors.Environment
{
    public class Bomb : ActorBase
    {
        private float timeLeft = MathF.Rnd.NextFloat(40f, 90f);

        public override void OnAttach(ActorInstantiationDetails details)
        {
            base.OnAttach(details);

            ushort theme = details.Params[0];
            IsFacingLeft = (details.Params[1] != 0);

            health = int.MaxValue;
            elasticity = 0.3f;

            switch (theme) {
                case 0: RequestMetadata("Object/Bomb"); break;
                case 1: RequestMetadata("Enemy/LizardFloat"); break;
                case 2: RequestMetadata("Enemy/LizardFloatXmas"); break;
            }

            SetAnimation("Bomb");
        }

        protected override void OnUpdate()
        {
            base.OnUpdate();

            if (timeLeft > 0f) {
                timeLeft -= Time.TimeMult;
            } else {
                DecreaseHealth(int.MaxValue);
            }
        }

        protected override void OnUpdateHitbox()
        {
            UpdateHitbox(6, 6);
        }

        protected override bool OnPerish(ActorBase collider)
        {
            Vector3 pos = Transform.Pos;

            foreach (ActorBase collision in api.FindCollisionActorsRadius(pos.X, pos.Y, 40)) {
                Player player = collision as Player;
                if (player != null) {
                    bool pushLeft = (pos.X > player.Transform.Pos.X);
                    player.TakeDamage(pushLeft ? -8f : 8f);
                }
            }

            // Explosion.Large is the same as Explosion.Bomb
            Explosion.Create(api, pos, Explosion.Large);

            api.PlayCommonSound(this, "Bomb");

            return base.OnPerish(collider);
        }
    }
}