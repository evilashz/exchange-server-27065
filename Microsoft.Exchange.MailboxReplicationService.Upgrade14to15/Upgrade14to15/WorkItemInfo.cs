using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15
{
	// Token: 0x02000080 RID: 128
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DebuggerStepThrough]
	[DataContract(Name = "WorkItemInfo", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.BOX.OrchestrationEngine.WcfService.Contract.WorkloadService")]
	public class WorkItemInfo : IExtensibleDataObject
	{
		// Token: 0x1700014F RID: 335
		// (get) Token: 0x0600031F RID: 799 RVA: 0x00004110 File Offset: 0x00002310
		// (set) Token: 0x06000320 RID: 800 RVA: 0x00004118 File Offset: 0x00002318
		public ExtensionDataObject ExtensionData
		{
			get
			{
				return this.extensionDataField;
			}
			set
			{
				this.extensionDataField = value;
			}
		}

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x06000321 RID: 801 RVA: 0x00004121 File Offset: 0x00002321
		// (set) Token: 0x06000322 RID: 802 RVA: 0x00004129 File Offset: 0x00002329
		[DataMember]
		public DateTime Created
		{
			get
			{
				return this.CreatedField;
			}
			set
			{
				this.CreatedField = value;
			}
		}

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x06000323 RID: 803 RVA: 0x00004132 File Offset: 0x00002332
		// (set) Token: 0x06000324 RID: 804 RVA: 0x0000413A File Offset: 0x0000233A
		[DataMember]
		public DateTime Modified
		{
			get
			{
				return this.ModifiedField;
			}
			set
			{
				this.ModifiedField = value;
			}
		}

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x06000325 RID: 805 RVA: 0x00004143 File Offset: 0x00002343
		// (set) Token: 0x06000326 RID: 806 RVA: 0x0000414B File Offset: 0x0000234B
		[DataMember]
		public PilotUserInfo PilotUser
		{
			get
			{
				return this.PilotUserField;
			}
			set
			{
				this.PilotUserField = value;
			}
		}

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x06000327 RID: 807 RVA: 0x00004154 File Offset: 0x00002354
		// (set) Token: 0x06000328 RID: 808 RVA: 0x0000415C File Offset: 0x0000235C
		[DataMember]
		public DateTime ScheduledDate
		{
			get
			{
				return this.ScheduledDateField;
			}
			set
			{
				this.ScheduledDateField = value;
			}
		}

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x06000329 RID: 809 RVA: 0x00004165 File Offset: 0x00002365
		// (set) Token: 0x0600032A RID: 810 RVA: 0x0000416D File Offset: 0x0000236D
		[DataMember]
		public TenantInfo Tenant
		{
			get
			{
				return this.TenantField;
			}
			set
			{
				this.TenantField = value;
			}
		}

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x0600032B RID: 811 RVA: 0x00004176 File Offset: 0x00002376
		// (set) Token: 0x0600032C RID: 812 RVA: 0x0000417E File Offset: 0x0000237E
		[DataMember]
		public string WorkItemId
		{
			get
			{
				return this.WorkItemIdField;
			}
			set
			{
				this.WorkItemIdField = value;
			}
		}

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x0600032D RID: 813 RVA: 0x00004187 File Offset: 0x00002387
		// (set) Token: 0x0600032E RID: 814 RVA: 0x0000418F File Offset: 0x0000238F
		[DataMember]
		public WorkItemStatusInfo WorkItemStatus
		{
			get
			{
				return this.WorkItemStatusField;
			}
			set
			{
				this.WorkItemStatusField = value;
			}
		}

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x0600032F RID: 815 RVA: 0x00004198 File Offset: 0x00002398
		// (set) Token: 0x06000330 RID: 816 RVA: 0x000041A0 File Offset: 0x000023A0
		[DataMember]
		public string WorkItemType
		{
			get
			{
				return this.WorkItemTypeField;
			}
			set
			{
				this.WorkItemTypeField = value;
			}
		}

		// Token: 0x04000171 RID: 369
		private ExtensionDataObject extensionDataField;

		// Token: 0x04000172 RID: 370
		private DateTime CreatedField;

		// Token: 0x04000173 RID: 371
		private DateTime ModifiedField;

		// Token: 0x04000174 RID: 372
		private PilotUserInfo PilotUserField;

		// Token: 0x04000175 RID: 373
		private DateTime ScheduledDateField;

		// Token: 0x04000176 RID: 374
		private TenantInfo TenantField;

		// Token: 0x04000177 RID: 375
		private string WorkItemIdField;

		// Token: 0x04000178 RID: 376
		private WorkItemStatusInfo WorkItemStatusField;

		// Token: 0x04000179 RID: 377
		private string WorkItemTypeField;
	}
}
