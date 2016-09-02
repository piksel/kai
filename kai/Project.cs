using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Piksel.Kai
{
    public class Project
    {
        public string RootPath { get; set; }
        public string FileListPath        { get { return Path.Combine(ProjectPath, "filelist"); } }
        public string ProjectSettingsPath { get { return Path.Combine(ProjectPath, "settings.yaml"); } }
        public string MomentPath          { get { return Path.Combine(ProjectPath, "moments"); } }
        public string SnapshotsPath       { get { return Path.Combine(ProjectPath, "snapshots"); } }

        internal Moment GetMoment(int index)
        {
            return Moment.Load(this, index);
        }

        public Snapshots Snapshots { get; set; }

        internal Task<Moment> CreateMoment()
        {
            return Task.Run<Moment>(() =>
            {
                var moment = new Moment(this, ++ProjectSettings.MomentIndex);
                foreach (var file in FileList)
                {
                    moment.Hashes.Add(Hasher.HashFile(file));
                }
                return moment;
            });
        }

        internal Task<int> CreateSnapshots(Moment moment)
        {
            return Task.Run<int>(() =>
            {
                int newSnapshots = 0;
                for(int i=0; i<moment.Hashes.Count; i++)
                {
                    var fileSnapshots = Snapshots[i];
                    if(!fileSnapshots.Exists(moment.Hashes[i]))
                    {
                        CreateFileSnapshot(fileSnapshots, moment, i);
                        newSnapshots++;
                    }
                }
                return newSnapshots;
            });
        }

        private void CreateFileSnapshot(SnapshotFile fileSnapshots, Moment moment, int index)
        {
            
            var snapshotFile = fileSnapshots.SnapshotPath(moment.Hashes[index]);
            Directory.CreateDirectory(Path.GetDirectoryName(snapshotFile));

            File.Copy(FileList[index], snapshotFile);

        }

        List<string> _fileList;
        public List<string> FileList {
            get
            {
                if(_fileList == null)
                {
                    var flp = FileListPath;
                    if (File.Exists(flp))
                    {
                        _fileList = File.ReadAllLines(flp).ToList();
                    }
                    else
                    {
                        _fileList = new List<string>();
                    }
                }
                return _fileList;
            }
        }

        public void Save()
        {
            File.WriteAllLines(FileListPath, FileList.ToArray());
            ProjectSettings.Save(ProjectSettingsPath);
        }

        public ProjectSettings ProjectSettings { get; set; }

        string _projectPath;
        public string ProjectPath {  get
            {
                if(string.IsNullOrEmpty(_projectPath))
                    _projectPath =  Path.Combine(Path.GetFullPath(RootPath), ".kai");
                return _projectPath;
            }
        }



        public static Project Create(string path)
        {

            var p = new Project();
            p.RootPath = path;

            if (Directory.Exists(p.ProjectPath)) throw new Exception("Kai project already initialized!");

            Directory.CreateDirectory(p.ProjectPath);
            Directory.CreateDirectory(p.MomentPath);
            p.ProjectSettings = ProjectSettings.Default;

            return p;
        }

        internal Task<int> Scan()
        {
            return Task.Run<int>(() =>
            {
                int newFiles = 0;
                foreach(var file in Directory.EnumerateFiles(RootPath, "*", SearchOption.AllDirectories))
                {
                    var relativeFile = PathEx.GetRelativePath(RootPath, file);
                    if (relativeFile.StartsWith(".kai")) continue;
                    if (!FileList.Contains(relativeFile))
                    {
                        FileList.Add(relativeFile);
                        newFiles++;
                    }
                }

                return newFiles;
            });
              
        }

        public static Project Load(string path)
        {
            var p = new Project();
            p.RootPath = path;
            p.ProjectSettings = ProjectSettings.Load(p.ProjectSettingsPath);
            p.Snapshots = new Snapshots(p);

            return p;
        }
    }
}
