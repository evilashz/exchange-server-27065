using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15
{
	// Token: 0x0200008C RID: 140
	[DataContract(Name = "QueryTenantReadiness", Namespace = "http://tempuri.org/")]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DebuggerStepThrough]
	public class QueryTenantReadiness : IExtensibleDataObject
	{
		// Token: 0x17000174 RID: 372
		// (get) Token: 0x06000375 RID: 885 RVA: 0x000043E5 File Offset: 0x000025E5
		// (set) Token: 0x06000376 RID: 886 RVA: 0x000043ED File Offset: 0x000025ED
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

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x06000377 RID: 887 RVA: 0x000043F6 File Offset: 0x000025F6
		// (set) Token: 0x06000378 RID: 888 RVA: 0x000043FE File Offset: 0x000025FE
		[DataMember]
		public Guid[] tenantIds
		{
			get
			{
				return this.tenantIdsField;
			}
			set
			{
				this.tenantIdsField = value;
			}
		}

		// Token: 0x04000196 RID: 406
		private ExtensionDataObject extensionDataField;

		// Token: 0x04000197 RID: 407
		private Guid[] tenantIdsField;
	}
}
