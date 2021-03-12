using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200037C RID: 892
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class Attendee : RecipientBase
	{
		// Token: 0x06002765 RID: 10085 RVA: 0x0009D85A File Offset: 0x0009BA5A
		internal Attendee(CoreRecipient coreRecipient) : base(coreRecipient)
		{
		}

		// Token: 0x06002766 RID: 10086 RVA: 0x0009D864 File Offset: 0x0009BA64
		public bool IsSendable()
		{
			bool? flag = base.Participant.IsRoutable(null);
			return flag != null && flag.Value && !this.IsOrganizer;
		}

		// Token: 0x17000D0F RID: 3343
		// (get) Token: 0x06002767 RID: 10087 RVA: 0x0009D89B File Offset: 0x0009BA9B
		public bool IsOrganizer
		{
			get
			{
				return (base.RecipientFlags & RecipientFlags.Organizer) == RecipientFlags.Organizer;
			}
		}

		// Token: 0x17000D10 RID: 3344
		// (get) Token: 0x06002768 RID: 10088 RVA: 0x0009D8A8 File Offset: 0x0009BAA8
		// (set) Token: 0x06002769 RID: 10089 RVA: 0x0009D8B5 File Offset: 0x0009BAB5
		public ResponseType ResponseType
		{
			get
			{
				return base.GetValueOrDefault<ResponseType>(InternalSchema.RecipientTrackStatus);
			}
			set
			{
				EnumValidator.ThrowIfInvalid<ResponseType>(value, "value");
				base[InternalSchema.RecipientTrackStatus] = (int)value;
			}
		}

		// Token: 0x17000D11 RID: 3345
		// (get) Token: 0x0600276A RID: 10090 RVA: 0x0009D8D3 File Offset: 0x0009BAD3
		// (set) Token: 0x0600276B RID: 10091 RVA: 0x0009D8E0 File Offset: 0x0009BAE0
		public AttendeeType AttendeeType
		{
			get
			{
				return Attendee.RecipientItemTypeToAttendeeType(base.RecipientItemType);
			}
			set
			{
				EnumValidator.ThrowIfInvalid<AttendeeType>(value, "value");
				base.RecipientItemType = Attendee.AttendeeTypeToRecipientItemType(value);
			}
		}

		// Token: 0x17000D12 RID: 3346
		// (get) Token: 0x0600276C RID: 10092 RVA: 0x0009D8FC File Offset: 0x0009BAFC
		// (set) Token: 0x0600276D RID: 10093 RVA: 0x0009D92E File Offset: 0x0009BB2E
		public ExDateTime ReplyTime
		{
			get
			{
				ExDateTime valueOrDefault = base.GetValueOrDefault<ExDateTime>(InternalSchema.RecipientTrackStatusTime, ExDateTime.MinValue);
				if (!ExDateTime.Equals(valueOrDefault, CalendarItemBase.OutlookRtmNone))
				{
					return valueOrDefault;
				}
				return ExDateTime.MinValue;
			}
			set
			{
				base[InternalSchema.RecipientTrackStatusTime] = value;
			}
		}

		// Token: 0x17000D13 RID: 3347
		// (get) Token: 0x0600276E RID: 10094 RVA: 0x0009D941 File Offset: 0x0009BB41
		public ExDateTime ProposedStart
		{
			get
			{
				return base.GetValueOrDefault<ExDateTime>(InternalSchema.RecipientProposedStartTime, CalendarItemBase.OutlookRtmNone);
			}
		}

		// Token: 0x17000D14 RID: 3348
		// (get) Token: 0x0600276F RID: 10095 RVA: 0x0009D953 File Offset: 0x0009BB53
		public ExDateTime ProposedEnd
		{
			get
			{
				return base.GetValueOrDefault<ExDateTime>(InternalSchema.RecipientProposedEndTime, CalendarItemBase.OutlookRtmNone);
			}
		}

		// Token: 0x17000D15 RID: 3349
		// (get) Token: 0x06002770 RID: 10096 RVA: 0x0009D968 File Offset: 0x0009BB68
		public bool HasTimeProposal
		{
			get
			{
				return !this.ProposedStart.Equals(CalendarItemBase.OutlookRtmNone) && !this.ProposedEnd.Equals(CalendarItemBase.OutlookRtmNone);
			}
		}

		// Token: 0x06002771 RID: 10097 RVA: 0x0009D9A4 File Offset: 0x0009BBA4
		internal static AttendeeType RecipientItemTypeToAttendeeType(RecipientItemType type)
		{
			switch (type)
			{
			case RecipientItemType.To:
				return AttendeeType.Required;
			case RecipientItemType.Cc:
				return AttendeeType.Optional;
			case RecipientItemType.Bcc:
				return AttendeeType.Resource;
			default:
				ExTraceGlobals.StorageTracer.TraceDebug<RecipientItemType>(0L, "AttendeeType::MapiRecipientTypeToXsoAttendeeType. Wrong Recipienttype: {0}", type);
				return AttendeeType.Required;
			}
		}

		// Token: 0x06002772 RID: 10098 RVA: 0x0009D9E4 File Offset: 0x0009BBE4
		internal static RecipientItemType AttendeeTypeToRecipientItemType(AttendeeType attendeeType)
		{
			switch (attendeeType)
			{
			case AttendeeType.Required:
				return RecipientItemType.To;
			case AttendeeType.Optional:
				return RecipientItemType.Cc;
			case AttendeeType.Resource:
				return RecipientItemType.Bcc;
			default:
				ExTraceGlobals.StorageTracer.TraceDebug<AttendeeType>(0L, "AttendeeType::AttendeeTypeToRecipientType. Wrong Attendee.Type: {0}", attendeeType);
				throw new ArgumentException(ServerStrings.ExInvalidAttendeeType(attendeeType));
			}
		}

		// Token: 0x06002773 RID: 10099 RVA: 0x0009DA36 File Offset: 0x0009BC36
		internal static void SetDefaultAttendeeProperties(CoreRecipient coreRecipient)
		{
			RecipientBase.SetDefaultRecipientBaseProperties(coreRecipient);
			coreRecipient.RecipientItemType = Attendee.AttendeeTypeToRecipientItemType(AttendeeType.Required);
			coreRecipient.PropertyBag[InternalSchema.RecipientTrackStatus] = 0;
		}

		// Token: 0x06002774 RID: 10100 RVA: 0x0009DA60 File Offset: 0x0009BC60
		internal string GetAttendeeKey()
		{
			return (base.Participant.RoutingType + ":" + base.Participant.EmailAddress).ToUpper();
		}
	}
}
