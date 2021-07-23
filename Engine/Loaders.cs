using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using CrownEngine.Content;
using CrownEngine.Engine;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Design;
using System.IO;

namespace CrownEngine
{
    public static class Loaders
    {
        public static List<T> LoadListContent<T>(this ContentManager contentManager, string contentFolder)
        {
            DirectoryInfo dir = new DirectoryInfo(contentManager.RootDirectory + "/" + contentFolder);
            if (!dir.Exists)
                throw new DirectoryNotFoundException();
            List<T> result = new List<T>();

            FileInfo[] files = dir.GetFiles("*.*");
            foreach (FileInfo file in files)
            {
                result.Add(contentManager.Load<T>(contentFolder + "/" + file.Name.Split('.')[0]));
            }
            return result;
        }
    }
}