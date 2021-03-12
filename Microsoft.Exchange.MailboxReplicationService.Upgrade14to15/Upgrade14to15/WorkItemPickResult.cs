using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15
{
	// Token: 0x02000070 RID: 112
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "WorkItemPickResult", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.BOX.OrchestrationEngine.WcfService.Contract.SymphonyHandlerService")]
	[DebuggerStepThrough]
	public class WorkItemPickResult : IExtensibleDataObject
	{
		// Token: 0x17000118 RID: 280
		// (get) Token: 0x060002A3 RID: 675 RVA: 0x00003C91 File Offset: 0x00001E91
		// (set) Token: 0x060002A4 RID: 676 RVA: 0x00003C99 File Offset: 0x00001E99
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

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x060002A5 RID: 677 RVA: 0x00003CA2 File Offset: 0x00001EA2
		// (set) Token: 0x060002A6 RID: 678 RVA: 0x00003CAA File Offset: 0x00001EAA
		[DataMember]
		public byte[] Bookmark
		{
			get
			{
				return this.BookmarkField;
			}
			set
			{
				this.BookmarkField = value;
			}
		}

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x060002A7 RID: 679 RVA: 0x00003CB3 File Offset: 0x00001EB3
		// (set) Token: 0x060002A8 RID: 680 RVA: 0x00003CBB File Offset: 0x00001EBB
		[DataMember]
		public bool HasMoreResults
		{
			get
			{
				return this.HasMoreResultsField;
			}
			set
			{
				this.HasMoreResultsField = value;
			}
		}

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x060002A9 RID: 681 RVA: 0x00003CC4 File Offset: 0x00001EC4
		// (set) Token: 0x060002AA RID: 682 RVA: 0x00003CCC File Offset: 0x00001ECC
		[DataMember]
		public PickedWorkItemInfo[] WorkItems
		{
			get
			{
				return this.WorkItemsField;
			}
			set
			{
				this.WorkItemsField = value;
			}
		}

		// Token: 0x04000130 RID: 304
		private ExtensionDataObject extensionDataField;

		// Token: 0x04000131 RID: 305
		private byte[] BookmarkField;

		// Token: 0x04000132 RID: 306
		private bool HasMoreResultsField;

		// Token: 0x04000133 RID: 307
		private PickedWorkItemInfo[] WorkItemsField;
	}
}
