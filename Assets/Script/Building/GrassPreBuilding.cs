using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassPreBuilding : PreBuilding
{
    private void OnTriggerEnter(Collider other)
    {
        if (IsActive == true)
        {
            if (other.TryGetComponent<Player>(out Player player))
            {
                if (player.Backpack.HarvestedGrass > 0)
                {
                    Cost -= player.Backpack.PayGrassPrice(Cost, transform);
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
