using Hytale.API;
using Hytale.API.Blocks;
using Hytale.API.Inventory;
using System.Linq;

namespace AdvancedStorage.Server.Scripts
{
    /**
     * Gerencia a lógica de multiplicação de slots baseada nos upgrades inseridos
     */
    public class StackUpgradeHandler : IInventoryChangedHandler
    {
        /* Este evento é disparado sempre que algo muda nos inventários do bloco */
        public void OnInventoryChanged(Block block, string containerId)
        {
            /* Só nos interessa se a mudança foi no inventário de UPGRADES */
            if (containerId == "UpgradeInventory")
            {
                ApplyStackLimit(block);
            }
        }

        private void ApplyStackLimit(Block block)
        {
            int multiplier = CalculateTotalMultiplier(block);
            
            /* Acessa o inventário principal do baú */
            IInventory mainInv = block.GetInventory("MainGrid");

            /* Aplica o multiplicador em cada slot individualmente */
            /* No Hytale, modificamos o MaxStackSize permitido para o container específico */
            mainInv.SetCustomMaxStackMultiplier(multiplier);
        }

        private int CalculateTotalMultiplier(Block block)
        {
            IInventory upgradeInv = block.GetInventory("UpgradeInventory");
            int totalMultiplier = 1;

            /* Percorre os slots de upgrade procurando por "StackMultiplier" */
            for (int i = 0; i < upgradeInv.Size; i++)
            {
                ItemStack stack = upgradeInv.GetStack(i);
                if (!stack.IsEmpty && stack.Item.Tags.Contains("StackMultiplier"))
                {
                    int itemMultiplier = int.Parse(stack.Item.Tags.Get("StackMultiplier"));
                    
                    /* Para evitar valores infinitos, vamos usar o maior valor presente ou somar */
                    totalMultiplier = System.Math.Max(totalMultiplier, itemMultiplier);
                }
            }

            return totalMultiplier;
        }
    }
}