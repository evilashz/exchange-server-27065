using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x0200050C RID: 1292
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DbVolumeInvalidDirectoriesException : DatabaseVolumeInfoException
	{
		// Token: 0x06002F4D RID: 12109 RVA: 0x000C549F File Offset: 0x000C369F
		public DbVolumeInvalidDirectoriesException(string volumeName, string mountedFolder, int numExpected, int numActual) : base(ReplayStrings.DbVolumeInvalidDirectoriesException(volumeName, mountedFolder, numExpected, numActual))
		{
			this.volumeName = volumeName;
			this.mountedFolder = mountedFolder;
			this.numExpected = numExpected;
			this.numActual = numActual;
		}

		// Token: 0x06002F4E RID: 12110 RVA: 0x000C54D3 File Offset: 0x000C36D3
		public DbVolumeInvalidDirectoriesException(string volumeName, string mountedFolder, int numExpected, int numActual, Exception innerException) : base(ReplayStrings.DbVolumeInvalidDirectoriesException(volumeName, mountedFolder, numExpected, numActual), innerException)
		{
			this.volumeName = volumeName;
			this.mountedFolder = mountedFolder;
			this.numExpected = numExpected;
			this.numActual = numActual;
		}

		// Token: 0x06002F4F RID: 12111 RVA: 0x000C550C File Offset: 0x000C370C
		protected DbVolumeInvalidDirectoriesException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.volumeName = (string)info.GetValue("volumeName", typeof(string));
			this.mountedFolder = (string)info.GetValue("mountedFolder", typeof(string));
			this.numExpected = (int)info.GetValue("numExpected", typeof(int));
			this.numActual = (int)info.GetValue("numActual", typeof(int));
		}

		// Token: 0x06002F50 RID: 12112 RVA: 0x000C55A4 File Offset: 0x000C37A4
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("volumeName", this.volumeName);
			info.AddValue("mountedFolder", this.mountedFolder);
			info.AddValue("numExpected", this.numExpected);
			info.AddValue("numActual", this.numActual);
		}

		// Token: 0x17000C27 RID: 3111
		// (get) Token: 0x06002F51 RID: 12113 RVA: 0x000C55FD File Offset: 0x000C37FD
		public string VolumeName
		{
			get
			{
				return this.volumeName;
			}
		}

		// Token: 0x17000C28 RID: 3112
		// (get) Token: 0x06002F52 RID: 12114 RVA: 0x000C5605 File Offset: 0x000C3805
		public string MountedFolder
		{
			get
			{
				return this.mountedFolder;
			}
		}

		// Token: 0x17000C29 RID: 3113
		// (get) Token: 0x06002F53 RID: 12115 RVA: 0x000C560D File Offset: 0x000C380D
		public int NumExpected
		{
			get
			{
				return this.numExpected;
			}
		}

		// Token: 0x17000C2A RID: 3114
		// (get) Token: 0x06002F54 RID: 12116 RVA: 0x000C5615 File Offset: 0x000C3815
		public int NumActual
		{
			get
			{
				return this.numActual;
			}
		}

		// Token: 0x040015C4 RID: 5572
		private readonly string volumeName;

		// Token: 0x040015C5 RID: 5573
		private readonly string mountedFolder;

		// Token: 0x040015C6 RID: 5574
		private readonly int numExpected;

		// Token: 0x040015C7 RID: 5575
		private readonly int numActual;
	}
}
