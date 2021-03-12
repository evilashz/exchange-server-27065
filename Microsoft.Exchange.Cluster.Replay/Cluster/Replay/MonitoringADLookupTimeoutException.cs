using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020004E3 RID: 1251
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class MonitoringADLookupTimeoutException : MonitoringADConfigException
	{
		// Token: 0x06002E5D RID: 11869 RVA: 0x000C349F File Offset: 0x000C169F
		public MonitoringADLookupTimeoutException(int timeoutMs) : base(ReplayStrings.MonitoringADLookupTimeoutException(timeoutMs))
		{
			this.timeoutMs = timeoutMs;
		}

		// Token: 0x06002E5E RID: 11870 RVA: 0x000C34B9 File Offset: 0x000C16B9
		public MonitoringADLookupTimeoutException(int timeoutMs, Exception innerException) : base(ReplayStrings.MonitoringADLookupTimeoutException(timeoutMs), innerException)
		{
			this.timeoutMs = timeoutMs;
		}

		// Token: 0x06002E5F RID: 11871 RVA: 0x000C34D4 File Offset: 0x000C16D4
		protected MonitoringADLookupTimeoutException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.timeoutMs = (int)info.GetValue("timeoutMs", typeof(int));
		}

		// Token: 0x06002E60 RID: 11872 RVA: 0x000C34FE File Offset: 0x000C16FE
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("timeoutMs", this.timeoutMs);
		}

		// Token: 0x17000BDB RID: 3035
		// (get) Token: 0x06002E61 RID: 11873 RVA: 0x000C3519 File Offset: 0x000C1719
		public int TimeoutMs
		{
			get
			{
				return this.timeoutMs;
			}
		}

		// Token: 0x04001578 RID: 5496
		private readonly int timeoutMs;
	}
}
