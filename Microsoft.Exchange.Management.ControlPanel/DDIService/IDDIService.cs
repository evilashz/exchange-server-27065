using System;
using System.ServiceModel;
using Microsoft.Exchange.Management.ControlPanel;

namespace Microsoft.Exchange.Management.DDIService
{
	// Token: 0x02000121 RID: 289
	[ServiceContract(Namespace = "ECP", Name = "DDIService")]
	[ServiceKnownType("GetKnownTypes", typeof(DDIService))]
	public interface IDDIService
	{
		// Token: 0x06002042 RID: 8258
		[OperationContract]
		PowerShellResults<JsonDictionary<object>> GetList(DDIParameters filter, SortOptions sort);

		// Token: 0x06002043 RID: 8259
		[OperationContract]
		PowerShellResults<JsonDictionary<object>> GetObject(Identity identity);

		// Token: 0x06002044 RID: 8260
		[OperationContract]
		PowerShellResults<JsonDictionary<object>> GetObjectOnDemand(Identity identity, string workflowName);

		// Token: 0x06002045 RID: 8261
		[OperationContract]
		PowerShellResults<JsonDictionary<object>> GetObjectForNew(Identity identity);

		// Token: 0x06002046 RID: 8262
		[OperationContract]
		PowerShellResults<JsonDictionary<object>> SetObject(Identity identity, DDIParameters properties);

		// Token: 0x06002047 RID: 8263
		[OperationContract]
		PowerShellResults<JsonDictionary<object>> NewObject(DDIParameters properties);

		// Token: 0x06002048 RID: 8264
		[OperationContract]
		PowerShellResults RemoveObjects(Identity[] identities, DDIParameters parameters);

		// Token: 0x06002049 RID: 8265
		[OperationContract]
		PowerShellResults<JsonDictionary<object>> MultiObjectExecute(Identity[] identities, DDIParameters parameters);

		// Token: 0x0600204A RID: 8266
		[OperationContract]
		PowerShellResults<JsonDictionary<object>> SingleObjectExecute(Identity identity, DDIParameters properties);

		// Token: 0x0600204B RID: 8267
		[OperationContract]
		PowerShellResults<JsonDictionary<object>> GetProgress(string progressId);

		// Token: 0x0600204C RID: 8268
		[OperationContract]
		PowerShellResults Cancel(string progressId);
	}
}
