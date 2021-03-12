using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x02000396 RID: 918
	internal sealed class UpdateInboxRules : InboxRulesCommandBase<UpdateInboxRulesRequest, UpdateInboxRulesResponse>
	{
		// Token: 0x060019B8 RID: 6584 RVA: 0x00092BD8 File Offset: 0x00090DD8
		public UpdateInboxRules(CallContext callContext, UpdateInboxRulesRequest request) : base(callContext, ExTraceGlobals.UpdateInboxRulesCallTracer, (FaultInjection.LIDs)2972069181U, request)
		{
		}

		// Token: 0x060019B9 RID: 6585 RVA: 0x00092BEC File Offset: 0x00090DEC
		protected override UpdateInboxRulesResponse Execute(Rules serverRules, MailboxSession mailboxSession)
		{
			ServiceCommandBase.ThrowIfNullOrEmpty<RuleOperation>(base.Request.Operations, "Request.Operations", "UpdateInboxRules.ValidateRequest");
			if (!base.Request.RemoveOutlookRuleBlob && serverRules.LegacyOutlookRulesCacheExists)
			{
				throw new OutlookRuleBlobExistsException();
			}
			RuleOperationParser ruleOperationParser = new RuleOperationParser(base.Request.Operations.Length, base.CallContext, mailboxSession, serverRules, ExTraceGlobals.UpdateInboxRulesCallTracer, base.HashCode);
			Rules rules = ruleOperationParser.Parse(base.Request.Operations);
			UpdateInboxRulesResponse updateInboxRulesResponse = new UpdateInboxRulesResponse();
			if (!ruleOperationParser.HasOperationError)
			{
				rules.SaveBatch();
			}
			else
			{
				updateInboxRulesResponse.Initialize(ServiceResultCode.Error, new ServiceError((CoreResources.IDs)2296308088U, ResponseCodeType.ErrorInboxRulesValidationError, 0, ExchangeVersion.Current));
				updateInboxRulesResponse.RuleOperationErrors = ruleOperationParser.RuleOperationErrorList.ToArray();
			}
			return updateInboxRulesResponse;
		}
	}
}
