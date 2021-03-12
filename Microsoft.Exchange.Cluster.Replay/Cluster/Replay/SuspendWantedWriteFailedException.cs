using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x0200039B RID: 923
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class SuspendWantedWriteFailedException : TransientException
	{
		// Token: 0x06002751 RID: 10065 RVA: 0x000B5B5D File Offset: 0x000B3D5D
		public SuspendWantedWriteFailedException() : base(ReplayStrings.SuspendWantedWriteFailedException)
		{
		}

		// Token: 0x06002752 RID: 10066 RVA: 0x000B5B6A File Offset: 0x000B3D6A
		public SuspendWantedWriteFailedException(Exception innerException) : base(ReplayStrings.SuspendWantedWriteFailedException, innerException)
		{
		}

		// Token: 0x06002753 RID: 10067 RVA: 0x000B5B78 File Offset: 0x000B3D78
		protected SuspendWantedWriteFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06002754 RID: 10068 RVA: 0x000B5B82 File Offset: 0x000B3D82
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
