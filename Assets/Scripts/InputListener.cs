#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[InitializeOnLoad]
public static class InputListener
{
    static InputListener()
    {
        SceneView.duringSceneGui += SceneViewOnDuringSceneGui;
    }

    static void SceneViewOnDuringSceneGui(SceneView sceneView)
    {
        if (!Spline.EditingSpline || !Spline.EditingSpline.IsDrawingSpline)
            return;

        var e = Event.current;
        if (e.type == EventType.MouseDown && e.button == 0)
        {
            var mousePos = e.mousePosition;
            var viewportPoint = new Vector2(mousePos.x / sceneView.cameraViewport.width, 1.0f - mousePos.y / sceneView.cameraViewport.height);
            var screenRay = sceneView.camera.ViewportPointToRay(viewportPoint);
            if (Physics.Raycast(screenRay, out var hitInfo, float.MaxValue, LayerMask.GetMask("Ground")))
            {
                Spline.EditingSpline.AddSplinePoint(hitInfo.point);

                Event.current.Use();
            }
        }
    }
}
#endif