using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000505 RID: 1285
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class CouldNotFindVolumeException : DatabaseVolumeInfoException
	{
		// Token: 0x06002F25 RID: 12069 RVA: 0x000C4F6E File Offset: 0x000C316E
		public CouldNotFindVolumeException(string volumeName) : base(ReplayStrings.CouldNotFindVolumeException(volumeName))
		{
			this.volumeName = volumeName;
		}

		// Token: 0x06002F26 RID: 12070 RVA: 0x000C4F88 File Offset: 0x000C3188
		public CouldNotFindVolumeException(string volumeName, Exception innerException) : base(ReplayStrings.CouldNotFindVolumeException(volumeName), innerException)
		{
			this.volumeName = volumeName;
		}

		// Token: 0x06002F27 RID: 12071 RVA: 0x000C4FA3 File Offset: 0x000C31A3
		protected CouldNotFindVolumeException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.volumeName = (string)info.GetValue("volumeName", typeof(string));
		}

		// Token: 0x06002F28 RID: 12072 RVA: 0x000C4FCD File Offset: 0x000C31CD
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("volumeName", this.volumeName);
		}

		// Token: 0x17000C1B RID: 3099
		// (get) Token: 0x06002F29 RID: 12073 RVA: 0x000C4FE8 File Offset: 0x000C31E8
		public string VolumeName
		{
			get
			{
				return this.volumeName;
			}
		}

		// Token: 0x040015B8 RID: 5560
		private readonly string volumeName;
	}
}
