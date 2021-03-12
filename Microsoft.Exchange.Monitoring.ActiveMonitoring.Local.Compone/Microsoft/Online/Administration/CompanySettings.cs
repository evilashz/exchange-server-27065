using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration
{
	// Token: 0x020003D3 RID: 979
	[DataContract(Name = "CompanySettings", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration")]
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	public class CompanySettings : IExtensibleDataObject
	{
		// Token: 0x17000539 RID: 1337
		// (get) Token: 0x060017D3 RID: 6099 RVA: 0x0008CBFB File Offset: 0x0008ADFB
		// (set) Token: 0x060017D4 RID: 6100 RVA: 0x0008CC03 File Offset: 0x0008AE03
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

		// Token: 0x1700053A RID: 1338
		// (get) Token: 0x060017D5 RID: 6101 RVA: 0x0008CC0C File Offset: 0x0008AE0C
		// (set) Token: 0x060017D6 RID: 6102 RVA: 0x0008CC14 File Offset: 0x0008AE14
		[DataMember]
		public bool? SelfServePasswordResetEnabled
		{
			get
			{
				return this.SelfServePasswordResetEnabledField;
			}
			set
			{
				this.SelfServePasswordResetEnabledField = value;
			}
		}

		// Token: 0x040010D3 RID: 4307
		private ExtensionDataObject extensionDataField;

		// Token: 0x040010D4 RID: 4308
		private bool? SelfServePasswordResetEnabledField;
	}
}
