# Inventory System

## ✅ Completed

- [x] Floor & environment
- [x] Player movement
- [x] Third Person Perspective (TPP) camera
- [x] Placeholder object like `Chest`, `Shop` and `Crafting Bench`
- [x] Item System (`ScriptableObjects`, `categories`, `stackable` `items` and `database`)
- [x] Inventory Grid (UI) (`grid layout`, `slot prefab`, `drag & drop items`)
- [x] Stacking & Slot Logic (`Same items stack`, `maxStack`, `overflow goes to new slot`)

---

## 🚧 Next Steps

1. **Interactable Objects**

   - [x] Chest → open chest inventory
   - [ ] Shop → open shop inventory & currency system
   - [ ] Crafting Bench → open crafting UI

2. **Crafting System**

   - [ ] Make recipes as ScriptableObjects
   - [ ] Add crafting slots in the UI
   - [ ] Drag items into slots → if they match recipe → output result item

3. **Equipment System**

   - [ ] Add equipment slots (head, body, weapon, shield, etc.)
   - [ ] Items should be draggable into equipment slots
   - [ ] Equipped items should apply stats (damage, defense, buffs, etc.)

4. **Save / Load (optional)**
   - [ ] Save inventory data (JSON or something simple)
   - [ ] Save player’s inventory + chests + shop on exit
   - [ ] Load it again when the game starts
