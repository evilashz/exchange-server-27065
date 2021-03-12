using System;
using Microsoft.Exchange.Cluster.Common;

namespace Microsoft.Exchange.Cluster.Replay.MountPoint
{
	// Token: 0x0200023B RID: 571
	internal class MountedFolderPath : IEquatable<MountedFolderPath>, IComparable<MountedFolderPath>
	{
		// Token: 0x060015B1 RID: 5553 RVA: 0x000564BF File Offset: 0x000546BF
		public static bool IsNullOrEmpty(MountedFolderPath mountedFolderPath)
		{
			return mountedFolderPath == null || string.IsNullOrEmpty(mountedFolderPath.Path);
		}

		// Token: 0x060015B2 RID: 5554 RVA: 0x000564D4 File Offset: 0x000546D4
		public static bool IsEqual(MountedFolderPath src, MountedFolderPath dst)
		{
			return object.ReferenceEquals(src, dst) || (src != null && dst != null && StringUtil.IsEqualIgnoreCase(src.Path, dst.Path));
		}

		// Token: 0x060015B3 RID: 5555 RVA: 0x000564FA File Offset: 0x000546FA
		public MountedFolderPath(string mountedFolderPath) : this()
		{
			if (!string.IsNullOrEmpty(mountedFolderPath))
			{
				this.m_mountedFolderPathRaw = mountedFolderPath;
				this.m_mountedFolderPath = MountPointUtil.EnsurePathHasTrailingBackSlash(mountedFolderPath);
			}
		}

		// Token: 0x060015B4 RID: 5556 RVA: 0x0005651D File Offset: 0x0005471D
		private MountedFolderPath()
		{
			this.m_mountedFolderPathRaw = string.Empty;
			this.m_mountedFolderPath = string.Empty;
		}

		// Token: 0x1700061F RID: 1567
		// (get) Token: 0x060015B5 RID: 5557 RVA: 0x0005653B File Offset: 0x0005473B
		public string Path
		{
			get
			{
				return this.m_mountedFolderPath;
			}
		}

		// Token: 0x17000620 RID: 1568
		// (get) Token: 0x060015B6 RID: 5558 RVA: 0x00056543 File Offset: 0x00054743
		public string RawString
		{
			get
			{
				return this.m_mountedFolderPathRaw;
			}
		}

		// Token: 0x060015B7 RID: 5559 RVA: 0x0005654B File Offset: 0x0005474B
		public int CompareTo(MountedFolderPath other)
		{
			if (other == null)
			{
				return 1;
			}
			return StringUtil.CompareIgnoreCase(this.Path, other.Path);
		}

		// Token: 0x060015B8 RID: 5560 RVA: 0x00056563 File Offset: 0x00054763
		public bool Equals(MountedFolderPath other)
		{
			return MountedFolderPath.IsEqual(this, other);
		}

		// Token: 0x060015B9 RID: 5561 RVA: 0x0005656C File Offset: 0x0005476C
		public override bool Equals(object obj)
		{
			MountedFolderPath mountedFolderPath = obj as MountedFolderPath;
			return mountedFolderPath != null && MountedFolderPath.IsEqual(this, mountedFolderPath);
		}

		// Token: 0x060015BA RID: 5562 RVA: 0x0005658C File Offset: 0x0005478C
		public override string ToString()
		{
			return this.Path;
		}

		// Token: 0x060015BB RID: 5563 RVA: 0x00056594 File Offset: 0x00054794
		public override int GetHashCode()
		{
			return StringUtil.GetStringIHashCode(this.Path);
		}

		// Token: 0x04000893 RID: 2195
		public static readonly MountedFolderPath Empty = new MountedFolderPath();

		// Token: 0x04000894 RID: 2196
		private readonly string m_mountedFolderPathRaw;

		// Token: 0x04000895 RID: 2197
		private readonly string m_mountedFolderPath;
	}
}
