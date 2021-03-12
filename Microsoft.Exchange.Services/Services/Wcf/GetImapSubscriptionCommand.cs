using System;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Management.Aggregation;
using Microsoft.Exchange.Management.PSDirectInvoke;
using Microsoft.Exchange.Services.Core.DataConverter;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Wcf.Types;
using Microsoft.Exchange.Transport.Sync.Common.Subscription.Imap;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x0200099D RID: 2461
	internal sealed class GetImapSubscriptionCommand : SingleCmdletCommandBase<IdentityRequest, GetImapSubscriptionResponse, GetImapSubscription, IMAPSubscriptionProxy>
	{
		// Token: 0x06004637 RID: 17975 RVA: 0x000F75DD File Offset: 0x000F57DD
		public GetImapSubscriptionCommand(CallContext callContext, IdentityRequest request) : base(callContext, request, "Get-ImapSubscription", ScopeLocation.RecipientRead)
		{
		}

		// Token: 0x06004638 RID: 17976 RVA: 0x000F75F0 File Offset: 0x000F57F0
		protected override void PopulateTaskParameters()
		{
			GetImapSubscription task = this.cmdletRunner.TaskWrapper.Task;
			this.cmdletRunner.SetTaskParameter("Identity", task, new AggregationSubscriptionIdParameter(this.request.Identity));
		}

		// Token: 0x06004639 RID: 17977 RVA: 0x000F7630 File Offset: 0x000F5830
		protected override void PopulateResponseData(GetImapSubscriptionResponse response)
		{
			IMAPSubscriptionProxy result = this.cmdletRunner.TaskWrapper.Result;
			if (result == null)
			{
				response.ImapSubscription = null;
				return;
			}
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

		// Token: 0x0600463A RID: 17978 RVA: 0x000F777B File Offset: 0x000F597B
		protected override PSLocalTask<GetImapSubscription, IMAPSubscriptionProxy> InvokeCmdletFactory()
		{
			return CmdletTaskFactory.Instance.CreateGetImapSubscriptionTask(base.CallContext.AccessingPrincipal);
		}
	}
}
