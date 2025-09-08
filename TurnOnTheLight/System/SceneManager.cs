using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurnOnTheLight.System
{
    class SceneManager
    {
        public SceneManager()
        {
            _scenes = new Stack<IScene>();
        }
        public IScene CurrentScene{
            get
            {
                if (_scenes.Count == 0)
                {
                    return null;
                }
                return _scenes.Peek();
            }
        }
        public void AddScene(IScene scene)
        {
            _scenes.Push(scene);
        }

        public void RemoveScene()
        {
            _scenes.Pop();
        }
        public void ClearAllScenes()
        {
            _scenes.Clear();
        }
        private Stack<IScene> _scenes;
    }
}
