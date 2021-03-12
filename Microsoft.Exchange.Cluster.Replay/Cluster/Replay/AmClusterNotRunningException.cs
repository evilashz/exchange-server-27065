using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Cluster.Shared;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000463 RID: 1123
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class AmClusterNotRunningException : ClusterException
	{
		// Token: 0x06002B84 RID: 11140 RVA: 0x000BD884 File Offset: 0x000BBA84
		public AmClusterNotRunningException() : base(ReplayStrings.AmClusterNotRunningException)
		{
		}

		// Token: 0x06002B85 RID: 11141 RVA: 0x000BD896 File Offset: 0x000BBA96
		public AmClusterNotRunningException(Exception innerException) : base(ReplayStrings.AmClusterNotRunningException, innerException)
		{
		}

		// Token: 0x06002B86 RID: 11142 RVA: 0x000BD8A9 File Offset: 0x000BBAA9
		protected AmClusterNotRunningException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06002B87 RID: 11143 RVA: 0x000BD8B3 File Offset: 0x000BBAB3
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
