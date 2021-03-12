using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Data.Storage.StoreConfigurableType;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020003A3 RID: 931
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class CalendarItemBase : Item, ICalendarItemBase, IItem, IStoreObject, IStorePropertyBag, IPropertyBag, IReadOnlyPropertyBag, IDisposable
	{
		// Token: 0x0600294F RID: 10575 RVA: 0x000A4230 File Offset: 0x000A2430
		internal static int ConvertTimeToOwnerId(ExDateTime time)
		{
			int num = 0;
			long num2 = time.UtcTicks;
			if (num2 > 300000000L)
			{
				num2 -= 300000000L;
			}
			else
			{
				num2 = 0L;
			}
			num |= (time.Year & 4095) << 20;
			num |= (time.Month & 15) << 16;
			num |= (time.Day & 31) << 11;
			return num | (int)(num2 & 2047L);
		}

		// Token: 0x06002950 RID: 10576 RVA: 0x000A42A0 File Offset: 0x000A24A0
		internal static void CoreObjectUpdateLocationAddress(CoreItem coreItem)
		{
			PersistablePropertyBag persistablePropertyBag = Microsoft.Exchange.Data.Storage.CoreObject.GetPersistablePropertyBag(coreItem);
			InternalSchema.LocationAddress.UpdateCompositePropertyValue(persistablePropertyBag);
		}

		// Token: 0x06002951 RID: 10577 RVA: 0x000A42BF File Offset: 0x000A24BF
		internal CalendarItemBase(ICoreItem coreItem) : base(coreItem, false)
		{
			this.CreateCacheForChangeHighlight();
		}

		// Token: 0x06002952 RID: 10578 RVA: 0x000A42D0 File Offset: 0x000A24D0
		internal virtual void Initialize(bool newItem)
		{
			this.CheckDisposed("Initialize");
			base.LocationIdentifierHelperInstance.SetLocationIdentifier(46741U, LastChangeAction.Create);
			this.AppointmentSequenceNumber = 0;
			this[InternalSchema.MeetingRequestWasSent] = false;
			this[InternalSchema.IsAllDayEvent] = false;
			this[InternalSchema.FreeBusyStatus] = BusyType.Busy;
			this[InternalSchema.IntendedFreeBusyStatus] = BusyType.Unknown;
			this[InternalSchema.AppointmentState] = 0;
			if (newItem)
			{
				MailboxSession mailboxSession = base.Session as MailboxSession;
				PublicFolderSession publicFolderSession = base.Session as PublicFolderSession;
				if (mailboxSession != null)
				{
					if (mailboxSession.MailboxOwner.MailboxInfo.IsAggregated)
					{
						this.Organizer = CalendarItemBase.GetAggregatedOwner(mailboxSession);
					}
					else
					{
						this.Organizer = new Participant(mailboxSession.MailboxOwner);
					}
					string value = null;
					if (CalendarOriginatorIdProperty.TryCreate(mailboxSession, out value))
					{
						this[InternalSchema.CalendarOriginatorId] = value;
					}
				}
				else if (publicFolderSession != null && publicFolderSession.LogonType == LogonType.Delegated)
				{
					this.Organizer = publicFolderSession.ConnectAsParticipant;
				}
				ExDateTime now = ExDateTime.GetNow(base.PropertyBag.ExTimeZone);
				this.OwnerAppointmentId = new int?(CalendarItemBase.ConvertTimeToOwnerId(now));
				base.LocationIdentifierHelperInstance.SetLocationIdentifier(35477U);
				this[InternalSchema.Flags] = MessageFlags.IsRead;
				base.Reminder.MinutesBeforeStart = 15;
				base.Reminder.IsSet = true;
				this.ResponseType = ResponseType.Organizer;
				GlobalObjectId globalObjectId = new GlobalObjectId();
				this[InternalSchema.GlobalObjectId] = globalObjectId.Bytes;
				this.CleanGlobalObjectId = globalObjectId.Bytes;
				return;
			}
			base.LocationIdentifierHelperInstance.SetLocationIdentifier(33429U);
			this[InternalSchema.IsDraft] = false;
		}

		// Token: 0x06002953 RID: 10579 RVA: 0x000A4484 File Offset: 0x000A2684
		internal static Participant GetAggregatedOwner(MailboxSession mailboxSession)
		{
			using (UserConfigurationDictionaryAdapter<AggregatedAccountConfiguration> userConfigurationDictionaryAdapter = new UserConfigurationDictionaryAdapter<AggregatedAccountConfiguration>(mailboxSession, "AggregatedAccount", new GetUserConfigurationDelegate(UserConfigurationHelper.GetMailboxConfiguration), new SimplePropertyDefinition[]
			{
				AggregatedAccountConfigurationSchema.EmailAddressRaw
			}))
			{
				AggregatedAccountConfiguration aggregatedAccountConfiguration = userConfigurationDictionaryAdapter.Read(mailboxSession.MailboxOwner);
				if (aggregatedAccountConfiguration.EmailAddress != null)
				{
					string text = aggregatedAccountConfiguration.EmailAddress.Value.ToString();
					return new Participant(text, text, "SMTP");
				}
			}
			return new Participant(mailboxSession.MailboxOwner);
		}

		// Token: 0x06002954 RID: 10580 RVA: 0x000A4534 File Offset: 0x000A2734
		public new static CalendarItemBase Bind(StoreSession session, StoreId storeId)
		{
			return CalendarItemBase.Bind(session, storeId, null);
		}

		// Token: 0x06002955 RID: 10581 RVA: 0x000A453E File Offset: 0x000A273E
		public new static CalendarItemBase Bind(StoreSession session, StoreId storeId, params PropertyDefinition[] propsToReturn)
		{
			return CalendarItemBase.Bind(session, storeId, (ICollection<PropertyDefinition>)propsToReturn);
		}

		// Token: 0x06002956 RID: 10582 RVA: 0x000A454D File Offset: 0x000A274D
		public new static CalendarItemBase Bind(StoreSession session, StoreId storeId, ICollection<PropertyDefinition> propsToReturn)
		{
			return ItemBuilder.ItemBind<CalendarItemBase>(session, storeId, CalendarItemBaseSchema.Instance, propsToReturn);
		}

		// Token: 0x17000D93 RID: 3475
		public override object this[PropertyDefinition propertyDefinition]
		{
			get
			{
				return base[propertyDefinition];
			}
			set
			{
				base[propertyDefinition] = value;
			}
		}

		// Token: 0x17000D94 RID: 3476
		// (get) Token: 0x06002959 RID: 10585 RVA: 0x000A456F File Offset: 0x000A276F
		// (set) Token: 0x0600295A RID: 10586 RVA: 0x000A458C File Offset: 0x000A278C
		public ExDateTime AppointmentReplyTime
		{
			get
			{
				this.CheckDisposed("AppointmentReplyTime::get");
				return base.GetValueOrDefault<ExDateTime>(CalendarItemBaseSchema.AppointmentReplyTime, ExDateTime.MinValue);
			}
			internal set
			{
				this.CheckDisposed("AppointmentReplyTime::set");
				base.LocationIdentifierHelperInstance.SetLocationIdentifier(54645U);
				this[CalendarItemBaseSchema.AppointmentReplyTime] = value;
			}
		}

		// Token: 0x17000D95 RID: 3477
		// (get) Token: 0x0600295B RID: 10587 RVA: 0x000A45BA File Offset: 0x000A27BA
		// (set) Token: 0x0600295C RID: 10588 RVA: 0x000A45D2 File Offset: 0x000A27D2
		public string AppointmentReplyName
		{
			get
			{
				this.CheckDisposed("AppointmentReplyName::get");
				return base.GetValueOrDefault<string>(CalendarItemBaseSchema.AppointmentReplyName);
			}
			private set
			{
				this.CheckDisposed("AppointmentReplyName::set");
				this[CalendarItemBaseSchema.AppointmentReplyName] = value;
			}
		}

		// Token: 0x17000D96 RID: 3478
		// (get) Token: 0x0600295D RID: 10589 RVA: 0x000A45EB File Offset: 0x000A27EB
		// (set) Token: 0x0600295E RID: 10590 RVA: 0x000A4603 File Offset: 0x000A2803
		public int? OwnerAppointmentId
		{
			get
			{
				this.CheckDisposed("OwnerAppointmentId::get");
				return base.GetValueAsNullable<int>(CalendarItemBaseSchema.OwnerAppointmentID);
			}
			private set
			{
				this.CheckDisposed("OwnerAppointmentId::set");
				base.LocationIdentifierHelperInstance.SetLocationIdentifier(60053U);
				this[CalendarItemBaseSchema.OwnerAppointmentID] = value;
			}
		}

		// Token: 0x17000D97 RID: 3479
		// (get) Token: 0x0600295F RID: 10591 RVA: 0x000A4631 File Offset: 0x000A2831
		// (set) Token: 0x06002960 RID: 10592 RVA: 0x000A464A File Offset: 0x000A284A
		public byte[] CleanGlobalObjectId
		{
			get
			{
				this.CheckDisposed("CleanGlobalObjectId::get");
				return base.GetValueOrDefault<byte[]>(CalendarItemBaseSchema.CleanGlobalObjectId, null);
			}
			private set
			{
				this.CheckDisposed("CleanGlobalObjectId::set");
				base.LocationIdentifierHelperInstance.SetLocationIdentifier(58005U);
				this[CalendarItemBaseSchema.CleanGlobalObjectId] = value;
			}
		}

		// Token: 0x17000D98 RID: 3480
		// (get) Token: 0x06002961 RID: 10593
		// (set) Token: 0x06002962 RID: 10594
		public abstract int AppointmentLastSequenceNumber { get; set; }

		// Token: 0x17000D99 RID: 3481
		// (get) Token: 0x06002963 RID: 10595 RVA: 0x000A4673 File Offset: 0x000A2873
		// (set) Token: 0x06002964 RID: 10596 RVA: 0x000A468B File Offset: 0x000A288B
		public int AppointmentSequenceNumber
		{
			get
			{
				this.CheckDisposed("AppointmentSequenceNumber::get");
				return base.GetValueOrDefault<int>(CalendarItemBaseSchema.AppointmentSequenceNumber);
			}
			protected set
			{
				this.CheckDisposed("AppointmentSequenceNumber::set");
				base.LocationIdentifierHelperInstance.SetLocationIdentifier(45429U);
				this[CalendarItemBaseSchema.AppointmentSequenceNumber] = value;
			}
		}

		// Token: 0x17000D9A RID: 3482
		// (get) Token: 0x06002965 RID: 10597 RVA: 0x000A46B9 File Offset: 0x000A28B9
		// (set) Token: 0x06002966 RID: 10598 RVA: 0x000A46D6 File Offset: 0x000A28D6
		public ExDateTime OwnerCriticalChangeTime
		{
			get
			{
				this.CheckDisposed("OwnerCriticalChangeTime::get");
				return base.GetValueOrDefault<ExDateTime>(CalendarItemBaseSchema.OwnerCriticalChangeTime, ExDateTime.MinValue);
			}
			protected set
			{
				this.CheckDisposed("OwnerCriticalChangeTime::set");
				base.LocationIdentifierHelperInstance.SetLocationIdentifier(35189U);
				this[CalendarItemBaseSchema.OwnerCriticalChangeTime] = value;
			}
		}

		// Token: 0x17000D9B RID: 3483
		// (get) Token: 0x06002967 RID: 10599 RVA: 0x000A4704 File Offset: 0x000A2904
		public ExDateTime AttendeeCriticalChangeTime
		{
			get
			{
				this.CheckDisposed("AttendeeCriticalChangeTime::get");
				return base.GetValueOrDefault<ExDateTime>(CalendarItemBaseSchema.AttendeeCriticalChangeTime, ExDateTime.MinValue);
			}
		}

		// Token: 0x17000D9C RID: 3484
		// (get) Token: 0x06002968 RID: 10600 RVA: 0x000A4721 File Offset: 0x000A2921
		// (set) Token: 0x06002969 RID: 10601 RVA: 0x000A4729 File Offset: 0x000A2929
		public override string Subject
		{
			get
			{
				return base.Subject;
			}
			set
			{
				base.Subject = value;
				base.LocationIdentifierHelperInstance.SetLocationIdentifier(49813U);
			}
		}

		// Token: 0x17000D9D RID: 3485
		// (get) Token: 0x0600296A RID: 10602
		// (set) Token: 0x0600296B RID: 10603
		public abstract ExDateTime StartTime { get; set; }

		// Token: 0x17000D9E RID: 3486
		// (get) Token: 0x0600296C RID: 10604
		public abstract ExDateTime StartWallClock { get; }

		// Token: 0x17000D9F RID: 3487
		// (get) Token: 0x0600296D RID: 10605
		public abstract ExDateTime EndWallClock { get; }

		// Token: 0x17000DA0 RID: 3488
		// (get) Token: 0x0600296E RID: 10606
		// (set) Token: 0x0600296F RID: 10607
		public abstract ExDateTime EndTime { get; set; }

		// Token: 0x17000DA1 RID: 3489
		// (get) Token: 0x06002970 RID: 10608 RVA: 0x000A4744 File Offset: 0x000A2944
		// (set) Token: 0x06002971 RID: 10609 RVA: 0x000A4782 File Offset: 0x000A2982
		public ExTimeZone StartTimeZone
		{
			get
			{
				this.CheckDisposed("StartTimeZone::get");
				base.Load(new PropertyDefinition[]
				{
					CalendarItemBaseSchema.StartTimeZone
				});
				return base.TryGetProperty(CalendarItemBaseSchema.StartTimeZone) as ExTimeZone;
			}
			set
			{
				this.CheckDisposed("StartTimeZone::set");
				base.LocationIdentifierHelperInstance.SetLocationIdentifier(40949U);
				this[CalendarItemBaseSchema.StartTimeZone] = value;
			}
		}

		// Token: 0x17000DA2 RID: 3490
		// (get) Token: 0x06002972 RID: 10610 RVA: 0x000A47AC File Offset: 0x000A29AC
		// (set) Token: 0x06002973 RID: 10611 RVA: 0x000A47EA File Offset: 0x000A29EA
		public ExTimeZone EndTimeZone
		{
			get
			{
				this.CheckDisposed("EndTimeZone::get");
				base.Load(new PropertyDefinition[]
				{
					CalendarItemBaseSchema.EndTimeZone
				});
				return base.TryGetProperty(CalendarItemBaseSchema.EndTimeZone) as ExTimeZone;
			}
			set
			{
				this.CheckDisposed("EndTimeZone::set");
				base.LocationIdentifierHelperInstance.SetLocationIdentifier(45045U);
				this[CalendarItemBaseSchema.EndTimeZone] = value;
			}
		}

		// Token: 0x17000DA3 RID: 3491
		// (get) Token: 0x06002974 RID: 10612 RVA: 0x000A4813 File Offset: 0x000A2A13
		// (set) Token: 0x06002975 RID: 10613 RVA: 0x000A482B File Offset: 0x000A2A2B
		public bool IsAllDayEvent
		{
			get
			{
				this.CheckDisposed("IsAllDayEvent::get");
				return base.GetValueOrDefault<bool>(InternalSchema.IsAllDayEvent);
			}
			set
			{
				this.CheckDisposed("IsAllDayEvent::set");
				base.LocationIdentifierHelperInstance.SetLocationIdentifier(40309U);
				this[InternalSchema.IsAllDayEvent] = value;
			}
		}

		// Token: 0x17000DA4 RID: 3492
		// (get) Token: 0x06002976 RID: 10614 RVA: 0x000A4859 File Offset: 0x000A2A59
		public bool AllowNewTimeProposal
		{
			get
			{
				this.CheckDisposed("AllowNewTimeProposal::get");
				return !base.GetValueOrDefault<bool>(InternalSchema.DisallowNewTimeProposal);
			}
		}

		// Token: 0x17000DA5 RID: 3493
		// (get) Token: 0x06002977 RID: 10615 RVA: 0x000A4874 File Offset: 0x000A2A74
		public bool IsEvent
		{
			get
			{
				this.CheckDisposed("IsEvent::get");
				return base.GetValueOrDefault<bool>(InternalSchema.IsEvent);
			}
		}

		// Token: 0x06002978 RID: 10616 RVA: 0x000A488C File Offset: 0x000A2A8C
		public bool IsOrganizer()
		{
			this.CheckDisposed("IsOrganizer");
			return base.GetValueOrDefault<bool>(InternalSchema.IsOrganizer);
		}

		// Token: 0x17000DA6 RID: 3494
		// (get) Token: 0x06002979 RID: 10617 RVA: 0x000A48A4 File Offset: 0x000A2AA4
		public CalendarItemType CalendarItemType
		{
			get
			{
				this.CheckDisposed("CalendarItemType::get");
				return Microsoft.Exchange.Data.Storage.PropertyBag.CheckPropertyValue<CalendarItemType>(InternalSchema.CalendarItemType, base.TryGetProperty(InternalSchema.CalendarItemType), CalendarItemType.Single);
			}
		}

		// Token: 0x17000DA7 RID: 3495
		// (get) Token: 0x0600297A RID: 10618 RVA: 0x000A48C8 File Offset: 0x000A2AC8
		public bool IsCalendarItemTypeOccurrenceOrException
		{
			get
			{
				CalendarItemType calendarItemType = this.CalendarItemType;
				return calendarItemType == CalendarItemType.Occurrence || calendarItemType == CalendarItemType.Exception;
			}
		}

		// Token: 0x17000DA8 RID: 3496
		// (get) Token: 0x0600297B RID: 10619 RVA: 0x000A48E6 File Offset: 0x000A2AE6
		// (set) Token: 0x0600297C RID: 10620 RVA: 0x000A48FF File Offset: 0x000A2AFF
		public ResponseType ResponseType
		{
			get
			{
				this.CheckDisposed("ResponseType::get");
				return base.GetValueOrDefault<ResponseType>(CalendarItemBaseSchema.ResponseType, ResponseType.NotResponded);
			}
			set
			{
				this.CheckDisposed("ResponseType::set");
				EnumValidator.ThrowIfInvalid<ResponseType>(value, "value");
				base.LocationIdentifierHelperInstance.SetLocationIdentifier(56693U);
				this[CalendarItemBaseSchema.ResponseType] = value;
			}
		}

		// Token: 0x17000DA9 RID: 3497
		// (get) Token: 0x0600297D RID: 10621 RVA: 0x000A4938 File Offset: 0x000A2B38
		public IAttendeeCollection AttendeeCollection
		{
			get
			{
				return this.FetchAttendeeCollection(true);
			}
		}

		// Token: 0x17000DAA RID: 3498
		// (get) Token: 0x0600297E RID: 10622 RVA: 0x000A4941 File Offset: 0x000A2B41
		public bool AttendeesChanged
		{
			get
			{
				this.CheckDisposed("AttendeesChanged::get");
				this.CalculateAttendeeDiff();
				return (this.addedAttendeeArray != null && this.addedAttendeeArray.Length > 0) || (this.removedAttendeeArray != null && this.removedAttendeeArray.Length > 0);
			}
		}

		// Token: 0x17000DAB RID: 3499
		// (get) Token: 0x0600297F RID: 10623 RVA: 0x000A497E File Offset: 0x000A2B7E
		// (set) Token: 0x06002980 RID: 10624 RVA: 0x000A49A5 File Offset: 0x000A2BA5
		public Reminders<EventTimeBasedInboxReminder> EventTimeBasedInboxReminders
		{
			get
			{
				this.CheckDisposed("EventTimeBasedInboxReminders::get");
				if (this.eventTimeBasedInboxReminders == null)
				{
					this.eventTimeBasedInboxReminders = this.FetchEventTimeBasedInboxReminders();
				}
				return this.eventTimeBasedInboxReminders;
			}
			set
			{
				this.CheckDisposed("EventTimeBasedInboxReminders::set");
				this.UpdateEventTimeBasedInboxRemindersForSave(value);
				Reminders<EventTimeBasedInboxReminder>.Set(this, CalendarItemBaseSchema.EventTimeBasedInboxReminders, value);
				this.eventTimeBasedInboxReminders = value;
			}
		}

		// Token: 0x17000DAC RID: 3500
		// (get) Token: 0x06002981 RID: 10625 RVA: 0x000A49CC File Offset: 0x000A2BCC
		// (set) Token: 0x06002982 RID: 10626 RVA: 0x000A49E5 File Offset: 0x000A2BE5
		public BusyType FreeBusyStatus
		{
			get
			{
				this.CheckDisposed("FreeBusyStatus::get");
				return base.GetValueOrDefault<BusyType>(InternalSchema.FreeBusyStatus, BusyType.Busy);
			}
			set
			{
				this.CheckDisposed("FreeBusyStatus::set");
				EnumValidator.ThrowIfInvalid<BusyType>(value, "value");
				base.LocationIdentifierHelperInstance.SetLocationIdentifier(44405U);
				this[InternalSchema.FreeBusyStatus] = value;
			}
		}

		// Token: 0x17000DAD RID: 3501
		// (get) Token: 0x06002983 RID: 10627 RVA: 0x000A4A1E File Offset: 0x000A2C1E
		// (set) Token: 0x06002984 RID: 10628 RVA: 0x000A4A3B File Offset: 0x000A2C3B
		public string SeriesId
		{
			get
			{
				this.CheckDisposed("SeriesId::get");
				return base.GetValueOrDefault<string>(InternalSchema.SeriesId, string.Empty);
			}
			set
			{
				this.CheckDisposed("SeriesId::set");
				this[InternalSchema.SeriesId] = value;
			}
		}

		// Token: 0x17000DAE RID: 3502
		// (get) Token: 0x06002985 RID: 10629 RVA: 0x000A4A54 File Offset: 0x000A2C54
		// (set) Token: 0x06002986 RID: 10630 RVA: 0x000A4A71 File Offset: 0x000A2C71
		public string ClientId
		{
			get
			{
				this.CheckDisposed("ClientId::get");
				return base.GetValueOrDefault<string>(InternalSchema.EventClientId, string.Empty);
			}
			set
			{
				this.CheckDisposed("ClientId::set");
				base.LocationIdentifierHelperInstance.SetLocationIdentifier(63612U);
				this[InternalSchema.EventClientId] = value;
			}
		}

		// Token: 0x17000DAF RID: 3503
		// (get) Token: 0x06002987 RID: 10631 RVA: 0x000A4A9A File Offset: 0x000A2C9A
		// (set) Token: 0x06002988 RID: 10632 RVA: 0x000A4AB3 File Offset: 0x000A2CB3
		public bool IsHiddenFromLegacyClients
		{
			get
			{
				this.CheckDisposed("IsHiddenFromLegacyClients::get");
				return base.GetValueOrDefault<bool>(CalendarItemBaseSchema.IsHiddenFromLegacyClients, false);
			}
			set
			{
				this.CheckDisposed("IsHiddenFromLegacyClients::set");
				this[CalendarItemBaseSchema.IsHiddenFromLegacyClients] = value;
			}
		}

		// Token: 0x17000DB0 RID: 3504
		// (get) Token: 0x06002989 RID: 10633 RVA: 0x000A4AD1 File Offset: 0x000A2CD1
		// (set) Token: 0x0600298A RID: 10634 RVA: 0x000A4AEE File Offset: 0x000A2CEE
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
				base.LocationIdentifierHelperInstance.SetLocationIdentifier(60789U);
				this[InternalSchema.Location] = value;
			}
		}

		// Token: 0x17000DB1 RID: 3505
		// (get) Token: 0x0600298B RID: 10635 RVA: 0x000A4B17 File Offset: 0x000A2D17
		// (set) Token: 0x0600298C RID: 10636 RVA: 0x000A4B30 File Offset: 0x000A2D30
		public string LocationDisplayName
		{
			get
			{
				this.CheckDisposed("LocationDisplayName::get");
				return base.GetValueOrDefault<string>(InternalSchema.LocationDisplayName, null);
			}
			set
			{
				this.CheckDisposed("LocationDisplayName::set");
				base.LocationIdentifierHelperInstance.SetLocationIdentifier(57640U);
				base.SetOrDeleteProperty(InternalSchema.LocationDisplayName, value);
			}
		}

		// Token: 0x17000DB2 RID: 3506
		// (get) Token: 0x0600298D RID: 10637 RVA: 0x000A4B59 File Offset: 0x000A2D59
		// (set) Token: 0x0600298E RID: 10638 RVA: 0x000A4B72 File Offset: 0x000A2D72
		public string LocationAnnotation
		{
			get
			{
				this.CheckDisposed("LocationAnnotation::get");
				return base.GetValueOrDefault<string>(InternalSchema.LocationAnnotation, null);
			}
			set
			{
				this.CheckDisposed("LocationAnnotation::set");
				base.LocationIdentifierHelperInstance.SetLocationIdentifier(40464U);
				base.SetOrDeleteProperty(InternalSchema.LocationAnnotation, value);
			}
		}

		// Token: 0x17000DB3 RID: 3507
		// (get) Token: 0x0600298F RID: 10639 RVA: 0x000A4B9B File Offset: 0x000A2D9B
		// (set) Token: 0x06002990 RID: 10640 RVA: 0x000A4BB4 File Offset: 0x000A2DB4
		public LocationSource LocationSource
		{
			get
			{
				this.CheckDisposed("LocationSource::get");
				return (LocationSource)base.GetValueOrDefault<int>(InternalSchema.LocationSource, 0);
			}
			set
			{
				this.CheckDisposed("LocationSource::set");
				EnumValidator.ThrowIfInvalid<LocationSource>(value, "LocationSource");
				base.LocationIdentifierHelperInstance.SetLocationIdentifier(33064U);
				this[InternalSchema.LocationSource] = value;
			}
		}

		// Token: 0x17000DB4 RID: 3508
		// (get) Token: 0x06002991 RID: 10641 RVA: 0x000A4BED File Offset: 0x000A2DED
		// (set) Token: 0x06002992 RID: 10642 RVA: 0x000A4C06 File Offset: 0x000A2E06
		public string LocationUri
		{
			get
			{
				this.CheckDisposed("LocationUri::get");
				return base.GetValueOrDefault<string>(InternalSchema.LocationUri, null);
			}
			set
			{
				this.CheckDisposed("LocationUri::set");
				base.LocationIdentifierHelperInstance.SetLocationIdentifier(49448U);
				base.SetOrDeleteProperty(InternalSchema.LocationUri, value);
			}
		}

		// Token: 0x17000DB5 RID: 3509
		// (get) Token: 0x06002993 RID: 10643 RVA: 0x000A4C30 File Offset: 0x000A2E30
		// (set) Token: 0x06002994 RID: 10644 RVA: 0x000A4C5C File Offset: 0x000A2E5C
		public double? Latitude
		{
			get
			{
				this.CheckDisposed("Latitude::get");
				return base.GetValueOrDefault<double?>(InternalSchema.Latitude, null);
			}
			set
			{
				this.CheckDisposed("Latitude::set");
				if (value.Equals(base.GetValueAsNullable<double>(InternalSchema.Latitude)))
				{
					return;
				}
				base.LocationIdentifierHelperInstance.SetLocationIdentifier(48936U);
				base.SetOrDeleteProperty(InternalSchema.Latitude, value);
			}
		}

		// Token: 0x17000DB6 RID: 3510
		// (get) Token: 0x06002995 RID: 10645 RVA: 0x000A4CB8 File Offset: 0x000A2EB8
		// (set) Token: 0x06002996 RID: 10646 RVA: 0x000A4CE4 File Offset: 0x000A2EE4
		public double? Longitude
		{
			get
			{
				this.CheckDisposed("Longitude::get");
				return base.GetValueOrDefault<double?>(InternalSchema.Longitude, null);
			}
			set
			{
				this.CheckDisposed("Longitude::set");
				if (value.Equals(base.GetValueAsNullable<double>(InternalSchema.Longitude)))
				{
					return;
				}
				base.LocationIdentifierHelperInstance.SetLocationIdentifier(65320U);
				base.SetOrDeleteProperty(InternalSchema.Longitude, value);
			}
		}

		// Token: 0x17000DB7 RID: 3511
		// (get) Token: 0x06002997 RID: 10647 RVA: 0x000A4D40 File Offset: 0x000A2F40
		// (set) Token: 0x06002998 RID: 10648 RVA: 0x000A4D6C File Offset: 0x000A2F6C
		public double? Accuracy
		{
			get
			{
				this.CheckDisposed("Accuracy::get");
				return base.GetValueOrDefault<double?>(InternalSchema.Accuracy, null);
			}
			set
			{
				this.CheckDisposed("Accuracy::set");
				if (value.Equals(base.GetValueAsNullable<double>(InternalSchema.Accuracy)))
				{
					return;
				}
				base.LocationIdentifierHelperInstance.SetLocationIdentifier(40744U);
				base.SetOrDeleteProperty(InternalSchema.Accuracy, value);
			}
		}

		// Token: 0x17000DB8 RID: 3512
		// (get) Token: 0x06002999 RID: 10649 RVA: 0x000A4DC8 File Offset: 0x000A2FC8
		// (set) Token: 0x0600299A RID: 10650 RVA: 0x000A4DF4 File Offset: 0x000A2FF4
		public double? Altitude
		{
			get
			{
				this.CheckDisposed("Altitude::get");
				return base.GetValueOrDefault<double?>(InternalSchema.Altitude, null);
			}
			set
			{
				this.CheckDisposed("Altitude::set");
				if (value.Equals(base.GetValueAsNullable<double>(InternalSchema.Altitude)))
				{
					return;
				}
				base.LocationIdentifierHelperInstance.SetLocationIdentifier(57128U);
				base.SetOrDeleteProperty(InternalSchema.Altitude, value);
			}
		}

		// Token: 0x17000DB9 RID: 3513
		// (get) Token: 0x0600299B RID: 10651 RVA: 0x000A4E50 File Offset: 0x000A3050
		// (set) Token: 0x0600299C RID: 10652 RVA: 0x000A4E7C File Offset: 0x000A307C
		public double? AltitudeAccuracy
		{
			get
			{
				this.CheckDisposed("AltitudeAccuracy::get");
				return base.GetValueOrDefault<double?>(InternalSchema.AltitudeAccuracy, null);
			}
			set
			{
				this.CheckDisposed("AltitudeAccuracy::set");
				if (value.Equals(base.GetValueAsNullable<double>(InternalSchema.AltitudeAccuracy)))
				{
					return;
				}
				base.LocationIdentifierHelperInstance.SetLocationIdentifier(44840U);
				base.SetOrDeleteProperty(InternalSchema.AltitudeAccuracy, value);
			}
		}

		// Token: 0x17000DBA RID: 3514
		// (get) Token: 0x0600299D RID: 10653 RVA: 0x000A4ED5 File Offset: 0x000A30D5
		// (set) Token: 0x0600299E RID: 10654 RVA: 0x000A4EEE File Offset: 0x000A30EE
		public string LocationStreet
		{
			get
			{
				this.CheckDisposed("LocationStreet::get");
				return base.GetValueOrDefault<string>(CalendarItemBaseSchema.LocationStreet, null);
			}
			set
			{
				this.CheckDisposed("LocationStreet::set");
				base.LocationIdentifierHelperInstance.SetLocationIdentifier(61224U);
				base.SetOrDeleteProperty(CalendarItemBaseSchema.LocationStreet, value);
			}
		}

		// Token: 0x17000DBB RID: 3515
		// (get) Token: 0x0600299F RID: 10655 RVA: 0x000A4F17 File Offset: 0x000A3117
		// (set) Token: 0x060029A0 RID: 10656 RVA: 0x000A4F30 File Offset: 0x000A3130
		public string LocationCity
		{
			get
			{
				this.CheckDisposed("LocationCity::get");
				return base.GetValueOrDefault<string>(CalendarItemBaseSchema.LocationCity, null);
			}
			set
			{
				this.CheckDisposed("LocationCity::set");
				base.LocationIdentifierHelperInstance.SetLocationIdentifier(36928U);
				base.SetOrDeleteProperty(CalendarItemBaseSchema.LocationCity, value);
			}
		}

		// Token: 0x17000DBC RID: 3516
		// (get) Token: 0x060029A1 RID: 10657 RVA: 0x000A4F59 File Offset: 0x000A3159
		// (set) Token: 0x060029A2 RID: 10658 RVA: 0x000A4F72 File Offset: 0x000A3172
		public string LocationState
		{
			get
			{
				this.CheckDisposed("LocationState::get");
				return base.GetValueOrDefault<string>(CalendarItemBaseSchema.LocationState, null);
			}
			set
			{
				this.CheckDisposed("LocationState::set");
				base.LocationIdentifierHelperInstance.SetLocationIdentifier(53312U);
				base.SetOrDeleteProperty(CalendarItemBaseSchema.LocationState, value);
			}
		}

		// Token: 0x17000DBD RID: 3517
		// (get) Token: 0x060029A3 RID: 10659 RVA: 0x000A4F9B File Offset: 0x000A319B
		// (set) Token: 0x060029A4 RID: 10660 RVA: 0x000A4FB4 File Offset: 0x000A31B4
		public string LocationCountry
		{
			get
			{
				this.CheckDisposed("LocationCountry::get");
				return base.GetValueOrDefault<string>(CalendarItemBaseSchema.LocationCountry, null);
			}
			set
			{
				this.CheckDisposed("LocationCountry::set");
				base.LocationIdentifierHelperInstance.SetLocationIdentifier(41024U);
				base.SetOrDeleteProperty(CalendarItemBaseSchema.LocationCountry, value);
			}
		}

		// Token: 0x17000DBE RID: 3518
		// (get) Token: 0x060029A5 RID: 10661 RVA: 0x000A4FDD File Offset: 0x000A31DD
		// (set) Token: 0x060029A6 RID: 10662 RVA: 0x000A4FF6 File Offset: 0x000A31F6
		public string LocationPostalCode
		{
			get
			{
				this.CheckDisposed("LocationPostalCode::get");
				return base.GetValueOrDefault<string>(CalendarItemBaseSchema.LocationPostalCode, null);
			}
			set
			{
				this.CheckDisposed("LocationPostalCode::set");
				base.LocationIdentifierHelperInstance.SetLocationIdentifier(57408U);
				base.SetOrDeleteProperty(CalendarItemBaseSchema.LocationPostalCode, value);
			}
		}

		// Token: 0x17000DBF RID: 3519
		// (get) Token: 0x060029A7 RID: 10663 RVA: 0x000A501F File Offset: 0x000A321F
		// (set) Token: 0x060029A8 RID: 10664 RVA: 0x000A5038 File Offset: 0x000A3238
		public string OnlineMeetingConfLink
		{
			get
			{
				this.CheckDisposed("OnlineMeetingConfLink::get");
				return base.GetValueOrDefault<string>(InternalSchema.OnlineMeetingConfLink, null);
			}
			set
			{
				this.CheckDisposed("OnlineMeetingConfLink::set");
				base.SetOrDeleteProperty(InternalSchema.OnlineMeetingConfLink, value);
			}
		}

		// Token: 0x17000DC0 RID: 3520
		// (get) Token: 0x060029A9 RID: 10665 RVA: 0x000A5051 File Offset: 0x000A3251
		// (set) Token: 0x060029AA RID: 10666 RVA: 0x000A506A File Offset: 0x000A326A
		public string OnlineMeetingExternalLink
		{
			get
			{
				this.CheckDisposed("OnlineMeetingExternalLink::get");
				return base.GetValueOrDefault<string>(InternalSchema.OnlineMeetingExternalLink, null);
			}
			set
			{
				this.CheckDisposed("OnlineMeetingExternalLink::set");
				base.SetOrDeleteProperty(InternalSchema.OnlineMeetingExternalLink, value);
			}
		}

		// Token: 0x17000DC1 RID: 3521
		// (get) Token: 0x060029AB RID: 10667 RVA: 0x000A5083 File Offset: 0x000A3283
		// (set) Token: 0x060029AC RID: 10668 RVA: 0x000A509C File Offset: 0x000A329C
		public string OnlineMeetingInternalLink
		{
			get
			{
				this.CheckDisposed("OnlineMeetingInternalLink::get");
				return base.GetValueOrDefault<string>(InternalSchema.OnlineMeetingInternalLink, null);
			}
			set
			{
				this.CheckDisposed("OnlineMeetingInternalLink::set");
				base.SetOrDeleteProperty(InternalSchema.OnlineMeetingInternalLink, value);
			}
		}

		// Token: 0x17000DC2 RID: 3522
		// (get) Token: 0x060029AD RID: 10669 RVA: 0x000A50B5 File Offset: 0x000A32B5
		// (set) Token: 0x060029AE RID: 10670 RVA: 0x000A50CE File Offset: 0x000A32CE
		public string UCOpenedConferenceID
		{
			get
			{
				this.CheckDisposed("UCOpenedConferenceID::get");
				return base.GetValueOrDefault<string>(InternalSchema.UCOpenedConferenceID, null);
			}
			set
			{
				this.CheckDisposed("UCOpenedConferenceID::set");
				base.SetOrDeleteProperty(InternalSchema.UCOpenedConferenceID, value);
			}
		}

		// Token: 0x17000DC3 RID: 3523
		// (get) Token: 0x060029AF RID: 10671 RVA: 0x000A50E7 File Offset: 0x000A32E7
		// (set) Token: 0x060029B0 RID: 10672 RVA: 0x000A5100 File Offset: 0x000A3300
		public string UCCapabilities
		{
			get
			{
				this.CheckDisposed("UCCapabilities::get");
				return base.GetValueOrDefault<string>(InternalSchema.UCCapabilities, null);
			}
			set
			{
				this.CheckDisposed("UCCapabilities::set");
				base.SetOrDeleteProperty(InternalSchema.UCCapabilities, value);
			}
		}

		// Token: 0x17000DC4 RID: 3524
		// (get) Token: 0x060029B1 RID: 10673 RVA: 0x000A5119 File Offset: 0x000A3319
		// (set) Token: 0x060029B2 RID: 10674 RVA: 0x000A5132 File Offset: 0x000A3332
		public string UCInband
		{
			get
			{
				this.CheckDisposed("UCInband::get");
				return base.GetValueOrDefault<string>(InternalSchema.UCInband, null);
			}
			set
			{
				this.CheckDisposed("UCInband::set");
				base.SetOrDeleteProperty(InternalSchema.UCInband, value);
			}
		}

		// Token: 0x17000DC5 RID: 3525
		// (get) Token: 0x060029B3 RID: 10675 RVA: 0x000A514B File Offset: 0x000A334B
		// (set) Token: 0x060029B4 RID: 10676 RVA: 0x000A5164 File Offset: 0x000A3364
		public string UCMeetingSetting
		{
			get
			{
				this.CheckDisposed("UCMeetingSetting::get");
				return base.GetValueOrDefault<string>(InternalSchema.UCMeetingSetting, null);
			}
			set
			{
				this.CheckDisposed("UCMeetingSetting::set");
				base.SetOrDeleteProperty(InternalSchema.UCMeetingSetting, value);
			}
		}

		// Token: 0x17000DC6 RID: 3526
		// (get) Token: 0x060029B5 RID: 10677 RVA: 0x000A517D File Offset: 0x000A337D
		// (set) Token: 0x060029B6 RID: 10678 RVA: 0x000A5196 File Offset: 0x000A3396
		public string UCMeetingSettingSent
		{
			get
			{
				this.CheckDisposed("UCMeetingSettingSent::get");
				return base.GetValueOrDefault<string>(InternalSchema.UCMeetingSettingSent, null);
			}
			set
			{
				this.CheckDisposed("UCMeetingSettingSent::set");
				base.SetOrDeleteProperty(InternalSchema.UCMeetingSettingSent, value);
			}
		}

		// Token: 0x17000DC7 RID: 3527
		// (get) Token: 0x060029B7 RID: 10679 RVA: 0x000A51AF File Offset: 0x000A33AF
		// (set) Token: 0x060029B8 RID: 10680 RVA: 0x000A51C8 File Offset: 0x000A33C8
		public string ConferenceTelURI
		{
			get
			{
				this.CheckDisposed("ConferenceTelURI::get");
				return base.GetValueOrDefault<string>(InternalSchema.ConferenceTelURI, null);
			}
			set
			{
				this.CheckDisposed("ConferenceTelURI::set");
				base.SetOrDeleteProperty(InternalSchema.ConferenceTelURI, value);
			}
		}

		// Token: 0x17000DC8 RID: 3528
		// (get) Token: 0x060029B9 RID: 10681 RVA: 0x000A51E1 File Offset: 0x000A33E1
		// (set) Token: 0x060029BA RID: 10682 RVA: 0x000A51FA File Offset: 0x000A33FA
		public string ConferenceInfo
		{
			get
			{
				this.CheckDisposed("ConferenceInfo::get");
				return base.GetValueOrDefault<string>(InternalSchema.ConferenceInfo, null);
			}
			set
			{
				this.CheckDisposed("ConferenceInfo::set");
				base.SetOrDeleteProperty(InternalSchema.ConferenceInfo, value);
			}
		}

		// Token: 0x17000DC9 RID: 3529
		// (get) Token: 0x060029BB RID: 10683 RVA: 0x000A5213 File Offset: 0x000A3413
		// (set) Token: 0x060029BC RID: 10684 RVA: 0x000A522C File Offset: 0x000A342C
		public byte[] OutlookUserPropsPropDefStream
		{
			get
			{
				this.CheckDisposed("OutlookUserPropsPropDefStream::get");
				return base.GetValueOrDefault<byte[]>(InternalSchema.OutlookUserPropsPropDefStream, null);
			}
			set
			{
				this.CheckDisposed("OutlookUserPropsPropDefStream::set");
				base.SetOrDeleteProperty(InternalSchema.OutlookUserPropsPropDefStream, value);
			}
		}

		// Token: 0x17000DCA RID: 3530
		// (get) Token: 0x060029BD RID: 10685 RVA: 0x000A5245 File Offset: 0x000A3445
		// (set) Token: 0x060029BE RID: 10686 RVA: 0x000A5262 File Offset: 0x000A3462
		public string When
		{
			get
			{
				this.CheckDisposed("When::get");
				return base.GetValueOrDefault<string>(InternalSchema.When, string.Empty);
			}
			set
			{
				this.CheckDisposed("When::set");
				base.LocationIdentifierHelperInstance.SetLocationIdentifier(36213U);
				this[InternalSchema.When] = value;
			}
		}

		// Token: 0x17000DCB RID: 3531
		// (get) Token: 0x060029BF RID: 10687 RVA: 0x000A528B File Offset: 0x000A348B
		// (set) Token: 0x060029C0 RID: 10688 RVA: 0x000A52A3 File Offset: 0x000A34A3
		public bool IsMeeting
		{
			get
			{
				this.CheckDisposed("IsMeeting::get");
				return base.GetValueOrDefault<bool>(InternalSchema.IsMeeting);
			}
			set
			{
				this.CheckDisposed("IsMeeting::set");
				base.LocationIdentifierHelperInstance.SetLocationIdentifier(52597U);
				this[InternalSchema.IsMeeting] = value;
			}
		}

		// Token: 0x17000DCC RID: 3532
		// (get) Token: 0x060029C1 RID: 10689 RVA: 0x000A52D4 File Offset: 0x000A34D4
		public bool IsCancelled
		{
			get
			{
				this.CheckDisposed("IsMeetingCancelled::get");
				AppointmentStateFlags valueOrDefault = base.GetValueOrDefault<AppointmentStateFlags>(CalendarItemBaseSchema.AppointmentState);
				return CalendarItemBase.IsAppointmentStateCancelled(valueOrDefault);
			}
		}

		// Token: 0x17000DCD RID: 3533
		// (get) Token: 0x060029C2 RID: 10690 RVA: 0x000A52FE File Offset: 0x000A34FE
		public bool MeetingRequestWasSent
		{
			get
			{
				this.CheckDisposed("MeetingRequestWasSent::get");
				return base.GetValueOrDefault<bool>(InternalSchema.MeetingRequestWasSent);
			}
		}

		// Token: 0x17000DCE RID: 3534
		// (get) Token: 0x060029C3 RID: 10691 RVA: 0x000A5316 File Offset: 0x000A3516
		// (set) Token: 0x060029C4 RID: 10692 RVA: 0x000A532E File Offset: 0x000A352E
		public Participant Organizer
		{
			get
			{
				this.CheckDisposed("Organizer::get");
				return base.GetValueOrDefault<Participant>(InternalSchema.From);
			}
			private set
			{
				base.SetOrDeleteProperty(InternalSchema.From, value);
			}
		}

		// Token: 0x17000DCF RID: 3535
		// (get) Token: 0x060029C5 RID: 10693 RVA: 0x000A533C File Offset: 0x000A353C
		public bool IsOrganizerExternal
		{
			get
			{
				this.CheckDisposed("IsOrganizerExternal::get");
				Participant organizer = this.Organizer;
				return organizer == null || MeetingMessage.IsFromExternalParticipant(organizer.RoutingType);
			}
		}

		// Token: 0x17000DD0 RID: 3536
		// (get) Token: 0x060029C6 RID: 10694 RVA: 0x000A5371 File Offset: 0x000A3571
		public string CalendarOriginatorId
		{
			get
			{
				this.CheckDisposed("CalendarOriginatorId::get");
				return base.GetValueOrDefault<string>(CalendarItemBaseSchema.CalendarOriginatorId);
			}
		}

		// Token: 0x17000DD1 RID: 3537
		// (get) Token: 0x060029C7 RID: 10695 RVA: 0x000A5389 File Offset: 0x000A3589
		public virtual bool IsForwardAllowed
		{
			get
			{
				this.CheckDisposed("IsForwardAllowed::get");
				return true;
			}
		}

		// Token: 0x17000DD2 RID: 3538
		// (get) Token: 0x060029C8 RID: 10696 RVA: 0x000A5397 File Offset: 0x000A3597
		// (set) Token: 0x060029C9 RID: 10697 RVA: 0x000A53B0 File Offset: 0x000A35B0
		public ClientIntentFlags ClientIntent
		{
			get
			{
				this.CheckDisposed("ClientIntent::get");
				return base.GetValueOrDefault<ClientIntentFlags>(CalendarItemBaseSchema.ClientIntent, ClientIntentFlags.None);
			}
			set
			{
				this.CheckDisposed("ClientIntent::set");
				EnumValidator.ThrowIfInvalid<ClientIntentFlags>(value, "ClientIntent");
				base.LocationIdentifierHelperInstance.SetLocationIdentifier(59551U);
				this[CalendarItemBaseSchema.ClientIntent] = value;
			}
		}

		// Token: 0x17000DD3 RID: 3539
		// (get) Token: 0x060029CA RID: 10698 RVA: 0x000A53E9 File Offset: 0x000A35E9
		// (set) Token: 0x060029CB RID: 10699 RVA: 0x000A5401 File Offset: 0x000A3601
		public bool ResponseRequested
		{
			get
			{
				this.CheckDisposed("ResponseRequested::get");
				return base.GetValueOrDefault<bool>(InternalSchema.IsResponseRequested);
			}
			set
			{
				this.CheckDisposed("ResponseRequested::set");
				base.LocationIdentifierHelperInstance.SetLocationIdentifier(46620U);
				this[InternalSchema.IsResponseRequested] = value;
			}
		}

		// Token: 0x17000DD4 RID: 3540
		// (get) Token: 0x060029CC RID: 10700 RVA: 0x000A542F File Offset: 0x000A362F
		public override Schema Schema
		{
			get
			{
				this.CheckDisposed("Schema::get");
				return CalendarItemBaseSchema.Instance;
			}
		}

		// Token: 0x17000DD5 RID: 3541
		// (get) Token: 0x060029CD RID: 10701 RVA: 0x000A5441 File Offset: 0x000A3641
		// (set) Token: 0x060029CE RID: 10702 RVA: 0x000A544E File Offset: 0x000A364E
		public bool IsReminderSet
		{
			get
			{
				return base.Reminder.IsSet;
			}
			set
			{
				base.Reminder.IsSet = value;
			}
		}

		// Token: 0x17000DD6 RID: 3542
		// (get) Token: 0x060029CF RID: 10703 RVA: 0x000A545C File Offset: 0x000A365C
		// (set) Token: 0x060029D0 RID: 10704 RVA: 0x000A5469 File Offset: 0x000A3669
		public int ReminderMinutesBeforeStart
		{
			get
			{
				return base.Reminder.MinutesBeforeStart;
			}
			set
			{
				base.Reminder.MinutesBeforeStart = value;
			}
		}

		// Token: 0x17000DD7 RID: 3543
		// (get) Token: 0x060029D1 RID: 10705 RVA: 0x000A5477 File Offset: 0x000A3677
		public string ItemClass
		{
			get
			{
				return base.TryGetProperty(InternalSchema.ItemClass) as string;
			}
		}

		// Token: 0x17000DD8 RID: 3544
		// (get) Token: 0x060029D2 RID: 10706 RVA: 0x000A5489 File Offset: 0x000A3689
		// (set) Token: 0x060029D3 RID: 10707 RVA: 0x000A54B5 File Offset: 0x000A36B5
		public RemindersState<EventTimeBasedInboxReminderState> EventTimeBasedInboxRemindersState
		{
			get
			{
				this.CheckDisposed("EventTimeBasedInboxRemindersState::get");
				if (this.eventTimeBasedInboxRemindersState == null)
				{
					this.eventTimeBasedInboxRemindersState = RemindersState<EventTimeBasedInboxReminderState>.Get(this, CalendarItemBaseSchema.EventTimeBasedInboxRemindersState);
				}
				return this.eventTimeBasedInboxRemindersState;
			}
			set
			{
				this.CheckDisposed("EventTimeBasedInboxRemindersState::set");
				RemindersState<EventTimeBasedInboxReminderState>.Set(this, CalendarItemBaseSchema.EventTimeBasedInboxRemindersState, value);
				this.eventTimeBasedInboxRemindersState = value;
			}
		}

		// Token: 0x060029D4 RID: 10708 RVA: 0x000A54D5 File Offset: 0x000A36D5
		public static bool IsAppointmentStateCancelled(AppointmentStateFlags appointmentState)
		{
			appointmentState &= AppointmentStateFlags.Cancelled;
			return appointmentState == AppointmentStateFlags.Cancelled;
		}

		// Token: 0x060029D5 RID: 10709
		public abstract string GenerateWhen();

		// Token: 0x060029D6 RID: 10710 RVA: 0x000A54E0 File Offset: 0x000A36E0
		public MeetingResponse RespondToMeetingRequest(ResponseType responseType)
		{
			return this.RespondToMeetingRequest(responseType, false, false, null, null);
		}

		// Token: 0x060029D7 RID: 10711 RVA: 0x000A5508 File Offset: 0x000A3708
		public MeetingResponse RespondToMeetingRequest(ResponseType responseType, bool autoCaptureClientIntent, bool intendToSendResponse, ExDateTime? proposedStart = null, ExDateTime? proposedEnd = null)
		{
			this.CheckDisposed("RespondToMeetingRequest");
			EnumValidator.ThrowIfInvalid<ResponseType>(responseType, "responseType");
			if (autoCaptureClientIntent)
			{
				this.SetClientIntentBasedOnResponse(responseType, intendToSendResponse);
			}
			return this.RespondToMeetingRequest(responseType, null, proposedStart, proposedEnd);
		}

		// Token: 0x060029D8 RID: 10712 RVA: 0x000A5538 File Offset: 0x000A3738
		public virtual MeetingResponse RespondToMeetingRequest(ResponseType responseType, string subjectPrefix, ExDateTime? proposedStart = null, ExDateTime? proposedEnd = null)
		{
			this.CheckDisposed("RespondToMeetingRequest");
			EnumValidator.ThrowIfInvalid<ResponseType>(responseType, "responseType");
			ExTraceGlobals.MeetingMessageTracer.Information<GlobalObjectId, ResponseType>((long)this.GetHashCode(), "Storage.CalendarItemBase.RespondToMeetingRequest: GOID={0}; responseType={1}", this.GlobalObjectId, responseType);
			if (this.IsCancelled)
			{
				throw new InvalidOperationException("RespondToMeetingRequest called on cancelled meeting");
			}
			if (this.IsOrganizer())
			{
				throw new InvalidOperationException(ServerStrings.ExCannotCreateMeetingResponse);
			}
			if (this.Organizer == null)
			{
				throw new CorruptDataException(ServerStrings.ExInvalidOrganizer);
			}
			MailboxSession mailboxSession = base.Session as MailboxSession;
			bool flag = false;
			if (mailboxSession != null)
			{
				if (mailboxSession.MasterMailboxSession != null)
				{
					base.LocationIdentifierHelperInstance.SetLocationIdentifier(46453U);
					this.AppointmentReplyName = mailboxSession.MasterMailboxSession.MailboxOwner.MailboxInfo.DisplayName;
				}
				else if (mailboxSession.LogonType == LogonType.Delegated)
				{
					base.LocationIdentifierHelperInstance.SetLocationIdentifier(62837U);
					this.AppointmentReplyName = mailboxSession.DelegateUser.DisplayName;
				}
				else
				{
					base.LocationIdentifierHelperInstance.SetLocationIdentifier(38261U);
					this.AppointmentReplyName = mailboxSession.MailboxOwner.MailboxInfo.DisplayName;
				}
				mailboxSession = this.GetMessageMailboxSession(out flag);
			}
			bool flag2 = this.IsCorrelated;
			this.ResponseType = responseType;
			BusyType valueOrDefault = base.GetValueOrDefault<BusyType>(InternalSchema.IntendedFreeBusyStatus, BusyType.Busy);
			if (ResponseType.Tentative == responseType)
			{
				this.FreeBusyStatus = ((valueOrDefault == BusyType.Free) ? BusyType.Free : BusyType.Tentative);
			}
			else if (ResponseType.Accept == responseType)
			{
				this.FreeBusyStatus = ((valueOrDefault == BusyType.Unknown) ? BusyType.Busy : valueOrDefault);
			}
			else if (ResponseType.Decline == responseType)
			{
				this.FreeBusyStatus = BusyType.Free;
				if (flag)
				{
					base.LocationIdentifierHelperInstance.SetLocationIdentifier(42357U);
					this[InternalSchema.OriginalStoreEntryId] = base.Session.Mailbox.StoreObjectId.ProviderLevelItemId;
				}
			}
			base.LocationIdentifierHelperInstance.SetLocationIdentifier(58741U, LastChangeAction.RespondToMeetingRequest);
			MeetingResponse meetingResponse = null;
			bool flag3 = false;
			byte[] bytes = CTSGlobals.AsciiEncoding.GetBytes(this[InternalSchema.AppointmentReplyName].ToString());
			MeetingResponse result;
			try
			{
				meetingResponse = this.CreateResponse(mailboxSession, responseType, flag, proposedStart, proposedEnd);
				this.AppointmentReplyTime = meetingResponse.AttendeeCriticalChangeTime;
				this.SaveWithConflictCheck(SaveMode.ResolveConflicts);
				if (proposedStart != null && proposedEnd != null && string.IsNullOrEmpty(subjectPrefix))
				{
					subjectPrefix = ClientStrings.MeetingProposedNewTime.ToString(base.Session.InternalCulture);
				}
				CalendarItemBase.SetMeetingResponseSubjectPrefix(responseType, meetingResponse, subjectPrefix);
				flag3 = true;
				if (responseType == ResponseType.Decline)
				{
					this.UpdateAppointmentTombstone(bytes);
				}
				result = meetingResponse;
			}
			finally
			{
				if (!flag3 && meetingResponse != null)
				{
					meetingResponse.Dispose();
					meetingResponse = null;
				}
			}
			return result;
		}

		// Token: 0x060029D9 RID: 10713
		public abstract void MoveToFolder(MailboxSession destinationSession, StoreObjectId destinationFolderId);

		// Token: 0x060029DA RID: 10714
		public abstract void CopyToFolder(MailboxSession destinationSession, StoreObjectId destinationFolderId);

		// Token: 0x060029DB RID: 10715 RVA: 0x000A57A8 File Offset: 0x000A39A8
		private static void SetMeetingResponseSubjectPrefix(ResponseType responseType, MeetingMessage meetingMessage, string overrideSubjectPrefix)
		{
			if (overrideSubjectPrefix != null)
			{
				meetingMessage[InternalSchema.SubjectPrefix] = overrideSubjectPrefix;
				return;
			}
			LocalizedString localizedString = LocalizedString.Empty;
			switch (responseType)
			{
			case ResponseType.Tentative:
				localizedString = ClientStrings.MeetingTentative;
				break;
			case ResponseType.Accept:
				localizedString = ClientStrings.MeetingAccept;
				break;
			case ResponseType.Decline:
				localizedString = ClientStrings.MeetingDecline;
				break;
			default:
				throw new NotSupportedException(ServerStrings.ExResponseTypeNoSubjectPrefix(responseType.ToString()));
			}
			meetingMessage[InternalSchema.SubjectPrefix] = localizedString.ToString(meetingMessage.Session.InternalPreferedCulture);
		}

		// Token: 0x060029DC RID: 10716 RVA: 0x000A5834 File Offset: 0x000A3A34
		private void UpdateAppointmentTombstone(byte[] username)
		{
			if (username == null)
			{
				ExTraceGlobals.MeetingMessageTracer.TraceError((long)this.GetHashCode(), "TombstoneRecord with UserName null cannot be created. Hence AppointmentTombstone is not updated!");
				return;
			}
			MailboxSession mailboxSession = base.Session as MailboxSession;
			if (mailboxSession != null && mailboxSession.MailboxOwner != null && mailboxSession.MailboxOwner.Delegates != null && mailboxSession.MailboxOwner.Delegates.Any<ADObjectId>())
			{
				try
				{
					TombstoneRecord tombstoneRecord = new TombstoneRecord
					{
						StartTime = this.StartTimeZone.ConvertDateTime(this.StartTime),
						EndTime = this.EndTimeZone.ConvertDateTime(this.EndTime),
						GlobalObjectId = this.GlobalObjectId.Bytes,
						UserName = username
					};
					StoreObjectId defaultFolderId = mailboxSession.GetDefaultFolderId(DefaultFolderType.FreeBusyData);
					VersionedId localFreeBusyMessageId = mailboxSession.GetLocalFreeBusyMessageId(defaultFolderId);
					if (localFreeBusyMessageId != null)
					{
						using (Item item = Microsoft.Exchange.Data.Storage.Item.Bind(mailboxSession, localFreeBusyMessageId, CalendarItemBase.AppointmentTombstomeDefinition))
						{
							ExTraceGlobals.MeetingMessageTracer.Information<string>((long)this.GetHashCode(), "Reading AppointmentTombstone property for the mailbox: {0}.", mailboxSession.MailboxOwner.MailboxInfo.DisplayName);
							byte[] valueOrDefault = item.GetValueOrDefault<byte[]>(InternalSchema.AppointmentTombstones);
							int valueOrDefault2 = item.GetValueOrDefault<int>(InternalSchema.OutlookFreeBusyMonthCount, 2);
							AppointmentTombstone appointmentTombstone = new AppointmentTombstone();
							try
							{
								appointmentTombstone.LoadTombstoneRecords(valueOrDefault, valueOrDefault2);
							}
							catch (CorruptDataException)
							{
								ExTraceGlobals.MeetingMessageTracer.TraceError<string>((long)this.GetHashCode(), "Appointment tombstone is corrupted for: {0}", mailboxSession.MailboxOwner.MailboxInfo.DisplayName);
							}
							ExTraceGlobals.MeetingMessageTracer.Information<string>((long)this.GetHashCode(), "Updating AppointmentTombstone property for the mailbox: {0}.", mailboxSession.MailboxOwner.MailboxInfo.DisplayName);
							appointmentTombstone.AppendTombstoneRecord(tombstoneRecord);
							item.SafeSetProperty(InternalSchema.AppointmentTombstones, appointmentTombstone.ToByteArray());
							item.Save(SaveMode.ResolveConflicts);
							goto IL_1D4;
						}
					}
					ExTraceGlobals.MeetingMessageTracer.TraceError<string>((long)this.GetHashCode(), "Outlook freebusy message was not found for the mailbox: {0}.", mailboxSession.MailboxOwner.MailboxInfo.DisplayName);
					IL_1D4:;
				}
				catch (StoragePermanentException ex)
				{
					ExTraceGlobals.MeetingMessageTracer.TraceError<string, string>((long)this.GetHashCode(), "Unable to update AppointmentTombstone for mailbox '{0}' with exception:'{1}'", mailboxSession.MailboxOwner.MailboxInfo.DisplayName, ex.Message);
				}
				catch (StorageTransientException ex2)
				{
					ExTraceGlobals.MeetingMessageTracer.TraceError<string, string>((long)this.GetHashCode(), "Unable to update AppointmentTombstone for mailbox '{0}' with exception:'{1}'", mailboxSession.MailboxOwner.MailboxInfo.DisplayName, ex2.Message);
				}
			}
		}

		// Token: 0x060029DD RID: 10717 RVA: 0x000A5AE0 File Offset: 0x000A3CE0
		public static StoreObjectId GetDraftsFolderIdOrThrow(MailboxSession mailboxSession)
		{
			if (mailboxSession == null)
			{
				throw new NotSupportedException();
			}
			StoreObjectId defaultFolderId = mailboxSession.GetDefaultFolderId(DefaultFolderType.Drafts);
			if (defaultFolderId == null)
			{
				ExTraceGlobals.MeetingMessageTracer.TraceError(-1L, "CalendarItemBase::GetDraftsFolderIdOrThrow. Drafts folder cannot be found.");
				throw new ObjectNotFoundException(ServerStrings.ExDefaultFolderNotFound(DefaultFolderType.Drafts));
			}
			return defaultFolderId;
		}

		// Token: 0x060029DE RID: 10718 RVA: 0x000A5B24 File Offset: 0x000A3D24
		protected virtual void InitializeMeetingResponse(MeetingResponse meetingResponse, ResponseType responseType, bool isCalendarDelegateAccess, ExDateTime? proposedStart, ExDateTime? proposedEnd)
		{
			meetingResponse[InternalSchema.IconIndex] = CalendarItemBase.GetIconIndexToSet(responseType);
			if (isCalendarDelegateAccess)
			{
				meetingResponse.From = new Participant(((MailboxSession)base.Session).MailboxOwner);
			}
			CalendarItemBase.CopyPropertiesTo(this, meetingResponse, CalendarItemProperties.MeetingResponseProperties);
			if (this.AssociatedItemId != null)
			{
				meetingResponse.AssociatedItemId = this.AssociatedItemId;
			}
			meetingResponse[CalendarItemBaseSchema.AttendeeCriticalChangeTime] = ExDateTime.GetNow(base.PropertyBag.ExTimeZone);
			meetingResponse.Recipients.Add(this.Organizer);
		}

		// Token: 0x060029DF RID: 10719 RVA: 0x000A5BB8 File Offset: 0x000A3DB8
		private MeetingResponse CreateResponse(MailboxSession mailboxSession, ResponseType responseType, bool isCalendarDelegateAccess, ExDateTime? proposedStart = null, ExDateTime? proposedEnd = null)
		{
			bool flag = false;
			MeetingResponse meetingResponse = this.CreateNewMeetingResponse(mailboxSession, responseType);
			try
			{
				this.InitializeMeetingResponse(meetingResponse, responseType, isCalendarDelegateAccess, proposedStart, proposedEnd);
				flag = true;
			}
			finally
			{
				if (!flag && meetingResponse != null)
				{
					meetingResponse.Dispose();
					meetingResponse = null;
				}
			}
			return meetingResponse;
		}

		// Token: 0x060029E0 RID: 10720 RVA: 0x000A5C04 File Offset: 0x000A3E04
		private static IconIndex GetIconIndexToSet(ResponseType responseType)
		{
			IconIndex result;
			switch (responseType)
			{
			default:
				throw new ArgumentException(ServerStrings.ExInvalidResponseType(responseType));
			case ResponseType.Tentative:
				result = IconIndex.AppointmentMeetMaybe;
				break;
			case ResponseType.Accept:
				result = IconIndex.AppointmentMeetYes;
				break;
			case ResponseType.Decline:
				result = IconIndex.AppointmentMeetNo;
				break;
			}
			return result;
		}

		// Token: 0x060029E1 RID: 10721 RVA: 0x000A5C60 File Offset: 0x000A3E60
		public virtual void SendMeetingMessages(bool isToAllAttendees, int? seriesSequenceNumber = null, bool autoCaptureClientIntent = false, bool copyToSentItems = true, string occurrencesViewPropertiesBlob = null, byte[] masterGoid = null)
		{
			this.CheckDisposed("SendMeetingMessages");
			ExTraceGlobals.MeetingMessageTracer.Information<GlobalObjectId, bool>((long)this.GetHashCode(), "Storage.CalendarItemBase.SendMeetingMessages: GOID={0}; isToAllAttendees={1}", this.GlobalObjectId, isToAllAttendees);
			base.LocationIdentifierHelperInstance.SetLocationIdentifier(34975U, LastChangeAction.SendMeetingMessages);
			if (autoCaptureClientIntent)
			{
				this.SetClientIntentBasedOnModifiedProperties(null);
			}
			MailboxSession mailboxSession = this.SendMessagesProlog();
			this.AdjustIsToAllAttendees(ref isToAllAttendees);
			if (this.MeetingRequestWasSent && this.removedAttendeeArray != null)
			{
				IList<Attendee> list = new List<Attendee>(this.removedAttendeeArray.Length);
				foreach (Attendee attendee in this.removedAttendeeArray)
				{
					if (attendee.IsSendable())
					{
						list.Add(attendee);
					}
				}
				this.SendMeetingCancellations(mailboxSession, isToAllAttendees, list, copyToSentItems, false, null);
			}
			this.SendMeetingRequests(mailboxSession, copyToSentItems, isToAllAttendees, int.MinValue, occurrencesViewPropertiesBlob, seriesSequenceNumber, masterGoid);
			base.LocationIdentifierHelperInstance.SetLocationIdentifier(34165U, LastChangeAction.SendMeetingMessages);
			this.SendMessagesEpilog();
		}

		// Token: 0x060029E2 RID: 10722 RVA: 0x000A5D50 File Offset: 0x000A3F50
		public void SetClientIntentBasedOnModifiedProperties(ClientIntentFlags? mask)
		{
			this.CheckDisposed("SetClientIntentBasedOnModifiedProperties");
			this.ClientIntent = this.CalculateClientIntentBasedOnModifiedProperties();
			if (base.PropertyBag.IsPropertyDirty(CalendarItemBaseSchema.Location))
			{
				this.ClientIntent |= ClientIntentFlags.ModifiedLocation;
			}
			if (mask != null)
			{
				this.ClientIntent &= mask.Value;
			}
		}

		// Token: 0x060029E3 RID: 10723 RVA: 0x000A5DB5 File Offset: 0x000A3FB5
		protected virtual ClientIntentFlags CalculateClientIntentBasedOnModifiedProperties()
		{
			return ClientIntentFlags.None;
		}

		// Token: 0x060029E4 RID: 10724 RVA: 0x000A5DB8 File Offset: 0x000A3FB8
		private void SetClientIntentBasedOnResponse(ResponseType responseType, bool intendToSendResponse)
		{
			switch (responseType)
			{
			case ResponseType.Tentative:
				this.SetTentativeIntent(intendToSendResponse);
				return;
			case ResponseType.Accept:
				this.SetAcceptIntent(intendToSendResponse);
				return;
			case ResponseType.Decline:
				this.SetDeclineIntent(intendToSendResponse);
				return;
			default:
				return;
			}
		}

		// Token: 0x060029E5 RID: 10725 RVA: 0x000A5DF3 File Offset: 0x000A3FF3
		protected virtual void SetTentativeIntent(bool intendToSendResponse)
		{
			this.ClientIntent = (intendToSendResponse ? ClientIntentFlags.RespondedTentative : ClientIntentFlags.None);
		}

		// Token: 0x060029E6 RID: 10726 RVA: 0x000A5E03 File Offset: 0x000A4003
		protected virtual void SetAcceptIntent(bool intendToSendResponse)
		{
			this.ClientIntent = (intendToSendResponse ? ClientIntentFlags.RespondedAccept : ClientIntentFlags.None);
		}

		// Token: 0x060029E7 RID: 10727
		protected abstract void SetDeclineIntent(bool intendToSendResponse);

		// Token: 0x060029E8 RID: 10728 RVA: 0x000A5E13 File Offset: 0x000A4013
		public void UnsafeSetMeetingRequestWasSent(bool value)
		{
			ExTraceGlobals.MeetingMessageTracer.Information<GlobalObjectId, bool>((long)this.GetHashCode(), "Storage.CalendarItemBase.UnsafeSetMeetingRequestWasSent: GOID={0}; value={1}.", this.GlobalObjectId, value);
			base.LocationIdentifierHelperInstance.SetLocationIdentifier(50549U);
			this[InternalSchema.MeetingRequestWasSent] = value;
		}

		// Token: 0x060029E9 RID: 10729 RVA: 0x000A5E54 File Offset: 0x000A4054
		public virtual bool DeleteMeeting(DeleteItemFlags deleteFlag)
		{
			return base.Session.Delete(deleteFlag, new StoreId[]
			{
				base.StoreObjectId
			}).OperationResult == OperationResult.Succeeded;
		}

		// Token: 0x060029EA RID: 10730 RVA: 0x000A5E88 File Offset: 0x000A4088
		public virtual MeetingCancellation CancelMeeting(int? seriesSequenceNumber = null, byte[] masterGoid = null)
		{
			bool flag = false;
			this.CheckDisposed("CancelMeeting");
			if (!this.IsOrganizer())
			{
				throw new InvalidOperationException(ServerStrings.ExCannotCreateMeetingCancellation);
			}
			ExTraceGlobals.MeetingMessageTracer.Information<GlobalObjectId>((long)this.GetHashCode(), "Storage.CalendarItemBase.CancelMeeting: GOID={0}", this.GlobalObjectId);
			base.LocationIdentifierHelperInstance.SetLocationIdentifier(47477U);
			this[InternalSchema.FreeBusyStatus] = BusyType.Free;
			int num = 0;
			object obj = base.TryGetProperty(InternalSchema.AppointmentState);
			if (!PropertyError.IsPropertyError(obj))
			{
				num = (int)obj;
			}
			if ((num & 4) == 0)
			{
				num |= 4;
				base.LocationIdentifierHelperInstance.SetLocationIdentifier(63861U);
				this[InternalSchema.AppointmentState] = num;
			}
			bool flag2;
			MailboxSession messageMailboxSession = this.GetMessageMailboxSession(out flag2);
			MeetingCancellation meetingCancellation = this.CreateMeetingCancellation(messageMailboxSession, true, seriesSequenceNumber, masterGoid);
			MeetingCancellation result;
			try
			{
				meetingCancellation.CopySendableParticipantsToMessage(this.AttendeeCollection);
				base.LocationIdentifierHelperInstance.SetLocationIdentifier(39285U, LastChangeAction.CancelMeeting);
				this.SaveWithConflictCheck(SaveMode.ResolveConflicts);
				base.Load(null);
				flag = true;
				result = meetingCancellation;
			}
			finally
			{
				if (!flag)
				{
					Util.DisposeIfPresent(meetingCancellation);
				}
			}
			return result;
		}

		// Token: 0x060029EB RID: 10731 RVA: 0x000A5FA8 File Offset: 0x000A41A8
		public MessageItem CreateReply(StoreObjectId parentFolderId, ReplyForwardConfiguration replyForwardParameters)
		{
			this.CheckDisposed("CreateReply");
			Util.ThrowOnNullArgument(parentFolderId, "parentFolderId");
			Util.ThrowOnNullArgument(replyForwardParameters, "replyForwardParameters");
			MailboxSession mailboxSession = base.Session as MailboxSession;
			if (mailboxSession == null)
			{
				string message = "CalendarItemBase::CreateReply: Unable to create reply/forward on non-Mailbox session";
				ExTraceGlobals.StorageTracer.TraceError((long)this.GetHashCode(), message);
				throw new NotSupportedException(message);
			}
			return this.CreateReply(mailboxSession, parentFolderId, replyForwardParameters);
		}

		// Token: 0x060029EC RID: 10732 RVA: 0x000A6010 File Offset: 0x000A4210
		public MessageItem CreateReply(MailboxSession session, StoreObjectId parentFolderId, ReplyForwardConfiguration replyForwardParameters)
		{
			this.CheckDisposed("CreateReply");
			Util.ThrowOnNullArgument(session, "session");
			Util.ThrowOnNullArgument(parentFolderId, "parentFolderId");
			Util.ThrowOnNullArgument(replyForwardParameters, "replyForwardParameters");
			if (!this.IsMeeting)
			{
				throw new NotSupportedException(ServerStrings.AppointmentActionNotSupported("CreateReply"));
			}
			ExTraceGlobals.MeetingMessageTracer.Information((long)this.GetHashCode(), "CalendarItemBase::CreateReply.");
			MessageItem messageItem = null;
			bool flag = false;
			try
			{
				messageItem = base.InternalCreateReply(session, parentFolderId, replyForwardParameters);
				base.LocationIdentifierHelperInstance.SetLocationIdentifier(55669U, LastChangeAction.CreateReply);
				flag = true;
			}
			finally
			{
				if (!flag && messageItem != null)
				{
					messageItem.Dispose();
					messageItem = null;
				}
			}
			return messageItem;
		}

		// Token: 0x060029ED RID: 10733 RVA: 0x000A60C0 File Offset: 0x000A42C0
		public MessageItem CreateReplyAll(StoreObjectId parentFolderId, ReplyForwardConfiguration replyForwardParameters)
		{
			this.CheckDisposed("CreateReplyAll");
			Util.ThrowOnNullArgument(parentFolderId, "parentFolderId");
			Util.ThrowOnNullArgument(replyForwardParameters, "replyForwardParameters");
			MailboxSession mailboxSession = base.Session as MailboxSession;
			if (mailboxSession == null)
			{
				string message = "CalendarItemBase::CreateReplyAll: Unable to create reply/forward on non-Mailbox session";
				ExTraceGlobals.StorageTracer.TraceError((long)this.GetHashCode(), message);
				throw new NotSupportedException(message);
			}
			return this.CreateReplyAll(mailboxSession, parentFolderId, replyForwardParameters);
		}

		// Token: 0x060029EE RID: 10734 RVA: 0x000A6128 File Offset: 0x000A4328
		public MessageItem CreateReplyAll(MailboxSession session, StoreObjectId parentFolderId, ReplyForwardConfiguration replyForwardParameters)
		{
			this.CheckDisposed("CreateReplyAll");
			Util.ThrowOnNullArgument(session, "session");
			Util.ThrowOnNullArgument(parentFolderId, "parentFolderId");
			Util.ThrowOnNullArgument(replyForwardParameters, "replyForwardParameters");
			if (!this.IsMeeting)
			{
				throw new NotSupportedException(ServerStrings.AppointmentActionNotSupported("CreateReplyAll"));
			}
			ExTraceGlobals.MeetingMessageTracer.Information((long)this.GetHashCode(), "CalendarItemBase::CreateReplyAll.");
			bool flag = false;
			MessageItem messageItem = null;
			try
			{
				messageItem = base.InternalCreateReplyAll(session, parentFolderId, replyForwardParameters);
				base.LocationIdentifierHelperInstance.SetLocationIdentifier(43381U, LastChangeAction.CreateReply);
				flag = true;
			}
			finally
			{
				if (!flag && messageItem != null)
				{
					messageItem.Dispose();
					messageItem = null;
				}
			}
			return messageItem;
		}

		// Token: 0x060029EF RID: 10735 RVA: 0x000A61D8 File Offset: 0x000A43D8
		public MessageItem CreateForward(StoreObjectId parentFolderId, ReplyForwardConfiguration replyForwardParameters)
		{
			this.CheckDisposed("CreateForward");
			Util.ThrowOnNullArgument(parentFolderId, "parentFolderId");
			Util.ThrowOnNullArgument(replyForwardParameters, "replyForwardParameters");
			bool flag;
			MailboxSession messageMailboxSession = this.GetMessageMailboxSession(out flag);
			return this.CreateForward(messageMailboxSession, parentFolderId, replyForwardParameters, null, null);
		}

		// Token: 0x060029F0 RID: 10736 RVA: 0x000A6224 File Offset: 0x000A4424
		public MessageItem CreateForward(MailboxSession session, StoreObjectId parentFolderId, ReplyForwardConfiguration replyForwardParameters, int? seriesSequenceNumber = null, string occurrencesViewPropertiesBlob = null)
		{
			this.CheckDisposed("CreateForward");
			this.ValidateForwardArguments(session, parentFolderId, replyForwardParameters);
			ExTraceGlobals.MeetingMessageTracer.Information<GlobalObjectId>((long)this.GetHashCode(), "Storage.CalendarItemBase.CreateForward: GOID={0}", this.GlobalObjectId);
			bool flag = false;
			MessageItem messageItem = null;
			try
			{
				if (this.IsMeeting)
				{
					messageItem = this.ForwardMeeting(session, parentFolderId, replyForwardParameters, seriesSequenceNumber, occurrencesViewPropertiesBlob);
				}
				else
				{
					messageItem = this.ForwardAppointment(session, parentFolderId, replyForwardParameters);
				}
				base.LocationIdentifierHelperInstance.SetLocationIdentifier(59765U, LastChangeAction.CreateForward);
				flag = true;
			}
			finally
			{
				if (!flag && messageItem != null)
				{
					messageItem.Dispose();
					messageItem = null;
				}
			}
			return messageItem;
		}

		// Token: 0x060029F1 RID: 10737 RVA: 0x000A62EC File Offset: 0x000A44EC
		public void ExportAsICAL(Stream outputStream, string charset, OutboundConversionOptions options)
		{
			this.CheckDisposed("ExportAsICAL");
			Util.ThrowOnNullArgument(options, "options");
			Util.ThrowOnNullOrEmptyArgument(options.ImceaEncapsulationDomain, "options.ImceaEncapsulationDomain");
			OutboundAddressCache addressCache = new OutboundAddressCache(options, new ConversionLimitsTracker(options.Limits));
			addressCache.CopyDataFromItem(this);
			addressCache.Resolve();
			ConvertUtils.CallCts(ExTraceGlobals.ICalTracer, "CalendarItemBase::ExportAsICAL", ServerStrings.ConversionCorruptContent, delegate
			{
				CalendarDocument.ItemToICal(this, null, addressCache, outputStream, charset, options);
			});
		}

		// Token: 0x060029F2 RID: 10738 RVA: 0x000A63A4 File Offset: 0x000A45A4
		private MailboxSession GetMessageMailboxSession(out bool isCalendarDelegateAccess)
		{
			MailboxSession mailboxSession = base.Session as MailboxSession;
			if (mailboxSession == null)
			{
				throw new NotSupportedException("Calendar work flow is only enabled on MailboxSession.");
			}
			if (mailboxSession.LogonType == LogonType.Delegated && mailboxSession.IsInternallyOpenedDelegateAccess)
			{
				isCalendarDelegateAccess = true;
				return mailboxSession.MasterMailboxSession;
			}
			isCalendarDelegateAccess = false;
			return mailboxSession;
		}

		// Token: 0x060029F3 RID: 10739 RVA: 0x000A63EA File Offset: 0x000A45EA
		protected virtual void ValidateForwardArguments(MailboxSession session, StoreObjectId parentFolderId, ReplyForwardConfiguration replyForwardParameters)
		{
			Util.ThrowOnNullArgument(session, "session");
			Util.ThrowOnNullArgument(parentFolderId, "parentFolderId");
			Util.ThrowOnNullArgument(replyForwardParameters, "replyForwardParameters");
		}

		// Token: 0x060029F4 RID: 10740 RVA: 0x000A6410 File Offset: 0x000A4610
		protected override void OnBeforeSave()
		{
			this.CheckDisposed("OnBeforeSave");
			if (!base.IsInMemoryObject)
			{
				bool flag = this.IsCorrelated;
				this.UpdateAttendeesOnException();
				if (this.IsAttendeeListCreated)
				{
					((IAttendeeCollectionImpl)this.AttendeeCollection).Cleanup();
				}
				this.needToCalculateAttendeeDiff = this.IsAttendeeListDirty;
				if (this.CalendarItemType != CalendarItemType.Occurrence)
				{
					AppointmentAuxiliaryFlags valueOrDefault = base.GetValueOrDefault<AppointmentAuxiliaryFlags>(CalendarItemBaseSchema.AppointmentAuxiliaryFlags);
					if ((valueOrDefault & AppointmentAuxiliaryFlags.ForwardedAppointment) != (AppointmentAuxiliaryFlags)0)
					{
						this[CalendarItemBaseSchema.AppointmentAuxiliaryFlags] = valueOrDefault - AppointmentAuxiliaryFlags.ForwardedAppointment;
					}
				}
			}
			base.OnBeforeSave();
			if (!base.IsInMemoryObject)
			{
				this.OnBeforeSaveUpdateChangeHighlights();
				this.OnBeforeSaveUpdateLastChangeAction();
				this.OnBeforeSaveUpdateDisplayAttendees();
				this.OnBeforeSaveUpdateSender();
			}
		}

		// Token: 0x060029F5 RID: 10741 RVA: 0x000A64B4 File Offset: 0x000A46B4
		private void OnBeforeSaveUpdateChangeHighlights()
		{
			if (base.CoreItem.AreOptionalAutoloadPropertiesLoaded && this.MeetingRequestWasSent)
			{
				ChangeHighlightHelper changeHighlightHelper = MeetingRequest.ComparePropertyBags(this.propertyBagForChangeHighlight, base.PropertyBag);
				if (base.Body.Size != this.originalBodySize)
				{
					base.LocationIdentifierHelperInstance.SetLocationIdentifier(41461U);
					changeHighlightHelper[InternalSchema.HtmlBody] = true;
				}
				base.LocationIdentifierHelperInstance.SetLocationIdentifier(57845U);
				this[InternalSchema.ChangeHighlight] = changeHighlightHelper.HighlightFlags;
			}
		}

		// Token: 0x060029F6 RID: 10742 RVA: 0x000A6540 File Offset: 0x000A4740
		private void OnBeforeSaveUpdateLastChangeAction()
		{
			if (base.PropertyBag.IsDirty)
			{
				base.LocationIdentifierHelperInstance.SetLocationIdentifier(33269U);
				this[InternalSchema.OutlookVersion] = StorageGlobals.ExchangeVersion;
				this[InternalSchema.OutlookInternalVersion] = (int)StorageGlobals.BuildVersion;
			}
		}

		// Token: 0x060029F7 RID: 10743 RVA: 0x000A6590 File Offset: 0x000A4790
		private void OnBeforeSaveUpdateDisplayAttendees()
		{
			if (this.IsOrganizer() && this.AttendeesChanged)
			{
				StorePropertyDefinition[] sourceProperties = new StorePropertyDefinition[]
				{
					InternalSchema.DisplayAll,
					InternalSchema.DisplayTo,
					InternalSchema.DisplayCc
				};
				StorePropertyDefinition[] targetProperties = new StorePropertyDefinition[]
				{
					InternalSchema.DisplayAttendeesAll,
					InternalSchema.DisplayAttendeesTo,
					InternalSchema.DisplayAttendeesCc
				};
				CalendarItemBase.CopyPropertiesTo(this, this, sourceProperties, targetProperties);
			}
		}

		// Token: 0x060029F8 RID: 10744 RVA: 0x000A65F8 File Offset: 0x000A47F8
		private void OnBeforeSaveUpdateSender()
		{
			object propertyValue = base.TryGetProperty(InternalSchema.Sender);
			object obj = base.TryGetProperty(InternalSchema.From);
			if (PropertyError.IsPropertyError(propertyValue) && !PropertyError.IsPropertyError(obj))
			{
				this[InternalSchema.Sender] = obj;
			}
		}

		// Token: 0x17000DD9 RID: 3545
		// (get) Token: 0x060029F9 RID: 10745 RVA: 0x000A6639 File Offset: 0x000A4839
		// (set) Token: 0x060029FA RID: 10746 RVA: 0x000A664C File Offset: 0x000A484C
		internal override VersionedId AssociatedItemId
		{
			get
			{
				this.CheckDisposed("AssociatedItemId");
				return this.associatedId;
			}
			set
			{
				this.CheckDisposed("AssociatedItemId");
				this.associatedId = value;
			}
		}

		// Token: 0x060029FB RID: 10747
		internal abstract IAttendeeCollection FetchAttendeeCollection(bool forceOpen);

		// Token: 0x17000DDA RID: 3546
		// (get) Token: 0x060029FC RID: 10748
		internal abstract bool IsAttendeeListDirty { get; }

		// Token: 0x17000DDB RID: 3547
		// (get) Token: 0x060029FD RID: 10749
		internal abstract bool IsAttendeeListCreated { get; }

		// Token: 0x060029FE RID: 10750 RVA: 0x000A6660 File Offset: 0x000A4860
		protected virtual void InternalUpdateSequencingProperties(bool isToAllAttendees, MeetingMessage message, int minSequenceNumber, int? seriesSequenceNumber = null)
		{
		}

		// Token: 0x060029FF RID: 10751 RVA: 0x000A6662 File Offset: 0x000A4862
		protected virtual Reminders<EventTimeBasedInboxReminder> FetchEventTimeBasedInboxReminders()
		{
			return Reminders<EventTimeBasedInboxReminder>.Get(this, CalendarItemBaseSchema.EventTimeBasedInboxReminders);
		}

		// Token: 0x06002A00 RID: 10752 RVA: 0x000A666F File Offset: 0x000A486F
		protected virtual void UpdateEventTimeBasedInboxRemindersForSave(Reminders<EventTimeBasedInboxReminder> reminders)
		{
		}

		// Token: 0x06002A01 RID: 10753 RVA: 0x000A6671 File Offset: 0x000A4871
		private void UpdateSequencingProperties(bool isToAllAttendees, MeetingMessage message, int minSequenceNumber, int? seriesSequenceNumber = null)
		{
			if (seriesSequenceNumber != null)
			{
				message.SeriesSequenceNumber = seriesSequenceNumber.Value;
			}
			this.InternalUpdateSequencingProperties(isToAllAttendees, message, minSequenceNumber, seriesSequenceNumber);
		}

		// Token: 0x06002A02 RID: 10754 RVA: 0x000A6694 File Offset: 0x000A4894
		public static IconIndex CalculateMeetingRequestIcon(MeetingRequest meetingRequest)
		{
			IconIndex result = IconIndex.Default;
			MeetingMessageType meetingRequestType = meetingRequest.MeetingRequestType;
			if (meetingRequestType != MeetingMessageType.NewMeetingRequest && meetingRequestType != MeetingMessageType.FullUpdate)
			{
				if (meetingRequestType == MeetingMessageType.InformationalUpdate)
				{
					result = IconIndex.AppointmentMeetInfo;
				}
			}
			else
			{
				bool valueOrDefault = meetingRequest.GetValueOrDefault<bool>(InternalSchema.AppointmentRecurring);
				if (meetingRequest.Recipients.Count == 0)
				{
					if (valueOrDefault)
					{
						result = IconIndex.AppointmentRecur;
					}
					else
					{
						result = IconIndex.BaseAppointment;
					}
				}
				else if (valueOrDefault)
				{
					result = IconIndex.AppointmentMeetRecur;
				}
				else
				{
					result = IconIndex.AppointmentMeet;
				}
			}
			return result;
		}

		// Token: 0x06002A03 RID: 10755 RVA: 0x000A6704 File Offset: 0x000A4904
		internal void SetChangeHighlight(int highlight)
		{
			if (highlight != 0)
			{
				base.LocationIdentifierHelperInstance.SetLocationIdentifier(45941U);
				this[InternalSchema.ChangeHighlight] = (highlight | base.GetValueOrDefault<int>(InternalSchema.ChangeHighlight));
			}
		}

		// Token: 0x06002A04 RID: 10756
		protected abstract void SendMeetingCancellations(MailboxSession mailboxSession, bool isToAllAttendees, IList<Attendee> removedAttendeeList, bool copyToSentItems, bool ignoreSendAsRight, CancellationRumInfo rumInfo);

		// Token: 0x06002A05 RID: 10757 RVA: 0x000A6736 File Offset: 0x000A4936
		protected void SendMessage(MailboxSession mailboxSession, MessageItem message, bool copyToSentItems, bool ignoreSendAsRight)
		{
			MeetingMessage.SendLocalOrRemote(message, copyToSentItems, ignoreSendAsRight);
		}

		// Token: 0x06002A06 RID: 10758 RVA: 0x000A6741 File Offset: 0x000A4941
		protected MeetingCancellation CreateMeetingCancellation(MailboxSession mailboxSession, bool isToAllAttendees, int? seriesSequenceNumber = null, byte[] masterGoid = null)
		{
			return this.CreateMeetingCancellation(mailboxSession, isToAllAttendees, int.MinValue, new Action<MeetingCancellation>(this.CopyBodyWithPrefixToMeetingMessage), true, seriesSequenceNumber, masterGoid);
		}

		// Token: 0x06002A07 RID: 10759 RVA: 0x000A6760 File Offset: 0x000A4960
		protected MeetingCancellation CreateMeetingCancellation(MailboxSession mailboxSession, bool isToAllAttendees, int minSequenceNumber, Action<MeetingCancellation> setBodyAndAdjustFlags, bool includeAttachments, int? seriesSequenceNumber = null, byte[] masterGoid = null)
		{
			if (!this.IsOrganizer())
			{
				throw new InvalidOperationException(ServerStrings.ExCannotCreateMeetingCancellation);
			}
			ExTraceGlobals.MeetingMessageTracer.Information<int>((long)this.GetHashCode(), "CalendarItemBase::CreateMeetingCancellation. HashCode = {0}.", this.GetHashCode());
			MeetingCancellation meetingCancellation = null;
			bool flag = false;
			MeetingCancellation result;
			try
			{
				meetingCancellation = this.CreateNewMeetingCancelation(mailboxSession);
				CalendarItemBase.CopyPropertiesTo(this, meetingCancellation, MeetingMessage.MeetingMessageProperties);
				Microsoft.Exchange.Data.Storage.Item.CopyCustomPublicStrings(this, meetingCancellation);
				this.UpdateSequencingProperties(isToAllAttendees, meetingCancellation, minSequenceNumber, seriesSequenceNumber);
				setBodyAndAdjustFlags(meetingCancellation);
				if (this.CalendarItemType == CalendarItemType.RecurringMaster)
				{
					CalendarItemBase.CopyPropertiesTo(this, meetingCancellation, new PropertyDefinition[]
					{
						InternalSchema.AppointmentRecurrenceBlob
					});
				}
				meetingCancellation[InternalSchema.FreeBusyStatus] = BusyType.Free;
				meetingCancellation[InternalSchema.IntendedFreeBusyStatus] = BusyType.Free;
				meetingCancellation[InternalSchema.MeetingRequestType] = MeetingMessageType.FullUpdate;
				meetingCancellation.AdjustAppointmentState();
				meetingCancellation[InternalSchema.SubjectPrefix] = ClientStrings.MeetingCancel.ToString(meetingCancellation.Session.InternalPreferedCulture);
				if (includeAttachments)
				{
					base.ReplaceAttachments(meetingCancellation);
				}
				meetingCancellation.SetOrDeleteProperty(InternalSchema.MasterGlobalObjectId, masterGoid);
				flag = true;
				result = meetingCancellation;
			}
			finally
			{
				if (!flag && meetingCancellation != null)
				{
					meetingCancellation.Dispose();
					meetingCancellation = null;
				}
			}
			return result;
		}

		// Token: 0x06002A08 RID: 10760 RVA: 0x000A6894 File Offset: 0x000A4A94
		protected void ResetAttendeeCache()
		{
			if (this.originalAttendeeArray == null)
			{
				this.originalAttendeeArray = this.GetAttendeeArray();
			}
			this.needToCalculateAttendeeDiff = true;
			this.addedAttendeeArray = null;
			this.removedAttendeeArray = null;
		}

		// Token: 0x06002A09 RID: 10761 RVA: 0x000A68C0 File Offset: 0x000A4AC0
		protected void SwapCoreObject(Item newItem)
		{
			ICoreObject coreObject = base.CoreObject;
			base.CoreObject = newItem.CoreObject;
			((IDirectPropertyBag)base.PropertyBag).Context.StoreObject = this;
			newItem.CoreObject = coreObject;
			((IDirectPropertyBag)newItem.PropertyBag).Context.StoreObject = newItem;
		}

		// Token: 0x06002A0A RID: 10762 RVA: 0x000A690C File Offset: 0x000A4B0C
		private Attendee[] GetAttendeeArray()
		{
			Attendee[] array = new Attendee[this.AttendeeCollection.Count];
			for (int i = 0; i < this.AttendeeCollection.Count; i++)
			{
				array[i] = this.AttendeeCollection[i];
			}
			return array;
		}

		// Token: 0x06002A0B RID: 10763 RVA: 0x000A6950 File Offset: 0x000A4B50
		private void CalculateAttendeeDiff()
		{
			if (!this.NeedToCalculateAttendeeDiff)
			{
				return;
			}
			CalendarItemBase.AttendeeComparer attendeeComparer = new CalendarItemBase.AttendeeComparer(base.Session as MailboxSession);
			Array.Sort(this.originalAttendeeArray, attendeeComparer);
			Attendee[] attendeeArray = this.GetAttendeeArray();
			Array.Sort(attendeeArray, attendeeComparer);
			List<Attendee> list = new List<Attendee>();
			List<Attendee> list2 = new List<Attendee>();
			int i = 0;
			int j = 0;
			while (j < this.originalAttendeeArray.Length)
			{
				if (i >= attendeeArray.Length)
				{
					break;
				}
				int num = attendeeComparer.Compare(this.originalAttendeeArray[j], attendeeArray[i]);
				if (num < 0)
				{
					list2.Add(this.originalAttendeeArray[j]);
					j++;
				}
				else if (num > 0)
				{
					list.Add(attendeeArray[i]);
					i++;
				}
				else
				{
					j++;
					i++;
				}
			}
			while (j < this.originalAttendeeArray.Length)
			{
				list2.Add(this.originalAttendeeArray[j]);
				j++;
			}
			while (i < attendeeArray.Length)
			{
				list.Add(attendeeArray[i]);
				i++;
			}
			this.removedAttendeeArray = list2.ToArray();
			this.addedAttendeeArray = list.ToArray();
			this.needToCalculateAttendeeDiff = false;
		}

		// Token: 0x17000DDC RID: 3548
		// (get) Token: 0x06002A0C RID: 10764 RVA: 0x000A6A67 File Offset: 0x000A4C67
		private bool NeedToCalculateAttendeeDiff
		{
			get
			{
				if (this.IsAttendeeListDirty)
				{
					this.needToCalculateAttendeeDiff = true;
				}
				return this.needToCalculateAttendeeDiff;
			}
		}

		// Token: 0x17000DDD RID: 3549
		// (get) Token: 0x06002A0D RID: 10765 RVA: 0x000A6A7E File Offset: 0x000A4C7E
		// (set) Token: 0x06002A0E RID: 10766 RVA: 0x000A6A86 File Offset: 0x000A4C86
		internal bool? IsAllDayEventCache
		{
			get
			{
				return this.isAllDayEventCache;
			}
			set
			{
				this.isAllDayEventCache = value;
			}
		}

		// Token: 0x06002A0F RID: 10767 RVA: 0x000A6A90 File Offset: 0x000A4C90
		protected void CreateCacheForChangeHighlight()
		{
			if (!base.IsNew)
			{
				if (this.propertyBagForChangeHighlight == null)
				{
					this.propertyBagForChangeHighlight = new MemoryPropertyBag();
					this.propertyBagForChangeHighlight.ExTimeZone = base.PropertyBag.ExTimeZone;
				}
				IDirectPropertyBag directPropertyBag = this.propertyBagForChangeHighlight;
				for (int i = 0; i < ChangeHighlightHelper.HighlightProperties.Length; i++)
				{
					object propertyValue = base.TryGetProperty(ChangeHighlightHelper.HighlightProperties[i]);
					directPropertyBag.SetValue(ChangeHighlightHelper.HighlightProperties[i], propertyValue);
				}
				if (((IDirectPropertyBag)base.PropertyBag).IsLoaded(InternalSchema.TextBody))
				{
					this.originalBodySize = base.Body.Size;
				}
			}
		}

		// Token: 0x06002A10 RID: 10768 RVA: 0x000A6B29 File Offset: 0x000A4D29
		public static void CopyPropertiesTo(StoreObject sourceItem, StoreObject targetItem, params PropertyDefinition[] properties)
		{
			CalendarItemBase.CopyPropertiesTo(sourceItem, targetItem, properties, properties);
		}

		// Token: 0x06002A11 RID: 10769 RVA: 0x000A6B34 File Offset: 0x000A4D34
		public static void CopyPropertiesTo(StoreObject sourceItem, StoreObject targetItem, PropertyDefinition[] sourceProperties, PropertyDefinition[] targetProperties)
		{
			if (sourceProperties.Length != targetProperties.Length)
			{
				throw new ArgumentException("Property arrays with different sizes");
			}
			for (int i = 0; i < sourceProperties.Length; i++)
			{
				PropertyDefinition propertyDefinition = sourceProperties[i];
				PropertyDefinition propertyDefinition2 = targetProperties[i];
				object obj = sourceItem.TryGetProperty(propertyDefinition);
				if (!PropertyError.IsPropertyError(obj))
				{
					if (obj != null)
					{
						object obj2;
						try
						{
							obj2 = targetItem.TryGetProperty(propertyDefinition2);
						}
						catch (NotInBagPropertyErrorException)
						{
							obj2 = null;
						}
						if (propertyDefinition2.Equals(InternalSchema.SentRepresentingEmailAddress) || propertyDefinition2.Equals(InternalSchema.SenderEmailAddress))
						{
							if (!CalendarItemBase.AreEqualIgnoreCase(obj2, obj))
							{
								targetItem.LocationIdentifierHelperInstance.SetLocationIdentifier(37996U);
								targetItem[propertyDefinition2] = obj;
							}
						}
						else if (!Util.ValueEquals(obj2, obj))
						{
							targetItem.LocationIdentifierHelperInstance.SetLocationIdentifier(49525U);
							targetItem[propertyDefinition2] = obj;
						}
					}
				}
				else if (PropertyError.IsPropertyValueTooBig(obj))
				{
					using (Stream stream = sourceItem.OpenPropertyStream(propertyDefinition, PropertyOpenMode.ReadOnly))
					{
						using (Stream stream2 = targetItem.OpenPropertyStream(propertyDefinition2, PropertyOpenMode.Create))
						{
							Util.StreamHandler.CopyStreamData(stream, stream2);
						}
					}
				}
			}
		}

		// Token: 0x06002A12 RID: 10770 RVA: 0x000A6C68 File Offset: 0x000A4E68
		public static string CreateWhenStringForBodyPrefix(Item item, ExTimeZone preferredTimeZone = null)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("{0} {1}", ClientStrings.WhenPart.ToString(item.Session.InternalPreferedCulture), CalendarItem.InternalWhen(item, null, true, preferredTimeZone).ToString(item.Session.InternalPreferedCulture));
			stringBuilder.AppendLine();
			string valueOrDefault = item.GetValueOrDefault<string>(InternalSchema.Location);
			if (!string.IsNullOrEmpty(valueOrDefault))
			{
				stringBuilder.AppendFormat("{0} {1}", ClientStrings.WherePart.ToString(item.Session.InternalPreferedCulture), valueOrDefault);
				stringBuilder.AppendLine();
			}
			stringBuilder.AppendLine();
			stringBuilder.Append("*~*~*~*~*~*~*~*~*~*");
			stringBuilder.AppendLine();
			return stringBuilder.ToString();
		}

		// Token: 0x17000DDE RID: 3550
		// (get) Token: 0x06002A13 RID: 10771 RVA: 0x000A6D23 File Offset: 0x000A4F23
		// (set) Token: 0x06002A14 RID: 10772 RVA: 0x000A6D2B File Offset: 0x000A4F2B
		internal bool IsCorrelated
		{
			get
			{
				return this.isCorrelated;
			}
			set
			{
				this.isCorrelated = value;
			}
		}

		// Token: 0x06002A15 RID: 10773 RVA: 0x000A6D34 File Offset: 0x000A4F34
		private static bool AreEqualIgnoreCase(object propValue, object originalValue)
		{
			return propValue is string && originalValue is string && ((string)propValue).Equals((string)originalValue, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06002A16 RID: 10774 RVA: 0x000A6D5C File Offset: 0x000A4F5C
		private void AdjustIsToAllAttendees(ref bool isToAllAttendees)
		{
			if (!isToAllAttendees && this.removedAttendeeArray != null && this.removedAttendeeArray.Length != 0)
			{
				foreach (Attendee attendee in Util.CompositeEnumerator<Attendee>(new IEnumerable<Attendee>[]
				{
					this.AttendeeCollection,
					this.removedAttendeeArray
				}))
				{
					bool? flag = attendee.Participant.IsRoutable(null);
					if (flag != null && flag.Value)
					{
						bool flag2 = attendee.IsDistributionList() ?? true;
						if (flag2)
						{
							isToAllAttendees = true;
							break;
						}
					}
				}
			}
		}

		// Token: 0x17000DDF RID: 3551
		// (get) Token: 0x06002A17 RID: 10775 RVA: 0x000A6E24 File Offset: 0x000A5024
		public GlobalObjectId GlobalObjectId
		{
			get
			{
				if (this.cachedGlobalObjectId != null)
				{
					return this.cachedGlobalObjectId;
				}
				byte[] valueOrDefault = base.GetValueOrDefault<byte[]>(CalendarItemBaseSchema.GlobalObjectId);
				if (valueOrDefault != null)
				{
					this.cachedGlobalObjectId = new GlobalObjectId(valueOrDefault);
				}
				return this.cachedGlobalObjectId;
			}
		}

		// Token: 0x06002A18 RID: 10776 RVA: 0x000A6E64 File Offset: 0x000A5064
		public static bool IsTenantToBeFixed(MailboxSession mailboxSession)
		{
			if (mailboxSession == null || mailboxSession.MailboxOwner == null)
			{
				return false;
			}
			VariantConfigurationSnapshot snapshot = VariantConfiguration.GetSnapshot(mailboxSession.MailboxOwner.GetContext(null), null, null);
			return snapshot.CalendarLogging.FixMissingMeetingBody.Enabled;
		}

		// Token: 0x06002A19 RID: 10777 RVA: 0x000A6EA8 File Offset: 0x000A50A8
		internal virtual void SendUpdateRums(UpdateRumInfo rumInfo, bool copyToSentItems)
		{
			this.CheckDisposed("SendUpdateRums");
			ExTraceGlobals.MeetingMessageTracer.Information<GlobalObjectId>((long)this.GetHashCode(), "Storage.CalendarItemBase.SendUpdateRums: GOID={0};", this.GlobalObjectId);
			if (!this.IsOrganizer())
			{
				ExTraceGlobals.MeetingMessageTracer.Information((long)this.GetHashCode(), "Storage.CalendarItemBase.SendUpdateRums: Invalid organizer, skip sending RUMs");
				return;
			}
			if (this.MeetingRequestWasSent)
			{
				MailboxSession mailboxSession = this.SendMessagesProlog();
				this.SendRumRequest(mailboxSession, rumInfo, copyToSentItems);
				base.LocationIdentifierHelperInstance.SetLocationIdentifier(49141U, LastChangeAction.UpdateRumSent);
				this.SendMessagesEpilog();
			}
		}

		// Token: 0x06002A1A RID: 10778 RVA: 0x000A6F2C File Offset: 0x000A512C
		internal void SendForwardRum(UpdateRumInfo rumInfo, bool copyToSentItems)
		{
			this.CheckDisposed("SendForwardRum");
			ExTraceGlobals.MeetingMessageTracer.Information<GlobalObjectId>((long)this.GetHashCode(), "Storage.CalendarItemBase.SendForwardRum: GOID={0};", this.GlobalObjectId);
			if (this.MeetingRequestWasSent)
			{
				ForwardCreation forwardCreation = null;
				bool flag;
				MailboxSession messageMailboxSession = this.GetMessageMailboxSession(out flag);
				ReplyForwardConfiguration replyForwardParameters = new ReplyForwardConfiguration(BodyFormat.TextHtml, ForwardCreationFlags.None, messageMailboxSession.InternalCulture);
				using (MeetingRequest meetingRequest = this.ForwardMeeting(messageMailboxSession, CalendarItemBase.GetDraftsFolderIdOrThrow(messageMailboxSession), replyForwardParameters, out forwardCreation, null, null))
				{
					meetingRequest.CopySendableParticipantsToMessage(rumInfo.AttendeeList);
					this.AdjustRumMessage(messageMailboxSession, meetingRequest, rumInfo, false);
					if (rumInfo is MissingAttendeeItemRumInfo)
					{
						meetingRequest[InternalSchema.ChangeHighlight] = ChangeHighlightProperties.BodyProps;
					}
					this.SendMessage(messageMailboxSession, meetingRequest, copyToSentItems, true);
					base.LocationIdentifierHelperInstance.SetLocationIdentifier(61941U, LastChangeAction.ForwardRumSent);
				}
			}
		}

		// Token: 0x06002A1B RID: 10779 RVA: 0x000A7014 File Offset: 0x000A5214
		internal virtual void SendCancellationRums(CancellationRumInfo rumInfo, bool copyToSentItems)
		{
			this.CheckDisposed("SendCancellationRums");
			if (this.MeetingRequestWasSent)
			{
				bool flag;
				MailboxSession messageMailboxSession = this.GetMessageMailboxSession(out flag);
				this.SendMeetingCancellations(messageMailboxSession, false, rumInfo.AttendeeList, copyToSentItems, true, rumInfo);
				base.LocationIdentifierHelperInstance.SetLocationIdentifier(37365U, LastChangeAction.CancellationRumSent);
				this.SaveWithConflictCheck(SaveMode.ResolveConflicts);
			}
		}

		// Token: 0x06002A1C RID: 10780 RVA: 0x000A7068 File Offset: 0x000A5268
		internal virtual void SendResponseRum(ResponseRumInfo rumInfo, bool copyToSentItems)
		{
			this.CheckDisposed("SendResponseRum");
			bool isCalendarDelegateAccess;
			MailboxSession messageMailboxSession = this.GetMessageMailboxSession(out isCalendarDelegateAccess);
			using (MeetingResponse meetingResponse = this.CreateResponse(messageMailboxSession, this.ResponseType, isCalendarDelegateAccess, null, null))
			{
				this.AdjustRumMessage(messageMailboxSession, meetingResponse, rumInfo, false);
				this.SendMessage(messageMailboxSession, meetingResponse, copyToSentItems, true);
			}
		}

		// Token: 0x06002A1D RID: 10781 RVA: 0x000A70DC File Offset: 0x000A52DC
		internal void SendAttendeeInquiryRum(AttendeeInquiryRumInfo rumInfo, bool copyToSentItems)
		{
			this.SendAttendeeInquiryRum(rumInfo, copyToSentItems, null);
		}

		// Token: 0x06002A1E RID: 10782 RVA: 0x000A70E8 File Offset: 0x000A52E8
		internal virtual void SendAttendeeInquiryRum(AttendeeInquiryRumInfo rumInfo, bool copyToSentItems, string subjectOverride)
		{
			this.CheckDisposed("SendAttendeeInquiryRum");
			bool flag;
			MailboxSession messageMailboxSession = this.GetMessageMailboxSession(out flag);
			using (MeetingInquiryMessage meetingInquiryMessage = MeetingInquiryMessage.Create(messageMailboxSession, CalendarItemBase.GetDraftsFolderIdOrThrow(messageMailboxSession), rumInfo))
			{
				meetingInquiryMessage.GlobalObjectId = this.GlobalObjectId;
				meetingInquiryMessage.Recipients.Add(this.Organizer);
				meetingInquiryMessage.Subject = (subjectOverride ?? base.GetValueOrDefault<string>(ItemSchema.NormalizedSubject));
				this.SendMessage(messageMailboxSession, meetingInquiryMessage, copyToSentItems, true);
			}
		}

		// Token: 0x06002A1F RID: 10783 RVA: 0x000A7174 File Offset: 0x000A5374
		internal MeetingRequest InternalCreateMeetingRequest(MailboxSession mailboxSession, bool isToAllAttendees, IList<Attendee> attendeeRecipients, Action<MeetingRequest> setBodyAndAdjustFlags, int changeHighlights, int minSequenceNumber, MeetingMessageType requestType, bool sendAttachments, string occurrencesViewPropertiesBlob, int? seriesSequenceNumber = null, byte[] masterGoid = null)
		{
			MeetingRequest meetingRequest = this.CreateNewMeetingRequest(mailboxSession);
			this.InitializeMeetingRequest(setBodyAndAdjustFlags, meetingRequest);
			if (!this.MeetingRequestWasSent)
			{
				isToAllAttendees = true;
			}
			this.UpdateSequencingProperties(isToAllAttendees, meetingRequest, minSequenceNumber, seriesSequenceNumber);
			meetingRequest.AdjustAppointmentState();
			meetingRequest[InternalSchema.ChangeHighlight] = changeHighlights;
			meetingRequest.MeetingRequestType = requestType;
			BusyType valueOrDefault = base.GetValueOrDefault<BusyType>(InternalSchema.FreeBusyStatus, BusyType.Busy);
			meetingRequest[InternalSchema.IntendedFreeBusyStatus] = valueOrDefault;
			meetingRequest[InternalSchema.FreeBusyStatus] = ((valueOrDefault != BusyType.Free) ? BusyType.Tentative : BusyType.Free);
			meetingRequest[InternalSchema.AppointmentClass] = this.ItemClass;
			meetingRequest.CopySendableParticipantsToMessage(attendeeRecipients);
			if (!isToAllAttendees)
			{
				List<BlobRecipient> list = new List<BlobRecipient>();
				foreach (Attendee attendee in this.AttendeeCollection)
				{
					if (!this.ContainsAttendeeParticipant(attendeeRecipients, attendee))
					{
						list.Add(new BlobRecipient(attendee));
					}
				}
				meetingRequest.SetUnsendableRecipients(list);
			}
			if (sendAttachments)
			{
				base.ReplaceAttachments(meetingRequest);
			}
			meetingRequest[InternalSchema.IconIndex] = CalendarItemBase.CalculateMeetingRequestIcon(meetingRequest);
			meetingRequest[InternalSchema.CalendarProcessingSteps] = 0;
			if (!string.IsNullOrEmpty(occurrencesViewPropertiesBlob))
			{
				meetingRequest.OccurrencesExceptionalViewProperties = occurrencesViewPropertiesBlob;
			}
			meetingRequest.SetOrDeleteProperty(InternalSchema.MasterGlobalObjectId, masterGoid);
			return meetingRequest;
		}

		// Token: 0x06002A20 RID: 10784 RVA: 0x000A72CC File Offset: 0x000A54CC
		protected virtual void CopyMeetingRequestProperties(MeetingRequest meetingRequest)
		{
			CalendarItemBase.CopyPropertiesTo(this, meetingRequest, MeetingMessage.MeetingMessageProperties);
			CalendarItemBase.CopyPropertiesTo(this, meetingRequest, MeetingMessage.WriteOnCreateProperties);
		}

		// Token: 0x06002A21 RID: 10785
		protected abstract MeetingRequest CreateNewMeetingRequest(MailboxSession mailboxSession);

		// Token: 0x06002A22 RID: 10786
		protected abstract MeetingCancellation CreateNewMeetingCancelation(MailboxSession mailboxSession);

		// Token: 0x06002A23 RID: 10787
		protected abstract MeetingResponse CreateNewMeetingResponse(MailboxSession mailboxSession, ResponseType responseType);

		// Token: 0x06002A24 RID: 10788
		protected abstract void SetSequencingPropertiesForForward(MeetingRequest meetingRequest);

		// Token: 0x06002A25 RID: 10789 RVA: 0x000A72E6 File Offset: 0x000A54E6
		protected virtual void InitializeMeetingRequest(Action<MeetingRequest> setBodyAndAdjustFlags, MeetingRequest meetingRequest)
		{
			this.CopyMeetingRequestProperties(meetingRequest);
			if (setBodyAndAdjustFlags != null)
			{
				setBodyAndAdjustFlags(meetingRequest);
			}
		}

		// Token: 0x06002A26 RID: 10790 RVA: 0x000A72F9 File Offset: 0x000A54F9
		protected virtual void UpdateAttendeesOnException()
		{
		}

		// Token: 0x06002A27 RID: 10791 RVA: 0x000A72FC File Offset: 0x000A54FC
		public void SaveWithConflictCheck(SaveMode saveMode)
		{
			ConflictResolutionResult conflictResolutionResult = base.Save(saveMode);
			if (conflictResolutionResult.SaveStatus == SaveResult.IrresolvableConflict)
			{
				throw new SaveConflictException(ServerStrings.ExSaveFailedBecauseOfConflicts(base.InternalObjectId), conflictResolutionResult);
			}
		}

		// Token: 0x17000DE0 RID: 3552
		// (get) Token: 0x06002A28 RID: 10792
		protected abstract bool IsInThePast { get; }

		// Token: 0x06002A29 RID: 10793 RVA: 0x000A732C File Offset: 0x000A552C
		private void SendMeetingRequests(MailboxSession mailboxSession, bool copyToSentItems, bool isToAllAttendees, int minSequenceNumber, string occurrencesViewPropertiesBlob = null, int? seriesSequenceNumber = null, byte[] masterGoid = null)
		{
			int num = 0;
			MeetingMessageType meetingMessageType = MeetingMessageType.NewMeetingRequest;
			if (this.MeetingRequestWasSent)
			{
				ChangeHighlightHelper changeHighlightHelper = MeetingRequest.CompareForChangeHighlightOnUpdatedItems(this.propertyBagForChangeHighlight, base.Body.Size, base.PropertyBag, this.originalBodySize);
				num = changeHighlightHelper.HighlightFlags;
				meetingMessageType = changeHighlightHelper.SuggestedMeetingType;
				int valueOrDefault = base.GetValueOrDefault<int>(InternalSchema.ChangeHighlight, 0);
				num |= valueOrDefault;
			}
			IList<Attendee> list = isToAllAttendees ? this.AttendeeCollection : ((IList<Attendee>)this.addedAttendeeArray);
			if (meetingMessageType == MeetingMessageType.FullUpdate)
			{
				foreach (Attendee attendee in list)
				{
					attendee.ResponseType = ResponseType.None;
					if (this.IsCalendarItemTypeOccurrenceOrException)
					{
						attendee.RecipientFlags |= RecipientFlags.ExceptionalResponse;
					}
				}
			}
			this.SendMeetingRequests(mailboxSession, copyToSentItems, isToAllAttendees, list, new Action<MeetingRequest>(this.CopyBodyWithPrefixToMeetingMessage), num, minSequenceNumber, meetingMessageType, true, false, occurrencesViewPropertiesBlob, seriesSequenceNumber, masterGoid);
		}

		// Token: 0x06002A2A RID: 10794 RVA: 0x000A7428 File Offset: 0x000A5628
		private void CopyBodyWithPrefixToMeetingMessage(MeetingMessage message)
		{
			ReplyForwardConfiguration configuration = new ReplyForwardConfiguration(base.Body.Format);
			ReplyForwardCommon.CopyBodyWithPrefix(base.Body, message.Body, configuration, default(BodyConversionCallbacks));
		}

		// Token: 0x06002A2B RID: 10795 RVA: 0x000A74A0 File Offset: 0x000A56A0
		private void SendRumRequest(MailboxSession mailboxSession, UpdateRumInfo rumInfo, bool copyToSentItems)
		{
			Action<MeetingRequest> setBodyAndAdjustFlags = (rumInfo is MissingAttendeeItemRumInfo && CalendarItemBase.IsTenantToBeFixed(mailboxSession)) ? delegate(MeetingRequest meetingRequest)
			{
				this.AdjustRumMessage2(mailboxSession, meetingRequest, rumInfo);
			} : delegate(MeetingRequest meetingRequest)
			{
				this.AdjustRumMessage(mailboxSession, meetingRequest, rumInfo, false);
			};
			this.SendMeetingRequests(mailboxSession, copyToSentItems, false, rumInfo.AttendeeList, setBodyAndAdjustFlags, (rumInfo is MissingAttendeeItemRumInfo) ? 128 : 0, rumInfo.AttendeeRequiredSequenceNumber, MeetingMessageType.InformationalUpdate, false, true, null, null, null);
		}

		// Token: 0x06002A2C RID: 10796 RVA: 0x000A754E File Offset: 0x000A574E
		protected void AdjustRumMessage2(MailboxSession mailboxSession, MeetingMessage message, RumInfo rumInfo)
		{
			this.CopyBodyWithPrefixToMeetingMessage(message);
			this.AdjustRumMessage(mailboxSession, message, rumInfo, true);
		}

		// Token: 0x06002A2D RID: 10797 RVA: 0x000A7564 File Offset: 0x000A5764
		protected void AdjustRumMessage(MailboxSession mailboxSession, MessageItem message, RumInfo rumInfo, bool skipBodyUpdate = false)
		{
			RumDecorator rumDecorator = RumDecorator.CreateInstance(rumInfo);
			rumDecorator.AdjustRumMessage(mailboxSession, message, rumInfo, null, skipBodyUpdate);
		}

		// Token: 0x06002A2E RID: 10798 RVA: 0x000A7584 File Offset: 0x000A5784
		private void SendMeetingRequests(MailboxSession mailboxSession, bool copyToSentItems, bool isToAllAttendees, IList<Attendee> attendeeRecipients, Action<MeetingRequest> setBodyAndAdjustFlags, int changeHighlights, int minSequenceNumber, MeetingMessageType requestType, bool sendAttachments, bool ignoreSendAsRight, string occurrencesViewPropertiesBlob = null, int? seriesSequenceNumber = null, byte[] masterGoid = null)
		{
			if (attendeeRecipients == null || attendeeRecipients.Count == 0)
			{
				ExTraceGlobals.MeetingMessageTracer.Information<GlobalObjectId>((long)this.GetHashCode(), "Storage.CalendarItemBase.SendMeetingRequests: GOID={0}; There are no attendees to send to.", this.GlobalObjectId);
				return;
			}
			using (MeetingRequest meetingRequest = this.InternalCreateMeetingRequest(mailboxSession, isToAllAttendees, attendeeRecipients, setBodyAndAdjustFlags, changeHighlights, minSequenceNumber, requestType, sendAttachments, occurrencesViewPropertiesBlob, seriesSequenceNumber, masterGoid))
			{
				this.SendMessage(mailboxSession, meetingRequest, copyToSentItems, ignoreSendAsRight);
				base.LocationIdentifierHelperInstance.SetLocationIdentifier(53621U);
				this[InternalSchema.MeetingRequestWasSent] = true;
				this.CreateCacheForChangeHighlight();
			}
		}

		// Token: 0x06002A2F RID: 10799 RVA: 0x000A7624 File Offset: 0x000A5824
		private bool ContainsAttendeeParticipant(IList<Attendee> list, Attendee attendee)
		{
			if (attendee == null)
			{
				throw new ArgumentNullException("attendee");
			}
			if (attendee.Participant == null)
			{
				throw new ArgumentNullException("attendee.Participant");
			}
			foreach (Attendee attendee2 in list)
			{
				if (attendee2 == attendee)
				{
					return true;
				}
				if (attendee2 != null && attendee2.Participant != null && (attendee2.Participant == attendee.Participant || Participant.HasSameEmail(attendee2.Participant, attendee.Participant, base.Session as MailboxSession, true)))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002A30 RID: 10800 RVA: 0x000A76E0 File Offset: 0x000A58E0
		private MessageItem ForwardAppointment(MailboxSession session, StoreObjectId parentFolderId, ReplyForwardConfiguration replyForwardParameters)
		{
			MessageItem messageItem = null;
			bool flag = false;
			MessageItem result;
			try
			{
				messageItem = MessageItem.Create(session, parentFolderId);
				messageItem[InternalSchema.SubjectPrefix] = ClientStrings.ItemForward.ToString(session.InternalPreferedCulture);
				messageItem[InternalSchema.NormalizedSubject] = base.GetValueOrDefault<string>(InternalSchema.NormalizedSubjectInternal, string.Empty);
				using (Attachment attachment = messageItem.AttachmentCollection.AddExistingItem(this))
				{
					attachment.Save();
				}
				flag = true;
				result = messageItem;
			}
			finally
			{
				if (!flag && messageItem != null)
				{
					messageItem.Dispose();
					messageItem = null;
				}
			}
			return result;
		}

		// Token: 0x06002A31 RID: 10801 RVA: 0x000A7784 File Offset: 0x000A5984
		private MeetingRequest ForwardMeeting(MailboxSession session, StoreObjectId parentFolderId, ReplyForwardConfiguration replyForwardParameters, int? seriesSequenceNumber, string occurrencesViewPropertiesBlob)
		{
			bool flag = false;
			ForwardCreation forwardCreation = null;
			MeetingRequest meetingRequest = null;
			try
			{
				meetingRequest = this.ForwardMeeting(session, parentFolderId, replyForwardParameters, out forwardCreation, seriesSequenceNumber, occurrencesViewPropertiesBlob);
				forwardCreation.PopulateContents();
				meetingRequest.AdjustAppointmentStateFlagsForForward();
				flag = true;
			}
			finally
			{
				if (!flag && meetingRequest != null)
				{
					meetingRequest.Dispose();
					meetingRequest = null;
				}
			}
			return meetingRequest;
		}

		// Token: 0x06002A32 RID: 10802 RVA: 0x000A77D8 File Offset: 0x000A59D8
		private MeetingRequest ForwardMeeting(MailboxSession session, StoreObjectId parentFolderId, ReplyForwardConfiguration replyForwardParameters, out ForwardCreation forward, int? seriesSequenceNumber = null, string occurrencesViewPropertiesBlob = null)
		{
			ExTraceGlobals.MeetingMessageTracer.Information<GlobalObjectId>((long)this.GetHashCode(), "Storage.CalendarItemBase.ForwardMeeting: GOID={0}", this.GlobalObjectId);
			MeetingRequest meetingRequest = null;
			bool flag = false;
			MeetingRequest result;
			try
			{
				meetingRequest = this.CreateNewMeetingRequest(session);
				forward = new ForwardCreation(this, meetingRequest, replyForwardParameters);
				forward.PopulateProperties(false);
				List<BlobRecipient> list = new List<BlobRecipient>();
				foreach (Attendee attendee in this.AttendeeCollection)
				{
					if ((attendee.RecipientFlags & RecipientFlags.Organizer) != RecipientFlags.Organizer)
					{
						list.Add(new BlobRecipient(attendee));
					}
				}
				meetingRequest.SetUnsendableRecipients(list);
				this.SetSequencingPropertiesForForward(meetingRequest);
				if (seriesSequenceNumber != null)
				{
					meetingRequest.SeriesSequenceNumber = seriesSequenceNumber.Value;
				}
				if (!string.IsNullOrEmpty(occurrencesViewPropertiesBlob))
				{
					meetingRequest.OccurrencesExceptionalViewProperties = occurrencesViewPropertiesBlob;
				}
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

		// Token: 0x06002A33 RID: 10803 RVA: 0x000A78D8 File Offset: 0x000A5AD8
		private MailboxSession SendMessagesProlog()
		{
			if (!this.IsOrganizer())
			{
				throw new InvalidOperationException(ServerStrings.ExCannotSendMeetingMessages);
			}
			this.IsMeeting = true;
			((IAttendeeCollectionImpl)this.AttendeeCollection).Cleanup();
			((IAttendeeCollectionImpl)this.AttendeeCollection).LoadIsDistributionList();
			this.SaveWithConflictCheck(SaveMode.ResolveConflicts);
			base.Load(null);
			this.CalculateAttendeeDiff();
			bool flag;
			return this.GetMessageMailboxSession(out flag);
		}

		// Token: 0x06002A34 RID: 10804 RVA: 0x000A7940 File Offset: 0x000A5B40
		private void SendMessagesEpilog()
		{
			this.originalAttendeeArray = null;
			this.ResetAttendeeCache();
			this.SaveWithConflictCheck(SaveMode.ResolveConflicts);
		}

		// Token: 0x040017F8 RID: 6136
		protected const bool DefaultAutoCaptureClientIntent = false;

		// Token: 0x040017F9 RID: 6137
		protected const bool DefaultCopyToSentItems = true;

		// Token: 0x040017FA RID: 6138
		protected const string DefaultOccurrencesViewPropertiesBlob = null;

		// Token: 0x040017FB RID: 6139
		private const int DefaultPublishingMonths = 2;

		// Token: 0x040017FC RID: 6140
		private const int MapiBusyTypeFree = 0;

		// Token: 0x040017FD RID: 6141
		private const int MapiBusyTypeTentative = 1;

		// Token: 0x040017FE RID: 6142
		private const int MapiBusyTypeBusy = 2;

		// Token: 0x040017FF RID: 6143
		private const int MapiBusyTypeOOF = 3;

		// Token: 0x04001800 RID: 6144
		private const BusyType DefaultBusyType = BusyType.Busy;

		// Token: 0x04001801 RID: 6145
		private static readonly PropertyDefinition[] AppointmentTombstomeDefinition = new PropertyDefinition[]
		{
			InternalSchema.AppointmentTombstones,
			InternalSchema.OutlookFreeBusyMonthCount
		};

		// Token: 0x04001802 RID: 6146
		private Attendee[] originalAttendeeArray;

		// Token: 0x04001803 RID: 6147
		private Attendee[] addedAttendeeArray;

		// Token: 0x04001804 RID: 6148
		private Attendee[] removedAttendeeArray;

		// Token: 0x04001805 RID: 6149
		private bool needToCalculateAttendeeDiff;

		// Token: 0x04001806 RID: 6150
		private bool? isAllDayEventCache;

		// Token: 0x04001807 RID: 6151
		private MemoryPropertyBag propertyBagForChangeHighlight;

		// Token: 0x04001808 RID: 6152
		private long originalBodySize;

		// Token: 0x04001809 RID: 6153
		private VersionedId associatedId;

		// Token: 0x0400180A RID: 6154
		private Reminders<EventTimeBasedInboxReminder> eventTimeBasedInboxReminders;

		// Token: 0x0400180B RID: 6155
		private RemindersState<EventTimeBasedInboxReminderState> eventTimeBasedInboxRemindersState;

		// Token: 0x0400180C RID: 6156
		private GlobalObjectId cachedGlobalObjectId;

		// Token: 0x0400180D RID: 6157
		private bool isCorrelated;

		// Token: 0x0400180E RID: 6158
		internal static ExDateTime OutlookRtmNone = new ExDateTime(ExTimeZone.UtcTimeZone, 4501, 1, 1, 0, 0, 0);

		// Token: 0x020003A4 RID: 932
		private class AttendeeComparer : IComparer
		{
			// Token: 0x06002A36 RID: 10806 RVA: 0x000A799B File Offset: 0x000A5B9B
			public AttendeeComparer(MailboxSession session)
			{
				this.session = session;
			}

			// Token: 0x06002A37 RID: 10807 RVA: 0x000A79AC File Offset: 0x000A5BAC
			public int Compare(object x, object y)
			{
				Attendee attendee = x as Attendee;
				Attendee attendee2 = y as Attendee;
				if (attendee == null || attendee2 == null)
				{
					throw new ArgumentException();
				}
				if (Participant.HasSameEmail(attendee.Participant, attendee2.Participant, this.session, false))
				{
					return 0;
				}
				if (attendee.Participant == null)
				{
					return -1;
				}
				if (attendee2.Participant == null)
				{
					return 1;
				}
				if (attendee.Participant.EmailAddress == null != (attendee2.Participant.EmailAddress == null))
				{
					if (attendee.Participant.EmailAddress == null)
					{
						return -1;
					}
					return 1;
				}
				else
				{
					if (attendee.Participant.EmailAddress == null)
					{
						return string.Compare(attendee.Participant.ToString(), attendee2.Participant.ToString(), StringComparison.CurrentCulture);
					}
					return string.Compare(attendee.Participant.EmailAddress, attendee2.Participant.EmailAddress, StringComparison.CurrentCulture);
				}
			}

			// Token: 0x0400180F RID: 6159
			private MailboxSession session;
		}
	}
}
