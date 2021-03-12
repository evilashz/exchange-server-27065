using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000400 RID: 1024
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class RumDecorator
	{
		// Token: 0x06002EB9 RID: 11961 RVA: 0x000C0799 File Offset: 0x000BE999
		private RumDecorator()
		{
		}

		// Token: 0x06002EBA RID: 11962 RVA: 0x000C07A4 File Offset: 0x000BE9A4
		internal static RumDecorator CreateInstance(RumInfo info)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			RumDecorator rumDecorator = new RumDecorator();
			rumDecorator.Initialize(info);
			return rumDecorator;
		}

		// Token: 0x06002EBB RID: 11963 RVA: 0x000C07D0 File Offset: 0x000BE9D0
		internal void AdjustRumMessage(MailboxSession mailboxSession, MessageItem message, RumInfo rumInfo, string textToInject, bool skipBodyUpdate = false)
		{
			this.AdjustInfoTypeSpecificProperties(message, textToInject, skipBodyUpdate);
			AppointmentAuxiliaryFlags? valueAsNullable = message.GetValueAsNullable<AppointmentAuxiliaryFlags>(MeetingMessageSchema.AppointmentAuxiliaryFlags);
			message[MeetingMessageSchema.AppointmentAuxiliaryFlags] = ((valueAsNullable != null) ? (valueAsNullable.Value | AppointmentAuxiliaryFlags.RepairUpdateMessage) : AppointmentAuxiliaryFlags.RepairUpdateMessage);
			message.Sender = this.InternalGetRumSender(mailboxSession);
			message.From = new Participant(mailboxSession.MailboxOwner);
		}

		// Token: 0x06002EBC RID: 11964 RVA: 0x000C083C File Offset: 0x000BEA3C
		private void AdjustInfoTypeSpecificProperties(MessageItem message, string textToInject, bool skipBodyUpdate = false)
		{
			if (message == null)
			{
				throw new ArgumentNullException("message");
			}
			if (this.info is MissingAttendeeItemRumInfo)
			{
				this.AssertMessageTypeMatch(message is MeetingRequest, message);
				MissingAttendeeItemRumInfo missingAttendeeItemRumInfo = (MissingAttendeeItemRumInfo)this.info;
				message[MeetingMessageSchema.ItemVersion] = missingAttendeeItemRumInfo.DeletedItemVersion;
				if (!skipBodyUpdate)
				{
					this.DecorateMissingItemRumBody(message, textToInject, missingAttendeeItemRumInfo);
					return;
				}
				return;
			}
			else
			{
				if (this.info is UpdateRumInfo)
				{
					this.AssertMessageTypeMatch(message is MeetingRequest, message);
					this.DecorateGeneralUpdateRumBody(message, textToInject, (UpdateRumInfo)this.info);
					return;
				}
				if (this.info is CancellationRumInfo)
				{
					this.AssertMessageTypeMatch(message is MeetingCancellation, message);
					this.DecorateCancellationRumBody(message, textToInject, (CancellationRumInfo)this.info);
					return;
				}
				if (this.info is ResponseRumInfo)
				{
					this.AssertMessageTypeMatch(message is MeetingResponse, message);
					this.DecorateResponseRumBody(message, textToInject, (ResponseRumInfo)this.info);
					return;
				}
				if (this.info is AttendeeInquiryRumInfo)
				{
					this.AssertMessageTypeMatch(message is MeetingInquiryMessage, message);
					this.DecorateAttendeeInquiryRumBody(message, textToInject, (AttendeeInquiryRumInfo)this.info);
					return;
				}
				throw new NotSupportedException(string.Format("Rum type '{0}' is not supported.", this.info.GetType().Name));
			}
		}

		// Token: 0x06002EBD RID: 11965 RVA: 0x000C098A File Offset: 0x000BEB8A
		private void Initialize(RumInfo info)
		{
			this.info = info;
		}

		// Token: 0x06002EBE RID: 11966 RVA: 0x000C0993 File Offset: 0x000BEB93
		private void AssertMessageTypeMatch(bool condition, MessageItem message)
		{
			this.info.GetType();
			message.GetType();
		}

		// Token: 0x06002EBF RID: 11967 RVA: 0x000C09A8 File Offset: 0x000BEBA8
		private void ProjectInconsistencyGroup(MessageItem message, CalendarInconsistencyGroup group, HashSet<CalendarInconsistencyGroup> projectedGroups, List<string> projection)
		{
			if (!projectedGroups.Contains(group))
			{
				switch (group)
				{
				case CalendarInconsistencyGroup.StartTime:
					projection.Add(this.GetLocalizedString(ClientStrings.UpdateRumStartTimeFlag, message));
					break;
				case CalendarInconsistencyGroup.EndTime:
					projection.Add(this.GetLocalizedString(ClientStrings.UpdateRumEndTimeFlag, message));
					break;
				case CalendarInconsistencyGroup.Recurrence:
					projection.Add(this.GetLocalizedString(ClientStrings.UpdateRumRecurrenceFlag, message));
					break;
				case CalendarInconsistencyGroup.Location:
					projection.Add(this.GetLocalizedString(ClientStrings.UpdateRumLocationFlag, message));
					break;
				case CalendarInconsistencyGroup.Cancellation:
					projection.Add(this.GetLocalizedString(ClientStrings.UpdateRumCancellationFlag, message));
					break;
				case CalendarInconsistencyGroup.MissingItem:
					projection.Add(this.GetLocalizedString(ClientStrings.UpdateRumMissingItemFlag, message));
					break;
				case CalendarInconsistencyGroup.Duplicate:
					projection.Add(this.GetLocalizedString(ClientStrings.UpdateRumDuplicateFlags, message));
					break;
				}
				projectedGroups.Add(group);
			}
		}

		// Token: 0x06002EC0 RID: 11968 RVA: 0x000C0A88 File Offset: 0x000BEC88
		private void DecorateMissingItemRumBody(MessageItem message, string textToInject, MissingAttendeeItemRumInfo info)
		{
			string localizedString = this.GetLocalizedString(ClientStrings.MissingItemRumDescription("2013"), message);
			this.DecorateRumBody(message, textToInject, localizedString, null);
		}

		// Token: 0x06002EC1 RID: 11969 RVA: 0x000C0AB4 File Offset: 0x000BECB4
		private void DecorateGeneralUpdateRumBody(MessageItem message, string textToInject, UpdateRumInfo info)
		{
			KeyValuePair<string, List<string>> item = new KeyValuePair<string, List<string>>(this.GetLocalizedString(ClientStrings.UpdateRumInconsistencyFlagsLabel, message), new List<string>());
			HashSet<CalendarInconsistencyGroup> projectedGroups = new HashSet<CalendarInconsistencyGroup>();
			foreach (CalendarInconsistencyFlag flag in info.InconsistencyFlagList)
			{
				this.ProjectInconsistencyFlag(message, item.Value, projectedGroups, flag);
			}
			List<KeyValuePair<string, List<string>>> list;
			if (item.Value.Count == 0)
			{
				list = null;
			}
			else
			{
				list = new List<KeyValuePair<string, List<string>>>(1);
				list.Add(item);
			}
			this.DecorateRumBody(message, textToInject, this.GetLocalizedString(ClientStrings.UpdateRumDescription, message), list);
		}

		// Token: 0x06002EC2 RID: 11970 RVA: 0x000C0B64 File Offset: 0x000BED64
		private void ProjectInconsistencyFlag(MessageItem message, List<string> projection, HashSet<CalendarInconsistencyGroup> projectedGroups, CalendarInconsistencyFlag flag)
		{
			switch (flag)
			{
			default:
				this.ProjectInconsistencyGroup(message, CalendarInconsistencyGroup.None, projectedGroups, projection);
				return;
			case CalendarInconsistencyFlag.TimeOverlap:
				this.ProjectInconsistencyGroup(message, CalendarInconsistencyGroup.StartTime, projectedGroups, projection);
				this.ProjectInconsistencyGroup(message, CalendarInconsistencyGroup.EndTime, projectedGroups, projection);
				return;
			case CalendarInconsistencyFlag.StartTime:
			case CalendarInconsistencyFlag.StartTimeZone:
				this.ProjectInconsistencyGroup(message, CalendarInconsistencyGroup.StartTime, projectedGroups, projection);
				return;
			case CalendarInconsistencyFlag.EndTime:
				this.ProjectInconsistencyGroup(message, CalendarInconsistencyGroup.EndTime, projectedGroups, projection);
				return;
			case CalendarInconsistencyFlag.RecurringTimeZone:
			case CalendarInconsistencyFlag.RecurrenceBlob:
			case CalendarInconsistencyFlag.RecurrenceAnomaly:
			case CalendarInconsistencyFlag.RecurringException:
			case CalendarInconsistencyFlag.ModifiedOccurrenceMatch:
			case CalendarInconsistencyFlag.MissingOccurrenceDeletion:
			case CalendarInconsistencyFlag.ExtraOccurrenceDeletion:
				this.ProjectInconsistencyGroup(message, CalendarInconsistencyGroup.Recurrence, projectedGroups, projection);
				return;
			case CalendarInconsistencyFlag.Location:
				this.ProjectInconsistencyGroup(message, CalendarInconsistencyGroup.Location, projectedGroups, projection);
				return;
			case CalendarInconsistencyFlag.Cancellation:
				this.ProjectInconsistencyGroup(message, CalendarInconsistencyGroup.Cancellation, projectedGroups, projection);
				return;
			case CalendarInconsistencyFlag.MissingItem:
				this.ProjectInconsistencyGroup(message, CalendarInconsistencyGroup.MissingItem, projectedGroups, projection);
				return;
			case CalendarInconsistencyFlag.DuplicatedItem:
				this.ProjectInconsistencyGroup(message, CalendarInconsistencyGroup.Duplicate, projectedGroups, projection);
				return;
			}
		}

		// Token: 0x06002EC3 RID: 11971 RVA: 0x000C0C4A File Offset: 0x000BEE4A
		private void DecorateCancellationRumBody(MessageItem message, string textToInject, CancellationRumInfo info)
		{
			this.DecorateRumBody(message, textToInject, this.GetLocalizedString(ClientStrings.CancellationRumDescription, message), null);
		}

		// Token: 0x06002EC4 RID: 11972 RVA: 0x000C0C61 File Offset: 0x000BEE61
		private void DecorateAttendeeInquiryRumBody(MessageItem message, string textToInject, AttendeeInquiryRumInfo info)
		{
			this.DecorateRumBody(message, textToInject, this.GetLocalizedString(ClientStrings.AttendeeInquiryRumDescription, message), null);
		}

		// Token: 0x06002EC5 RID: 11973 RVA: 0x000C0C78 File Offset: 0x000BEE78
		private void DecorateResponseRumBody(MessageItem message, string textToInject, ResponseRumInfo info)
		{
			this.DecorateRumBody(message, textToInject, string.Empty, null);
		}

		// Token: 0x06002EC6 RID: 11974 RVA: 0x000C0C88 File Offset: 0x000BEE88
		private void DecorateRumBody(MessageItem message, string textToInject, string description, List<KeyValuePair<string, List<string>>> details)
		{
			string arg = string.Format("<p><font color='#000000' size='2' face='Tahoma'>{0}</font></p>", description);
			string arg2 = string.Empty;
			if (details != null && details.Count != 0)
			{
				StringBuilder stringBuilder = new StringBuilder();
				foreach (KeyValuePair<string, List<string>> keyValuePair in details)
				{
					StringBuilder stringBuilder2 = new StringBuilder();
					foreach (string arg3 in keyValuePair.Value)
					{
						stringBuilder2.AppendFormat("<tr><td width='29'>&nbsp;</td><td><font color='#000000' size='2' face='Tahoma'>{0}</font></td></tr>", arg3);
					}
					stringBuilder.AppendFormat("<table border='0' cellpadding='2'><tr><td width='29'>&nbsp;</td><td><b><font color='#808080' size='2' face='Tahoma'>{0}</font></b></td></tr>{1}</table><br />", keyValuePair.Key, stringBuilder2);
				}
				arg2 = stringBuilder.ToString();
			}
			string arg4 = string.Format("<hr/><font color='#808080' size='1' face='Arial'>{0}</font>", this.GetLocalizedString(ClientStrings.RumFooter("2013"), message));
			BodyWriteConfiguration bodyWriteConfiguration = new BodyWriteConfiguration(BodyFormat.TextHtml);
			if (!string.IsNullOrEmpty(textToInject))
			{
				bodyWriteConfiguration.AddInjectedText(textToInject, null, BodyInjectionFormat.Text);
			}
			using (TextWriter textWriter = message.Body.OpenTextWriter(bodyWriteConfiguration))
			{
				textWriter.Write(string.Format("<html><body>{0}{1}{2}</body></html>", arg, arg2, arg4));
			}
		}

		// Token: 0x06002EC7 RID: 11975 RVA: 0x000C0DE4 File Offset: 0x000BEFE4
		private string GetLocalizedString(LocalizedString source, MessageItem message)
		{
			return source.ToString(message.Session.InternalCulture);
		}

		// Token: 0x06002EC8 RID: 11976 RVA: 0x000C0DF8 File Offset: 0x000BEFF8
		private Participant InternalGetRumSender(MailboxSession mailboxSession)
		{
			IConfigurationSession adconfigurationSession = mailboxSession.GetADConfigurationSession(true, ConsistencyMode.PartiallyConsistent);
			return new Participant(adconfigurationSession.FindMicrosoftExchangeRecipient());
		}

		// Token: 0x0400198D RID: 6541
		private const string ExchangeVersionNumber = "2013";

		// Token: 0x0400198E RID: 6542
		private const string HtmlBodyFormatString = "<html><body>{0}{1}{2}</body></html>";

		// Token: 0x0400198F RID: 6543
		private const string DescriptionFormatString = "<p><font color='#000000' size='2' face='Tahoma'>{0}</font></p>";

		// Token: 0x04001990 RID: 6544
		private const string DetailsFormatString = "<table border='0' cellpadding='2'><tr><td width='29'>&nbsp;</td><td><b><font color='#808080' size='2' face='Tahoma'>{0}</font></b></td></tr>{1}</table><br />";

		// Token: 0x04001991 RID: 6545
		private const string DetailRowFormatString = "<tr><td width='29'>&nbsp;</td><td><font color='#000000' size='2' face='Tahoma'>{0}</font></td></tr>";

		// Token: 0x04001992 RID: 6546
		private const string FooterFormatString = "<hr/><font color='#808080' size='1' face='Arial'>{0}</font>";

		// Token: 0x04001993 RID: 6547
		private RumInfo info;
	}
}
