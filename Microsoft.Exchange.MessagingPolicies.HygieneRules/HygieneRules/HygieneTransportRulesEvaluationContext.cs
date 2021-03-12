using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Routing;
using Microsoft.Exchange.MessagingPolicies.Rules;

namespace Microsoft.Exchange.MessagingPolicies.HygieneRules
{
	// Token: 0x02000007 RID: 7
	internal class HygieneTransportRulesEvaluationContext : RulesEvaluationContext
	{
		// Token: 0x06000013 RID: 19 RVA: 0x000022D8 File Offset: 0x000004D8
		public HygieneTransportRulesEvaluationContext(RuleCollection rules, SmtpServer server, QueuedMessageEventSource eventSource, MailItem mailItem) : base(rules)
		{
			this.MailItem = mailItem;
			this.Server = server;
			this.EventSource = eventSource;
			if (this.Server != null)
			{
				this.UserComparer = new UserComparer(this.Server.AddressBook);
				this.MembershipChecker = new MembershipChecker(this.Server.AddressBook);
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000014 RID: 20 RVA: 0x00002336 File Offset: 0x00000536
		// (set) Token: 0x06000015 RID: 21 RVA: 0x0000233E File Offset: 0x0000053E
		public MailItem MailItem { get; private set; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000016 RID: 22 RVA: 0x00002347 File Offset: 0x00000547
		// (set) Token: 0x06000017 RID: 23 RVA: 0x0000234F File Offset: 0x0000054F
		public SmtpServer Server { get; private set; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000018 RID: 24 RVA: 0x00002358 File Offset: 0x00000558
		// (set) Token: 0x06000019 RID: 25 RVA: 0x00002360 File Offset: 0x00000560
		public List<EnvelopeRecipient> MatchedRecipients { get; set; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600001A RID: 26 RVA: 0x00002369 File Offset: 0x00000569
		// (set) Token: 0x0600001B RID: 27 RVA: 0x00002371 File Offset: 0x00000571
		public QueuedMessageEventSource EventSource { get; private set; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600001C RID: 28 RVA: 0x0000237A File Offset: 0x0000057A
		// (set) Token: 0x0600001D RID: 29 RVA: 0x00002382 File Offset: 0x00000582
		public IStringComparer UserComparer { get; private set; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600001E RID: 30 RVA: 0x0000238B File Offset: 0x0000058B
		// (set) Token: 0x0600001F RID: 31 RVA: 0x00002393 File Offset: 0x00000593
		public IStringComparer MembershipChecker { get; private set; }
	}
}
