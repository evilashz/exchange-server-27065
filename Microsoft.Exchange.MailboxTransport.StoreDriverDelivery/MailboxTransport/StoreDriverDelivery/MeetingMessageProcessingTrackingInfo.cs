using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxTransport.StoreDriverDelivery
{
	// Token: 0x0200009D RID: 157
	internal class MeetingMessageProcessingTrackingInfo
	{
		// Token: 0x1700017D RID: 381
		// (get) Token: 0x06000549 RID: 1353 RVA: 0x0001CA34 File Offset: 0x0001AC34
		// (set) Token: 0x0600054A RID: 1354 RVA: 0x0001CA3C File Offset: 0x0001AC3C
		public int MeetingMessageProcessingAttempts { get; set; }

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x0600054B RID: 1355 RVA: 0x0001CA45 File Offset: 0x0001AC45
		// (set) Token: 0x0600054C RID: 1356 RVA: 0x0001CA4D File Offset: 0x0001AC4D
		public int CalendarUpdateXsoCodeAttempts { get; set; }

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x0600054D RID: 1357 RVA: 0x0001CA56 File Offset: 0x0001AC56
		// (set) Token: 0x0600054E RID: 1358 RVA: 0x0001CA5E File Offset: 0x0001AC5E
		public MeetingMessageProcessStages Stage { get; set; }

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x0600054F RID: 1359 RVA: 0x0001CA67 File Offset: 0x0001AC67
		// (set) Token: 0x06000550 RID: 1360 RVA: 0x0001CA6F File Offset: 0x0001AC6F
		public bool ProcessingSucceeded { get; set; }

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x06000551 RID: 1361 RVA: 0x0001CA78 File Offset: 0x0001AC78
		// (set) Token: 0x06000552 RID: 1362 RVA: 0x0001CA80 File Offset: 0x0001AC80
		public string OrgId { get; set; }

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x06000553 RID: 1363 RVA: 0x0001CA89 File Offset: 0x0001AC89
		// (set) Token: 0x06000554 RID: 1364 RVA: 0x0001CA91 File Offset: 0x0001AC91
		public string Goid { get; set; }

		// Token: 0x06000555 RID: 1365 RVA: 0x0001CA9A File Offset: 0x0001AC9A
		public MeetingMessageProcessingTrackingInfo(string legacyDN, Guid mbxGuid)
		{
			this.legacyDN = legacyDN;
			this.mbxGuid = mbxGuid;
		}

		// Token: 0x06000556 RID: 1366 RVA: 0x0001CAC6 File Offset: 0x0001ACC6
		public void AddLogMessage(string logMessage)
		{
			if (string.IsNullOrEmpty(logMessage))
			{
				return;
			}
			this.additionalLogMessages.Append("[");
			this.additionalLogMessages.Append(logMessage);
			this.additionalLogMessages.Append("]");
		}

		// Token: 0x06000557 RID: 1367 RVA: 0x0001CB00 File Offset: 0x0001AD00
		public void SaveExceptionInfo(Exception ex)
		{
			if (ex == null)
			{
				return;
			}
			this.exceptionsInfo.Append("[");
			this.exceptionsInfo.Append(string.Format("CurrentRetryValues- MeetingMessageProcessingAttempts-{0} CalendarUpdateXSOCodeAttempts-{1} ", this.MeetingMessageProcessingAttempts, this.CalendarUpdateXsoCodeAttempts));
			this.exceptionsInfo.Append(this.GetExceptionInfo(ex));
			if (ex.InnerException != null)
			{
				this.exceptionsInfo.Append(" InnerException - ");
				this.exceptionsInfo.Append(this.GetExceptionInfo(ex.InnerException));
			}
			this.exceptionsInfo.Append("]");
		}

		// Token: 0x06000558 RID: 1368 RVA: 0x0001CBA4 File Offset: 0x0001ADA4
		public List<KeyValuePair<string, string>> GetExtraEventData()
		{
			List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
			list.Add(new KeyValuePair<string, string>("LegacyDN", this.legacyDN));
			if (this.OrgId != null)
			{
				list.Add(new KeyValuePair<string, string>("OrganizationId", this.OrgId));
			}
			if (this.Goid != null)
			{
				list.Add(new KeyValuePair<string, string>("GlobalObjectId", this.Goid));
			}
			list.Add(new KeyValuePair<string, string>("MbxGuid", this.mbxGuid.ToString()));
			list.Add(new KeyValuePair<string, string>("ProcessingSucceeded", this.ProcessingSucceeded.ToString()));
			list.Add(new KeyValuePair<string, string>("ProcessingStage", this.Stage.ToString()));
			list.Add(new KeyValuePair<string, string>("MeetingMessageProcessingAttempts", this.MeetingMessageProcessingAttempts.ToString(CultureInfo.InvariantCulture)));
			list.Add(new KeyValuePair<string, string>("CalendarUpdateXSOCodeAttempts", this.CalendarUpdateXsoCodeAttempts.ToString(CultureInfo.InvariantCulture)));
			this.AddNewEventData("AdditionalInfo", this.additionalLogMessages, list);
			this.AddNewEventData("ExceptionInfo", this.exceptionsInfo, list);
			return list;
		}

		// Token: 0x06000559 RID: 1369 RVA: 0x0001CCD8 File Offset: 0x0001AED8
		private void AddNewEventData(string key, StringBuilder sbValue, List<KeyValuePair<string, string>> extraEventData)
		{
			string value = sbValue.ToString();
			if (!string.IsNullOrEmpty(value))
			{
				extraEventData.Add(new KeyValuePair<string, string>(key, value));
			}
		}

		// Token: 0x0600055A RID: 1370 RVA: 0x0001CD04 File Offset: 0x0001AF04
		private string GetExceptionInfo(Exception ex)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(ex.GetType().Name + " ExceptionMessage " + SpecialCharacters.SanitizeForLogging(ex.Message));
			string stackTrace = ex.StackTrace;
			if (!string.IsNullOrEmpty(stackTrace))
			{
				stringBuilder.Append(" StackTrace - ");
				stringBuilder.Append(SpecialCharacters.SanitizeForLogging(stackTrace));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x040002ED RID: 749
		private const string LegacyDnKey = "LegacyDN";

		// Token: 0x040002EE RID: 750
		private const string MbxGuidKey = "MbxGuid";

		// Token: 0x040002EF RID: 751
		private const string ProcessingSucceededKey = "ProcessingSucceeded";

		// Token: 0x040002F0 RID: 752
		private const string ProcessingStageKey = "ProcessingStage";

		// Token: 0x040002F1 RID: 753
		private const string MeetingMessageProcessingAttemptsKey = "MeetingMessageProcessingAttempts";

		// Token: 0x040002F2 RID: 754
		private const string CalendarUpdateXsoCodeAttemptsKey = "CalendarUpdateXSOCodeAttempts";

		// Token: 0x040002F3 RID: 755
		private const string ExceptionsInfoKey = "ExceptionInfo";

		// Token: 0x040002F4 RID: 756
		private const string AdditionalInfoKey = "AdditionalInfo";

		// Token: 0x040002F5 RID: 757
		private const string OrganizationId = "OrganizationId";

		// Token: 0x040002F6 RID: 758
		private const string GlobalObjectId = "GlobalObjectId";

		// Token: 0x040002F7 RID: 759
		private StringBuilder exceptionsInfo = new StringBuilder();

		// Token: 0x040002F8 RID: 760
		private StringBuilder additionalLogMessages = new StringBuilder();

		// Token: 0x040002F9 RID: 761
		private readonly string legacyDN;

		// Token: 0x040002FA RID: 762
		private readonly Guid mbxGuid;
	}
}
