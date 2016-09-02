using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Piksel.Kai
{
    public class Snapshots
    {
        public Project Project { get; }

        public Snapshots(Project project)
        {
            this.Project = project;
        }

        public SnapshotFile this[int index]
        {
            get
            {
                return new SnapshotFile(this, index);
            }
        }
            
    }

    public class SnapshotFile
    {
        private int index;
        private Snapshots snapshots;

        public string FilePath {
            get
            {
                return snapshots.Project.FileList[index];
            }
        }

        string _identifier;
        public string Identifier
        {
            get
            {
                if (string.IsNullOrEmpty(_identifier))
                    _identifier = index.ToString("x8");
                return _identifier;
            }
        }

        string _snapshotPath;
        public string SnapshotRootPath
        {
            get
            {
                if (string.IsNullOrEmpty(_snapshotPath))
                    _snapshotPath = Path.Combine(snapshots.Project.SnapshotsPath, Identifier[7].ToString(), Identifier);
                return _snapshotPath;
            }
        }

        public string SnapshotPath(uint hash)
        {
            return Path.Combine(SnapshotRootPath, hash.ToString("x8"));
        }

        public bool Exists(uint hash) {
            return File.Exists(SnapshotPath(hash));
        }

        public SnapshotFile(Snapshots snapshots, int index)
        {
            if (snapshots.Project.FileList.Count <= index)
                throw new IndexOutOfRangeException("Invalid file index!");

            this.snapshots = snapshots;
            this.index = index;
        }
    }
}
