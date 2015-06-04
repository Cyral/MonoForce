using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework.Content;
using MonoForce.External.Zip;

namespace MonoForce.Controls
{
    /// </remarks>
    /// This class is based on Nick Gravelyn's EasyZip library.
    /// <remarks>
    /// <summary>
    /// Additionally it is capable of loading assets from a zip file.
    /// content pipeline the same way like <see cref="Microsoft.Xna.Framework.Content.ContentManager" /> does.
    /// Loads managed objects from the binary files produced by the design time
    /// </summary>
    public class ArchiveManager : ContentManager
    {
        /// <summary>
        /// Gets the path to the archive file associated with the manager.
        /// </summary>
        public virtual string ArchivePath
        {
            get { return archivePath; }
        }

        /// <summary>
        /// Indicates if an archive file was specified to read from or not. ???
        /// </summary>
        public bool UseArchive
        {
            get { return useArchive; }
            set { useArchive = value; }
        }

        /// <summary>
        /// Archive file associated with the archive manager.
        /// </summary>
        private readonly ZipFile archive;

        /// <summary>
        /// Path to the archive file associated with the archive manager.
        /// </summary>
        private readonly string archivePath;

        /// <summary>
        /// Indicates if an archive file was specified to read from or not. ???
        /// </summary>
        private bool useArchive;

        public ArchiveManager(IServiceProvider serviceProvider) : this(serviceProvider, null)
        {
        }

        public ArchiveManager(IServiceProvider serviceProvider, string archive) : base(serviceProvider)
        {
            if (archive != null)
            {
                this.archive = ZipFile.Read(archive);
                archivePath = archive;
                useArchive = true;
            }
        }

        /// (Full Path + Asset Name)
        /// </returns>
        /// <returns>
        /// Returns an array of asset names contained in the archive.
        /// <summary>
        /// Gets the list of all assets contained inside of the archive.
        /// </summary>
        public string[] GetAssetNames()
        {
            if (useArchive && archive != null)
            {
                var filenames = new List<string>();

                foreach (var entry in archive)
                {
                    var name = entry.FileName;
                    if (name.EndsWith(".xnb"))
                    {
                        name = name.Remove(name.Length - 4, 4);
                        filenames.Add(name);
                    }
                }
                return filenames.ToArray();
            }
            return null;
        }

        /// (Full Path + Asset Name)
        /// </returns>
        /// <returns>
        /// Returns an array of asset names contained in the specified directory.
        /// <param name="path">Directory in the archive to retrieve asset names from.</param>
        /// <summary>
        /// Gets the list of all assets contained inside the archive in the specified directory.
        /// </summary>
        public string[] GetAssetNames(string path)
        {
            if (useArchive && archive != null)
            {
                if (path != null && path != "" && path != "\\" && path != "/")
                {
                    var filenames = new List<string>();

                    foreach (var entry in archive)
                    {
                        var name = entry.FileName;
                        if (name.EndsWith(".xnb"))
                        {
                            name = name.Remove(name.Length - 4, 4);
                        }

                        var parts = name.Split('/');
                        var dir = "";
                        for (var i = 0; i < parts.Length - 1; i++)
                        {
                            dir += parts[i] + '/';
                        }

                        path = path.Replace("\\", "/");
                        if (path.StartsWith("/")) path = path.Remove(0, 1);
                        if (!path.EndsWith("/")) path += '/';

                        if (dir.ToLower() == path.ToLower() && !name.EndsWith("/"))
                        {
                            filenames.Add(name);
                        }
                    }
                    return filenames.ToArray();
                }
                return GetAssetNames();
            }
            return null;
        }

        /// <returns>Returns an array of all directories under the specified path.</returns>
        /// <param name="path">Directory to start searching from in the archive.</param>
        /// <summary>
        /// Gets the list of all directories contained in the archive.
        /// </summary>
        public string[] GetDirectories(string path)
        {
            if (useArchive && archive != null)
            {
                if (path != null && path != "" && path != "\\" && path != "/")
                {
                    var dirs = new List<string>();

                    path = path.Replace("\\", "/");
                    if (path.StartsWith("/")) path = path.Remove(0, 1);
                    if (!path.EndsWith("/")) path += '/';

                    foreach (var entry in archive)
                    {
                        var name = entry.FileName;
                        if (name.ToLower().StartsWith(path.ToLower()))
                        {
                            var i = name.IndexOf("/", path.Length);
                            var item = name.Substring(path.Length, i - path.Length) + "\\";
                            if (!dirs.Contains(item))
                            {
                                dirs.Add(item);
                            }
                        }
                    }
                    return dirs.ToArray();
                }
                return GetAssetNames();
            }
            if (Directory.Exists(path))
            {
                var dirs = Directory.GetDirectories(path);

                for (var i = 0; i < dirs.Length; i++)
                {
                    var parts = dirs[i].Split('\\');
                    dirs[i] = parts[parts.Length - 1] + '\\';
                }

                return dirs;
            }
            return null;
        }

        /// <returns>Returns the opened stream.</returns>
        /// <param name="filename">Name of the file to read.</param>
        /// <summary>
        /// Opens a stream for reading the specified file from the archive.
        /// </summary>
        public Stream GetFileStream(string filename)
        {
            if (useArchive && archive != null)
            {
                filename = filename.Replace("\\", "/").ToLower();
                if (filename.StartsWith("/")) filename = filename.Remove(0, 1);

                foreach (var entry in archive)
                {
                    var entryName = entry.FileName.ToLower();

                    if (entryName.Equals(filename))
                        return entry.GetStream();
                }

                throw new Exception("Cannot find file \"" + filename + "\" in the archive.");
            }
            return null;
        }

        /// <returns>Returns the opened stream.</returns>
        /// <param name="assetName">Name of the asset to read.</param>
        /// <summary>
        /// Opens a stream for reading the specified asset contained inside the archive.
        /// </summary>
        protected override Stream OpenStream(string assetName)
        {
            if (useArchive && archive != null)
            {
                assetName = assetName.Replace("\\", "/");
                if (assetName.StartsWith("/")) assetName = assetName.Remove(0, 1);

                var fullAssetName = (assetName + ".xnb").ToLower();

                foreach (var entry in archive)
                {
                    var ze = new ZipDirEntry(entry);

                    var entryName = entry.FileName.ToLower();

//Load
                    if (entryName == fullAssetName)
                    {
                        return entry.GetStream();
                    }
                }
                throw new Exception("Cannot find asset \"" + assetName + "\" in the archive.");
            }
            return base.OpenStream(assetName);
        }
    }
}
