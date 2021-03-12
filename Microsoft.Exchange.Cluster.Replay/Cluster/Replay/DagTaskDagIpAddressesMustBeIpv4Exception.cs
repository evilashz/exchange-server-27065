using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020003D5 RID: 981
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DagTaskDagIpAddressesMustBeIpv4Exception : LocalizedException
	{
		// Token: 0x06002894 RID: 10388 RVA: 0x000B82E1 File Offset: 0x000B64E1
		public DagTaskDagIpAddressesMustBeIpv4Exception() : base(ReplayStrings.DagTaskDagIpAddressesMustBeIpv4Exception)
		{
		}

		// Token: 0x06002895 RID: 10389 RVA: 0x000B82EE File Offset: 0x000B64EE
		public DagTaskDagIpAddressesMustBeIpv4Exception(Exception innerException) : base(ReplayStrings.DagTaskDagIpAddressesMustBeIpv4Exception, innerException)
		{
		}

		// Token: 0x06002896 RID: 10390 RVA: 0x000B82FC File Offset: 0x000B64FC
		protected DagTaskDagIpAddressesMustBeIpv4Exception(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06002897 RID: 10391 RVA: 0x000B8306 File Offset: 0x000B6506
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
