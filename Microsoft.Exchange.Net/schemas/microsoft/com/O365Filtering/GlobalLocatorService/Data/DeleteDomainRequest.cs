using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace schemas.microsoft.com.O365Filtering.GlobalLocatorService.Data
{
	// Token: 0x02000C43 RID: 3139
	[DataContract(Name = "DeleteDomainRequest", Namespace = "http://schemas.microsoft.com/O365Filtering/GlobalLocatorService/Data")]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DebuggerStepThrough]
	public class DeleteDomainRequest : IExtensibleDataObject
	{
		// Token: 0x1700115E RID: 4446
		// (get) Token: 0x060044CB RID: 17611 RVA: 0x000B6FD1 File Offset: 0x000B51D1
		// (set) Token: 0x060044CC RID: 17612 RVA: 0x000B6FD9 File Offset: 0x000B51D9
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

		// Token: 0x1700115F RID: 4447
		// (get) Token: 0x060044CD RID: 17613 RVA: 0x000B6FE2 File Offset: 0x000B51E2
		// (set) Token: 0x060044CE RID: 17614 RVA: 0x000B6FEA File Offset: 0x000B51EA
		[DataMember(IsRequired = true)]
		public DomainQuery Domain
		{
			get
			{
				return this.DomainField;
			}
			set
			{
				this.DomainField = value;
			}
		}

		// Token: 0x17001160 RID: 4448
		// (get) Token: 0x060044CF RID: 17615 RVA: 0x000B6FF3 File Offset: 0x000B51F3
		// (set) Token: 0x060044D0 RID: 17616 RVA: 0x000B6FFB File Offset: 0x000B51FB
		[DataMember(IsRequired = true)]
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

		// Token: 0x04003A24 RID: 14884
		private ExtensionDataObject extensionDataField;

		// Token: 0x04003A25 RID: 14885
		private DomainQuery DomainField;

		// Token: 0x04003A26 RID: 14886
		private Guid TenantIdField;
	}
}
