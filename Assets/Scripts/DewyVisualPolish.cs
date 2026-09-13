using UnityEngine;
using UnityEngine.UI;

public sealed class DewyVisualPolish : MonoBehaviour
{
    private DewyApp app;
    private DewyApp.Page lastPage = (DewyApp.Page)(-1);
    private RectTransform lastStage;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    private static void Install()
    {
        if (Object.FindFirstObjectByType<DewyVisualPolish>() != null) return;
        new GameObject("DewyVisualPolish").AddComponent<DewyVisualPolish>();
    }

    private void Update()
    {
        if (app == null) app = Object.FindFirstObjectByType<DewyApp>();
        if (app == null || app.Stage == null) return;

        if (lastStage == app.Stage && lastPage == app.CurrentPage) return;
        lastStage = app.Stage;
        lastPage = app.CurrentPage;
        ApplyPagePolish();
    }

    private void ApplyPagePolish()
    {
        switch (app.CurrentPage)
        {
            case DewyApp.Page.Home:
                FixHomeSunRays();
                break;
            case DewyApp.Page.Scene1:
                AddScene1Arrow();
                break;
            case DewyApp.Page.Scene4:
                AddGuidedPath(
                    "GroundwaterPath",
                    new Vector2(224f, 162f),
                    DewyPages.GROUNDWATER_CHECKPOINTS);
                break;
            case DewyApp.Page.Scene5:
                AddGuidedPath(
                    "StreamPath",
                    new Vector2(96f, 168f),
                    DewyPages.STREAM_CHECKPOINTS);
                break;
            case DewyApp.Page.Scene6:
                AddGuidedPath(
                    "RiverPath",
                    new Vector2(88f, 168f),
                    DewyPages.RIVER_CHECKPOINTS);
                break;
        }
    }

    private void FixHomeSunRays()
    {
        RectTransform sun = app.Stage.Find("Sun") as RectTransform;
        if (sun == null) return;

        float sunWidth = sun.rect.width > 0f ? sun.rect.width : sun.sizeDelta.x;
        float sunHeight = sun.rect.height > 0f ? sun.rect.height : sun.sizeDelta.y;
        float centerX = sun.anchoredPosition.x + sunWidth * 0.5f;
        float centerY = -sun.anchoredPosition.y + sunHeight * 0.5f;
        float radius = Mathf.Max(sunWidth, sunHeight) * 0.72f;

        for (int i = 0; i < 12; i++)
        {
            RectTransform ray = app.Stage.Find("Ray" + i) as RectTransform;
            if (ray == null) continue;

            float angle = -90f + i * 30f;
            float radians = angle * Mathf.Deg2Rad;
            float rayX = centerX + Mathf.Cos(radians) * radius;
            float rayY = centerY + Mathf.Sin(radians) * radius;

            ray.anchorMin = ray.anchorMax = new Vector2(0f, 1f);
            ray.pivot = new Vector2(0.5f, 0.5f);
            ray.sizeDelta = new Vector2(6f, 20f);
            ray.anchoredPosition = new Vector2(rayX, -rayY);
            ray.localEulerAngles = new Vector3(0f, 0f, angle + 90f);
            Image image = ray.GetComponent<Image>();
            if (image != null) image.raycastTarget = false;
        }
    }

    private void AddScene1Arrow()
    {
        RectTransform sun = app.Stage.Find("Sun") as RectTransform;
        if (sun == null || sun.Find("SunArrowGraphic") != null) return;

        float size = sun.rect.width > 0f ? sun.rect.width : 92f;
        RectTransform root = DewyUI.MakeRect("SunArrowGraphic", sun);
        DewyUI.Stretch(root);
        Color color = DewyUI.Hex("#9B6811");

        Image shaft = DewyUI.Panel("Shaft", root, color, size * 0.5f - 2f, size * 0.34f, 4f, size * 0.31f, true);
        shaft.raycastTarget = false;

        Image headLeft = DewyUI.Panel("HeadLeft", root, color, size * 0.5f - 12f, size * 0.27f, 4f, 18f, true);
        headLeft.rectTransform.localEulerAngles = new Vector3(0f, 0f, -45f);
        headLeft.raycastTarget = false;

        Image headRight = DewyUI.Panel("HeadRight", root, color, size * 0.5f + 8f, size * 0.27f, 4f, 18f, true);
        headRight.rectTransform.localEulerAngles = new Vector3(0f, 0f, 45f);
        headRight.raycastTarget = false;
    }

    private void AddGuidedPath(string name, Vector2 startCenter, Vector2[] checkpoints)
    {
        if (app.Stage.Find(name) != null || checkpoints == null || checkpoints.Length == 0) return;

        Vector2[] route = new Vector2[checkpoints.Length + 1];
        route[0] = startCenter;
        for (int i = 0; i < checkpoints.Length; i++)
            route[i + 1] = checkpoints[i] + new Vector2(14f, 14f);

        RectTransform root = DewyUI.MakeRect(name, app.Stage);
        DewyUI.Stretch(root);

        Transform firstCheckpoint = app.Stage.Find("Checkpoint0");
        if (firstCheckpoint != null)
            root.SetSiblingIndex(firstCheckpoint.GetSiblingIndex());

        Color dotColor = new Color(1f, 1f, 1f, 0.68f);
        const float dotSize = 7f;
        const float spacing = 17f;

        for (int segment = 0; segment < route.Length - 1; segment++)
        {
            Vector2 a = route[segment];
            Vector2 b = route[segment + 1];
            float distance = Vector2.Distance(a, b);
            int steps = Mathf.Max(2, Mathf.FloorToInt(distance / spacing));

            for (int step = 1; step < steps; step++)
            {
                Vector2 p = Vector2.Lerp(a, b, step / (float)steps);
                Image dot = DewyUI.Circle(
                    "PathDot_" + segment + "_" + step,
                    root,
                    dotColor,
                    p.x - dotSize * 0.5f,
                    p.y - dotSize * 0.5f,
                    dotSize);
                dot.raycastTarget = false;
            }
        }
    }
}
