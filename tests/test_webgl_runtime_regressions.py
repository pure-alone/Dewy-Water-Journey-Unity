import unittest
from pathlib import Path

ROOT = Path(__file__).resolve().parents[1]


def text(path):
    return (ROOT / path).read_text(encoding='utf-8')


class WebGLRuntimeRegressionTests(unittest.TestCase):
    def test_standalone_input_module_has_legacy_input_enabled(self):
        app = text('Assets/Scripts/DewyApp.cs')
        settings = text('ProjectSettings/ProjectSettings.asset')
        self.assertIn('typeof(StandaloneInputModule)', app)
        self.assertIn('activeInputHandler: 0', settings)

    def test_audio_has_listener_and_user_gesture_resume(self):
        app = text('Assets/Scripts/DewyApp.cs')
        audio = text('Assets/Scripts/DewyAudio.cs')
        self.assertIn('AudioListener', audio)
        self.assertIn('NotifyUserGesture', audio)
        self.assertIn('Input.GetMouseButtonDown(0)', app)
        self.assertIn('Audio.NotifyUserGesture()', app)

    def test_home_and_scene1_layout_matches_html_source(self):
        app = text('Assets/Scripts/DewyApp.cs')
        self.assertIn('ApplyHtmlLayoutParity(page)', app)
        self.assertIn('DewyUI.Place(sun, 281f, 88f, 92f, 92f)', app)
        self.assertIn('ray.anchoredPosition = new Vector2(324f, -75f)', app)
        self.assertIn('DewyUI.Place(sun, 163f, 300f, 92f, 92f)', app)
        self.assertIn('DewyUI.Place(dewy, 173f, 418f, 72f, 72f)', app)
        self.assertIn('new Vector2(173f, -250f)', app)


if __name__ == '__main__':
    unittest.main()
