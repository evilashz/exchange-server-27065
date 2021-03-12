using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x0200017E RID: 382
	internal sealed class ParticipantProperty : SingleRecipientPropertyBase, IToXmlCommand, IPropertyCommand
	{
		// Token: 0x06000AE7 RID: 2791 RVA: 0x0003489A File Offset: 0x00032A9A
		private ParticipantProperty(CommandContext commandContext, PropertyDefinition displayNamePropDef, PropertyDefinition routingTypePropDef, PropertyDefinition emailAddressPropDef, PropertyDefinition participantPropDef, PropertyDefinition sipUriPropDef) : base(commandContext)
		{
			this.displayNamePropertyDefinition = displayNamePropDef;
			this.routingTypePropertyDefinition = routingTypePropDef;
			this.emailAddressPropertyDefinition = emailAddressPropDef;
			this.participantPropertyDefinition = participantPropDef;
			this.sipUriPropertyDefinition = sipUriPropDef;
		}

		// Token: 0x06000AE8 RID: 2792 RVA: 0x000348CC File Offset: 0x00032ACC
		protected override Participant GetParticipant(Item storeItem)
		{
			Participant result = null;
			if (PropertyCommand.StorePropertyExists(storeItem, this.participantPropertyDefinition))
			{
				result = (storeItem[this.participantPropertyDefinition] as Participant);
			}
			return result;
		}

		// Token: 0x06000AE9 RID: 2793 RVA: 0x000348FC File Offset: 0x00032AFC
		protected override void SetParticipant(Item storeItem, Participant participant)
		{
		}

		// Token: 0x06000AEA RID: 2794 RVA: 0x000348FE File Offset: 0x00032AFE
		protected override PropertyDefinition GetParticipantDisplayNamePropertyDefinition()
		{
			return this.displayNamePropertyDefinition;
		}

		// Token: 0x06000AEB RID: 2795 RVA: 0x00034906 File Offset: 0x00032B06
		protected override PropertyDefinition GetParticipantEmailAddressPropertyDefinition()
		{
			return this.emailAddressPropertyDefinition;
		}

		// Token: 0x06000AEC RID: 2796 RVA: 0x0003490E File Offset: 0x00032B0E
		protected override PropertyDefinition GetParticipantRoutingTypePropertyDefinition()
		{
			return this.routingTypePropertyDefinition;
		}

		// Token: 0x06000AED RID: 2797 RVA: 0x00034916 File Offset: 0x00032B16
		protected override PropertyDefinition GetParticipantSipUriPropertyDefinition()
		{
			return this.sipUriPropertyDefinition;
		}

		// Token: 0x06000AEE RID: 2798 RVA: 0x0003491E File Offset: 0x00032B1E
		public static ParticipantProperty CreateCommandForReceivedBy(CommandContext commandContext)
		{
			return new ParticipantProperty(commandContext, MessageItemSchema.ReceivedByName, MessageItemSchema.ReceivedByAddrType, MessageItemSchema.ReadReceiptEmailAddress, MessageItemSchema.ReceivedBy, ParticipantSchema.SipUri);
		}

		// Token: 0x06000AEF RID: 2799 RVA: 0x0003493F File Offset: 0x00032B3F
		public static ParticipantProperty CreateCommandForReceivedRepresenting(CommandContext commandContext)
		{
			return new ParticipantProperty(commandContext, MessageItemSchema.ReceivedRepresentingDisplayName, MessageItemSchema.ReceivedRepresentingAddressType, MessageItemSchema.ReceivedRepresentingEmailAddress, MessageItemSchema.ReceivedRepresenting, ParticipantSchema.SipUri);
		}

		// Token: 0x040007DC RID: 2012
		private PropertyDefinition displayNamePropertyDefinition;

		// Token: 0x040007DD RID: 2013
		private PropertyDefinition routingTypePropertyDefinition;

		// Token: 0x040007DE RID: 2014
		private PropertyDefinition emailAddressPropertyDefinition;

		// Token: 0x040007DF RID: 2015
		private PropertyDefinition participantPropertyDefinition;

		// Token: 0x040007E0 RID: 2016
		private PropertyDefinition sipUriPropertyDefinition;
	}
}
