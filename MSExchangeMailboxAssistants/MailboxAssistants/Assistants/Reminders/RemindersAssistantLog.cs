using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Principal;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants.Reminders;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.Reminders
{
	// Token: 0x02000252 RID: 594
	internal class RemindersAssistantLog : QuickLog, IRemindersAssistantLog
	{
		// Token: 0x06001644 RID: 5700 RVA: 0x0007DC97 File Offset: 0x0007BE97
		private RemindersAssistantLog() : base(500)
		{
		}

		// Token: 0x170005BD RID: 1469
		// (get) Token: 0x06001645 RID: 5701 RVA: 0x0007DCA4 File Offset: 0x0007BEA4
		public static RemindersAssistantLog Instance
		{
			get
			{
				return RemindersAssistantLog.instance;
			}
		}

		// Token: 0x06001646 RID: 5702 RVA: 0x0007DCAC File Offset: 0x0007BEAC
		public void LogEntry(IMailboxSession session, string entry, params object[] args)
		{
			ArgumentValidator.ThrowIfNull("session", session);
			ArgumentValidator.ThrowIfTypeInvalid<MailboxSession>("session", session);
			ArgumentValidator.ThrowIfNull("entry", entry);
			ExTraceGlobals.GeneralTracer.TraceDebug((long)this.GetHashCode(), entry, args);
			base.AppendFormatLogEntry(session as MailboxSession, entry, args);
		}

		// Token: 0x06001647 RID: 5703 RVA: 0x0007DCFC File Offset: 0x0007BEFC
		public void LogEntry(IMailboxSession session, Exception e, bool logWatsonReport, string entry, params object[] args)
		{
			ArgumentValidator.ThrowIfNull("session", session);
			ArgumentValidator.ThrowIfTypeInvalid<MailboxSession>("session", session);
			ArgumentValidator.ThrowIfNull("e", e);
			ArgumentValidator.ThrowIfNull("entry", entry);
			ExTraceGlobals.GeneralTracer.TraceError<IExchangePrincipal>((long)this.GetHashCode(), "Mailbox: {0}", session.MailboxOwner);
			ExTraceGlobals.GeneralTracer.TraceError((long)this.GetHashCode(), entry, args);
			ExTraceGlobals.GeneralTracer.TraceError((long)this.GetHashCode(), e.ToString());
			base.AppendFormatLogEntry(session as MailboxSession, e, logWatsonReport, entry, args);
		}

		// Token: 0x170005BE RID: 1470
		// (get) Token: 0x06001648 RID: 5704 RVA: 0x0007DD90 File Offset: 0x0007BF90
		protected override string LogMessageClass
		{
			get
			{
				return "IPM.Microsoft.RemindersAssistant.Log";
			}
		}

		// Token: 0x04000D19 RID: 3353
		private const int MaxLogEntries = 500;

		// Token: 0x04000D1A RID: 3354
		private const string MessageClass = "IPM.Microsoft.RemindersAssistant.Log";

		// Token: 0x04000D1B RID: 3355
		private static RemindersAssistantLog instance = new RemindersAssistantLog();
	}
}
