using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020003E1 RID: 993
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class MeetingRequest : MeetingMessageInstance, IMeetingRequest, IMeetingMessage, IMessageItem, IToDoItem, IItem, IStoreObject, IStorePropertyBag, IPropertyBag, IReadOnlyPropertyBag, IDisposable
	{
		// Token: 0x06002D12 RID: 11538 RVA: 0x000B947E File Offset: 0x000B767E
		internal MeetingRequest(ICoreItem coreItem) : base(coreItem)
		{
		}

		// Token: 0x06002D13 RID: 11539 RVA: 0x000B948F File Offset: 0x000B768F
		public new static MeetingRequest Bind(StoreSession session, StoreId storeId)
		{
			return MeetingRequest.Bind(session, storeId, null);
		}

		// Token: 0x06002D14 RID: 11540 RVA: 0x000B9499 File Offset: 0x000B7699
		public new static MeetingRequest Bind(StoreSession session, StoreId storeId, params PropertyDefinition[] propsToReturn)
		{
			return MeetingRequest.Bind(session, storeId, (ICollection<PropertyDefinition>)propsToReturn);
		}

		// Token: 0x06002D15 RID: 11541 RVA: 0x000B94A8 File Offset: 0x000B76A8
		public new static MeetingRequest Bind(StoreSession session, StoreId storeId, ICollection<PropertyDefinition> propsToReturn)
		{
			return ItemBuilder.ItemBind<MeetingRequest>(session, storeId, MeetingMessageInstanceSchema.Instance, propsToReturn);
		}

		// Token: 0x06002D16 RID: 11542 RVA: 0x000B94B8 File Offset: 0x000B76B8
		public static MeetingRequest CreateMeetingRequest(MailboxSession mailboxSession)
		{
			MeetingRequest meetingRequest = ItemBuilder.CreateNewItem<MeetingRequest>(mailboxSession, mailboxSession.GetDefaultFolderId(DefaultFolderType.Drafts), ItemCreateInfo.MeetingRequestInfo);
			meetingRequest.Initialize("IPM.Schedule.Meeting.Request");
			return meetingRequest;
		}

		// Token: 0x06002D17 RID: 11543 RVA: 0x000B94E4 File Offset: 0x000B76E4
		public static MeetingRequest CreateMeetingRequestSeries(MailboxSession mailboxSession)
		{
			MeetingRequest meetingRequest = ItemBuilder.CreateNewItem<MeetingRequest>(mailboxSession, mailboxSession.GetDefaultFolderId(DefaultFolderType.Drafts), ItemCreateInfo.MeetingRequestSeriesInfo);
			meetingRequest.Initialize("IPM.MeetingMessageSeries.Request");
			return meetingRequest;
		}

		// Token: 0x06002D18 RID: 11544 RVA: 0x000B9510 File Offset: 0x000B7710
		public override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<MeetingRequest>(this);
		}

		// Token: 0x17000E84 RID: 3716
		// (get) Token: 0x06002D19 RID: 11545 RVA: 0x000B9518 File Offset: 0x000B7718
		// (set) Token: 0x06002D1A RID: 11546 RVA: 0x000B9534 File Offset: 0x000B7734
		public MeetingMessageType MeetingRequestType
		{
			get
			{
				this.CheckDisposed("MeetingRequestType::get");
				return base.GetValueOrDefault<MeetingMessageType>(InternalSchema.MeetingRequestType, MeetingMessageType.NewMeetingRequest);
			}
			set
			{
				this.CheckDisposed("MeetingRequestType::set");
				EnumValidator.ThrowIfInvalid<MeetingMessageType>(value, "value");
				base.LocationIdentifierHelperInstance.SetLocationIdentifier(38389U);
				MeetingMessageType? valueAsNullable = base.GetValueAsNullable<MeetingMessageType>(InternalSchema.MeetingRequestType);
				if (valueAsNullable == null || value != valueAsNullable)
				{
					this[InternalSchema.MeetingRequestType] = value;
					this.SetCalendarProcessingSteps(CalendarProcessingSteps.ChangedMtgType);
				}
			}
		}

		// Token: 0x17000E85 RID: 3717
		// (get) Token: 0x06002D1B RID: 11547 RVA: 0x000B95B4 File Offset: 0x000B77B4
		// (set) Token: 0x06002D1C RID: 11548 RVA: 0x000B9628 File Offset: 0x000B7828
		public string OccurrencesExceptionalViewProperties
		{
			get
			{
				this.CheckDisposed("OccurrencesExceptionalViewProperties::get");
				if (!this.occurrencesExceptionalViewPropertiesLoaded)
				{
					this.LoadOccurrencesExceptionalViewProperties();
				}
				string result;
				using (Stream stream = base.OpenPropertyStream(MeetingRequestSchema.OccurrencesExceptionalViewProperties, PropertyOpenMode.ReadOnly))
				{
					using (StreamReader streamReader = new StreamReader(stream))
					{
						result = streamReader.ReadToEnd();
					}
				}
				return result;
			}
			set
			{
				this.CheckDisposed("OccurrencesExceptionalViewProperties::set");
				base.LocationIdentifierHelperInstance.SetLocationIdentifier(57340U);
				if (!this.occurrencesExceptionalViewPropertiesLoaded)
				{
					this.LoadOccurrencesExceptionalViewProperties();
				}
				using (Stream stream = base.OpenPropertyStream(MeetingRequestSchema.OccurrencesExceptionalViewProperties, PropertyOpenMode.Create))
				{
					using (StreamWriter streamWriter = new StreamWriter(stream))
					{
						streamWriter.Write(value);
					}
				}
			}
		}

		// Token: 0x17000E86 RID: 3718
		// (get) Token: 0x06002D1D RID: 11549 RVA: 0x000B96B0 File Offset: 0x000B78B0
		// (set) Token: 0x06002D1E RID: 11550 RVA: 0x000B96CD File Offset: 0x000B78CD
		public string Location
		{
			get
			{
				this.CheckDisposed("Location::get");
				return base.GetValueOrDefault<string>(InternalSchema.Location, string.Empty);
			}
			set
			{
				this.CheckDisposed("Location::set");
				base.LocationIdentifierHelperInstance.SetLocationIdentifier(42155U);
				this[InternalSchema.Location] = value;
			}
		}

		// Token: 0x17000E87 RID: 3719
		// (get) Token: 0x06002D1F RID: 11551 RVA: 0x000B96F6 File Offset: 0x000B78F6
		// (set) Token: 0x06002D20 RID: 11552 RVA: 0x000B9713 File Offset: 0x000B7913
		public string OldLocation
		{
			get
			{
				this.CheckDisposed("OldLocation::get");
				return base.GetValueOrDefault<string>(InternalSchema.OldLocation, string.Empty);
			}
			set
			{
				this.CheckDisposed("OldLocation::set");
				base.LocationIdentifierHelperInstance.SetLocationIdentifier(54773U);
				this[InternalSchema.OldLocation] = value;
			}
		}

		// Token: 0x17000E88 RID: 3720
		// (get) Token: 0x06002D21 RID: 11553 RVA: 0x000B973C File Offset: 0x000B793C
		public override Schema Schema
		{
			get
			{
				this.CheckDisposed("Schema::get");
				return MeetingRequestSchema.Instance;
			}
		}

		// Token: 0x17000E89 RID: 3721
		// (get) Token: 0x06002D22 RID: 11554 RVA: 0x000B974E File Offset: 0x000B794E
		public ChangeHighlightProperties ChangeHighlight
		{
			get
			{
				this.CheckDisposed("ChangeHighlight::get");
				return base.GetValueOrDefault<ChangeHighlightProperties>(InternalSchema.ChangeHighlight);
			}
		}

		// Token: 0x17000E8A RID: 3722
		// (get) Token: 0x06002D23 RID: 11555 RVA: 0x000B9766 File Offset: 0x000B7966
		public string LocationDisplayName
		{
			get
			{
				this.CheckDisposed("LocationDisplayName::get");
				return base.GetValueOrDefault<string>(InternalSchema.LocationDisplayName, null);
			}
		}

		// Token: 0x17000E8B RID: 3723
		// (get) Token: 0x06002D24 RID: 11556 RVA: 0x000B977F File Offset: 0x000B797F
		public string LocationAnnotation
		{
			get
			{
				this.CheckDisposed("LocationAnnotation::get");
				return base.GetValueOrDefault<string>(InternalSchema.LocationAnnotation, null);
			}
		}

		// Token: 0x17000E8C RID: 3724
		// (get) Token: 0x06002D25 RID: 11557 RVA: 0x000B9798 File Offset: 0x000B7998
		public LocationSource LocationSource
		{
			get
			{
				this.CheckDisposed("LocationSource::get");
				return base.GetValueOrDefault<LocationSource>(InternalSchema.LocationSource, LocationSource.None);
			}
		}

		// Token: 0x17000E8D RID: 3725
		// (get) Token: 0x06002D26 RID: 11558 RVA: 0x000B97B1 File Offset: 0x000B79B1
		public string LocationUri
		{
			get
			{
				this.CheckDisposed("LocationUri::get");
				return base.GetValueOrDefault<string>(InternalSchema.LocationUri, null);
			}
		}

		// Token: 0x17000E8E RID: 3726
		// (get) Token: 0x06002D27 RID: 11559 RVA: 0x000B97CC File Offset: 0x000B79CC
		public double? Latitude
		{
			get
			{
				this.CheckDisposed("Latitude::get");
				return base.GetValueOrDefault<double?>(InternalSchema.Latitude, null);
			}
		}

		// Token: 0x17000E8F RID: 3727
		// (get) Token: 0x06002D28 RID: 11560 RVA: 0x000B97F8 File Offset: 0x000B79F8
		public double? Longitude
		{
			get
			{
				this.CheckDisposed("Longitude::get");
				return base.GetValueOrDefault<double?>(InternalSchema.Longitude, null);
			}
		}

		// Token: 0x17000E90 RID: 3728
		// (get) Token: 0x06002D29 RID: 11561 RVA: 0x000B9824 File Offset: 0x000B7A24
		public double? Accuracy
		{
			get
			{
				this.CheckDisposed("Accuracy::get");
				return base.GetValueOrDefault<double?>(InternalSchema.Accuracy, null);
			}
		}

		// Token: 0x17000E91 RID: 3729
		// (get) Token: 0x06002D2A RID: 11562 RVA: 0x000B9850 File Offset: 0x000B7A50
		public double? Altitude
		{
			get
			{
				this.CheckDisposed("Altitude::get");
				return base.GetValueOrDefault<double?>(InternalSchema.Altitude, null);
			}
		}

		// Token: 0x17000E92 RID: 3730
		// (get) Token: 0x06002D2B RID: 11563 RVA: 0x000B987C File Offset: 0x000B7A7C
		public double? AltitudeAccuracy
		{
			get
			{
				this.CheckDisposed("AltitudeAccuracy::get");
				return base.GetValueOrDefault<double?>(InternalSchema.AltitudeAccuracy, null);
			}
		}

		// Token: 0x17000E93 RID: 3731
		// (get) Token: 0x06002D2C RID: 11564 RVA: 0x000B98A8 File Offset: 0x000B7AA8
		public string LocationStreet
		{
			get
			{
				this.CheckDisposed("LocationStreet::get");
				return base.GetValueOrDefault<string>(MeetingMessageSchema.LocationStreet, null);
			}
		}

		// Token: 0x17000E94 RID: 3732
		// (get) Token: 0x06002D2D RID: 11565 RVA: 0x000B98C1 File Offset: 0x000B7AC1
		public string LocationCity
		{
			get
			{
				this.CheckDisposed("LocationCity::get");
				return base.GetValueOrDefault<string>(MeetingMessageSchema.LocationCity, null);
			}
		}

		// Token: 0x17000E95 RID: 3733
		// (get) Token: 0x06002D2E RID: 11566 RVA: 0x000B98DA File Offset: 0x000B7ADA
		public string LocationState
		{
			get
			{
				this.CheckDisposed("LocationState::get");
				return base.GetValueOrDefault<string>(MeetingMessageSchema.LocationState, null);
			}
		}

		// Token: 0x17000E96 RID: 3734
		// (get) Token: 0x06002D2F RID: 11567 RVA: 0x000B98F3 File Offset: 0x000B7AF3
		public string LocationCountry
		{
			get
			{
				this.CheckDisposed("LocationCountry::get");
				return base.GetValueOrDefault<string>(MeetingMessageSchema.LocationCountry, null);
			}
		}

		// Token: 0x17000E97 RID: 3735
		// (get) Token: 0x06002D30 RID: 11568 RVA: 0x000B990C File Offset: 0x000B7B0C
		public string LocationPostalCode
		{
			get
			{
				this.CheckDisposed("LocationPostalCode::get");
				return base.GetValueOrDefault<string>(MeetingMessageSchema.LocationPostalCode, null);
			}
		}

		// Token: 0x06002D31 RID: 11569 RVA: 0x000B9928 File Offset: 0x000B7B28
		public bool IsForwardedMeeting()
		{
			this.CheckDisposed("IsForwardedMeeting");
			AppointmentAuxiliaryFlags valueOrDefault = base.PropertyBag.GetValueOrDefault<AppointmentAuxiliaryFlags>(MeetingMessageSchema.AppointmentAuxiliaryFlags);
			return (valueOrDefault & AppointmentAuxiliaryFlags.ForwardedAppointment) != (AppointmentAuxiliaryFlags)0;
		}

		// Token: 0x06002D32 RID: 11570 RVA: 0x000B995C File Offset: 0x000B7B5C
		public ResponseType? GetCalendarItemResponseType()
		{
			this.CheckDisposed("CalendarItemResponseType");
			ResponseType? result = null;
			CalendarItemBase correlatedItemInternal = this.GetCorrelatedItemInternal(true);
			if (correlatedItemInternal != null)
			{
				result = new ResponseType?(correlatedItemInternal.ResponseType);
			}
			return result;
		}

		// Token: 0x17000E98 RID: 3736
		// (get) Token: 0x06002D33 RID: 11571 RVA: 0x000B9995 File Offset: 0x000B7B95
		public bool AllowNewTimeProposal
		{
			get
			{
				this.CheckDisposed("AllowNewTimeProposal::get");
				return !base.GetValueOrDefault<bool>(InternalSchema.DisallowNewTimeProposal);
			}
		}

		// Token: 0x06002D34 RID: 11572 RVA: 0x000B99B0 File Offset: 0x000B7BB0
		internal static List<BlobRecipient> MergeRecipientLists(RecipientCollection recipients, List<BlobRecipient> blobRecipientList)
		{
			List<BlobRecipient> list = new List<BlobRecipient>();
			foreach (Recipient recipient in recipients)
			{
				list.Add(new BlobRecipient(recipient));
			}
			foreach (BlobRecipient item in blobRecipientList)
			{
				list.Add(item);
			}
			return list;
		}

		// Token: 0x06002D35 RID: 11573 RVA: 0x000B9A44 File Offset: 0x000B7C44
		internal static List<BlobRecipient> FilterBlobRecipientList(List<BlobRecipient> blobRecipientList)
		{
			List<BlobRecipient> list = new List<BlobRecipient>();
			foreach (BlobRecipient blobRecipient in blobRecipientList)
			{
				if (blobRecipient.GetValueAsNullable<RecipientType>(InternalSchema.RecipientType) != RecipientType.Bcc)
				{
					list.Add(blobRecipient);
				}
			}
			return list;
		}

		// Token: 0x06002D36 RID: 11574 RVA: 0x000B9AC4 File Offset: 0x000B7CC4
		public override MessageItem CreateForward(MailboxSession session, StoreId parentFolderId, ReplyForwardConfiguration configuration)
		{
			this.CheckDisposed("CreateForward");
			ExTraceGlobals.MeetingMessageTracer.Information<GlobalObjectId>((long)this.GetHashCode(), "Storage.MeetingMessage.CreateForward: GOID={0}", this.GlobalObjectId);
			Util.ThrowOnNullArgument(session, "session");
			Util.ThrowOnNullArgument(parentFolderId, "parentFolderId");
			Util.ThrowOnNullArgument(configuration, "configuration");
			MeetingRequest meetingRequest = null;
			bool flag = false;
			MessageItem result;
			try
			{
				meetingRequest = (base.IsSeriesMessage ? MeetingRequest.CreateMeetingRequestSeries(session) : MeetingRequest.CreateMeetingRequest(session));
				ForwardCreation forwardCreation = new ForwardCreation(this, meetingRequest, configuration);
				forwardCreation.PopulateProperties();
				meetingRequest.AdjustAppointmentStateFlagsForForward();
				List<BlobRecipient> mergedRecipientList = this.GetMergedRecipientList();
				meetingRequest.SetUnsendableRecipients(mergedRecipientList);
				base.LocationIdentifierHelperInstance.SetLocationIdentifier(42485U, LastChangeAction.CreateForward);
				flag = true;
				result = meetingRequest;
			}
			finally
			{
				if (!flag && meetingRequest != null)
				{
					meetingRequest.Dispose();
					meetingRequest = null;
				}
			}
			return result;
		}

		// Token: 0x06002D37 RID: 11575 RVA: 0x000B9B90 File Offset: 0x000B7D90
		public MeetingForwardNotification CreateNotification()
		{
			this.CheckDisposed("CreateNotification");
			MeetingForwardNotification result = MeetingForwardNotification.Create(this);
			base.LocationIdentifierHelperInstance.SetLocationIdentifier(58869U, LastChangeAction.CreateForwardNotification);
			return result;
		}

		// Token: 0x06002D38 RID: 11576 RVA: 0x000B9BC1 File Offset: 0x000B7DC1
		public CalendarItemBase UpdateCalendarItem(int defaultReminderMinutes, bool canUpdatePrincipalCalendar)
		{
			this.CheckDisposed("UpdateCalendarItem");
			base.CheckPreConditionForDelegatedMeeting(canUpdatePrincipalCalendar);
			this.consumerDefaultMinutesBeforeStart = defaultReminderMinutes;
			return this.UpdateCalendarItem(canUpdatePrincipalCalendar);
		}

		// Token: 0x06002D39 RID: 11577 RVA: 0x000B9BE3 File Offset: 0x000B7DE3
		public bool TryUpdateCalendarItem(ref CalendarItemBase originalCalendarItem, int defaultReminderMinutes, bool canUpdatePrincipalCalendar)
		{
			this.CheckDisposed("TryUpdateCalendarItem");
			base.CheckPreConditionForDelegatedMeeting(canUpdatePrincipalCalendar);
			this.consumerDefaultMinutesBeforeStart = defaultReminderMinutes;
			return this.TryUpdateCalendarItem(ref originalCalendarItem, canUpdatePrincipalCalendar);
		}

		// Token: 0x06002D3A RID: 11578 RVA: 0x000B9C08 File Offset: 0x000B7E08
		public string GenerateOldWhen()
		{
			ExDateTime? valueAsNullable = base.GetValueAsNullable<ExDateTime>(InternalSchema.OldStartWhole);
			ExDateTime? valueAsNullable2 = base.GetValueAsNullable<ExDateTime>(InternalSchema.OldEndWhole);
			return CalendarItem.GenerateWhenForSingleInstance(valueAsNullable, valueAsNullable2).ToString(base.Session.InternalPreferedCulture);
		}

		// Token: 0x06002D3B RID: 11579 RVA: 0x000B9C48 File Offset: 0x000B7E48
		public override CalendarItemBase GetEmbeddedItem()
		{
			CalendarItemBase embeddedItem = base.GetEmbeddedItem();
			this.UpdateParticipantsOnCalendarItem(embeddedItem, true);
			return embeddedItem;
		}

		// Token: 0x06002D3C RID: 11580 RVA: 0x000B9C68 File Offset: 0x000B7E68
		public void ProcessMeetingRequestForGroupMailbox(ref CalendarItemBase originalCalendarItem)
		{
			this.CheckDisposed("ProcessMeetingRequestForGroupMailbox");
			originalCalendarItem.Reminder.IsSet = false;
			if (!originalCalendarItem.IsCancelled)
			{
				originalCalendarItem.RespondToMeetingRequest(ResponseType.Accept, true, false, null, null);
			}
		}

		// Token: 0x17000E99 RID: 3737
		// (get) Token: 0x06002D3D RID: 11581 RVA: 0x000B9CB3 File Offset: 0x000B7EB3
		protected override bool ShouldBeSentFromOrganizer
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06002D3E RID: 11582 RVA: 0x000B9CB8 File Offset: 0x000B7EB8
		protected override void UpdateCalendarItemInternal(ref CalendarItemBase correlatedCalendarItem)
		{
			ExTraceGlobals.MeetingMessageTracer.Information<GlobalObjectId>((long)this.GetHashCode(), "Storage.MeetingRequest.UpdateCalendarItemInternal: GOID={0}", this.GlobalObjectId);
			CalendarItemBase calendarItemBase = null;
			try
			{
				calendarItemBase = base.GetCalendarItemToUpdate(correlatedCalendarItem);
				this.UpdateMeetingRequest(calendarItemBase);
				this.UpdateCalendarItemProperties(calendarItemBase);
				this.UpdateAttachmentsOnCalendarItem(calendarItemBase);
				this.UpdateParticipantsOnCalendarItem(calendarItemBase, false);
				ExTraceGlobals.MeetingMessageTracer.Information<GlobalObjectId>((long)this.GetHashCode(), "Leaving Storage.MeetingRequest.UpdateCalendarItemInternal: GOID={0}", this.GlobalObjectId);
				if (correlatedCalendarItem == null)
				{
					correlatedCalendarItem = calendarItemBase;
				}
			}
			finally
			{
				if (calendarItemBase != null && calendarItemBase != correlatedCalendarItem)
				{
					calendarItemBase.Dispose();
					calendarItemBase = null;
				}
			}
		}

		// Token: 0x06002D3F RID: 11583 RVA: 0x000B9D50 File Offset: 0x000B7F50
		private void UpdateCalendarItemProperties(CalendarItemBase calendarItemBase)
		{
			CalendarItemOccurrence calendarItemOccurrence = calendarItemBase as CalendarItemOccurrence;
			CalendarItem calendarItem = calendarItemBase as CalendarItem;
			bool preserveLocalExceptions = false;
			if (calendarItemOccurrence != null)
			{
				calendarItemOccurrence.MakeModifiedOccurrence();
			}
			else if (calendarItem != null)
			{
				preserveLocalExceptions = this.ShouldPreserveLocalExceptions(calendarItem);
			}
			this.CopyMeetingRequestProperties(calendarItemBase, preserveLocalExceptions);
		}

		// Token: 0x06002D40 RID: 11584 RVA: 0x000B9D8B File Offset: 0x000B7F8B
		private void UpdateAttachmentsOnCalendarItem(CalendarItemBase calendarItem)
		{
			if (!base.IsRepairUpdateMessage)
			{
				calendarItem.LocationIdentifierHelperInstance.SetLocationIdentifier(50037U);
				base.ReplaceAttachments(calendarItem);
				return;
			}
			ExTraceGlobals.MeetingMessageTracer.Information<GlobalObjectId>((long)this.GetHashCode(), "Storage.MeetingRequest.UpdateAttachments will preserve attachments for this message: GOID={0}", this.GlobalObjectId);
		}

		// Token: 0x06002D41 RID: 11585 RVA: 0x000B9DCC File Offset: 0x000B7FCC
		private bool ShouldPreserveLocalExceptions(CalendarItem calendarItem)
		{
			InternalRecurrence recurrenceFromItem = CalendarItem.GetRecurrenceFromItem(this);
			Recurrence recurrence = null;
			try
			{
				recurrence = calendarItem.Recurrence;
			}
			catch (RecurrenceFormatException ex)
			{
				ExTraceGlobals.MeetingMessageTracer.Information<string, GlobalObjectId>((long)this.GetHashCode(), "Storage.MeetingRequest.ShouldPreserveLocalExceptions encountered a recurrence format exception ({0}). Local exceptions will not be preserved: GOID={1}", ex.Message, this.GlobalObjectId);
			}
			catch (CorruptDataException ex2)
			{
				ExTraceGlobals.MeetingMessageTracer.Information<string, GlobalObjectId>((long)this.GetHashCode(), "Storage.MeetingRequest.ShouldPreserveLocalExceptions encountered a corrupt data exception ({0}). Local exceptions will not be preserved: GOID={1}", ex2.Message, this.GlobalObjectId);
			}
			return recurrence != null && recurrenceFromItem != null && recurrence.Equals(recurrenceFromItem) && MeetingRequest.ArePropsEqual(calendarItem.TryGetProperty(InternalSchema.StartTime), base.TryGetProperty(InternalSchema.StartTime)) && MeetingRequest.ArePropsEqual(calendarItem.TryGetProperty(InternalSchema.EndTime), base.TryGetProperty(InternalSchema.EndTime));
		}

		// Token: 0x06002D42 RID: 11586 RVA: 0x000B9E9C File Offset: 0x000B809C
		private void CopyRecurrenceBlob(CalendarItem calendarItem, byte[] recurrenceBlob, bool preserveLocalExceptions)
		{
			InternalRecurrence originalRecurrence = preserveLocalExceptions ? CalendarItem.GetRecurrenceFromItem(calendarItem) : null;
			calendarItem.LocationIdentifierHelperInstance.SetLocationIdentifier(46197U);
			calendarItem[InternalSchema.AppointmentRecurrenceBlob] = recurrenceBlob;
			if (preserveLocalExceptions)
			{
				ExTraceGlobals.MeetingMessageTracer.Information<GlobalObjectId, string>((long)this.GetHashCode(), "Storage.MeetingRequest.CopyMeetingRequestProperties: GOID={0}; {1}", this.GlobalObjectId, "Will merge the recurrence blob.");
				CalendarItemBase.CopyPropertiesTo(this, calendarItem, new PropertyDefinition[]
				{
					CalendarItemBaseSchema.TimeZone,
					CalendarItemBaseSchema.TimeZoneBlob,
					CalendarItemBaseSchema.TimeZoneDefinitionRecurring
				});
				InternalRecurrence recurrenceFromItem = CalendarItem.GetRecurrenceFromItem(calendarItem);
				if (RecurrenceBlobMerger.Merge(base.Session, calendarItem, this.GlobalObjectId, originalRecurrence, recurrenceFromItem))
				{
					calendarItem[InternalSchema.AppointmentRecurrenceBlob] = recurrenceFromItem.ToByteArray();
				}
			}
		}

		// Token: 0x06002D43 RID: 11587 RVA: 0x000B9F4C File Offset: 0x000B814C
		private void SetDefaultMeetingRequestTypeIfNeeded(CalendarItemBase calendarItem)
		{
			object valueOrDefault = base.GetValueOrDefault<object>(InternalSchema.MeetingRequestType);
			MeetingMessageType meetingRequestType = MeetingMessageType.FullUpdate;
			bool flag = false;
			if (valueOrDefault == null)
			{
				flag = true;
			}
			if (calendarItem.IsNew && this.MeetingRequestType != MeetingMessageType.PrincipalWantsCopy)
			{
				if (!(calendarItem is CalendarItemOccurrence))
				{
					meetingRequestType = MeetingMessageType.NewMeetingRequest;
				}
				flag = true;
			}
			if (flag)
			{
				base.LocationIdentifierHelperInstance.SetLocationIdentifier(48245U, LastChangeAction.SetDefaultMeetingRequestType);
				this.MeetingRequestType = meetingRequestType;
			}
		}

		// Token: 0x06002D44 RID: 11588 RVA: 0x000B9FB0 File Offset: 0x000B81B0
		internal void UpdateMeetingRequest(CalendarItemBase calendarItem)
		{
			ExTraceGlobals.MeetingMessageTracer.Information<GlobalObjectId>((long)this.GetHashCode(), "Storage.MeetingRequest.UpdateMeetingRequest: GOID={0}", this.GlobalObjectId);
			this.SetDefaultMeetingRequestTypeIfNeeded(calendarItem);
			this.UpdateIconIndex();
			base.AdjustAppointmentState();
			if (!calendarItem.IsNew)
			{
				if (calendarItem.CalendarItemType != CalendarItemType.RecurringMaster)
				{
					base.LocationIdentifierHelperInstance.SetLocationIdentifier(64629U);
					this[InternalSchema.OldStartWhole] = calendarItem.StartTime;
					this[InternalSchema.OldEndWhole] = calendarItem.EndTime;
					this.SetCalendarProcessingSteps(CalendarProcessingSteps.CopiedOldProps);
				}
				string valueOrDefault = base.GetValueOrDefault<string>(InternalSchema.OldLocation, string.Empty);
				if (string.IsNullOrEmpty(valueOrDefault))
				{
					string location = calendarItem.Location;
					if (!string.IsNullOrEmpty(location))
					{
						this.OldLocation = location;
						this.SetCalendarProcessingSteps(CalendarProcessingSteps.CopiedOldProps);
					}
				}
				if (!PropertyError.IsPropertyError(calendarItem.TryGetProperty(CalendarItemBaseSchema.ResponseType)))
				{
					base.LocationIdentifierHelperInstance.SetLocationIdentifier(56437U);
					this[InternalSchema.ResponseState] = (int)calendarItem.ResponseType;
				}
			}
		}

		// Token: 0x06002D45 RID: 11589 RVA: 0x000BA0BC File Offset: 0x000B82BC
		private void UpdateIconIndex()
		{
			IconIndex valueOrDefault = base.GetValueOrDefault<IconIndex>(InternalSchema.IconIndex, IconIndex.Default);
			if (valueOrDefault == IconIndex.Default)
			{
				base.LocationIdentifierHelperInstance.SetLocationIdentifier(44149U);
				this[InternalSchema.IconIndex] = CalendarItemBase.CalculateMeetingRequestIcon(this);
			}
		}

		// Token: 0x06002D46 RID: 11590 RVA: 0x000BA100 File Offset: 0x000B8300
		private void LoadOccurrencesExceptionalViewProperties()
		{
			base.Load(new PropertyDefinition[]
			{
				MeetingRequestSchema.OccurrencesExceptionalViewProperties
			});
			this.occurrencesExceptionalViewPropertiesLoaded = true;
		}

		// Token: 0x06002D47 RID: 11591 RVA: 0x000BA12C File Offset: 0x000B832C
		private void CopyMeetingRequestProperties(CalendarItemBase calendarItem, bool preserveLocalExceptions)
		{
			long size = calendarItem.Body.Size;
			calendarItem.LocationIdentifierHelperInstance.SetLocationIdentifier(42101U);
			if (!base.IsRepairUpdateMessage || (this.ChangeHighlight & ChangeHighlightProperties.BodyProps) == ChangeHighlightProperties.BodyProps)
			{
				Body.CopyBody(this, calendarItem, false);
				this.CopyNlgPropertiesTo(calendarItem);
			}
			ChangeHighlightProperties changeHighlight = this.ChangeHighlight;
			this.ProcessChangeHighlights(calendarItem, calendarItem.Body.Size, size);
			MeetingMessageType meetingMessageType = base.GetValueOrDefault<MeetingMessageType>(InternalSchema.MeetingRequestType, MeetingMessageType.NewMeetingRequest);
			if (meetingMessageType == MeetingMessageType.PrincipalWantsCopy)
			{
				if (calendarItem.IsNew)
				{
					meetingMessageType = MeetingMessageType.NewMeetingRequest;
					ExTraceGlobals.MeetingMessageTracer.Information<GlobalObjectId>((long)this.GetHashCode(), "Storage.MeetingRequest.CopyMeetingRequestProperties: GOID={0}; Meeting type is PrincipalWantsCopy and calendar item just created.", this.GlobalObjectId);
				}
				else
				{
					meetingMessageType = base.GetValueOrDefault<MeetingMessageType>(InternalSchema.OriginalMeetingType, MeetingMessageType.NewMeetingRequest);
					ExTraceGlobals.MeetingMessageTracer.Information<GlobalObjectId, MeetingMessageType>((long)this.GetHashCode(), "Storage.MeetingRequest.CopyMeetingRequestProperties: GOID={0}; Meeting type is PrincipalWantsCopy. Will use OriginalMeetingType {1}", this.GlobalObjectId, meetingMessageType);
				}
			}
			base.LocationIdentifierHelperInstance.SetLocationIdentifier(60533U, LastChangeAction.CopyMeetingRequestProperties);
			calendarItem.LocationIdentifierHelperInstance.SetLocationIdentifier(35957U, LastChangeAction.CopyMeetingRequestProperties);
			if (MeetingMessageType.NewMeetingRequest == meetingMessageType || calendarItem.IsNew)
			{
				ExTraceGlobals.MeetingMessageTracer.Information<GlobalObjectId, string>((long)this.GetHashCode(), "Storage.MeetingRequest.CopyMeetingRequestProperties: GOID={0}; {1}", this.GlobalObjectId, "Copying WriteOnCreate properties onto calendar item.");
				calendarItem.LocationIdentifierHelperInstance.SetLocationIdentifier(52341U);
				CalendarItemBase.CopyPropertiesTo(this, calendarItem, MeetingMessage.WriteOnCreateProperties);
				if (base.IsSeriesMessage)
				{
					CalendarItemBase.CopyPropertiesTo(this, calendarItem, MeetingMessage.WriteOnCreateSeriesProperties);
				}
				Reminder.EnsureMinutesBeforeStartIsInRange(calendarItem, this.consumerDefaultMinutesBeforeStart);
				if (base.IsRepairUpdateMessage)
				{
					int? valueAsNullable = base.GetValueAsNullable<int>(CalendarItemBaseSchema.ItemVersion);
					if (valueAsNullable != null)
					{
						calendarItem[CalendarItemBaseSchema.ItemVersion] = valueAsNullable;
					}
				}
			}
			if (meetingMessageType == MeetingMessageType.NewMeetingRequest || (!base.IsRepairUpdateMessage && meetingMessageType == MeetingMessageType.FullUpdate))
			{
				calendarItem.ResponseType = ResponseType.NotResponded;
				BusyType valueOrDefault = base.GetValueOrDefault<BusyType>(InternalSchema.IntendedFreeBusyStatus, BusyType.Busy);
				BusyType valueOrDefault2 = base.GetValueOrDefault<BusyType>(InternalSchema.FreeBusyStatus, BusyType.Tentative);
				calendarItem.LocationIdentifierHelperInstance.SetLocationIdentifier(58485U);
				calendarItem[InternalSchema.IntendedFreeBusyStatus] = valueOrDefault;
				calendarItem[InternalSchema.FreeBusyStatus] = ((valueOrDefault != BusyType.Free) ? valueOrDefault2 : BusyType.Free);
			}
			ExTraceGlobals.MeetingMessageTracer.Information<GlobalObjectId, string>((long)this.GetHashCode(), "Storage.MeetingRequest.CopyMeetingRequestProperties: GOID={0}; {1}", this.GlobalObjectId, "Copying properties onto calendar item.");
			byte[] largeBinaryProperty = base.PropertyBag.GetLargeBinaryProperty(InternalSchema.AppointmentRecurrenceBlob);
			CalendarItem calendarItem2 = calendarItem as CalendarItem;
			bool flag = false;
			if (largeBinaryProperty != null && calendarItem2 != null)
			{
				calendarItem2.SuppressUpdateRecurrenceTimeOffset = true;
				this.CopyRecurrenceBlob(calendarItem2, largeBinaryProperty, preserveLocalExceptions);
				flag = true;
			}
			else if (calendarItem.CalendarItemType == CalendarItemType.RecurringMaster)
			{
				CalendarItem calendarItem3 = calendarItem as CalendarItem;
				if (calendarItem3 != null)
				{
					calendarItem3.Recurrence = null;
				}
			}
			calendarItem.LocationIdentifierHelperInstance.SetLocationIdentifier(62581U);
			if (calendarItem is CalendarItem)
			{
				PropertyChangeMetadataProcessingFlags propertyChangeMetadataProcessingFlags = calendarItem.GetValueOrDefault<PropertyChangeMetadataProcessingFlags>(CalendarItemSchema.PropertyChangeMetadataProcessingFlags, PropertyChangeMetadataProcessingFlags.None);
				propertyChangeMetadataProcessingFlags |= PropertyChangeMetadataProcessingFlags.OverrideMetadata;
				calendarItem[CalendarItemSchema.PropertyChangeMetadataProcessingFlags] = propertyChangeMetadataProcessingFlags;
			}
			if (this.ShouldPreserveAttendeesChanges(calendarItem))
			{
				ExTraceGlobals.MeetingMessageTracer.Information<GlobalObjectId, string>((long)this.GetHashCode(), "Storage.MeetingRequest.CopyMeetingRequestProperties: GOID={0}; {1}", this.GlobalObjectId, "Will copy properties trying to preserve attendee's changes.");
				CalendarItemBase.CopyPropertiesTo(this, calendarItem, CalendarItemProperties.NonPreservableMeetingMessageProperties);
				PreservableMeetingMessageProperty.CopyPreserving(new PreservablePropertyContext(this, calendarItem, changeHighlight));
				if (calendarItem is CalendarItem)
				{
					PropertyChangeMetadata valueOrDefault3 = calendarItem.GetValueOrDefault<PropertyChangeMetadata>(InternalSchema.PropertyChangeMetadata);
					PropertyChangeMetadata valueOrDefault4 = base.GetValueOrDefault<PropertyChangeMetadata>(InternalSchema.PropertyChangeMetadata);
					PropertyChangeMetadata propertyChangeMetadata = PropertyChangeMetadata.Merge(valueOrDefault3, valueOrDefault4);
					if (propertyChangeMetadata != null)
					{
						calendarItem[InternalSchema.PropertyChangeMetadata] = propertyChangeMetadata;
					}
				}
			}
			else
			{
				if (calendarItem.CalendarItemType == CalendarItemType.RecurringMaster || calendarItem.CalendarItemType == CalendarItemType.Single)
				{
					calendarItem.DeleteProperties(MeetingMessage.DisplayTimeZoneProperties);
				}
				if (calendarItem is CalendarItem)
				{
					CalendarItemBase.CopyPropertiesTo(this, calendarItem, new PropertyDefinition[]
					{
						InternalSchema.PropertyChangeMetadataRaw
					});
				}
				CalendarItemBase.CopyPropertiesTo(this, calendarItem, MeetingMessage.MeetingMessageProperties);
			}
			string valueOrDefault5 = base.GetValueOrDefault<string>(InternalSchema.AppointmentClass);
			if (valueOrDefault5 != null && ObjectClass.IsDerivedClass(valueOrDefault5, "IPM.Appointment"))
			{
				calendarItem.ClassName = valueOrDefault5;
			}
			Microsoft.Exchange.Data.Storage.Item.CopyCustomPublicStrings(this, calendarItem);
			calendarItem.LocationIdentifierHelperInstance.SetLocationIdentifier(54389U);
			CalendarItemBase.CopyPropertiesTo(this, calendarItem, new PropertyDefinition[]
			{
				InternalSchema.TimeZoneDefinitionRecurring
			});
			if (meetingMessageType == MeetingMessageType.InformationalUpdate && !calendarItem.IsCalendarItemTypeOccurrenceOrException)
			{
				Sensitivity? valueAsNullable2 = base.GetValueAsNullable<Sensitivity>(InternalSchema.Sensitivity);
				if (valueAsNullable2 != null && calendarItem.Sensitivity != Sensitivity.Private && Enum.IsDefined(typeof(Sensitivity), valueAsNullable2.Value))
				{
					calendarItem.Sensitivity = valueAsNullable2.Value;
				}
			}
			calendarItem.LocationIdentifierHelperInstance.SetLocationIdentifier(50293U);
			calendarItem.Reminder.Adjust();
			if (flag && calendarItem2 != null)
			{
				calendarItem2.ReloadRecurrence();
			}
		}

		// Token: 0x06002D48 RID: 11592 RVA: 0x000BA594 File Offset: 0x000B8794
		protected override bool CheckPreConditions(CalendarItemBase originalCalendarItem, bool shouldThrow, bool canUpdatePrincipalCalendar)
		{
			if (!base.CheckPreConditions(originalCalendarItem, shouldThrow, canUpdatePrincipalCalendar))
			{
				return false;
			}
			bool flag = (originalCalendarItem != null) ? originalCalendarItem.IsOrganizer() : base.IsMailboxOwnerTheSender();
			if (!flag)
			{
				return true;
			}
			if (shouldThrow)
			{
				throw new InvalidOperationException(ServerStrings.ExOrganizerCannotCallUpdateCalendarItem);
			}
			ExTraceGlobals.MeetingMessageTracer.Information<GlobalObjectId>((long)this.GetHashCode(), "Storage.MeetingMessage.CheckPreConditions: GOID={0}; isOrganizer=true; returning false", this.GlobalObjectId);
			return false;
		}

		// Token: 0x06002D49 RID: 11593 RVA: 0x000BA5F8 File Offset: 0x000B87F8
		private void ProcessChangeHighlights(CalendarItemBase calendarItem, long meetingRequestBodySize, long calendarItemBodySize)
		{
			int num = base.GetValueOrDefault<int>(InternalSchema.ChangeHighlight, -1);
			if (calendarItem.IsNew && !(calendarItem is CalendarItemOccurrence))
			{
				num = 0;
				base.LocationIdentifierHelperInstance.SetLocationIdentifier(47221U);
				this[InternalSchema.ChangeHighlight] = num;
			}
			else
			{
				ChangeHighlightHelper changeHighlightHelper = MeetingRequest.CompareForChangeHighlightOnUpdatedItems(base.PropertyBag, meetingRequestBodySize, calendarItem.PropertyBag, calendarItemBodySize);
				if (this.ShouldPreserveAttendeesChanges(calendarItem))
				{
					PreservablePropertyContext context = new PreservablePropertyContext(this, calendarItem, (ChangeHighlightProperties)num);
					foreach (StorePropertyDefinition storePropertyDefinition in ChangeHighlightHelper.HighlightProperties)
					{
						if (changeHighlightHelper[storePropertyDefinition])
						{
							PreservableMeetingMessageProperty preservableMeetingMessageProperty = null;
							if (PreservableMeetingMessageProperty.InstanceFromPropertyDefinition.TryGetValue(storePropertyDefinition, out preservableMeetingMessageProperty))
							{
								changeHighlightHelper[storePropertyDefinition] = !preservableMeetingMessageProperty.ShouldPreserve(context);
							}
						}
					}
				}
				num = changeHighlightHelper.HighlightFlags;
				MeetingMessageType meetingMessageType = changeHighlightHelper.SuggestedMeetingType;
				if (meetingMessageType == MeetingMessageType.InformationalUpdate && calendarItem.IsCancelled)
				{
					meetingMessageType = MeetingMessageType.FullUpdate;
				}
				if (this.MeetingRequestType != MeetingMessageType.PrincipalWantsCopy)
				{
					this.MeetingRequestType = meetingMessageType;
				}
				else
				{
					base.LocationIdentifierHelperInstance.SetLocationIdentifier(35701U);
					this[InternalSchema.OriginalMeetingType] = meetingMessageType;
				}
				base.LocationIdentifierHelperInstance.SetLocationIdentifier(52085U);
				this[InternalSchema.ChangeHighlight] = num;
			}
			calendarItem.SetChangeHighlight(num);
		}

		// Token: 0x06002D4A RID: 11594 RVA: 0x000BA74C File Offset: 0x000B894C
		internal static ChangeHighlightHelper CompareForChangeHighlightOnUpdatedItems(PropertyBag propBagItem1, long bodySizeItem1, PropertyBag propBagItem2, long bodySizeItem2)
		{
			ChangeHighlightHelper changeHighlightHelper = MeetingRequest.ComparePropertyBags(propBagItem1, propBagItem2, true);
			if (bodySizeItem1 != bodySizeItem2)
			{
				changeHighlightHelper[InternalSchema.HtmlBody] = true;
			}
			return changeHighlightHelper;
		}

		// Token: 0x06002D4B RID: 11595 RVA: 0x000BA773 File Offset: 0x000B8973
		internal static ChangeHighlightHelper ComparePropertyBags(PropertyBag meetingPropBag, PropertyBag calItemPropBag)
		{
			return MeetingRequest.ComparePropertyBags(meetingPropBag, calItemPropBag, false);
		}

		// Token: 0x06002D4C RID: 11596 RVA: 0x000BA780 File Offset: 0x000B8980
		internal static bool? HasRecurrenceTypeChanged(object meetingProperty, object calendarItemProperty)
		{
			if (meetingProperty is int && (int)meetingProperty == 0 && calendarItemProperty is PropertyError)
			{
				return null;
			}
			return new bool?(!MeetingRequest.ArePropsEqual(meetingProperty, calendarItemProperty));
		}

		// Token: 0x06002D4D RID: 11597 RVA: 0x000BA7C0 File Offset: 0x000B89C0
		internal static bool? HasRecurrencePatternChanged(object meetingProperty, object calendarItemProperty)
		{
			if (string.IsNullOrEmpty(meetingProperty as string) && (calendarItemProperty is PropertyError || string.IsNullOrEmpty(calendarItemProperty as string)))
			{
				return null;
			}
			return new bool?(!MeetingRequest.ArePropsEqual(meetingProperty, calendarItemProperty));
		}

		// Token: 0x06002D4E RID: 11598 RVA: 0x000BA808 File Offset: 0x000B8A08
		internal static ChangeHighlightHelper ComparePropertyBags(PropertyBag meetingPropBag, PropertyBag calItemPropBag, bool isUpdate)
		{
			ChangeHighlightHelper changeHighlightHelper = new ChangeHighlightHelper(0, isUpdate);
			if (meetingPropBag == null || calItemPropBag == null)
			{
				return changeHighlightHelper;
			}
			object[] array = new object[ChangeHighlightHelper.HighlightProperties.Length];
			object[] array2 = new object[ChangeHighlightHelper.HighlightProperties.Length];
			for (int i = 0; i < ChangeHighlightHelper.HighlightProperties.Length; i++)
			{
				StorePropertyDefinition storePropertyDefinition = ChangeHighlightHelper.HighlightProperties[i];
				array[i] = meetingPropBag.TryGetProperty(storePropertyDefinition);
				array2[i] = calItemPropBag.TryGetProperty(storePropertyDefinition);
				if (storePropertyDefinition == InternalSchema.MapiRecurrenceType)
				{
					bool? flag = MeetingRequest.HasRecurrenceTypeChanged(array[i], array2[i]);
					if (flag != null)
					{
						changeHighlightHelper[storePropertyDefinition] = flag.Value;
					}
				}
				else if (storePropertyDefinition == InternalSchema.RecurrencePattern)
				{
					bool? flag2 = MeetingRequest.HasRecurrencePatternChanged(array[i], array2[i]);
					if (flag2 != null)
					{
						changeHighlightHelper[storePropertyDefinition] = flag2.Value;
					}
				}
				else if (storePropertyDefinition != InternalSchema.Duration || !string.IsNullOrEmpty(array[i] as string))
				{
					changeHighlightHelper[storePropertyDefinition] = !MeetingRequest.ArePropsEqual(array[i], array2[i]);
				}
			}
			return changeHighlightHelper;
		}

		// Token: 0x06002D4F RID: 11599 RVA: 0x000BA90C File Offset: 0x000B8B0C
		private void UpdateParticipantsOnCalendarItem(CalendarItemBase calendarItem, bool isReadOnlyItem)
		{
			this.CheckDisposed("ProcessParticipants");
			base.LocationIdentifierHelperInstance.SetLocationIdentifier(62325U, LastChangeAction.ProcessParticipants);
			IAttendeeCollection attendeeCollection = calendarItem.AttendeeCollection;
			calendarItem.LocationIdentifierHelperInstance.SetLocationIdentifier(37749U);
			attendeeCollection.Clear();
			bool flag = false;
			if (base.From != null)
			{
				calendarItem.LocationIdentifierHelperInstance.SetLocationIdentifier(54133U);
				attendeeCollection.Add(base.From, AttendeeType.Required, null, null, false).RecipientFlags = (RecipientFlags.Sendable | RecipientFlags.Organizer);
				flag = true;
			}
			List<BlobRecipient> list = this.GetUnsendableRecipients();
			if (!isReadOnlyItem)
			{
				this.SetUnsendableRecipients(list);
			}
			list = MeetingRequest.MergeRecipientLists(base.Recipients, list);
			Participant participant = null;
			if (base.IsDelegated())
			{
				participant = (Participant)base.TryGetProperty(InternalSchema.ReceivedBy);
			}
			foreach (BlobRecipient blobRecipient in list)
			{
				RecipientFlags valueOrDefault = blobRecipient.GetValueOrDefault<RecipientFlags>(InternalSchema.RecipientFlags);
				bool flag2 = (valueOrDefault & RecipientFlags.Organizer) == RecipientFlags.Organizer;
				if (flag2)
				{
					if (flag)
					{
						continue;
					}
					flag = true;
				}
				if (!blobRecipient.Participant.AreAddressesEqual(base.From))
				{
					RecipientItemType type = MapiUtil.MapiRecipientTypeToRecipientItemType(blobRecipient.GetValueOrDefault<RecipientType>(InternalSchema.RecipientType, RecipientType.To));
					AttendeeType attendeeType = Attendee.RecipientItemTypeToAttendeeType(type);
					Participant participant2 = blobRecipient.Participant;
					if (participant != null && Participant.HasSameEmail(participant2, participant, base.MailboxSession, true))
					{
						calendarItem.LocationIdentifierHelperInstance.SetLocationIdentifier(41845U);
						attendeeCollection.Add(base.ReceivedRepresenting, attendeeType, null, null, false);
						participant = null;
					}
					else
					{
						calendarItem.LocationIdentifierHelperInstance.SetLocationIdentifier(58229U);
						attendeeCollection.Add(participant2, attendeeType, null, null, false);
					}
				}
			}
		}

		// Token: 0x06002D50 RID: 11600 RVA: 0x000BAAFC File Offset: 0x000B8CFC
		private bool ShouldPreserveAttendeesChanges(CalendarItemBase calItem)
		{
			MeetingMessageType valueOrDefault = base.GetValueOrDefault<MeetingMessageType>(InternalSchema.MeetingRequestType, MeetingMessageType.NewMeetingRequest);
			return valueOrDefault == MeetingMessageType.InformationalUpdate || valueOrDefault == MeetingMessageType.FullUpdate;
		}

		// Token: 0x06002D51 RID: 11601 RVA: 0x000BAB28 File Offset: 0x000B8D28
		private void CopyNlgPropertiesTo(CalendarItemBase calendarItem)
		{
			bool flag = false;
			foreach (PropertyExistenceTracker propertyDefinition in MeetingRequest.NlgExtractedExistenceProperties)
			{
				if (base.GetValueOrDefault<bool>(propertyDefinition, false))
				{
					flag = true;
					break;
				}
			}
			if (flag)
			{
				base.Load(MeetingRequest.NlgExtractedProperties);
			}
			foreach (StorePropertyDefinition storePropertyDefinition in MeetingRequest.NlgExtractedProperties)
			{
				object obj = flag ? base.TryGetProperty(storePropertyDefinition) : null;
				if (obj != null && !PropertyError.IsPropertyError(obj))
				{
					calendarItem[storePropertyDefinition] = obj;
				}
				else
				{
					calendarItem.DeleteProperties(new PropertyDefinition[]
					{
						storePropertyDefinition
					});
				}
			}
		}

		// Token: 0x06002D52 RID: 11602 RVA: 0x000BABD0 File Offset: 0x000B8DD0
		internal static bool ArePropsEqual(object obj1, object obj2)
		{
			if (obj1 == null && obj2 == null)
			{
				return true;
			}
			if ((obj1 == null && obj2 != null) || (obj1 != null && obj2 == null))
			{
				return false;
			}
			PropertyError propertyError = obj1 as PropertyError;
			PropertyError propertyError2 = obj2 as PropertyError;
			if (propertyError != null && propertyError2 != null)
			{
				return propertyError.PropertyErrorCode == propertyError2.PropertyErrorCode;
			}
			return obj1.Equals(obj2);
		}

		// Token: 0x06002D53 RID: 11603 RVA: 0x000BAC1D File Offset: 0x000B8E1D
		internal override void Initialize()
		{
			this.Initialize("IPM.Schedule.Meeting.Request");
		}

		// Token: 0x06002D54 RID: 11604 RVA: 0x000BAC2C File Offset: 0x000B8E2C
		internal List<BlobRecipient> GetMergedRecipientList()
		{
			List<BlobRecipient> unsendableRecipients = this.GetUnsendableRecipients();
			return MeetingRequest.MergeRecipientLists(base.Recipients, unsendableRecipients);
		}

		// Token: 0x06002D55 RID: 11605 RVA: 0x000BAC4C File Offset: 0x000B8E4C
		internal List<BlobRecipient> GetUnsendableRecipients()
		{
			List<BlobRecipient> blobRecipientList = BlobRecipientParser.ReadRecipients(this, InternalSchema.UnsendableRecipients);
			return MeetingRequest.FilterBlobRecipientList(blobRecipientList);
		}

		// Token: 0x06002D56 RID: 11606 RVA: 0x000BAC6C File Offset: 0x000B8E6C
		internal void SetUnsendableRecipients(List<BlobRecipient> list)
		{
			base.LocationIdentifierHelperInstance.SetLocationIdentifier(33653U, LastChangeAction.SetUnsendableRecipients);
			List<BlobRecipient> list2 = MeetingRequest.FilterBlobRecipientList(list);
			BlobRecipientParser.WriteRecipients(this, InternalSchema.UnsendableRecipients, list2);
		}

		// Token: 0x06002D57 RID: 11607 RVA: 0x000BAC9E File Offset: 0x000B8E9E
		private void Initialize(string itemClass)
		{
			base.Initialize();
			base.LocationIdentifierHelperInstance.SetLocationIdentifier(46581U);
			this[InternalSchema.ItemClass] = itemClass;
			base.IsResponseRequested = true;
			this[InternalSchema.IsReplyRequested] = true;
		}

		// Token: 0x040018FB RID: 6395
		private static readonly StorePropertyDefinition[] NlgExtractedProperties = new StorePropertyDefinition[]
		{
			InternalSchema.XmlExtractedAddresses,
			InternalSchema.XmlExtractedContacts,
			InternalSchema.XmlExtractedEmails,
			InternalSchema.XmlExtractedMeetings,
			InternalSchema.XmlExtractedPhones,
			InternalSchema.XmlExtractedTasks,
			InternalSchema.XmlExtractedUrls
		};

		// Token: 0x040018FC RID: 6396
		private static readonly StorePropertyDefinition[] NlgExtractedExistenceProperties = new StorePropertyDefinition[]
		{
			InternalSchema.ExtractedAddressesExists,
			InternalSchema.ExtractedContactsExists,
			InternalSchema.ExtractedEmailsExists,
			InternalSchema.ExtractedMeetingsExists,
			InternalSchema.ExtractedPhonesExists,
			InternalSchema.ExtractedTasksExists,
			InternalSchema.ExtractedUrlsExists
		};

		// Token: 0x040018FD RID: 6397
		private bool occurrencesExceptionalViewPropertiesLoaded;

		// Token: 0x040018FE RID: 6398
		private int consumerDefaultMinutesBeforeStart = 15;
	}
}
