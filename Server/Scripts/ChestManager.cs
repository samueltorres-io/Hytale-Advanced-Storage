/* Server/Scripts/ChestManager */

/**
 * Este script fará o seguinte:
 * 1. intercepta o clique (copper tier upgrade -> wood chest)
 * 2. valida o upgrade
 * 3. salva os itens (que estavam no baú enteriormente)
 * 4. troca o bloco
 * 5. devolve os itens
*/

using Hytale.API;
using Hytale.API.Entities;
using Hytale.API.Blocks;
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

            /* 1. Verificar se o item é um Tier Upgrade do nosso mod */
            if (heldItem.Tags.Contains("UpgradeToTier"))
            {
                string targetTier = heldItem.Tags.Get("UpgradeToTier");
                string requiredBlock = heldItem.Tags.Get("RequiredBaseBlock");

                /* 2. Validar se o bloco clicado é o alvo correto para este upgrade */
                if (targetBlock.Id == requiredBlock || (targetBlock.Tags.Contains("IsAdvancedStorage") && IsNextTier(targetBlock, targetTier)))
                {
                    /* Cancela a abertura do baú vanilla para processar o upgrade */
                    event.Cancel();
                    
                    PerformUpgrade(player, targetBlock, heldItem, targetTier);
                }
            }
        }

        private void PerformUpgrade(Player player, Block oldBlock, Item upgradeItem, string newTier)
        {
            /* A. Salvar Inventário Antigo (Itens e upgrades)*/
            IInventory oldMainInv = oldBlock.GetInventory("MainGrid");
            IInventory oldUpgradeInv = oldBlock.GetInventory("UpgradeInventory");
            
            List<ItemStack> savedMainItems = SaveInventory(oldMainInv);
            List<ItemStack> savedUpgrades = SaveInventory(oldUpgradeInv);

            /* B. Definir o ID do novo bloco baseado no Tier (Mapeamento) */
            string newBlockId = GetChestIdFromTier(newTier);

            /* C. Substituir o Bloco no Mundo */
            World.SetBlock(oldBlock.Position, Block.FromId(newBlockId));
            
            /* D. Recuperar o novo inventário e inserir os itens salvos */
            Block newBlock = World.GetBlock(oldBlock.Position);

            RestoreInventory(newBlock.GetInventory("MainGrid"), savedMainItems);
            RestoreInventory(newBlock.GetInventory("UpgradeInventory"), savedUpgrades);

            IInventory newInventory = newBlock.GetInventory();

            foreach (var item in savedItems)
            {
                newInventory.AddItem(item);
            }

            /* E. Consumir o item de upgrade e feedback */
            player.RemoveHeldItem(1);
            // player.PlaySound("advancedstorage:sfx_upgrade_metal");
            player.SendNotification("Chest upgraded to level " + newTier + "!");
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
            /* Lógica simples: Se o baú atual é Tier 1, o upgrade deve ser Tier 2 */
            int current = int.Parse(currentBlock.Tags.Get("StorageTier"));
            int next = int.Parse(upgradeTier);
            return next == current + 1;
        }

        private List<ItemStack> SaveInventory(IInventory inv)
        {
            List<ItemStack> items = new List<ItemStack>();
            if (inv == null) return items;
            for (int i = 0; i < inv.Size; i++) {
                if (!inv.GetStack(i).IsEmpty) items.Add(inv.GetStack(i).Clone());
            }
            return items;
        }
    }
}