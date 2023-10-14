using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class CustomCursor: Singleton<CustomCursor>
{
    public Texture2D[] cursors;
    private Texture2D currentCursor;
    private Vector2 cursorHotspot;
    void Start()
    {
        cursorHotspot = Vector2.zero;
        currentCursor = cursors[0];
        Cursor.SetCursor(currentCursor, cursorHotspot, CursorMode.Auto);
    }

    public void setCursor(int _index)
    {
        currentCursor = cursors[_index];
        Cursor.SetCursor(currentCursor, cursorHotspot, CursorMode.Auto);
    }

}
