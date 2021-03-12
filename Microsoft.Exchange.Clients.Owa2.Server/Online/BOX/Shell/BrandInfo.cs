using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.BOX.Shell
{
	// Token: 0x02000067 RID: 103
	[DataContract(Name = "BrandInfo", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.BOX.Shell")]
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	public class BrandInfo : IExtensibleDataObject
	{
		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x06000374 RID: 884 RVA: 0x0000DC14 File Offset: 0x0000BE14
		// (set) Token: 0x06000375 RID: 885 RVA: 0x0000DC1C File Offset: 0x0000BE1C
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

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x06000376 RID: 886 RVA: 0x0000DC25 File Offset: 0x0000BE25
		// (set) Token: 0x06000377 RID: 887 RVA: 0x0000DC2D File Offset: 0x0000BE2D
		[DataMember]
		public string Id
		{
			get
			{
				return this.IdField;
			}
			set
			{
				this.IdField = value;
			}
		}

		// Token: 0x040001A5 RID: 421
		private ExtensionDataObject extensionDataField;

		// Token: 0x040001A6 RID: 422
		private string IdField;
	}
}
