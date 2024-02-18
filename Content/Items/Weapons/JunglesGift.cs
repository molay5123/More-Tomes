using System;
using System.Collections.Generic;
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
    public class JunglesGift : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.DefaultToMagicWeapon(ProjectileID.SporeTrap, 6, 7f, true);

            Item.width = 40;
            Item.height = 40;
            Item.scale = 1f;

            Item.damage = 5;
            Item.knockBack = 0.5f;

            Item.rare = ItemRarityID.Orange;

            Item.value = Item.sellPrice(0, 0, 50);
            Item.mana = 3;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Vector2 NewVelocity = velocity.RotatedByRandom(MathHelper.ToRadians(20));

            NewVelocity *= 1f - Main.rand.NextFloat(0.1f);

            Projectile.NewProjectile(source, position, NewVelocity, type, damage, knockback, Main.myPlayer);

            if (Main.rand.NextBool(3))
                Projectile.NewProjectile(source, position, NewVelocity, type, damage, knockback, Main.myPlayer);

            return false;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Stinger, 10);
            recipe.AddIngredient(ItemID.JungleSpores, 12);
            recipe.AddIngredient(ItemID.Vine, 3);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}