using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Hygiene.Data.AsyncQueue
{
	// Token: 0x02000029 RID: 41
	internal class AsyncQueueStatusReport : ConfigurablePropertyBag
	{
		// Token: 0x17000073 RID: 115
		// (get) Token: 0x06000157 RID: 343 RVA: 0x000059D0 File Offset: 0x00003BD0
		public override ObjectId Identity
		{
			get
			{
				return new ConfigObjectId(this.RequestId.ToString());
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x06000158 RID: 344 RVA: 0x000059F6 File Offset: 0x00003BF6
		public Guid OrganizationalUnitRoot
		{
			get
			{
				return (Guid)this[AsyncQueueStatusReportSchema.OrganizationalUnitRootProperty];
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x06000159 RID: 345 RVA: 0x00005A08 File Offset: 0x00003C08
		public Guid RequestId
		{
			get
			{
				return (Guid)this[AsyncQueueStatusReportSchema.RequestIdProperty];
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x0600015A RID: 346 RVA: 0x00005A1A File Offset: 0x00003C1A
		public string FriendlyName
		{
			get
			{
				return (string)this[AsyncQueueStatusReportSchema.FriendlyNameProperty];
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x0600015B RID: 347 RVA: 0x00005A2C File Offset: 0x00003C2C
		public DateTime CreationTime
		{
			get
			{
				return (DateTime)this[AsyncQueueStatusReportSchema.CreatedDatetimeProperty];
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x0600015C RID: 348 RVA: 0x00005A3E File Offset: 0x00003C3E
		public string StepName
		{
			get
			{
				return (string)this[AsyncQueueStatusReportSchema.StepNameProperty];
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x0600015D RID: 349 RVA: 0x00005A50 File Offset: 0x00003C50
		public Guid RequestStepId
		{
			get
			{
				return (Guid)this[AsyncQueueStatusReportSchema.RequestStepIdProperty];
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x0600015E RID: 350 RVA: 0x00005A62 File Offset: 0x00003C62
		public AsyncQueueStatus StepStatus
		{
			get
			{
				return (AsyncQueueStatus)this[AsyncQueueStatusReportSchema.StepStatusProperty];
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x0600015F RID: 351 RVA: 0x00005A74 File Offset: 0x00003C74
		public int FetchCount
		{
			get
			{
				return (int)this[AsyncQueueStatusReportSchema.FetchCountProperty];
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x06000160 RID: 352 RVA: 0x00005A86 File Offset: 0x00003C86
		public int ErrorCount
		{
			get
			{
				return (int)this[AsyncQueueStatusReportSchema.ErrorCountProperty];
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x06000161 RID: 353 RVA: 0x00005A98 File Offset: 0x00003C98
		public DateTime? NextFetchDatetime
		{
			get
			{
				return (DateTime?)this[AsyncQueueStatusReportSchema.NextFetchDatetimeProperty];
			}
		}

		// Token: 0x06000162 RID: 354 RVA: 0x00005AAA File Offset: 0x00003CAA
		public override Type GetSchemaType()
		{
			return typeof(AsyncQueueStatusReportSchema);
		}
	}
}
