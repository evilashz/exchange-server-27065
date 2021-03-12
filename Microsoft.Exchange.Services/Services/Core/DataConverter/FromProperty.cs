using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x0200017C RID: 380
	internal class FromProperty : SingleRecipientPropertyBase, ISetUpdateCommand, IDeleteUpdateCommand, IUpdateCommand, IPropertyCommand
	{
		// Token: 0x06000AD8 RID: 2776 RVA: 0x00034634 File Offset: 0x00032834
		public FromProperty(CommandContext commandContext) : base(commandContext)
		{
		}

		// Token: 0x06000AD9 RID: 2777 RVA: 0x00034640 File Offset: 0x00032840
		protected override Participant GetParticipant(Item storeItem)
		{
			MessageItem messageItem = storeItem as MessageItem;
			if (messageItem == null)
			{
				return null;
			}
			return messageItem.From;
		}

		// Token: 0x06000ADA RID: 2778 RVA: 0x00034660 File Offset: 0x00032860
		protected override void SetParticipant(Item storeItem, Participant participant)
		{
			MessageItem messageItem = storeItem as MessageItem;
			if (messageItem == null)
			{
				throw new InvalidPropertyRequestException(this.commandContext.PropertyInformation.PropertyPath);
			}
			messageItem.From = participant;
		}

		// Token: 0x06000ADB RID: 2779 RVA: 0x00034694 File Offset: 0x00032894
		protected override PropertyDefinition GetParticipantDisplayNamePropertyDefinition()
		{
			return ItemSchema.SentRepresentingDisplayName;
		}

		// Token: 0x06000ADC RID: 2780 RVA: 0x0003469B File Offset: 0x0003289B
		protected override PropertyDefinition GetParticipantEmailAddressPropertyDefinition()
		{
			return ItemSchema.SentRepresentingEmailAddress;
		}

		// Token: 0x06000ADD RID: 2781 RVA: 0x000346A2 File Offset: 0x000328A2
		protected override PropertyDefinition GetParticipantRoutingTypePropertyDefinition()
		{
			return ItemSchema.SentRepresentingType;
		}

		// Token: 0x06000ADE RID: 2782 RVA: 0x000346A9 File Offset: 0x000328A9
		protected override PropertyDefinition GetParticipantSipUriPropertyDefinition()
		{
			return ParticipantSchema.SipUri;
		}

		// Token: 0x06000ADF RID: 2783 RVA: 0x000346B0 File Offset: 0x000328B0
		protected override Participant GetParticipantFromAddress(Item item, EmailAddressWrapper address)
		{
			object obj = null;
			try
			{
				obj = item.TryGetProperty(MessageItemSchema.SharingInstanceGuid);
			}
			catch (NotInBagPropertyErrorException)
			{
			}
			if (obj == null || obj is PropertyError)
			{
				return base.GetParticipantFromAddress(item, address);
			}
			return new Participant(address.Name, address.EmailAddress, address.RoutingType);
		}

		// Token: 0x06000AE0 RID: 2784 RVA: 0x0003470C File Offset: 0x0003290C
		public override void SetUpdate(SetPropertyUpdate setPropertyUpdate, UpdateCommandSettings updateCommandSettings)
		{
			MessageItem messageItem = updateCommandSettings.StoreObject as MessageItem;
			if (messageItem != null && messageItem.IsDraft)
			{
				ServiceObject serviceObject = setPropertyUpdate.ServiceObject;
				SingleRecipientType valueOrDefault = serviceObject.GetValueOrDefault<SingleRecipientType>(this.commandContext.PropertyInformation);
				this.SetParticipant(messageItem, this.GetParticipantFromAddress(messageItem, valueOrDefault.Mailbox));
				return;
			}
			throw new InvalidPropertySetException(CoreResources.IDs.MessageMessageIsNotDraft, updateCommandSettings.PropertyUpdate.PropertyPath);
		}

		// Token: 0x06000AE1 RID: 2785 RVA: 0x00034778 File Offset: 0x00032978
		public override void DeleteUpdate(DeletePropertyUpdate deletePropertyUpdate, UpdateCommandSettings updateCommandSettings)
		{
			MessageItem messageItem = updateCommandSettings.StoreObject as MessageItem;
			if (messageItem != null && messageItem.IsDraft)
			{
				messageItem.From = null;
				return;
			}
			throw new InvalidPropertyDeleteException(CoreResources.IDs.MessageMessageIsNotDraft, updateCommandSettings.PropertyUpdate.PropertyPath);
		}

		// Token: 0x06000AE2 RID: 2786 RVA: 0x000347BE File Offset: 0x000329BE
		public static FromProperty CreateCommand(CommandContext commandContext)
		{
			return new FromProperty(commandContext);
		}
	}
}
