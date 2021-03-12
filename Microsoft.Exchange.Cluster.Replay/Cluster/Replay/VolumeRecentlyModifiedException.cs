using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000500 RID: 1280
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class VolumeRecentlyModifiedException : DatabaseVolumeInfoException
	{
		// Token: 0x06002F05 RID: 12037 RVA: 0x000C4A9A File Offset: 0x000C2C9A
		public VolumeRecentlyModifiedException(string volumeName, TimeSpan threshold, string dateTimeUtc, string mountPoint, string lastUpdatePath) : base(ReplayStrings.VolumeRecentlyModifiedException(volumeName, threshold, dateTimeUtc, mountPoint, lastUpdatePath))
		{
			this.volumeName = volumeName;
			this.threshold = threshold;
			this.dateTimeUtc = dateTimeUtc;
			this.mountPoint = mountPoint;
			this.lastUpdatePath = lastUpdatePath;
		}

		// Token: 0x06002F06 RID: 12038 RVA: 0x000C4AD8 File Offset: 0x000C2CD8
		public VolumeRecentlyModifiedException(string volumeName, TimeSpan threshold, string dateTimeUtc, string mountPoint, string lastUpdatePath, Exception innerException) : base(ReplayStrings.VolumeRecentlyModifiedException(volumeName, threshold, dateTimeUtc, mountPoint, lastUpdatePath), innerException)
		{
			this.volumeName = volumeName;
			this.threshold = threshold;
			this.dateTimeUtc = dateTimeUtc;
			this.mountPoint = mountPoint;
			this.lastUpdatePath = lastUpdatePath;
		}

		// Token: 0x06002F07 RID: 12039 RVA: 0x000C4B18 File Offset: 0x000C2D18
		protected VolumeRecentlyModifiedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.volumeName = (string)info.GetValue("volumeName", typeof(string));
			this.threshold = (TimeSpan)info.GetValue("threshold", typeof(TimeSpan));
			this.dateTimeUtc = (string)info.GetValue("dateTimeUtc", typeof(string));
			this.mountPoint = (string)info.GetValue("mountPoint", typeof(string));
			this.lastUpdatePath = (string)info.GetValue("lastUpdatePath", typeof(string));
		}

		// Token: 0x06002F08 RID: 12040 RVA: 0x000C4BD0 File Offset: 0x000C2DD0
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("volumeName", this.volumeName);
			info.AddValue("threshold", this.threshold);
			info.AddValue("dateTimeUtc", this.dateTimeUtc);
			info.AddValue("mountPoint", this.mountPoint);
			info.AddValue("lastUpdatePath", this.lastUpdatePath);
		}

		// Token: 0x17000C0F RID: 3087
		// (get) Token: 0x06002F09 RID: 12041 RVA: 0x000C4C3F File Offset: 0x000C2E3F
		public string VolumeName
		{
			get
			{
				return this.volumeName;
			}
		}

		// Token: 0x17000C10 RID: 3088
		// (get) Token: 0x06002F0A RID: 12042 RVA: 0x000C4C47 File Offset: 0x000C2E47
		public TimeSpan Threshold
		{
			get
			{
				return this.threshold;
			}
		}

		// Token: 0x17000C11 RID: 3089
		// (get) Token: 0x06002F0B RID: 12043 RVA: 0x000C4C4F File Offset: 0x000C2E4F
		public string DateTimeUtc
		{
			get
			{
				return this.dateTimeUtc;
			}
		}

		// Token: 0x17000C12 RID: 3090
		// (get) Token: 0x06002F0C RID: 12044 RVA: 0x000C4C57 File Offset: 0x000C2E57
		public string MountPoint
		{
			get
			{
				return this.mountPoint;
			}
		}

		// Token: 0x17000C13 RID: 3091
		// (get) Token: 0x06002F0D RID: 12045 RVA: 0x000C4C5F File Offset: 0x000C2E5F
		public string LastUpdatePath
		{
			get
			{
				return this.lastUpdatePath;
			}
		}

		// Token: 0x040015AC RID: 5548
		private readonly string volumeName;

		// Token: 0x040015AD RID: 5549
		private readonly TimeSpan threshold;

		// Token: 0x040015AE RID: 5550
		private readonly string dateTimeUtc;

		// Token: 0x040015AF RID: 5551
		private readonly string mountPoint;

		// Token: 0x040015B0 RID: 5552
		private readonly string lastUpdatePath;
	}
}
