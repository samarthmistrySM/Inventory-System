# 🗃️ Inventory System (Unity)

This is a modular, grid-based **inventory system** built in Unity for RPGs and adventure-style games.  
The goal was to make something flexible and expandable, while keeping it simple to plug into any project.  

Right now it already handles **stackable items, equipment, chests, shops, and crafting benches**, all powered by a **ScriptableObject-driven item system**.

---

## ✅ What’s Done

### 🎮 Core Gameplay
- **Floor & Environment**  
  Simple test scene setup with ground and placeholder objects.  

- **Player Movement**  
  Basic third-person controller with smooth movement & rotation.  

- **Camera (TPP)**  
  Third-person camera that follows the player and allows orbit/zoom.  

### 📦 Items & Inventory
- **Item System**  
  - Uses `ScriptableObjects` for easy item creation.  
  - Supports categories (Weapons, Armor, Consumables, etc.).  
  - Stackable and non-stackable items.  
  - Central database for item management.  

- **Inventory UI**  
  - Grid-based layout with prefab slots.  
  - Drag-and-drop support.  
  - Hover and drag feedback built in.  

- **Stacking & Slot Logic**  
  - Same items auto-stack.  
  - Respects max stack size per item.  
  - Overflow moves into the next available slot.  

### 🏪 Interactables
- **Chests**  
  - Open/close chest UI in-world.  
  - Move items between chest and player inventory.  

- **Shops**  
  - Placeholder shop UI.  
  - Currency system in place for transactions.  

- **Crafting Bench**  
  - Early setup for combining items into new ones.  

### ⚔️ Equipment
- Equipment slots in the UI (weapon, armor, etc.).  
- Drag items into slots to equip them.  
- Equipped items update player stats.  

---

## 🚧 Next Steps

Planned features I want to add:  
- **Save/Load**  
  - Store player inventory, chests, and shops (JSON or something simple).  
  - Load everything back in when the game starts.  

---

## 🛠️ Tech Stack

- Unity (2022+)  
- C# for logic  
- ScriptableObjects for items  
- Unity UI (Canvas + prefabs) for inventory and equipment  

---

## 📌 Notes

- The whole system is designed to be **modular**, so adding new slot types, categories, or item behaviors should be straightforward.  
- The current focus is on single-player, but the same system could be extended for multiplayer.  
- Save/load will probably use JSON for now, but other options (PlayerPrefs, binary, database) could work too.  

---

This is still a work in progress, but it’s already functional and can be dropped into most Unity projects with minimal setup.  
