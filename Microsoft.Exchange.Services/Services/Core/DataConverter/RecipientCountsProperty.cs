using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x0200017F RID: 383
	internal sealed class RecipientCountsProperty : ComplexPropertyBase, IToServiceObjectCommand, IPropertyCommand
	{
		// Token: 0x06000AF0 RID: 2800 RVA: 0x00034960 File Offset: 0x00032B60
		public RecipientCountsProperty(CommandContext commandContext) : base(commandContext)
		{
		}

		// Token: 0x06000AF1 RID: 2801 RVA: 0x00034969 File Offset: 0x00032B69
		public static RecipientCountsProperty CreateCommand(CommandContext commandContext)
		{
			return new RecipientCountsProperty(commandContext);
		}

		// Token: 0x06000AF2 RID: 2802 RVA: 0x00034971 File Offset: 0x00032B71
		public void ToXml()
		{
			throw new InvalidOperationException("RecipientCountsProperty.ToXml should not be called.");
		}

		// Token: 0x06000AF3 RID: 2803 RVA: 0x00034980 File Offset: 0x00032B80
		public void ToServiceObject()
		{
			ToServiceObjectCommandSettings commandSettings = base.GetCommandSettings<ToServiceObjectCommandSettings>();
			ServiceObject serviceObject = commandSettings.ServiceObject;
			PropertyInformation propertyInformation = this.commandContext.PropertyInformation;
			MessageItem messageItem = commandSettings.StoreObject as MessageItem;
			if (messageItem == null)
			{
				return;
			}
			RecipientCountsType recipientCountsType = new RecipientCountsType();
			recipientCountsType.ToRecipientsCount = 0;
			recipientCountsType.CcRecipientsCount = 0;
			recipientCountsType.BccRecipientsCount = 0;
			foreach (Recipient recipient in messageItem.Recipients)
			{
				if (recipient.Participant != null)
				{
					switch (recipient.RecipientItemType)
					{
					case RecipientItemType.To:
						recipientCountsType.ToRecipientsCount++;
						break;
					case RecipientItemType.Cc:
						recipientCountsType.CcRecipientsCount++;
						break;
					case RecipientItemType.Bcc:
						recipientCountsType.BccRecipientsCount++;
						break;
					}
				}
			}
			serviceObject.PropertyBag[propertyInformation] = recipientCountsType;
		}
	}
}
