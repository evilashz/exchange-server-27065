using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000504 RID: 1284
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class CouldNotFindVolumeForFormatException : DatabaseVolumeInfoException
	{
		// Token: 0x06002F21 RID: 12065 RVA: 0x000C4F35 File Offset: 0x000C3135
		public CouldNotFindVolumeForFormatException() : base(ReplayStrings.CouldNotFindVolumeForFormatException)
		{
		}

		// Token: 0x06002F22 RID: 12066 RVA: 0x000C4F47 File Offset: 0x000C3147
		public CouldNotFindVolumeForFormatException(Exception innerException) : base(ReplayStrings.CouldNotFindVolumeForFormatException, innerException)
		{
		}

		// Token: 0x06002F23 RID: 12067 RVA: 0x000C4F5A File Offset: 0x000C315A
		protected CouldNotFindVolumeForFormatException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06002F24 RID: 12068 RVA: 0x000C4F64 File Offset: 0x000C3164
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
