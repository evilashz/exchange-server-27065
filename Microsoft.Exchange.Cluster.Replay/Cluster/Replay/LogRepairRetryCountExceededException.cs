using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020004D9 RID: 1241
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class LogRepairRetryCountExceededException : LocalizedException
	{
		// Token: 0x06002E26 RID: 11814 RVA: 0x000C2E1A File Offset: 0x000C101A
		public LogRepairRetryCountExceededException(long retryCount) : base(ReplayStrings.LogRepairRetryCountExceeded(retryCount))
		{
			this.retryCount = retryCount;
		}

		// Token: 0x06002E27 RID: 11815 RVA: 0x000C2E2F File Offset: 0x000C102F
		public LogRepairRetryCountExceededException(long retryCount, Exception innerException) : base(ReplayStrings.LogRepairRetryCountExceeded(retryCount), innerException)
		{
			this.retryCount = retryCount;
		}

		// Token: 0x06002E28 RID: 11816 RVA: 0x000C2E45 File Offset: 0x000C1045
		protected LogRepairRetryCountExceededException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.retryCount = (long)info.GetValue("retryCount", typeof(long));
		}

		// Token: 0x06002E29 RID: 11817 RVA: 0x000C2E6F File Offset: 0x000C106F
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("retryCount", this.retryCount);
		}

		// Token: 0x17000BCC RID: 3020
		// (get) Token: 0x06002E2A RID: 11818 RVA: 0x000C2E8A File Offset: 0x000C108A
		public long RetryCount
		{
			get
			{
				return this.retryCount;
			}
		}

		// Token: 0x04001569 RID: 5481
		private readonly long retryCount;
	}
}
