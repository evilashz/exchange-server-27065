using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000401 RID: 1025
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ReplayServiceSuspendInPlaceReseedBlockedException : TaskServerException
	{
		// Token: 0x06002972 RID: 10610 RVA: 0x000B9A4A File Offset: 0x000B7C4A
		public ReplayServiceSuspendInPlaceReseedBlockedException() : base(ReplayStrings.ReplayServiceSuspendInPlaceReseedBlockedException)
		{
		}

		// Token: 0x06002973 RID: 10611 RVA: 0x000B9A5C File Offset: 0x000B7C5C
		public ReplayServiceSuspendInPlaceReseedBlockedException(Exception innerException) : base(ReplayStrings.ReplayServiceSuspendInPlaceReseedBlockedException, innerException)
		{
		}

		// Token: 0x06002974 RID: 10612 RVA: 0x000B9A6F File Offset: 0x000B7C6F
		protected ReplayServiceSuspendInPlaceReseedBlockedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06002975 RID: 10613 RVA: 0x000B9A79 File Offset: 0x000B7C79
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
