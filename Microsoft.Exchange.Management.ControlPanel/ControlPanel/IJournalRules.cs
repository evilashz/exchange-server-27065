using System;
using System.ServiceModel;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000415 RID: 1045
	[ServiceContract(Namespace = "ECP", Name = "JournalRules")]
	public interface IJournalRules : IDataSourceService<JournalRuleFilter, JournalRuleRow, JournalRule, SetJournalRule, NewJournalRule>, IDataSourceService<JournalRuleFilter, JournalRuleRow, JournalRule, SetJournalRule, NewJournalRule, BaseWebServiceParameters>, IEditListService<JournalRuleFilter, JournalRuleRow, JournalRule, NewJournalRule, BaseWebServiceParameters>, IGetListService<JournalRuleFilter, JournalRuleRow>, INewObjectService<JournalRuleRow, NewJournalRule>, IRemoveObjectsService<BaseWebServiceParameters>, IEditObjectForListService<JournalRule, SetJournalRule, JournalRuleRow>, IGetObjectService<JournalRule>, IGetObjectForListService<JournalRuleRow>
	{
		// Token: 0x0600351C RID: 13596
		[OperationContract]
		PowerShellResults<JournalRuleRow> DisableRule(Identity[] identities, BaseWebServiceParameters parameters);

		// Token: 0x0600351D RID: 13597
		[OperationContract]
		PowerShellResults<JournalRuleRow> EnableRule(Identity[] identities, BaseWebServiceParameters parameters);
	}
}
