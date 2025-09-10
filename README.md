# Inventory System

## ✅ Completed
- [x] Floor & environment
- [x] Player movement
- [x] Third Person Perspective (TPP) camera

---

## 🚧 Next Steps

1. **Item System**
   - [ ] Make ScriptableObjects for items
   - [ ] Add some basic categories (resources, weapons, armor, consumables)
   - [ ] Each item should have: icon, stackable or not, max stack size

2. **Inventory Grid (UI)**
   - [ ] Build a simple grid layout in Unity UI
   - [ ] Create a slot prefab (bg image + item icon + quantity text)
   - [ ] Items should be draggable between slots

3. **Stacking & Slot Logic**
   - [ ] Same items should stack together
   - [ ] Don’t go over the item’s `maxStack`
   - [ ] If stack is full → put item in a new slot

4. **Crafting System**
   - [ ] Make recipes as ScriptableObjects
   - [ ] Add crafting slots in the UI
   - [ ] Drag items into slots → if they match recipe → output result item

5. **Equipment System**
   - [ ] Add equipment slots (head, body, weapon, shield, etc.)
   - [ ] Items should be draggable into equipment slots
   - [ ] Equipped items should apply stats (damage, defense, buffs, etc.)

6. **Save / Load (optional)**
   - [ ] Save inventory data (JSON or something simple)
   - [ ] Save player’s inventory on exit
   - [ ] Load it again when the game starts
