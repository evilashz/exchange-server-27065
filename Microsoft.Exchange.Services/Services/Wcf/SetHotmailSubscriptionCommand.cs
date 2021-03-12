using System;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Extensions;
using Microsoft.Exchange.Management.Aggregation;
using Microsoft.Exchange.Management.PSDirectInvoke;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Wcf.Types;
using Microsoft.Exchange.Transport.Sync.Common.Subscription.DeltaSync;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x020009B0 RID: 2480
	internal sealed class SetHotmailSubscriptionCommand : SingleCmdletCommandBase<SetHotmailSubscriptionRequest, OptionsResponseBase, SetHotmailSubscription, HotmailSubscriptionProxy>
	{
		// Token: 0x06004689 RID: 18057 RVA: 0x000FA148 File Offset: 0x000F8348
		public SetHotmailSubscriptionCommand(CallContext callContext, SetHotmailSubscriptionRequest request) : base(callContext, request, "Set-HotmailSubscription", ScopeLocation.RecipientWrite)
		{
		}

		// Token: 0x0600468A RID: 18058 RVA: 0x000FA158 File Offset: 0x000F8358
		protected override void PopulateTaskParameters()
		{
			SetHotmailSubscription task = this.cmdletRunner.TaskWrapper.Task;
			SetHotmailSubscriptionData hotmailSubscription = this.request.HotmailSubscription;
			this.cmdletRunner.SetTaskParameterIfModified("Identity", hotmailSubscription, task, (hotmailSubscription.Identity == null) ? null : new AggregationSubscriptionIdParameter(hotmailSubscription.Identity));
			this.cmdletRunner.SetTaskParameterIfModified("Password", hotmailSubscription, task, hotmailSubscription.Password.ConvertToSecureString());
			this.cmdletRunner.SetRemainingModifiedTaskParameters(hotmailSubscription, task);
		}

		// Token: 0x0600468B RID: 18059 RVA: 0x000FA1DA File Offset: 0x000F83DA
		protected override PSLocalTask<SetHotmailSubscription, HotmailSubscriptionProxy> InvokeCmdletFactory()
		{
			return CmdletTaskFactory.Instance.CreateSetHotmailSubscriptionTask(base.CallContext.AccessingPrincipal);
		}

		// Token: 0x040028AB RID: 10411
		public const string PasswordTaskPropertyName = "Password";
	}
}
