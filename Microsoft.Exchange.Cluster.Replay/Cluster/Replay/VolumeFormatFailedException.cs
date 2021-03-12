using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000502 RID: 1282
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class VolumeFormatFailedException : DatabaseVolumeInfoException
	{
		// Token: 0x06002F14 RID: 12052 RVA: 0x000C4D3D File Offset: 0x000C2F3D
		public VolumeFormatFailedException(string volumeName, string mountPoint, string err) : base(ReplayStrings.VolumeFormatFailedException(volumeName, mountPoint, err))
		{
			this.volumeName = volumeName;
			this.mountPoint = mountPoint;
			this.err = err;
		}

		// Token: 0x06002F15 RID: 12053 RVA: 0x000C4D67 File Offset: 0x000C2F67
		public VolumeFormatFailedException(string volumeName, string mountPoint, string err, Exception innerException) : base(ReplayStrings.VolumeFormatFailedException(volumeName, mountPoint, err), innerException)
		{
			this.volumeName = volumeName;
			this.mountPoint = mountPoint;
			this.err = err;
		}

		// Token: 0x06002F16 RID: 12054 RVA: 0x000C4D94 File Offset: 0x000C2F94
		protected VolumeFormatFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.volumeName = (string)info.GetValue("volumeName", typeof(string));
			this.mountPoint = (string)info.GetValue("mountPoint", typeof(string));
			this.err = (string)info.GetValue("err", typeof(string));
		}

		// Token: 0x06002F17 RID: 12055 RVA: 0x000C4E09 File Offset: 0x000C3009
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("volumeName", this.volumeName);
			info.AddValue("mountPoint", this.mountPoint);
			info.AddValue("err", this.err);
		}

		// Token: 0x17000C16 RID: 3094
		// (get) Token: 0x06002F18 RID: 12056 RVA: 0x000C4E46 File Offset: 0x000C3046
		public string VolumeName
		{
			get
			{
				return this.volumeName;
			}
		}

		// Token: 0x17000C17 RID: 3095
		// (get) Token: 0x06002F19 RID: 12057 RVA: 0x000C4E4E File Offset: 0x000C304E
		public string MountPoint
		{
			get
			{
				return this.mountPoint;
			}
		}

		// Token: 0x17000C18 RID: 3096
		// (get) Token: 0x06002F1A RID: 12058 RVA: 0x000C4E56 File Offset: 0x000C3056
		public string Err
		{
			get
			{
				return this.err;
			}
		}

		// Token: 0x040015B3 RID: 5555
		private readonly string volumeName;

		// Token: 0x040015B4 RID: 5556
		private readonly string mountPoint;

		// Token: 0x040015B5 RID: 5557
		private readonly string err;
	}
}
