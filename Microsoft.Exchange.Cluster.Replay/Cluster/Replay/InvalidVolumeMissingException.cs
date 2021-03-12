using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x0200050D RID: 1293
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class InvalidVolumeMissingException : DatabaseVolumeInfoException
	{
		// Token: 0x06002F55 RID: 12117 RVA: 0x000C561D File Offset: 0x000C381D
		public InvalidVolumeMissingException(string volumeName) : base(ReplayStrings.InvalidVolumeMissingException(volumeName))
		{
			this.volumeName = volumeName;
		}

		// Token: 0x06002F56 RID: 12118 RVA: 0x000C5637 File Offset: 0x000C3837
		public InvalidVolumeMissingException(string volumeName, Exception innerException) : base(ReplayStrings.InvalidVolumeMissingException(volumeName), innerException)
		{
			this.volumeName = volumeName;
		}

		// Token: 0x06002F57 RID: 12119 RVA: 0x000C5652 File Offset: 0x000C3852
		protected InvalidVolumeMissingException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.volumeName = (string)info.GetValue("volumeName", typeof(string));
		}

		// Token: 0x06002F58 RID: 12120 RVA: 0x000C567C File Offset: 0x000C387C
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("volumeName", this.volumeName);
		}

		// Token: 0x17000C2B RID: 3115
		// (get) Token: 0x06002F59 RID: 12121 RVA: 0x000C5697 File Offset: 0x000C3897
		public string VolumeName
		{
			get
			{
				return this.volumeName;
			}
		}

		// Token: 0x040015C8 RID: 5576
		private readonly string volumeName;
	}
}
