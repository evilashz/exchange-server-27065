using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020003A5 RID: 933
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class CalendarItemInstance : CalendarItemBase, ICalendarItemInstance, ICalendarItemBase, IItem, IStoreObject, IStorePropertyBag, IPropertyBag, IReadOnlyPropertyBag, IDisposable
	{
		// Token: 0x06002A38 RID: 10808 RVA: 0x000A7A84 File Offset: 0x000A5C84
		internal CalendarItemInstance(ICoreItem coreItem) : base(coreItem)
		{
		}

		// Token: 0x06002A39 RID: 10809 RVA: 0x000A7A8D File Offset: 0x000A5C8D
		public new static CalendarItemInstance Bind(StoreSession session, StoreId storeId)
		{
			return CalendarItemInstance.Bind(session, storeId, null);
		}

		// Token: 0x06002A3A RID: 10810 RVA: 0x000A7A97 File Offset: 0x000A5C97
		public new static CalendarItemInstance Bind(StoreSession session, StoreId storeId, params PropertyDefinition[] propsToReturn)
		{
			return CalendarItemInstance.Bind(session, storeId, (ICollection<PropertyDefinition>)propsToReturn);
		}

		// Token: 0x06002A3B RID: 10811 RVA: 0x000A7AA6 File Offset: 0x000A5CA6
		public new static CalendarItemInstance Bind(StoreSession session, StoreId storeId, ICollection<PropertyDefinition> propsToReturn)
		{
			return ItemBuilder.ItemBind<CalendarItemInstance>(session, storeId, CalendarItemInstanceSchema.Instance, propsToReturn);
		}

		// Token: 0x06002A3C RID: 10812 RVA: 0x000A7AB5 File Offset: 0x000A5CB5
		public override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<CalendarItemInstance>(this);
		}

		// Token: 0x17000DE1 RID: 3553
		// (get) Token: 0x06002A3D RID: 10813 RVA: 0x000A7ABD File Offset: 0x000A5CBD
		public override Schema Schema
		{
			get
			{
				this.CheckDisposed("Schema::get");
				return CalendarItemInstanceSchema.Instance;
			}
		}

		// Token: 0x17000DE2 RID: 3554
		// (get) Token: 0x06002A3E RID: 10814 RVA: 0x000A7AD0 File Offset: 0x000A5CD0
		// (set) Token: 0x06002A3F RID: 10815 RVA: 0x000A7B10 File Offset: 0x000A5D10
		public override ExDateTime StartTime
		{
			get
			{
				this.CheckDisposed("StartTime::get");
				object obj = base.TryGetProperty(InternalSchema.StartTime);
				if (obj is ExDateTime)
				{
					return (ExDateTime)obj;
				}
				throw new CorruptDataException(ServerStrings.ExStartTimeNotSet);
			}
			set
			{
				this.CheckDisposed("StartTime::set");
				if (!base.IsInMemoryObject)
				{
					value.CheckExpectedTimeZone(base.Session.ExTimeZone, ExTimeZoneHelperForMigrationOnly.ValidationLevel.VeryHigh);
				}
				base.LocationIdentifierHelperInstance.SetLocationIdentifier(48501U);
				this[InternalSchema.StartTime] = value;
			}
		}

		// Token: 0x17000DE3 RID: 3555
		// (get) Token: 0x06002A40 RID: 10816 RVA: 0x000A7B64 File Offset: 0x000A5D64
		public override ExDateTime StartWallClock
		{
			get
			{
				this.CheckDisposed("StartWallClock::get");
				return base.GetValueOrDefault<ExDateTime>(CalendarItemInstanceSchema.StartWallClock);
			}
		}

		// Token: 0x17000DE4 RID: 3556
		// (get) Token: 0x06002A41 RID: 10817 RVA: 0x000A7B7C File Offset: 0x000A5D7C
		public override ExDateTime EndWallClock
		{
			get
			{
				this.CheckDisposed("EndWallClock::get");
				return base.GetValueOrDefault<ExDateTime>(CalendarItemInstanceSchema.EndWallClock);
			}
		}

		// Token: 0x17000DE5 RID: 3557
		// (get) Token: 0x06002A42 RID: 10818 RVA: 0x000A7B94 File Offset: 0x000A5D94
		// (set) Token: 0x06002A43 RID: 10819 RVA: 0x000A7BD4 File Offset: 0x000A5DD4
		public override ExDateTime EndTime
		{
			get
			{
				this.CheckDisposed("EndTime::get");
				object obj = base.TryGetProperty(InternalSchema.EndTime);
				if (obj is ExDateTime)
				{
					return (ExDateTime)obj;
				}
				throw new CorruptDataException(ServerStrings.ExEndTimeNotSet);
			}
			set
			{
				this.CheckDisposed("EndTime::set");
				if (!base.IsInMemoryObject)
				{
					value.CheckExpectedTimeZone(base.Session.ExTimeZone, ExTimeZoneHelperForMigrationOnly.ValidationLevel.VeryHigh);
				}
				base.LocationIdentifierHelperInstance.SetLocationIdentifier(64885U);
				this[InternalSchema.EndTime] = value;
			}
		}

		// Token: 0x06002A44 RID: 10820 RVA: 0x000A7C28 File Offset: 0x000A5E28
		internal override void Initialize(bool newItem)
		{
			base.Initialize(newItem);
			this[InternalSchema.ItemClass] = "IPM.Appointment";
			ExDateTime nextHalfHour = this.GetNextHalfHour();
			this[InternalSchema.StartTime] = nextHalfHour;
			this[InternalSchema.EndTime] = nextHalfHour.AddMinutes(30.0);
			this[InternalSchema.AppointmentRecurring] = false;
			this[InternalSchema.IsRecurring] = false;
			this[InternalSchema.IsException] = false;
			this[InternalSchema.ConversationIndexTracking] = true;
			this[InternalSchema.SideEffects] = (SideEffects.OpenToDelete | SideEffects.CoerceToInbox | SideEffects.OpenToCopy | SideEffects.OpenToMove | SideEffects.OpenForCtxMenu);
		}

		// Token: 0x06002A45 RID: 10821 RVA: 0x000A7CDD File Offset: 0x000A5EDD
		public override void MoveToFolder(MailboxSession destinationSession, StoreObjectId destinationFolderId)
		{
			this.CheckDisposed("MoveToFolder");
			this.CopyMoveToFolder(new CalendarItemInstance.CopyMoveOperation(base.Session.Move), destinationSession, destinationFolderId, false);
		}

		// Token: 0x06002A46 RID: 10822 RVA: 0x000A7D04 File Offset: 0x000A5F04
		public override void CopyToFolder(MailboxSession destinationSession, StoreObjectId destinationFolderId)
		{
			this.CheckDisposed("CopyToFolder");
			this.CopyMoveToFolder(new CalendarItemInstance.CopyMoveOperation(base.Session.Copy), destinationSession, destinationFolderId, true);
		}

		// Token: 0x06002A47 RID: 10823 RVA: 0x000A7D2B File Offset: 0x000A5F2B
		protected override MeetingRequest CreateNewMeetingRequest(MailboxSession mailboxSession)
		{
			return MeetingRequest.CreateMeetingRequest(mailboxSession);
		}

		// Token: 0x06002A48 RID: 10824 RVA: 0x000A7D33 File Offset: 0x000A5F33
		protected override MeetingCancellation CreateNewMeetingCancelation(MailboxSession mailboxSession)
		{
			return MeetingCancellation.CreateMeetingCancellation(mailboxSession);
		}

		// Token: 0x06002A49 RID: 10825 RVA: 0x000A7D3B File Offset: 0x000A5F3B
		protected override MeetingResponse CreateNewMeetingResponse(MailboxSession mailboxSession, ResponseType responseType)
		{
			return MeetingResponse.CreateMeetingResponse(mailboxSession, responseType);
		}

		// Token: 0x06002A4A RID: 10826 RVA: 0x000A7D44 File Offset: 0x000A5F44
		protected override void SendMeetingCancellations(MailboxSession mailboxSession, bool isToAllAttendees, IList<Attendee> removedAttendeeList, bool copyToSentItems, bool ignoreSendAsRight, CancellationRumInfo rumInfo)
		{
			if (removedAttendeeList.Count == 0)
			{
				return;
			}
			ExTraceGlobals.MeetingMessageTracer.Information<GlobalObjectId, int>((long)this.GetHashCode(), "Storage.CalendarItemBase.SendMeetingCancellations: GOID={0}; users={1}", base.GlobalObjectId, removedAttendeeList.Count);
			using (MeetingCancellation meetingCancellation = (rumInfo != null) ? this.CreateCancellationRum(mailboxSession, rumInfo) : base.CreateMeetingCancellation(mailboxSession, isToAllAttendees, null, null))
			{
				meetingCancellation.CopySendableParticipantsToMessage(removedAttendeeList);
				base.LocationIdentifierHelperInstance.SetLocationIdentifier(33141U, LastChangeAction.SendMeetingCancellations);
				base.SendMessage(mailboxSession, meetingCancellation, copyToSentItems, ignoreSendAsRight);
			}
		}

		// Token: 0x06002A4B RID: 10827 RVA: 0x000A7DE0 File Offset: 0x000A5FE0
		protected override void SetSequencingPropertiesForForward(MeetingRequest meetingRequest)
		{
			int appointmentLastSequenceNumber = this.AppointmentLastSequenceNumber;
			int valueOrDefault = meetingRequest.GetValueOrDefault<int>(InternalSchema.AppointmentSequenceNumber, appointmentLastSequenceNumber);
			if (valueOrDefault < appointmentLastSequenceNumber)
			{
				meetingRequest[InternalSchema.AppointmentSequenceNumber] = appointmentLastSequenceNumber;
			}
		}

		// Token: 0x06002A4C RID: 10828 RVA: 0x000A7E18 File Offset: 0x000A6018
		protected override void InitializeMeetingRequest(Action<MeetingRequest> setBodyAndAdjustFlags, MeetingRequest meetingRequest)
		{
			base.InitializeMeetingRequest(setBodyAndAdjustFlags, meetingRequest);
			Microsoft.Exchange.Data.Storage.Item.CopyCustomPublicStrings(this, meetingRequest);
			this.ClearCounterProposal();
			if (base.CalendarItemType == CalendarItemType.RecurringMaster)
			{
				CalendarItemBase.CopyPropertiesTo(this, meetingRequest, new PropertyDefinition[]
				{
					InternalSchema.AppointmentRecurrenceBlob
				});
			}
		}

		// Token: 0x06002A4D RID: 10829 RVA: 0x000A7E5C File Offset: 0x000A605C
		protected override void InternalUpdateSequencingProperties(bool isToAllAttendees, MeetingMessage message, int minSequenceNumber, int? seriesSequenceNumber = null)
		{
			ExDateTime now = ExDateTime.GetNow(base.PropertyBag.ExTimeZone);
			base.OwnerCriticalChangeTime = now;
			int appointmentSequenceNumber = base.AppointmentSequenceNumber;
			int appointmentLastSequenceNumber = this.AppointmentLastSequenceNumber;
			int valueOrDefault = base.GetValueOrDefault<int>(InternalSchema.CdoSequenceNumber);
			int num = Math.Max(minSequenceNumber, Math.Max(appointmentSequenceNumber, Math.Max(appointmentLastSequenceNumber, valueOrDefault)));
			if (base.MeetingRequestWasSent)
			{
				base.LocationIdentifierHelperInstance.SetLocationIdentifier(40959U);
				num++;
			}
			if (isToAllAttendees)
			{
				base.LocationIdentifierHelperInstance.SetLocationIdentifier(51573U);
				this[InternalSchema.AppointmentSequenceTime] = now;
				base.AppointmentSequenceNumber = num;
			}
			this.AppointmentLastSequenceNumber = num;
			if (message != null)
			{
				message[InternalSchema.OwnerCriticalChangeTime] = now;
				message[InternalSchema.AppointmentSequenceNumber] = num;
				ExDateTime? valueAsNullable = base.GetValueAsNullable<ExDateTime>(InternalSchema.AppointmentSequenceTime);
				message[InternalSchema.AppointmentSequenceTime] = ((valueAsNullable != null) ? valueAsNullable.Value : now);
			}
			base.LocationIdentifierHelperInstance.SetLocationIdentifier(37237U, LastChangeAction.UpdateSequenceNumber);
		}

		// Token: 0x06002A4E RID: 10830 RVA: 0x000A7F6C File Offset: 0x000A616C
		protected override void InitializeMeetingResponse(MeetingResponse meetingResponse, ResponseType responseType, bool isCalendarDelegateAccess, ExDateTime? proposedStart, ExDateTime? proposedEnd)
		{
			base.InitializeMeetingResponse(meetingResponse, responseType, isCalendarDelegateAccess, proposedStart, proposedEnd);
			if (proposedStart != null && proposedEnd != null)
			{
				this.ValidateTimeProposal(proposedStart.Value, proposedEnd.Value);
				meetingResponse.SetTimeProposal(proposedStart.Value, proposedEnd.Value);
			}
		}

		// Token: 0x06002A4F RID: 10831 RVA: 0x000A7FC0 File Offset: 0x000A61C0
		protected override ClientIntentFlags CalculateClientIntentBasedOnModifiedProperties()
		{
			ClientIntentFlags clientIntentFlags = base.CalculateClientIntentBasedOnModifiedProperties();
			if (base.PropertyBag.IsPropertyDirty(InternalSchema.MapiIsAllDayEvent))
			{
				clientIntentFlags |= ClientIntentFlags.ModifiedTime;
			}
			else
			{
				if (base.PropertyBag.IsPropertyDirty(CalendarItemInstanceSchema.StartTime))
				{
					clientIntentFlags |= ClientIntentFlags.ModifiedStartTime;
				}
				if (base.PropertyBag.IsPropertyDirty(CalendarItemInstanceSchema.EndTime))
				{
					clientIntentFlags |= ClientIntentFlags.ModifiedEndTime;
				}
			}
			return clientIntentFlags;
		}

		// Token: 0x06002A50 RID: 10832 RVA: 0x000A8025 File Offset: 0x000A6225
		protected override void OnBeforeSave()
		{
			base.OnBeforeSave();
			if (!base.IsInMemoryObject)
			{
				this.OnBeforeSaveUpdateStartTimeEndTime();
			}
		}

		// Token: 0x06002A51 RID: 10833 RVA: 0x000A8060 File Offset: 0x000A6260
		private MeetingCancellation CreateCancellationRum(MailboxSession mailboxSession, CancellationRumInfo rumInfo)
		{
			Action<MeetingCancellation> setBodyAndAdjustFlags = delegate(MeetingCancellation cancellation)
			{
				this.AdjustRumMessage(mailboxSession, cancellation, rumInfo, false);
			};
			return base.CreateMeetingCancellation(mailboxSession, false, rumInfo.AttendeeRequiredSequenceNumber, setBodyAndAdjustFlags, false, null, null);
		}

		// Token: 0x06002A52 RID: 10834 RVA: 0x000A80BC File Offset: 0x000A62BC
		private void ClearCounterProposal()
		{
			int num = base.GetValueOrDefault<int>(InternalSchema.AppointmentCounterProposalCount);
			ExTraceGlobals.MeetingMessageTracer.Information<GlobalObjectId, int>((long)this.GetHashCode(), "Storage.CalendarItemBase.ClearCounterProposal: GOID={0}; count={1}", base.GlobalObjectId, num);
			base.LocationIdentifierHelperInstance.SetLocationIdentifier(41333U);
			this[InternalSchema.AppointmentCounterProposal] = false;
			base.LocationIdentifierHelperInstance.SetLocationIdentifier(57717U);
			this[InternalSchema.AppointmentCounterProposalCount] = 0;
			if (num > 0)
			{
				foreach (Attendee attendee in base.AttendeeCollection)
				{
					bool valueOrDefault = attendee.GetValueOrDefault<bool>(InternalSchema.RecipientProposed);
					if (valueOrDefault)
					{
						attendee[InternalSchema.RecipientProposed] = false;
						attendee[InternalSchema.RecipientProposedStartTime] = CalendarItemBase.OutlookRtmNone;
						attendee[InternalSchema.RecipientProposedEndTime] = CalendarItemBase.OutlookRtmNone;
						if (--num == 0)
						{
							break;
						}
					}
				}
			}
		}

		// Token: 0x06002A53 RID: 10835 RVA: 0x000A81C4 File Offset: 0x000A63C4
		private ExDateTime GetNextHalfHour()
		{
			ExDateTime d = ExDateTime.GetNow(base.PropertyBag.ExTimeZone);
			if (d.Minute < 30)
			{
				d = d.AddMinutes((double)(30 - d.Minute));
			}
			else
			{
				d = d.AddMinutes((double)(60 - d.Minute));
			}
			return d - TimeSpan.FromMilliseconds((double)(d.Second * 1000 + d.Millisecond));
		}

		// Token: 0x06002A54 RID: 10836 RVA: 0x000A8238 File Offset: 0x000A6438
		private void CopyMoveToFolder(CalendarItemInstance.CopyMoveOperation operation, MailboxSession destinationSession, StoreObjectId destinationFolderId, bool isCopy)
		{
			this.ThrowIfCopyMovePrereqsFail(destinationSession, destinationFolderId, isCopy);
			operation(destinationSession, destinationFolderId, new StoreId[]
			{
				base.Id
			});
		}

		// Token: 0x06002A55 RID: 10837 RVA: 0x000A826C File Offset: 0x000A646C
		private void ThrowIfCopyMovePrereqsFail(MailboxSession destinationSession, StoreObjectId destinationFolderId, bool isCopy)
		{
			Util.ThrowOnNullArgument(destinationFolderId, "destinationFolderId");
			if (destinationFolderId.ObjectType != StoreObjectType.CalendarFolder)
			{
				throw new ArgumentException("Destination folder must be a calendar folder", "destinationFolderId");
			}
			if (!(base.Session is MailboxSession))
			{
				throw new InvalidOperationException("Only mailbox sessions are supported");
			}
			if (base.ParentId.Equals(destinationFolderId))
			{
				throw new ArgumentException("The destination folder must be different from the source folder.", "destinationFolderId");
			}
			if (!this.IsInThePast)
			{
				throw new FutureMeetingException("Only meetings in the past can be copied or moved");
			}
			if (isCopy)
			{
				StoreObjectId defaultFolderId = ((MailboxSession)base.Session).GetDefaultFolderId(DefaultFolderType.Calendar);
				if (base.ParentId.Equals(defaultFolderId) || destinationFolderId.Equals(defaultFolderId))
				{
					throw new PrimaryCalendarFolderException("Copy is not allowed to/from the primary calendar");
				}
			}
			using (CalendarFolder calendarFolder = CalendarFolder.Bind(destinationSession, destinationFolderId))
			{
				IList list = CalendarCorrelationMatch.FindMatches(calendarFolder, base.GlobalObjectId, null);
				if (list.Count > 0)
				{
					throw new CalendarItemExistsException("There is already a calendar item with this GOID in the destination folder");
				}
			}
		}

		// Token: 0x06002A56 RID: 10838 RVA: 0x000A8364 File Offset: 0x000A6564
		private void OnBeforeSaveUpdateStartTimeEndTime()
		{
			if (base.IsAllDayEventCache == true)
			{
				ExDateTime? valueAsNullable = base.GetValueAsNullable<ExDateTime>(CalendarItemInstanceSchema.StartTime);
				ExDateTime? valueAsNullable2 = base.GetValueAsNullable<ExDateTime>(CalendarItemInstanceSchema.EndTime);
				if (valueAsNullable != null && valueAsNullable2 != null)
				{
					if (!object.Equals(valueAsNullable.Value, base.TryGetProperty(InternalSchema.MapiStartTime)))
					{
						base.LocationIdentifierHelperInstance.SetLocationIdentifier(35317U);
						this[CalendarItemInstanceSchema.StartTime] = valueAsNullable.Value;
					}
					if (!object.Equals(valueAsNullable2.Value, base.TryGetProperty(InternalSchema.MapiEndTime)))
					{
						base.LocationIdentifierHelperInstance.SetLocationIdentifier(51701U);
						this[CalendarItemInstanceSchema.EndTime] = valueAsNullable2.Value;
					}
				}
			}
		}

		// Token: 0x06002A57 RID: 10839 RVA: 0x000A844C File Offset: 0x000A664C
		private void ValidateTimeProposal(ExDateTime proposedStart, ExDateTime proposedEnd)
		{
			if (base.CalendarItemType == CalendarItemType.RecurringMaster)
			{
				throw new InvalidTimeProposalException(ServerStrings.ErrorTimeProposalInvalidOnRecurringMaster);
			}
			if (!base.AllowNewTimeProposal)
			{
				throw new InvalidTimeProposalException(ServerStrings.ErrorTimeProposalInvalidWhenNotAllowedByOrganizer);
			}
			if (proposedStart.CompareTo(proposedEnd) > 0)
			{
				throw new InvalidTimeProposalException(ServerStrings.ErrorTimeProposalEndTimeBeforeStartTime);
			}
			TimeSpan timeSpan = proposedEnd - proposedStart;
			if (timeSpan.TotalDays > 1825.0)
			{
				throw new InvalidTimeProposalException(ServerStrings.ErrorTimeProposalInvalidDuration((int)timeSpan.TotalDays));
			}
		}

		// Token: 0x020003A6 RID: 934
		// (Invoke) Token: 0x06002A59 RID: 10841
		private delegate AggregateOperationResult CopyMoveOperation(StoreSession destinationSession, StoreId destinationFolderId, params StoreId[] ids);
	}
}
