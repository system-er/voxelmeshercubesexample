using Godot;
using System;


public class voxelmeshercubesexample : Spatial
{

    private VoxelBuffer vb = new VoxelBuffer();
    private VoxelColorPalette vcp = new VoxelColorPalette();



    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        //set some colors for example
        vcp.SetColor(1, Color.Color8(0, 0, 0)); //black
        vcp.SetColor(2, Color.Color8(255, 255, 255)); //white
        for (int i = 1; i < 9; i++)
        {
            vcp.SetColor(i + 7, Color.Color8((byte)(i * 32 - 1), (byte)(i * 32 - 1), (byte)(i * 32 - 1))); //grey
        }
        for (byte i = 1; i < 9; i++)
        {
            vcp.SetColor(i + 15, Color.Color8((byte)(i * 32 - 1), 0, 0)); //red
        }
        for (byte i = 1; i < 9; i++)
        {
            vcp.SetColor(i + 23, Color.Color8(0, (byte)(i * 32 - 1), 0)); //green
        }
        for (byte i = 1; i < 9; i++)
        {
            vcp.SetColor(i + 31, Color.Color8(0, 0, (byte)(i * 32 - 1))); //blue
        }
        for (byte i = 1; i < 9; i++)
        {
            vcp.SetColor(i + 39, Color.Color8((byte)(i * 32 - 1), 0, (byte)(i * 32 - 1))); //pink
        }
        for (byte i = 1; i < 9; i++)
        {
            vcp.SetColor(i + 47, Color.Color8((byte)(i * 32 - 1), (byte)(i * 32 - 1), 0)); //yellow
        }
        for (byte i = 1; i < 9; i++)
        {
            vcp.SetColor(i + 55, Color.Color8(0, (byte)(i * 32 - 1), (byte)(i * 32 - 1))); //lightblue
        }
        for (byte i = 1; i < 9; i++)
        {
            vcp.SetColor(i + 63, Color.Color8((byte)(i * 32 - 1), (byte)(i * 14 - 1), 0)); //orange
        }
        for (byte i = 1; i < 9; i++)
        {
            vcp.SetColor(i + 71, Color.Color8((byte)(i * 12 - 1), 0, (byte)(i * 32 - 1))); //violet
        }



        VoxelTool vt = vb.GetVoxelTool();
        vt.Channel = ((int)VoxelBuffer.ChannelId.ChannelColor);

        MeshInstance mi = new MeshInstance();
        VoxelMesherCubes mesher = new VoxelMesherCubes();
        mesher.ColorMode = VoxelMesherCubes.ColorModeEnum.MesherPalette;
        mesher.Palette = vcp;
        mesher.GreedyMeshingEnabled = true;
        Godot.Collections.Array gdmat = new Godot.Collections.Array();
        var mat1 = (SpatialMaterial)GD.Load("res://materials/vertex_color_material.tres"); //material with Vertex Color->Use As Albedo->true
        var mat2 = (SpatialMaterial)GD.Load("res://materials/transparent_color_material.tres");//material with Flags->Transparent->true
        gdmat.Add(mat1);
        gdmat.Add(mat2);

        vb.Create(64, 32, 64);
        vb.SetChannelDepth(2, VoxelBuffer.Depth.Depth8Bit);

        vt.Value = 39;
        vt.DoBox(new Vector3(1, 1, 1), new Vector3(15, 1, 15));

        vt.Value = 79;
        vt.DoSphere(new Vector3(7, 7, 7), 4);

        //colortable
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                for (int k = 1; k < 2; k++)
                {
                    vb.SetVoxel((ulong)(j * 8 + i), i + 1, j + 2, k + 1, 2);
                }
            }
        }


        mi.Mesh = mesher.BuildMesh(vb, gdmat);
        AddChild(mi);

    }


}
