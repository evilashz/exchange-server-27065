using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;
using Microsoft.com.IPC.WSService;

namespace Microsoft.com.IPC.WSServerLicensingService
{
	// Token: 0x02000A07 RID: 2567
	[DebuggerStepThrough]
	[DataContract(Name = "BatchLicenseResponses", Namespace = "http://microsoft.com/IPC/WSServerLicensingService")]
	[GeneratedCode("System.Runtime.Serialization", "3.0.0.0")]
	public class BatchLicenseResponses : IExtensibleDataObject
	{
		// Token: 0x17000E3C RID: 3644
		// (get) Token: 0x060037F8 RID: 14328 RVA: 0x0008D5CC File Offset: 0x0008B7CC
		// (set) Token: 0x060037F9 RID: 14329 RVA: 0x0008D5D4 File Offset: 0x0008B7D4
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

		// Token: 0x17000E3D RID: 3645
		// (get) Token: 0x060037FA RID: 14330 RVA: 0x0008D5DD File Offset: 0x0008B7DD
		// (set) Token: 0x060037FB RID: 14331 RVA: 0x0008D5E5 File Offset: 0x0008B7E5
		[DataMember]
		public LicenseResponse[] LicenseResponses
		{
			get
			{
				return this.LicenseResponsesField;
			}
			set
			{
				this.LicenseResponsesField = value;
			}
		}

		// Token: 0x17000E3E RID: 3646
		// (get) Token: 0x060037FC RID: 14332 RVA: 0x0008D5EE File Offset: 0x0008B7EE
		// (set) Token: 0x060037FD RID: 14333 RVA: 0x0008D5F6 File Offset: 0x0008B7F6
		[DataMember]
		public XrmlCertificateChain ServerLicenseCertificateChain
		{
			get
			{
				return this.ServerLicenseCertificateChainField;
			}
			set
			{
				this.ServerLicenseCertificateChainField = value;
			}
		}

		// Token: 0x04002F65 RID: 12133
		private ExtensionDataObject extensionDataField;

		// Token: 0x04002F66 RID: 12134
		private LicenseResponse[] LicenseResponsesField;

		// Token: 0x04002F67 RID: 12135
		private XrmlCertificateChain ServerLicenseCertificateChainField;
	}
}
