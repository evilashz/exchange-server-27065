using System;
using Microsoft.Exchange.Data.ApplicationLogic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Notifications.Broker
{
	// Token: 0x0200001F RID: 31
	public sealed class BrokerSubscriptionDiagnosticsHandler : ExchangeDiagnosableWrapper<DatabasesSnapshot>
	{
		// Token: 0x06000158 RID: 344 RVA: 0x000092AA File Offset: 0x000074AA
		private BrokerSubscriptionDiagnosticsHandler()
		{
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000159 RID: 345 RVA: 0x000092B2 File Offset: 0x000074B2
		protected override string UsageText
		{
			get
			{
				return "The BrokerSubscriptionDiagnosticsHandler is a diagnostics handler that returns information about the contents of the broker subscription data.";
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x0600015A RID: 346 RVA: 0x000092B9 File Offset: 0x000074B9
		protected override string UsageSample
		{
			get
			{
				return "Example 1: Return a list of databases and mailboxes with subscriptions.  Does NOT return subscription data.\r\n   Get-ExchangeDiagnosticInfo -Process Microsoft.Exchange.Notifications.Broker -Component BrokerSubscriptions\r\n\r\nExample 2: Return a list of databases and mailboxes with subscriptions as well as all subscriptions for a particular source smtpAddress.\r\n   Get-ExchangeDiagnosticInfo -Process Microsoft.Exchange.Notifications.Broker -Component BrokerSubscriptions -Argument emailAddress=<emailAddress>\r\n\r\nExample 3: Return a list of databases and mailboxes with subscriptions as well as all subscriptions for a particular source mailbox guid.\r\n   Get-ExchangeDiagnosticInfo -Process Microsoft.Exchange.Notifications.Broker -Component BrokerSubscriptions -Argument mbxGuid=<mbxGuid>\r\n\r\n";
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x0600015B RID: 347 RVA: 0x000092C0 File Offset: 0x000074C0
		protected override string ComponentName
		{
			get
			{
				return "BrokerSubscriptions";
			}
		}

		// Token: 0x0600015C RID: 348 RVA: 0x000092C8 File Offset: 0x000074C8
		public static BrokerSubscriptionDiagnosticsHandler GetInstance()
		{
			if (BrokerSubscriptionDiagnosticsHandler.instance == null)
			{
				lock (BrokerSubscriptionDiagnosticsHandler.lockObject)
				{
					if (BrokerSubscriptionDiagnosticsHandler.instance == null)
					{
						BrokerSubscriptionDiagnosticsHandler.instance = new BrokerSubscriptionDiagnosticsHandler();
					}
				}
			}
			return BrokerSubscriptionDiagnosticsHandler.instance;
		}

		// Token: 0x0600015D RID: 349 RVA: 0x00009320 File Offset: 0x00007520
		internal override DatabasesSnapshot GetExchangeDiagnosticsInfoData(DiagnosableParameters argument)
		{
			string text = null;
			Guid empty = Guid.Empty;
			if (!string.IsNullOrEmpty(argument.Argument))
			{
				string[] array = argument.Argument.Split(new char[]
				{
					','
				});
				foreach (string text2 in array)
				{
					if (text2.StartsWith("emailaddress=", StringComparison.OrdinalIgnoreCase))
					{
						text = text2.Substring("emailaddress=".Length);
						text = base.RemoveQuotes(text);
						break;
					}
					if (text2.StartsWith("mbxguid=", StringComparison.OrdinalIgnoreCase))
					{
						string input = text2.Substring("mbxguid=".Length);
						input = base.RemoveQuotes(input);
						if (Guid.TryParse(input, out empty))
						{
							break;
						}
						empty = Guid.Empty;
					}
				}
			}
			return DatabasesSnapshot.Create(text, empty);
		}

		// Token: 0x0400009D RID: 157
		private const string EmailAddressPrefix = "emailaddress=";

		// Token: 0x0400009E RID: 158
		private const string MailboxGuidPrefix = "mbxguid=";

		// Token: 0x0400009F RID: 159
		private static BrokerSubscriptionDiagnosticsHandler instance;

		// Token: 0x040000A0 RID: 160
		private static object lockObject = new object();
	}
}
