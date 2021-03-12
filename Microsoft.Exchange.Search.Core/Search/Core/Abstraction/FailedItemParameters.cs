using System;
using System.Globalization;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Search.Core.Abstraction
{
	// Token: 0x02000012 RID: 18
	internal class FailedItemParameters
	{
		// Token: 0x06000050 RID: 80 RVA: 0x00002601 File Offset: 0x00000801
		public FailedItemParameters(FailureMode failureMode, FieldSet fields)
		{
			this.ResultLimit = int.MaxValue;
			this.Fields = fields;
			this.FailureMode = failureMode;
			this.Culture = CultureInfo.InvariantCulture;
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000051 RID: 81 RVA: 0x0000262D File Offset: 0x0000082D
		// (set) Token: 0x06000052 RID: 82 RVA: 0x00002635 File Offset: 0x00000835
		public Guid? MailboxGuid { get; set; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000053 RID: 83 RVA: 0x0000263E File Offset: 0x0000083E
		// (set) Token: 0x06000054 RID: 84 RVA: 0x00002646 File Offset: 0x00000846
		public FieldSet Fields { get; set; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000055 RID: 85 RVA: 0x0000264F File Offset: 0x0000084F
		// (set) Token: 0x06000056 RID: 86 RVA: 0x00002657 File Offset: 0x00000857
		public int? ErrorCode { get; set; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000057 RID: 87 RVA: 0x00002660 File Offset: 0x00000860
		// (set) Token: 0x06000058 RID: 88 RVA: 0x00002668 File Offset: 0x00000868
		public FailureMode FailureMode { get; set; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000059 RID: 89 RVA: 0x00002671 File Offset: 0x00000871
		// (set) Token: 0x0600005A RID: 90 RVA: 0x00002679 File Offset: 0x00000879
		public ExDateTime? StartDate { get; set; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600005B RID: 91 RVA: 0x00002682 File Offset: 0x00000882
		// (set) Token: 0x0600005C RID: 92 RVA: 0x0000268A File Offset: 0x0000088A
		public ExDateTime? EndDate { get; set; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600005D RID: 93 RVA: 0x00002693 File Offset: 0x00000893
		// (set) Token: 0x0600005E RID: 94 RVA: 0x0000269B File Offset: 0x0000089B
		public StoreObjectId ParentEntryId { get; set; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600005F RID: 95 RVA: 0x000026A4 File Offset: 0x000008A4
		// (set) Token: 0x06000060 RID: 96 RVA: 0x000026AC File Offset: 0x000008AC
		public int ResultLimit { get; set; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000061 RID: 97 RVA: 0x000026B5 File Offset: 0x000008B5
		// (set) Token: 0x06000062 RID: 98 RVA: 0x000026BD File Offset: 0x000008BD
		public long StartingIndexId { get; set; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000063 RID: 99 RVA: 0x000026C6 File Offset: 0x000008C6
		// (set) Token: 0x06000064 RID: 100 RVA: 0x000026CE File Offset: 0x000008CE
		public bool? IsPartiallyProcessed { get; set; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000065 RID: 101 RVA: 0x000026D7 File Offset: 0x000008D7
		// (set) Token: 0x06000066 RID: 102 RVA: 0x000026DF File Offset: 0x000008DF
		public CultureInfo Culture { get; set; }
	}
}
