using ProceduralToolkit;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BedGenerator {

    public static GameObject BedCollider(
        float legWidth, float legHeight,
        float width, float depth,
        float platformHeight, float mattressHeight,
        float pillowHeight, float pillowSpacing, float pillowDepth,
        float headboardHeight, float footboardHeight)
    {
        GameObject bed = new GameObject("bed");
        //mattress and platform
        BoxCollider bC = bed.AddComponent<BoxCollider>();
        bC.size = new Vector3(width, platformHeight + mattressHeight, depth);
        bC.center = new Vector3(0, legHeight + (platformHeight + mattressHeight) / 2, 0);

        //headboard
        if (headboardHeight > 0) {
            GameObject head = new GameObject("headboard");
            BoxCollider hC = head.AddComponent<BoxCollider>();
            hC.size = new Vector3(width, headboardHeight, legWidth);
            hC.center = new Vector3(0, legHeight + platformHeight + headboardHeight * .5f, depth * .5f - legWidth * .5f);
            head.transform.parent = bed.transform;
        }

        //footboard
        if (footboardHeight > 0)
        {
            GameObject foot = new GameObject("footboard");
            BoxCollider fC = foot.AddComponent<BoxCollider>();
            fC.size = new Vector3(width, footboardHeight, legWidth);
            fC.center = new Vector3(0, legHeight + platformHeight + footboardHeight * .5f, -depth * .5f + legWidth * .5f);
            foot.transform.parent = bed.transform;
        }

        //legs
        Vector3 legPos = new Vector3(width * .5f - legWidth * .5f, legHeight * .5f, depth * .5f - legWidth * .5f);
        GameObject leg1 = new GameObject("leg");
        leg1.transform.parent = bed.transform;
        BoxCollider l1C = leg1.AddComponent<BoxCollider>();
        l1C.size = new Vector3(legWidth, legHeight, legWidth);
        l1C.center = legPos;

        GameObject leg2 = GameObject.Instantiate(leg1, bed.transform, false);
        leg2.GetComponent<BoxCollider>().center = Vector3.Scale(legPos, new Vector3(-1, 1, 1));
        GameObject leg3 = GameObject.Instantiate(leg1, bed.transform, false);
        leg3.GetComponent<BoxCollider>().center = Vector3.Scale(legPos, new Vector3(1, 1, -1));
        GameObject leg4 = GameObject.Instantiate(leg1, bed.transform, false);
        leg4.GetComponent<BoxCollider>().center = Vector3.Scale(legPos, new Vector3(-1, 1, -1));

        return bed;
    }

    public static MeshDraft Bed(
        float legWidth, float legHeight,
        float width, float depth,
        float platformHeight, float mattressHeight,
        float pillowHeight, float pillowSpacing, float pillowDepth,
        float headboardHeight, float footboardHeight,
        Color frameColor, Color beddingColor)
    {
        var bed = new MeshDraft { name = "Bed" };

        var frame = new MeshDraft();
        var bedding = new MeshDraft();

        //Generate bed platform
        frame.Add(platform(Vector3.up * legHeight, width, depth, platformHeight));
        //Generate mattress
        bedding.Add(platform(Vector3.up * (legHeight + platformHeight), 
            width - legWidth * 2, depth - legWidth * 2, mattressHeight));

        //generate legs
        float legX = width / 2 - legWidth / 2;
        float legZ = depth / 2 - legWidth / 2;
        frame.Add(leg(Vector3.right * legX + Vector3.forward * legZ, legWidth, legHeight));
        frame.Add(leg(Vector3.right * -legX + Vector3.forward * legZ, legWidth, legHeight));
        frame.Add(leg(Vector3.right * legX + Vector3.forward * -legZ, legWidth, legHeight));
        frame.Add(leg(Vector3.right * -legX + Vector3.forward * -legZ, legWidth, legHeight));

        //generate headboard
        if(headboardHeight > 0)
            frame.Add(platform(
                Vector3.up * (legHeight + platformHeight) + Vector3.forward * (depth / 2 - legWidth / 2), 
                width, legWidth, headboardHeight));

        //generate footboard
        if (footboardHeight > 0)
            frame.Add(platform(
                Vector3.up * (legHeight + platformHeight) + Vector3.back * (depth / 2 - legWidth / 2), 
                width, legWidth, footboardHeight));

        //generate pillow
        bedding.Add(platform(Vector3.up * (legHeight + platformHeight + mattressHeight) + 
            Vector3.forward * (depth / 2 - legWidth - pillowSpacing - pillowDepth / 2), 
            width - pillowSpacing * 2, pillowDepth, pillowHeight));

        //paint parts
        frame.Paint(frameColor);
        bedding.Paint(beddingColor);

        //assemble the bed
        bed.Add(frame);
        bed.Add(bedding);

        return bed;
    }

    private static MeshDraft leg(Vector3 center, float width, float height)
    {
        var draft = MeshDraft.Hexahedron(width, width, height);
        draft.Move(center + Vector3.up * height / 2);
        return draft;
    }

    private static MeshDraft platform(Vector3 center, float width, float length, float height)
    {
        var draft = MeshDraft.Hexahedron(width, length, height);
        draft.Move(center + Vector3.up * height / 2);
        return draft;
    }
}
