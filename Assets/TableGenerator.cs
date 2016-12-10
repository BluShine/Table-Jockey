using ProceduralToolkit;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TableGenerator {

    public static GameObject TableCollider(
        float legWidth, float legHeight,
        float tableWidth, float tableDepth, float tableHeight)
    {
        GameObject table = new GameObject("table top");
        //table top
        BoxCollider tC = table.AddComponent<BoxCollider>();
        tC.size = new Vector3(tableWidth, tableHeight, tableDepth);
        tC.center = Vector3.up * (legHeight + tableHeight / 2);
        //legs
        Vector3 legPos = new Vector3(tableWidth / 2 - legWidth / 2, legHeight / 2, tableDepth / 2 - legWidth / 2);
        GameObject leg1 = new GameObject("leg");
        leg1.transform.parent = table.transform;
        BoxCollider l1C = leg1.AddComponent<BoxCollider>();
        l1C.size = new Vector3(legWidth, legHeight, legWidth);
        l1C.center = legPos;

        GameObject leg2 = GameObject.Instantiate(leg1, table.transform, false);
        leg2.GetComponent<BoxCollider>().center = Vector3.Scale(legPos, new Vector3(-1, 1, 1));

        GameObject leg3 = GameObject.Instantiate(leg1, table.transform, false);
        leg3.GetComponent<BoxCollider>().center = Vector3.Scale(legPos, new Vector3(1, 1, -1));

        GameObject leg4 = GameObject.Instantiate(leg1, table.transform, false);
        leg4.GetComponent<BoxCollider>().center = Vector3.Scale(legPos, new Vector3(-1, 1, -1));

        return table;
    }

    public static MeshDraft Table(
        float legWidth, float legHeight,
        float tableWidth, float tableDepth, float tableHeight,
        Color color)
    {
        var table = new MeshDraft { name = "Table" };

        //Generate table top
        table.Add(tableTop(Vector3.up * legHeight, tableWidth, tableDepth, tableHeight));

        //generate legs
        float legX = tableWidth / 2 - legWidth / 2;
        float legZ = tableDepth / 2 - legWidth / 2;
        table.Add(leg(Vector3.right * legX + Vector3.forward * legZ, legWidth, legHeight));
        table.Add(leg(Vector3.right * -legX + Vector3.forward * legZ, legWidth, legHeight));
        table.Add(leg(Vector3.right * legX + Vector3.forward * -legZ, legWidth, legHeight));
        table.Add(leg(Vector3.right * -legX + Vector3.forward * -legZ, legWidth, legHeight));

        table.Paint(color);

        return table;
    }

    private static MeshDraft leg(Vector3 center, float width, float height)
    {
        var draft = MeshDraft.Hexahedron(width, width, height);
        draft.Move(center + Vector3.up * height / 2);
        return draft;
    }

    private static MeshDraft tableTop(Vector3 center, float width, float length, float height)
    {
        var draft = MeshDraft.Hexahedron(width, length, height);
        draft.Move(center + Vector3.up * height / 2);
        return draft;
    }
}
