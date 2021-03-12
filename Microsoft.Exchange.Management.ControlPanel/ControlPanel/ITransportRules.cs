using System;
using System.ServiceModel;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200045B RID: 1115
	[ServiceContract(Namespace = "ECP", Name = "TransportRules")]
	public interface ITransportRules : IDataSourceService<TransportRuleFilter, RuleRow, TransportRule, SetTransportRule, NewTransportRule>, IDataSourceService<TransportRuleFilter, RuleRow, TransportRule, SetTransportRule, NewTransportRule, BaseWebServiceParameters>, IEditListService<TransportRuleFilter, RuleRow, TransportRule, NewTransportRule, BaseWebServiceParameters>, IGetListService<TransportRuleFilter, RuleRow>, INewObjectService<RuleRow, NewTransportRule>, IRemoveObjectsService<BaseWebServiceParameters>, IEditObjectForListService<TransportRule, SetTransportRule, RuleRow>, IGetObjectService<TransportRule>, IGetObjectForListService<RuleRow>
	{
		// Token: 0x060038E1 RID: 14561
		[OperationContract]
		PowerShellResults IncreasePriority(Identity[] identities, BaseWebServiceParameters parameters);

		// Token: 0x060038E2 RID: 14562
		[OperationContract]
		PowerShellResults DecreasePriority(Identity[] identities, BaseWebServiceParameters parameters);

		// Token: 0x060038E3 RID: 14563
		[OperationContract]
		PowerShellResults<RuleRow> DisableRule(Identity[] identities, BaseWebServiceParameters parameters);

		// Token: 0x060038E4 RID: 14564
		[OperationContract]
		PowerShellResults<RuleRow> EnableRule(Identity[] identities, BaseWebServiceParameters parameters);
	}
}
