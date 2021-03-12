using System;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Clients.Owa.Premium
{
	// Token: 0x020004CB RID: 1227
	[OwaEventNamespace("perf")]
	internal sealed class PerformanceConsoleEventHandler : ItemEventHandler
	{
		// Token: 0x06002EE1 RID: 12001 RVA: 0x0010DAA7 File Offset: 0x0010BCA7
		public static void Register()
		{
			OwaEventRegistry.RegisterHandler(typeof(PerformanceConsoleEventHandler));
		}

		// Token: 0x06002EE2 RID: 12002 RVA: 0x0010DAB8 File Offset: 0x0010BCB8
		[OwaEvent("strt")]
		public void StartPerfConsole()
		{
			base.ResponseContentType = OwaEventContentType.Javascript;
			OwaContext.Current.UserContext.IsPerformanceConsoleOn = true;
		}

		// Token: 0x06002EE3 RID: 12003 RVA: 0x0010DAD1 File Offset: 0x0010BCD1
		[OwaEvent("stop")]
		public void StopPerfConsole()
		{
			base.ResponseContentType = OwaEventContentType.Javascript;
			OwaContext.Current.UserContext.IsPerformanceConsoleOn = false;
		}

		// Token: 0x06002EE4 RID: 12004 RVA: 0x0010DAEC File Offset: 0x0010BCEC
		[OwaEvent("reportPerf")]
		[OwaEventParameter("b", typeof(string))]
		public void ReportPerformanceExperience()
		{
			base.ResponseContentType = OwaEventContentType.Javascript;
			string text = (string)base.GetParameter("s");
			MessageItem messageItem = MessageItem.Create(base.UserContext.MailboxSession, base.UserContext.DraftsFolderId);
			messageItem[ItemSchema.ConversationIndexTracking] = true;
			Markup markup = Markup.Html;
			BodyConversionUtilities.SetBody(messageItem, (string)base.GetParameter("b"), markup, StoreObjectType.Message, base.UserContext);
			messageItem.Recipients.Add(new Participant(null, Globals.ErrorReportAddress, "SMTP"), RecipientItemType.To);
			messageItem.Subject = string.Concat(new string[]
			{
				"Performance Report on ",
				DateTime.UtcNow.ToLocalTime().ToShortDateString(),
				" ",
				DateTime.UtcNow.ToLocalTime().ToShortTimeString(),
				" for ",
				base.OwaContext.UserContext.ExchangePrincipal.MailboxInfo.DisplayName
			});
			messageItem.Save(SaveMode.ResolveConflicts);
			messageItem.Load();
			this.Writer.Write(messageItem.Id.ObjectId.ToBase64String());
		}

		// Token: 0x040020BE RID: 8382
		public const string EventNamespace = "perf";

		// Token: 0x040020BF RID: 8383
		public const string MethodReportExperience = "reportPerf";

		// Token: 0x040020C0 RID: 8384
		public const string MethodStart = "strt";

		// Token: 0x040020C1 RID: 8385
		public const string MethodStop = "stop";

		// Token: 0x040020C2 RID: 8386
		public const string Body = "b";

		// Token: 0x040020C3 RID: 8387
		public const string Subject = "s";
	}
}
