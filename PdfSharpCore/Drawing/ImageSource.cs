﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MigraDocCore.DocumentObjectModel.MigraDoc.DocumentObjectModel.Shapes
{

    public abstract class ImageSource
    {
        public static ImageSource ImageSourceImpl { get; set; }

        public interface IImageSource : IDisposable
        {
            int Width { get; }
            int Height { get; }
            string Name { get; }
            void SaveAsJpeg(MemoryStream ms);
        }

        protected abstract IImageSource FromFileImpl(string path);
        protected abstract IImageSource FromBinaryImpl(string name, Func<byte[]> imageSource);
        protected abstract IImageSource FromStreamImpl(string name, Func<Stream> imageStream);


        public static IImageSource FromFile(string path)
        {
            return ImageSourceImpl.FromFileImpl(path);
        }

        public static IImageSource FromBinary(string name, Func<byte[]> imageSource)
        {
            return ImageSourceImpl.FromBinaryImpl(name, imageSource);
        }

        public static IImageSource FromStream(string name, Func<Stream> imageStream)
        {
            return ImageSourceImpl.FromStreamImpl(name, imageStream);
        }
    }
}