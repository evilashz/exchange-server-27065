using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Microsoft.Exchange.Clients.Common;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Clients.Owa.Core.Controls;
using Microsoft.Exchange.Clients.Owa.Premium.Controls;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Clients;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.InfoWorker.Common.Availability;
using Microsoft.Exchange.InfoWorker.Common.MeetingSuggestions;

namespace Microsoft.Exchange.Clients.Owa.Premium
{
	// Token: 0x0200049E RID: 1182
	[OwaEventNamespace("EditCalendarItem")]
	[OwaEventSegmentation(Feature.Calendar)]
	internal sealed class EditCalendarItemEventHandler : RecurringItemEventHandler
	{
		// Token: 0x06002D8B RID: 11659 RVA: 0x001002A8 File Offset: 0x000FE4A8
		public static void Register()
		{
			OwaEventRegistry.RegisterEnum(typeof(MeetingAttendeeType));
			OwaEventRegistry.RegisterEnum(typeof(EditCalendarItemEventHandler.SchedulingUpdatesData));
			OwaEventRegistry.RegisterEnum(typeof(OwaRecurrenceType));
			OwaEventRegistry.RegisterEnum(typeof(RecurrenceRangeType));
			OwaEventRegistry.RegisterEnum(typeof(CalendarItemTrackingTab.TrackingTableColumn));
			OwaEventRegistry.RegisterStruct(typeof(SchedulingRecipientInfo));
			OwaEventRegistry.RegisterHandler(typeof(EditCalendarItemEventHandler));
		}

		// Token: 0x06002D8C RID: 11660 RVA: 0x00100320 File Offset: 0x000FE520
		[OwaEvent("GRC")]
		public void GetRoomsCache()
		{
			ExTraceGlobals.MailCallTracer.TraceDebug((long)this.GetHashCode(), "EditCalendarItemEventHandler.GetRoomsCache");
			RecipientCache recipientCache = RoomsCache.TryGetCache(base.OwaContext.UserContext);
			if (recipientCache != null)
			{
				this.Writer.WriteLine("new Array(");
				recipientCache.Sort();
				for (int i = 0; i < recipientCache.CacheLength; i++)
				{
					if (i > 0)
					{
						this.Writer.WriteLine(",");
					}
					AutoCompleteCacheEntry.RenderEntryJavascript(this.Writer, recipientCache.CacheEntries[i]);
				}
				this.Writer.WriteLine(");");
			}
		}

		// Token: 0x06002D8D RID: 11661 RVA: 0x001003B9 File Offset: 0x000FE5B9
		[OwaEventVerb(OwaEventVerb.Get)]
		[OwaEvent("LRD")]
		public void LoadRecurrenceDialog()
		{
			this.HttpContext.Server.Execute("forms/premium/calendaritemrecurrence.aspx", this.Writer);
		}

		// Token: 0x06002D8E RID: 11662 RVA: 0x001003D8 File Offset: 0x000FE5D8
		[OwaEventParameter("ST", typeof(ExDateTime))]
		[OwaEvent("LST")]
		[OwaEventParameter("ET", typeof(ExDateTime))]
		[OwaEventParameter("fId", typeof(OwaStoreObjectId), false, true)]
		[OwaEventVerb(OwaEventVerb.Get)]
		public void LoadSchedulingTab()
		{
			ExDateTime exDateTime = (ExDateTime)base.GetParameter("ST");
			ExDateTime date = (ExDateTime)base.GetParameter("ET");
			OwaStoreObjectId owaStoreObjectId = (OwaStoreObjectId)base.GetParameter("fId");
			StringBuilder stringBuilder = new StringBuilder("forms/premium/calendaritemschedulingtab.aspx?sd=", 196);
			stringBuilder.Append(Utilities.UrlEncode(DateTimeUtilities.GetIsoDateFormat(date)));
			stringBuilder.Append("&ed=");
			stringBuilder.Append(Utilities.UrlEncode(DateTimeUtilities.GetIsoDateFormat(date)));
			if (owaStoreObjectId != null)
			{
				stringBuilder.Append("&fid=");
				stringBuilder.Append(Utilities.UrlEncode(owaStoreObjectId.ToString()));
			}
			this.HttpContext.Server.Execute(stringBuilder.ToString(), this.Writer);
		}

		// Token: 0x06002D8F RID: 11663 RVA: 0x00100498 File Offset: 0x000FE698
		[OwaEventVerb(OwaEventVerb.Get)]
		[OwaEventParameter("Id", typeof(OwaStoreObjectId))]
		[OwaEventParameter("sc", typeof(CalendarItemTrackingTab.TrackingTableColumn), false, true)]
		[OwaEvent("LTT")]
		public void LoadTrackingTab()
		{
			OwaStoreObjectId owaStoreObjectId = (OwaStoreObjectId)base.GetParameter("Id");
			string text = "forms/premium/calendaritemtrackingtab.aspx?id=" + Utilities.UrlEncode(owaStoreObjectId.ToBase64String());
			if (base.IsParameterSet("sc"))
			{
				CalendarItemTrackingTab.TrackingTableColumn trackingTableColumn = (CalendarItemTrackingTab.TrackingTableColumn)base.GetParameter("sc");
				text += "&sc=";
				string str = text;
				int num = (int)trackingTableColumn;
				text = str + num.ToString();
			}
			this.HttpContext.Server.Execute(text, this.Writer);
		}

		// Token: 0x06002D90 RID: 11664 RVA: 0x00100520 File Offset: 0x000FE720
		[OwaEventParameter("Recips", typeof(RecipientInfoAC), true, true)]
		[OwaEventParameter("RcrRngO", typeof(int))]
		[OwaEventParameter("FrcSv", typeof(bool), false, true)]
		[OwaEventParameter("NtfyPrncpl", typeof(bool), false, true)]
		[OwaEventParameter("HideMailTipsByDefault", typeof(bool), false, true)]
		[OwaEvent("Save")]
		[OwaEventParameter("RcrRngE", typeof(ExDateTime))]
		[OwaEventParameter("CK", typeof(string), false, true)]
		[OwaEventParameter("fId", typeof(OwaStoreObjectId), false, true)]
		[OwaEventParameter("To", typeof(RecipientInfo), true, true)]
		[OwaEventParameter("Cc", typeof(RecipientInfo), true, true)]
		[OwaEventParameter("Bcc", typeof(RecipientInfo), true, true)]
		[OwaEventParameter("Id", typeof(OwaStoreObjectId), false, true)]
		[OwaEventParameter("Subj", typeof(string))]
		[OwaEventParameter("Loc", typeof(string))]
		[OwaEventParameter("Imp", typeof(Importance))]
		[OwaEventParameter("Prvt", typeof(bool))]
		[OwaEventParameter("ST", typeof(ExDateTime))]
		[OwaEventParameter("ET", typeof(ExDateTime))]
		[OwaEventParameter("AllDay", typeof(bool))]
		[OwaEventParameter("RR", typeof(bool))]
		[OwaEventParameter("RemS", typeof(bool))]
		[OwaEventParameter("RemT", typeof(int))]
		[OwaEventParameter("Fbs", typeof(Microsoft.Exchange.Data.Storage.BusyType))]
		[OwaEventParameter("Mtng", typeof(bool))]
		[OwaEventParameter("Body", typeof(string))]
		[OwaEventParameter("Text", typeof(bool), false, true)]
		[OwaEventParameter("RcrT", typeof(int))]
		[OwaEventParameter("RcrI", typeof(int))]
		[OwaEventParameter("RcrDys", typeof(int))]
		[OwaEventParameter("RcrDy", typeof(int))]
		[OwaEventParameter("RcrM", typeof(int))]
		[OwaEventParameter("RcrO", typeof(int))]
		[OwaEventParameter("RcrRngT", typeof(RecurrenceRangeType))]
		[OwaEventParameter("RcrRngS", typeof(ExDateTime))]
		public void Save()
		{
			ExTraceGlobals.CalendarCallTracer.TraceDebug((long)this.GetHashCode(), "EditCalendarItemEventHandler.Save");
			bool flag = base.IsParameterSet("Id");
			bool flag2 = false;
			object parameter = base.GetParameter("FrcSv");
			if (parameter != null)
			{
				flag2 = (bool)parameter;
			}
			this.UpdateAutoCompleteCache();
			base.SaveHideMailTipsByDefault();
			CalendarItemBase calendarItem;
			CalendarItemBase calendarItemBase = calendarItem = this.GetCalendarItem(new PropertyDefinition[]
			{
				CalendarItemBaseSchema.Location,
				CalendarItemBaseSchema.IsMeeting,
				CalendarItemBaseSchema.MeetingRequestWasSent
			});
			try
			{
				bool flag3;
				EditCalendarItemEventHandler.CriticalUpdateProperties criticalUpdateProperties = this.UpdateCalendarItemProperties(calendarItemBase, (bool)base.GetParameter("Mtng"), out flag3);
				if (!flag2 && flag && calendarItemBase.IsMeeting && calendarItemBase.MeetingRequestWasSent && criticalUpdateProperties != EditCalendarItemEventHandler.CriticalUpdateProperties.None)
				{
					Strings.IDs? ds = null;
					switch (criticalUpdateProperties)
					{
					case EditCalendarItemEventHandler.CriticalUpdateProperties.Location:
						ds = new Strings.IDs?(-552157857);
						break;
					case EditCalendarItemEventHandler.CriticalUpdateProperties.Time:
						ds = new Strings.IDs?(1448196555);
						break;
					case EditCalendarItemEventHandler.CriticalUpdateProperties.Location | EditCalendarItemEventHandler.CriticalUpdateProperties.Time:
						ds = new Strings.IDs?(910667932);
						break;
					case EditCalendarItemEventHandler.CriticalUpdateProperties.Attendees:
						ds = new Strings.IDs?(-1241378469);
						break;
					case EditCalendarItemEventHandler.CriticalUpdateProperties.Location | EditCalendarItemEventHandler.CriticalUpdateProperties.Attendees:
						ds = new Strings.IDs?(-1559164140);
						break;
					case EditCalendarItemEventHandler.CriticalUpdateProperties.Time | EditCalendarItemEventHandler.CriticalUpdateProperties.Attendees:
						ds = new Strings.IDs?(1936509526);
						break;
					case EditCalendarItemEventHandler.CriticalUpdateProperties.Location | EditCalendarItemEventHandler.CriticalUpdateProperties.Time | EditCalendarItemEventHandler.CriticalUpdateProperties.Attendees:
						ds = new Strings.IDs?(1311904671);
						break;
					}
					base.RenderPartialFailure(string.Format((ds != null) ? LocalizedStrings.GetHtmlEncoded(ds.Value) : string.Empty, LocalizedStrings.GetHtmlEncoded(1594681135)), null, ButtonDialogIcon.NotSet, OwaEventHandlerErrorCode.MeetingCriticalUpdateProperties);
				}
				else
				{
					Utilities.SaveItem(calendarItemBase, flag);
					base.WriteIdAndChangeKey(calendarItemBase, flag);
					if (!flag && ExTraceGlobals.CalendarDataTracer.IsTraceEnabled(TraceType.DebugTrace))
					{
						ExTraceGlobals.CalendarDataTracer.TraceDebug<string>((long)this.GetHashCode(), "New calendar item ID is '{0}'", Utilities.GetIdAsString(calendarItemBase));
					}
					base.MoveItemToDestinationFolderIfInScratchPad(calendarItemBase);
					this.SendPrincipalNotification(Utilities.GetIdAsString(calendarItemBase), PrincipalNotificationMessage.ActionType.Save, !flag, (bool)base.GetParameter("Mtng"));
				}
			}
			finally
			{
				if (calendarItem != null)
				{
					((IDisposable)calendarItem).Dispose();
				}
			}
		}

		// Token: 0x06002D91 RID: 11665 RVA: 0x0010073C File Offset: 0x000FE93C
		[OwaEventParameter("RcrRngO", typeof(int))]
		[OwaEventParameter("RcrRngE", typeof(ExDateTime))]
		[OwaEventParameter("FrcSnd", typeof(bool), false, true)]
		[OwaEventParameter("SndAll", typeof(bool), false, true)]
		[OwaEventParameter("NtfyPrncpl", typeof(bool), false, true)]
		[OwaEventParameter("HideMailTipsByDefault", typeof(bool), false, true)]
		[OwaEvent("Send")]
		[OwaEventParameter("CK", typeof(string), false, true)]
		[OwaEventParameter("fId", typeof(OwaStoreObjectId), false, true)]
		[OwaEventParameter("To", typeof(RecipientInfo), true, true)]
		[OwaEventParameter("Cc", typeof(RecipientInfo), true, true)]
		[OwaEventParameter("Bcc", typeof(RecipientInfo), true, true)]
		[OwaEventParameter("Recips", typeof(RecipientInfoAC), true, true)]
		[OwaEventParameter("Subj", typeof(string))]
		[OwaEventParameter("Loc", typeof(string))]
		[OwaEventParameter("Imp", typeof(Importance))]
		[OwaEventParameter("Prvt", typeof(bool))]
		[OwaEventParameter("ST", typeof(ExDateTime))]
		[OwaEventParameter("ET", typeof(ExDateTime))]
		[OwaEventParameter("AllDay", typeof(bool))]
		[OwaEventParameter("RR", typeof(bool))]
		[OwaEventParameter("RemS", typeof(bool))]
		[OwaEventParameter("RemT", typeof(int))]
		[OwaEventParameter("Fbs", typeof(Microsoft.Exchange.Data.Storage.BusyType))]
		[OwaEventParameter("Mtng", typeof(bool))]
		[OwaEventParameter("Body", typeof(string))]
		[OwaEventParameter("Text", typeof(bool), false, true)]
		[OwaEventParameter("RcrT", typeof(int))]
		[OwaEventParameter("RcrI", typeof(int))]
		[OwaEventParameter("RcrDys", typeof(int))]
		[OwaEventParameter("RcrDy", typeof(int))]
		[OwaEventParameter("RcrM", typeof(int))]
		[OwaEventParameter("RcrO", typeof(int))]
		[OwaEventParameter("RcrRngT", typeof(RecurrenceRangeType))]
		[OwaEventParameter("RcrRngS", typeof(ExDateTime))]
		[OwaEventParameter("Id", typeof(OwaStoreObjectId), false, true)]
		public void Send()
		{
			ExTraceGlobals.CalendarCallTracer.TraceDebug((long)this.GetHashCode(), "EditCalendarItemEventHandler.Send");
			bool isToAllAttendees = true;
			this.UpdateAutoCompleteCache();
			base.SaveHideMailTipsByDefault();
			CalendarItemBase calendarItem;
			CalendarItemBase calendarItemBase = calendarItem = this.GetCalendarItem(new PropertyDefinition[0]);
			try
			{
				if (!calendarItemBase.IsOrganizer())
				{
					throw new OwaEventHandlerException(LocalizedStrings.GetNonEncoded(1360823576));
				}
				bool flag;
				EditCalendarItemEventHandler.CriticalUpdateProperties criticalUpdateProperties = this.UpdateCalendarItemProperties(calendarItemBase, (bool)base.GetParameter("Mtng"), out flag);
				if (flag)
				{
					base.WriteChangeKey(calendarItemBase);
				}
				bool flag2 = false;
				object parameter = base.GetParameter("FrcSnd");
				ExDateTime localTime = DateTimeUtilities.GetLocalTime();
				if (parameter != null)
				{
					flag2 = (bool)parameter;
				}
				if (calendarItemBase.IsMeeting && !calendarItemBase.MeetingRequestWasSent && calendarItemBase.AttendeeCollection.Count == 0)
				{
					base.RenderPartialFailure(-1902165978);
				}
				else if (Utilities.RecipientsOnlyHaveEmptyPDL<Attendee>(base.UserContext, calendarItemBase.AttendeeCollection))
				{
					base.RenderPartialFailure(1389137820);
				}
				else
				{
					if (!flag2)
					{
						StringBuilder stringBuilder = new StringBuilder();
						if (calendarItemBase.CalendarItemType == CalendarItemType.RecurringMaster)
						{
							CalendarItem calendarItem2 = calendarItemBase as CalendarItem;
							if (!(calendarItem2.Recurrence.Range is NoEndRecurrenceRange))
							{
								OccurrenceInfo lastOccurrence = calendarItem2.Recurrence.GetLastOccurrence();
								if (lastOccurrence != null && lastOccurrence.EndTime < localTime)
								{
									stringBuilder.Append("\n\t");
									stringBuilder.Append(LocalizedStrings.GetHtmlEncoded(2056979915));
								}
							}
						}
						else if (calendarItemBase.EndTime < localTime)
						{
							stringBuilder.Append("\n\t");
							stringBuilder.Append(LocalizedStrings.GetHtmlEncoded(839442440));
						}
						if (string.IsNullOrEmpty(calendarItemBase.Subject))
						{
							stringBuilder.Append("\n\t");
							stringBuilder.Append(LocalizedStrings.GetHtmlEncoded(-25858033));
						}
						if (string.IsNullOrEmpty(calendarItemBase.Location))
						{
							stringBuilder.Append("\n\t");
							stringBuilder.Append(LocalizedStrings.GetHtmlEncoded(-1681723506));
						}
						if (0 < stringBuilder.Length)
						{
							stringBuilder.Insert(0, "\n");
							stringBuilder.Insert(0, LocalizedStrings.GetHtmlEncoded(1040416023));
							stringBuilder.Append("\n\n");
							stringBuilder.Append(LocalizedStrings.GetHtmlEncoded(105464887));
							base.RenderPartialFailure(stringBuilder.ToString(), null, ButtonDialogIcon.NotSet, OwaEventHandlerErrorCode.MissingMeetingFields);
							return;
						}
					}
					if (calendarItemBase.IsMeeting && calendarItemBase.MeetingRequestWasSent && calendarItemBase.AttendeeCollection.Count == 0)
					{
						isToAllAttendees = false;
					}
					else if (base.IsParameterSet("SndAll"))
					{
						isToAllAttendees = (bool)base.GetParameter("SndAll");
					}
					else if (calendarItemBase.IsMeeting && calendarItemBase.MeetingRequestWasSent && criticalUpdateProperties == EditCalendarItemEventHandler.CriticalUpdateProperties.Attendees)
					{
						this.Writer.Write("<div id=divChosA></div>");
						return;
					}
					ExTraceGlobals.CalendarTracer.TraceDebug((long)this.GetHashCode(), "Sending meeting request");
					calendarItemBase[CalendarItemBaseSchema.When] = calendarItemBase.GenerateWhen();
					calendarItemBase.LocationIdentifierHelperInstance.SetLocationIdentifier(58527U, LastChangeAction.SendMeetingUpdate);
					calendarItemBase.SendMeetingMessages(isToAllAttendees, null, true, true, null, null);
					bool flag3 = false;
					if (base.IsParameterSet("NtfyPrncpl"))
					{
						flag3 = (bool)base.GetParameter("NtfyPrncpl");
					}
					if (flag3)
					{
						OwaStoreObjectId owaStoreObjectId = (OwaStoreObjectId)base.GetParameter("Id");
						PrincipalNotificationMessage.ActionType actionType = PrincipalNotificationMessage.ActionType.Send;
						calendarItemBase.Load();
						if (owaStoreObjectId != null)
						{
							this.SendPrincipalNotification(owaStoreObjectId.ToBase64String(), actionType, false, true);
						}
						else
						{
							this.SendPrincipalNotification(Utilities.GetIdAsString(calendarItemBase), actionType, true, true);
						}
					}
				}
			}
			finally
			{
				if (calendarItem != null)
				{
					((IDisposable)calendarItem).Dispose();
				}
			}
		}

		// Token: 0x06002D92 RID: 11666 RVA: 0x00100AD4 File Offset: 0x000FECD4
		[OwaEventParameter("RcrRngT", typeof(RecurrenceRangeType))]
		[OwaEventParameter("Subj", typeof(string))]
		[OwaEventParameter("Loc", typeof(string))]
		[OwaEvent("CM")]
		[OwaEventParameter("Id", typeof(OwaStoreObjectId))]
		[OwaEventParameter("CK", typeof(string))]
		[OwaEventParameter("To", typeof(RecipientInfo), true, true)]
		[OwaEventParameter("Cc", typeof(RecipientInfo), true, true)]
		[OwaEventParameter("Bcc", typeof(RecipientInfo), true, true)]
		[OwaEventParameter("Recips", typeof(RecipientInfoAC), true, true)]
		[OwaEventParameter("Imp", typeof(Importance))]
		[OwaEventParameter("Prvt", typeof(bool))]
		[OwaEventParameter("ST", typeof(ExDateTime))]
		[OwaEventParameter("ET", typeof(ExDateTime))]
		[OwaEventParameter("AllDay", typeof(bool))]
		[OwaEventParameter("RR", typeof(bool))]
		[OwaEventParameter("RemS", typeof(bool))]
		[OwaEventParameter("RemT", typeof(int))]
		[OwaEventParameter("Fbs", typeof(Microsoft.Exchange.Data.Storage.BusyType))]
		[OwaEventParameter("Mtng", typeof(bool), false, true)]
		[OwaEventParameter("Body", typeof(string))]
		[OwaEventParameter("Text", typeof(bool), false, true)]
		[OwaEventParameter("RcrT", typeof(int))]
		[OwaEventParameter("RcrI", typeof(int))]
		[OwaEventParameter("RcrDys", typeof(int))]
		[OwaEventParameter("RcrDy", typeof(int))]
		[OwaEventParameter("RcrM", typeof(int))]
		[OwaEventParameter("RcrO", typeof(int))]
		[OwaEventParameter("RcrRngS", typeof(ExDateTime))]
		[OwaEventParameter("RcrRngO", typeof(int))]
		[OwaEventParameter("RcrRngE", typeof(ExDateTime))]
		[OwaEventParameter("fId", typeof(OwaStoreObjectId), false, true)]
		[OwaEventParameter("NtfyPrncpl", typeof(bool), false, true)]
		public void CancelMeeting()
		{
			ExTraceGlobals.CalendarCallTracer.TraceDebug((long)this.GetHashCode(), "EditCalendarItemEventHandler.CancelMeeting");
			this.UpdateAutoCompleteCache();
			CalendarItemBase calendarItem;
			CalendarItemBase calendarItemBase = calendarItem = this.GetCalendarItem(new PropertyDefinition[0]);
			try
			{
				if (0 < calendarItemBase.AttendeeCollection.Count)
				{
					bool flag;
					this.UpdateCalendarItemProperties(calendarItemBase, true, out flag);
					if (flag)
					{
						base.WriteChangeKey(calendarItemBase);
					}
					if (calendarItemBase.AttendeeCollection.Count == 0)
					{
						base.RenderPartialFailure(-1902165978);
						return;
					}
					using (MeetingCancellation meetingCancellation = calendarItemBase.CancelMeeting(null, null))
					{
						meetingCancellation.Send();
					}
				}
				OwaStoreObjectId owaStoreObjectId = (OwaStoreObjectId)base.GetParameter("Id");
				this.SendPrincipalNotification(owaStoreObjectId.ToBase64String(), PrincipalNotificationMessage.ActionType.Cancel, false, true);
				calendarItemBase.DeleteMeeting(DeleteItemFlags.MoveToDeletedItems | DeleteItemFlags.CancelCalendarItem);
			}
			finally
			{
				if (calendarItem != null)
				{
					((IDisposable)calendarItem).Dispose();
				}
			}
		}

		// Token: 0x06002D93 RID: 11667 RVA: 0x00100BC4 File Offset: 0x000FEDC4
		[OwaEventParameter("CK", typeof(string))]
		[OwaEventParameter("Id", typeof(OwaStoreObjectId))]
		[OwaEventParameter("Prm", typeof(bool))]
		[OwaEventParameter("RcrRngE", typeof(ExDateTime), false, true)]
		[OwaEventParameter("Mtng", typeof(bool))]
		[OwaEventParameter("fId", typeof(OwaStoreObjectId), false, true)]
		[OwaEventParameter("NtfyPrncpl", typeof(bool), false, true)]
		[OwaEvent("Delete")]
		public override void Delete()
		{
			ExTraceGlobals.MailCallTracer.TraceDebug((long)this.GetHashCode(), "EditCalendarItemEventHandler.Delete");
			CalendarItemBase calendarItemBase = null;
			OwaStoreObjectId owaStoreObjectId = (OwaStoreObjectId)base.GetParameter("Id");
			bool flag = (bool)base.GetParameter("Prm");
			bool flag2 = (bool)base.GetParameter("Mtng");
			try
			{
				calendarItemBase = base.GetRequestItem<CalendarItemBase>(new PropertyDefinition[]
				{
					CalendarItemBaseSchema.IsMeeting,
					CalendarItemBaseSchema.MeetingRequestWasSent,
					StoreObjectSchema.EffectiveRights
				});
				if (base.IsParameterSet("RcrRngE"))
				{
					ExDateTime endRange = (ExDateTime)base.GetParameter("RcrRngE");
					CalendarItem calendarItem = (CalendarItem)calendarItemBase;
					int num = MeetingUtilities.CancelRecurrence(calendarItem, endRange);
					if (num == -2147483648)
					{
						this.Writer.Write("<div id=divNA></div>");
						return;
					}
					if (0 < num)
					{
						if (!Utilities.IsPublic(calendarItem) && calendarItem.IsMeeting && calendarItem.MeetingRequestWasSent)
						{
							this.Writer.Write("<div id=divOC>");
							this.Writer.Write(num);
							this.Writer.Write("</div>");
							this.Writer.Write("<div id=divW>");
							EndDateRecurrenceRange range = new EndDateRecurrenceRange(calendarItem.Recurrence.Range.StartDate, endRange.IncrementDays(-1));
							this.Writer.Write(CalendarUtilities.GenerateWhen(base.UserContext, calendarItem.StartTime, calendarItem.EndTime, new Recurrence(calendarItem.Recurrence.Pattern, range)));
							this.Writer.Write("</div>");
						}
						else if (!ItemUtility.UserCanEditItem(calendarItem))
						{
							base.ResponseContentType = OwaEventContentType.Html;
							this.Writer.Write("<div id=divNP></div>");
						}
						else
						{
							calendarItem.Recurrence = new Recurrence(calendarItem.Recurrence.Pattern, new EndDateRecurrenceRange(calendarItem.Recurrence.Range.StartDate, endRange.IncrementDays(-1)));
							Utilities.SaveItem(calendarItem, true);
						}
						return;
					}
				}
				if (flag2 && !Utilities.IsPublic(calendarItemBase) && calendarItemBase.MeetingRequestWasSent)
				{
					bool flag3 = true;
					ExDateTime localTime = DateTimeUtilities.GetLocalTime();
					if (calendarItemBase.CalendarItemType == CalendarItemType.RecurringMaster)
					{
						CalendarItem calendarItem2 = (CalendarItem)calendarItemBase;
						if (!(calendarItem2.Recurrence.Range is NoEndRecurrenceRange))
						{
							OccurrenceInfo lastOccurrence = calendarItem2.Recurrence.GetLastOccurrence();
							if (lastOccurrence != null && lastOccurrence.EndTime < localTime)
							{
								flag3 = false;
							}
						}
					}
					else if (calendarItemBase.EndTime < localTime)
					{
						flag3 = false;
					}
					if (flag3)
					{
						Infobar.RenderMessage(this.SanitizingWriter, InfobarMessageType.Informational, SanitizedHtmlString.FromStringId(1328030972), "divCM", false, base.UserContext);
						return;
					}
				}
				if (!CalendarUtilities.UserCanDeleteCalendarItem(calendarItemBase))
				{
					base.ResponseContentType = OwaEventContentType.Html;
					this.Writer.Write("<div id=divNP></div>");
					return;
				}
				calendarItemBase.DeleteMeeting(flag ? DeleteItemFlags.SoftDelete : DeleteItemFlags.MoveToDeletedItems);
			}
			finally
			{
				if (calendarItemBase != null)
				{
					calendarItemBase.Dispose();
					calendarItemBase = null;
				}
			}
			if (!flag2)
			{
				this.SendPrincipalNotification(owaStoreObjectId.ToBase64String(), PrincipalNotificationMessage.ActionType.Delete, false, flag2);
			}
		}

		// Token: 0x06002D94 RID: 11668 RVA: 0x00100EEC File Offset: 0x000FF0EC
		[OwaEventParameter("Id", typeof(OwaStoreObjectId), false, true)]
		[OwaEventParameter("CK", typeof(string), false, true)]
		[OwaEventParameter("Body", typeof(string), false, true)]
		[OwaEvent("PromoteInlineAttachments")]
		public void PromoteInlineAttachments()
		{
			CalendarItemBase calendarItem;
			CalendarItemBase calendarItemBase = calendarItem = this.GetCalendarItem(new PropertyDefinition[0]);
			try
			{
				bool flag = false;
				string text = base.GetParameter("Body") as string;
				if (!string.IsNullOrEmpty(text))
				{
					flag = AttachmentUtility.RemoveUnlinkedInlineAttachments(calendarItemBase, text);
				}
				bool flag2 = AttachmentUtility.PromoteInlineAttachments(calendarItemBase);
				if (flag || flag2)
				{
					calendarItemBase.Load();
					base.WriteChangeKey(calendarItemBase);
					if (flag2)
					{
						ArrayList attachmentInformation = AttachmentWell.GetAttachmentInformation(calendarItemBase, null, base.UserContext.IsPublicLogon);
						RenderingUtilities.RenderAttachmentItems(this.SanitizingWriter, attachmentInformation, base.UserContext);
					}
				}
			}
			finally
			{
				if (calendarItem != null)
				{
					((IDisposable)calendarItem).Dispose();
				}
			}
		}

		// Token: 0x06002D95 RID: 11669 RVA: 0x00100F90 File Offset: 0x000FF190
		[OwaEventParameter("Id", typeof(OwaStoreObjectId))]
		[OwaEvent("GetCancelRecurrenceDialog")]
		public void GetCancelRecurrenceDialog()
		{
			OwaStoreObjectId owaStoreObjectId = (OwaStoreObjectId)base.GetParameter("Id");
			bool isMeeting;
			bool flag;
			bool showWarningAttendeesWillNotBeNotified;
			using (CalendarItemBase readOnlyRequestItem = base.GetReadOnlyRequestItem<CalendarItemBase>(new PropertyDefinition[]
			{
				CalendarItemBaseSchema.IsMeeting,
				CalendarItemBaseSchema.MeetingRequestWasSent
			}))
			{
				isMeeting = readOnlyRequestItem.IsMeeting;
				flag = Utilities.IsPublic(readOnlyRequestItem);
				showWarningAttendeesWillNotBeNotified = (isMeeting && flag && readOnlyRequestItem.MeetingRequestWasSent);
			}
			CalendarUtilities.RenderCancelRecurrenceCalendarItemDialog(this.Writer, false, isMeeting, flag, showWarningAttendeesWillNotBeNotified);
		}

		// Token: 0x06002D96 RID: 11670 RVA: 0x0010101C File Offset: 0x000FF21C
		[OwaEventParameter("RcrI", typeof(int), false, true)]
		[OwaEventParameter("ST", typeof(ExDateTime))]
		[OwaEventParameter("ET", typeof(ExDateTime))]
		[OwaEventParameter("RcrT", typeof(int))]
		[OwaEventParameter("RcrRngE", typeof(ExDateTime), false, true)]
		[OwaEventParameter("RcrDys", typeof(int), false, true)]
		[OwaEventParameter("RcrDy", typeof(int), false, true)]
		[OwaEventParameter("RcrM", typeof(int), false, true)]
		[OwaEventParameter("RcrO", typeof(int), false, true)]
		[OwaEventParameter("RcrRngT", typeof(RecurrenceRangeType))]
		[OwaEventParameter("RcrRngS", typeof(ExDateTime), false, true)]
		[OwaEventParameter("RcrRngO", typeof(int), false, true)]
		[OwaEvent("GetWhenString")]
		public void GetWhenString()
		{
			this.Writer.Write(CalendarUtilities.GenerateWhen(base.UserContext, (ExDateTime)base.GetParameter("ST"), (ExDateTime)base.GetParameter("ET"), base.CreateRecurrenceFromRequest()));
		}

		// Token: 0x06002D97 RID: 11671 RVA: 0x0010105C File Offset: 0x000FF25C
		private CalendarItemBase GetCalendarItem(params PropertyDefinition[] prefetchProperties)
		{
			CalendarItemBase calendarItemBase = null;
			bool flag = false;
			CalendarItemBase result;
			try
			{
				bool flag2 = base.IsParameterSet("Id");
				if (flag2)
				{
					calendarItemBase = base.GetRequestItem<CalendarItemBase>(prefetchProperties);
				}
				else
				{
					ExTraceGlobals.CalendarTracer.TraceDebug((long)this.GetHashCode(), "ItemId is null. Creating new calendar item.");
					OwaStoreObjectId folderId = (OwaStoreObjectId)base.GetParameter("fId");
					calendarItemBase = Utilities.CreateItem<CalendarItem>(folderId);
					if (Globals.ArePerfCountersEnabled)
					{
						OwaSingleCounters.ItemsCreated.Increment();
					}
				}
				flag = true;
				result = calendarItemBase;
			}
			finally
			{
				if (!flag && calendarItemBase != null)
				{
					calendarItemBase.Dispose();
					calendarItemBase = null;
				}
			}
			return result;
		}

		// Token: 0x06002D98 RID: 11672 RVA: 0x001010F0 File Offset: 0x000FF2F0
		private EditCalendarItemEventHandler.CriticalUpdateProperties UpdateCalendarItemProperties(CalendarItemBase calendarItemBase, bool isMeeting, out bool itemUpdated)
		{
			itemUpdated = false;
			EditCalendarItemEventHandler.CriticalUpdateProperties criticalUpdateProperties = EditCalendarItemEventHandler.CriticalUpdateProperties.None;
			base.IsParameterSet("Id");
			if (!calendarItemBase.MeetingRequestWasSent)
			{
				calendarItemBase.IsMeeting = isMeeting;
			}
			calendarItemBase.Subject = (string)base.GetParameter("Subj");
			string location = (string)base.GetParameter("Loc");
			if (!string.Equals(calendarItemBase.Location, (string)base.GetParameter("Loc"), StringComparison.CurrentCulture))
			{
				criticalUpdateProperties |= EditCalendarItemEventHandler.CriticalUpdateProperties.Location;
			}
			calendarItemBase.Location = location;
			bool flag = (bool)base.GetParameter("AllDay");
			ExDateTime exDateTime = (ExDateTime)base.GetParameter("ST");
			ExDateTime exDateTime2 = (ExDateTime)base.GetParameter("ET");
			bool flag2 = false;
			CalendarItem calendarItem = calendarItemBase as CalendarItem;
			if (calendarItem != null && base.IsParameterSet("RcrT"))
			{
				Recurrence recurrence = base.CreateRecurrenceFromRequest();
				if (recurrence != null)
				{
					flag2 = true;
				}
				if ((recurrence != null || calendarItem.Recurrence != null) && (recurrence == null || calendarItem.Recurrence == null || !recurrence.Equals(calendarItem.Recurrence)))
				{
					criticalUpdateProperties |= EditCalendarItemEventHandler.CriticalUpdateProperties.Time;
					calendarItem.Recurrence = recurrence;
				}
				if (calendarItem.Recurrence != null)
				{
					flag = Utilities.IsAllDayEvent(exDateTime, exDateTime2);
				}
			}
			if (flag != calendarItemBase.IsAllDayEvent)
			{
				criticalUpdateProperties |= EditCalendarItemEventHandler.CriticalUpdateProperties.Time;
			}
			calendarItemBase.IsAllDayEvent = flag;
			if (exDateTime != calendarItemBase.StartTime)
			{
				criticalUpdateProperties |= EditCalendarItemEventHandler.CriticalUpdateProperties.Time;
			}
			calendarItemBase.StartTime = exDateTime;
			if (flag && !flag2)
			{
				exDateTime2 = exDateTime2.Date.IncrementDays(1);
			}
			if (exDateTime2 != calendarItemBase.EndTime)
			{
				criticalUpdateProperties |= EditCalendarItemEventHandler.CriticalUpdateProperties.Time;
			}
			if (!calendarItemBase.IsAllDayEvent && exDateTime2 < exDateTime)
			{
				throw new OwaInvalidRequestException("End time can not be before start time");
			}
			if (calendarItemBase.IsAllDayEvent && exDateTime2 < exDateTime)
			{
				calendarItemBase.EndTime = exDateTime;
			}
			else
			{
				calendarItemBase.EndTime = exDateTime2;
			}
			bool flag3 = (bool)base.GetParameter("RemS");
			calendarItemBase[ItemSchema.ReminderIsSet] = flag3;
			if (flag3)
			{
				int num = (int)base.GetParameter("RemT");
				if (num < 0)
				{
					throw new OwaInvalidRequestException("Reminder minutes before start cannot be a negative value");
				}
				calendarItemBase[ItemSchema.ReminderMinutesBeforeStart] = num;
			}
			calendarItemBase.FreeBusyStatus = (Microsoft.Exchange.Data.Storage.BusyType)base.GetParameter("Fbs");
			calendarItemBase[ItemSchema.IsResponseRequested] = (bool)base.GetParameter("RR");
			calendarItemBase.Importance = (Importance)base.GetParameter("Imp");
			if (calendarItemBase.CalendarItemType == CalendarItemType.Single || calendarItemBase.CalendarItemType == CalendarItemType.RecurringMaster)
			{
				calendarItemBase[ItemSchema.Sensitivity] = (((bool)base.GetParameter("Prvt")) ? Sensitivity.Private : Sensitivity.Normal);
			}
			object parameter = base.GetParameter("Body");
			object parameter2 = base.GetParameter("Text");
			if (parameter != null && parameter2 != null)
			{
				Markup markup = ((bool)parameter2) ? Markup.PlainText : Markup.Html;
				itemUpdated = BodyConversionUtilities.SetBody(calendarItemBase, (string)parameter, markup, StoreObjectType.CalendarItem, base.UserContext);
				if (itemUpdated)
				{
					calendarItemBase.Load();
				}
			}
			if (!Utilities.IsPublic(calendarItemBase))
			{
				calendarItemBase.AttendeeCollection.Clear();
				this.SetCalendarItemRecipients(calendarItemBase.AttendeeCollection, "To", AttendeeType.Required);
				this.SetCalendarItemRecipients(calendarItemBase.AttendeeCollection, "Cc", AttendeeType.Optional);
				this.SetCalendarItemRecipients(calendarItemBase.AttendeeCollection, "Bcc", AttendeeType.Resource);
				if (calendarItemBase.AttendeesChanged)
				{
					criticalUpdateProperties |= EditCalendarItemEventHandler.CriticalUpdateProperties.Attendees;
				}
			}
			return criticalUpdateProperties;
		}

		// Token: 0x06002D99 RID: 11673 RVA: 0x00101438 File Offset: 0x000FF638
		private void SetCalendarItemRecipients(IAttendeeCollection attendees, string wellName, AttendeeType attendeeType)
		{
			this.Writer.Write("<div id=\"");
			this.Writer.Write(wellName);
			this.Writer.Write("\">");
			RecipientInfo[] array = (RecipientInfo[])base.GetParameter(wellName);
			if (array == null)
			{
				this.Writer.Write("</div>");
				return;
			}
			List<Participant> list = new List<Participant>();
			foreach (RecipientInfo recipientInfo in array)
			{
				base.GetExchangeParticipantsFromRecipientInfo(recipientInfo, list);
			}
			for (int j = 0; j < list.Count; j++)
			{
				attendees.Add(list[j], attendeeType, null, null, false);
			}
			this.Writer.Write("</div>");
		}

		// Token: 0x06002D9A RID: 11674 RVA: 0x00101500 File Offset: 0x000FF700
		private void SendPrincipalNotification(string itemId, PrincipalNotificationMessage.ActionType actionType, bool isNewItem, bool isMeeting)
		{
			bool flag = false;
			if (base.IsParameterSet("NtfyPrncpl"))
			{
				flag = (bool)base.GetParameter("NtfyPrncpl");
			}
			if (flag)
			{
				OwaStoreObjectId folderId = (OwaStoreObjectId)base.GetParameter("fId");
				PrincipalNotificationMessage principalNotificationMessage = new PrincipalNotificationMessage(itemId, folderId, base.UserContext, this.HttpContext, actionType, isNewItem, isMeeting);
				principalNotificationMessage.SendNotificationMessage();
			}
		}

		// Token: 0x06002D9B RID: 11675 RVA: 0x00101560 File Offset: 0x000FF760
		[OwaEventParameter("Dta", typeof(int))]
		[OwaEvent("UpdateScheduling")]
		[OwaEventParameter("Dur", typeof(int))]
		[OwaEventParameter("Shw24", typeof(bool))]
		[OwaEventParameter("Rcp", typeof(SchedulingRecipientInfo), true, false)]
		[OwaEventParameter("Dt", typeof(ExDateTime))]
		[OwaEventParameter("Mo", typeof(ExDateTime))]
		[OwaEventParameter("Rcr", typeof(bool))]
		[OwaEventParameter("fId", typeof(OwaStoreObjectId), false, true)]
		public void UpdateScheduling()
		{
			ExTraceGlobals.CalendarCallTracer.TraceDebug((long)this.GetHashCode(), "EditCalendarItemEventHandler.UpdateScheduling");
			EditCalendarItemEventHandler.SchedulingUpdatesData schedulingUpdatesData = (EditCalendarItemEventHandler.SchedulingUpdatesData)base.GetParameter("Dta");
			bool flag = (bool)base.GetParameter("Shw24");
			int num = (int)base.GetParameter("Dur");
			bool flag2 = (bool)base.GetParameter("Rcr");
			SchedulingRecipientInfo[] array = (SchedulingRecipientInfo[])base.GetParameter("Rcp");
			ExDateTime date = DateTimeUtilities.GetLocalTime().Date;
			ExDateTime date2 = ((ExDateTime)base.GetParameter("Dt")).Date;
			ExDateTime month = (ExDateTime)base.GetParameter("Mo");
			OwaStoreObjectId owaStoreObjectId = (OwaStoreObjectId)base.GetParameter("fId");
			if (!flag && owaStoreObjectId != null && owaStoreObjectId.IsOtherMailbox)
			{
				throw new OwaInvalidRequestException("Cannot show working hour when create appointment or meeting request in other's calendar");
			}
			ExDateTime minValue = ExDateTime.MinValue;
			ExDateTime minValue2 = ExDateTime.MinValue;
			ExDateTime exDateTime = ExDateTime.MinValue;
			ExDateTime exDateTime2 = ExDateTime.MinValue;
			AvailabilityQueryResult availabilityQueryResult = null;
			string value = string.Empty;
			bool flag3 = false;
			if (schedulingUpdatesData == EditCalendarItemEventHandler.SchedulingUpdatesData.None)
			{
				throw new OwaInvalidRequestException("SchedulingUpdatesData.None");
			}
			if (array.Length == 0)
			{
				throw new OwaInvalidRequestException("There must be at least 1 recipient");
			}
			if (Configuration.MaximumIdentityArraySize < array.Length)
			{
				string description = string.Format(LocalizedStrings.GetNonEncoded(-1838527881), Configuration.MaximumIdentityArraySize);
				string message = string.Format("Number of recipients excedes limit of {0}", Configuration.MaximumIdentityArraySize);
				throw new OwaEventHandlerException(message, description);
			}
			Microsoft.Exchange.Clients.Owa.Core.WorkingHours workingHours;
			if (owaStoreObjectId == null || !owaStoreObjectId.IsOtherMailbox)
			{
				workingHours = base.UserContext.WorkingHours;
			}
			else
			{
				workingHours = base.UserContext.GetOthersWorkingHours(owaStoreObjectId);
			}
			if (EditCalendarItemEventHandler.SchedulingUpdatesData.None < (schedulingUpdatesData & EditCalendarItemEventHandler.SchedulingUpdatesData.SelectedDate))
			{
				int num2 = ExDateTime.DaysInMonth(month.Year, month.Month);
				if (num2 < date2.Day)
				{
					date2 = new ExDateTime(base.UserContext.TimeZone, month.Year, month.Month, num2);
				}
				else
				{
					date2 = new ExDateTime(base.UserContext.TimeZone, month.Year, month.Month, date2.Day);
				}
			}
			DatePickerBase.GetVisibleDateRange(month, out minValue, out minValue2, base.UserContext.TimeZone);
			if (date2 < minValue || minValue2 < date2)
			{
				throw new OwaInvalidRequestException("Selected date must be a date visible in the grid");
			}
			if (month.Year < date.Year || (date.Year == month.Year && month.Month < date.Month) || num < 30 || 1440 < num)
			{
				ExTraceGlobals.CalendarTracer.TraceDebug((long)this.GetHashCode(), "Not getting suggestion buckets because the date picker month is in the past, the duration of the meeting is too short, or the duration is too long");
				schedulingUpdatesData &= ~EditCalendarItemEventHandler.SchedulingUpdatesData.SuggestionBuckets;
				flag3 = true;
			}
			if (flag2)
			{
				value = LocalizedStrings.GetHtmlEncoded(-438819805);
				schedulingUpdatesData &= ~EditCalendarItemEventHandler.SchedulingUpdatesData.Suggestions;
			}
			if (date2 < date)
			{
				value = LocalizedStrings.GetHtmlEncoded(1443871515);
				schedulingUpdatesData &= ~EditCalendarItemEventHandler.SchedulingUpdatesData.Suggestions;
			}
			else if (num < 30)
			{
				value = string.Format(LocalizedStrings.GetHtmlEncoded(-1299787530), 30);
				schedulingUpdatesData &= ~EditCalendarItemEventHandler.SchedulingUpdatesData.Suggestions;
			}
			else if (1440 < num)
			{
				value = LocalizedStrings.GetHtmlEncoded(-2065540826);
				schedulingUpdatesData &= ~EditCalendarItemEventHandler.SchedulingUpdatesData.Suggestions;
			}
			if (EditCalendarItemEventHandler.SchedulingUpdatesData.None < (schedulingUpdatesData & EditCalendarItemEventHandler.SchedulingUpdatesData.SuggestionBuckets) || EditCalendarItemEventHandler.SchedulingUpdatesData.None < (schedulingUpdatesData & EditCalendarItemEventHandler.SchedulingUpdatesData.Suggestions))
			{
				if ((schedulingUpdatesData & EditCalendarItemEventHandler.SchedulingUpdatesData.SuggestionBuckets) == EditCalendarItemEventHandler.SchedulingUpdatesData.None)
				{
					exDateTime = date2;
					exDateTime2 = exDateTime.IncrementDays(1);
				}
				else
				{
					if (month.Year == date.Year && month.Month == date.Month)
					{
						exDateTime = date;
					}
					else
					{
						exDateTime = minValue;
					}
					exDateTime2 = minValue2.IncrementDays(1);
				}
			}
			AvailabilityQuery availabilityQuery = null;
			FreeBusyViewOptions freeBusyViewOptions = null;
			bool flag4 = EditCalendarItemEventHandler.SchedulingUpdatesData.None < (schedulingUpdatesData & EditCalendarItemEventHandler.SchedulingUpdatesData.AllRecipientsFreeBusyData) || EditCalendarItemEventHandler.SchedulingUpdatesData.None < (schedulingUpdatesData & EditCalendarItemEventHandler.SchedulingUpdatesData.NewRecipientsFreeBusyData);
			if (flag4 || EditCalendarItemEventHandler.SchedulingUpdatesData.None < (schedulingUpdatesData & EditCalendarItemEventHandler.SchedulingUpdatesData.Suggestions) || EditCalendarItemEventHandler.SchedulingUpdatesData.None < (schedulingUpdatesData & EditCalendarItemEventHandler.SchedulingUpdatesData.SuggestionBuckets))
			{
				availabilityQuery = new AvailabilityQuery();
				availabilityQuery.MailboxArray = new MailboxData[array.Length];
				availabilityQuery.ClientContext = ClientContext.Create(base.UserContext.LogonIdentity.ClientSecurityContext, base.UserContext.ExchangePrincipal.MailboxInfo.OrganizationId, OwaContext.TryGetCurrentBudget(), base.UserContext.TimeZone, base.UserContext.UserCulture, AvailabilityQuery.CreateNewMessageId());
				bool flag5 = flag || SchedulingTabRenderingUtilities.CalculateTotalWorkingHours(workingHours) * 60 < num;
				for (int i = 0; i < array.Length; i++)
				{
					availabilityQuery.MailboxArray[i] = new MailboxData();
					availabilityQuery.MailboxArray[i].Email = new EmailAddress();
					availabilityQuery.MailboxArray[i].Email.Address = array[i].EmailAddress;
					availabilityQuery.MailboxArray[i].Email.RoutingType = array[i].RoutingType;
					availabilityQuery.MailboxArray[i].AttendeeType = array[i].AttendeeType;
					availabilityQuery.MailboxArray[i].ExcludeConflicts = false;
				}
				if (EditCalendarItemEventHandler.SchedulingUpdatesData.None < (schedulingUpdatesData & EditCalendarItemEventHandler.SchedulingUpdatesData.NewRecipientsFreeBusyData) || EditCalendarItemEventHandler.SchedulingUpdatesData.None < (schedulingUpdatesData & EditCalendarItemEventHandler.SchedulingUpdatesData.AllRecipientsFreeBusyData))
				{
					freeBusyViewOptions = new FreeBusyViewOptions();
					freeBusyViewOptions.TimeWindow = new Duration();
					freeBusyViewOptions.TimeWindow.StartTime = ((DateTime)minValue).Date;
					freeBusyViewOptions.TimeWindow.EndTime = ((DateTime)minValue2.IncrementDays(1)).Date;
					freeBusyViewOptions.MergedFreeBusyIntervalInMinutes = 30;
					freeBusyViewOptions.RequestedView = FreeBusyViewType.MergedOnly;
					availabilityQuery.DesiredFreeBusyView = freeBusyViewOptions;
					ExTraceGlobals.CalendarTracer.TraceDebug<DateTime, DateTime>((long)this.GetHashCode(), "Getting free/busy data from {0} to {1}", freeBusyViewOptions.TimeWindow.StartTime, freeBusyViewOptions.TimeWindow.EndTime);
				}
				if (EditCalendarItemEventHandler.SchedulingUpdatesData.None < (schedulingUpdatesData & EditCalendarItemEventHandler.SchedulingUpdatesData.Suggestions) || EditCalendarItemEventHandler.SchedulingUpdatesData.None < (schedulingUpdatesData & EditCalendarItemEventHandler.SchedulingUpdatesData.SuggestionBuckets))
				{
					SuggestionsViewOptions suggestionsViewOptions = new SuggestionsViewOptions();
					suggestionsViewOptions.DetailedSuggestionsWindow = new Duration();
					suggestionsViewOptions.DetailedSuggestionsWindow.StartTime = ((DateTime)exDateTime).Date;
					suggestionsViewOptions.DetailedSuggestionsWindow.EndTime = ((DateTime)exDateTime2).Date;
					suggestionsViewOptions.MeetingDurationInMinutes = num;
					if (flag5)
					{
						if (num == 1440)
						{
							suggestionsViewOptions.MaximumNonWorkHourResultsByDay = 1;
							suggestionsViewOptions.MaximumResultsByDay = 1;
						}
						else if (workingHours.IsWorkDay(date2.DayOfWeek))
						{
							suggestionsViewOptions.MaximumNonWorkHourResultsByDay = 2;
						}
						else
						{
							suggestionsViewOptions.MaximumNonWorkHourResultsByDay = suggestionsViewOptions.MaximumResultsByDay;
						}
					}
					availabilityQuery.DesiredSuggestionsView = suggestionsViewOptions;
					ExTraceGlobals.CalendarTracer.TraceDebug<DateTime, DateTime>((long)this.GetHashCode(), "Getting suggestions from {0} to {1}", suggestionsViewOptions.DetailedSuggestionsWindow.StartTime, suggestionsViewOptions.DetailedSuggestionsWindow.EndTime);
				}
				availabilityQueryResult = Utilities.ExecuteAvailabilityQuery(availabilityQuery);
			}
			if (flag4)
			{
				SchedulingTabRenderingUtilities.RenderRecipientFreeBusyData(this.Writer, array, (availabilityQueryResult == null) ? null : availabilityQueryResult.FreeBusyResults, new ExDateTime(base.UserContext.TimeZone, freeBusyViewOptions.TimeWindow.StartTime), new ExDateTime(base.UserContext.TimeZone, freeBusyViewOptions.TimeWindow.EndTime), flag, EditCalendarItemEventHandler.SchedulingUpdatesData.None < (schedulingUpdatesData & EditCalendarItemEventHandler.SchedulingUpdatesData.AllRecipientsFreeBusyData), base.UserContext.TimeZone, workingHours);
			}
			SuggestionDayResult suggestionDayResult = null;
			SuggestionDayResult[] array2 = null;
			if (EditCalendarItemEventHandler.SchedulingUpdatesData.None < (schedulingUpdatesData & EditCalendarItemEventHandler.SchedulingUpdatesData.Suggestions) || EditCalendarItemEventHandler.SchedulingUpdatesData.None < (schedulingUpdatesData & EditCalendarItemEventHandler.SchedulingUpdatesData.SuggestionBuckets))
			{
				if (EditCalendarItemEventHandler.SchedulingUpdatesData.None < (schedulingUpdatesData & EditCalendarItemEventHandler.SchedulingUpdatesData.SuggestionBuckets))
				{
					ExTraceGlobals.CalendarTracer.TraceDebug<ExDateTime, ExDateTime>((long)this.GetHashCode(), "Getting suggestions from {0} to {1}", exDateTime, exDateTime2);
				}
				array2 = availabilityQueryResult.DailyMeetingSuggestions;
				if (availabilityQueryResult != null && EditCalendarItemEventHandler.SchedulingUpdatesData.None < (schedulingUpdatesData & EditCalendarItemEventHandler.SchedulingUpdatesData.Suggestions))
				{
					ExTraceGlobals.CalendarTracer.TraceDebug<ExDateTime>((long)this.GetHashCode(), "Getting suggestions for {0}", date2);
					if (availabilityQueryResult.DailyMeetingSuggestions != null)
					{
						for (int j = 0; j < array2.Length; j++)
						{
							if (array2[j].Date.Date.Equals(((DateTime)date2).Date))
							{
								suggestionDayResult = array2[j];
								break;
							}
						}
					}
				}
			}
			if (EditCalendarItemEventHandler.SchedulingUpdatesData.None < (schedulingUpdatesData & EditCalendarItemEventHandler.SchedulingUpdatesData.SuggestionBuckets) || flag3)
			{
				this.Writer.Write("<div id=sb>");
				SchedulingTabRenderingUtilities.RenderSuggestionQualities(this.Writer, array2, flag3, minValue, minValue2, workingHours);
				this.Writer.Write("</div>");
			}
			Suggestion[] array3 = null;
			if (EditCalendarItemEventHandler.SchedulingUpdatesData.None < (schedulingUpdatesData & EditCalendarItemEventHandler.SchedulingUpdatesData.Suggestions))
			{
				if (suggestionDayResult == null)
				{
					value = LocalizedStrings.GetHtmlEncoded(376047783);
				}
				else
				{
					array3 = suggestionDayResult.SuggestionArray;
					if (array3.Length == 0)
					{
						if (!workingHours.IsWorkDay(date2.DayOfWeek))
						{
							value = LocalizedStrings.GetHtmlEncoded(1274187420);
						}
						else
						{
							value = LocalizedStrings.GetHtmlEncoded(376047783);
						}
					}
				}
			}
			this.Writer.Write("<div id=sugLst>");
			if (!string.IsNullOrEmpty(value))
			{
				this.Writer.Write("<div class=err>");
				this.Writer.Write(value);
				this.Writer.Write("</div>");
			}
			else
			{
				SchedulingTabRenderingUtilities.RenderSuggestions(this.Writer, array3, availabilityQuery.MailboxArray, array, base.UserContext);
			}
			this.Writer.Write("</div>");
			if (EditCalendarItemEventHandler.SchedulingUpdatesData.None < (schedulingUpdatesData & EditCalendarItemEventHandler.SchedulingUpdatesData.DatePicker))
			{
				this.Writer.Write("<div id=dp>");
				DatePicker datePicker = new DatePicker(string.Empty, new ExDateTime[]
				{
					date2
				});
				datePicker.RenderMonth(this.Writer);
				this.Writer.Write("</div>");
			}
			if (EditCalendarItemEventHandler.SchedulingUpdatesData.None < (schedulingUpdatesData & EditCalendarItemEventHandler.SchedulingUpdatesData.FreeBusyGridHeaders))
			{
				int num3 = flag ? 24 : SchedulingTabRenderingUtilities.CalculateTotalWorkingHours(workingHours);
				if (num3 > 0)
				{
					this.Writer.Write("<div id=fbgD>a_iDys = ");
					this.Writer.Write(((DateTime)minValue2 - (DateTime)minValue).Days + 1);
					this.Writer.Write(";\na_iHrs = ");
					this.Writer.Write(num3);
					this.Writer.Write(";\nvar dtSD = ");
					RenderingUtilities.RenderDateTimeScriptObject(this.Writer, date2);
					this.Writer.Write(";\na_dtFBS = ");
					RenderingUtilities.RenderDateTimeScriptObject(this.Writer, minValue);
					this.Writer.Write(";\na_dtFBE = ");
					RenderingUtilities.RenderDateTimeScriptObject(this.Writer, minValue2);
					this.Writer.Write(";\na_rgDNms = new Array(");
					SchedulingTabRenderingUtilities.RenderGridDayNames(this.Writer, minValue, minValue2);
					this.Writer.Write(");</div>");
				}
			}
		}

		// Token: 0x06002D9C RID: 11676 RVA: 0x00101F1C File Offset: 0x0010011C
		private void UpdateAutoCompleteCache()
		{
			RecipientInfoAC[] array = (RecipientInfoAC[])base.GetParameter("Recips");
			if (array != null && array.Length > 0)
			{
				AutoCompleteCache.UpdateAutoCompleteCacheFromRecipientInfoList(array, base.OwaContext.UserContext);
			}
		}

		// Token: 0x04001E3F RID: 7743
		public const string EventNamespace = "EditCalendarItem";

		// Token: 0x04001E40 RID: 7744
		public const string MethodGetRoomsCache = "GRC";

		// Token: 0x04001E41 RID: 7745
		public const string MethodSend = "Send";

		// Token: 0x04001E42 RID: 7746
		public const string MethodCancelMeeting = "CM";

		// Token: 0x04001E43 RID: 7747
		public const string MethodGetCancelRecurrenceDialog = "GetCancelRecurrenceDialog";

		// Token: 0x04001E44 RID: 7748
		public const string MethodGetWhenString = "GetWhenString";

		// Token: 0x04001E45 RID: 7749
		public const string MethodUpdateScheduling = "UpdateScheduling";

		// Token: 0x04001E46 RID: 7750
		public const string MethodLoadRecurrenceDialog = "LRD";

		// Token: 0x04001E47 RID: 7751
		public const string MethodLoadSchedulingTab = "LST";

		// Token: 0x04001E48 RID: 7752
		public const string MethodLoadTrackingTab = "LTT";

		// Token: 0x04001E49 RID: 7753
		public const string Subject = "Subj";

		// Token: 0x04001E4A RID: 7754
		public const string Importance = "Imp";

		// Token: 0x04001E4B RID: 7755
		public const string Private = "Prvt";

		// Token: 0x04001E4C RID: 7756
		public const string To = "To";

		// Token: 0x04001E4D RID: 7757
		public const string Cc = "Cc";

		// Token: 0x04001E4E RID: 7758
		public const string Bcc = "Bcc";

		// Token: 0x04001E4F RID: 7759
		public const string RequestResponse = "RR";

		// Token: 0x04001E50 RID: 7760
		public const string Location = "Loc";

		// Token: 0x04001E51 RID: 7761
		public const string Start = "ST";

		// Token: 0x04001E52 RID: 7762
		public const string End = "ET";

		// Token: 0x04001E53 RID: 7763
		public const string AllDay = "AllDay";

		// Token: 0x04001E54 RID: 7764
		public const string ReminderSet = "RemS";

		// Token: 0x04001E55 RID: 7765
		public const string ReminderTime = "RemT";

		// Token: 0x04001E56 RID: 7766
		public const string FreeBusyStatus = "Fbs";

		// Token: 0x04001E57 RID: 7767
		public const string IsMeeting = "Mtng";

		// Token: 0x04001E58 RID: 7768
		public const string Body = "Body";

		// Token: 0x04001E59 RID: 7769
		public const string Text = "Text";

		// Token: 0x04001E5A RID: 7770
		public const string ForceSend = "FrcSnd";

		// Token: 0x04001E5B RID: 7771
		public const string ForceSave = "FrcSv";

		// Token: 0x04001E5C RID: 7772
		public const string IsToAllAttendees = "SndAll";

		// Token: 0x04001E5D RID: 7773
		public const string SchedulingData = "Dta";

		// Token: 0x04001E5E RID: 7774
		public const string Duration = "Dur";

		// Token: 0x04001E5F RID: 7775
		public const string SelectedDate = "Dt";

		// Token: 0x04001E60 RID: 7776
		public const string Month = "Mo";

		// Token: 0x04001E61 RID: 7777
		public const string Recipients = "Rcp";

		// Token: 0x04001E62 RID: 7778
		public const string AutoCompleteRecipients = "Recips";

		// Token: 0x04001E63 RID: 7779
		public const string Show24Hours = "Shw24";

		// Token: 0x04001E64 RID: 7780
		public const string Recurring = "Rcr";

		// Token: 0x04001E65 RID: 7781
		public const string IsPermanentDelete = "Prm";

		// Token: 0x04001E66 RID: 7782
		public const string SortedColumn = "sc";

		// Token: 0x04001E67 RID: 7783
		public const string NotifyPrincipal = "NtfyPrncpl";

		// Token: 0x0200049F RID: 1183
		[Flags]
		private enum CriticalUpdateProperties
		{
			// Token: 0x04001E69 RID: 7785
			None = 0,
			// Token: 0x04001E6A RID: 7786
			Location = 1,
			// Token: 0x04001E6B RID: 7787
			Time = 2,
			// Token: 0x04001E6C RID: 7788
			Attendees = 4
		}

		// Token: 0x020004A0 RID: 1184
		[Flags]
		private enum SchedulingUpdatesData
		{
			// Token: 0x04001E6E RID: 7790
			None = 0,
			// Token: 0x04001E6F RID: 7791
			Suggestions = 1,
			// Token: 0x04001E70 RID: 7792
			FreeBusyGridHeaders = 2,
			// Token: 0x04001E71 RID: 7793
			DatePicker = 4,
			// Token: 0x04001E72 RID: 7794
			SuggestionBuckets = 8,
			// Token: 0x04001E73 RID: 7795
			NewRecipientsFreeBusyData = 16,
			// Token: 0x04001E74 RID: 7796
			AllRecipientsFreeBusyData = 32,
			// Token: 0x04001E75 RID: 7797
			SelectedDate = 64
		}
	}
}
