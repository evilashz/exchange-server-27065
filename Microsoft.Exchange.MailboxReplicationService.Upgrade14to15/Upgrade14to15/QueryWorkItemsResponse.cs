using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15
{
	// Token: 0x02000083 RID: 131
	[DataContract(Name = "QueryWorkItemsResponse", Namespace = "http://tempuri.org/")]
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	public class QueryWorkItemsResponse : IExtensibleDataObject
	{
		// Token: 0x17000165 RID: 357
		// (get) Token: 0x0600034E RID: 846 RVA: 0x0000429E File Offset: 0x0000249E
		// (set) Token: 0x0600034F RID: 847 RVA: 0x000042A6 File Offset: 0x000024A6
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

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x06000350 RID: 848 RVA: 0x000042AF File Offset: 0x000024AF
		// (set) Token: 0x06000351 RID: 849 RVA: 0x000042B7 File Offset: 0x000024B7
		[DataMember]
		public WorkItemQueryResult QueryWorkItemsResult
		{
			get
			{
				return this.QueryWorkItemsResultField;
			}
			set
			{
				this.QueryWorkItemsResultField = value;
			}
		}

		// Token: 0x04000187 RID: 391
		private ExtensionDataObject extensionDataField;

		// Token: 0x04000188 RID: 392
		private WorkItemQueryResult QueryWorkItemsResultField;
	}
}
