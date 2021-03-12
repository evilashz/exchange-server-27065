using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x02000182 RID: 386
	internal class SenderProperty : SingleRecipientPropertyBase
	{
		// Token: 0x06000B04 RID: 2820 RVA: 0x00034DE4 File Offset: 0x00032FE4
		public SenderProperty(CommandContext commandContext) : base(commandContext)
		{
		}

		// Token: 0x06000B05 RID: 2821 RVA: 0x00034DF0 File Offset: 0x00032FF0
		protected override Participant GetParticipant(Item storeItem)
		{
			MessageItem messageItem = storeItem as MessageItem;
			if (messageItem == null)
			{
				return null;
			}
			return messageItem.Sender;
		}

		// Token: 0x06000B06 RID: 2822 RVA: 0x00034E10 File Offset: 0x00033010
		protected override void SetParticipant(Item storeItem, Participant participant)
		{
			MessageItem messageItem = storeItem as MessageItem;
			if (messageItem == null)
			{
				throw new InvalidPropertyRequestException(this.commandContext.PropertyInformation.PropertyPath);
			}
			messageItem.Sender = participant;
		}

		// Token: 0x06000B07 RID: 2823 RVA: 0x00034E44 File Offset: 0x00033044
		protected override PropertyDefinition GetParticipantDisplayNamePropertyDefinition()
		{
			return MessageItemSchema.SenderDisplayName;
		}

		// Token: 0x06000B08 RID: 2824 RVA: 0x00034E4B File Offset: 0x0003304B
		protected override PropertyDefinition GetParticipantEmailAddressPropertyDefinition()
		{
			return MessageItemSchema.SenderEmailAddress;
		}

		// Token: 0x06000B09 RID: 2825 RVA: 0x00034E52 File Offset: 0x00033052
		protected override PropertyDefinition GetParticipantRoutingTypePropertyDefinition()
		{
			return MessageItemSchema.SenderAddressType;
		}

		// Token: 0x06000B0A RID: 2826 RVA: 0x00034E59 File Offset: 0x00033059
		protected override PropertyDefinition GetParticipantSipUriPropertyDefinition()
		{
			return ParticipantSchema.SipUri;
		}

		// Token: 0x06000B0B RID: 2827 RVA: 0x00034E60 File Offset: 0x00033060
		public static SenderProperty CreateCommand(CommandContext commandContext)
		{
			return new SenderProperty(commandContext);
		}
	}
}
