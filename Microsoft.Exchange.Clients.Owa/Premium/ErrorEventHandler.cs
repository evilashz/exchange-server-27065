using System;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Clients;

namespace Microsoft.Exchange.Clients.Owa.Premium
{
	// Token: 0x020004B2 RID: 1202
	[OwaEventNamespace("Error")]
	internal sealed class ErrorEventHandler : OwaEventHandlerBase
	{
		// Token: 0x06002E10 RID: 11792 RVA: 0x001064D1 File Offset: 0x001046D1
		[OwaEvent("SendReport")]
		[OwaEventParameter("b", typeof(string))]
		[OwaEventParameter("s", typeof(string))]
		public void SendReport()
		{
			ExTraceGlobals.CoreCallTracer.TraceDebug((long)this.GetHashCode(), "ErrorEventHandler.SendReport");
			this.SendEmailReport((string)base.GetParameter("b"), (string)base.GetParameter("s"));
		}

		// Token: 0x06002E11 RID: 11793 RVA: 0x00106510 File Offset: 0x00104710
		private void SendEmailReport(string body, string subject)
		{
			ExTraceGlobals.CoreCallTracer.TraceDebug((long)this.GetHashCode(), "ErrorEventHandler.SendEmailReport");
			if (!Globals.EnableEmailReports)
			{
				return;
			}
			body = string.Format("{0}\r\n--------------------------------------------------\r\n{1}", body, Utilities.GetExtraWatsonData(base.OwaContext));
			MessageItem messageItem = MessageItem.Create(base.UserContext.MailboxSession, base.UserContext.DraftsFolderId);
			messageItem.Subject = subject;
			ItemUtility.SetItemBody(messageItem, BodyFormat.TextPlain, body);
			messageItem.Recipients.Add(new Participant(null, Globals.ErrorReportAddress, "SMTP"), RecipientItemType.To);
			messageItem[ItemSchema.ConversationIndexTracking] = true;
			messageItem.Send();
		}

		// Token: 0x04001F5E RID: 8030
		public const string EventNamespace = "Error";

		// Token: 0x04001F5F RID: 8031
		public const string MethodSendReport = "SendReport";

		// Token: 0x04001F60 RID: 8032
		public const string MethodSendClientError = "ClientError";

		// Token: 0x04001F61 RID: 8033
		public const string Subject = "s";

		// Token: 0x04001F62 RID: 8034
		public const string Body = "b";

		// Token: 0x04001F63 RID: 8035
		public const string LineNumber = "ln";

		// Token: 0x04001F64 RID: 8036
		public const string Function = "fn";

		// Token: 0x04001F65 RID: 8037
		public const string ExceptionMessage = "msg";

		// Token: 0x04001F66 RID: 8038
		public const string CallStack = "cs";
	}
}
