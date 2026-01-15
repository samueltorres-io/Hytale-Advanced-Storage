Arquitetura de Dados: O Componente AdvancedStorage
Não queremos criar 10 scripts diferentes para 10 baús. Queremos um único script que mude seu comportamento baseado no "Tier".

O que precisamos codificar primeiro:

Enum de Tiers: Definir Tier.COPPER, Tier.IRON, etc.

Componente de Bloco: Um script que armazena o tier atual, os upgrades instalados e o multiplicador de stack.




--------------------------------


Mecânica do "Evolved Core" e Evolução de Tier
Esta é a funcionalidade de clicar no baú para ele evoluir. No código, isso funcionará assim:

Interceptar o clique: O servidor detecta que o jogador usou o item TierUpgrade_Copper em um bloco Vanilla_Chest.

Validação: O script verifica se o bloco é um baú válido e se o jogador tem o upgrade correto para o próximo nível.

Transmutação:

O bloco antigo é removido (ou transformado).

Os itens internos são preservados em uma lista temporária.

O novo bloco (Advanced Chest) é colocado com o Tier definido como Copper.

Skin Swap: O modelo .blockymodel deve receber a textura correspondente (Chest_Copper_Texture.png).

Como você definiu que o upgrade é feito via uma ferramenta (Evolved Core), precisamos criar um script para esse item.


---------------------------



Sistema de Inventário Dinâmico (Slots e Multiplicadores)
Esta é a parte mais complexa. O Hytale gerencia inventários via JSON, mas para o seu mod, o tamanho do inventário precisa ser variável.

Slots Dinâmicos: Você precisará de uma função que calcule o layout da UI (User Interface) baseado no Tier. Se Tier = 1, renderiza 3x9. Se Tier = 9, renderiza 18x9.

Logic Stack Multiplier: * Você deve criar um "Hook" (gancho) no evento de OnItemAdded.

Se o baú tiver o Upgrade_Stack_T1, a lógica de MaxStackSize do item dentro daquele container deve ser ignorada e substituída por Item.DefaultStack * Multiplier.

A Interface Customizada (UI)
O Hytale usa arquivos JSON de UI para definir como os slots aparecem na tela. O baú vanilla usa uma grid simples, mas o seu precisa de:

Grid Principal: Dinâmica conforme o Tier.

Slots de Upgrade: Slots separados (à direita ou abaixo) para colocar os itens de "Stack Multiplier".

Sugestão de Estrutura para Common/UI/AdvancedChest.json: Você precisará definir um ContainerGrid que aponte para os slots de inventário e um SlotGroup específico para os upgrades.




-----------------------------------------


Gerenciamento de Multiplicadores (Stack Multiplier)
Para que o Tier 1 multiplique o stack por 2 e o Tier 5 por 64, não mexemos no item globalmente, mas sim na lógica de transferência de itens do seu baú.

Quando um item entra no baú, o script verifica os slots de upgrade.

Se houver um Upgrade_Stack_T1, o limite do slot é alterado de 100 para 200.

