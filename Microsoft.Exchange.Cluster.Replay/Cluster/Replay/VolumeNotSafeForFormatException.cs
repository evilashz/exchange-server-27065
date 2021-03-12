using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000501 RID: 1281
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class VolumeNotSafeForFormatException : DatabaseVolumeInfoException
	{
		// Token: 0x06002F0E RID: 12046 RVA: 0x000C4C67 File Offset: 0x000C2E67
		public VolumeNotSafeForFormatException(string volumeName, string mountPoint) : base(ReplayStrings.VolumeNotSafeForFormatException(volumeName, mountPoint))
		{
			this.volumeName = volumeName;
			this.mountPoint = mountPoint;
		}

		// Token: 0x06002F0F RID: 12047 RVA: 0x000C4C89 File Offset: 0x000C2E89
		public VolumeNotSafeForFormatException(string volumeName, string mountPoint, Exception innerException) : base(ReplayStrings.VolumeNotSafeForFormatException(volumeName, mountPoint), innerException)
		{
			this.volumeName = volumeName;
			this.mountPoint = mountPoint;
		}

		// Token: 0x06002F10 RID: 12048 RVA: 0x000C4CAC File Offset: 0x000C2EAC
		protected VolumeNotSafeForFormatException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.volumeName = (string)info.GetValue("volumeName", typeof(string));
			this.mountPoint = (string)info.GetValue("mountPoint", typeof(string));
		}

		// Token: 0x06002F11 RID: 12049 RVA: 0x000C4D01 File Offset: 0x000C2F01
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("volumeName", this.volumeName);
			info.AddValue("mountPoint", this.mountPoint);
		}

		// Token: 0x17000C14 RID: 3092
		// (get) Token: 0x06002F12 RID: 12050 RVA: 0x000C4D2D File Offset: 0x000C2F2D
		public string VolumeName
		{
			get
			{
				return this.volumeName;
			}
		}

		// Token: 0x17000C15 RID: 3093
		// (get) Token: 0x06002F13 RID: 12051 RVA: 0x000C4D35 File Offset: 0x000C2F35
		public string MountPoint
		{
			get
			{
				return this.mountPoint;
			}
		}

		// Token: 0x040015B1 RID: 5553
		private readonly string volumeName;

		// Token: 0x040015B2 RID: 5554
		private readonly string mountPoint;
	}
}
