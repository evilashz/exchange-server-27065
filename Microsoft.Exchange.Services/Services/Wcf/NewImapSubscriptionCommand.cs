using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Extensions;
using Microsoft.Exchange.Management.Aggregation;
using Microsoft.Exchange.Management.PSDirectInvoke;
using Microsoft.Exchange.Services.Core.DataConverter;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Wcf.Types;
using Microsoft.Exchange.Transport.Sync.Common.Subscription.Imap;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x020009A8 RID: 2472
	internal sealed class NewImapSubscriptionCommand : SingleCmdletCommandBase<NewImapSubscriptionRequest, NewImapSubscriptionResponse, NewImapSubscription, IMAPSubscriptionProxy>
	{
		// Token: 0x06004666 RID: 18022 RVA: 0x000F8969 File Offset: 0x000F6B69
		public NewImapSubscriptionCommand(CallContext callContext, NewImapSubscriptionRequest request) : base(callContext, request, "New-ImapSubscription", ScopeLocation.RecipientWrite)
		{
		}

		// Token: 0x06004667 RID: 18023 RVA: 0x000F897C File Offset: 0x000F6B7C
		protected override void PopulateTaskParameters()
		{
			NewImapSubscription task = this.cmdletRunner.TaskWrapper.Task;
			NewImapSubscriptionData imapSubscription = this.request.ImapSubscription;
			this.cmdletRunner.SetTaskParameterIfModified("EmailAddress", imapSubscription, task, (imapSubscription.EmailAddress == null) ? ((SmtpAddress)null) : ((SmtpAddress)imapSubscription.EmailAddress));
			this.cmdletRunner.SetTaskParameterIfModified("IncomingPassword", imapSubscription, task, (imapSubscription.IncomingPassword == null) ? null : imapSubscription.IncomingPassword.ConvertToSecureString());
			this.cmdletRunner.SetTaskParameterIfModified("IncomingServer", imapSubscription, task, (imapSubscription.IncomingServer == null) ? null : new Fqdn(imapSubscription.IncomingServer));
			this.cmdletRunner.SetRemainingModifiedTaskParameters(imapSubscription, task);
		}

		// Token: 0x06004668 RID: 18024 RVA: 0x000F8A38 File Offset: 0x000F6C38
		protected override void PopulateResponseData(NewImapSubscriptionResponse response)
		{
			IMAPSubscriptionProxy result = this.cmdletRunner.TaskWrapper.Result;
			response.ImapSubscription = new ImapSubscription
			{
				DetailedStatus = result.DetailedStatus,
				DisplayName = result.DisplayName,
				EmailAddress = result.EmailAddress.ToString(),
				Identity = new Identity(result.Identity.ToString(), result.DisplayName),
				IncomingAuth = result.IncomingAuthentication,
				IncomingPort = result.IncomingPort,
				IncomingSecurity = result.IncomingSecurity,
				IncomingServer = result.IncomingServer,
				IncomingUserName = result.IncomingUserName,
				IsErrorStatus = result.IsErrorStatus,
				IsValid = result.IsValid,
				LastSuccessfulSync = ((result.LastSuccessfulSync == null) ? null : ExDateTimeConverter.ToSoapHeaderTimeZoneRelatedXsdDateTime((ExDateTime)result.LastSuccessfulSync.Value)),
				Name = result.Name,
				SendAsState = result.SendAsState,
				Status = result.Status,
				StatusDescription = result.StatusDescription,
				SubscriptionType = result.SubscriptionType
			};
		}

		// Token: 0x06004669 RID: 18025 RVA: 0x000F8B78 File Offset: 0x000F6D78
		protected override PSLocalTask<NewImapSubscription, IMAPSubscriptionProxy> InvokeCmdletFactory()
		{
			return CmdletTaskFactory.Instance.CreateNewImapSubscriptionTask(base.CallContext.AccessingPrincipal);
		}
	}
}
