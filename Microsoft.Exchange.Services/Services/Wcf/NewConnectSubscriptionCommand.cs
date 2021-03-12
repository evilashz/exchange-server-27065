using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Management.Aggregation;
using Microsoft.Exchange.Management.PSDirectInvoke;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Wcf.Types;
using Microsoft.Exchange.Transport.Sync.Common.Subscription.Connect;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x020009BF RID: 2495
	internal sealed class NewConnectSubscriptionCommand : ConnectSubscriptionCommandBase<NewConnectSubscriptionRequest, NewConnectSubscriptionResponse, NewConnectSubscription, ConnectSubscriptionProxy>
	{
		// Token: 0x060046C4 RID: 18116 RVA: 0x000FBAA1 File Offset: 0x000F9CA1
		public NewConnectSubscriptionCommand(CallContext callContext, NewConnectSubscriptionRequest request) : base(callContext, request, "New-ConnectSubscription", ScopeLocation.RecipientWrite)
		{
		}

		// Token: 0x060046C5 RID: 18117 RVA: 0x000FBAB1 File Offset: 0x000F9CB1
		protected override void PopulateTaskParameters()
		{
			base.PopulateTaskParametersFromConnectSubscription(this.request.ConnectSubscription);
		}

		// Token: 0x060046C6 RID: 18118 RVA: 0x000FBAC4 File Offset: 0x000F9CC4
		protected override void PopulateResponseData(NewConnectSubscriptionResponse response)
		{
			ConnectSubscriptionProxy result = this.cmdletRunner.TaskWrapper.Result;
			response.ConnectSubscription = base.CreateConnectSubscriptionData(result);
		}

		// Token: 0x060046C7 RID: 18119 RVA: 0x000FBAEF File Offset: 0x000F9CEF
		protected override PSLocalTask<NewConnectSubscription, ConnectSubscriptionProxy> InvokeCmdletFactory()
		{
			return CmdletTaskFactory.Instance.CreateNewConnectSubscriptionTask(base.CallContext.AccessingPrincipal, base.CalculateParameterSet(this.request.ConnectSubscription.ConnectSubscriptionType));
		}
	}
}
