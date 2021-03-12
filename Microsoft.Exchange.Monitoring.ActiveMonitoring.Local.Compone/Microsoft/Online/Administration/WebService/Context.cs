using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration.WebService
{
	// Token: 0x02000350 RID: 848
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DebuggerStepThrough]
	[DataContract(Name = "Context", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration.WebService")]
	public class Context : IExtensibleDataObject
	{
		// Token: 0x17000487 RID: 1159
		// (get) Token: 0x060015F8 RID: 5624 RVA: 0x0008BC71 File Offset: 0x00089E71
		// (set) Token: 0x060015F9 RID: 5625 RVA: 0x0008BC79 File Offset: 0x00089E79
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

		// Token: 0x17000488 RID: 1160
		// (get) Token: 0x060015FA RID: 5626 RVA: 0x0008BC82 File Offset: 0x00089E82
		// (set) Token: 0x060015FB RID: 5627 RVA: 0x0008BC8A File Offset: 0x00089E8A
		[DataMember]
		public byte[] DataBlob
		{
			get
			{
				return this.DataBlobField;
			}
			set
			{
				this.DataBlobField = value;
			}
		}

		// Token: 0x04000FEF RID: 4079
		private ExtensionDataObject extensionDataField;

		// Token: 0x04000FF0 RID: 4080
		private byte[] DataBlobField;
	}
}
