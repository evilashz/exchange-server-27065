using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x02000311 RID: 785
	internal sealed class GetInboxRules : InboxRulesCommandBase<GetInboxRulesRequest, GetInboxRulesResponse>
	{
		// Token: 0x06001628 RID: 5672 RVA: 0x00072CD7 File Offset: 0x00070ED7
		public GetInboxRules(CallContext callContext, GetInboxRulesRequest request) : base(callContext, ExTraceGlobals.GetInboxRulesCallTracer, (FaultInjection.LIDs)3274059069U, request)
		{
		}

		// Token: 0x06001629 RID: 5673 RVA: 0x00072CEC File Offset: 0x00070EEC
		protected override GetInboxRulesResponse Execute(Rules serverRules, MailboxSession session)
		{
			GetInboxRulesResponse getInboxRulesResponse = new GetInboxRulesResponse();
			getInboxRulesResponse.OutlookRuleBlobExists = serverRules.LegacyOutlookRulesCacheExists;
			if (0 < serverRules.Count)
			{
				getInboxRulesResponse.InboxRules = new EwsRule[serverRules.Count];
				for (int i = 0; i < serverRules.Count; i++)
				{
					Rule serverRule = serverRules[i];
					EwsRule ewsRule = EwsRule.CreateFromServerRule(serverRule, base.HashCode, session, base.CallContext.ClientCulture);
					getInboxRulesResponse.InboxRules[i] = ewsRule;
				}
			}
			return getInboxRulesResponse;
		}
	}
}
