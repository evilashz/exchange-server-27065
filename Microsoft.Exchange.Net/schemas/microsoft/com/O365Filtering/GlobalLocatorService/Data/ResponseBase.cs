using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace schemas.microsoft.com.O365Filtering.GlobalLocatorService.Data
{
	// Token: 0x02000C2F RID: 3119
	[KnownType(typeof(DeleteUserResponse))]
	[KnownType(typeof(DeleteTenantResponse))]
	[KnownType(typeof(DeleteDomainResponse))]
	[KnownType(typeof(SaveDomainResponse))]
	[KnownType(typeof(SaveUserResponse))]
	[KnownType(typeof(FindUserResponse))]
	[KnownType(typeof(FindTenantResponse))]
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[KnownType(typeof(FindDomainsResponse))]
	[DataContract(Name = "ResponseBase", Namespace = "http://schemas.microsoft.com/O365Filtering/GlobalLocatorService/Data")]
	[KnownType(typeof(FindDomainResponse))]
	[KnownType(typeof(SaveTenantResponse))]
	public class ResponseBase : IExtensibleDataObject
	{
		// Token: 0x17001136 RID: 4406
		// (get) Token: 0x06004468 RID: 17512 RVA: 0x000B6C91 File Offset: 0x000B4E91
		// (set) Token: 0x06004469 RID: 17513 RVA: 0x000B6C99 File Offset: 0x000B4E99
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

		// Token: 0x17001137 RID: 4407
		// (get) Token: 0x0600446A RID: 17514 RVA: 0x000B6CA2 File Offset: 0x000B4EA2
		// (set) Token: 0x0600446B RID: 17515 RVA: 0x000B6CAA File Offset: 0x000B4EAA
		[DataMember]
		public string Diagnostics
		{
			get
			{
				return this.DiagnosticsField;
			}
			set
			{
				this.DiagnosticsField = value;
			}
		}

		// Token: 0x17001138 RID: 4408
		// (get) Token: 0x0600446C RID: 17516 RVA: 0x000B6CB3 File Offset: 0x000B4EB3
		// (set) Token: 0x0600446D RID: 17517 RVA: 0x000B6CBB File Offset: 0x000B4EBB
		[DataMember]
		public string TransactionID
		{
			get
			{
				return this.TransactionIDField;
			}
			set
			{
				this.TransactionIDField = value;
			}
		}

		// Token: 0x040039F7 RID: 14839
		private ExtensionDataObject extensionDataField;

		// Token: 0x040039F8 RID: 14840
		private string DiagnosticsField;

		// Token: 0x040039F9 RID: 14841
		private string TransactionIDField;
	}
}
