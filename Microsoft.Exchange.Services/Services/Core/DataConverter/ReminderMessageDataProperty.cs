using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x02000180 RID: 384
	internal sealed class ReminderMessageDataProperty : ComplexPropertyBase, IToServiceObjectCommand, IPropertyCommand
	{
		// Token: 0x06000AF4 RID: 2804 RVA: 0x00034A84 File Offset: 0x00032C84
		public ReminderMessageDataProperty(CommandContext commandContext) : base(commandContext)
		{
		}

		// Token: 0x06000AF5 RID: 2805 RVA: 0x00034A8D File Offset: 0x00032C8D
		public static ReminderMessageDataProperty CreateCommand(CommandContext commandContext)
		{
			return new ReminderMessageDataProperty(commandContext);
		}

		// Token: 0x06000AF6 RID: 2806 RVA: 0x00034A95 File Offset: 0x00032C95
		public void ToXml()
		{
			throw new InvalidOperationException("ApprovalRequestDataProperty.ToXml should not be called.");
		}

		// Token: 0x06000AF7 RID: 2807 RVA: 0x00034AA4 File Offset: 0x00032CA4
		public void ToServiceObject()
		{
			ToServiceObjectCommandSettings commandSettings = base.GetCommandSettings<ToServiceObjectCommandSettings>();
			PropertyInformation propertyInformation = this.commandContext.PropertyInformation;
			MessageItem messageItem = commandSettings.StoreObject as MessageItem;
			if (messageItem == null || !messageItem.IsEventReminderMessage() || !(messageItem is ReminderMessage))
			{
				return;
			}
			ExDateTime valueOrDefault = messageItem.GetValueOrDefault<ExDateTime>(ReminderMessageSchema.ReminderStartTime, ExDateTime.MinValue);
			if (valueOrDefault == ExDateTime.MinValue)
			{
				return;
			}
			ReminderMessageDataType value = new ReminderMessageDataType((ReminderMessage)messageItem);
			ServiceObject serviceObject = commandSettings.ServiceObject;
			serviceObject.PropertyBag[propertyInformation] = value;
		}
	}
}
