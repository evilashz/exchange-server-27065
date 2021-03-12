using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15
{
	// Token: 0x02000084 RID: 132
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "QueryTenantWorkItems", Namespace = "http://tempuri.org/")]
	[DebuggerStepThrough]
	public class QueryTenantWorkItems : IExtensibleDataObject
	{
		// Token: 0x17000167 RID: 359
		// (get) Token: 0x06000353 RID: 851 RVA: 0x000042C8 File Offset: 0x000024C8
		// (set) Token: 0x06000354 RID: 852 RVA: 0x000042D0 File Offset: 0x000024D0
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

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x06000355 RID: 853 RVA: 0x000042D9 File Offset: 0x000024D9
		// (set) Token: 0x06000356 RID: 854 RVA: 0x000042E1 File Offset: 0x000024E1
		[DataMember]
		public Guid tenantId
		{
			get
			{
				return this.tenantIdField;
			}
			set
			{
				this.tenantIdField = value;
			}
		}

		// Token: 0x04000189 RID: 393
		private ExtensionDataObject extensionDataField;

		// Token: 0x0400018A RID: 394
		private Guid tenantIdField;
	}
}
