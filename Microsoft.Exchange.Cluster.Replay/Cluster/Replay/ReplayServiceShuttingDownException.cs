using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020003F1 RID: 1009
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ReplayServiceShuttingDownException : TaskServerException
	{
		// Token: 0x0600292C RID: 10540 RVA: 0x000B94F9 File Offset: 0x000B76F9
		public ReplayServiceShuttingDownException() : base(ReplayStrings.ReplayServiceShuttingDownException)
		{
		}

		// Token: 0x0600292D RID: 10541 RVA: 0x000B950B File Offset: 0x000B770B
		public ReplayServiceShuttingDownException(Exception innerException) : base(ReplayStrings.ReplayServiceShuttingDownException, innerException)
		{
		}

		// Token: 0x0600292E RID: 10542 RVA: 0x000B951E File Offset: 0x000B771E
		protected ReplayServiceShuttingDownException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600292F RID: 10543 RVA: 0x000B9528 File Offset: 0x000B7728
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
