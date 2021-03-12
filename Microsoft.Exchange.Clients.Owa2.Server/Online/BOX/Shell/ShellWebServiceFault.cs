using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.BOX.Shell
{
	// Token: 0x02000077 RID: 119
	[DebuggerStepThrough]
	[DataContract(Name = "ShellWebServiceFault", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.BOX.Shell")]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	public class ShellWebServiceFault : IExtensibleDataObject
	{
		// Token: 0x17000137 RID: 311
		// (get) Token: 0x06000410 RID: 1040 RVA: 0x0000E13B File Offset: 0x0000C33B
		// (set) Token: 0x06000411 RID: 1041 RVA: 0x0000E143 File Offset: 0x0000C343
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

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x06000412 RID: 1042 RVA: 0x0000E14C File Offset: 0x0000C34C
		// (set) Token: 0x06000413 RID: 1043 RVA: 0x0000E154 File Offset: 0x0000C354
		[DataMember]
		public FaultCode FaultCode
		{
			get
			{
				return this.FaultCodeField;
			}
			set
			{
				this.FaultCodeField = value;
			}
		}

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x06000414 RID: 1044 RVA: 0x0000E15D File Offset: 0x0000C35D
		// (set) Token: 0x06000415 RID: 1045 RVA: 0x0000E165 File Offset: 0x0000C365
		[DataMember]
		public string Message
		{
			get
			{
				return this.MessageField;
			}
			set
			{
				this.MessageField = value;
			}
		}

		// Token: 0x0400020F RID: 527
		private ExtensionDataObject extensionDataField;

		// Token: 0x04000210 RID: 528
		private FaultCode FaultCodeField;

		// Token: 0x04000211 RID: 529
		private string MessageField;
	}
}
