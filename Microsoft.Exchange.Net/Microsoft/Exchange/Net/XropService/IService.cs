using System;
using System.ServiceModel;

namespace Microsoft.Exchange.Net.XropService
{
	// Token: 0x02000B95 RID: 2965
	[ServiceContract(Name = "XropService", Namespace = "http://schemas.microsoft.com/exchange/2010/xrop", SessionMode = SessionMode.Required)]
	[CLSCompliant(false)]
	public interface IService
	{
		// Token: 0x06003F72 RID: 16242
		[OperationContract(Action = "http://schemas.microsoft.com/exchange/2010/xrop/Connect", AsyncPattern = true, IsInitiating = true, IsTerminating = false)]
		IAsyncResult BeginConnect(ConnectRequestMessage request, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x06003F73 RID: 16243
		ConnectResponseMessage EndConnect(IAsyncResult asyncResult);

		// Token: 0x06003F74 RID: 16244
		[OperationContract(Action = "http://schemas.microsoft.com/exchange/2010/xrop/Execute", AsyncPattern = true, IsInitiating = false, IsTerminating = false)]
		IAsyncResult BeginExecute(ExecuteRequestMessage request, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x06003F75 RID: 16245
		ExecuteResponseMessage EndExecute(IAsyncResult asyncResult);

		// Token: 0x06003F76 RID: 16246
		[OperationContract(Action = "http://schemas.microsoft.com/exchange/2010/xrop/Disconnect", AsyncPattern = true, IsInitiating = false, IsTerminating = true)]
		IAsyncResult BeginDisconnect(DisconnectRequestMessage request, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x06003F77 RID: 16247
		DisconnectResponseMessage EndDisconnect(IAsyncResult asyncResult);
	}
}
