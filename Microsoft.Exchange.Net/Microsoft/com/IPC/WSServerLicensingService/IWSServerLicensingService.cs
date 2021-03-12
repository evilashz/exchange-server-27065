using System;
using System.CodeDom.Compiler;
using System.ServiceModel;
using Microsoft.com.IPC.WSService;

namespace Microsoft.com.IPC.WSServerLicensingService
{
	// Token: 0x02000A09 RID: 2569
	[ServiceContract(Namespace = "http://microsoft.com/IPC/WSServerLicensingService", ConfigurationName = "Microsoft.com.IPC.WSServerLicensingService.IWSServerLicensingService")]
	[GeneratedCode("System.ServiceModel", "3.0.0.0")]
	public interface IWSServerLicensingService
	{
		// Token: 0x06003806 RID: 14342
		[OperationContract(Action = "http://microsoft.com/IPC/WSServerLicensingService/IWSServerLicensingService/AcquireServerLicense", ReplyAction = "http://microsoft.com/IPC/WSServerLicensingService/IWSServerLicensingService/AcquireServerLicenseResponse")]
		[FaultContract(typeof(ActiveFederationFault), Action = "http://microsoft.com/IPC/WSServerLicensingService/IWSServerLicensingService/AcquireServerLicenseActiveFederationFaultFault", Name = "ActiveFederationFault", Namespace = "http://microsoft.com/IPC/WSService")]
		AcquireServerLicenseResponseMessage AcquireServerLicense(AcquireServerLicenseRequestMessage request);

		// Token: 0x06003807 RID: 14343
		[OperationContract(AsyncPattern = true, Action = "http://microsoft.com/IPC/WSServerLicensingService/IWSServerLicensingService/AcquireServerLicense", ReplyAction = "http://microsoft.com/IPC/WSServerLicensingService/IWSServerLicensingService/AcquireServerLicenseResponse")]
		IAsyncResult BeginAcquireServerLicense(AcquireServerLicenseRequestMessage request, AsyncCallback callback, object asyncState);

		// Token: 0x06003808 RID: 14344
		AcquireServerLicenseResponseMessage EndAcquireServerLicense(IAsyncResult result);
	}
}
