using System;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Management.Aggregation;
using Microsoft.Exchange.Management.PSDirectInvoke;
using Microsoft.Exchange.Services.Core.DataConverter;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Wcf.Types;
using Microsoft.Exchange.Transport.Sync.Common.Subscription.DeltaSync;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x0200099C RID: 2460
	internal sealed class GetHotmailSubscriptionCommand : SingleCmdletCommandBase<IdentityRequest, GetHotmailSubscriptionResponse, GetHotmailSubscription, HotmailSubscriptionProxy>
	{
		// Token: 0x06004633 RID: 17971 RVA: 0x000F746B File Offset: 0x000F566B
		public GetHotmailSubscriptionCommand(CallContext callContext, IdentityRequest request) : base(callContext, request, "Get-HotmailSubscription", ScopeLocation.RecipientRead)
		{
		}

		// Token: 0x06004634 RID: 17972 RVA: 0x000F747C File Offset: 0x000F567C
		protected override void PopulateTaskParameters()
		{
			GetHotmailSubscription task = this.cmdletRunner.TaskWrapper.Task;
			this.cmdletRunner.SetTaskParameter("Identity", task, new AggregationSubscriptionIdParameter(this.request.Identity));
		}

		// Token: 0x06004635 RID: 17973 RVA: 0x000F74BC File Offset: 0x000F56BC
		protected override void PopulateResponseData(GetHotmailSubscriptionResponse response)
		{
			HotmailSubscriptionProxy result = this.cmdletRunner.TaskWrapper.Result;
			if (result == null)
			{
				response.HotmailSubscription = null;
				return;
			}
			response.HotmailSubscription = new HotmailSubscription
			{
				DetailedStatus = result.DetailedStatus,
				DisplayName = result.DisplayName,
				EmailAddress = result.EmailAddress.ToString(),
				Identity = new Identity(result.Identity.ToString(), result.DisplayName),
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

		// Token: 0x06004636 RID: 17974 RVA: 0x000F75C6 File Offset: 0x000F57C6
		protected override PSLocalTask<GetHotmailSubscription, HotmailSubscriptionProxy> InvokeCmdletFactory()
		{
			return CmdletTaskFactory.Instance.CreateGetHotmailSubscriptionTask(base.CallContext.AccessingPrincipal);
		}
	}
}
