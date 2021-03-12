using System;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Routing;
using Microsoft.Exchange.MessagingPolicies.Rules;

namespace Microsoft.Exchange.MessagingPolicies.HygieneRules
{
	// Token: 0x02000004 RID: 4
	internal static class HygieneRuleUtils
	{
		// Token: 0x06000009 RID: 9 RVA: 0x000021CC File Offset: 0x000003CC
		public static bool TryRunRuleCollection(RuleCollection rules, SmtpServer server, MailItem mailItem, QueuedMessageEventSource eventSource, out Exception e)
		{
			e = null;
			HygieneTransportRulesEvaluationContext context = new HygieneTransportRulesEvaluationContext(rules, server, eventSource, mailItem);
			try
			{
				HygieneTransportRulesEvaluator hygieneTransportRulesEvaluator = new HygieneTransportRulesEvaluator(context);
				hygieneTransportRulesEvaluator.Run();
			}
			catch (Exception ex)
			{
				e = ex;
				return false;
			}
			return true;
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002214 File Offset: 0x00000414
		private static int CompareTransportRule(TransportRule x, TransportRule y)
		{
			return x.Priority.CompareTo(y.Priority);
		}

		// Token: 0x0400000C RID: 12
		public const int FixedObjectOverhead = 18;
	}
}
