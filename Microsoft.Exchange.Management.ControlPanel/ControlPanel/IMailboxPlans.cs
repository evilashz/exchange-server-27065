using System;
using System.ServiceModel;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000511 RID: 1297
	[ServiceContract(Namespace = "ECP", Name = "MailboxPlans")]
	public interface IMailboxPlans : IGetListService<MailboxPlanFilter, MailboxPlan>
	{
	}
}
