/* Server/Scripts/StackUpgradeHandler.cs */

using Hytale.API;
using Hytale.API.Blocks;
using Hytale.API.Inventory;
using System;

namespace AdvancedStorage.Server.Scripts
{
    public class StackUpgradeHandler : IInventoryChangedHandler
    {
        public void OnInventoryChanged(Block block, string containerId)
        {
            /* Só processa se a mudança for no inventário de upgrades */
            if (containerId == "UpgradeInventory") {
                ValidateAndApply(block);
            }
        }

        private void ValidateAndApply(Block block)
        {
            IInventory upgradeInv = block.GetInventory("UpgradeInventory");
            IInventory mainInv = block.GetInventory("MainGrid");

            int totalEnergyUsed = 0;
            int stackUpgradeCount = 0;
            int totalMultiplier = 0;

            /* Pega a energia máxima do baú (Tag definida no JSON do Bloco) */
            int.TryParse(block.Tags.Get("UpgradeEnergyMax") ?? "5", out int maxEnergy);

            for (int i = 0; i < upgradeInv.Size; i++)
            {
                ItemStack stack = upgradeInv.GetStack(i);
                if (stack.IsEmpty) continue;

                /* 1. Soma o custo de energia de qualquer item no slot de upgrade */
                int.TryParse(stack.Item.Tags.Get("UpgradeCost") ?? "0", out int cost);
                totalEnergyUsed += cost;

                /* 2. Lógica específica para Stack Upgrades */
                if (stack.Item.Tags.Get("UpgradeType") == "Stack") {
                    stackUpgradeCount++;
                    int.TryParse(stack.Item.Tags.Get("StackMultiplier") ?? "1", out int m);
                    totalMultiplier += m; /* Soma linear: x2 + x2 = x4 */
                }
            }

            block.Tags.Set("CurrentEnergy", totalEnergyUsed.ToString());

            /* Validação de Regras */
            bool overEnergy = totalEnergyUsed > maxEnergy;
            bool overLimit = stackUpgradeCount > 2;

            if (overEnergy || overLimit) {
                /* Penalidade: Se violar as regras, o baú volta ao stack padrão (x1) */
                mainInv.SetCustomMaxStackMultiplier(1);
                block.Tags.Set("EnergyStatus", "OVERLOAD");
            } else {
                mainInv.SetCustomMaxStackMultiplier(Math.Max(1, totalMultiplier));
                block.Tags.Set("EnergyStatus", "OK");
            }
        }
    }
}