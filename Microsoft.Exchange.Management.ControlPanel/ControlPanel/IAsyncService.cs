using System;
using System.ServiceModel;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000253 RID: 595
	[ServiceContract(Namespace = "ECP", Name = "IAsyncService")]
	public interface IAsyncService
	{
		// Token: 0x060028AB RID: 10411
		[OperationContract]
		PowerShellResults<JsonDictionary<object>> GetProgress(string progressId);
	}
}
