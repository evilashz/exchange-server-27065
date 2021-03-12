using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.ServiceModel;
using Microsoft.com.IPC.WSService;

// Token: 0x02000A05 RID: 2565
[DebuggerStepThrough]
[GeneratedCode("System.ServiceModel", "3.0.0.0")]
[MessageContract(WrapperName = "CertifyResponseMessage", WrapperNamespace = "http://microsoft.com/IPC/WSCertificationService", IsWrapped = true)]
public class CertifyResponseMessage
{
	// Token: 0x060037ED RID: 14317 RVA: 0x0008D562 File Offset: 0x0008B762
	public CertifyResponseMessage()
	{
	}

	// Token: 0x060037EE RID: 14318 RVA: 0x0008D56A File Offset: 0x0008B76A
	public CertifyResponseMessage(VersionData VersionData, XrmlCertificateChain GroupIdentityCredential)
	{
		this.VersionData = VersionData;
		this.GroupIdentityCredential = GroupIdentityCredential;
	}

	// Token: 0x04002F5F RID: 12127
	[MessageHeader(Namespace = "http://microsoft.com/IPC/WSService")]
	public VersionData VersionData;

	// Token: 0x04002F60 RID: 12128
	[MessageBodyMember(Namespace = "http://microsoft.com/IPC/WSCertificationService", Order = 0)]
	public XrmlCertificateChain GroupIdentityCredential;
}
