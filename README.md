# Hytale Advanced Storage (Evolved Storage)

O Advanced Storage é um mod focado em elevar o patamar de organização e logística no Hytale. Inspirado pelo clássico Sophisticated Storage, este mod introduz um sistema de baús modulares, upgrades de capacidade e compressão de itens através de uma progressão baseada em Tiers.

**Status:** development

---

## Funcionalidades Principais

**Progressão por Tiers:** Evolua seus baús desde madeira básica até metais lendários como Thorium.

**Sistema de Upgrades In-Place:** Utilize o *Evolved Core* para realizar upgrades visuais e funcionais em tempo real.

**Modularidade:** Adicione funcionalidades (como expansão de stack) através de slots de upgrade dedicados.

**Balanceamento Nativo:** Totalmente integrado aos materiais e níveis de workbench do Hytale.

---

## Progressão de Tiers (Chest)

Diferente do vanilla, onde baús são limitados a 18 slots, o Advanced Storage permite uma expansão massiva:

| Tier | Nome | Material Base | Slots | Slots Upgrade | Requisito Workcench | Receita de Upgrade (Template +) |
| ---- | ---- | ------------- | ----- | ---------------- | ------------------- | ---------------------------- |
| 0 | Vanilla Chest | Wood | 18 (2x9) | 0 | Nível 1 | --- |
| 1 | Cooper Chest | Cooper Ingot | 27 (3X9) | 1 | Nível 1 | Template + 4 Cooper Ingot |
| 2 | Iron Chest | Iron Ingot | 45 (5x9) | 2 | Nível 2 | Last Template + 4 Iron Ingot |
| 3 | Silver Chest | Silver Ingot | 63 (7x9) | 3 | Nível 2 | Last Template + 4 Silver Ingot |
| 4 | Gold Chest | Gold Ingot | 72 (8x9) | 3 | Nível 2 | Last Template + 4 Gold Ingot |
| 5 | Cobalt Chest | Cobalt Ingot | 81 (9x9) | 4 | Nível 3 | Last Template + 4 Cobalt Ingot |
| 6 | Thorium Chest | Thorium Ingot | 90 (10x9) | 4 | Nível 3 | Last Template + 4 Thorium Ingot |
| 7 | Adamantite Chest | Adamantite Ingot | 108 (12x9) | 5 | Nível 3 | Last Template + 4 Adamantite Ingot |
| 8 | Mithril Chest | Mithril Ingot | 135 (15x9) | 6 | Nível 4 | Last Template + 4 Mithril Ingot |
| 9 | Onyxium Chest | Onyxium Ingot | 162 (18x9) | 7 | Nível 4 | Last Template + 4 Onyxium Ingot |

---

## Sistema de Tier Upgrades (Upgrade de Tier dos Baús)

O jogador precisa de um *Template de Upgrade* (um item base) que, ao ser combinado com o metal do próximo Tier, se transforma no item que realiza o upgrade no baú.

Este é o item que o jogador crafta na Workbench para iniciar qualquer processo de evolução de Tier.

- **Craft:** 4 Iron Ingots + 1 Paper/Scroll.

- **Função:** Serve como base para todos os upgrades de Tier do báu.

O primeiro upgrade é o base, que apenas serve para a criação dos outros templates.

Usamos o template base para criar o template de cooper.
Para criar o proximo template tier, que seria o de iron, usamos o template de copper, e assim vai para os outros upgrades tiers.

---

## Sistema de Upgrades

Upgrades colcoanos nos slots de upgrade de um baú!

Upgrades de quantidade de itens em um slot, magnetic, etc...

### Stack Upgrade

O gerenciamento de espaço é otimizado através de Stack Upgrades. Eles não aumentam o número de slots, mas multiplicam a capacidade de cada slot individualmente de forma linear, respeitando as bases do Hytale (25 para Gemas e 100 para Ingots).

| Upgrade | Multiplicador | Exemplo Gemas (25) | Exemplo all (100) |
| ------- | ------------- | ------------------ | ----------------- |
| Tier 1 (Base) | x2 | 50 | 200 |
| Tier 2 (Advanced) | x4 | 100 | 400 |
| Tier 3 (Elite) | x8 | 200 | 800 |
| Tier 4 (Ultimate) | x16 | 400 | 1600 |
| Tier 5 (Supreme) | x64 | 1600 | 6400 |

---

## Mecânicas de Crafting & Economia

### O "Evolved Core" (O Ativador)

Para evoluir um baú, o jogador não utiliza a bancada de craft diretamente no bloco, mas sim uma ferramenta de transmutação.

- **Receita:** 4 Iron Ingots + 1 Fiber + 1 Wood Chest.

- **Mecânica:** Clique com o botão direito em um baú colocado no mundo para ativar a evolução visual e funcional.

### Upgrades Laterais

Os upgrades são itens físicos inseridos na interface do baú, nos slots de upgrade.

- **Exemplo (Stack T1):** 4 Iron Ingots + 2 Leathers.

---

## Regras de Uso e Energia

Para manter o equilíbrio (balance) e evitar abusos:

- **Pontos de Upgrade:** Cada baú possui uma capacidade de "Energia de Modificação". Upgrades de Tier maior consomem mais pontos, forçando o jogador a escolher estrategicamente entre diferentes tipos de melhorias.

- **Impossibilidade de Downgrade:** Devido à integridade dos dados de inventário (evitar perda de itens em slots que deixariam de existir), não é possível reverter um tier de baú. Uma vez evoluído, ele permanece no tier atual ou superior.

- **Limite por Categoria:** Cada container suporta no máximo 2 upgrades de uma mesma categoria (ex: 2 de Stack), independente do nível.

---

## Tech Stack (Hytale Modding)

**Nota:** Este mod está em fase *Beta*. Se você é um criador de conteúdo ou desenvolvedor e deseja colaborar, sinta-se à vontade para abrir uma Issue ou enviar um Pull Request.