using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFootsteps : MonoBehaviour {

    private enum CURRENT_TERRAIN { GRASS, GRAVEL, WOOD_FLOOR, WATER, SAND };
    private string[] LayerSounds = { "event:/WalkingGrass", "event:/WalkingStone", "event:/WalkingWoodOS", "event:/WalkingWater", "event:/WalkingSand" };
    [SerializeField]
    private CURRENT_TERRAIN currentTerrain;

    private FMOD.Studio.EventInstance foosteps;

    private void Update()
    {
        DetermineTerrain();
    }

    private void DetermineTerrain()
    {
        RaycastHit[] hit;

        // Originally set at 10.0f, but needs to be set to 0.25 for Robot scenario due to how the level is built.
        hit = Physics.RaycastAll(transform.position, Vector3.down, 10.0f);

        foreach (RaycastHit rayhit in hit)
        {
            if (rayhit.transform.gameObject.layer == LayerMask.NameToLayer("Gravel"))
            {
                currentTerrain = CURRENT_TERRAIN.GRAVEL;
                break;
            }
            else if (rayhit.transform.gameObject.layer == LayerMask.NameToLayer("Wood"))
            {
                currentTerrain = CURRENT_TERRAIN.WOOD_FLOOR;
                break;
            }
            else if (rayhit.transform.gameObject.layer == LayerMask.NameToLayer("Grass"))
            {
                currentTerrain = CURRENT_TERRAIN.GRASS;
            }
            else if (rayhit.transform.gameObject.layer == LayerMask.NameToLayer("Water"))
            {
                currentTerrain = CURRENT_TERRAIN.WATER;
            }
            else if (rayhit.transform.gameObject.layer == LayerMask.NameToLayer("Sand"))
            {
                currentTerrain = CURRENT_TERRAIN.SAND;
            }
        }
    }

    public void SelectAndPlayFootstep()
    {     
        switch (currentTerrain)
        {
            case CURRENT_TERRAIN.GRAVEL:
                PlayFootstep(1);
                break;

            case CURRENT_TERRAIN.GRASS:
                PlayFootstep(0);
                break;

            case CURRENT_TERRAIN.WOOD_FLOOR:
                PlayFootstep(2);
                break;

            case CURRENT_TERRAIN.WATER:
                PlayFootstep(3);
                break;
            case CURRENT_TERRAIN.SAND:
                PlayFootstep(4);
                break;
            default:
                PlayFootstep(0);
                break;
        }
    }

    private void PlayFootstep(int terrain)
    {
        foosteps.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        foosteps = FMODUnity.RuntimeManager.CreateInstance(LayerSounds[terrain]);
        foosteps.setParameterByName("Terrain", terrain);
        foosteps.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
        foosteps.start();
        foosteps.release();
    }
}