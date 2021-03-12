using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Clients.Owa.Core.Controls;
using Microsoft.Exchange.Clients.Owa.Premium.Controls;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Security;

namespace Microsoft.Exchange.Clients.Owa.Premium
{
	// Token: 0x020004EC RID: 1260
	internal static class CalendarUtilities
	{
		// Token: 0x06002FCD RID: 12237 RVA: 0x001163AC File Offset: 0x001145AC
		static CalendarUtilities()
		{
			CalendarUtilities.PrintQueryProperties = new PropertyDefinition[CalendarUtilities.QueryProperties.Length + CalendarUtilities.AdditionalQueryPropertiesForPrint.Length];
			CalendarUtilities.QueryProperties.CopyTo(CalendarUtilities.PrintQueryProperties, 0);
			CalendarUtilities.AdditionalQueryPropertiesForPrint.CopyTo(CalendarUtilities.PrintQueryProperties, CalendarUtilities.QueryProperties.Length);
		}

		// Token: 0x06002FCE RID: 12238 RVA: 0x00116524 File Offset: 0x00114724
		public static void RenderBusyTypeDropdownList(TextWriter output, Item item, bool forceDisable)
		{
			if (output == null)
			{
				throw new ArgumentNullException("output");
			}
			BusyType busyType = BusyType.Busy;
			if (item != null)
			{
				object obj = item.TryGetProperty(CalendarItemBaseSchema.FreeBusyStatus);
				if (obj is int)
				{
					busyType = (BusyType)obj;
				}
			}
			new BusyTypeDropDownList("divBsyType", busyType)
			{
				Enabled = !forceDisable
			}.Render(output);
		}

		// Token: 0x06002FCF RID: 12239 RVA: 0x0011657C File Offset: 0x0011477C
		public static void RenderReminderDropdownList(TextWriter output, Item item, bool isReminderSet, bool forceDisable)
		{
			if (output == null)
			{
				throw new ArgumentNullException("output");
			}
			UserContext userContext = UserContextManager.GetUserContext();
			double reminderTime = (double)userContext.CalendarSettings.DefaultReminderTime;
			if (item != null && item.TryGetProperty(ItemSchema.ReminderMinutesBeforeStart) is int)
			{
				int num = (int)item[ItemSchema.ReminderMinutesBeforeStart];
				if (num >= 0 && num <= 5040000)
				{
					reminderTime = (double)num;
				}
			}
			new ReminderDropDownList("divRmdTime", reminderTime)
			{
				Enabled = (!forceDisable && isReminderSet)
			}.Render(output);
		}

		// Token: 0x06002FD0 RID: 12240 RVA: 0x00116600 File Offset: 0x00114800
		public static void AddCalendarInfobarMessages(Infobar infobar, CalendarItemBase calendarItemBase, MeetingMessage meetingMessage, UserContext userContext)
		{
			if (infobar == null)
			{
				throw new ArgumentNullException("infobar");
			}
			if (calendarItemBase == null)
			{
				throw new ArgumentNullException("calendarItemBase");
			}
			if (userContext == null)
			{
				throw new ArgumentNullException("userContext");
			}
			bool flag = calendarItemBase.IsOrganizer() && calendarItemBase.IsMeeting;
			ExDateTime localTime = DateTimeUtilities.GetLocalTime();
			bool flag2 = false;
			if (calendarItemBase.IsMeeting && calendarItemBase.IsCancelled)
			{
				infobar.AddMessage(-161808760, InfobarMessageType.Informational);
			}
			if (calendarItemBase.CalendarItemType == CalendarItemType.RecurringMaster)
			{
				CalendarItem calendarItem = (CalendarItem)calendarItemBase;
				if (calendarItem.Recurrence != null && !(calendarItem.Recurrence.Range is NoEndRecurrenceRange))
				{
					OccurrenceInfo lastOccurrence = calendarItem.Recurrence.GetLastOccurrence();
					if (lastOccurrence != null && lastOccurrence.EndTime < localTime)
					{
						infobar.AddMessage(-2124392108, InfobarMessageType.Informational);
						flag2 = true;
					}
				}
			}
			else if (calendarItemBase.EndTime < localTime)
			{
				flag2 = true;
				if (calendarItemBase.CalendarItemType != CalendarItemType.RecurringMaster)
				{
					infobar.AddMessage(-593429293, InfobarMessageType.Informational);
				}
			}
			InfobarMessageBuilder.AddFlag(infobar, calendarItemBase, userContext);
			if (flag)
			{
				if (calendarItemBase.MeetingRequestWasSent)
				{
					CalendarUtilities.AddAttendeeResponseCountMessage(infobar, calendarItemBase);
				}
				else
				{
					infobar.AddMessage(613373695, InfobarMessageType.Informational);
				}
			}
			if (!calendarItemBase.IsOrganizer() && calendarItemBase.IsMeeting)
			{
				bool flag3 = false;
				MeetingRequest meetingRequest = meetingMessage as MeetingRequest;
				if (meetingRequest != null)
				{
					flag3 = (meetingRequest.MeetingRequestType == MeetingMessageType.PrincipalWantsCopy);
				}
				if (calendarItemBase.ResponseType != ResponseType.NotResponded)
				{
					Strings.IDs? ds = null;
					Strings.IDs? ds2 = null;
					switch (calendarItemBase.ResponseType)
					{
					case ResponseType.Tentative:
						ds = new Strings.IDs?(-1859761232);
						ds2 = new Strings.IDs?(1365345389);
						break;
					case ResponseType.Accept:
						ds = new Strings.IDs?(-700793833);
						ds2 = new Strings.IDs?(-1153967082);
						break;
					case ResponseType.Decline:
						ds = new Strings.IDs?(-278420592);
						ds2 = new Strings.IDs?(2009978813);
						break;
					}
					if (ds != null)
					{
						ExDateTime property = ItemUtility.GetProperty<ExDateTime>(calendarItemBase, CalendarItemBaseSchema.AppointmentReplyTime, ExDateTime.MinValue);
						string text = Strings.None;
						string text2 = string.Empty;
						if (property != ExDateTime.MinValue)
						{
							text = property.ToString(userContext.UserOptions.DateFormat);
							text2 = property.ToString(userContext.UserOptions.TimeFormat);
						}
						string property2 = ItemUtility.GetProperty<string>(calendarItemBase, CalendarItemBaseSchema.AppointmentReplyName, string.Empty);
						SanitizedHtmlString messageHtml;
						if (string.Compare(property2, userContext.ExchangePrincipal.MailboxInfo.DisplayName, StringComparison.OrdinalIgnoreCase) == 0)
						{
							messageHtml = SanitizedHtmlString.Format(LocalizedStrings.GetHtmlEncoded(ds.Value), new object[]
							{
								text,
								text2
							});
						}
						else
						{
							messageHtml = SanitizedHtmlString.Format(LocalizedStrings.GetHtmlEncoded(ds2.Value), new object[]
							{
								property2,
								text,
								text2
							});
						}
						infobar.AddMessage(messageHtml, InfobarMessageType.Informational);
						return;
					}
				}
				else if (!flag2 && !calendarItemBase.IsCancelled)
				{
					if (!flag3)
					{
						bool property3 = ItemUtility.GetProperty<bool>(calendarItemBase, ItemSchema.IsResponseRequested, true);
						if (property3)
						{
							infobar.AddMessage(919273049, InfobarMessageType.Informational);
						}
						else
						{
							infobar.AddMessage(1602295502, InfobarMessageType.Informational);
						}
					}
					else
					{
						infobar.AddMessage(-200304859, InfobarMessageType.Informational);
					}
					CalendarUtilities.GetConflictingAppointments(infobar, calendarItemBase, userContext);
				}
			}
		}

		// Token: 0x06002FD1 RID: 12241 RVA: 0x00116914 File Offset: 0x00114B14
		private static void AddAttendeeResponseCountMessage(Infobar infobar, CalendarItemBase calendarItemBase)
		{
			SanitizedHtmlString attendeeResponseCountMessage = MeetingUtilities.GetAttendeeResponseCountMessage(calendarItemBase);
			infobar.AddMessage(attendeeResponseCountMessage, InfobarMessageType.Informational);
		}

		// Token: 0x06002FD2 RID: 12242 RVA: 0x00116930 File Offset: 0x00114B30
		private static void GetConflictingAppointments(Infobar infobar, CalendarItemBase calendarItemBase, UserContext userContext)
		{
			if (Utilities.IsPublic(calendarItemBase))
			{
				return;
			}
			using (CalendarFolder calendarFolder = CalendarFolder.Bind(calendarItemBase.Session as MailboxSession, DefaultFolderType.Calendar))
			{
				AdjacencyOrConflictInfo[] adjacentOrConflictingItems = calendarFolder.GetAdjacentOrConflictingItems(calendarItemBase);
				if (adjacentOrConflictingItems != null && adjacentOrConflictingItems.Length != 0)
				{
					if (Utilities.IsOtherMailbox(calendarItemBase))
					{
						CalendarUtilities.AddConflictingAppointmentsInfobarMessage(infobar, adjacentOrConflictingItems, userContext, calendarItemBase.CalendarItemType, Utilities.GetMailboxOwnerDisplayName((MailboxSession)calendarItemBase.Session), OwaStoreObjectId.CreateFromStoreObject(calendarFolder));
					}
					else
					{
						CalendarUtilities.AddConflictingAppointmentsInfobarMessage(infobar, adjacentOrConflictingItems, userContext, calendarItemBase.CalendarItemType);
					}
				}
			}
		}

		// Token: 0x06002FD3 RID: 12243 RVA: 0x001169C4 File Offset: 0x00114BC4
		private static void AddConflictingAppointmentsInfobarMessage(Infobar infobar, AdjacencyOrConflictInfo[] adjacencyOrConflictInfo, UserContext userContext, CalendarItemType calendarItemType)
		{
			CalendarUtilities.AddConflictingAppointmentsInfobarMessage(infobar, adjacencyOrConflictInfo, userContext, calendarItemType, null, null);
		}

		// Token: 0x06002FD4 RID: 12244 RVA: 0x001169D4 File Offset: 0x00114BD4
		private static void AddConflictingAppointmentsInfobarMessage(Infobar infobar, AdjacencyOrConflictInfo[] adjacencyOrConflictInfo, UserContext userContext, CalendarItemType calendarItemType, string receivedRepresentingDisplayName, OwaStoreObjectId folderId)
		{
			List<AdjacencyOrConflictInfo> list = new List<AdjacencyOrConflictInfo>();
			List<AdjacencyOrConflictInfo> list2 = new List<AdjacencyOrConflictInfo>();
			List<AdjacencyOrConflictInfo> list3 = new List<AdjacencyOrConflictInfo>();
			for (int i = 0; i < adjacencyOrConflictInfo.Length; i++)
			{
				AdjacencyOrConflictType adjacencyOrConflictType = adjacencyOrConflictInfo[i].AdjacencyOrConflictType;
				BusyType freeBusyStatus = adjacencyOrConflictInfo[i].FreeBusyStatus;
				if (freeBusyStatus != BusyType.Free)
				{
					if ((adjacencyOrConflictType & AdjacencyOrConflictType.Conflicts) != (AdjacencyOrConflictType)0)
					{
						list.Add(adjacencyOrConflictInfo[i]);
					}
					else if ((adjacencyOrConflictType & AdjacencyOrConflictType.Precedes) != (AdjacencyOrConflictType)0)
					{
						list2.Add(adjacencyOrConflictInfo[i]);
					}
					else if ((adjacencyOrConflictType & AdjacencyOrConflictType.Follows) != (AdjacencyOrConflictType)0)
					{
						list3.Add(adjacencyOrConflictInfo[i]);
					}
				}
			}
			if (list.Count == 0 && list2.Count == 0 && list3.Count == 0)
			{
				return;
			}
			SanitizedHtmlString sanitizedHtmlString = null;
			if (calendarItemType != CalendarItemType.RecurringMaster)
			{
				SanitizingStringBuilder<OwaHtml> sanitizingStringBuilder = new SanitizingStringBuilder<OwaHtml>();
				Strings.IDs localizedId = 1786149639;
				Strings.IDs localizedId2 = -669919370;
				if (list.Count > 0)
				{
					if (string.IsNullOrEmpty(receivedRepresentingDisplayName))
					{
						sanitizedHtmlString = SanitizedHtmlString.FromStringId(-812272237);
					}
					else
					{
						sanitizedHtmlString = SanitizedHtmlString.FromStringId(49937409);
					}
				}
				else if (list2.Count > 0 && list3.Count > 0)
				{
					if (string.IsNullOrEmpty(receivedRepresentingDisplayName))
					{
						sanitizedHtmlString = SanitizedHtmlString.FromStringId(2138994880);
					}
					else
					{
						sanitizedHtmlString = SanitizedHtmlString.FromStringId(-1207817018);
					}
					localizedId = -1877110893;
					localizedId2 = 1083835406;
				}
				else if (list2.Count > 0)
				{
					if (string.IsNullOrEmpty(receivedRepresentingDisplayName))
					{
						sanitizedHtmlString = SanitizedHtmlString.FromStringId(-1508975609);
					}
					else
					{
						sanitizedHtmlString = SanitizedHtmlString.FromStringId(602898401);
					}
					localizedId = 2029212075;
					localizedId2 = -1796482192;
				}
				else if (list3.Count > 0)
				{
					if (string.IsNullOrEmpty(receivedRepresentingDisplayName))
					{
						sanitizedHtmlString = SanitizedHtmlString.FromStringId(-1710537313);
					}
					else
					{
						sanitizedHtmlString = SanitizedHtmlString.FromStringId(-996033031);
					}
					localizedId = -608468101;
					localizedId2 = -1733349590;
				}
				sanitizingStringBuilder.Append("<span id=spnS class=IbL>");
				sanitizingStringBuilder.Append(LocalizedStrings.GetNonEncoded(localizedId));
				sanitizingStringBuilder.Append("</span>");
				sanitizingStringBuilder.Append("<span id=spnH class=IbL style=\"display:none\">");
				sanitizingStringBuilder.Append(LocalizedStrings.GetNonEncoded(localizedId2));
				sanitizingStringBuilder.Append("</span>");
				SanitizingStringBuilder<OwaHtml> sanitizingStringBuilder2 = new SanitizingStringBuilder<OwaHtml>();
				sanitizingStringBuilder2.Append("<div id=\"divCnf\">");
				if (list.Count > 0)
				{
					sanitizingStringBuilder2.Append<SanitizedHtmlString>(CalendarUtilities.BuildAdjacencyOrConflictSection(list, LocalizedStrings.GetNonEncoded(-1874853770), userContext, folderId));
				}
				if (list2.Count > 0)
				{
					sanitizingStringBuilder2.Append<SanitizedHtmlString>(CalendarUtilities.BuildAdjacencyOrConflictSection(list2, LocalizedStrings.GetNonEncoded(2095567903), userContext, folderId));
				}
				if (list3.Count > 0)
				{
					sanitizingStringBuilder2.Append<SanitizedHtmlString>(CalendarUtilities.BuildAdjacencyOrConflictSection(list3, LocalizedStrings.GetNonEncoded(-51439729), userContext, folderId));
				}
				sanitizingStringBuilder2.Append("</div>");
				if (!string.IsNullOrEmpty(receivedRepresentingDisplayName))
				{
					sanitizedHtmlString = SanitizedHtmlString.Format(sanitizedHtmlString.ToString(), new object[]
					{
						receivedRepresentingDisplayName
					});
				}
				infobar.AddMessage(sanitizedHtmlString, InfobarMessageType.Expanding, "divIbL", sanitizingStringBuilder.ToSanitizedString<SanitizedHtmlString>(), sanitizingStringBuilder2.ToSanitizedString<SanitizedHtmlString>());
				return;
			}
			if (list.Count > 0)
			{
				sanitizedHtmlString = SanitizedHtmlString.FromStringId(890561325);
			}
			else if (list2.Count > 0 || list3.Count > 0)
			{
				sanitizedHtmlString = SanitizedHtmlString.FromStringId(1923039961);
			}
			infobar.AddMessage(sanitizedHtmlString, InfobarMessageType.Informational);
		}

		// Token: 0x06002FD5 RID: 12245 RVA: 0x00116CC8 File Offset: 0x00114EC8
		private static SanitizedHtmlString BuildAdjacencyOrConflictSection(List<AdjacencyOrConflictInfo> appointments, string sectionTitle, UserContext userContext, OwaStoreObjectId folderId)
		{
			SanitizingStringBuilder<OwaHtml> sanitizingStringBuilder = new SanitizingStringBuilder<OwaHtml>(128);
			sanitizingStringBuilder.Append("<div id=\"divCnfHdr\">");
			sanitizingStringBuilder.Append(sectionTitle);
			sanitizingStringBuilder.Append("</div>");
			foreach (AdjacencyOrConflictInfo adjacencyOrConflictInfo in appointments)
			{
				BusyType freeBusyStatus = adjacencyOrConflictInfo.FreeBusyStatus;
				bool flag = adjacencyOrConflictInfo.Sensitivity == Sensitivity.Private && folderId != null && folderId.IsOtherMailbox;
				sanitizingStringBuilder.Append("<div class=\"cnf\" id=\"divCnfItem\">");
				sanitizingStringBuilder.Append("<div id=\"divCnfIcnSbj\">");
				switch (freeBusyStatus)
				{
				case BusyType.Tentative:
					userContext.RenderThemeImage(sanitizingStringBuilder.UnsafeInnerStringBuilder, ThemeFileId.Tentative, "tntv", new object[0]);
					break;
				case BusyType.Busy:
					userContext.RenderThemeImage(sanitizingStringBuilder.UnsafeInnerStringBuilder, ThemeFileId.Busy, "busy", new object[0]);
					break;
				case BusyType.OOF:
					userContext.RenderThemeImage(sanitizingStringBuilder.UnsafeInnerStringBuilder, ThemeFileId.OutOfOffice, "oof", new object[0]);
					break;
				}
				sanitizingStringBuilder.Append("<span id=\"");
				sanitizingStringBuilder.Append(adjacencyOrConflictInfo.OccurrenceInfo.VersionedId.ObjectId.ToBase64String());
				if (adjacencyOrConflictInfo.GlobalObjectId != null)
				{
					sanitizingStringBuilder.Append("\" gid=\"");
					sanitizingStringBuilder.Append(Convert.ToBase64String(adjacencyOrConflictInfo.GlobalObjectId));
				}
				if (folderId != null)
				{
					sanitizingStringBuilder.Append("\" fid=\"");
					sanitizingStringBuilder.Append(folderId.ToBase64String());
				}
				sanitizingStringBuilder.Append("\"");
				if (!flag)
				{
					sanitizingStringBuilder.Append(" class=\"IbL\" ");
					sanitizingStringBuilder.Append<SanitizedEventHandlerString>(Utilities.GetScriptHandler("onclick", "Owa.Components.MeetingHelpers.onClickAppointment(_this);"));
				}
				sanitizingStringBuilder.Append(">");
				if (flag)
				{
					sanitizingStringBuilder.Append(LocalizedStrings.GetNonEncoded(840767634));
				}
				else
				{
					string text = string.Empty;
					if (!string.IsNullOrEmpty(adjacencyOrConflictInfo.Subject))
					{
						text = adjacencyOrConflictInfo.Subject.Trim();
					}
					if (text.Length == 0)
					{
						sanitizingStringBuilder.Append(userContext.DirectionMark);
						sanitizingStringBuilder.Append(LocalizedStrings.GetNonEncoded(6409762));
						sanitizingStringBuilder.Append(LocalizedStrings.GetNonEncoded(-776227687));
						sanitizingStringBuilder.Append(LocalizedStrings.GetNonEncoded(-1023695022));
						sanitizingStringBuilder.Append(userContext.DirectionMark);
					}
					else
					{
						sanitizingStringBuilder.Append(text);
					}
				}
				sanitizingStringBuilder.Append("</span></div><div class=\"cnf\" id=\"divCnfLctTime\">");
				string empty = string.Empty;
				ExDateTime startTime = adjacencyOrConflictInfo.OccurrenceInfo.StartTime;
				ExDateTime endTime = adjacencyOrConflictInfo.OccurrenceInfo.EndTime;
				TimeSpan timeSpan = endTime - startTime;
				if (startTime.Day != endTime.Day || timeSpan.TotalDays >= 1.0)
				{
					sanitizingStringBuilder.Append<SanitizedHtmlString>(SanitizedHtmlString.Format(LocalizedStrings.GetNonEncoded(492249539), new object[]
					{
						startTime.ToString(userContext.UserOptions.DateFormat),
						startTime.ToString(userContext.UserOptions.TimeFormat),
						endTime.ToString(userContext.UserOptions.DateFormat),
						endTime.ToString(userContext.UserOptions.TimeFormat)
					}));
				}
				else
				{
					sanitizingStringBuilder.Append<SanitizedHtmlString>(SanitizedHtmlString.Format(LocalizedStrings.GetNonEncoded(-792821726), new object[]
					{
						startTime.ToString(userContext.UserOptions.TimeFormat),
						endTime.ToString(userContext.UserOptions.TimeFormat)
					}));
				}
				sanitizingStringBuilder.Append("&nbsp;&nbsp;&nbsp;");
				if (!flag && adjacencyOrConflictInfo.Location != null && adjacencyOrConflictInfo.Location.Trim().Length > 0)
				{
					sanitizingStringBuilder.Append(userContext.DirectionMark);
					sanitizingStringBuilder.Append(LocalizedStrings.GetNonEncoded(6409762));
					sanitizingStringBuilder.Append(adjacencyOrConflictInfo.Location);
					sanitizingStringBuilder.Append(LocalizedStrings.GetNonEncoded(-1023695022));
					sanitizingStringBuilder.Append(userContext.DirectionMark);
				}
				sanitizingStringBuilder.Append("</div></div>");
			}
			return sanitizingStringBuilder.ToSanitizedString<SanitizedHtmlString>();
		}

		// Token: 0x06002FD6 RID: 12246 RVA: 0x001170B8 File Offset: 0x001152B8
		public static void RenderCancelRecurrenceMeetingDialog(TextWriter output, bool showCancelOccurrence)
		{
			CalendarUtilities.RenderCancelRecurrenceCalendarItemDialog(output, showCancelOccurrence, true, false, false);
		}

		// Token: 0x06002FD7 RID: 12247 RVA: 0x001170C4 File Offset: 0x001152C4
		public static void RenderCancelRecurrenceCalendarItemDialog(TextWriter output, bool showCancelOccurrence, bool isMeeting, bool isPermanentDelete, bool showWarningAttendeesWillNotBeNotified)
		{
			output.Write("<div id=\"divCRMsg\" style=\"display:none\" sTtl=\"");
			if (isMeeting)
			{
				output.Write(SanitizedHtmlString.FromStringId(-2063563644));
			}
			else
			{
				output.Write(SanitizedHtmlString.FromStringId(78467316));
			}
			output.Write("\" sOk=\"");
			output.Write(SanitizedHtmlString.FromStringId(2041362128));
			output.Write("\" sCncl=\"");
			output.Write(SanitizedHtmlString.FromStringId(-1936577052));
			output.Write("\">");
			if (showWarningAttendeesWillNotBeNotified)
			{
				output.Write("<div class=\"w\">");
				output.Write(SanitizedHtmlString.FromStringId(-1626455311));
				output.Write("</div>");
			}
			if (showCancelOccurrence)
			{
				output.Write("<div class=\"cancelRcrRow\">");
				output.Write("<div class=\"fltBefore cancelRcrInput\"><input type=\"radio\" name=\"rdoCncl\" id=\"rdoCnclO\"></div>");
				output.Write("<div class=\"fltBefore\"><label for=\"rdoCnclO\">");
				if (isPermanentDelete)
				{
					output.Write(SanitizedHtmlString.FromStringId(-897929905));
				}
				else
				{
					output.Write(SanitizedHtmlString.FromStringId(-673339501));
				}
				output.Write("</label></div><div class=\"clear\"></div></div>");
			}
			output.Write("<div class=\"cancelRcrRow\">");
			output.Write("<div class=\"fltBefore cancelRcrInput\"><input type=\"radio\" name=\"rdoCncl\" id=\"rdoCnclD\"></div>");
			output.Write("<div class=\"fltBefore\"><label for=\"rdoCnclD\">");
			StringBuilder stringBuilder = new StringBuilder();
			using (SanitizingStringWriter<OwaHtml> sanitizingStringWriter = new SanitizingStringWriter<OwaHtml>(stringBuilder))
			{
				sanitizingStringWriter.Write("&nbsp;</label></div><div class=\"fltBefore dtPkerAd\">");
				DatePickerDropDownCombo.RenderDatePicker(sanitizingStringWriter, "divRecurrenceDate", DateTimeUtilities.GetLocalTime());
				sanitizingStringWriter.Write("</div><div class=\"fltBefore\"><label for=\"rdoCnclD\">");
				sanitizingStringWriter.Close();
			}
			if (isPermanentDelete)
			{
				output.Write(SanitizedHtmlString.Format(LocalizedStrings.GetHtmlEncoded(-1487900473), new object[]
				{
					SanitizedHtmlString.GetSanitizedStringWithoutEncoding(stringBuilder.ToString())
				}));
			}
			else
			{
				output.Write(SanitizedHtmlString.Format(LocalizedStrings.GetHtmlEncoded(-1062918861), new object[]
				{
					SanitizedHtmlString.GetSanitizedStringWithoutEncoding(stringBuilder.ToString())
				}));
			}
			output.Write("</label></div><div class=\"clear\"></div></div>");
			output.Write("<div class=\"cancelRcrRow\">");
			output.Write("<div class=\"fltBefore cancelRcrInput\"><input type=\"radio\" name=\"rdoCncl\" id=\"rdoCnclA\"></div>");
			output.Write("<div class=\"fltBefore\"><label for=\"rdoCnclA\">");
			if (isPermanentDelete)
			{
				output.Write(SanitizedHtmlString.FromStringId(817420711));
			}
			else
			{
				output.Write(SanitizedHtmlString.FromStringId(1631668395));
			}
			output.Write("</label></div>");
			output.Write("</div>");
		}

		// Token: 0x06002FD8 RID: 12248 RVA: 0x001172F8 File Offset: 0x001154F8
		public static string GenerateWhen(UserContext userContext, ExDateTime startTime, ExDateTime endTime, Recurrence recurrence)
		{
			string result;
			using (CalendarItem calendarItem = CalendarItem.Create(userContext.MailboxSession, userContext.CalendarFolderId))
			{
				calendarItem.StartTime = startTime;
				calendarItem.EndTime = endTime;
				calendarItem.Recurrence = recurrence;
				result = calendarItem.GenerateWhen();
			}
			return result;
		}

		// Token: 0x06002FD9 RID: 12249 RVA: 0x00117350 File Offset: 0x00115550
		internal static bool UserCanDeleteCalendarItem(CalendarItemBase calendarItemBase)
		{
			if (calendarItemBase == null)
			{
				throw new ArgumentNullException("calendarItemBase");
			}
			switch (calendarItemBase.CalendarItemType)
			{
			case CalendarItemType.Occurrence:
			case CalendarItemType.Exception:
				return ItemUtility.UserCanEditItem(calendarItemBase);
			}
			return ItemUtility.UserCanDeleteItem(calendarItemBase);
		}

		// Token: 0x06002FDA RID: 12250 RVA: 0x0011739C File Offset: 0x0011559C
		public static ExDateTime[] GetViewDaysForPublishedView(ISessionContext sessionContext, ExDateTime[] days, CalendarViewType viewType)
		{
			ExDateTime exDateTime = (days == null || days.Length == 0) ? DateTimeUtilities.GetLocalTime().Date : days[0];
			switch (viewType)
			{
			case CalendarViewType.Min:
				if (days == null || days.Length == 0)
				{
					days = new ExDateTime[]
					{
						exDateTime
					};
				}
				break;
			case CalendarViewType.Weekly:
			case CalendarViewType.WorkWeek:
				days = DateTimeUtilities.GetWeekFromDay(exDateTime, sessionContext.WeekStartDay, 62, viewType == CalendarViewType.WorkWeek);
				break;
			case CalendarViewType.Monthly:
				days = CalendarUtilities.GetViewDaysForMonthlyView(sessionContext, exDateTime);
				break;
			case CalendarViewType.WeeklyAgenda:
			case CalendarViewType.WorkWeeklyAgenda:
			{
				bool flag = viewType == CalendarViewType.WorkWeeklyAgenda;
				ExDateTime firstDay = DateTimeUtilities.GetWeekFromDay(exDateTime, sessionContext.WeekStartDay, 62, flag)[0];
				days = CalendarUtilities.GetViewDaysForWeeklyAgenda(62, firstDay, flag);
				break;
			}
			}
			return days;
		}

		// Token: 0x06002FDB RID: 12251 RVA: 0x00117464 File Offset: 0x00115664
		public static ExDateTime[] GetViewDays(UserContext userContext, ExDateTime[] days, CalendarViewType viewType, OwaStoreObjectId folderId, FolderViewStates viewStates)
		{
			ExDateTime exDateTime = (days == null || days.Length == 0) ? DateTimeUtilities.GetLocalTime().Date : days[0];
			switch (viewType)
			{
			case CalendarViewType.Min:
				if (days == null || days.Length == 0)
				{
					int dailyViewDays = viewStates.DailyViewDays;
					days = new ExDateTime[dailyViewDays];
					for (int i = 0; i < dailyViewDays; i++)
					{
						days[i] = exDateTime;
						exDateTime = exDateTime.IncrementDays(1);
					}
				}
				break;
			case CalendarViewType.Weekly:
				days = DateTimeUtilities.GetWeekFromDay(exDateTime, userContext.UserOptions.WeekStartDay, 0, false);
				break;
			case CalendarViewType.WorkWeek:
				days = DateTimeUtilities.GetWeekFromDay(exDateTime, userContext.UserOptions.WeekStartDay, CalendarUtilities.GetWorkDays(userContext, folderId), true);
				break;
			case CalendarViewType.Monthly:
				days = CalendarUtilities.GetViewDaysForMonthlyView(userContext, exDateTime);
				break;
			case CalendarViewType.WeeklyAgenda:
			case CalendarViewType.WorkWeeklyAgenda:
			{
				ExDateTime firstDay = CalendarUtilities.GetWeekDays(folderId, userContext, exDateTime, false)[0];
				int workDays = CalendarUtilities.GetWorkDays(userContext, folderId);
				days = CalendarUtilities.GetViewDaysForWeeklyAgenda(workDays, firstDay, viewType == CalendarViewType.WorkWeeklyAgenda);
				break;
			}
			}
			return days;
		}

		// Token: 0x06002FDC RID: 12252 RVA: 0x0011756C File Offset: 0x0011576C
		private static ExDateTime[] GetViewDaysForMonthlyView(ISessionContext sessionContext, ExDateTime start)
		{
			ExDateTime exDateTime;
			ExDateTime exDateTime2;
			DatePickerBase.GetVisibleDateRange(start, out exDateTime, out exDateTime2, sessionContext.TimeZone);
			ExDateTime[] array = new ExDateTime[42 - exDateTime2.Day / 7 * 7];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = exDateTime.IncrementDays(i);
			}
			return array;
		}

		// Token: 0x06002FDD RID: 12253 RVA: 0x001175C0 File Offset: 0x001157C0
		private static ExDateTime[] GetViewDaysForWeeklyAgenda(int workDay, ExDateTime firstDay, bool isWorkWeek)
		{
			List<ExDateTime> list = new List<ExDateTime>();
			ExDateTime exDateTime = firstDay;
			while (!DateTimeUtilities.IsWorkingDay(exDateTime, workDay))
			{
				exDateTime = exDateTime.IncrementDays(1);
			}
			for (int i = 0; i < 7; i++)
			{
				if (DateTimeUtilities.IsWorkingDay(exDateTime, workDay) || !isWorkWeek)
				{
					list.Add(exDateTime);
				}
				exDateTime = exDateTime.IncrementDays(1);
			}
			return list.ToArray();
		}

		// Token: 0x06002FDE RID: 12254 RVA: 0x00117618 File Offset: 0x00115818
		public static void RenderPreviousNextButtons(ISessionContext sessionContext, TextWriter output)
		{
			output.Write("<div id=\"divNPP\">");
			sessionContext.RenderThemeImageWithToolTip(output, sessionContext.IsRtl ? ThemeFileId.NextArrow : ThemeFileId.PreviousArrow, null, 1104568029, new string[0]);
			output.Write("</div>");
			output.Write("<div id=\"divNPN\">");
			sessionContext.RenderThemeImageWithToolTip(output, sessionContext.IsRtl ? ThemeFileId.PreviousArrow : ThemeFileId.NextArrow, null, -871205065, new string[0]);
			output.Write("</div>");
		}

		// Token: 0x06002FDF RID: 12255 RVA: 0x001176A0 File Offset: 0x001158A0
		public static bool UserHasRightToLoad(Folder folder)
		{
			object obj = folder.TryGetProperty(StoreObjectSchema.EffectiveRights);
			EffectiveRights valueToTest = (EffectiveRights)obj;
			return Utilities.IsFlagSet((int)valueToTest, 2);
		}

		// Token: 0x06002FE0 RID: 12256 RVA: 0x001176C7 File Offset: 0x001158C7
		public static void GetCalendarViewParamsFromViewStates(FolderViewStates states, out int viewWidth, ref CalendarViewType viewType, out ReadingPanePosition readingPanePosition)
		{
			if (viewType == CalendarViewType.None)
			{
				viewType = states.CalendarViewType;
			}
			viewWidth = states.GetViewWidth(450);
			if (viewType == CalendarViewType.Monthly)
			{
				readingPanePosition = ReadingPanePosition.Off;
				return;
			}
			if (viewType == CalendarViewType.Min)
			{
				readingPanePosition = states.ReadingPanePosition;
				return;
			}
			readingPanePosition = states.ReadingPanePositionMultiDay;
		}

		// Token: 0x06002FE1 RID: 12257 RVA: 0x00117704 File Offset: 0x00115904
		public static void BuildCalendarInfobar(Infobar infobar, UserContext userContext, OwaStoreObjectId folderId, int colorIndex, bool renderNotifyForOtherUser)
		{
			if (infobar == null)
			{
				throw new ArgumentNullException("infobar");
			}
			if (userContext.CalendarFolderOwaId.Equals(folderId))
			{
				return;
			}
			PropertyDefinition[] prefetchProperties = new PropertyDefinition[]
			{
				StoreObjectSchema.DisplayName,
				FolderSchema.ExtendedFolderFlags,
				StoreObjectSchema.ContainerClass
			};
			try
			{
				using (CalendarFolder folder = Utilities.GetFolder<CalendarFolder>(userContext, folderId, prefetchProperties))
				{
					SharedCalendarItemInfobar sharedCalendarItemInfobar = new SharedCalendarItemInfobar(userContext, folder, colorIndex, renderNotifyForOtherUser);
					sharedCalendarItemInfobar.Build(infobar);
				}
			}
			catch (WrongObjectTypeException)
			{
			}
		}

		// Token: 0x06002FE2 RID: 12258 RVA: 0x00117798 File Offset: 0x00115998
		public static string GetDisplayAttendees(MeetingRequest meetingRequest, RecipientWellType type)
		{
			if (meetingRequest == null)
			{
				throw new ArgumentNullException("meetingRequest");
			}
			object obj = null;
			string result = string.Empty;
			if (type == RecipientWellType.To)
			{
				obj = meetingRequest.TryGetProperty(CalendarItemBaseSchema.DisplayAttendeesTo);
			}
			else if (type == RecipientWellType.Cc)
			{
				obj = meetingRequest.TryGetProperty(CalendarItemBaseSchema.DisplayAttendeesCc);
			}
			string text = obj as string;
			if (text != null)
			{
				result = text;
			}
			return result;
		}

		// Token: 0x06002FE3 RID: 12259 RVA: 0x001177EC File Offset: 0x001159EC
		public static ExDateTime[] GetWeekDays(OwaStoreObjectId folderId, UserContext userContext, ExDateTime dayInWeek, bool isWorkWeek)
		{
			ExDateTime[] weekFromDay;
			if (isWorkWeek)
			{
				weekFromDay = DateTimeUtilities.GetWeekFromDay(dayInWeek, userContext.UserOptions.WeekStartDay, CalendarUtilities.GetWorkDays(userContext, folderId), true);
			}
			else
			{
				weekFromDay = DateTimeUtilities.GetWeekFromDay(dayInWeek, userContext.UserOptions.WeekStartDay, 0, false);
			}
			return weekFromDay;
		}

		// Token: 0x06002FE4 RID: 12260 RVA: 0x00117830 File Offset: 0x00115A30
		public static int GetWorkDays(UserContext userContext, OwaStoreObjectId folderId)
		{
			if (userContext == null)
			{
				throw new ArgumentNullException("userContext");
			}
			if (folderId == null)
			{
				throw new ArgumentNullException("folderId");
			}
			WorkingHours workingHours;
			if (!folderId.IsOtherMailbox)
			{
				workingHours = userContext.WorkingHours;
			}
			else
			{
				workingHours = userContext.GetOthersWorkingHours(folderId);
			}
			int num = workingHours.WorkDays;
			if (num == 0)
			{
				num = 31;
			}
			return num;
		}

		// Token: 0x06002FE5 RID: 12261 RVA: 0x00117880 File Offset: 0x00115A80
		public static void AdjustTimesWithTimeZone(ExDateTime[] times, ExTimeZone timeZone)
		{
			if (times == null)
			{
				throw new ArgumentNullException("times");
			}
			if (timeZone == null)
			{
				throw new ArgumentNullException("timeZone");
			}
			for (int i = 0; i < times.Length; i++)
			{
				times[i] = new ExDateTime(timeZone, (DateTime)times[i]);
			}
		}

		// Token: 0x06002FE6 RID: 12262 RVA: 0x001178DC File Offset: 0x00115ADC
		public static void GetReceiverGSCalendarIdStringAndDisplayName(UserContext userContext, MeetingMessage item, out string id, out string displayName)
		{
			id = string.Empty;
			displayName = null;
			ExchangePrincipal exchangePrincipal;
			if (userContext.DelegateSessionManager.TryGetExchangePrincipal(item.ReceivedRepresenting.EmailAddress, out exchangePrincipal))
			{
				OwaStoreObjectId owaStoreObjectId = OwaStoreObjectId.CreateFromGSCalendarLegacyDN(exchangePrincipal.LegacyDn);
				id = owaStoreObjectId.ToBase64String();
				displayName = exchangePrincipal.MailboxInfo.DisplayName;
			}
		}

		// Token: 0x06002FE7 RID: 12263 RVA: 0x0011792E File Offset: 0x00115B2E
		public static bool FullMonthNameRequired(CultureInfo culture)
		{
			return culture.LCID == 1041 || culture.LCID == 1042;
		}

		// Token: 0x06002FE8 RID: 12264 RVA: 0x00117950 File Offset: 0x00115B50
		public static bool CanSubscribeInternetCalendar()
		{
			UserContext userContext = UserContextManager.GetUserContext();
			MailboxSession mailboxSession = (userContext == null) ? null : userContext.MailboxSession;
			return mailboxSession != null && mailboxSession.MailboxOwner.MailboxInfo.Location.ServerVersion >= Server.E14SP1MinVersion;
		}

		// Token: 0x06002FE9 RID: 12265 RVA: 0x00117994 File Offset: 0x00115B94
		public static string GetWebCalendarUrl(MailboxSession session, StoreObjectId folderId)
		{
			if (session == null)
			{
				throw new ArgumentNullException("session");
			}
			if (folderId == null)
			{
				throw new ArgumentNullException("folderId");
			}
			using (PublishingSubscriptionManager publishingSubscriptionManager = new PublishingSubscriptionManager(session))
			{
				PublishingSubscriptionData byLocalFolderId = publishingSubscriptionManager.GetByLocalFolderId(folderId);
				if (byLocalFolderId != null)
				{
					return byLocalFolderId.PublishingUrl.ToString();
				}
			}
			return null;
		}

		// Token: 0x0400218C RID: 8588
		public const int MaximumCalendarCount = 5;

		// Token: 0x0400218D RID: 8589
		public static readonly PropertyDefinition[] QueryProperties = new PropertyDefinition[]
		{
			ItemSchema.Id,
			CalendarItemInstanceSchema.StartTime,
			CalendarItemInstanceSchema.EndTime,
			ItemSchema.Subject,
			CalendarItemBaseSchema.Location,
			CalendarItemBaseSchema.OrganizerDisplayName,
			CalendarItemBaseSchema.CalendarItemType,
			ItemSchema.HasAttachment,
			CalendarItemBaseSchema.FreeBusyStatus,
			ItemSchema.Sensitivity,
			CalendarItemBaseSchema.AppointmentState,
			CalendarItemBaseSchema.IsException,
			CalendarItemBaseSchema.IsOrganizer,
			ItemSchema.Categories,
			CalendarItemBaseSchema.AppointmentColor
		};

		// Token: 0x0400218E RID: 8590
		private static readonly PropertyDefinition[] AdditionalQueryPropertiesForPrint = new PropertyDefinition[]
		{
			CalendarItemBaseSchema.DisplayAttendeesTo,
			CalendarItemBaseSchema.DisplayAttendeesCc
		};

		// Token: 0x0400218F RID: 8591
		public static readonly PropertyDefinition[] PrintQueryProperties;

		// Token: 0x04002190 RID: 8592
		public static readonly PropertyDefinition[] FolderViewProperties = new PropertyDefinition[]
		{
			StoreObjectSchema.DisplayName,
			ViewStateProperties.CalendarViewType,
			ViewStateProperties.ReadingPanePosition,
			ViewStateProperties.ReadingPanePositionMultiDay,
			ViewStateProperties.ViewWidth,
			ViewStateProperties.DailyViewDays,
			StoreObjectSchema.EffectiveRights,
			FolderSchema.ExtendedFolderFlags
		};

		// Token: 0x04002191 RID: 8593
		public static readonly PropertyDefinition[] RenderPayloadFolderProperties = new PropertyDefinition[]
		{
			ViewStateProperties.DailyViewDays,
			ViewStateProperties.CalendarViewType,
			ViewStateProperties.ReadingPanePosition,
			ViewStateProperties.ReadingPanePositionMultiDay,
			StoreObjectSchema.EffectiveRights
		};
	}
}
