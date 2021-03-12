using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020004B1 RID: 1201
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class FailedToGetClusterCoreGroupException : TransientException
	{
		// Token: 0x06002D40 RID: 11584 RVA: 0x000C10FD File Offset: 0x000BF2FD
		public FailedToGetClusterCoreGroupException() : base(ReplayStrings.ErrorFailedToGetClusterCoreGroup)
		{
		}

		// Token: 0x06002D41 RID: 11585 RVA: 0x000C110A File Offset: 0x000BF30A
		public FailedToGetClusterCoreGroupException(Exception innerException) : base(ReplayStrings.ErrorFailedToGetClusterCoreGroup, innerException)
		{
		}

		// Token: 0x06002D42 RID: 11586 RVA: 0x000C1118 File Offset: 0x000BF318
		protected FailedToGetClusterCoreGroupException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06002D43 RID: 11587 RVA: 0x000C1122 File Offset: 0x000BF322
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
