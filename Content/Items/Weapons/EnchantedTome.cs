using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace MoreTomes.Content.Items.Weapons
{
    public class EnchantedTome : ModItem
    {
        public override void SetStaticDefaults()
        {
            ItemID.Sets.ShimmerTransformToItem[Type] = ItemID.EnchantedSword; // Huh why not - Many
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.DefaultToMagicWeapon(ModContent.ProjectileType<EnchantedStar>(), 34, 11f, true);

            Item.width = 40;
            Item.height = 40;
            Item.scale = 1f;

            Item.damage = 38;
            Item.knockBack = 4.5f;

            Item.rare = ItemRarityID.Green;
            Item.UseSound = SoundID.Item9;

            Item.value = Item.sellPrice(0, 3);
            Item.mana = 16;
        }
           public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.EnchantedBoomerang, 1);
            recipe.AddIngredient(ItemID.Book, 1);
            recipe.AddIngredient(ItemID.FallenStar, 1);
            recipe.AddTile(TileID.Workbench);
            recipe.Register();
        }
    }

    public class EnchantedStar : ModProjectile
    {
        public override string Texture => "Terraria/Images/Projectile_728";
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailingMode[Projectile.type] = 2;
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 10;
        }
        public override void SetDefaults()
        {
            Projectile.width = 30;
            Projectile.height = 26;
            Projectile.aiStyle = 0;
            Projectile.friendly = true;
            Projectile.penetrate = 1;
            Projectile.scale = 1f;
            Projectile.timeLeft = 120;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.extraUpdates = 0;
            Projectile.scale = 0.7f;
            Projectile.light = 0.2f;
        }
        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
        {
            width = 16;
            height = 16;
            return base.TileCollideStyle(ref width, ref height, ref fallThrough, ref hitboxCenterFrac);
        }
        public override void AI()
        {
            int num224 = Main.rand.Next(3);
            int num225 = Dust.NewDust(new Vector2(Projectile.position.X - Projectile.velocity.X * 4f + 2f, Projectile.position.Y + 2f - Projectile.velocity.Y * 4f), 20, 20, num224 switch
            {
                0 => 15,
                1 => 57,
                _ => 58,
            }, 0f, 0f, 100, default(Color), 1.25f);
            Dust dust2 = Main.dust[num225];
            dust2.velocity *= 0.1f;

            Projectile.rotation += 0.2f * Projectile.direction;
        }
        public override void PostDraw(Color lightColor)
        {
            Texture2D value12 = TextureAssets.Projectile[Projectile.type].Value;
            Rectangle val71 = default(Rectangle);
            val71 = new Rectangle(0, 0, value12.Width, value12.Height);
            Vector2 origin6 = val71.Size() / 2f;
            Color alpha3 = Projectile.GetAlpha(lightColor);
            Texture2D value13 = TextureAssets.Extra[91].Value;
            Rectangle val72 = value13.Frame();
            Vector2 origin7 = default(Vector2);
            origin7 = new Vector2((float)val72.Width / 2f, 10f);
            Vector2 val73 = default(Vector2);
            val73 = new Vector2(0f, Projectile.gfxOffY);
            Vector2 spinningpoint = default(Vector2);
            spinningpoint = new Vector2(0f, -10f);
            float num195 = (float)Main.timeForVisualEffects / 60f;
            Vector2 val74 = Projectile.Center + Projectile.velocity;
            Color val76 = Color.White * 0.5f;
            float num196 = -0.2f;
            Main.EntitySpriteDraw(value13, val74 - Main.screenPosition + val73 + spinningpoint.RotatedBy((float)Math.PI * 2f * num195), val72, Color.Blue * 0.3f, Projectile.velocity.ToRotation() + (float)Math.PI / 2f, origin7, 1f + num196, (SpriteEffects)0);
            Main.EntitySpriteDraw(value13, val74 - Main.screenPosition + val73 + spinningpoint.RotatedBy((float)Math.PI * 2f * num195 + (float)Math.PI * 2f / 3f), val72, Color.Blue * 0.3f, Projectile.velocity.ToRotation() + (float)Math.PI / 2f, origin7, 0.7f + num196, (SpriteEffects)0);
            Main.EntitySpriteDraw(value13, val74 - Main.screenPosition + val73 + spinningpoint.RotatedBy((float)Math.PI * 2f * num195 + 4.1887903f), val72, Color.Blue * 0.3f, Projectile.velocity.ToRotation() + (float)Math.PI / 2f, origin7, 0.85f + num196, (SpriteEffects)0);
            Vector2 val77 = Projectile.Center - Projectile.velocity * 0.5f;
            for (float num197 = 0f; num197 < 1f; num197 += 0.5f)
            {
                float num198 = num195 % 0.5f / 0.5f;
                num198 = (num198 + num197) % 1f;
                float num199 = num198 * 2f;
                if (num199 > 1f)
                {
                    num199 = 2f - num199;
                }
                Main.EntitySpriteDraw(value13, val77 - Main.screenPosition + val73, val72, val76 * num199, Projectile.velocity.ToRotation() + (float)Math.PI / 2f, origin7, 0.3f + num198 * 0.5f, (SpriteEffects)0);
            }
            Main.EntitySpriteDraw(value12, Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY), val71, alpha3, Projectile.rotation, origin6, Projectile.scale + 0.1f, SpriteEffects.None);
        }
        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
            for (int num977 = 4; num977 < 24; num977++)
            {
                float num978 = Projectile.oldVelocity.X * (30f / (float)num977);
                float num979 = Projectile.oldVelocity.Y * (30f / (float)num977);
                int num980 = Main.rand.Next(3);
                int num981 = Dust.NewDust(new Vector2(Projectile.position.X - num978, Projectile.position.Y - num979), 8, 8, num980 switch
                {
                    0 => 15,
                    1 => 57,
                    _ => 58,
                }, Projectile.oldVelocity.X * 0.2f, Projectile.oldVelocity.Y * 0.2f, 100, default(Color), 1.8f);
                Dust dust2 = Main.dust[num981];
                dust2.velocity *= 1.5f;
                Main.dust[num981].noGravity = true;
            }
        }
        public override Color? GetAlpha(Color lightColor)
            => new Color?(new Color(255, 255, 255, 0));
    }
}
