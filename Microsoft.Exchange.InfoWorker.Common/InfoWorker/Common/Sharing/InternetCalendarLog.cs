using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.InfoWorker.Common.Sharing
{
	// Token: 0x0200026C RID: 620
	internal class InternetCalendarLog : QuickLog
	{
		// Token: 0x060011AA RID: 4522 RVA: 0x000524B9 File Offset: 0x000506B9
		private InternetCalendarLog() : base(100)
		{
		}

		// Token: 0x060011AB RID: 4523 RVA: 0x000524C4 File Offset: 0x000506C4
		public static void LogEntry(MailboxSession session, string entry)
		{
			string entry2 = string.Format("{0}, Mailbox: '{1}', Entry {2}.", DateTime.UtcNow.ToString(), session.DisplayName, entry);
			InternetCalendarLog.instance.WriteLogEntry(session, entry2);
		}

		// Token: 0x060011AC RID: 4524 RVA: 0x00052504 File Offset: 0x00050704
		public static void LogEntry(MailboxSession session, PublishingSubscriptionData subscriptionData, HttpWebRequest request, List<LocalizedString> errors)
		{
			StringBuilder stringBuilder = new StringBuilder(InternetCalendarLog.MaxErrorBufferSize);
			if (errors != null && errors.Count > 0)
			{
				string arg;
				if (request != null && request.Proxy is WebProxy)
				{
					arg = ((WebProxy)request.Proxy).Address.ToString();
				}
				else
				{
					arg = "<unknown>";
				}
				stringBuilder.AppendFormat("Errors while synchronizing calendar. Subscription data: {0}, proxy is: {1}. ", subscriptionData.ToString(), arg);
				foreach (LocalizedString value in errors)
				{
					if (stringBuilder.Length > InternetCalendarLog.MaxErrorBufferSize)
					{
						InternetCalendarLog.instance.WriteLogEntry(session, stringBuilder.ToString());
						stringBuilder.Length = 0;
					}
					stringBuilder.Append(value);
					stringBuilder.Append(';');
				}
				if (stringBuilder.Length > 0)
				{
					InternetCalendarLog.LogEntry(session, stringBuilder.ToString());
				}
			}
		}

		// Token: 0x1700047E RID: 1150
		// (get) Token: 0x060011AD RID: 4525 RVA: 0x000525FC File Offset: 0x000507FC
		protected override string LogMessageClass
		{
			get
			{
				return "IPM.Microsoft.InternetCalendar.Log";
			}
		}

		// Token: 0x04000BA5 RID: 2981
		private const int DefaultMaxLogEntries = 100;

		// Token: 0x04000BA6 RID: 2982
		private const string UnknownProxy = "<unknown>";

		// Token: 0x04000BA7 RID: 2983
		public const string MessageClass = "IPM.Microsoft.InternetCalendar.Log";

		// Token: 0x04000BA8 RID: 2984
		protected static readonly int MaxErrorBufferSize = 1024;

		// Token: 0x04000BA9 RID: 2985
		private static InternetCalendarLog instance = new InternetCalendarLog();
	}
}
