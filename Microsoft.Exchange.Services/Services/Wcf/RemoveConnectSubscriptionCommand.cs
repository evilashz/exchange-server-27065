using System;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Management.Aggregation;
using Microsoft.Exchange.Management.PSDirectInvoke;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Wcf.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x020009C0 RID: 2496
	internal sealed class RemoveConnectSubscriptionCommand : SingleCmdletCommandBase<RemoveConnectSubscriptionRequest, RemoveConnectSubscriptionResponse, RemoveConnectSubscription, object>
	{
		// Token: 0x060046C8 RID: 18120 RVA: 0x000FBB1C File Offset: 0x000F9D1C
		public RemoveConnectSubscriptionCommand(CallContext callContext, RemoveConnectSubscriptionRequest request) : base(callContext, request, "Remove-ConnectSubscription", ScopeLocation.RecipientWrite)
		{
		}

		// Token: 0x060046C9 RID: 18121 RVA: 0x000FBB2C File Offset: 0x000F9D2C
		protected override void PopulateTaskParameters()
		{
			RemoveConnectSubscription task = this.cmdletRunner.TaskWrapper.Task;
			this.cmdletRunner.SetTaskParameter("Identity", task, new AggregationSubscriptionIdParameter(this.request.Identity));
		}

		// Token: 0x060046CA RID: 18122 RVA: 0x000FBB6B File Offset: 0x000F9D6B
		protected override PSLocalTask<RemoveConnectSubscription, object> InvokeCmdletFactory()
		{
			return CmdletTaskFactory.Instance.CreateRemoveConnectSubscriptionTask(base.CallContext.AccessingPrincipal);
		}
	}
}
