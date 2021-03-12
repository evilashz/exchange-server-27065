using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020004ED RID: 1261
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DbHTFirstLookupTimeoutException : DatabaseHealthTrackerException
	{
		// Token: 0x06002E90 RID: 11920 RVA: 0x000C39F7 File Offset: 0x000C1BF7
		public DbHTFirstLookupTimeoutException(int timeoutMs) : base(ReplayStrings.DbHTFirstLookupTimeoutException(timeoutMs))
		{
			this.timeoutMs = timeoutMs;
		}

		// Token: 0x06002E91 RID: 11921 RVA: 0x000C3A11 File Offset: 0x000C1C11
		public DbHTFirstLookupTimeoutException(int timeoutMs, Exception innerException) : base(ReplayStrings.DbHTFirstLookupTimeoutException(timeoutMs), innerException)
		{
			this.timeoutMs = timeoutMs;
		}

		// Token: 0x06002E92 RID: 11922 RVA: 0x000C3A2C File Offset: 0x000C1C2C
		protected DbHTFirstLookupTimeoutException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.timeoutMs = (int)info.GetValue("timeoutMs", typeof(int));
		}

		// Token: 0x06002E93 RID: 11923 RVA: 0x000C3A56 File Offset: 0x000C1C56
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("timeoutMs", this.timeoutMs);
		}

		// Token: 0x17000BE6 RID: 3046
		// (get) Token: 0x06002E94 RID: 11924 RVA: 0x000C3A71 File Offset: 0x000C1C71
		public int TimeoutMs
		{
			get
			{
				return this.timeoutMs;
			}
		}

		// Token: 0x04001583 RID: 5507
		private readonly int timeoutMs;
	}
}
