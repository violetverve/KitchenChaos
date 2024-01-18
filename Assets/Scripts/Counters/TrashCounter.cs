using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCounter : BaseCounter {
   
   public static event System.EventHandler OnAnyObjectTrashed;

   new public static void ResetStaticData() {
       OnAnyObjectTrashed = null;
   }

   public override void Interact(Player player) {
        if (player.HasKitchenObject()) {
            player.GetKitchenObject().DestroySelf();

            OnAnyObjectTrashed?.Invoke(this, System.EventArgs.Empty);
        }
   }
}
