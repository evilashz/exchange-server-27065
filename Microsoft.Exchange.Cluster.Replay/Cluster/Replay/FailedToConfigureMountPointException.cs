using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000509 RID: 1289
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class FailedToConfigureMountPointException : DatabaseVolumeInfoException
	{
		// Token: 0x06002F3C RID: 12092 RVA: 0x000C526C File Offset: 0x000C346C
		public FailedToConfigureMountPointException(string volumeName, string reason) : base(ReplayStrings.FailedToConfigureMountPointException(volumeName, reason))
		{
			this.volumeName = volumeName;
			this.reason = reason;
		}

		// Token: 0x06002F3D RID: 12093 RVA: 0x000C528E File Offset: 0x000C348E
		public FailedToConfigureMountPointException(string volumeName, string reason, Exception innerException) : base(ReplayStrings.FailedToConfigureMountPointException(volumeName, reason), innerException)
		{
			this.volumeName = volumeName;
			this.reason = reason;
		}

		// Token: 0x06002F3E RID: 12094 RVA: 0x000C52B4 File Offset: 0x000C34B4
		protected FailedToConfigureMountPointException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.volumeName = (string)info.GetValue("volumeName", typeof(string));
			this.reason = (string)info.GetValue("reason", typeof(string));
		}

		// Token: 0x06002F3F RID: 12095 RVA: 0x000C5309 File Offset: 0x000C3509
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("volumeName", this.volumeName);
			info.AddValue("reason", this.reason);
		}

		// Token: 0x17000C22 RID: 3106
		// (get) Token: 0x06002F40 RID: 12096 RVA: 0x000C5335 File Offset: 0x000C3535
		public string VolumeName
		{
			get
			{
				return this.volumeName;
			}
		}

		// Token: 0x17000C23 RID: 3107
		// (get) Token: 0x06002F41 RID: 12097 RVA: 0x000C533D File Offset: 0x000C353D
		public string Reason
		{
			get
			{
				return this.reason;
			}
		}

		// Token: 0x040015BF RID: 5567
		private readonly string volumeName;

		// Token: 0x040015C0 RID: 5568
		private readonly string reason;
	}
}
