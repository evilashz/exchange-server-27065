using System;
using System.Management.Automation;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x02000033 RID: 51
	[Cmdlet("Update", "DistributionGroupMember", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
	public sealed class UpdateDistributionGroupMember : UpdateDistributionGroupMemberBase
	{
	}
}
