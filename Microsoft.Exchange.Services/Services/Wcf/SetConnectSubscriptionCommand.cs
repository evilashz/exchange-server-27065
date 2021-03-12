using System;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Management.Aggregation;
using Microsoft.Exchange.Management.PSDirectInvoke;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Wcf.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x020009C1 RID: 2497
	internal sealed class SetConnectSubscriptionCommand : ConnectSubscriptionCommandBase<SetConnectSubscriptionRequest, SetConnectSubscriptionResponse, SetConnectSubscription, object>
	{
		// Token: 0x060046CB RID: 18123 RVA: 0x000FBB82 File Offset: 0x000F9D82
		public SetConnectSubscriptionCommand(CallContext callContext, SetConnectSubscriptionRequest request) : base(callContext, request, "Set-ConnectSubscription", ScopeLocation.RecipientWrite)
		{
		}

		// Token: 0x060046CC RID: 18124 RVA: 0x000FBB94 File Offset: 0x000F9D94
		protected override void PopulateTaskParameters()
		{
			SetConnectSubscription task = this.cmdletRunner.TaskWrapper.Task;
			SetConnectSubscriptionData connectSubscription = this.request.ConnectSubscription;
			this.cmdletRunner.SetTaskParameterIfModified("Identity", connectSubscription, task, new AggregationSubscriptionIdParameter(connectSubscription.Identity));
			base.PopulateTaskParametersFromConnectSubscription(connectSubscription);
		}

		// Token: 0x060046CD RID: 18125 RVA: 0x000FBBE2 File Offset: 0x000F9DE2
		protected override PSLocalTask<SetConnectSubscription, object> InvokeCmdletFactory()
		{
			return CmdletTaskFactory.Instance.CreateSetConnectSubscriptionTask(base.CallContext.AccessingPrincipal, base.CalculateParameterSet(this.request.ConnectSubscription.ConnectSubscriptionType));
		}
	}
}
