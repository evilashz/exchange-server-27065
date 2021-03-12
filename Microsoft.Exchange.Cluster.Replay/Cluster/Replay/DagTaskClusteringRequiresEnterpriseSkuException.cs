using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020003DE RID: 990
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DagTaskClusteringRequiresEnterpriseSkuException : DagTaskServerException
	{
		// Token: 0x060028C4 RID: 10436 RVA: 0x000B886A File Offset: 0x000B6A6A
		public DagTaskClusteringRequiresEnterpriseSkuException() : base(ReplayStrings.DagTaskClusteringRequiresEnterpriseSkuException)
		{
		}

		// Token: 0x060028C5 RID: 10437 RVA: 0x000B887C File Offset: 0x000B6A7C
		public DagTaskClusteringRequiresEnterpriseSkuException(Exception innerException) : base(ReplayStrings.DagTaskClusteringRequiresEnterpriseSkuException, innerException)
		{
		}

		// Token: 0x060028C6 RID: 10438 RVA: 0x000B888F File Offset: 0x000B6A8F
		protected DagTaskClusteringRequiresEnterpriseSkuException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060028C7 RID: 10439 RVA: 0x000B8899 File Offset: 0x000B6A99
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
