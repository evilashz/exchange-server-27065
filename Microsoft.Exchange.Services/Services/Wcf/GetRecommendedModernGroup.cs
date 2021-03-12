using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage.Inference.GroupingModel;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Wcf.Types;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000927 RID: 2343
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class GetRecommendedModernGroup : ServiceCommand<GetModernGroupResponse>
	{
		// Token: 0x060043E0 RID: 17376 RVA: 0x000E6BF0 File Offset: 0x000E4DF0
		public GetRecommendedModernGroup(CallContext context, GetModernGroupRequest request) : base(context)
		{
			this.request = request;
			request.ValidateRequest();
			if (!VariantConfiguration.GetSnapshot(base.CallContext.AccessingADUser.GetContext(null), null, null).Inference.InferenceGroupingModel.Enabled)
			{
				ExTraceGlobals.ModernGroupsTracer.TraceError<string>((long)this.GetHashCode(), "User {0} is not enabled for flighting the InferenceGroupingModel feature!", base.CallContext.AccessingADUser.UserPrincipalName);
				throw FaultExceptionUtilities.CreateFault(new InvalidRequestException(), FaultParty.Sender);
			}
		}

		// Token: 0x060043E1 RID: 17377 RVA: 0x000E6CA4 File Offset: 0x000E4EA4
		protected override GetModernGroupResponse InternalExecute()
		{
			IRecommendedGroupInfo latentGroupRecommendation = GroupRecommendationsHelper.GetLatentGroupRecommendation(base.MailboxIdentityMailboxSession, new SmtpAddress(this.request.SmtpAddress), delegate(string message)
			{
				ExTraceGlobals.ModernGroupsTracer.TraceDebug((long)this.GetHashCode(), message);
			}, delegate(Exception e)
			{
				ExTraceGlobals.ModernGroupsTracer.TraceError<string>((long)this.GetHashCode(), "Exception while retrieving modern group recommendations: {0}", e.Message);
			});
			if (latentGroupRecommendation == null)
			{
				ExTraceGlobals.ModernGroupsTracer.TraceError<string>((long)this.GetHashCode(), "[GetRecommendedGroups.InternalExecute: could not find recommendation object for recomendation id {0}.", this.request.SmtpAddress);
			}
			return GroupRecommendationsHelper.ConvertLatentGroupRecommendationToModernGroupResponse(latentGroupRecommendation, base.CallContext.AccessingADUser.PrimarySmtpAddress);
		}

		// Token: 0x04002794 RID: 10132
		private readonly GetModernGroupRequest request;
	}
}
