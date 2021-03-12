using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020004E4 RID: 1252
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class MonitoringADFirstLookupTimeoutException : MonitoringADConfigException
	{
		// Token: 0x06002E62 RID: 11874 RVA: 0x000C3521 File Offset: 0x000C1721
		public MonitoringADFirstLookupTimeoutException(int timeoutMs) : base(ReplayStrings.MonitoringADFirstLookupTimeoutException(timeoutMs))
		{
			this.timeoutMs = timeoutMs;
		}

		// Token: 0x06002E63 RID: 11875 RVA: 0x000C353B File Offset: 0x000C173B
		public MonitoringADFirstLookupTimeoutException(int timeoutMs, Exception innerException) : base(ReplayStrings.MonitoringADFirstLookupTimeoutException(timeoutMs), innerException)
		{
			this.timeoutMs = timeoutMs;
		}

		// Token: 0x06002E64 RID: 11876 RVA: 0x000C3556 File Offset: 0x000C1756
		protected MonitoringADFirstLookupTimeoutException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.timeoutMs = (int)info.GetValue("timeoutMs", typeof(int));
		}

		// Token: 0x06002E65 RID: 11877 RVA: 0x000C3580 File Offset: 0x000C1780
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("timeoutMs", this.timeoutMs);
		}

		// Token: 0x17000BDC RID: 3036
		// (get) Token: 0x06002E66 RID: 11878 RVA: 0x000C359B File Offset: 0x000C179B
		public int TimeoutMs
		{
			get
			{
				return this.timeoutMs;
			}
		}

		// Token: 0x04001579 RID: 5497
		private readonly int timeoutMs;
	}
}
