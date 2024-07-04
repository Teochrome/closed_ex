using System;
using Avalonia.Media.Imaging;
using Avalonia.Platform;

namespace DemoExamTraining2.Helpers;

public static class ImageHelper
{
    public static Bitmap LoadFromResource(Uri resourceUri)
    {
        return new Bitmap(AssetLoader.Open(resourceUri));
    }
}