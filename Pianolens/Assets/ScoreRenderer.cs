using UnityEngine;
using System.Collections;
using System.Drawing;
using AlphaTab.Audio.Generator;
using AlphaTab.Audio.Model;
using AlphaTab.Importer;
using AlphaTab.IO;
using AlphaTab.Model;
using UnityEngine.UI;
using System.Collections.Generic;
using System.IO;

public class ScoreRenderer : MonoBehaviour {
   
    // Use this for initialization
    void Start () {
        Score score = ScoreLoader.LoadScore(Application.dataPath+"/Midi/song");
        var settings = AlphaTab.Settings.Defaults;
        settings.Engine = "gdi";
        var renderer = new AlphaTab.Rendering.ScoreRenderer(settings, null);

        // iterate tracks
        for (int i = 0, j = score.Tracks.Count; i < j; i++)
        {
            var track = score.Tracks[i];

            // render track
            //Console.WriteLine("Rendering track {0} - {1}", i + 1, track.Name);
            var images = new List<System.Drawing.Image>();
            var totalWidth = 0;
            var totalHeight = 0;
            renderer.PartialRenderFinished += r =>
            {
                images.Add((System.Drawing.Image)r.RenderResult);
            };
            renderer.RenderFinished += r =>
            {
                totalWidth = (int)r.TotalWidth;
                totalHeight = (int)r.TotalHeight;
            };
            renderer.Render(track);

            // write png
            var info = new FileInfo(Application.dataPath + "/Midi/song");
            var path = Path.Combine(info.DirectoryName, Path.GetFileNameWithoutExtension(info.Name) + "-" + i + ".png");

            using (var bmp = new Bitmap(totalWidth, totalHeight))
            {
                int y = 0;
                using (var g = System.Drawing.Graphics.FromImage(bmp))
                {
                    foreach (var image in images)
                    {
                        g.DrawImage(image, new Rectangle(0, y, image.Width, image.Height),
                            new Rectangle(0, 0, image.Width, image.Height), GraphicsUnit.Pixel);
                        y += image.Height;
                    }
                }
                bmp.Save(path, System.Drawing.Imaging.ImageFormat.Png);
            }
        }

        //Canvas theCanvas = this.gameObject.AddComponent<Canvas>();
        //GraphicRaycaster theGraphicRaycaster = this.gameObject.AddComponent<GraphicRaycaster>();
        //RectTransform theCanvasRT = this.gameObject.GetComponent<RectTransform>();
        //theCanvas.renderMode = RenderMode.WorldSpace;
        //theCanvasRT. = 800;
        //theCanvas.pixel

    }

    // Update is called once per frame
    void Update () {
	
	}
}
