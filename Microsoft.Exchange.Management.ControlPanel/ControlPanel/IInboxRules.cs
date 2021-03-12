using System;
using System.ServiceModel;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000403 RID: 1027
	[ServiceContract(Namespace = "ECP", Name = "InboxRules")]
	public interface IInboxRules : IDataSourceService<InboxRuleFilter, RuleRow, InboxRule, SetInboxRule, NewInboxRule, RemoveInboxRule>, IEditListService<InboxRuleFilter, RuleRow, InboxRule, NewInboxRule, RemoveInboxRule>, IGetListService<InboxRuleFilter, RuleRow>, INewObjectService<RuleRow, NewInboxRule>, IRemoveObjectsService<RemoveInboxRule>, IEditObjectForListService<InboxRule, SetInboxRule, RuleRow>, IGetObjectService<InboxRule>, IGetObjectForListService<RuleRow>
	{
		// Token: 0x060034AB RID: 13483
		[OperationContract]
		PowerShellResults IncreasePriority(Identity[] identities, ChangeInboxRule parameters);

		// Token: 0x060034AC RID: 13484
		[OperationContract]
		PowerShellResults DecreasePriority(Identity[] identities, ChangeInboxRule parameters);

		// Token: 0x060034AD RID: 13485
		[OperationContract]
		PowerShellResults<RuleRow> DisableRule(Identity[] identities, DisableInboxRule parameters);

		// Token: 0x060034AE RID: 13486
		[OperationContract]
		PowerShellResults<RuleRow> EnableRule(Identity[] identities, EnableInboxRule parameters);
	}
}
