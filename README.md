# Dewy's Water Journey — Unity WebGL

Native Unity UGUI conversion of the supplied **Dewy's Water Journey** HTML prototype for PROG2006 Assessment 2.

## Preserved experience

- Home + 6 interactive story scenes + Credits in one native Unity scene.
- Portrait reference resolution: **450 × 900**.
- Original story copy and student credit: **Zhihe Zhang**.
- Scene 1: drag the sun upward to unlock **Evaporation**.
- Scene 2: drag all four droplets into the cloud for **Condensation**.
- Scene 3: tap the rain cloud four times for **Precipitation**.
- Scene 4: guide Dewy through ordered **Infiltration / Groundwater** checkpoints.
- Scene 5: guide Dewy through ordered **Runoff & Collection** checkpoints.
- Scene 6: guide Dewy through the river route back to the ocean and reveal the ending card.
- Next stays locked until the current scene interaction is complete.
- Sound on/off is persisted through `PlayerPrefs`.

## Audio assets

The supplied project archive contains all **18 original MP3 files** under `Assets/Resources/Audio/`, copied byte-for-byte with no codec validation or transcoding. The current ChatGPT GitHub connector used for this delivery can write UTF-8 repository files but cannot safely stream the larger local binary MP3 files into a new Git blob without changing/truncating bytes. For that reason the `main` branch intentionally does **not** contain a partially corrupted audio set.

To make a GitHub clone audio-complete, copy the `Assets/Resources/Audio/` folder from the complete project ZIP supplied with this delivery into the same path and commit the MP3 files normally. `DewyAudio.cs` already contains the original BGM/SFX mappings and safely ignores a missing clip, so the code/project structure remains usable while the binaries are absent.

## Unity version

The project is pinned to **Unity 6000.3.15f1 (Unity 6.3 LTS)** in `ProjectSettings/ProjectVersion.txt`.

## Open locally

1. Clone this repository (or use the complete project ZIP if you want the original audio already present).
2. In Unity Hub choose **Add project from disk** and select the repository folder.
3. Open it with Unity **6000.3.15f1** and ensure **Web Build Support** is installed.
4. Open `Assets/Scenes/Main.unity`.
5. Press Play.

The single scene contains a `DewyBootstrap` component. The complete Canvas, EventSystem, page flow and UGUI visuals are created natively at runtime.

## Build WebGL locally

Use **Dewy > Build WebGL for itch.io** in the Unity Editor. Output is written to:

`Builds/WebGL/`

The build helper applies:

- Web player size: 450 × 900
- Custom template: `PROJECT:Dewy`
- Gzip compression with decompression fallback
- Main scene: `Assets/Scenes/Main.unity`

## Unity Cloud / Build Automation

1. In Unity Cloud, connect the GitHub repository `pure-alone/Dewy-Water-Journey-Unity`.
2. Create a **Build Automation** target for **WebGL**.
3. Use branch **main**.
4. Select **Unity 6000.3 / Unity 6.3 LTS**; the project is pinned to `6000.3.15f1`.
5. For an audio-complete cloud build, commit the 18 MP3 files from the supplied complete project ZIP before triggering the build.
6. Trigger the build and download the WebGL artifact when it completes.

`Assets/Editor/DewyBuild.cs` includes a pre-build hook that reapplies the 450 × 900 WebGL/template settings during cloud builds.

## Deploy to itch.io

1. Extract the downloaded Unity Cloud WebGL artifact if needed.
2. Zip the **contents** of the WebGL output folder so `index.html` is at the ZIP root.
3. In itch.io create/edit the project and select **Kind of project = HTML**.
4. Upload the ZIP and mark it playable in the browser.
5. Use an embed viewport of **450 × 900**, or allow fullscreen/mobile scaling.
6. Test sound toggle, all drag/tap completion gates, Credits and Restart Journey.

## Key project files

- `Assets/Scenes/Main.unity` — single Unity scene.
- `Assets/Scripts/DewyBootstrap.cs` — scene bootstrap.
- `Assets/Scripts/DewyApp.cs` — Canvas, page state, navigation, progress, locked Next, toast.
- `Assets/Scripts/DewyPages.cs` — all Home/Scene 1–6/Credits visuals and interactions.
- `Assets/Scripts/DewyUI.cs` — reusable UGUI primitives and procedural rounded/circle sprites.
- `Assets/Scripts/DewyAudio.cs` — BGM/SFX mapping and persisted sound preference.
- `Assets/Scripts/DewyDragHandler.cs` — pointer drag callbacks.
- `Assets/Scripts/DewyContent.cs` — original story and Credits copy.
- `Assets/WebGLTemplates/Dewy/` — 450 × 900 WebGL shell.
- `Assets/Editor/DewyBuild.cs` — local/cloud WebGL settings.
- `tests/test_project_structure.py` — structural parity/build-readiness checks.

## Validation available without Unity

```bash
python -m unittest discover -s tests -v
```

The complete local delivery (including the 18 MP3s) passes these structural tests. They validate project structure, story copy, interaction coverage, audio inventory and byte identity, 450 × 900 configuration, scene registration and WebGL configuration. Actual C# compilation and WebGL player generation require the Unity Editor or Unity Build Automation.
