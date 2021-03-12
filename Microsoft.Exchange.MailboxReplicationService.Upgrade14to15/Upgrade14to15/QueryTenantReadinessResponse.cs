using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15
{
	// Token: 0x0200008D RID: 141
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "QueryTenantReadinessResponse", Namespace = "http://tempuri.org/")]
	[DebuggerStepThrough]
	public class QueryTenantReadinessResponse : IExtensibleDataObject
	{
		// Token: 0x17000176 RID: 374
		// (get) Token: 0x0600037A RID: 890 RVA: 0x0000440F File Offset: 0x0000260F
		// (set) Token: 0x0600037B RID: 891 RVA: 0x00004417 File Offset: 0x00002617
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

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x0600037C RID: 892 RVA: 0x00004420 File Offset: 0x00002620
		// (set) Token: 0x0600037D RID: 893 RVA: 0x00004428 File Offset: 0x00002628
		[DataMember]
		public TenantReadiness[] QueryTenantReadinessResult
		{
			get
			{
				return this.QueryTenantReadinessResultField;
			}
			set
			{
				this.QueryTenantReadinessResultField = value;
			}
		}

		// Token: 0x04000198 RID: 408
		private ExtensionDataObject extensionDataField;

		// Token: 0x04000199 RID: 409
		private TenantReadiness[] QueryTenantReadinessResultField;
	}
}
