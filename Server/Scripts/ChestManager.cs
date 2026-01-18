/* Server/Scripts/ChestManager */

using Hytale.API;
using Hytale.API.Entities;
using Hytale.API.Blocks;
using Hytale.API.Inventory;
using System.Collections.Generic;

namespace AdvancedStorage.Server.Scripts
{
    public class ChestManager : IBlockInteractionHandler
    {
        /* Evento disparado quando o jogador clica com o botão direito (Use) */
        public void OnInteract(Player player, BlockInteractionEvent event)
        {
            Item heldItem = player.GetHeldItem();
            Block targetBlock = event.Block;

            /* Evita erro se o jogador interagir com a mão vazia */
            if (heldItem == null || heldItem.Tags == null) return;

            /* 1. Verificar se o item é um Tier Upgrade do nosso mod */
            if (heldItem.Tags.Contains("UpgradeToTier"))
            {
                string targetTier = heldItem.Tags.Get("UpgradeToTier");
                string requiredBlock = heldItem.Tags.Get("RequiredBaseBlock");

                /* 2. Validar o alvo. Se for Wood Chest (Vanilla) ou um baú do nosso mod */
                bool isVanillaTarget = (requiredBlock == "hytale:wood_chest" && targetBlock.Id == "hytale:wood_chest");
                bool isModTarget = targetBlock.Tags.Contains("IsAdvancedStorage") && IsNextTier(targetBlock, targetTier);

                if (targetBlock.Id == requiredBlock || isVanillaTarget || isModTarget)
                {
                    event.Cancel();
                    PerformUpgrade(player, targetBlock, heldItem, targetTier);
                }
            }
        }

        private void PerformUpgrade(Player player, Block oldBlock, Item upgradeItem, string newTier)
        {
            /* A. Salvar Inventários */
            IInventory oldMainInv = oldBlock.GetInventory("MainGrid"); 
            if (oldMainInv == null) oldMainInv = oldBlock.GetInventory();

            IInventory oldUpgradeInv = oldBlock.GetInventory("UpgradeInventory");
            
            List<ItemStack> savedMainItems = SaveInventory(oldMainInv);
            List<ItemStack> savedUpgrades = SaveInventory(oldUpgradeInv);

            /* B. Definir novo ID */
            string newBlockId = GetChestIdFromTier(newTier);

            /* C. Substituir Bloco */
            World.SetBlock(oldBlock.Position, Block.FromId(newBlockId));
            
            /* D. Restaurar Itens no Novo Bloco */
            Block newBlock = World.GetBlock(oldBlock.Position);
            
            /* Restaura o grid principal */
            if (newBlock.GetInventory("MainGrid") != null)
                RestoreInventory(newBlock.GetInventory("MainGrid"), savedMainItems);
            
            /* Restaura os upgrades */
            if (newBlock.GetInventory("UpgradeInventory") != null)
                RestoreInventory(newBlock.GetInventory("UpgradeInventory"), savedUpgrades);

            /* E. Consumir Upgrade e Feedback */
            player.RemoveHeldItem(1);
            player.PlaySound("SFX_Anvil_Use");
            player.SendNotification("Chest upgraded to Tier " + newTier + "!");
        }

        private string GetChestIdFromTier(string tier)
        {
            switch (tier)
            {
                case "1": return "advancedstorage:advanced_storage_chest_copper";
                case "2": return "advancedstorage:advanced_storage_chest_iron";
                case "3": return "advancedstorage:advanced_storage_chest_silver";
                case "4": return "advancedstorage:advanced_storage_chest_gold";
                case "5": return "advancedstorage:advanced_storage_chest_cobalt";
                case "6": return "advancedstorage:advanced_storage_chest_thorium";
                case "7": return "advancedstorage:advanced_storage_chest_adamantite";
                case "8": return "advancedstorage:advanced_storage_chest_mithril";
                case "9": return "advancedstorage:advanced_storage_chest_onyxium";
                default: return "hytale:wood_chest";
            }
        }

        private bool IsNextTier(Block currentBlock, string upgradeTier)
        {
            if (!currentBlock.Tags.Contains("StorageTier")) return false;
            int current = int.Parse(currentBlock.Tags.Get("StorageTier"));
            int next = int.Parse(upgradeTier);
            return next == current + 1;
        }

        private List<ItemStack> SaveInventory(IInventory inv)
        {
            List<ItemStack> items = new List<ItemStack>();
            if (inv == null) return items;
            
            for (int i = 0; i < inv.Size; i++) {
                ItemStack stack = inv.GetStack(i);
                if (stack != null && !stack.IsEmpty) {
                    items.Add(stack.Clone());
                } else {
                    items.Add(ItemStack.Empty);
                }
            }
            return items;
        }

        private void RestoreInventory(IInventory inv, List<ItemStack> items)
        {
            if (inv == null || items == null) return;

            for (int i = 0; i < inv.Size && i < items.Count; i++)
            {
                if (!items[i].IsEmpty)
                {
                    inv.SetStack(i, items[i]);
                }
            }
        }
    }
}