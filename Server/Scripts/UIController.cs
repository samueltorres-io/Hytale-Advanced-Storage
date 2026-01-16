/* Server/Scripts/UIController.cs */

/**
 * Script será responsável por "forçar" o jogo a usar o arquivo AdvancedChest.json em vez
 * da interface de baú padrão quando o jogador abrir um baú do mod
*/

using Hytale.API;
using Hytale.API.Entities;
using Hytale.API.Blocks;
using Hytale.API.UI;

namespace AdvancedStorage.Server.Scripts
{
    public class UIController : IBlockInteractionHandler
    {
        public void OnInteract(Player player, BlockInteractionEvent event)
        {
            Block targetBlock = event.Block;

            /* Verifica se é um baú do nosso mod */
            if (targetBlock.Tags.Contains("IsAdvancedStorage"))
            {
                /* Cancela a abertura padrão do Hytale */
                event.Cancel();

                /* Abre a UI customizada passando o bloco como contexto */
                player.OpenCustomWindow("advancedstorage:AdvancedChest", targetBlock);
                
                player.PlaySound("SFX_Chest_Open");
            }
        }
    }
}