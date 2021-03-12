using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Transport;

namespace Microsoft.Exchange.MessagingPolicies.Rules
{
	// Token: 0x02000081 RID: 129
	internal static class RoutingActionUtils
	{
		// Token: 0x060003F9 RID: 1017 RVA: 0x000152D0 File Offset: 0x000134D0
		internal static void ProcessRecipients(TransportRulesEvaluationContext context, Action<EnvelopeRecipient> onRecipient)
		{
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}
			if (onRecipient == null)
			{
				throw new ArgumentNullException("onRecipient");
			}
			IEnumerable<EnvelopeRecipient> enumerable;
			if (context.MatchedRecipients == null)
			{
				enumerable = context.MailItem.Recipients;
			}
			else
			{
				enumerable = context.MatchedRecipients;
			}
			foreach (EnvelopeRecipient obj in enumerable)
			{
				onRecipient(obj);
			}
		}
	}
}
