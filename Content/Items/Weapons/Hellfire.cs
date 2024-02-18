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
    public class Hellfire : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.DefaultToMagicWeapon(ProjectileID.Flames, 6, 0f, true);

            Item.width = 40;
            Item.height = 40;
            Item.scale = 1f;

            Item.damage = 10;
            Item.knockBack = 1.5f;

            Item.rare = ItemRarityID.Orange;

            Item.value = Item.sellPrice(0, 3);
            Item.mana = 3;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.HellstoneBar, 15);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            position = Main.MouseWorld + new Vector2(Main.rand.Next(-40, 41), Main.rand.Next(-40, 41));
            Projectile.NewProjectileDirect(source, position, velocity, type, damage, knockback);

            return false;
        }
    }
}