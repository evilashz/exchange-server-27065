using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15
{
	// Token: 0x02000085 RID: 133
	[DebuggerStepThrough]
	[DataContract(Name = "QueryTenantWorkItemsResponse", Namespace = "http://tempuri.org/")]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	public class QueryTenantWorkItemsResponse : IExtensibleDataObject
	{
		// Token: 0x17000169 RID: 361
		// (get) Token: 0x06000358 RID: 856 RVA: 0x000042F2 File Offset: 0x000024F2
		// (set) Token: 0x06000359 RID: 857 RVA: 0x000042FA File Offset: 0x000024FA
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

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x0600035A RID: 858 RVA: 0x00004303 File Offset: 0x00002503
		// (set) Token: 0x0600035B RID: 859 RVA: 0x0000430B File Offset: 0x0000250B
		[DataMember]
		public WorkItemInfo[] QueryTenantWorkItemsResult
		{
			get
			{
				return this.QueryTenantWorkItemsResultField;
			}
			set
			{
				this.QueryTenantWorkItemsResultField = value;
			}
		}

		// Token: 0x0400018B RID: 395
		private ExtensionDataObject extensionDataField;

		// Token: 0x0400018C RID: 396
		private WorkItemInfo[] QueryTenantWorkItemsResultField;
	}
}
