using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000402 RID: 1026
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ReplayServiceSuspendWantedSetException : TaskServerException
	{
		// Token: 0x06002976 RID: 10614 RVA: 0x000B9A83 File Offset: 0x000B7C83
		public ReplayServiceSuspendWantedSetException() : base(ReplayStrings.ReplayServiceSuspendWantedSetException)
		{
		}

		// Token: 0x06002977 RID: 10615 RVA: 0x000B9A95 File Offset: 0x000B7C95
		public ReplayServiceSuspendWantedSetException(Exception innerException) : base(ReplayStrings.ReplayServiceSuspendWantedSetException, innerException)
		{
		}

		// Token: 0x06002978 RID: 10616 RVA: 0x000B9AA8 File Offset: 0x000B7CA8
		protected ReplayServiceSuspendWantedSetException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06002979 RID: 10617 RVA: 0x000B9AB2 File Offset: 0x000B7CB2
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
