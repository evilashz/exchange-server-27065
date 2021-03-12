using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Extensions;
using Microsoft.Exchange.Management.Aggregation;
using Microsoft.Exchange.Management.PSDirectInvoke;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Wcf.Types;
using Microsoft.Exchange.Transport.Sync.Common.Subscription.Imap;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x020009B1 RID: 2481
	internal sealed class SetImapSubscriptionCommand : SingleCmdletCommandBase<SetImapSubscriptionRequest, OptionsResponseBase, SetImapSubscription, IMAPSubscriptionProxy>
	{
		// Token: 0x0600468C RID: 18060 RVA: 0x000FA1F1 File Offset: 0x000F83F1
		public SetImapSubscriptionCommand(CallContext callContext, SetImapSubscriptionRequest request) : base(callContext, request, "Set-ImapSubscription", ScopeLocation.RecipientWrite)
		{
		}

		// Token: 0x0600468D RID: 18061 RVA: 0x000FA204 File Offset: 0x000F8404
		protected override void PopulateTaskParameters()
		{
			SetImapSubscription task = this.cmdletRunner.TaskWrapper.Task;
			SetImapSubscriptionData imapSubscription = this.request.ImapSubscription;
			this.cmdletRunner.SetTaskParameterIfModified("EmailAddress", imapSubscription, task, (imapSubscription.EmailAddress == null) ? ((SmtpAddress)null) : ((SmtpAddress)imapSubscription.EmailAddress));
			this.cmdletRunner.SetTaskParameterIfModified("Identity", imapSubscription, task, (imapSubscription.Identity == null) ? null : new AggregationSubscriptionIdParameter(imapSubscription.Identity));
			this.cmdletRunner.SetTaskParameterIfModified("IncomingPassword", imapSubscription, task, (imapSubscription.IncomingPassword == null) ? null : imapSubscription.IncomingPassword.ConvertToSecureString());
			this.cmdletRunner.SetTaskParameterIfModified("IncomingServer", imapSubscription, task, (imapSubscription.IncomingServer == null) ? null : new Fqdn(imapSubscription.IncomingServer));
			this.cmdletRunner.SetTaskParameterIfModified("ResendVerification", imapSubscription, task, new SwitchParameter(imapSubscription.ResendVerification));
			this.cmdletRunner.SetRemainingModifiedTaskParameters(imapSubscription, task);
		}

		// Token: 0x0600468E RID: 18062 RVA: 0x000FA30D File Offset: 0x000F850D
		protected override PSLocalTask<SetImapSubscription, IMAPSubscriptionProxy> InvokeCmdletFactory()
		{
			return CmdletTaskFactory.Instance.CreateSetImapSubscriptionTask(base.CallContext.AccessingPrincipal);
		}
	}
}
