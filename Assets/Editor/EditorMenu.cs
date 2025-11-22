// // ©2015 - 2025 Candy Smith
// // All rights reserved
// // Redistribution of this software is strictly not allowed.
// // Copy of this software can be obtained from unity asset store only.
// // THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// // IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// // FITNESS FOR A PARTICULAR PURPOSE AND NON-INFRINGEMENT. IN NO EVENT SHALL THE
// // AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// // LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// // OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// // THE SOFTWARE.

using UnityEditor;
using UnityEngine;
using UnityEditor.SceneManagement;

namespace TATA.Scripts.Editor
{
    public static class EditorMenu
    {
        [MenuItem("Scene/Scene 1 &1")]
        private static void SwitchToScene1() => SwitchScene(0);

        [MenuItem("Scene/Scene 2 &2")]
        private static void SwitchToScene2() => SwitchScene(1);

        [MenuItem("Scene/Scene 3 &3")]
        private static void SwitchToScene3() => SwitchScene(2);

        [MenuItem("Scene/Scene 4 &4")]
        private static void SwitchToScene4() => SwitchScene(3);

        private static void SwitchScene(int sceneIndex)
        {
            var scenes = EditorBuildSettings.scenes;
            if (sceneIndex >= 0 && sceneIndex < scenes.Length && scenes[sceneIndex].enabled)
            {
                if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
                {
                    EditorSceneManager.OpenScene(scenes[sceneIndex].path);
                }
            }
            else
            {
                Debug.LogWarning($"Scene index {sceneIndex} is invalid");
            }
        }
    }
}