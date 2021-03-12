using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Extensions;
using Microsoft.Exchange.Management.Aggregation;
using Microsoft.Exchange.Management.PSDirectInvoke;
using Microsoft.Exchange.Services.Core.DataConverter;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Wcf.Types;
using Microsoft.Exchange.Transport.Sync.Common.Subscription.Pim;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x020009AB RID: 2475
	internal sealed class NewSubscriptionCommand : SingleCmdletCommandBase<NewSubscriptionRequest, NewSubscriptionResponse, NewSubscription, PimSubscriptionProxy>
	{
		// Token: 0x06004672 RID: 18034 RVA: 0x000F9A47 File Offset: 0x000F7C47
		public NewSubscriptionCommand(CallContext callContext, NewSubscriptionRequest request) : base(callContext, request, "New-Subscription", ScopeLocation.RecipientWrite)
		{
		}

		// Token: 0x06004673 RID: 18035 RVA: 0x000F9A58 File Offset: 0x000F7C58
		protected override void PopulateTaskParameters()
		{
			NewSubscription task = this.cmdletRunner.TaskWrapper.Task;
			NewSubscriptionData subscription = this.request.Subscription;
			this.cmdletRunner.SetTaskParameterIfModified("EmailAddress", subscription, task, new SmtpAddress(subscription.EmailAddress));
			this.cmdletRunner.SetTaskParameterIfModified("Force", subscription, task, new SwitchParameter(subscription.Force));
			this.cmdletRunner.SetTaskParameterIfModified("Hotmail", subscription, task, new SwitchParameter(subscription.Hotmail));
			this.cmdletRunner.SetTaskParameterIfModified("Imap", subscription, task, new SwitchParameter(subscription.Imap));
			this.cmdletRunner.SetTaskParameterIfModified("Password", subscription, task, subscription.Password.ConvertToSecureString());
			this.cmdletRunner.SetTaskParameterIfModified("Pop", subscription, task, new SwitchParameter(subscription.Pop));
			this.cmdletRunner.SetRemainingModifiedTaskParameters(subscription, task);
		}

		// Token: 0x06004674 RID: 18036 RVA: 0x000F9B58 File Offset: 0x000F7D58
		protected override void PopulateResponseData(NewSubscriptionResponse response)
		{
			PimSubscriptionProxy result = this.cmdletRunner.TaskWrapper.Result;
			response.Subscription = new Subscription
			{
				DetailedStatus = result.DetailedStatus,
				DisplayName = result.DisplayName,
				EmailAddress = result.EmailAddress.ToString(),
				Identity = new Identity(result.Identity.ToString()),
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

		// Token: 0x06004675 RID: 18037 RVA: 0x000F9C51 File Offset: 0x000F7E51
		protected override PSLocalTask<NewSubscription, PimSubscriptionProxy> InvokeCmdletFactory()
		{
			return CmdletTaskFactory.Instance.CreateNewSubscriptionTask(base.CallContext.AccessingPrincipal);
		}
	}
}
