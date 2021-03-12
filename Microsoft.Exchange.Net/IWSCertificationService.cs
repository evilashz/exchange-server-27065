using System;
using System.CodeDom.Compiler;
using System.ServiceModel;
using Microsoft.com.IPC.WSService;

// Token: 0x02000A00 RID: 2560
[ServiceContract(Namespace = "http://microsoft.com/IPC/WSCertificationService", ConfigurationName = "IWSCertificationService")]
[GeneratedCode("System.ServiceModel", "3.0.0.0")]
public interface IWSCertificationService
{
	// Token: 0x060037D3 RID: 14291
	[FaultContract(typeof(ActiveFederationFault), Action = "http://microsoft.com/IPC/WSCertificationService/IWSCertificationService/CertifyActiveFederationFaultFault", Name = "ActiveFederationFault", Namespace = "http://microsoft.com/IPC/WSService")]
	[OperationContract(Action = "http://microsoft.com/IPC/WSCertificationService/IWSCertificationService/Certify", ReplyAction = "http://microsoft.com/IPC/WSCertificationService/IWSCertificationService/CertifyResponse")]
	CertifyResponseMessage Certify(CertifyRequestMessage request);

	// Token: 0x060037D4 RID: 14292
	[OperationContract(AsyncPattern = true, Action = "http://microsoft.com/IPC/WSCertificationService/IWSCertificationService/Certify", ReplyAction = "http://microsoft.com/IPC/WSCertificationService/IWSCertificationService/CertifyResponse")]
	IAsyncResult BeginCertify(CertifyRequestMessage request, AsyncCallback callback, object asyncState);

	// Token: 0x060037D5 RID: 14293
	CertifyResponseMessage EndCertify(IAsyncResult result);
}
