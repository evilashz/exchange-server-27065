using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020003D9 RID: 985
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class MeetingMessage : MessageItem, IMeetingMessage, IMessageItem, IToDoItem, IItem, IStoreObject, IStorePropertyBag, IPropertyBag, IReadOnlyPropertyBag, IDisposable
	{
		// Token: 0x17000E67 RID: 3687
		// (get) Token: 0x06002C6A RID: 11370 RVA: 0x000B5967 File Offset: 0x000B3B67
		public bool IsSeriesMessage
		{
			get
			{
				return ObjectClass.IsMeetingMessageSeries(this.ClassName);
			}
		}

		// Token: 0x06002C6B RID: 11371 RVA: 0x000B5974 File Offset: 0x000B3B74
		internal MeetingMessage(ICoreItem coreItem) : base(coreItem, false)
		{
		}

		// Token: 0x06002C6C RID: 11372 RVA: 0x000B5980 File Offset: 0x000B3B80
		public static bool IsDelegateOnlyFromSession(MailboxSession session)
		{
			DelegateRuleType? delegateRuleType;
			return MeetingMessage.TryGetDelegateRuleTypeFromSession(session, out delegateRuleType) && delegateRuleType == DelegateRuleType.ForwardAndDelete;
		}

		// Token: 0x17000E68 RID: 3688
		// (get) Token: 0x06002C6D RID: 11373 RVA: 0x000B59AE File Offset: 0x000B3BAE
		public override Schema Schema
		{
			get
			{
				this.CheckDisposed("Schema::get");
				return MeetingMessageSchema.Instance;
			}
		}

		// Token: 0x06002C6E RID: 11374 RVA: 0x000B59C0 File Offset: 0x000B3BC0
		public new static MeetingMessage Bind(StoreSession session, StoreId storeId)
		{
			return MeetingMessage.Bind(session, storeId, null);
		}

		// Token: 0x06002C6F RID: 11375 RVA: 0x000B59CA File Offset: 0x000B3BCA
		public new static MeetingMessage Bind(StoreSession session, StoreId storeId, params PropertyDefinition[] propsToReturn)
		{
			return MeetingMessage.Bind(session, storeId, (ICollection<PropertyDefinition>)propsToReturn);
		}

		// Token: 0x06002C70 RID: 11376 RVA: 0x000B59D9 File Offset: 0x000B3BD9
		public new static MeetingMessage Bind(StoreSession session, StoreId storeId, ICollection<PropertyDefinition> propsToReturn)
		{
			return ItemBuilder.ItemBind<MeetingMessage>(session, storeId, MeetingMessageSchema.Instance, propsToReturn);
		}

		// Token: 0x06002C71 RID: 11377 RVA: 0x000B59E8 File Offset: 0x000B3BE8
		public static bool IsFromExternalParticipant(string routingType)
		{
			return !string.Equals("EX", routingType, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x17000E69 RID: 3689
		// (get) Token: 0x06002C72 RID: 11378 RVA: 0x000B59FC File Offset: 0x000B3BFC
		public bool IsArchiveMigratedMessage
		{
			get
			{
				ExDateTime valueOrDefault = base.GetValueOrDefault<ExDateTime>(MeetingMessageSchema.EHAMigrationExpirationDate);
				return !(valueOrDefault == ExDateTime.MinValue);
			}
		}

		// Token: 0x17000E6A RID: 3690
		// (get) Token: 0x06002C73 RID: 11379 RVA: 0x000B5A25 File Offset: 0x000B3C25
		protected MailboxSession MailboxSession
		{
			get
			{
				return base.Session as MailboxSession;
			}
		}

		// Token: 0x17000E6B RID: 3691
		// (get) Token: 0x06002C74 RID: 11380 RVA: 0x000B5A34 File Offset: 0x000B3C34
		public bool IsExternalMessage
		{
			get
			{
				this.CheckDisposed("IsExternalMessage::get");
				string valueOrDefault = base.GetValueOrDefault<string>(ItemSchema.SentRepresentingType);
				return MeetingMessage.IsFromExternalParticipant(valueOrDefault);
			}
		}

		// Token: 0x06002C75 RID: 11381 RVA: 0x000B5A60 File Offset: 0x000B3C60
		public bool IsDelegated()
		{
			MailboxSession mailboxSession = this.MailboxSession;
			if (mailboxSession == null)
			{
				return false;
			}
			Participant participant = new Participant(null, mailboxSession.MailboxOwnerLegacyDN, "EX");
			Participant valueOrDefault = base.GetValueOrDefault<Participant>(InternalSchema.ReceivedBy);
			return valueOrDefault != null && this.ReceivedRepresenting != null && Participant.HasSameEmail(valueOrDefault, participant, mailboxSession, true) && !Participant.HasSameEmail(valueOrDefault, this.ReceivedRepresenting, mailboxSession, true);
		}

		// Token: 0x17000E6C RID: 3692
		// (get) Token: 0x06002C76 RID: 11382 RVA: 0x000B5ACC File Offset: 0x000B3CCC
		public Participant ReceivedRepresenting
		{
			get
			{
				this.CheckDisposed("ReceivedRepresenting::get");
				return base.GetValueOrDefault<Participant>(InternalSchema.ReceivedRepresenting);
			}
		}

		// Token: 0x17000E6D RID: 3693
		// (get) Token: 0x06002C77 RID: 11383 RVA: 0x000B5AF1 File Offset: 0x000B3CF1
		public string CalendarOriginatorId
		{
			get
			{
				this.CheckDisposed("CalendarOriginatorId::get");
				return base.GetValueOrDefault<string>(InternalSchema.CalendarOriginatorId);
			}
		}

		// Token: 0x17000E6E RID: 3694
		// (get) Token: 0x06002C78 RID: 11384 RVA: 0x000B5B09 File Offset: 0x000B3D09
		// (set) Token: 0x06002C79 RID: 11385 RVA: 0x000B5B26 File Offset: 0x000B3D26
		public string SeriesId
		{
			get
			{
				this.CheckDisposed("SeriesId::get");
				return base.GetValueOrDefault<string>(MeetingMessageSchema.SeriesId, string.Empty);
			}
			set
			{
				this.CheckDisposed("SeriesId::set");
				base.LocationIdentifierHelperInstance.SetLocationIdentifier(65532U);
				this[InternalSchema.SeriesId] = value;
			}
		}

		// Token: 0x17000E6F RID: 3695
		// (get) Token: 0x06002C7A RID: 11386 RVA: 0x000B5B4F File Offset: 0x000B3D4F
		// (set) Token: 0x06002C7B RID: 11387 RVA: 0x000B5B68 File Offset: 0x000B3D68
		public int SeriesSequenceNumber
		{
			get
			{
				this.CheckDisposed("SeriesSequenceNumber::get");
				return base.GetValueOrDefault<int>(MeetingMessageSchema.SeriesSequenceNumber, -1);
			}
			set
			{
				this.CheckDisposed("SeriesSequenceNumber::set");
				base.LocationIdentifierHelperInstance.SetLocationIdentifier(40956U);
				this[MeetingMessageSchema.SeriesSequenceNumber] = value;
			}
		}

		// Token: 0x06002C7C RID: 11388 RVA: 0x000B5B96 File Offset: 0x000B3D96
		public virtual CalendarItemBase GetCorrelatedItem()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06002C7D RID: 11389 RVA: 0x000B5B9D File Offset: 0x000B3D9D
		public virtual CalendarItemBase GetCorrelatedItem(bool shouldDetectDuplicateIds, out IEnumerable<VersionedId> detectedDuplicatesIds)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06002C7E RID: 11390 RVA: 0x000B5BA4 File Offset: 0x000B3DA4
		public virtual CalendarItemBase GetCachedCorrelatedItem()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06002C7F RID: 11391 RVA: 0x000B5BAB File Offset: 0x000B3DAB
		public virtual CalendarItemBase TryGetCorrelatedItemFromHintId(MailboxSession session, StoreObjectId hintId)
		{
			throw new NotImplementedException();
		}

		// Token: 0x17000E70 RID: 3696
		// (get) Token: 0x06002C80 RID: 11392 RVA: 0x000B5BB2 File Offset: 0x000B3DB2
		// (set) Token: 0x06002C81 RID: 11393 RVA: 0x000B5BB9 File Offset: 0x000B3DB9
		public virtual bool IsProcessed
		{
			get
			{
				throw new NotSupportedException();
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17000E71 RID: 3697
		// (get) Token: 0x06002C82 RID: 11394 RVA: 0x000B5BC0 File Offset: 0x000B3DC0
		public bool IsRepairUpdateMessage
		{
			get
			{
				AppointmentAuxiliaryFlags valueOrDefault = base.GetValueOrDefault<AppointmentAuxiliaryFlags>(MeetingMessageSchema.AppointmentAuxiliaryFlags);
				return (valueOrDefault & AppointmentAuxiliaryFlags.RepairUpdateMessage) != (AppointmentAuxiliaryFlags)0;
			}
		}

		// Token: 0x17000E72 RID: 3698
		// (get) Token: 0x06002C83 RID: 11395 RVA: 0x000B5BE3 File Offset: 0x000B3DE3
		// (set) Token: 0x06002C84 RID: 11396 RVA: 0x000B5BFB File Offset: 0x000B3DFB
		public bool CalendarProcessed
		{
			get
			{
				this.CheckDisposed("CalendarProcessed::get");
				return base.GetValueOrDefault<bool>(InternalSchema.CalendarProcessed);
			}
			set
			{
				this.CheckDisposed("CalendarProcessed::set");
				base.LocationIdentifierHelperInstance.SetLocationIdentifier(52853U);
				this[InternalSchema.CalendarProcessed] = value;
			}
		}

		// Token: 0x06002C85 RID: 11397 RVA: 0x000B5C29 File Offset: 0x000B3E29
		public virtual CalendarItemBase GetEmbeddedItem()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06002C86 RID: 11398 RVA: 0x000B5C30 File Offset: 0x000B3E30
		public virtual CalendarItemBase GetCachedEmbeddedItem()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06002C87 RID: 11399 RVA: 0x000B5C37 File Offset: 0x000B3E37
		public virtual bool TryUpdateCalendarItem(ref CalendarItemBase originalCalendarItem, bool canUpdatePrincipalCalendar)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06002C88 RID: 11400 RVA: 0x000B5C3E File Offset: 0x000B3E3E
		public virtual CalendarItemBase UpdateCalendarItem(bool canUpdatePrincipalCalendar)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06002C89 RID: 11401 RVA: 0x000B5C48 File Offset: 0x000B3E48
		protected static bool TryGetDelegateRuleTypeFromSession(MailboxSession session, out DelegateRuleType? ruleType)
		{
			bool result = false;
			ruleType = null;
			if (session != null && session.Capabilities.CanHaveDelegateUsers)
			{
				try
				{
					DelegateUserCollection delegateUserCollection = new DelegateUserCollection(session);
					if (delegateUserCollection.Count > 0)
					{
						ruleType = new DelegateRuleType?(delegateUserCollection.DelegateRuleType);
					}
					result = true;
				}
				catch (DelegateUserNoFreeBusyFolderException)
				{
					ExTraceGlobals.MeetingMessageTracer.Information((long)session.GetHashCode(), "Storage.MeetingMessage.TryGetDelegateType: NoFreeBusyData Folder, failing to get delegate rule type.");
				}
				catch (ObjectNotFoundException)
				{
					ExTraceGlobals.MeetingMessageTracer.Information((long)session.GetHashCode(), "Storage.MeetingMessage.TryGetDelegateType: No delegates found, failing to get delegate rule type.");
				}
			}
			return result;
		}

		// Token: 0x06002C8A RID: 11402 RVA: 0x000B5CE4 File Offset: 0x000B3EE4
		public bool IsOutOfDate()
		{
			this.CheckDisposed("IsOutOfDate");
			ExTraceGlobals.MeetingMessageTracer.Information((long)this.GetHashCode(), "Storage.MeetingMessage.IsOutOfDate");
			MeetingMessageType? valueAsNullable = base.GetValueAsNullable<MeetingMessageType>(InternalSchema.MeetingRequestType);
			if (valueAsNullable != null && valueAsNullable.Value == MeetingMessageType.Outdated)
			{
				return true;
			}
			CalendarItemBase correlatedItemInternal = this.GetCorrelatedItemInternal(true);
			return this.IsOutOfDate(correlatedItemInternal);
		}

		// Token: 0x06002C8B RID: 11403 RVA: 0x000B5D46 File Offset: 0x000B3F46
		public bool IsOutOfDate(CalendarItemBase correlatedCalendarItem)
		{
			this.CheckDisposed("IsOutOfDate");
			return this.IsOutOfDateInternal(correlatedCalendarItem);
		}

		// Token: 0x06002C8C RID: 11404 RVA: 0x000B5D5A File Offset: 0x000B3F5A
		public virtual string GenerateWhen(CultureInfo preferedCulture, ExTimeZone preferredTimeZone)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06002C8D RID: 11405 RVA: 0x000B5D61 File Offset: 0x000B3F61
		public string GenerateWhen(CultureInfo preferedCulture)
		{
			return this.GenerateWhen(preferedCulture, null);
		}

		// Token: 0x06002C8E RID: 11406 RVA: 0x000B5D6B File Offset: 0x000B3F6B
		public string GenerateWhen()
		{
			return this.GenerateWhen(base.Session.InternalPreferedCulture);
		}

		// Token: 0x06002C8F RID: 11407 RVA: 0x000B5D80 File Offset: 0x000B3F80
		public static void SendLocalOrRemote(MessageItem messageItem, bool copyToSentItems = true, bool ignoreSendAsRight = true)
		{
			MailboxSession mailboxSession = messageItem.Session as MailboxSession;
			if (mailboxSession != null && mailboxSession.MailboxOwner.MailboxInfo.IsAggregated && mailboxSession.ActivitySession != null)
			{
				messageItem.From = CalendarItemBase.GetAggregatedOwner(mailboxSession);
				if (messageItem.StoreObjectId == null)
				{
					messageItem.Save(SaveMode.NoConflictResolution);
					messageItem.Load();
				}
				messageItem.RemoteSend();
				return;
			}
			SubmitMessageFlags submitFlags = ignoreSendAsRight ? SubmitMessageFlags.IgnoreSendAsRight : SubmitMessageFlags.None;
			if (copyToSentItems)
			{
				messageItem.Send(submitFlags);
				return;
			}
			messageItem.SendWithoutSavingMessage(submitFlags);
		}

		// Token: 0x06002C90 RID: 11408 RVA: 0x000B5DF9 File Offset: 0x000B3FF9
		public override void SetFlag(string flagRequest, ExDateTime? startDate, ExDateTime? dueDate)
		{
			this.CheckDisposed("SetFlag");
			base.LocationIdentifierHelperInstance.SetLocationIdentifier(40565U, LastChangeAction.SetFlag);
			base.SetFlagInternal(startDate, dueDate);
		}

		// Token: 0x17000E73 RID: 3699
		public override object this[PropertyDefinition propertyDefinition]
		{
			get
			{
				return base[propertyDefinition];
			}
			set
			{
				if (InternalSchema.FlagRequest.Equals(propertyDefinition))
				{
					ExTraceGlobals.MeetingMessageTracer.TraceError<string>((long)this.GetHashCode(), "MeetingMessage::Indexer. Property cannot be set for meeting requests. {0}", propertyDefinition.Name);
					return;
				}
				base[propertyDefinition] = value;
			}
		}

		// Token: 0x17000E74 RID: 3700
		// (get) Token: 0x06002C93 RID: 11411 RVA: 0x000B5E5D File Offset: 0x000B405D
		public virtual bool IsRecurringMaster
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06002C94 RID: 11412 RVA: 0x000B5E60 File Offset: 0x000B4060
		public bool IsOrganizer()
		{
			this.CheckDisposed("IsOrganizer");
			bool valueOrDefault = base.GetValueOrDefault<bool>(InternalSchema.IsOrganizer);
			ExTraceGlobals.MeetingMessageTracer.Information<GlobalObjectId, bool>((long)this.GetHashCode(), "Storage.MeetingMessage.IsOrganizer: GOID={0}. IsOrganizer={1}", this.GlobalObjectId, valueOrDefault);
			return valueOrDefault;
		}

		// Token: 0x06002C95 RID: 11413 RVA: 0x000B5EA2 File Offset: 0x000B40A2
		public virtual CalendarItemBase PreProcess(bool createNewItem, bool processExternal, int defaultReminderMinutes)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06002C96 RID: 11414 RVA: 0x000B5EAC File Offset: 0x000B40AC
		public void MarkAsOutOfDate()
		{
			this.CheckDisposed("MarkAsOutOfDate");
			ExTraceGlobals.MeetingMessageTracer.Information((long)this.GetHashCode(), "Storage.MeetingMessage.MarkAsOutOfDate");
			if (this.IsRepairUpdateMessage || this is MeetingRequest || this is MeetingCancellation)
			{
				base.LocationIdentifierHelperInstance.SetLocationIdentifier(61045U);
				this[InternalSchema.MeetingRequestType] = MeetingMessageType.Outdated;
			}
		}

		// Token: 0x06002C97 RID: 11415 RVA: 0x000B5F18 File Offset: 0x000B4118
		public void MarkAsHijacked()
		{
			this.CheckDisposed("MarkAsHijacked");
			ExTraceGlobals.MeetingMessageTracer.Information((long)this.GetHashCode(), "Storage.MeetingMessage.MarkAsHijacked");
			this.MarkAsOutOfDate();
			if (this.IsRepairUpdateMessage || this is MeetingRequest || this is MeetingCancellation)
			{
				base.LocationIdentifierHelperInstance.SetLocationIdentifier(46108U);
				this[InternalSchema.OriginalMeetingType] = MeetingMessageType.Outdated;
				this[InternalSchema.AppointmentStateInternal] = 0;
				this[InternalSchema.HijackedMeeting] = true;
			}
		}

		// Token: 0x06002C98 RID: 11416 RVA: 0x000B5FAC File Offset: 0x000B41AC
		public bool VerifyCalendarOriginatorId(MailboxSession itemStore, CalendarItemBase calendarItem, string internetMessageId, out string participantMatchFailure)
		{
			participantMatchFailure = null;
			if (calendarItem == null)
			{
				return true;
			}
			if (!(this is MeetingRequest) && !(this is MeetingCancellation))
			{
				return true;
			}
			string valueOrDefault = base.GetValueOrDefault<string>(InternalSchema.CalendarOriginatorId);
			string valueOrDefault2 = calendarItem.GetValueOrDefault<string>(InternalSchema.CalendarOriginatorId);
			int? num = CalendarOriginatorIdProperty.Compare(valueOrDefault2, valueOrDefault);
			if (num != null)
			{
				if (num == 0)
				{
					return true;
				}
				if (num > 0)
				{
					ExTraceGlobals.MeetingMessageTracer.Information((long)this.GetHashCode(), "Calendar originator Id on the meeting request - {0} partially matches the originator Id on the calendar item - {1}. InternetMessageId = {2}, Mailbox = {3}.", new object[]
					{
						valueOrDefault,
						valueOrDefault2,
						internetMessageId,
						itemStore.MailboxOwnerLegacyDN
					});
					return true;
				}
				return false;
			}
			else
			{
				ExTraceGlobals.MeetingMessageTracer.Information((long)this.GetHashCode(), "Calendar originator Id on the meeting request - {0} does not match the originator Id on the calendar item - {1}. InternetMessageId = {2}, Mailbox = {3}. Verifying From.", new object[]
				{
					valueOrDefault,
					valueOrDefault2,
					internetMessageId,
					itemStore.MailboxOwnerLegacyDN
				});
				Participant organizer = calendarItem.Organizer;
				Participant from = base.From;
				if (!(organizer != null))
				{
					participantMatchFailure = "Organizer on the calendar item is not set.";
					return true;
				}
				if (Participant.HasSameEmail(organizer, from))
				{
					ExTraceGlobals.MeetingMessageTracer.Information((long)this.GetHashCode(), "From on the meeting request - {0} matches Organizer on the calendar item - {1}. InternetMessageId = {2}, Mailbox = {3}.", new object[]
					{
						from,
						organizer,
						internetMessageId,
						itemStore.MailboxOwnerLegacyDN
					});
					return true;
				}
				IRecipientSession tenantOrRootOrgRecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(ConsistencyMode.PartiallyConsistent, Participant.BatchBuilder.GetADSessionSettings(itemStore), 870, "VerifyCalendarOriginatorId", "f:\\15.00.1497\\sources\\dev\\data\\src\\storage\\Calendar\\MeetingMessage.cs");
				ADRecipient adrecipient = null;
				if (from.TryGetADRecipient(tenantOrRootOrgRecipientSession, out adrecipient) && adrecipient != null)
				{
					if (Participant.HasProxyAddress(adrecipient, organizer))
					{
						ExTraceGlobals.MeetingMessageTracer.Information((long)this.GetHashCode(), "HasProxyAddress succeeded - From: {0} on calendar item matches From on the meeting request - {1}. InternetMessageId = {2}, Mailbox = {3}.", new object[]
						{
							organizer,
							from,
							internetMessageId,
							itemStore.MailboxOwnerLegacyDN
						});
						return true;
					}
					participantMatchFailure = string.Format("Particpant match failure, Failed to match proxyAddresses. Calendar.From={0}, mtg.From={1}", organizer, from);
				}
				else
				{
					participantMatchFailure = string.Format("Particpant match failure, Failed to get the AD Recipient. Calendar.From={0}, mtg.From={1}", organizer, from);
				}
				return true;
			}
		}

		// Token: 0x06002C99 RID: 11417 RVA: 0x000B61B4 File Offset: 0x000B43B4
		internal void CopySendableParticipantsToMessage(IList<Attendee> attendeeList)
		{
			foreach (Attendee attendee in attendeeList)
			{
				if (attendee.IsSendable())
				{
					base.Recipients.Add(attendee.Participant, Attendee.AttendeeTypeToRecipientItemType(attendee.AttendeeType));
				}
			}
		}

		// Token: 0x06002C9A RID: 11418 RVA: 0x000B621C File Offset: 0x000B441C
		internal virtual CalendarItemBase GetCorrelatedItemInternal(bool cache)
		{
			IEnumerable<VersionedId> enumerable;
			return this.GetCorrelatedItemInternal(cache, false, out enumerable);
		}

		// Token: 0x06002C9B RID: 11419 RVA: 0x000B6233 File Offset: 0x000B4433
		internal virtual CalendarItemBase GetCorrelatedItemInternal(bool cache, bool shouldDetectDuplicateIds, out IEnumerable<VersionedId> detectedDuplicatesIds)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06002C9C RID: 11420 RVA: 0x000B623A File Offset: 0x000B443A
		protected internal virtual CalendarItemOccurrence RecoverDeletedOccurrence()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06002C9D RID: 11421 RVA: 0x000B6244 File Offset: 0x000B4444
		protected static MailboxSession GetCalendarMailboxSession(MeetingMessage meetingMessage)
		{
			if (!(meetingMessage.Session is MailboxSession))
			{
				throw new NotSupportedException();
			}
			MailboxSession mailboxSession = (MailboxSession)meetingMessage.Session;
			if (meetingMessage.IsDelegated())
			{
				Participant valueOrDefault = meetingMessage.GetValueOrDefault<Participant>(InternalSchema.ReceivedRepresenting);
				ExchangePrincipal principal = MeetingMessage.InternalGetExchangePrincipal(valueOrDefault, mailboxSession);
				mailboxSession = mailboxSession.InternalGetDelegateSessionEntry(principal, OpenBy.Internal).MailboxSession;
			}
			return mailboxSession;
		}

		// Token: 0x06002C9E RID: 11422
		protected abstract void UpdateCalendarItemInternal(ref CalendarItemBase originalCalendarItem);

		// Token: 0x06002C9F RID: 11423 RVA: 0x000B629B File Offset: 0x000B449B
		protected virtual bool IsOutOfDateInternal(CalendarItemBase correlatedCalendarItem)
		{
			return this.CompareToCalendarItem(correlatedCalendarItem) < 0;
		}

		// Token: 0x06002CA0 RID: 11424 RVA: 0x000B62A7 File Offset: 0x000B44A7
		protected internal virtual int CompareToCalendarItem(CalendarItemBase correlatedCalendarItem)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06002CA1 RID: 11425 RVA: 0x000B62AE File Offset: 0x000B44AE
		protected virtual bool CheckPreConditions(CalendarItemBase originalCalendarItem, bool shouldThrow, bool canUpdatePrincipalCalendar)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06002CA2 RID: 11426 RVA: 0x000B62B8 File Offset: 0x000B44B8
		protected CalendarItemBase GetCalendarItemToUpdate(CalendarItemBase correlatedCalendarItem)
		{
			CalendarItemBase calendarItemBase = correlatedCalendarItem;
			if (calendarItemBase == null)
			{
				MailboxSession calendarMailboxSession = MeetingMessage.GetCalendarMailboxSession(this);
				if ((calendarItemBase = this.RecoverDeletedOccurrence()) == null)
				{
					ExTraceGlobals.MeetingMessageTracer.Information<GlobalObjectId>((long)this.GetHashCode(), "Storage.MeetingMessage.GetCalendarItemToUpdate: GOID={0}. Creating calendar item", this.GlobalObjectId);
					StoreObjectId parentFolderId = calendarMailboxSession.SafeGetDefaultFolderId(DefaultFolderType.Calendar);
					calendarItemBase = (this.IsSeriesMessage ? CalendarItemSeries.CreateSeries(calendarMailboxSession, parentFolderId, false) : CalendarItem.CreateCalendarItem(calendarMailboxSession, parentFolderId, false));
				}
				else
				{
					ExTraceGlobals.MeetingMessageTracer.Information<GlobalObjectId>((long)this.GetHashCode(), "Storage.MeetingMessage.GetCalendarItemToUpdate: GOID={0}. Recovered deleted occurrence.", this.GlobalObjectId);
				}
			}
			return calendarItemBase;
		}

		// Token: 0x06002CA3 RID: 11427 RVA: 0x000B633C File Offset: 0x000B453C
		internal Attendee FindAttendee(CalendarItemBase calendarItem)
		{
			this.CheckDisposed("FindAttendee");
			IAttendeeCollection attendeeCollection = calendarItem.AttendeeCollection;
			bool flag = false;
			Attendee attendee = null;
			bool flag2 = false;
			if (this.GetFromRecipient() == null)
			{
				if (base.From == null)
				{
					return null;
				}
				flag2 = true;
			}
			for (int i = 0; i < attendeeCollection.Count; i++)
			{
				attendee = attendeeCollection[i];
				bool flag3;
				if (flag2)
				{
					flag3 = base.From.AreAddressesEqual(attendee.Participant);
				}
				else
				{
					flag3 = Participant.HasProxyAddress(this.adFromRecipient, attendee.Participant);
				}
				if (flag3)
				{
					ExTraceGlobals.MeetingMessageTracer.Information((long)this.GetHashCode(), "User found in attendee list.");
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				return null;
			}
			return attendee;
		}

		// Token: 0x06002CA4 RID: 11428 RVA: 0x000B63EB File Offset: 0x000B45EB
		protected override Reminder CreateReminderObject()
		{
			return null;
		}

		// Token: 0x06002CA5 RID: 11429 RVA: 0x000B63F0 File Offset: 0x000B45F0
		internal void AdjustAppointmentStateFlagsForForward()
		{
			base.LocationIdentifierHelperInstance.SetLocationIdentifier(55925U);
			this.AdjustAppointmentState();
			int? num = base.GetValueAsNullable<int>(InternalSchema.AppointmentAuxiliaryFlags);
			if (num != null)
			{
				num |= 4;
			}
			else
			{
				num = new int?(4);
			}
			base.LocationIdentifierHelperInstance.SetLocationIdentifier(43637U);
			this[InternalSchema.AppointmentAuxiliaryFlags] = num;
		}

		// Token: 0x06002CA6 RID: 11430 RVA: 0x000B6478 File Offset: 0x000B4678
		public bool IsMailboxOwnerTheSender()
		{
			this.CheckDisposed("IsMailboxOwnerTheSender");
			if (!(base.Session is MailboxSession))
			{
				return false;
			}
			string valueOrDefault = base.GetValueOrDefault<string>(InternalSchema.SentRepresentingEmailAddress, string.Empty);
			bool flag;
			if (valueOrDefault.Length == 0)
			{
				flag = false;
				ExTraceGlobals.MeetingMessageTracer.Information<GlobalObjectId>((long)this.GetHashCode(), "Storage.MeetingMessage.IsMailboxOwnerTheSender: GOID={0}; no sent representing email address; returning false", this.GlobalObjectId);
			}
			else
			{
				MailboxSession mailboxSession = this.MailboxSession;
				string mailboxOwnerLegacyDN = mailboxSession.MailboxOwnerLegacyDN;
				flag = Participant.HasSameEmail(Participant.Parse(mailboxOwnerLegacyDN), Participant.Parse(valueOrDefault), mailboxSession, false);
				ExTraceGlobals.MeetingMessageTracer.Information<GlobalObjectId, bool>((long)this.GetHashCode(), "Storage.MeetingMessage.IsMailboxOwnerTheSender: GOID={0}; comparing representing email address; result={1}", this.GlobalObjectId, flag);
			}
			return flag;
		}

		// Token: 0x17000E75 RID: 3701
		// (get) Token: 0x06002CA7 RID: 11431 RVA: 0x000B6518 File Offset: 0x000B4718
		public virtual GlobalObjectId GlobalObjectId
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x06002CA8 RID: 11432 RVA: 0x000B6520 File Offset: 0x000B4720
		protected virtual AppointmentStateFlags CalculatedAppointmentState()
		{
			this.CheckDisposed("CalculatedAppointmentState");
			AppointmentStateFlags appointmentStateFlags = this.AppointmentState;
			appointmentStateFlags |= AppointmentStateFlags.Meeting;
			return appointmentStateFlags | AppointmentStateFlags.Received;
		}

		// Token: 0x06002CA9 RID: 11433 RVA: 0x000B6548 File Offset: 0x000B4748
		internal void AdjustAppointmentState()
		{
			this.CheckDisposed("AdjustAppointmentState");
			this.AppointmentState = this.CalculatedAppointmentState();
		}

		// Token: 0x17000E76 RID: 3702
		// (get) Token: 0x06002CAA RID: 11434 RVA: 0x000B6561 File Offset: 0x000B4761
		// (set) Token: 0x06002CAB RID: 11435 RVA: 0x000B6579 File Offset: 0x000B4779
		protected AppointmentStateFlags AppointmentState
		{
			get
			{
				this.CheckDisposed("AppointmentState::get");
				return base.GetValueOrDefault<AppointmentStateFlags>(InternalSchema.AppointmentState);
			}
			set
			{
				this.CheckDisposed("AppointmentState::set");
				this[InternalSchema.AppointmentState] = value;
			}
		}

		// Token: 0x06002CAC RID: 11436 RVA: 0x000B6598 File Offset: 0x000B4798
		public bool SkipMessage(bool processExternal, CalendarItemBase existingCalendarItem)
		{
			this.CheckDisposed("SkipMessage");
			if (!this.IsExternalMessage)
			{
				return this.IsRepairUpdateMessage && this.ShouldBeSentFromOrganizer && this.InternalShouldSkipRUM(existingCalendarItem);
			}
			if (this.IsRepairUpdateMessage)
			{
				ExTraceGlobals.MeetingMessageTracer.Information<GlobalObjectId>((long)this.GetHashCode(), "Storage.MeetingMessage.SkipMessage: GOID={0}. Skipping external RUM.", this.GlobalObjectId);
				return true;
			}
			if (this is MeetingResponse)
			{
				return false;
			}
			if (processExternal)
			{
				return false;
			}
			ExTraceGlobals.MeetingMessageTracer.Information<GlobalObjectId>((long)this.GetHashCode(), "Storage.MeetingMessage.SkipMessage: GOID={0}. Skipping external message.", this.GlobalObjectId);
			return true;
		}

		// Token: 0x06002CAD RID: 11437 RVA: 0x000B6624 File Offset: 0x000B4824
		private bool? IsInternalMessageOrganizerRogue(IRecipientSession adSession, CalendarItemBase existingCalendarItem)
		{
			if (existingCalendarItem == null)
			{
				return new bool?(false);
			}
			if (existingCalendarItem.IsOrganizerExternal)
			{
				return this.AreOrganizersDifferent(base.From, existingCalendarItem.Organizer, adSession);
			}
			Participant organizer = existingCalendarItem.Organizer;
			if (Participant.HasSameEmail(organizer, base.From, this.MailboxSession, true))
			{
				return new bool?(false);
			}
			return this.AreOrganizersDifferent(organizer, base.From, adSession);
		}

		// Token: 0x06002CAE RID: 11438 RVA: 0x000B6688 File Offset: 0x000B4888
		private bool? AreOrganizersDifferent(Participant internalOrganizer, Participant otherOrganizer, IRecipientSession adSession)
		{
			ADRecipient adrecipient = null;
			if (!internalOrganizer.TryGetADRecipient(adSession, out adrecipient) || adrecipient == null)
			{
				ExTraceGlobals.MeetingMessageTracer.Information<GlobalObjectId, Participant>((long)this.GetHashCode(), "Storage.MeetingMessage.AreOrganizersDifferent: GOID={0}. Unable to get the ADRecipient for the organizer ({1}).", this.GlobalObjectId, internalOrganizer);
				return null;
			}
			Participant participant = new Participant(adrecipient);
			return new bool?(!Participant.HasSameEmail(participant, otherOrganizer, this.MailboxSession, true));
		}

		// Token: 0x17000E77 RID: 3703
		// (get) Token: 0x06002CAF RID: 11439
		protected abstract bool ShouldBeSentFromOrganizer { get; }

		// Token: 0x06002CB0 RID: 11440 RVA: 0x000B66E9 File Offset: 0x000B48E9
		protected void CheckPreConditionForDelegatedMeeting(bool canUpdatePrincipalCalendar)
		{
			if (this.IsDelegated() && !canUpdatePrincipalCalendar)
			{
				throw new InvalidOperationException("Cannot process delegate meeting message if canUpdatePrincipalCalendar has been specified to false.");
			}
		}

		// Token: 0x06002CB1 RID: 11441 RVA: 0x000B6701 File Offset: 0x000B4901
		public virtual void SetCalendarProcessingSteps(CalendarProcessingSteps stepComplete)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06002CB2 RID: 11442 RVA: 0x000B6708 File Offset: 0x000B4908
		protected override void OnBeforeSave()
		{
			base.OnBeforeSave();
			this.OnBeforeSaveUpdateLastChangeAction();
		}

		// Token: 0x06002CB3 RID: 11443 RVA: 0x000B6718 File Offset: 0x000B4918
		private void OnBeforeSaveUpdateLastChangeAction()
		{
			if (base.PropertyBag.IsDirty)
			{
				base.LocationIdentifierHelperInstance.SetLocationIdentifier(51829U);
				this[InternalSchema.OutlookVersion] = StorageGlobals.ExchangeVersion;
				this[InternalSchema.OutlookInternalVersion] = (int)StorageGlobals.BuildVersion;
			}
		}

		// Token: 0x06002CB4 RID: 11444 RVA: 0x000B6768 File Offset: 0x000B4968
		private ADRecipient GetFromRecipient()
		{
			if (this.adFromRecipient == null)
			{
				Participant from = base.From;
				if (from == null)
				{
					ExTraceGlobals.MeetingMessageTracer.Information((long)this.GetHashCode(), "The meeting message response doesn't have a From");
					return null;
				}
				ADSessionSettings adsessionSettings = Participant.BatchBuilder.GetADSessionSettings(this.MailboxSession);
				IRecipientSession tenantOrRootOrgRecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(ConsistencyMode.PartiallyConsistent, adsessionSettings, 1529, "GetFromRecipient", "f:\\15.00.1497\\sources\\dev\\data\\src\\storage\\Calendar\\MeetingMessage.cs");
				if (!from.TryGetADRecipient(tenantOrRootOrgRecipientSession, out this.adFromRecipient) || this.adFromRecipient == null)
				{
					ExTraceGlobals.MeetingMessageTracer.Information<Participant>((long)this.GetHashCode(), "Failed to get the AD recipient for user {0}.", from);
					return null;
				}
			}
			return this.adFromRecipient;
		}

		// Token: 0x06002CB5 RID: 11445 RVA: 0x000B6806 File Offset: 0x000B4A06
		internal virtual void Initialize()
		{
			this.CheckDisposed("Initialize");
			base.LocationIdentifierHelperInstance.SetLocationIdentifier(62069U, LastChangeAction.Create);
		}

		// Token: 0x06002CB6 RID: 11446 RVA: 0x000B6824 File Offset: 0x000B4A24
		private static ExchangePrincipal InternalGetExchangePrincipal(Participant principal, MailboxSession calendarMailboxSession)
		{
			ExchangePrincipal result;
			if (string.Compare(principal.RoutingType, "EX", StringComparison.OrdinalIgnoreCase) == 0)
			{
				result = ExchangePrincipal.FromLegacyDN(calendarMailboxSession.GetADSessionSettings(), principal.EmailAddress, RemotingOptions.AllowCrossSite);
			}
			else
			{
				result = ExchangePrincipal.FromProxyAddress(calendarMailboxSession.GetADSessionSettings(), principal.EmailAddress, RemotingOptions.AllowCrossSite);
			}
			return result;
		}

		// Token: 0x06002CB7 RID: 11447 RVA: 0x000B6870 File Offset: 0x000B4A70
		private bool InternalShouldSkipRUM(CalendarItemBase existingCalendarItem)
		{
			IRecipientSession adrecipientSession = base.Session.GetADRecipientSession(true, ConsistencyMode.PartiallyConsistent);
			if (adrecipientSession == null)
			{
				ExTraceGlobals.MeetingMessageTracer.TraceDebug<GlobalObjectId>((long)this.GetHashCode(), "Storage.MeetingMessage.SkipMessage: GOID={0}. Skipping the message since there's no session provided for the required AD lookup.", this.GlobalObjectId);
				return true;
			}
			bool? flag = this.IsInternalMessageOrganizerRogue(adrecipientSession, existingCalendarItem);
			return flag == null || flag.Value;
		}

		// Token: 0x040018DC RID: 6364
		internal static readonly StorePropertyDefinition[] PreservableProperties = PreservableMeetingMessageProperty.PreservablePropertyDefinitions.ToArray<StorePropertyDefinition>();

		// Token: 0x040018DD RID: 6365
		internal static readonly StorePropertyDefinition[] MeetingMessageProperties = CalendarItemProperties.NonPreservableMeetingMessageProperties.Concat(MeetingMessage.PreservableProperties).ToArray<StorePropertyDefinition>();

		// Token: 0x040018DE RID: 6366
		internal static readonly StorePropertyDefinition[] DisplayTimeZoneProperties = new StorePropertyDefinition[]
		{
			InternalSchema.TimeZoneDefinitionStart,
			InternalSchema.TimeZoneDefinitionEnd,
			InternalSchema.TimeZoneDefinitionRecurring
		};

		// Token: 0x040018DF RID: 6367
		internal static readonly StorePropertyDefinition[] WriteOnCreateProperties = new StorePropertyDefinition[]
		{
			InternalSchema.MapiSensitivity,
			InternalSchema.Categories,
			InternalSchema.ReminderMinutesBeforeStartInternal,
			InternalSchema.ReminderIsSetInternal,
			InternalSchema.ReminderNextTime,
			InternalSchema.AcceptLanguage
		};

		// Token: 0x040018E0 RID: 6368
		internal static readonly StorePropertyDefinition[] WriteOnCreateSeriesProperties = new StorePropertyDefinition[]
		{
			InternalSchema.SeriesReminderIsSet
		};

		// Token: 0x040018E1 RID: 6369
		private ADRecipient adFromRecipient;
	}
}
