using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using BepInEx;


using COM3D2.SimpleUI;
using COM3D2.SimpleUI.Extensions;

namespace COM3D2.SimpleUIDemo
{
    [BepInPlugin("org.bepinex.plugins.com3d2.simpleuidemo", "SimpleUIDemo", "1.0.0.0")]
    public class SimpleUIDemoPlugin: BaseUnityPlugin
    {
        bool BoolValue { get; set; }
        string StringValue { get; set; }
        int IntValue { get; set; }
        float FloatValue { get; set; }


        public void Awake()
        {
            DontDestroyOnLoad(this);
        }

        public void Start()
        {
            //Demo1();
            //Demo2();
            //Demo3();
            //Demo4();
            //Demo5();

            //Demo10();
            //Demo8();

            //var atlas = UIUtils.GetAtlas("AtlasSceneEdit");
            //DemoAtlas(atlas);
            //Demo9();
        }

        public void OnLevelWasLoaded(int level)
        {
        }

        public IFixedLayout Demo1()
        {
            var ui = SimpleUIRoot.Create();
            ui.Box(new Rect(30, 30, 400, 250), "Hello world!");
            return ui;
        }

        public void Demo2()
        {
            var ui = SimpleUIRoot.Create();
            ui.Box(new Rect(0, 0, 100, 50), "Top-left");
            ui.Box(new Rect(ui.width - 100, 0, 100, 50), "Top-right");
            ui.Box(new Rect(0, ui.height - 50, 100, 50), "Bottom-left");
            ui.Box(new Rect(ui.width - 100, ui.height - 50, 100, 50), "Bottom-right");
        }

        public void Demo3()
        {
            var ui = SimpleUIRoot.Create();

            // Make a background box
            ui.Box(new Rect(10, 10, 100, 90), "Loader Menu");

            // Make the first button. If it is pressed, Application.Loadlevel (1) will be executed
            ui.Button(new Rect(20, 40, 80, 20), "Level 1", delegate()
            {
                UnityEngine.Debug.Log("Application.LoadLevel(1)");
            });

            // Make the second button.
            ui.Button(new Rect(20, 70, 80, 20), "Level 2", delegate()
            {
                UnityEngine.Debug.Log("Application.LoadLevel(2)");
            });
        }

        public void Demo4()
        {
            var ui = SimpleUIRoot.Create();
            ui.TextField(new Rect(20, 100, 200, 50), "text field");

            ui.Toggle(new Rect(20, 155, 200, 50), "test toggle", false);
        }

        public void Demo5()
        {
            var ui = SimpleUIRoot.Create();

            // Make a group on the center of the screen
            var group = ui.Group(new Rect(ui.width / 2 - 50, ui.height / 2 - 50, 200, 100));
            // All rectangles are now adjusted to the group. (0,0) is the topleft corner of the group.

            // We'll make a box so you can see where the group is on-screen.
            group.Box(new Rect(0, 0, 200, 100), "Group is here");
            group.Button(new Rect(10, 40, 180, 30), "Click me");
        }        

        public void Demo7()
        {
            var ui = SimpleUIRoot.Create();
            ui.Box(new Rect(25, 175, 1050, 100), "");
            
            var area = ui.Area(new Rect(50, 200, 1000, 50), null);
            
            area.TextField(new Vector2(180, 50), "Test text");
            area.Toolbar(new Vector2(180, 50), 0, new string[] { "1", "2" });
            area.Toggle(new Vector2(180, 50), "Test toggle", true);
            area.Button(new Vector2(180, 50), "Button 3");
            area.HorizontalSlider(new Vector2(180, 50), 0, 0, 100);
        }

        public void Demo7point5()
        {
            // extensions test
            var ui = SimpleUIRoot.Create();

            var group = ui.Group(new Rect(25, 175, 1050, 100));
            group.Draggable = true;

            var box = group.Box(new Rect(0, 0, 1050, 100), "");

            var area = group.Area(new Rect(4, 4, 1000, 50), null);
            area.OnLayout(() =>
            {
                box.size = new Vector2(area.contentWidth + 50, area.contentHeight + 50);
            });

            var dropdown = area.Dropdown(new Vector2(50, 50), "Hist", "test 1", new List<string> { "test 1", "test2", "this is a long long long line" });

            var textfield = area.TextField(new Vector2(180, 50), "Search...");

            dropdown.OnChange(v =>
            {
                textfield.Value = v;
            });

            var toolbar = area.Toolbar(new Vector2(180, 50), 0, new string[] { "1", "2" });

            var toggle = area.Toggle(new Vector2(180, 50), "Test toggle", true);
            toggle.Bind(this, o => o.BoolValue)
                .OnChange(v =>
                {
                    Logger.LogInfo($"Bind demo: \n\t change value is {v} \n\t instance value is {this.BoolValue}");
                })
                .VisibleWhen(toolbar, 1);

            area.Button(new Vector2(180, 50), "Button 3")
                .VisibleWhen(toggle);

            area.HorizontalSlider(new Vector2(180, 50), 0, 0, 100)
                .Bind(this, o=> o.FloatValue);

            area.Button(new Vector2(100, 50), "Submit", this.testSubmit);
        }

        void testSubmit()
        {
            Logger.LogInfo($"UI submitted with bound values {this.BoolValue}, {this.FloatValue}");
        }



        public void Demo8()
        {
            var ui = SimpleUIRoot.Create();
            ui.Box(new Rect(50, 200, 500, 100), "");

            ui.Toolbar(new Rect(50, 200, 500, 50), 0, new string[] { "Button 1", "Button 2", "Button 3", "Button 4" },
            index =>
            {
                Logger.LogInfo(String.Format("Toolbar selected {0}", index));
            });
        }

        public void Demo9()
        {
            var ui = SimpleUIRoot.Create();
            ui.HorizontalSlider(new Rect(50, 200, 500, 50), 0, 0, 100);
        }


        public void Demo10()
        {
            // extensions test
            var ui = SimpleUIRoot.Create();
            var panelHeight = 40;

            var group = ui.Group(new Rect(25, 175, 1050, panelHeight + 16));
            group.Draggable = true;

            var box = group.Box(new Rect(0, 0, 1050, panelHeight+16), "");

            var area = group.Area(new Rect(8, 8, 1000, panelHeight), null);
            area.OnLayout(() =>
            {
                box.size = new Vector2(area.contentWidth + 16, area.contentHeight + 16);
            });

            area.Dropdown(new Vector2(50, panelHeight), "Hist", "test 1", new List<string> { "test 1", "test2", "this is a long long long line" });

            area.TextField(new Vector2(200, panelHeight), "");
            area.Button(new Vector2(70, panelHeight), "Search");

            area.Button(new Vector2(50, panelHeight), "Aa");

            area.Dropdown(new Vector2(50, panelHeight),
                "And",
                "And",
                new List<string> {
                    "And",
                    "Or",
                });

            area.Dropdown(new Vector2(50, panelHeight),
                "T/D",
                "[T/D] Title+Desc",
                new List<string> {
                    "[T/D] Title+Desc",
                    "[T]itle",
                    "[D]esc",
                });

            area.Dropdown(new Vector2(50, panelHeight),
                "A",
                "[A]ll items",
                new List<string> {
                    "[A]ll items",
                    "[COM]",
                    "[CM]",
                    "[Mods]",
                });

            area.Dropdown(new Vector2(50, panelHeight),
                "F/D",
                "[N]one",
                new List<string> {
                    "[N]one",
                    "[F]ilenames",
                    "[F/D] Filenames+Directory",
                });

            area.Toggle(new Vector2(180, panelHeight), "Translations", true);
        }

    }
}
