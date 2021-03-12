using System;
using System.Globalization;
using Microsoft.Exchange.Data.Storage.MailboxRules;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Transport.Logging.MessageTracking;

namespace Microsoft.Exchange.Transport.MailboxRules
{
	// Token: 0x02000094 RID: 148
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class StoreDriverLimitChecker : LimitChecker
	{
		// Token: 0x06000513 RID: 1299 RVA: 0x0001A801 File Offset: 0x00018A01
		public StoreDriverLimitChecker(IRuleEvaluationContext context) : base(context)
		{
		}

		// Token: 0x06000514 RID: 1300 RVA: 0x0001A80C File Offset: 0x00018A0C
		protected override void MessageTrackThrottle<C, L>(string limitType, C count, L limit)
		{
			if (base.ServerIPAddress == null)
			{
				base.ServerIPAddress = this.context.LocalServerNetworkAddress;
			}
			RuleEvaluationContext ruleEvaluationContext = this.context as RuleEvaluationContext;
			MessageTrackingLog.TrackThrottle(MessageTrackingSource.MAILBOXRULE, ruleEvaluationContext.MbxTransportMailItem, base.ServerIPAddress, this.context.CurrentFolderDisplayName, string.Format(CultureInfo.InvariantCulture, "{0}:{1}/{2}", new object[]
			{
				limitType,
				count,
				limit
			}), this.context.Recipient, this.context.CurrentRule.Name);
		}
	}
}
