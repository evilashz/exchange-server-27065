using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15
{
	// Token: 0x02000071 RID: 113
	[DataContract(Name = "PickedWorkItemInfo", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.BOX.OrchestrationEngine.WcfService.Contract.SymphonyHandlerService")]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DebuggerStepThrough]
	public class PickedWorkItemInfo : IExtensibleDataObject
	{
		// Token: 0x1700011C RID: 284
		// (get) Token: 0x060002AC RID: 684 RVA: 0x00003CDD File Offset: 0x00001EDD
		// (set) Token: 0x060002AD RID: 685 RVA: 0x00003CE5 File Offset: 0x00001EE5
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

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x060002AE RID: 686 RVA: 0x00003CEE File Offset: 0x00001EEE
		// (set) Token: 0x060002AF RID: 687 RVA: 0x00003CF6 File Offset: 0x00001EF6
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

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x060002B0 RID: 688 RVA: 0x00003CFF File Offset: 0x00001EFF
		// (set) Token: 0x060002B1 RID: 689 RVA: 0x00003D07 File Offset: 0x00001F07
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

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x060002B2 RID: 690 RVA: 0x00003D10 File Offset: 0x00001F10
		// (set) Token: 0x060002B3 RID: 691 RVA: 0x00003D18 File Offset: 0x00001F18
		[DataMember]
		public Guid TenantId
		{
			get
			{
				return this.TenantIdField;
			}
			set
			{
				this.TenantIdField = value;
			}
		}

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x060002B4 RID: 692 RVA: 0x00003D21 File Offset: 0x00001F21
		// (set) Token: 0x060002B5 RID: 693 RVA: 0x00003D29 File Offset: 0x00001F29
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

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x060002B6 RID: 694 RVA: 0x00003D32 File Offset: 0x00001F32
		// (set) Token: 0x060002B7 RID: 695 RVA: 0x00003D3A File Offset: 0x00001F3A
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

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x060002B8 RID: 696 RVA: 0x00003D43 File Offset: 0x00001F43
		// (set) Token: 0x060002B9 RID: 697 RVA: 0x00003D4B File Offset: 0x00001F4B
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

		// Token: 0x04000134 RID: 308
		private ExtensionDataObject extensionDataField;

		// Token: 0x04000135 RID: 309
		private PilotUserInfo PilotUserField;

		// Token: 0x04000136 RID: 310
		private DateTime ScheduledDateField;

		// Token: 0x04000137 RID: 311
		private Guid TenantIdField;

		// Token: 0x04000138 RID: 312
		private string WorkItemIdField;

		// Token: 0x04000139 RID: 313
		private WorkItemStatusInfo WorkItemStatusField;

		// Token: 0x0400013A RID: 314
		private string WorkItemTypeField;
	}
}
