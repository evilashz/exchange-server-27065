using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020004E6 RID: 1254
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class MonitoringADInitNotCompleteException : MonitoringADConfigException
	{
		// Token: 0x06002E6B RID: 11883 RVA: 0x000C35DC File Offset: 0x000C17DC
		public MonitoringADInitNotCompleteException() : base(ReplayStrings.MonitoringADInitNotCompleteException)
		{
		}

		// Token: 0x06002E6C RID: 11884 RVA: 0x000C35EE File Offset: 0x000C17EE
		public MonitoringADInitNotCompleteException(Exception innerException) : base(ReplayStrings.MonitoringADInitNotCompleteException, innerException)
		{
		}

		// Token: 0x06002E6D RID: 11885 RVA: 0x000C3601 File Offset: 0x000C1801
		protected MonitoringADInitNotCompleteException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06002E6E RID: 11886 RVA: 0x000C360B File Offset: 0x000C180B
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
