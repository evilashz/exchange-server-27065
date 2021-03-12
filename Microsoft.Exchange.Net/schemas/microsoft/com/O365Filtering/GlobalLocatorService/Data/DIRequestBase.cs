using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace schemas.microsoft.com.O365Filtering.GlobalLocatorService.Data
{
	// Token: 0x02000C21 RID: 3105
	[DataContract(Name = "DIRequestBase", Namespace = "http://schemas.microsoft.com/O365Filtering/GlobalLocatorService/Data")]
	[DebuggerStepThrough]
	[KnownType(typeof(DIFindTenantRequest))]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[KnownType(typeof(DIFindDomainsRequest))]
	public class DIRequestBase : IExtensibleDataObject
	{
		// Token: 0x17001112 RID: 4370
		// (get) Token: 0x06004414 RID: 17428 RVA: 0x000B69CE File Offset: 0x000B4BCE
		// (set) Token: 0x06004415 RID: 17429 RVA: 0x000B69D6 File Offset: 0x000B4BD6
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

		// Token: 0x17001113 RID: 4371
		// (get) Token: 0x06004416 RID: 17430 RVA: 0x000B69DF File Offset: 0x000B4BDF
		// (set) Token: 0x06004417 RID: 17431 RVA: 0x000B69E7 File Offset: 0x000B4BE7
		[DataMember(IsRequired = true)]
		public string[] Namespaces
		{
			get
			{
				return this.NamespacesField;
			}
			set
			{
				this.NamespacesField = value;
			}
		}

		// Token: 0x040039CD RID: 14797
		private ExtensionDataObject extensionDataField;

		// Token: 0x040039CE RID: 14798
		private string[] NamespacesField;
	}
}
