import unittest
from pathlib import Path

ROOT = Path(__file__).resolve().parents[1]

def text(path):
    return (ROOT / path).read_text(encoding='utf-8')

class VisualPolishRuntimeTests(unittest.TestCase):
    def test_polish_installs_automatically(self):
        code = text('Assets/Scripts/DewyVisualPolish.cs')
        self.assertIn('RuntimeInitializeOnLoadMethod', code)
        self.assertIn('ApplyPagePolish()', code)

    def test_home_sun_rays_follow_sun_center(self):
        code = text('Assets/Scripts/DewyVisualPolish.cs')
        self.assertIn('FixHomeSunRays()', code)
        self.assertIn('centerX = sun.anchoredPosition.x', code)
        self.assertIn('ray.anchoredPosition = new Vector2(rayX, -rayY)', code)

    def test_scene1_has_graphic_up_arrow(self):
        code = text('Assets/Scripts/DewyVisualPolish.cs')
        self.assertIn('AddScene1Arrow()', code)
        self.assertIn('SunArrowGraphic', code)
        self.assertIn('HeadLeft', code)
        self.assertIn('HeadRight', code)

    def test_scenes_4_5_6_get_dotted_routes(self):
        code = text('Assets/Scripts/DewyVisualPolish.cs')
        self.assertIn('"GroundwaterPath"', code)
        self.assertIn('"StreamPath"', code)
        self.assertIn('"RiverPath"', code)
        self.assertIn('PathDot_', code)

if __name__ == '__main__':
    unittest.main()
