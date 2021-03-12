using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020003E2 RID: 994
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class MeetingResponse : MeetingMessageInstance, IMeetingResponse, IMeetingMessageInstance, IMeetingMessage, IMessageItem, IToDoItem, IItem, IStoreObject, IStorePropertyBag, IPropertyBag, IReadOnlyPropertyBag, IDisposable
	{
		// Token: 0x06002D59 RID: 11609 RVA: 0x000BAD73 File Offset: 0x000B8F73
		internal MeetingResponse(ICoreItem coreItem) : base(coreItem)
		{
		}

		// Token: 0x17000E9A RID: 3738
		// (get) Token: 0x06002D5A RID: 11610 RVA: 0x000BAD7C File Offset: 0x000B8F7C
		public override Schema Schema
		{
			get
			{
				this.CheckDisposed("Schema::get");
				return MeetingResponseSchema.Instance;
			}
		}

		// Token: 0x17000E9B RID: 3739
		// (get) Token: 0x06002D5B RID: 11611 RVA: 0x000BAD8E File Offset: 0x000B8F8E
		public ResponseType ResponseType
		{
			get
			{
				this.CheckDisposed("ResponseType::get");
				return (ResponseType)this[MeetingResponseSchema.ResponseType];
			}
		}

		// Token: 0x17000E9C RID: 3740
		// (get) Token: 0x06002D5C RID: 11612 RVA: 0x000BADAB File Offset: 0x000B8FAB
		public StoreObjectId AssociatedMeetingRequestId
		{
			get
			{
				this.CheckDisposed("AssociatedMeetingRequestId");
				if (this.AssociatedItemId != null)
				{
					return this.AssociatedItemId.ObjectId;
				}
				return null;
			}
		}

		// Token: 0x17000E9D RID: 3741
		// (get) Token: 0x06002D5D RID: 11613 RVA: 0x000BADCD File Offset: 0x000B8FCD
		public ExDateTime AttendeeCriticalChangeTime
		{
			get
			{
				this.CheckDisposed("AttendeeCriticalChangeTime::get");
				return base.GetValueOrDefault<ExDateTime>(CalendarItemBaseSchema.AttendeeCriticalChangeTime, ExDateTime.MinValue);
			}
		}

		// Token: 0x17000E9E RID: 3742
		// (get) Token: 0x06002D5E RID: 11614 RVA: 0x000BADEA File Offset: 0x000B8FEA
		// (set) Token: 0x06002D5F RID: 11615 RVA: 0x000BAE07 File Offset: 0x000B9007
		public string Location
		{
			get
			{
				this.CheckDisposed("Location::get");
				return (string)this[InternalSchema.Location];
			}
			set
			{
				this.CheckDisposed("Location::set");
				this[InternalSchema.Location] = value;
			}
		}

		// Token: 0x17000E9F RID: 3743
		// (get) Token: 0x06002D60 RID: 11616 RVA: 0x000BAE20 File Offset: 0x000B9020
		// (set) Token: 0x06002D61 RID: 11617 RVA: 0x000BAE3D File Offset: 0x000B903D
		public ExDateTime ProposedStart
		{
			get
			{
				this.CheckDisposed("ProposedStart::get");
				return base.GetValueOrDefault<ExDateTime>(InternalSchema.AppointmentCounterStartWhole, CalendarItemBase.OutlookRtmNone);
			}
			private set
			{
				this.CheckDisposed("ProposedStart::set");
				this[InternalSchema.AppointmentCounterStartWhole] = value;
			}
		}

		// Token: 0x17000EA0 RID: 3744
		// (get) Token: 0x06002D62 RID: 11618 RVA: 0x000BAE5B File Offset: 0x000B905B
		// (set) Token: 0x06002D63 RID: 11619 RVA: 0x000BAE78 File Offset: 0x000B9078
		public ExDateTime ProposedEnd
		{
			get
			{
				this.CheckDisposed("ProposedEnd::get");
				return base.GetValueOrDefault<ExDateTime>(InternalSchema.AppointmentCounterEndWhole, CalendarItemBase.OutlookRtmNone);
			}
			private set
			{
				this.CheckDisposed("ProposedEnd::set");
				this[InternalSchema.AppointmentCounterEndWhole] = value;
			}
		}

		// Token: 0x17000EA1 RID: 3745
		// (get) Token: 0x06002D64 RID: 11620 RVA: 0x000BAE96 File Offset: 0x000B9096
		public bool IsCounterProposal
		{
			get
			{
				return this.ProposedStart != CalendarItemBase.OutlookRtmNone && this.ProposedEnd != CalendarItemBase.OutlookRtmNone;
			}
		}

		// Token: 0x17000EA2 RID: 3746
		// (get) Token: 0x06002D65 RID: 11621 RVA: 0x000BAEBC File Offset: 0x000B90BC
		public bool IsSilent
		{
			get
			{
				return base.GetValueOrDefault<bool>(InternalSchema.IsSilent, false);
			}
		}

		// Token: 0x06002D66 RID: 11622 RVA: 0x000BAECC File Offset: 0x000B90CC
		internal void SetTimeProposal(ExDateTime proposedStart, ExDateTime proposedEnd)
		{
			this.CheckDisposed("SetTimeProposal");
			ArgumentValidator.ThrowIfNull("proposedStart", proposedStart);
			ArgumentValidator.ThrowIfNull("proposedEnd", proposedEnd);
			this.ProposedStart = proposedStart;
			this.ProposedEnd = proposedEnd;
			this[InternalSchema.AppointmentCounterProposal] = true;
			this[InternalSchema.AppointmentProposedDuration] = (int)(proposedEnd - proposedStart).TotalMinutes;
		}

		// Token: 0x06002D67 RID: 11623 RVA: 0x000BAF43 File Offset: 0x000B9143
		public new static MeetingResponse Bind(StoreSession session, StoreId storeId)
		{
			return MeetingResponse.Bind(session, storeId, null);
		}

		// Token: 0x06002D68 RID: 11624 RVA: 0x000BAF4D File Offset: 0x000B914D
		public new static MeetingResponse Bind(StoreSession session, StoreId storeId, ICollection<PropertyDefinition> propsToReturn)
		{
			return ItemBuilder.ItemBind<MeetingResponse>(session, storeId, MeetingResponseSchema.Instance, propsToReturn);
		}

		// Token: 0x06002D69 RID: 11625 RVA: 0x000BAF5C File Offset: 0x000B915C
		public override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<MeetingResponse>(this);
		}

		// Token: 0x06002D6A RID: 11626 RVA: 0x000BAF64 File Offset: 0x000B9164
		internal static void CoreObjectUpdateIsSilent(CoreItem coreItem)
		{
			if (((ICoreItem)coreItem).AreOptionalAutoloadPropertiesLoaded)
			{
				coreItem.PropertyBag[InternalSchema.IsSilent] = (coreItem.AttachmentCollection.Count == 0 && coreItem.Body.PreviewText.Trim().Length == 0);
			}
		}

		// Token: 0x06002D6B RID: 11627 RVA: 0x000BAFB8 File Offset: 0x000B91B8
		public static MeetingResponse CreateMeetingResponse(MailboxSession mailboxSession, ResponseType responseType)
		{
			MeetingResponse meetingResponse = ItemBuilder.CreateNewItem<MeetingResponse>(mailboxSession, CalendarItemBase.GetDraftsFolderIdOrThrow(mailboxSession), ItemCreateInfo.MeetingResponseInfo);
			meetingResponse.Initialize("IPM.Schedule.Meeting.Resp", responseType);
			return meetingResponse;
		}

		// Token: 0x06002D6C RID: 11628 RVA: 0x000BAFE4 File Offset: 0x000B91E4
		public static MeetingResponse CreateMeetingResponseSeries(MailboxSession mailboxSession, ResponseType responseType)
		{
			MeetingResponse meetingResponse = ItemBuilder.CreateNewItem<MeetingResponse>(mailboxSession, CalendarItemBase.GetDraftsFolderIdOrThrow(mailboxSession), ItemCreateInfo.MeetingResponseSeriesInfo);
			meetingResponse.Initialize("IPM.MeetingMessageSeries.Resp", responseType);
			return meetingResponse;
		}

		// Token: 0x06002D6D RID: 11629 RVA: 0x000BB010 File Offset: 0x000B9210
		internal void Initialize(string itemClassPrefix, ResponseType responseType)
		{
			base.Initialize();
			EnumValidator.AssertValid<ResponseType>(responseType);
			base.LocationIdentifierHelperInstance.SetLocationIdentifier(55413U);
			this[InternalSchema.ItemClass] = MeetingResponse.ItemClassFromResponseType(itemClassPrefix, responseType);
			this[InternalSchema.IconIndex] = IconIndex.AppointmentMeet;
		}

		// Token: 0x06002D6E RID: 11630 RVA: 0x000BB060 File Offset: 0x000B9260
		protected internal override int CompareToCalendarItem(CalendarItemBase correlatedCalendarItem)
		{
			int num = 0;
			Attendee attendee = null;
			bool flag = false;
			if (correlatedCalendarItem != null && base.IsRepairUpdateMessage)
			{
				attendee = this.FindOrAddAttendee(correlatedCalendarItem, out flag);
			}
			if ((!base.IsRepairUpdateMessage || !flag) && correlatedCalendarItem != null && correlatedCalendarItem.Id != null)
			{
				int? valueAsNullable = base.PropertyBag.GetValueAsNullable<int>(InternalSchema.AppointmentSequenceNumber);
				int? valueAsNullable2 = correlatedCalendarItem.PropertyBag.GetValueAsNullable<int>(InternalSchema.AppointmentSequenceNumber);
				if (valueAsNullable != null && valueAsNullable2 != null && valueAsNullable.Value != valueAsNullable2.Value)
				{
					num = valueAsNullable.Value - valueAsNullable2.Value;
				}
				else
				{
					if (!base.IsRepairUpdateMessage)
					{
						attendee = this.FindOrAddAttendee(correlatedCalendarItem, out flag);
					}
					if (attendee != null)
					{
						ExDateTime valueOrDefault = base.GetValueOrDefault<ExDateTime>(InternalSchema.AttendeeCriticalChangeTime, base.SentTime);
						num = ExDateTime.Compare(valueOrDefault, attendee.ReplyTime, Util.DateTimeComparisonRange);
					}
				}
			}
			if (num < 0)
			{
				ExTraceGlobals.MeetingMessageTracer.Information<GlobalObjectId>((long)this.GetHashCode(), "Storage.MeetingMessage.IsOutOfDate: GOID={0}; isOutOfDate=true", this.GlobalObjectId);
			}
			return num;
		}

		// Token: 0x17000EA3 RID: 3747
		// (get) Token: 0x06002D6F RID: 11631 RVA: 0x000BB15A File Offset: 0x000B935A
		protected override bool ShouldBeSentFromOrganizer
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06002D70 RID: 11632 RVA: 0x000BB160 File Offset: 0x000B9360
		protected override void UpdateCalendarItemInternal(ref CalendarItemBase originalCalendarItem)
		{
			ExTraceGlobals.MeetingMessageTracer.Information<GlobalObjectId>((long)this.GetHashCode(), "Storage.MeetingResponse.UpdateCalendarItemInternal: GOID={0}", this.GlobalObjectId);
			CalendarItemBase calendarItemBase = originalCalendarItem;
			bool flag;
			Attendee attendee = this.FindOrAddAttendee(calendarItemBase, out flag);
			if (attendee == null)
			{
				return;
			}
			if (this.IsAttendeeResponseOutOfDate(attendee))
			{
				ExTraceGlobals.MeetingMessageTracer.Information<GlobalObjectId>((long)this.GetHashCode(), "Storage.MeetingResponse.UpdateCalendarItemInternal: GOID={0}. Old Response, doing nothing", this.GlobalObjectId);
				return;
			}
			attendee.ResponseType = this.ResponseType;
			ExDateTime exDateTime = base.GetValueOrDefault<ExDateTime>(InternalSchema.AttendeeCriticalChangeTime, ExDateTime.MinValue);
			if (exDateTime == ExDateTime.MinValue)
			{
				exDateTime = base.SentTime;
			}
			if (exDateTime != ExDateTime.MinValue)
			{
				attendee.ReplyTime = exDateTime;
			}
			if (!base.IsSeriesMessage)
			{
				CalendarItemType calendarItemType = calendarItemBase.CalendarItemType;
				if (calendarItemType == CalendarItemType.Exception || calendarItemType == CalendarItemType.Occurrence)
				{
					attendee.RecipientFlags |= RecipientFlags.ExceptionalResponse;
				}
				this.ProcessCounterProposal(calendarItemBase, attendee);
			}
		}

		// Token: 0x06002D71 RID: 11633 RVA: 0x000BB238 File Offset: 0x000B9438
		protected override bool CheckPreConditions(CalendarItemBase originalCalendarItem, bool shouldThrow, bool canUpdatePrincipalCalendar)
		{
			if (!base.CheckPreConditions(originalCalendarItem, shouldThrow, canUpdatePrincipalCalendar))
			{
				return false;
			}
			if (originalCalendarItem == null)
			{
				ExTraceGlobals.MeetingMessageTracer.Information<GlobalObjectId>((long)this.GetHashCode(), "Storage.MeetingResponse.CheckPreConditions: GOID={0}; there is no calendar item; returning false.", this.GlobalObjectId);
				return false;
			}
			if (originalCalendarItem.IsOrganizer())
			{
				return true;
			}
			if (shouldThrow)
			{
				throw new InvalidOperationException(ServerStrings.ExCannotUpdateResponses);
			}
			ExTraceGlobals.MeetingMessageTracer.Information<GlobalObjectId>((long)this.GetHashCode(), "Storage.MeetingResponse.CheckPreConditions: GOID={0}; not organizer; returning false.", this.GlobalObjectId);
			return false;
		}

		// Token: 0x06002D72 RID: 11634 RVA: 0x000BB2B0 File Offset: 0x000B94B0
		private static string ItemClassFromResponseType(string itemClassPrefix, ResponseType responseType)
		{
			bool flag = ObjectClass.IsMeetingResponseSeries(itemClassPrefix);
			string result;
			switch (responseType)
			{
			case ResponseType.Tentative:
				result = (flag ? "IPM.MeetingMessageSeries.Resp.Tent" : "IPM.Schedule.Meeting.Resp.Tent");
				break;
			case ResponseType.Accept:
				result = (flag ? "IPM.MeetingMessageSeries.Resp.Pos" : "IPM.Schedule.Meeting.Resp.Pos");
				break;
			case ResponseType.Decline:
				result = (flag ? "IPM.MeetingMessageSeries.Resp.Neg" : "IPM.Schedule.Meeting.Resp.Neg");
				break;
			default:
				throw new ArgumentException(ServerStrings.ExUnknownResponseType, "responseType");
			}
			return result;
		}

		// Token: 0x06002D73 RID: 11635 RVA: 0x000BB328 File Offset: 0x000B9528
		private void ProcessCounterProposal(CalendarItemBase calendarItem, Attendee attendee)
		{
			base.LocationIdentifierHelperInstance.SetLocationIdentifier(57973U, LastChangeAction.ProcessCounterProposal);
			bool valueOrDefault = base.GetValueOrDefault<bool>(InternalSchema.AppointmentCounterProposal);
			int num = calendarItem.GetValueOrDefault<int>(InternalSchema.AppointmentCounterProposalCount);
			bool valueOrDefault2 = attendee.GetValueOrDefault<bool>(InternalSchema.RecipientProposed);
			if (valueOrDefault)
			{
				attendee[InternalSchema.RecipientProposed] = true;
				attendee[InternalSchema.RecipientProposedStartTime] = this.ProposedStart;
				attendee[InternalSchema.RecipientProposedEndTime] = this.ProposedEnd;
				if (!valueOrDefault2)
				{
					ExTraceGlobals.MeetingMessageTracer.Information<GlobalObjectId, string>((long)this.GetHashCode(), "Storage.MeetingResponse.ProcessCounterProposal: GOID={0}; new counter proposal from {1}.", this.GlobalObjectId, attendee.Participant.DisplayName);
					num++;
				}
				else
				{
					ExTraceGlobals.MeetingMessageTracer.Information<GlobalObjectId, string>((long)this.GetHashCode(), "Storage.MeetingResponse.ProcessCounterProposal: GOID={0}; updated counter proposal from {1}.", this.GlobalObjectId, attendee.Participant.DisplayName);
				}
				calendarItem[InternalSchema.AppointmentCounterProposal] = true;
				calendarItem[InternalSchema.AppointmentCounterProposalCount] = num;
			}
			else if (valueOrDefault2)
			{
				ExTraceGlobals.MeetingMessageTracer.Information<GlobalObjectId, string>((long)this.GetHashCode(), "Storage.MeetingResponse.ProcessCounterProposal: GOID={0}; reseting counter proposal from {1}.", this.GlobalObjectId, attendee.Participant.DisplayName);
				attendee[InternalSchema.RecipientProposed] = false;
				attendee[InternalSchema.RecipientProposedStartTime] = CalendarItemBase.OutlookRtmNone;
				attendee[InternalSchema.RecipientProposedEndTime] = CalendarItemBase.OutlookRtmNone;
				if (num > 0)
				{
					ExTraceGlobals.MeetingMessageTracer.Information<GlobalObjectId>((long)this.GetHashCode(), "Storage.MeetingResponse.ProcessCounterProposal: GOID={0}; no more counter proposals.", this.GlobalObjectId);
					num--;
				}
				calendarItem[InternalSchema.AppointmentCounterProposalCount] = num;
				if (num == 0)
				{
					calendarItem[InternalSchema.AppointmentCounterProposal] = false;
				}
			}
			this.SetCalendarProcessingSteps(CalendarProcessingSteps.CounterProposalSet);
		}

		// Token: 0x06002D74 RID: 11636 RVA: 0x000BB4E8 File Offset: 0x000B96E8
		private bool IsAttendeeResponseOutOfDate(Attendee attendee)
		{
			ExDateTime valueOrDefault = base.GetValueOrDefault<ExDateTime>(InternalSchema.AttendeeCriticalChangeTime, base.SentTime);
			bool flag = ExDateTime.Compare(valueOrDefault, attendee.ReplyTime, Util.DateTimeComparisonRange) < 0;
			if (flag)
			{
				ExTraceGlobals.MeetingMessageTracer.Information<GlobalObjectId>((long)this.GetHashCode(), "Storage.MeetingResponse.IsAttendeeResponseOutOfDate: GOID={0}; response is out of date.", this.GlobalObjectId);
			}
			return flag;
		}

		// Token: 0x06002D75 RID: 11637 RVA: 0x000BB53C File Offset: 0x000B973C
		private Attendee FindOrAddAttendee(CalendarItemBase calendarItem, out bool newAttendee)
		{
			IAttendeeCollection attendeeCollection = calendarItem.AttendeeCollection;
			Attendee attendee = base.FindAttendee(calendarItem);
			if (attendee == null && attendeeCollection != null && base.From != null)
			{
				ExTraceGlobals.MeetingMessageTracer.Information((long)this.GetHashCode(), "User not found, adding to the list.");
				attendee = attendeeCollection.Add(base.From, AttendeeType.Optional, null, null, false);
				newAttendee = true;
			}
			else
			{
				newAttendee = false;
			}
			return attendee;
		}

		// Token: 0x040018FF RID: 6399
		internal const string YouCannotUpdateResponses = "Cannot update calendar item with responses when you are not the organizer.";
	}
}
