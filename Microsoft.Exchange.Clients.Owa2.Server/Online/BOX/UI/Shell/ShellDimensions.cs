using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.BOX.UI.Shell
{
	// Token: 0x0200007E RID: 126
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "ShellDimensions", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.BOX.UI.Shell")]
	[DebuggerStepThrough]
	public class ShellDimensions : IExtensibleDataObject
	{
		// Token: 0x17000186 RID: 390
		// (get) Token: 0x060004B2 RID: 1202 RVA: 0x0000E69A File Offset: 0x0000C89A
		// (set) Token: 0x060004B3 RID: 1203 RVA: 0x0000E6A2 File Offset: 0x0000C8A2
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

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x060004B4 RID: 1204 RVA: 0x0000E6AB File Offset: 0x0000C8AB
		// (set) Token: 0x060004B5 RID: 1205 RVA: 0x0000E6B3 File Offset: 0x0000C8B3
		[DataMember]
		public int Top
		{
			get
			{
				return this.TopField;
			}
			set
			{
				this.TopField = value;
			}
		}

		// Token: 0x04000290 RID: 656
		private ExtensionDataObject extensionDataField;

		// Token: 0x04000291 RID: 657
		private int TopField;
	}
}
