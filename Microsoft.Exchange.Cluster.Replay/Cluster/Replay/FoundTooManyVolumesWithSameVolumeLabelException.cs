using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000512 RID: 1298
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class FoundTooManyVolumesWithSameVolumeLabelException : DatabaseVolumeInfoException
	{
		// Token: 0x06002F6E RID: 12142 RVA: 0x000C58B2 File Offset: 0x000C3AB2
		public FoundTooManyVolumesWithSameVolumeLabelException(string volumeNames, string volumeLabel) : base(ReplayStrings.FoundTooManyVolumesWithSameVolumeLabelException(volumeNames, volumeLabel))
		{
			this.volumeNames = volumeNames;
			this.volumeLabel = volumeLabel;
		}

		// Token: 0x06002F6F RID: 12143 RVA: 0x000C58D4 File Offset: 0x000C3AD4
		public FoundTooManyVolumesWithSameVolumeLabelException(string volumeNames, string volumeLabel, Exception innerException) : base(ReplayStrings.FoundTooManyVolumesWithSameVolumeLabelException(volumeNames, volumeLabel), innerException)
		{
			this.volumeNames = volumeNames;
			this.volumeLabel = volumeLabel;
		}

		// Token: 0x06002F70 RID: 12144 RVA: 0x000C58F8 File Offset: 0x000C3AF8
		protected FoundTooManyVolumesWithSameVolumeLabelException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.volumeNames = (string)info.GetValue("volumeNames", typeof(string));
			this.volumeLabel = (string)info.GetValue("volumeLabel", typeof(string));
		}

		// Token: 0x06002F71 RID: 12145 RVA: 0x000C594D File Offset: 0x000C3B4D
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("volumeNames", this.volumeNames);
			info.AddValue("volumeLabel", this.volumeLabel);
		}

		// Token: 0x17000C30 RID: 3120
		// (get) Token: 0x06002F72 RID: 12146 RVA: 0x000C5979 File Offset: 0x000C3B79
		public string VolumeNames
		{
			get
			{
				return this.volumeNames;
			}
		}

		// Token: 0x17000C31 RID: 3121
		// (get) Token: 0x06002F73 RID: 12147 RVA: 0x000C5981 File Offset: 0x000C3B81
		public string VolumeLabel
		{
			get
			{
				return this.volumeLabel;
			}
		}

		// Token: 0x040015CD RID: 5581
		private readonly string volumeNames;

		// Token: 0x040015CE RID: 5582
		private readonly string volumeLabel;
	}
}
