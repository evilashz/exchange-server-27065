using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15
{
	// Token: 0x02000073 RID: 115
	[DataContract(Name = "WorkItemStatusInfo", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.BOX.OrchestrationEngine.WcfService.Contract.WorkloadService")]
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	public class WorkItemStatusInfo : IExtensibleDataObject
	{
		// Token: 0x17000126 RID: 294
		// (get) Token: 0x060002C2 RID: 706 RVA: 0x00003D97 File Offset: 0x00001F97
		// (set) Token: 0x060002C3 RID: 707 RVA: 0x00003D9F File Offset: 0x00001F9F
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

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x060002C4 RID: 708 RVA: 0x00003DA8 File Offset: 0x00001FA8
		// (set) Token: 0x060002C5 RID: 709 RVA: 0x00003DB0 File Offset: 0x00001FB0
		[DataMember]
		public string Comment
		{
			get
			{
				return this.CommentField;
			}
			set
			{
				this.CommentField = value;
			}
		}

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x060002C6 RID: 710 RVA: 0x00003DB9 File Offset: 0x00001FB9
		// (set) Token: 0x060002C7 RID: 711 RVA: 0x00003DC1 File Offset: 0x00001FC1
		[DataMember]
		public int CompletedCount
		{
			get
			{
				return this.CompletedCountField;
			}
			set
			{
				this.CompletedCountField = value;
			}
		}

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x060002C8 RID: 712 RVA: 0x00003DCA File Offset: 0x00001FCA
		// (set) Token: 0x060002C9 RID: 713 RVA: 0x00003DD2 File Offset: 0x00001FD2
		[DataMember]
		public string HandlerState
		{
			get
			{
				return this.HandlerStateField;
			}
			set
			{
				this.HandlerStateField = value;
			}
		}

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x060002CA RID: 714 RVA: 0x00003DDB File Offset: 0x00001FDB
		// (set) Token: 0x060002CB RID: 715 RVA: 0x00003DE3 File Offset: 0x00001FE3
		[DataMember]
		public WorkItemStatus Status
		{
			get
			{
				return this.StatusField;
			}
			set
			{
				this.StatusField = value;
			}
		}

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x060002CC RID: 716 RVA: 0x00003DEC File Offset: 0x00001FEC
		// (set) Token: 0x060002CD RID: 717 RVA: 0x00003DF4 File Offset: 0x00001FF4
		[DataMember]
		public Uri StatusDetails
		{
			get
			{
				return this.StatusDetailsField;
			}
			set
			{
				this.StatusDetailsField = value;
			}
		}

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x060002CE RID: 718 RVA: 0x00003DFD File Offset: 0x00001FFD
		// (set) Token: 0x060002CF RID: 719 RVA: 0x00003E05 File Offset: 0x00002005
		[DataMember]
		public int TotalCount
		{
			get
			{
				return this.TotalCountField;
			}
			set
			{
				this.TotalCountField = value;
			}
		}

		// Token: 0x0400013E RID: 318
		private ExtensionDataObject extensionDataField;

		// Token: 0x0400013F RID: 319
		private string CommentField;

		// Token: 0x04000140 RID: 320
		private int CompletedCountField;

		// Token: 0x04000141 RID: 321
		private string HandlerStateField;

		// Token: 0x04000142 RID: 322
		private WorkItemStatus StatusField;

		// Token: 0x04000143 RID: 323
		private Uri StatusDetailsField;

		// Token: 0x04000144 RID: 324
		private int TotalCountField;
	}
}
