using System;
using System.ServiceModel;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000364 RID: 868
	[ServiceContract(Namespace = "ECP", Name = "UMDialingRuleGroupPicker")]
	public interface IUMDialingRuleGroupPicker : IGetListService<UMDialPlanFilterWithIdentity, UMDialingRuleGroupRow>
	{
	}
}
