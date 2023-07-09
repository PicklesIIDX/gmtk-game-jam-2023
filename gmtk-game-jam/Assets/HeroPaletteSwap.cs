using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class HeroPaletteSwap : MonoBehaviour
{
    [Serializable]
    public class HeroPalette
    {
        public Color OutlineColor = Color.black;
        public Color SkinColor;
        public Color TunicMainColor;
        public Color TunicAccentColor;
        public Color BootColor;
        public Color HairColor;
        public Color EyeWhiteColor = Color.white;
        public Color EyeColor;
    }

    public SpriteRenderer Sprite;

    public List<HeroPalette> AcceptablePalettes;
    private static HeroPalette LastPalette;
    public int SelectedPalette;
    public bool next;

    private void Update()
    {
        if (next)
        {
            next = false;
            SelectedPalette = (SelectedPalette + 1) % AcceptablePalettes.Count;
            SetPalette(SelectedPalette);
        }
        if (LastPalette != null)
        {
            SetPalette(LastPalette);
        }
    }

    private void Start()
    {
        if (gameObject.CompareTag("Player"))
        {
            RandomizePalette();
        }
        else if (LastPalette != null)
        {
            SetPalette(LastPalette);
        }
    }

    [ContextMenu("Randomize Palette")]
    private void RandomizePalette()
    {
        if (AcceptablePalettes.Count == 0) return;
        SelectedPalette = Random.Range(0, AcceptablePalettes.Count);
        SetPalette(SelectedPalette);
    }

    [ContextMenu("Set Palette")]
    private void SetPaletteToSelected()
    {
        SetPalette(SelectedPalette);
    }
    
    private void SetPalette(int index)
    {
        if (index < 0 || index >= AcceptablePalettes.Count) return;
        
        HeroPalette palette = AcceptablePalettes[index];
        SetPalette(palette);
    }

    private void SetPalette(HeroPalette palette)
    {
        LastPalette = palette;
        Sprite.material.SetColor("_OutlineColor", palette.OutlineColor);
        Sprite.material.SetColor("_SkinColor", palette.SkinColor);
        Sprite.material.SetColor("_TunicMainColor", palette.TunicMainColor);
        Sprite.material.SetColor("_TunicAccentColor", palette.TunicAccentColor);
        Sprite.material.SetColor("_BootColor", palette.BootColor);
        Sprite.material.SetColor("_HairColor", palette.HairColor);
        Sprite.material.SetColor("_EyeWhiteColor", palette.EyeWhiteColor);
        Sprite.material.SetColor("_EyeColor", palette.EyeColor);
    }
}
