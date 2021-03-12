using System;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Management.Aggregation;
using Microsoft.Exchange.Management.PSDirectInvoke;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Wcf.Types;
using Microsoft.Exchange.Transport.Sync.Common.Subscription.Pim;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x020009AE RID: 2478
	internal sealed class RemoveSubscriptionCommand : SingleCmdletCommandBase<IdentityRequest, OptionsResponseBase, RemoveSubscription, PimSubscriptionProxy>
	{
		// Token: 0x06004681 RID: 18049 RVA: 0x000F9E92 File Offset: 0x000F8092
		public RemoveSubscriptionCommand(CallContext callContext, IdentityRequest request) : base(callContext, request, "Remove-Subscription", ScopeLocation.RecipientWrite)
		{
		}

		// Token: 0x06004682 RID: 18050 RVA: 0x000F9EA4 File Offset: 0x000F80A4
		protected override void PopulateTaskParameters()
		{
			RemoveSubscription task = this.cmdletRunner.TaskWrapper.Task;
			this.cmdletRunner.SetTaskParameter("Identity", task, new AggregationSubscriptionIdParameter(this.request.Identity));
		}

		// Token: 0x06004683 RID: 18051 RVA: 0x000F9EE3 File Offset: 0x000F80E3
		protected override PSLocalTask<RemoveSubscription, PimSubscriptionProxy> InvokeCmdletFactory()
		{
			return CmdletTaskFactory.Instance.CreateRemoveSubscriptionTask(base.CallContext.AccessingPrincipal);
		}
	}
}
