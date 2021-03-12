using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000427 RID: 1063
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class AcllUnboundedDatalossDetectedException : TransientException
	{
		// Token: 0x06002A41 RID: 10817 RVA: 0x000BB30B File Offset: 0x000B950B
		public AcllUnboundedDatalossDetectedException(string dbName, string lastUpdatedTimeStr, string allowedDurationStr, string actualDurationStr) : base(ReplayStrings.AcllUnboundedDatalossDetectedException(dbName, lastUpdatedTimeStr, allowedDurationStr, actualDurationStr))
		{
			this.dbName = dbName;
			this.lastUpdatedTimeStr = lastUpdatedTimeStr;
			this.allowedDurationStr = allowedDurationStr;
			this.actualDurationStr = actualDurationStr;
		}

		// Token: 0x06002A42 RID: 10818 RVA: 0x000BB33A File Offset: 0x000B953A
		public AcllUnboundedDatalossDetectedException(string dbName, string lastUpdatedTimeStr, string allowedDurationStr, string actualDurationStr, Exception innerException) : base(ReplayStrings.AcllUnboundedDatalossDetectedException(dbName, lastUpdatedTimeStr, allowedDurationStr, actualDurationStr), innerException)
		{
			this.dbName = dbName;
			this.lastUpdatedTimeStr = lastUpdatedTimeStr;
			this.allowedDurationStr = allowedDurationStr;
			this.actualDurationStr = actualDurationStr;
		}

		// Token: 0x06002A43 RID: 10819 RVA: 0x000BB36C File Offset: 0x000B956C
		protected AcllUnboundedDatalossDetectedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.dbName = (string)info.GetValue("dbName", typeof(string));
			this.lastUpdatedTimeStr = (string)info.GetValue("lastUpdatedTimeStr", typeof(string));
			this.allowedDurationStr = (string)info.GetValue("allowedDurationStr", typeof(string));
			this.actualDurationStr = (string)info.GetValue("actualDurationStr", typeof(string));
		}

		// Token: 0x06002A44 RID: 10820 RVA: 0x000BB404 File Offset: 0x000B9604
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("dbName", this.dbName);
			info.AddValue("lastUpdatedTimeStr", this.lastUpdatedTimeStr);
			info.AddValue("allowedDurationStr", this.allowedDurationStr);
			info.AddValue("actualDurationStr", this.actualDurationStr);
		}

		// Token: 0x17000AAF RID: 2735
		// (get) Token: 0x06002A45 RID: 10821 RVA: 0x000BB45D File Offset: 0x000B965D
		public string DbName
		{
			get
			{
				return this.dbName;
			}
		}

		// Token: 0x17000AB0 RID: 2736
		// (get) Token: 0x06002A46 RID: 10822 RVA: 0x000BB465 File Offset: 0x000B9665
		public string LastUpdatedTimeStr
		{
			get
			{
				return this.lastUpdatedTimeStr;
			}
		}

		// Token: 0x17000AB1 RID: 2737
		// (get) Token: 0x06002A47 RID: 10823 RVA: 0x000BB46D File Offset: 0x000B966D
		public string AllowedDurationStr
		{
			get
			{
				return this.allowedDurationStr;
			}
		}

		// Token: 0x17000AB2 RID: 2738
		// (get) Token: 0x06002A48 RID: 10824 RVA: 0x000BB475 File Offset: 0x000B9675
		public string ActualDurationStr
		{
			get
			{
				return this.actualDurationStr;
			}
		}

		// Token: 0x0400144C RID: 5196
		private readonly string dbName;

		// Token: 0x0400144D RID: 5197
		private readonly string lastUpdatedTimeStr;

		// Token: 0x0400144E RID: 5198
		private readonly string allowedDurationStr;

		// Token: 0x0400144F RID: 5199
		private readonly string actualDurationStr;
	}
}
