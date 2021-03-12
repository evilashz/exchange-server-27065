using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000503 RID: 1283
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class VolumeCouldNotBeReclaimedException : DatabaseVolumeInfoException
	{
		// Token: 0x06002F1B RID: 12059 RVA: 0x000C4E5E File Offset: 0x000C305E
		public VolumeCouldNotBeReclaimedException(string volumeName, string mountPoint) : base(ReplayStrings.VolumeCouldNotBeReclaimedException(volumeName, mountPoint))
		{
			this.volumeName = volumeName;
			this.mountPoint = mountPoint;
		}

		// Token: 0x06002F1C RID: 12060 RVA: 0x000C4E80 File Offset: 0x000C3080
		public VolumeCouldNotBeReclaimedException(string volumeName, string mountPoint, Exception innerException) : base(ReplayStrings.VolumeCouldNotBeReclaimedException(volumeName, mountPoint), innerException)
		{
			this.volumeName = volumeName;
			this.mountPoint = mountPoint;
		}

		// Token: 0x06002F1D RID: 12061 RVA: 0x000C4EA4 File Offset: 0x000C30A4
		protected VolumeCouldNotBeReclaimedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.volumeName = (string)info.GetValue("volumeName", typeof(string));
			this.mountPoint = (string)info.GetValue("mountPoint", typeof(string));
		}

		// Token: 0x06002F1E RID: 12062 RVA: 0x000C4EF9 File Offset: 0x000C30F9
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("volumeName", this.volumeName);
			info.AddValue("mountPoint", this.mountPoint);
		}

		// Token: 0x17000C19 RID: 3097
		// (get) Token: 0x06002F1F RID: 12063 RVA: 0x000C4F25 File Offset: 0x000C3125
		public string VolumeName
		{
			get
			{
				return this.volumeName;
			}
		}

		// Token: 0x17000C1A RID: 3098
		// (get) Token: 0x06002F20 RID: 12064 RVA: 0x000C4F2D File Offset: 0x000C312D
		public string MountPoint
		{
			get
			{
				return this.mountPoint;
			}
		}

		// Token: 0x040015B6 RID: 5558
		private readonly string volumeName;

		// Token: 0x040015B7 RID: 5559
		private readonly string mountPoint;
	}
}
