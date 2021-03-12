using System;
using Microsoft.Exchange.Data.ApplicationLogic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Notifications.Broker
{
	// Token: 0x02000012 RID: 18
	public sealed class SubscriptionsForHandlerDiagnosticsHandler : ExchangeDiagnosableWrapper<SubscriptionsForHandlersSnapshot>
	{
		// Token: 0x060000CB RID: 203 RVA: 0x00005C59 File Offset: 0x00003E59
		private SubscriptionsForHandlerDiagnosticsHandler()
		{
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000CC RID: 204 RVA: 0x00005C61 File Offset: 0x00003E61
		protected override string UsageText
		{
			get
			{
				return "The SubscriptionsForHandler is a diagnostics handler the returns subscriptions for particular handlers.";
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000CD RID: 205 RVA: 0x00005C68 File Offset: 0x00003E68
		protected override string UsageSample
		{
			get
			{
				return "Example 1: Return metadata about all notification handlers.\r\n   Get-ExchangeDiagnosticInfo -Process Microsoft.Exchange.Notifications.Broker -Component SubscriptionsForHandler \r\n\r\nExample 2: Return all handlers for a particular source mailbox.\r\n   Get-ExchangeDiagnosticInfo -Process Microsoft.Exchange.Notifications.Broker -Component SubscriptionsForHandler -Argument \"mbxGuid=<mbxGuid>\"\r\n\r\nExample 3: Return all subscriptions that a particular handler is servicing.\r\n   Get-ExchangeDiagnosticInfo -Process Microsoft.Exchange.Notifications.Broker -Component SubscriptionsForHandler -Argument \"handler=<handlerName>\"\r\n\r\nExample 4: Return all subscriptions for a particular database.\r\n   Get-ExchangeDiagnosticInfo -Process Microsoft.Exchange.Notifications.Broker -Component SubscriptionsForHandler -Argument \"handler=<handlerName>,mdbGuid=<guid>\"\r\n\r\n";
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000CE RID: 206 RVA: 0x00005C6F File Offset: 0x00003E6F
		protected override string ComponentName
		{
			get
			{
				return "SubscriptionsForHandler";
			}
		}

		// Token: 0x060000CF RID: 207 RVA: 0x00005C78 File Offset: 0x00003E78
		public static SubscriptionsForHandlerDiagnosticsHandler GetInstance()
		{
			if (SubscriptionsForHandlerDiagnosticsHandler.instance == null)
			{
				lock (SubscriptionsForHandlerDiagnosticsHandler.lockObject)
				{
					if (SubscriptionsForHandlerDiagnosticsHandler.instance == null)
					{
						SubscriptionsForHandlerDiagnosticsHandler.instance = new SubscriptionsForHandlerDiagnosticsHandler();
					}
				}
			}
			return SubscriptionsForHandlerDiagnosticsHandler.instance;
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x00005CD0 File Offset: 0x00003ED0
		internal override SubscriptionsForHandlersSnapshot GetExchangeDiagnosticsInfoData(DiagnosableParameters argument)
		{
			Guid empty = Guid.Empty;
			Guid empty2 = Guid.Empty;
			string text = null;
			if (!string.IsNullOrEmpty(argument.Argument))
			{
				string[] array = argument.Argument.Split(new char[]
				{
					','
				});
				foreach (string text2 in array)
				{
					if (text2.StartsWith("mbxguid=", StringComparison.OrdinalIgnoreCase))
					{
						string input = text2.Substring("mbxguid=".Length);
						input = base.RemoveQuotes(input);
						if (!Guid.TryParse(input, out empty))
						{
							empty = Guid.Empty;
						}
					}
					else if (text2.StartsWith("mdbguid=", StringComparison.OrdinalIgnoreCase))
					{
						string input2 = text2.Substring("mdbguid=".Length);
						input2 = base.RemoveQuotes(input2);
						if (!Guid.TryParse(input2, out empty2))
						{
							empty2 = Guid.Empty;
						}
					}
					else if (text2.StartsWith("handler=", StringComparison.OrdinalIgnoreCase))
					{
						text = text2.Substring("handler=".Length);
						text = base.RemoveQuotes(text);
					}
				}
			}
			return SubscriptionsForHandlersSnapshot.Create(empty2, empty, text);
		}

		// Token: 0x0400005F RID: 95
		private const string DatabaseGuidPrefix = "mdbguid=";

		// Token: 0x04000060 RID: 96
		private const string MailboxGuidPrefix = "mbxguid=";

		// Token: 0x04000061 RID: 97
		private const string HandlerPrefix = "handler=";

		// Token: 0x04000062 RID: 98
		private static SubscriptionsForHandlerDiagnosticsHandler instance;

		// Token: 0x04000063 RID: 99
		private static object lockObject = new object();
	}
}
