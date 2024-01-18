using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter, IHasProgress {

    public static event EventHandler OnAnyCut;
    new public static void ResetStaticData() {
        OnAnyCut = null;
    }
    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;
    public event EventHandler OnCut;

    [SerializeField] private CuttingRecipeSO[] cutKitchenObjectSOArray;
    private int cuttingProgress;

    public override void Interact(Player player) {
        if (!HasKitchenObject()) {
            if (player.HasKitchenObject()) {
                if (HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO())) {
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                    cuttingProgress = 0;
                   
                    CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());
                   
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs { 
                        progressNormalized = (float)cuttingProgress / cuttingRecipeSO.cuttingProgressMax});
                }
            }
        } else {
            if (!player.HasKitchenObject()) {
                GetKitchenObject().SetKitchenObjectParent(player);
            } else {
                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject)) {
                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO())) {
                        GetKitchenObject().DestroySelf();
                    }
                }
            }
        }
    }

    public override void InteractAlternate(Player player) {
        if (HasKitchenObject() && HasRecipeWithInput(GetKitchenObject().GetKitchenObjectSO())) {
            cuttingProgress++;
            
            OnCut?.Invoke(this, EventArgs.Empty);
            OnAnyCut?.Invoke(this, EventArgs.Empty);
            
            CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());

            OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs { 
                    progressNormalized = (float)cuttingProgress / cuttingRecipeSO.cuttingProgressMax});
            if (cuttingProgress >= cuttingRecipeSO.cuttingProgressMax) {
                KitchenObjectSO outputKitchenObjectSO = GetOutputForInput(GetKitchenObject().GetKitchenObjectSO());
                GetKitchenObject().DestroySelf();
                KitchenObject.SpawnKitchenObject(outputKitchenObjectSO, this);
            }
        }
    }

    private bool HasRecipeWithInput(KitchenObjectSO input) {
        return GetCuttingRecipeSOWithInput(input) != null;
    }

    private KitchenObjectSO GetOutputForInput(KitchenObjectSO input) {
        CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(input);
        if (cuttingRecipeSO != null) {
           return cuttingRecipeSO.output;
        }
        return null;
    }

    private CuttingRecipeSO GetCuttingRecipeSOWithInput(KitchenObjectSO input) {
        foreach (CuttingRecipeSO cuttingRecipeSO in cutKitchenObjectSOArray) {
            if (cuttingRecipeSO.input == input) {
                return cuttingRecipeSO;
            }
        }

        return null;
    }
}

