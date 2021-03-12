using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.ServiceModel;
using Microsoft.com.IPC.WSService;

// Token: 0x02000A04 RID: 2564
[DebuggerStepThrough]
[MessageContract(WrapperName = "CertifyRequestMessage", WrapperNamespace = "http://microsoft.com/IPC/WSCertificationService", IsWrapped = true)]
[GeneratedCode("System.ServiceModel", "3.0.0.0")]
public class CertifyRequestMessage
{
	// Token: 0x060037EB RID: 14315 RVA: 0x0008D544 File Offset: 0x0008B744
	public CertifyRequestMessage()
	{
	}

	// Token: 0x060037EC RID: 14316 RVA: 0x0008D54C File Offset: 0x0008B74C
	public CertifyRequestMessage(VersionData VersionData, XrmlCertificateChain MachineCertificate)
	{
		this.VersionData = VersionData;
		this.MachineCertificate = MachineCertificate;
	}

	// Token: 0x04002F5D RID: 12125
	[MessageHeader(Namespace = "http://microsoft.com/IPC/WSService")]
	public VersionData VersionData;

	// Token: 0x04002F5E RID: 12126
	[MessageBodyMember(Namespace = "http://microsoft.com/IPC/WSCertificationService", Order = 0)]
	public XrmlCertificateChain MachineCertificate;
}
