using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace schemas.microsoft.com.O365Filtering.GlobalLocatorService.Data
{
	// Token: 0x02000C42 RID: 3138
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "DeleteTenantRequest", Namespace = "http://schemas.microsoft.com/O365Filtering/GlobalLocatorService/Data")]
	public class DeleteTenantRequest : IExtensibleDataObject
	{
		// Token: 0x1700115C RID: 4444
		// (get) Token: 0x060044C6 RID: 17606 RVA: 0x000B6FA7 File Offset: 0x000B51A7
		// (set) Token: 0x060044C7 RID: 17607 RVA: 0x000B6FAF File Offset: 0x000B51AF
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

		// Token: 0x1700115D RID: 4445
		// (get) Token: 0x060044C8 RID: 17608 RVA: 0x000B6FB8 File Offset: 0x000B51B8
		// (set) Token: 0x060044C9 RID: 17609 RVA: 0x000B6FC0 File Offset: 0x000B51C0
		[DataMember]
		public TenantQuery Tenant
		{
			get
			{
				return this.TenantField;
			}
			set
			{
				this.TenantField = value;
			}
		}

		// Token: 0x04003A22 RID: 14882
		private ExtensionDataObject extensionDataField;

		// Token: 0x04003A23 RID: 14883
		private TenantQuery TenantField;
	}
}
