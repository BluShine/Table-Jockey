using System.Collections.Generic;
using UnityEngine;

namespace ProceduralToolkit.Examples.UI
{
    public class ChairGeneratorUI : UIBase
    {
        public MeshFilter chairMeshFilter;
        public MeshFilter platformMeshFilter;
        public RectTransform leftPanel;

        [Space]
        [Range(minLegWidth, maxLegWidth)]
        public float legWidth = 0.07f;
        [Range(minLegHeight, maxLegHeight)]
        public float legHeight = 0.7f;
        [Range(minSeatWidth, maxSeatWidth)]
        public float seatWidth = 0.7f;
        [Range(minSeatDepth, maxSeatDepth)]
        public float seatDepth = 0.7f;
        [Range(minSeatHeight, maxSeatHeight)]
        public float seatHeight = 0.05f;
        [Range(minBackHeight, maxBackHeight)]
        public float backHeight = 0.8f;
        public bool hasStretchers = true;
        public bool hasArmrests = false;

        private const float minLegWidth = 0.05f;
        private const float maxLegWidth = 0.12f;
        private const float minLegHeight = 0.5f;
        private const float maxLegHeight = 1.2f;
        private const float minSeatWidth = 0.5f;
        private const float maxSeatWidth = 1.2f;
        private const float minSeatDepth = 0.3f;
        private const float maxSeatDepth = 1.2f;
        private const float minSeatHeight = 0.03f;
        private const float maxSeatHeight = 0.2f;
        private const float minBackHeight = 0.5f;
        private const float maxBackHeight = 1.3f;

        private const float platformBaseOffset = 0.05f;
        private const float platformHeight = 0.05f;
        private const float platformRadiusOffset = 0.5f;
        private const int platformSegments = 128;

        private List<ColorHSV> targetPalette = new List<ColorHSV>();
        private List<ColorHSV> currentPalette = new List<ColorHSV>();

        private void Awake()
        {
            RenderSettings.skybox = new Material(RenderSettings.skybox);

            Generate();
            currentPalette.AddRange(targetPalette);
        }

        private void Update()
        {
            
        }

        public void Generate()
        {
            targetPalette = RandomE.TetradicPalette(0.25f, 0.75f);
            targetPalette.Add(ColorHSV.Lerp(targetPalette[2], targetPalette[3], 0.5f));

            var chairDraft = ChairGenerator.Chair(legWidth, legHeight, seatWidth, seatDepth, seatHeight, backHeight,
                hasStretchers, hasArmrests, targetPalette[0].WithSV(0.8f, 0.8f).ToColor());
            var chairMesh = chairDraft.ToMesh();
            chairMesh.RecalculateBounds();
            chairMeshFilter.mesh = chairMesh;
            ChairGenerator.ChairCollider(legWidth, legHeight, seatWidth, seatDepth, seatHeight, backHeight,
                hasStretchers, hasArmrests).transform.SetParent(chairMeshFilter.transform, false);
        }
    }
}