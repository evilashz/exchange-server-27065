using System;
using System.Globalization;
using System.IO;
using System.Text;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.UM.UMCommon.MessageContent;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x020000A8 RID: 168
	internal abstract class MessageContentBuilder
	{
		// Token: 0x060005B1 RID: 1457 RVA: 0x00016D40 File Offset: 0x00014F40
		public static LocalizedString GetPhoneLabel(ContactInfo callerInfo)
		{
			LocalizedString result = LocalizedString.Empty;
			switch (callerInfo.FoundBy)
			{
			case FoundByType.BusinessPhone:
				result = Strings.WorkPhoneLabel;
				break;
			case FoundByType.MobilePhone:
				result = Strings.MobilePhoneLabel;
				break;
			case FoundByType.HomePhone:
				result = Strings.HomePhoneLabel;
				break;
			}
			return result;
		}

		// Token: 0x060005B2 RID: 1458 RVA: 0x00016D88 File Offset: 0x00014F88
		public static string FormatTelOrSipEntryAsAnchor(string anchor, string display, UMUriType uriType)
		{
			if (uriType == UMUriType.SipName)
			{
				return string.Concat(new string[]
				{
					"<a style=\"color: #3399ff; \" href=\"sip:",
					anchor,
					"\">",
					display,
					"</a>"
				});
			}
			return string.Concat(new string[]
			{
				"<a style=\"color: #3399ff; \" href=\"tel:",
				anchor,
				"\">",
				display,
				"</a>"
			});
		}

		// Token: 0x060005B3 RID: 1459 RVA: 0x00016DF4 File Offset: 0x00014FF4
		public static string FormatCallerId(PhoneNumber callerId, CultureInfo culture)
		{
			if (callerId.IsEmpty)
			{
				return Strings.AnonymousCaller.ToString(culture);
			}
			return callerId.ToDisplay;
		}

		// Token: 0x060005B4 RID: 1460 RVA: 0x00016E20 File Offset: 0x00015020
		public static string FormatCallerIdWithAnchor(PhoneNumber callerId, CultureInfo culture)
		{
			if (callerId.IsEmpty)
			{
				return Strings.AnonymousCaller.ToString(culture);
			}
			return MessageContentBuilder.FormatTelOrSipEntryAsAnchor(callerId.ToDial, callerId.ToDisplay, callerId.UriType);
		}

		// Token: 0x060005B5 RID: 1461 RVA: 0x00016E5B File Offset: 0x0001505B
		protected MessageContentBuilder(CultureInfo culture, UMDialPlan rcptDialPlan)
		{
			this.culture = culture;
			this.sb = new StringBuilder();
			this.RecipientDialPlan = rcptDialPlan;
		}

		// Token: 0x060005B6 RID: 1462 RVA: 0x00016E7C File Offset: 0x0001507C
		protected MessageContentBuilder(CultureInfo culture) : this(culture, null)
		{
		}

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x060005B7 RID: 1463 RVA: 0x00016E86 File Offset: 0x00015086
		internal virtual string Charset
		{
			get
			{
				return "utf-8";
			}
		}

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x060005B8 RID: 1464 RVA: 0x00016E8D File Offset: 0x0001508D
		// (set) Token: 0x060005B9 RID: 1465 RVA: 0x00016E95 File Offset: 0x00015095
		protected UMDialPlan RecipientDialPlan { get; set; }

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x060005BA RID: 1466 RVA: 0x00016E9E File Offset: 0x0001509E
		// (set) Token: 0x060005BB RID: 1467 RVA: 0x00016EA6 File Offset: 0x000150A6
		protected CultureInfo Culture
		{
			get
			{
				return this.culture;
			}
			set
			{
				this.culture = value;
			}
		}

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x060005BC RID: 1468
		protected abstract string EmailHeaderLine { get; }

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x060005BD RID: 1469
		protected abstract string CalendarHeaderLine { get; }

		// Token: 0x060005BE RID: 1470 RVA: 0x00016EAF File Offset: 0x000150AF
		public override string ToString()
		{
			return this.sb.ToString();
		}

		// Token: 0x060005BF RID: 1471 RVA: 0x00016EBC File Offset: 0x000150BC
		internal static MessageContentBuilder Create(CultureInfo culture, UMDialPlan rcptDialPlan)
		{
			return new HtmlContentBuilder(culture, rcptDialPlan);
		}

		// Token: 0x060005C0 RID: 1472 RVA: 0x00016EC5 File Offset: 0x000150C5
		internal static MessageContentBuilder Create(CultureInfo culture)
		{
			return new HtmlContentBuilder(culture, null);
		}

		// Token: 0x060005C1 RID: 1473 RVA: 0x00016ED0 File Offset: 0x000150D0
		internal virtual void AddEmailHeader(MessageItem original)
		{
			string text = null;
			string headerValue = null;
			string displayName = original.From.DisplayName;
			string headerValue2 = original.SentTime.ToString("f", this.culture);
			string subject = original.Subject;
			XsoUtil.BuildParticipantStrings(original.Recipients, out headerValue, out text);
			string entryName = this.FormatHeaderName(Strings.FromHeader);
			string entryName2 = this.FormatHeaderName(Strings.SentHeader);
			string entryName3 = this.FormatHeaderName(Strings.ToHeader);
			string entryName4 = (text.Length > 0) ? this.FormatHeaderName(Strings.CcHeader) : string.Empty;
			string entryName5 = this.FormatHeaderName(Strings.SubjectHeader);
			this.AppendCultureInvariantText(this.EmailHeaderLine);
			this.Append(Strings.HeaderEntry(entryName, this.FormatHeaderValue(displayName)));
			this.AddNewLine();
			this.Append(Strings.HeaderEntry(entryName2, this.FormatHeaderValue(headerValue2)));
			this.AddNewLine();
			this.Append(Strings.HeaderEntry(entryName3, this.FormatHeaderValue(headerValue)));
			this.AddNewLine();
			if (text.Length > 0)
			{
				this.Append(Strings.HeaderEntry(entryName4, this.FormatHeaderValue(text)));
				this.AddNewLine();
			}
			this.Append(Strings.HeaderEntry(entryName5, this.FormatHeaderValue(subject)));
			this.AddNewLine();
			this.AddNewLine();
		}

		// Token: 0x060005C2 RID: 1474 RVA: 0x00017010 File Offset: 0x00015210
		internal virtual void AddCalendarHeader(CalendarItemBase cal, ExTimeZone timezone, bool shouldAddLine)
		{
			string text = null;
			string headerValue = null;
			cal.Load(new PropertyDefinition[]
			{
				ItemSchema.SentTime,
				CalendarItemBaseSchema.OrganizerEmailAddress,
				ItemSchema.SentRepresentingDisplayName,
				ItemSchema.Subject,
				CalendarItemBaseSchema.Location,
				CalendarItemInstanceSchema.StartTime,
				CalendarItemInstanceSchema.EndTime
			});
			object obj = XsoUtil.SafeGetProperty(cal, ItemSchema.SentTime, null);
			string headerValue2 = (obj != null) ? ((ExDateTime)obj).ToString("f", this.culture) : string.Empty;
			string defaultValue = (string)XsoUtil.SafeGetProperty(cal, CalendarItemBaseSchema.OrganizerEmailAddress, string.Empty);
			string headerValue3 = (string)XsoUtil.SafeGetProperty(cal, ItemSchema.SentRepresentingDisplayName, defaultValue);
			string subject = cal.Subject;
			string location = cal.Location;
			string headerValue4 = this.BuildQualifiedMeetingTimeString(cal.StartTime, cal.EndTime, timezone);
			XsoUtil.BuildParticipantStrings(cal.AttendeeCollection, out headerValue, out text);
			string entryName = this.FormatHeaderName(Strings.FromHeader);
			string entryName2 = this.FormatHeaderName(Strings.SentHeader);
			string entryName3 = this.FormatHeaderName(Strings.ToHeader);
			string entryName4 = this.FormatHeaderName(Strings.SubjectHeader);
			string entryName5 = this.FormatHeaderName(Strings.WhenHeader);
			string entryName6 = this.FormatHeaderName(Strings.WhereHeader);
			if (shouldAddLine)
			{
				this.AppendCultureInvariantText(this.CalendarHeaderLine);
			}
			this.Append(Strings.HeaderEntry(entryName, this.FormatHeaderValue(headerValue3)));
			this.AddNewLine();
			this.Append(Strings.HeaderEntry(entryName2, this.FormatHeaderValue(headerValue2)));
			this.AddNewLine();
			this.Append(Strings.HeaderEntry(entryName3, this.FormatHeaderValue(headerValue)));
			this.AddNewLine();
			this.Append(Strings.HeaderEntry(entryName4, this.FormatHeaderValue(subject)));
			this.AddNewLine();
			this.Append(Strings.HeaderEntry(entryName5, this.FormatHeaderValue(headerValue4)));
			this.AddNewLine();
			this.Append(Strings.HeaderEntry(entryName6, this.FormatHeaderValue(location)));
			this.AddNewLine();
			this.AddNewLine();
		}

		// Token: 0x060005C3 RID: 1475 RVA: 0x00017204 File Offset: 0x00015404
		internal virtual MemoryStream ToStream()
		{
			return new MemoryStream(Encoding.UTF8.GetBytes(this.sb.ToString()));
		}

		// Token: 0x060005C4 RID: 1476
		internal abstract void AddVoicemailBody(PhoneNumber callerId, ContactInfo resolvedCallerInfo, string additionalText, ITranscriptionData transcriptionData);

		// Token: 0x060005C5 RID: 1477
		internal abstract void AddNDRBodyForCADRM(PhoneNumber callerId, ContactInfo resolvedCallerInfo, ExDateTime sentTime);

		// Token: 0x060005C6 RID: 1478
		internal abstract void AddVoicemailBody(ContactInfo resolvedCallerInfo, string additionalText);

		// Token: 0x060005C7 RID: 1479
		internal abstract void AddNDRBodyForInterpersonalDRM(UMSubscriber caller, RecipientDetails recipients, ExDateTime sentTime);

		// Token: 0x060005C8 RID: 1480
		internal abstract void AddFaxBody(PhoneNumber callerId, ContactInfo resolvedCallerInfo, string additionalText);

		// Token: 0x060005C9 RID: 1481
		internal abstract void AddMissedCallBody(PhoneNumber callerId, ContactInfo resolvedCallerInfo);

		// Token: 0x060005CA RID: 1482
		internal abstract void AddTeamPickUpBody(string answeredBy, PhoneNumber callerId, ContactInfo callerInfo);

		// Token: 0x060005CB RID: 1483
		internal abstract void AddCallNotForwardedBody(string target, PhoneNumber callerId, ContactInfo callerInfo);

		// Token: 0x060005CC RID: 1484
		internal abstract void AddCallForwardedBody(string target, PhoneNumber callerId, ContactInfo callerInfo);

		// Token: 0x060005CD RID: 1485
		internal abstract void AddIncomingCallLogBody(PhoneNumber callerId, ContactInfo callerInfo);

		// Token: 0x060005CE RID: 1486
		internal abstract void AddOutgoingCallLogBody(PhoneNumber targetPhone, ContactInfo calledPartyInfo);

		// Token: 0x060005CF RID: 1487
		internal abstract void AddEnterpriseNotifyMailBody(LocalizedString messageHeader, string[] accessNumbers, string extension, string pin, string additionalText);

		// Token: 0x060005D0 RID: 1488
		internal abstract void AddConsumerNotifyMailBody(LocalizedString messageHeader, string[] accessNumbers, string extension, string pin, string additionalText);

		// Token: 0x060005D1 RID: 1489
		internal abstract void AddAudioPreview(ITranscriptionData transcriptionData);

		// Token: 0x060005D2 RID: 1490 RVA: 0x00017220 File Offset: 0x00015420
		internal virtual string GetDisplayNameOrCallerId(ContactInfo callerInfo, PhoneNumber callerId)
		{
			return this.GetDisplayName(callerInfo) ?? MessageContentBuilder.FormatCallerId(callerId, this.Culture);
		}

		// Token: 0x060005D3 RID: 1491 RVA: 0x00017239 File Offset: 0x00015439
		internal virtual string GetDisplayName(ContactInfo callerInfo)
		{
			return callerInfo.DisplayName ?? callerInfo.EMailAddress;
		}

		// Token: 0x060005D4 RID: 1492 RVA: 0x0001724C File Offset: 0x0001544C
		internal virtual string GetFaxSubject(ContactInfo callerInfo, PhoneNumber callerId, uint pageCount, bool incomplete)
		{
			this.GetDisplayNameOrCallerId(callerInfo, callerId);
			LocalizedString autoGenSubject;
			if (!incomplete)
			{
				if (pageCount > 1U)
				{
					autoGenSubject = Strings.FaxMailSubjectInPages(pageCount);
				}
				else
				{
					autoGenSubject = Strings.FaxMailSubjectInPage;
				}
			}
			else if (pageCount > 1U)
			{
				autoGenSubject = Strings.IncompleteFaxMailSubjectInPages(pageCount);
			}
			else
			{
				autoGenSubject = Strings.IncompleteFaxMailSubjectInPage;
			}
			return this.GenerateSubject(autoGenSubject, null);
		}

		// Token: 0x060005D5 RID: 1493 RVA: 0x00017298 File Offset: 0x00015498
		internal virtual string GetNDRSubjectForInterpersonalDRM()
		{
			return Strings.InterpersonalNDRForDRMSubject.ToString(this.culture);
		}

		// Token: 0x060005D6 RID: 1494 RVA: 0x000172B8 File Offset: 0x000154B8
		internal virtual string GetNDRSubjectForCallAnsweringDRM()
		{
			return Strings.CallAnsweringNDRForDRMSubject.ToString(this.culture);
		}

		// Token: 0x060005D7 RID: 1495 RVA: 0x000172D8 File Offset: 0x000154D8
		internal virtual string GetVoicemailSubject(string durationString, string originalSubject)
		{
			LocalizedString autoGenSubject = Strings.VoicemailSubject(durationString);
			return this.GenerateSubject(autoGenSubject, originalSubject);
		}

		// Token: 0x060005D8 RID: 1496 RVA: 0x000172F4 File Offset: 0x000154F4
		internal virtual string GetMissedCallSubject(string originalSubject)
		{
			LocalizedString missedCallSubject = Strings.MissedCallSubject;
			return this.GenerateSubject(missedCallSubject, originalSubject);
		}

		// Token: 0x060005D9 RID: 1497 RVA: 0x00017310 File Offset: 0x00015510
		internal virtual string GetTeamPickUpSubject(ContactInfo callerInfo, PhoneNumber callerId, string answeredBy, string originalSubject)
		{
			LocalizedString phoneLabel = MessageContentBuilder.GetPhoneLabel(callerInfo);
			string displayNameOrCallerId = this.GetDisplayNameOrCallerId(callerInfo, callerId);
			LocalizedString autoGenSubject;
			if (!phoneLabel.IsEmpty)
			{
				autoGenSubject = Strings.TeamPickUpSubjectWithLabel(displayNameOrCallerId, phoneLabel, answeredBy);
			}
			else
			{
				autoGenSubject = Strings.TeamPickUpSubject(displayNameOrCallerId, answeredBy);
			}
			return this.GenerateSubject(autoGenSubject, originalSubject);
		}

		// Token: 0x060005DA RID: 1498 RVA: 0x00017354 File Offset: 0x00015554
		internal virtual string GetCallForwardedSubject(ContactInfo callerInfo, PhoneNumber callerId, string target, string originalSubject)
		{
			LocalizedString phoneLabel = MessageContentBuilder.GetPhoneLabel(callerInfo);
			string displayNameOrCallerId = this.GetDisplayNameOrCallerId(callerInfo, callerId);
			LocalizedString autoGenSubject;
			if (!phoneLabel.IsEmpty)
			{
				autoGenSubject = Strings.CallForwardedSubjectWithLabel(displayNameOrCallerId, phoneLabel, target);
			}
			else
			{
				autoGenSubject = Strings.CallForwardedSubject(displayNameOrCallerId, target);
			}
			return this.GenerateSubject(autoGenSubject, originalSubject);
		}

		// Token: 0x060005DB RID: 1499 RVA: 0x00017398 File Offset: 0x00015598
		internal virtual string GetCallNotForwardedSubject(ContactInfo callerInfo, PhoneNumber callerId, string target, string originalSubject)
		{
			string displayNameOrCallerId = this.GetDisplayNameOrCallerId(callerInfo, callerId);
			LocalizedString phoneLabel = MessageContentBuilder.GetPhoneLabel(callerInfo);
			LocalizedString autoGenSubject;
			if (!phoneLabel.IsEmpty)
			{
				autoGenSubject = Strings.CallNotForwardedSubjectWithLabel(displayNameOrCallerId, phoneLabel, target);
			}
			else
			{
				autoGenSubject = Strings.CallNotForwardedSubject(displayNameOrCallerId, target);
			}
			return this.GenerateSubject(autoGenSubject, originalSubject);
		}

		// Token: 0x060005DC RID: 1500 RVA: 0x000173DC File Offset: 0x000155DC
		internal virtual string GetIncomingCallLogSubject(ContactInfo callerInfo, PhoneNumber callerId)
		{
			LocalizedString phoneLabel = MessageContentBuilder.GetPhoneLabel(callerInfo);
			string displayNameOrCallerId = this.GetDisplayNameOrCallerId(callerInfo, callerId);
			LocalizedString autoGenSubject;
			if (!phoneLabel.IsEmpty)
			{
				autoGenSubject = Strings.IncomingCallLogSubjectWithLabel(displayNameOrCallerId, phoneLabel);
			}
			else
			{
				autoGenSubject = Strings.IncomingCallLogSubject(displayNameOrCallerId);
			}
			return this.GenerateSubject(autoGenSubject, null);
		}

		// Token: 0x060005DD RID: 1501 RVA: 0x0001741C File Offset: 0x0001561C
		internal virtual string GetOutgoingCallLogSubject(ContactInfo calledPartyInfo, PhoneNumber targetPhone)
		{
			LocalizedString phoneLabel = MessageContentBuilder.GetPhoneLabel(calledPartyInfo);
			string displayNameOrCallerId = this.GetDisplayNameOrCallerId(calledPartyInfo, targetPhone);
			LocalizedString autoGenSubject;
			if (!phoneLabel.IsEmpty)
			{
				autoGenSubject = Strings.OutgoingCallLogSubjectWithLabel(displayNameOrCallerId, phoneLabel);
			}
			else
			{
				autoGenSubject = Strings.OutgoingCallLogSubject(displayNameOrCallerId);
			}
			return this.GenerateSubject(autoGenSubject, null);
		}

		// Token: 0x060005DE RID: 1502
		internal abstract void AddLateForMeetingBody(CalendarItemBase cal, ExTimeZone timeZone, LocalizedString lateInfo);

		// Token: 0x060005DF RID: 1503
		internal abstract void AddRecordedReplyText(string displayName);

		// Token: 0x060005E0 RID: 1504
		internal abstract void AddRecordedForwardText(string displayName);

		// Token: 0x060005E1 RID: 1505
		protected abstract void AddNewLine();

		// Token: 0x060005E2 RID: 1506
		protected abstract void AddDivider();

		// Token: 0x060005E3 RID: 1507
		protected abstract void AddDocumentEnd();

		// Token: 0x060005E4 RID: 1508
		protected abstract void AddStart();

		// Token: 0x060005E5 RID: 1509 RVA: 0x0001745B File Offset: 0x0001565B
		protected void Append(LocalizedString message)
		{
			this.sb.Append(message.ToString(this.Culture));
		}

		// Token: 0x060005E6 RID: 1510 RVA: 0x00017476 File Offset: 0x00015676
		protected void AppendCultureInvariantText(string message)
		{
			this.sb.Append(message);
		}

		// Token: 0x060005E7 RID: 1511 RVA: 0x00017488 File Offset: 0x00015688
		protected void AppendFormat(string format, params object[] args)
		{
			if (args != null)
			{
				for (int i = 0; i < args.Length; i++)
				{
					if (args[i] is LocalizedString)
					{
						args[i] = ((LocalizedString)args[i]).ToString(this.Culture);
					}
				}
			}
			this.sb.AppendFormat(CultureInfo.InvariantCulture, format, args);
		}

		// Token: 0x060005E8 RID: 1512 RVA: 0x000174DC File Offset: 0x000156DC
		protected string BuildQualifiedMeetingTimeString(ExDateTime startTime, ExDateTime endTime, ExTimeZone timezone)
		{
			string format = "f";
			string format2 = "t";
			string start = startTime.ToString(format, this.culture);
			string end = (startTime.Day == endTime.Day) ? endTime.ToString(format2, this.culture) : endTime.ToString(format, this.culture);
			string timezone2 = timezone.LocalizableDisplayName.ToString(this.Culture);
			return Strings.QualifiedMeetingTime(start, end, timezone2).ToString(this.culture);
		}

		// Token: 0x060005E9 RID: 1513
		protected abstract string FormatHeaderName(LocalizedString headerName);

		// Token: 0x060005EA RID: 1514
		protected abstract string FormatHeaderValue(string headerValue);

		// Token: 0x060005EB RID: 1515 RVA: 0x00017564 File Offset: 0x00015764
		private string GenerateSubject(LocalizedString autoGenSubject, string originalSubject)
		{
			if (!string.IsNullOrEmpty(originalSubject))
			{
				return Strings.AutogeneratedPlusOriginalSubject(autoGenSubject, originalSubject).ToString(this.culture);
			}
			return autoGenSubject.ToString(this.culture);
		}

		// Token: 0x0400037C RID: 892
		public const string AnchorStyle = "color: #3399ff; ";

		// Token: 0x0400037D RID: 893
		private CultureInfo culture;

		// Token: 0x0400037E RID: 894
		private StringBuilder sb;
	}
}
