using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020003FD RID: 1021
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ReplayServiceSuspendBlockedResynchronizingException : TaskServerException
	{
		// Token: 0x06002962 RID: 10594 RVA: 0x000B9966 File Offset: 0x000B7B66
		public ReplayServiceSuspendBlockedResynchronizingException() : base(ReplayStrings.ReplayServiceSuspendBlockedResynchronizingException)
		{
		}

		// Token: 0x06002963 RID: 10595 RVA: 0x000B9978 File Offset: 0x000B7B78
		public ReplayServiceSuspendBlockedResynchronizingException(Exception innerException) : base(ReplayStrings.ReplayServiceSuspendBlockedResynchronizingException, innerException)
		{
		}

		// Token: 0x06002964 RID: 10596 RVA: 0x000B998B File Offset: 0x000B7B8B
		protected ReplayServiceSuspendBlockedResynchronizingException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06002965 RID: 10597 RVA: 0x000B9995 File Offset: 0x000B7B95
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
