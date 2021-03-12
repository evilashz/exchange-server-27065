using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Management.Aggregation;
using Microsoft.Exchange.Management.PSDirectInvoke;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Wcf.Types;
using Microsoft.Exchange.Transport.Sync.Common.Subscription.Connect;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x020009BC RID: 2492
	internal sealed class GetConnectSubscriptionCommand : ConnectSubscriptionCommandBase<GetConnectSubscriptionRequest, GetConnectSubscriptionResponse, GetConnectSubscription, ConnectSubscriptionProxy>
	{
		// Token: 0x060046B3 RID: 18099 RVA: 0x000FB58A File Offset: 0x000F978A
		public GetConnectSubscriptionCommand(CallContext callContext, GetConnectSubscriptionRequest request) : base(callContext, request, "Get-ConnectSubscription", ScopeLocation.RecipientRead)
		{
		}

		// Token: 0x060046B4 RID: 18100 RVA: 0x000FB59C File Offset: 0x000F979C
		protected override void PopulateTaskParameters()
		{
			GetConnectSubscription task = this.cmdletRunner.TaskWrapper.Task;
			if (this.request.Identity != null)
			{
				this.cmdletRunner.SetTaskParameter("Identity", task, new AggregationSubscriptionIdParameter(this.request.Identity));
			}
			this.cmdletRunner.SetTaskParameter("Mailbox", task, new MailboxIdParameter(base.CallContext.AccessingPrincipal.ObjectId));
		}

		// Token: 0x060046B5 RID: 18101 RVA: 0x000FB620 File Offset: 0x000F9820
		protected override void PopulateResponseData(GetConnectSubscriptionResponse response)
		{
			IEnumerable<ConnectSubscriptionProxy> allResults = this.cmdletRunner.TaskWrapper.AllResults;
			IEnumerable<ConnectSubscription> source = from e in allResults
			select base.CreateConnectSubscriptionData(e);
			response.ConnectSubscriptionCollection.ConnectSubscriptions = source.ToArray<ConnectSubscription>();
		}

		// Token: 0x060046B6 RID: 18102 RVA: 0x000FB662 File Offset: 0x000F9862
		protected override PSLocalTask<GetConnectSubscription, ConnectSubscriptionProxy> InvokeCmdletFactory()
		{
			return CmdletTaskFactory.Instance.CreateGetConnectSubscriptionTask(base.CallContext.AccessingPrincipal);
		}
	}
}
