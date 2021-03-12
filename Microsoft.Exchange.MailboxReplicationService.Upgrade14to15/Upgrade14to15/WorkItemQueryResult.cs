using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15
{
	// Token: 0x0200007F RID: 127
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "WorkItemQueryResult", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.BOX.OrchestrationEngine.WcfService.Contract.WorkloadService")]
	[DebuggerStepThrough]
	public class WorkItemQueryResult : IExtensibleDataObject
	{
		// Token: 0x1700014B RID: 331
		// (get) Token: 0x06000316 RID: 790 RVA: 0x000040C4 File Offset: 0x000022C4
		// (set) Token: 0x06000317 RID: 791 RVA: 0x000040CC File Offset: 0x000022CC
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

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x06000318 RID: 792 RVA: 0x000040D5 File Offset: 0x000022D5
		// (set) Token: 0x06000319 RID: 793 RVA: 0x000040DD File Offset: 0x000022DD
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

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x0600031A RID: 794 RVA: 0x000040E6 File Offset: 0x000022E6
		// (set) Token: 0x0600031B RID: 795 RVA: 0x000040EE File Offset: 0x000022EE
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

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x0600031C RID: 796 RVA: 0x000040F7 File Offset: 0x000022F7
		// (set) Token: 0x0600031D RID: 797 RVA: 0x000040FF File Offset: 0x000022FF
		[DataMember]
		public WorkItemInfo[] WorkItems
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

		// Token: 0x0400016D RID: 365
		private ExtensionDataObject extensionDataField;

		// Token: 0x0400016E RID: 366
		private byte[] BookmarkField;

		// Token: 0x0400016F RID: 367
		private bool HasMoreResultsField;

		// Token: 0x04000170 RID: 368
		private WorkItemInfo[] WorkItemsField;
	}
}
