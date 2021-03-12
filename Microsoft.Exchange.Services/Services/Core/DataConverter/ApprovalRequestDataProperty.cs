using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x02000178 RID: 376
	internal sealed class ApprovalRequestDataProperty : ComplexPropertyBase, IToServiceObjectCommand, IPropertyCommand
	{
		// Token: 0x06000AC6 RID: 2758 RVA: 0x0003413B File Offset: 0x0003233B
		public ApprovalRequestDataProperty(CommandContext commandContext) : base(commandContext)
		{
		}

		// Token: 0x06000AC7 RID: 2759 RVA: 0x00034144 File Offset: 0x00032344
		public static ApprovalRequestDataProperty CreateCommand(CommandContext commandContext)
		{
			return new ApprovalRequestDataProperty(commandContext);
		}

		// Token: 0x06000AC8 RID: 2760 RVA: 0x0003414C File Offset: 0x0003234C
		public void ToXml()
		{
			throw new InvalidOperationException("ApprovalRequestDataProperty.ToXml should not be called.");
		}

		// Token: 0x06000AC9 RID: 2761 RVA: 0x00034158 File Offset: 0x00032358
		public void ToServiceObject()
		{
			ToServiceObjectCommandSettings commandSettings = base.GetCommandSettings<ToServiceObjectCommandSettings>();
			PropertyInformation propertyInformation = this.commandContext.PropertyInformation;
			MessageItem messageItem = commandSettings.StoreObject as MessageItem;
			if (messageItem == null || !messageItem.IsValidApprovalRequest())
			{
				return;
			}
			ApprovalRequestDataType value = new ApprovalRequestDataType(messageItem);
			ServiceObject serviceObject = commandSettings.ServiceObject;
			serviceObject.PropertyBag[propertyInformation] = value;
		}
	}
}
