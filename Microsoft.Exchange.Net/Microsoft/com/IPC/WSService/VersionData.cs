using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.com.IPC.WSService
{
	// Token: 0x020009FC RID: 2556
	[DataContract(Name = "VersionData", Namespace = "http://microsoft.com/IPC/WSService")]
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "3.0.0.0")]
	public class VersionData : IExtensibleDataObject
	{
		// Token: 0x17000E2E RID: 3630
		// (get) Token: 0x060037C0 RID: 14272 RVA: 0x0008D1E1 File Offset: 0x0008B3E1
		// (set) Token: 0x060037C1 RID: 14273 RVA: 0x0008D1E9 File Offset: 0x0008B3E9
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

		// Token: 0x17000E2F RID: 3631
		// (get) Token: 0x060037C2 RID: 14274 RVA: 0x0008D1F2 File Offset: 0x0008B3F2
		// (set) Token: 0x060037C3 RID: 14275 RVA: 0x0008D1FA File Offset: 0x0008B3FA
		[DataMember]
		public string MinimumVersion
		{
			get
			{
				return this.MinimumVersionField;
			}
			set
			{
				this.MinimumVersionField = value;
			}
		}

		// Token: 0x17000E30 RID: 3632
		// (get) Token: 0x060037C4 RID: 14276 RVA: 0x0008D203 File Offset: 0x0008B403
		// (set) Token: 0x060037C5 RID: 14277 RVA: 0x0008D20B File Offset: 0x0008B40B
		[DataMember(Order = 1)]
		public string MaximumVersion
		{
			get
			{
				return this.MaximumVersionField;
			}
			set
			{
				this.MaximumVersionField = value;
			}
		}

		// Token: 0x04002F3A RID: 12090
		private ExtensionDataObject extensionDataField;

		// Token: 0x04002F3B RID: 12091
		private string MinimumVersionField;

		// Token: 0x04002F3C RID: 12092
		private string MaximumVersionField;
	}
}
