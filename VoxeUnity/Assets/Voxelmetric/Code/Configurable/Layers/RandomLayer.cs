﻿using Assets.Voxelmetric.Code.Common.Math;
using UnityEngine;
using Voxelmetric.Code.Core;
using Voxelmetric.Code.Load_Resources;

public class RandomLayer: TerrainLayer
{
    private Block blockToPlace;
    private float chance;

    protected override void SetUp(LayerConfig config)
    {
        // Config files for random layers MUST define these properties
        blockToPlace = world.blockProvider.GetBlock(properties["blockName"]);

        if (properties.ContainsKey("blockColors"))
        {
            string[] colors = properties["blockColors"].Split(',');
            ((ColoredBlock)blockToPlace).color = new Color(byte.Parse(colors[0]) / 255f, byte.Parse(colors[1]) / 255f, byte.Parse(colors[2]) / 255f);
        }

        chance = float.Parse(properties["chance"]);
    }
    
    public override float GetHeight(Chunk chunk, int layerIndex, int x, int z, float heightSoFar, float strength)
    {
        var lpos = new Vector3(chunk.pos.x + x, heightSoFar + 1f, chunk.pos.z);
        float posChance = Randomization.Random(lpos.GetHashCode(), 200);

        if (chance > posChance)
        {
            return heightSoFar + 1;
        }

        return heightSoFar;
    }

    public override float GenerateLayer(Chunk chunk, int layerIndex, int x, int z, float heightSoFar, float strength)
    {
        var lpos = new Vector3(chunk.pos.x + x, heightSoFar + 1f, chunk.pos.z);
        float posChance = Randomization.Random(lpos.GetHashCode(), 200);

        if (chance > posChance)
        {
            SetBlocks(chunk, x, z, (int)heightSoFar, (int)(heightSoFar+1f), blockToPlace);

            return heightSoFar+1;
        }

        return heightSoFar;
    }
}