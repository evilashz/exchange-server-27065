using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020004E5 RID: 1253
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class MonitoringADServiceShuttingDownException : MonitoringADConfigException
	{
		// Token: 0x06002E67 RID: 11879 RVA: 0x000C35A3 File Offset: 0x000C17A3
		public MonitoringADServiceShuttingDownException() : base(ReplayStrings.MonitoringADServiceShuttingDownException)
		{
		}

		// Token: 0x06002E68 RID: 11880 RVA: 0x000C35B5 File Offset: 0x000C17B5
		public MonitoringADServiceShuttingDownException(Exception innerException) : base(ReplayStrings.MonitoringADServiceShuttingDownException, innerException)
		{
		}

		// Token: 0x06002E69 RID: 11881 RVA: 0x000C35C8 File Offset: 0x000C17C8
		protected MonitoringADServiceShuttingDownException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06002E6A RID: 11882 RVA: 0x000C35D2 File Offset: 0x000C17D2
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
