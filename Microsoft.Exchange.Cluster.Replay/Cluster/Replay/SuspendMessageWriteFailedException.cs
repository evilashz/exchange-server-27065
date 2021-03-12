using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x0200039C RID: 924
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class SuspendMessageWriteFailedException : TransientException
	{
		// Token: 0x06002755 RID: 10069 RVA: 0x000B5B8C File Offset: 0x000B3D8C
		public SuspendMessageWriteFailedException() : base(ReplayStrings.SuspendWantedWriteFailedException)
		{
		}

		// Token: 0x06002756 RID: 10070 RVA: 0x000B5B99 File Offset: 0x000B3D99
		public SuspendMessageWriteFailedException(Exception innerException) : base(ReplayStrings.SuspendWantedWriteFailedException, innerException)
		{
		}

		// Token: 0x06002757 RID: 10071 RVA: 0x000B5BA7 File Offset: 0x000B3DA7
		protected SuspendMessageWriteFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06002758 RID: 10072 RVA: 0x000B5BB1 File Offset: 0x000B3DB1
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
