# Dewy's Water Journey — Unity WebGL

Native Unity conversion of the supplied **Dewy's Water Journey** HTML prototype for PROG2006 Assessment 2.

## What is preserved

- Home + 6 interactive story scenes + Credits.
- Portrait reference resolution: **450 × 900**.
- Same story copy, student credit (**Zhihe Zhang**), progress flow and water-cycle learning labels.
- Scene 1: drag the sun upward to trigger evaporation.
- Scene 2: drag all four droplets into the cloud for condensation.
- Scene 3: tap the rain cloud four times to refresh the forest.
- Scene 4: drag Dewy through ordered infiltration / groundwater checkpoints.
- Scene 5: drag Dewy downhill through runoff / collection checkpoints as the stream grows.
- Scene 6: guide Dewy through the river route back to the ocean, then show the ending card.
- Next is locked until the current scene interaction is completed.
- Sound on/off preference persists with `PlayerPrefs`.
- All original MP3 BGM and SFX are committed under `Assets/Resources/Audio/`.

## Unity version

The project is pinned to **Unity 6000.3.15f1 (Unity 6.3 LTS)** in `ProjectSettings/ProjectVersion.txt`.

## Open locally

1. Clone this repository.
2. In Unity Hub, choose **Add project from disk** and select the repository folder.
3. Install/open with Unity **6000.3.15f1** and make sure **Web Build Support** is installed.
4. Open `Assets/Scenes/Main.unity`.
5. Press Play. The UI is created at runtime by `DewyBootstrap` / `DewyApp`.

## Build WebGL locally

Use **Dewy > Build WebGL for itch.io** in the Unity Editor. The build is written to:

`Builds/WebGL/`

The build helper configures:

- Web player size: 450 × 900
- Custom template: `PROJECT:Dewy`
- Gzip compression + decompression fallback
- Main scene: `Assets/Scenes/Main.unity`

## Unity Cloud / Build Automation

1. Open Unity Cloud and connect this GitHub repository: `pure-alone/Dewy-Water-Journey-Unity`.
2. Create a Build Automation target for **WebGL**.
3. Use branch `main`.
4. Select Unity Editor **6000.3.15f1** (or the matching 6.3 LTS editor if your Unity Cloud organization aliases patch versions).
5. Start the build and download the WebGL build artifact when it succeeds.

The editor pre-build hook in `Assets/Editor/DewyBuild.cs` reapplies the required 450 × 900 WebGL/template settings during cloud builds.

## Upload to itch.io

1. Extract the Unity Cloud WebGL artifact if needed.
2. Zip the **contents of the WebGL output folder** so that `index.html` is at the root of the ZIP.
3. On itch.io create/edit a project with **Kind of project = HTML**.
4. Upload the ZIP and mark it as playable in the browser.
5. Set the embed viewport to **450 × 900** (or allow fullscreen/mobile scaling if preferred).
6. Publish and test sound, drag interactions, all six completion gates, Credits, and Restart.

## Project structure

- `Assets/Scripts/DewyApp.cs` — page flow, UI composition, all story interactions.
- `Assets/Scripts/DewyUI.cs` — reusable UGUI primitives and procedural rounded/circle/gradient sprites.
- `Assets/Scripts/DewyAudio.cs` — page BGM, SFX and persisted sound preference.
- `Assets/Scripts/DewyDragHandler.cs` — reusable pointer drag callbacks.
- `Assets/WebGLTemplates/Dewy/index.html` — portrait 450 × 900 WebGL shell.
- `Assets/Editor/DewyBuild.cs` — Cloud/local WebGL build settings and build menu command.
- `Assets/Resources/Audio/` — original audio from the HTML prototype.
- `tests/test_project_structure.py` — structural parity/build-readiness checks runnable without Unity.

## Validation without Unity

```bash
python -m unittest discover -s tests -v
```

These tests validate the project structure, source copy, interaction coverage, audio inventory, 450 × 900 configuration, scene registration and WebGL build configuration. Actual C# compilation/WebGL player generation is performed by Unity Editor / Unity Cloud Build.
