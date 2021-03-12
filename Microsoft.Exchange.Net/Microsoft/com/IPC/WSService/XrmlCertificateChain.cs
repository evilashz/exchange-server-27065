using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.com.IPC.WSService
{
	// Token: 0x020009FD RID: 2557
	[GeneratedCode("System.Runtime.Serialization", "3.0.0.0")]
	[DataContract(Name = "XrmlCertificateChain", Namespace = "http://microsoft.com/IPC/WSService")]
	[DebuggerStepThrough]
	public class XrmlCertificateChain : IExtensibleDataObject
	{
		// Token: 0x17000E31 RID: 3633
		// (get) Token: 0x060037C7 RID: 14279 RVA: 0x0008D21C File Offset: 0x0008B41C
		// (set) Token: 0x060037C8 RID: 14280 RVA: 0x0008D224 File Offset: 0x0008B424
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

		// Token: 0x17000E32 RID: 3634
		// (get) Token: 0x060037C9 RID: 14281 RVA: 0x0008D22D File Offset: 0x0008B42D
		// (set) Token: 0x060037CA RID: 14282 RVA: 0x0008D235 File Offset: 0x0008B435
		[DataMember]
		public string[] CertificateChain
		{
			get
			{
				return this.CertificateChainField;
			}
			set
			{
				this.CertificateChainField = value;
			}
		}

		// Token: 0x04002F3D RID: 12093
		private ExtensionDataObject extensionDataField;

		// Token: 0x04002F3E RID: 12094
		private string[] CertificateChainField;
	}
}
