using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalPreBuilding : PreBuilding
{
    private void OnTriggerEnter(Collider other)
    {
        if (IsActive == true)
        {
            if (other.TryGetComponent<Player>(out Player player))
            {
                if (player.Backpack.CollectedCrystal > 0)
                {
                    Cost -= player.Backpack.PayCrystalPrice(Cost, transform);
                    UI.SetValue();

                    if (Cost == 0)
                    {
                        Build();
                    }
                }
            }
        }
    }
}
