﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class NoiseSettings
{
    public enum FilterType {Simple,Rigid};
    public FilterType filterType;
    public SimpleNoiseSettings simpleNoiseSettings;
    public RigidNoiseSettings rigidNoiseSettings;

    [System.Serializable]
    public class SimpleNoiseSettings
    {
        public float strength = 1;
        [Range(1, 10)]
        public int numLayers = 1;
        public float baseRoughness = 1;
        public float persistence = 0.5f;
        public float roughness = 2;
        public Vector3 center;
        public float minValue;
    }

    [System.Serializable]
    public class RigidNoiseSettings : SimpleNoiseSettings
    {
        public float weightMultiplier = 0.8f;
    }
}
