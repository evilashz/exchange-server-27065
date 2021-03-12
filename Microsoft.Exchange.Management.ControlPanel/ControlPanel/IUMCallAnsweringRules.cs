using System;
using System.ServiceModel;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200046B RID: 1131
	[ServiceContract(Namespace = "ECP", Name = "UMCallAnsweringRules")]
	public interface IUMCallAnsweringRules : IDataSourceService<UMCallAnsweringRuleFilter, RuleRow, UMCallAnsweringRule, SetUMCallAnsweringRule, NewUMCallAnsweringRule, RemoveUMCallAnsweringRule>, IEditListService<UMCallAnsweringRuleFilter, RuleRow, UMCallAnsweringRule, NewUMCallAnsweringRule, RemoveUMCallAnsweringRule>, IGetListService<UMCallAnsweringRuleFilter, RuleRow>, INewObjectService<RuleRow, NewUMCallAnsweringRule>, IRemoveObjectsService<RemoveUMCallAnsweringRule>, IEditObjectForListService<UMCallAnsweringRule, SetUMCallAnsweringRule, RuleRow>, IGetObjectService<UMCallAnsweringRule>, IGetObjectForListService<RuleRow>
	{
		// Token: 0x06003936 RID: 14646
		[OperationContract]
		PowerShellResults IncreasePriority(Identity[] identities, ChangeUMCallAnsweringRule parameters);

		// Token: 0x06003937 RID: 14647
		[OperationContract]
		PowerShellResults DecreasePriority(Identity[] identities, ChangeUMCallAnsweringRule parameters);

		// Token: 0x06003938 RID: 14648
		[OperationContract]
		PowerShellResults<RuleRow> DisableRule(Identity[] identities, DisableUMCallAnsweringRule parameters);

		// Token: 0x06003939 RID: 14649
		[OperationContract]
		PowerShellResults<RuleRow> EnableRule(Identity[] identities, EnableUMCallAnsweringRule parameters);
	}
}
