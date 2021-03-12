using System;

namespace Microsoft.Exchange.Assistants
{
	// Token: 0x020000B0 RID: 176
	internal class QueryableGovernor : QueryableObjectImplBase<QueryableGovernorObjectSchema>
	{
		// Token: 0x17000158 RID: 344
		// (get) Token: 0x0600054B RID: 1355 RVA: 0x0001A951 File Offset: 0x00018B51
		// (set) Token: 0x0600054C RID: 1356 RVA: 0x0001A963 File Offset: 0x00018B63
		public string Status
		{
			get
			{
				return (string)this[QueryableGovernorObjectSchema.Status];
			}
			set
			{
				this[QueryableGovernorObjectSchema.Status] = value;
			}
		}

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x0600054D RID: 1357 RVA: 0x0001A971 File Offset: 0x00018B71
		// (set) Token: 0x0600054E RID: 1358 RVA: 0x0001A983 File Offset: 0x00018B83
		public DateTime LastRunTime
		{
			get
			{
				return (DateTime)this[QueryableGovernorObjectSchema.LastRunTime];
			}
			set
			{
				this[QueryableGovernorObjectSchema.LastRunTime] = value;
			}
		}

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x0600054F RID: 1359 RVA: 0x0001A996 File Offset: 0x00018B96
		// (set) Token: 0x06000550 RID: 1360 RVA: 0x0001A9A8 File Offset: 0x00018BA8
		public long NumberConsecutiveFailures
		{
			get
			{
				return (long)this[QueryableGovernorObjectSchema.NumberConsecutiveFailures];
			}
			set
			{
				this[QueryableGovernorObjectSchema.NumberConsecutiveFailures] = value;
			}
		}
	}
}
