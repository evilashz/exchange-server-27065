using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration.WebService
{
	// Token: 0x02000351 RID: 849
	[DebuggerStepThrough]
	[DataContract(Name = "ContractVersionHeader", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration.WebService")]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	public class ContractVersionHeader : IExtensibleDataObject
	{
		// Token: 0x17000489 RID: 1161
		// (get) Token: 0x060015FD RID: 5629 RVA: 0x0008BC9B File Offset: 0x00089E9B
		// (set) Token: 0x060015FE RID: 5630 RVA: 0x0008BCA3 File Offset: 0x00089EA3
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

		// Token: 0x1700048A RID: 1162
		// (get) Token: 0x060015FF RID: 5631 RVA: 0x0008BCAC File Offset: 0x00089EAC
		// (set) Token: 0x06001600 RID: 5632 RVA: 0x0008BCB4 File Offset: 0x00089EB4
		[DataMember]
		public Version BecVersion
		{
			get
			{
				return this.BecVersionField;
			}
			set
			{
				this.BecVersionField = value;
			}
		}

		// Token: 0x04000FF1 RID: 4081
		private ExtensionDataObject extensionDataField;

		// Token: 0x04000FF2 RID: 4082
		private Version BecVersionField;
	}
}
