using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace schemas.microsoft.com.O365Filtering.GlobalLocatorService.Data
{
	// Token: 0x02000C24 RID: 3108
	[KnownType(typeof(DIFindTenantResponse))]
	[KnownType(typeof(DIFindDomainsResponse))]
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "DIResponseBase", Namespace = "http://schemas.microsoft.com/O365Filtering/GlobalLocatorService/Data")]
	public class DIResponseBase : IExtensibleDataObject
	{
		// Token: 0x17001118 RID: 4376
		// (get) Token: 0x06004423 RID: 17443 RVA: 0x000B6A4C File Offset: 0x000B4C4C
		// (set) Token: 0x06004424 RID: 17444 RVA: 0x000B6A54 File Offset: 0x000B4C54
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

		// Token: 0x17001119 RID: 4377
		// (get) Token: 0x06004425 RID: 17445 RVA: 0x000B6A5D File Offset: 0x000B4C5D
		// (set) Token: 0x06004426 RID: 17446 RVA: 0x000B6A65 File Offset: 0x000B4C65
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

		// Token: 0x040039D3 RID: 14803
		private ExtensionDataObject extensionDataField;

		// Token: 0x040039D4 RID: 14804
		private string DiagnosticsField;
	}
}
