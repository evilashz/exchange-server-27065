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
using Microsoft.Exchange.Transport.Sync.Common.Subscription.Pop;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x020009AA RID: 2474
	internal sealed class NewPopSubscriptionCommand : SingleCmdletCommandBase<NewPopSubscriptionRequest, NewPopSubscriptionResponse, NewPopSubscription, PopSubscriptionProxy>
	{
		// Token: 0x0600466E RID: 18030 RVA: 0x000F9818 File Offset: 0x000F7A18
		public NewPopSubscriptionCommand(CallContext callContext, NewPopSubscriptionRequest request) : base(callContext, request, "New-PopSubscription", ScopeLocation.RecipientWrite)
		{
		}

		// Token: 0x0600466F RID: 18031 RVA: 0x000F9828 File Offset: 0x000F7A28
		protected override void PopulateTaskParameters()
		{
			NewPopSubscription task = this.cmdletRunner.TaskWrapper.Task;
			NewPopSubscriptionData popSubscription = this.request.PopSubscription;
			this.cmdletRunner.SetTaskParameterIfModified("EmailAddress", popSubscription, task, (popSubscription.EmailAddress == null) ? ((SmtpAddress)null) : ((SmtpAddress)popSubscription.EmailAddress));
			this.cmdletRunner.SetTaskParameterIfModified("IncomingPassword", popSubscription, task, (popSubscription.IncomingPassword == null) ? null : popSubscription.IncomingPassword.ConvertToSecureString());
			this.cmdletRunner.SetTaskParameterIfModified("IncomingServer", popSubscription, task, (popSubscription.IncomingServer == null) ? null : new Fqdn(popSubscription.IncomingServer));
			this.cmdletRunner.SetRemainingModifiedTaskParameters(popSubscription, task);
		}

		// Token: 0x06004670 RID: 18032 RVA: 0x000F98E4 File Offset: 0x000F7AE4
		protected override void PopulateResponseData(NewPopSubscriptionResponse response)
		{
			PopSubscriptionProxy result = this.cmdletRunner.TaskWrapper.Result;
			response.PopSubscription = new PopSubscription
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
				LeaveOnServer = result.LeaveOnServer,
				Name = result.Name,
				SendAsState = result.SendAsState,
				Status = result.Status,
				StatusDescription = result.StatusDescription,
				SubscriptionType = result.SubscriptionType
			};
		}

		// Token: 0x06004671 RID: 18033 RVA: 0x000F9A30 File Offset: 0x000F7C30
		protected override PSLocalTask<NewPopSubscription, PopSubscriptionProxy> InvokeCmdletFactory()
		{
			return CmdletTaskFactory.Instance.CreateNewPopSubscriptionTask(base.CallContext.AccessingPrincipal);
		}
	}
}
