using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x020000EE RID: 238
	internal sealed class OrganizerProperty : SingleRecipientPropertyBase
	{
		// Token: 0x0600067D RID: 1661 RVA: 0x00021950 File Offset: 0x0001FB50
		private OrganizerProperty(CommandContext commandContext) : base(commandContext)
		{
		}

		// Token: 0x0600067E RID: 1662 RVA: 0x00021959 File Offset: 0x0001FB59
		public static OrganizerProperty CreateCommand(CommandContext commandContext)
		{
			return new OrganizerProperty(commandContext);
		}

		// Token: 0x0600067F RID: 1663 RVA: 0x00021964 File Offset: 0x0001FB64
		protected override Participant GetParticipant(Item storeItem)
		{
			CalendarItemBase calendarItemBase = storeItem as CalendarItemBase;
			if (calendarItemBase == null)
			{
				calendarItemBase = ((MeetingRequest)storeItem).GetCachedEmbeddedItem();
				return calendarItemBase.Organizer;
			}
			return calendarItemBase.Organizer;
		}

		// Token: 0x06000680 RID: 1664 RVA: 0x00021994 File Offset: 0x0001FB94
		protected override void SetParticipant(Item storeItem, Participant participant)
		{
		}

		// Token: 0x06000681 RID: 1665 RVA: 0x00021996 File Offset: 0x0001FB96
		protected override PropertyDefinition GetParticipantDisplayNamePropertyDefinition()
		{
			return CalendarItemBaseSchema.OrganizerDisplayName;
		}

		// Token: 0x06000682 RID: 1666 RVA: 0x0002199D File Offset: 0x0001FB9D
		protected override PropertyDefinition GetParticipantEmailAddressPropertyDefinition()
		{
			return CalendarItemBaseSchema.OrganizerEmailAddress;
		}

		// Token: 0x06000683 RID: 1667 RVA: 0x000219A4 File Offset: 0x0001FBA4
		protected override PropertyDefinition GetParticipantRoutingTypePropertyDefinition()
		{
			return CalendarItemBaseSchema.OrganizerType;
		}

		// Token: 0x06000684 RID: 1668 RVA: 0x000219AB File Offset: 0x0001FBAB
		protected override PropertyDefinition GetParticipantSipUriPropertyDefinition()
		{
			return ParticipantSchema.SipUri;
		}
	}
}
