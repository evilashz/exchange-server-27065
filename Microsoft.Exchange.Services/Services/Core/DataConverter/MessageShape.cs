using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x020001B1 RID: 433
	internal sealed class MessageShape : Shape
	{
		// Token: 0x06000BC0 RID: 3008 RVA: 0x0003BC4C File Offset: 0x00039E4C
		static MessageShape()
		{
			MessageShape.defaultProperties.Add(ItemSchema.Attachments);
			MessageShape.defaultProperties.Add(ItemSchema.Body);
			MessageShape.defaultProperties.Add(ItemSchema.Categories);
			MessageShape.defaultProperties.Add(ItemSchema.DateTimeCreated);
			MessageShape.defaultProperties.Add(ItemSchema.HasAttachments);
			MessageShape.defaultProperties.Add(ItemSchema.IsAssociated);
			MessageShape.defaultProperties.Add(ItemSchema.ItemId);
			MessageShape.defaultProperties.Add(ItemSchema.ResponseObjects);
			MessageShape.defaultProperties.Add(ItemSchema.Sensitivity);
			MessageShape.defaultProperties.Add(ItemSchema.DateTimeSent);
			MessageShape.defaultProperties.Add(ItemSchema.Size);
			MessageShape.defaultProperties.Add(ItemSchema.Subject);
			MessageShape.defaultProperties.Add(MessageSchema.BccRecipients);
			MessageShape.defaultProperties.Add(MessageSchema.CcRecipients);
			MessageShape.defaultProperties.Add(MessageSchema.From);
			MessageShape.defaultProperties.Add(MessageSchema.IsDeliveryReceiptRequested);
			MessageShape.defaultProperties.Add(MessageSchema.IsRead);
			MessageShape.defaultProperties.Add(MessageSchema.IsReadReceiptRequested);
			MessageShape.defaultProperties.Add(MessageSchema.ToRecipients);
			MessageShape.defaultPropertiesForPropertyBag = new List<PropertyInformation>();
			MessageShape.defaultPropertiesForPropertyBag.InsertRange(0, MessageShape.defaultProperties);
			MessageShape.defaultPropertiesForPropertyBag.Remove(ItemSchema.Body);
			MessageShape.defaultPropertiesForPropertyBag.Remove(MessageSchema.IsDeliveryReceiptRequested);
			MessageShape.defaultPropertiesForPropertyBag.Remove(MessageSchema.IsReadReceiptRequested);
		}

		// Token: 0x06000BC1 RID: 3009 RVA: 0x0003BDCA File Offset: 0x00039FCA
		private MessageShape(List<PropertyInformation> defaultProperties) : base(Schema.Message, MessageSchema.GetSchema(), ItemShape.CreateShape(), defaultProperties)
		{
		}

		// Token: 0x06000BC2 RID: 3010 RVA: 0x0003BDE2 File Offset: 0x00039FE2
		internal static MessageShape CreateShape()
		{
			return new MessageShape(MessageShape.defaultProperties);
		}

		// Token: 0x06000BC3 RID: 3011 RVA: 0x0003BDEE File Offset: 0x00039FEE
		internal static MessageShape CreateShapeForPropertyBag()
		{
			return new MessageShape(MessageShape.defaultPropertiesForPropertyBag);
		}

		// Token: 0x0400095C RID: 2396
		private static List<PropertyInformation> defaultProperties = new List<PropertyInformation>();

		// Token: 0x0400095D RID: 2397
		private static List<PropertyInformation> defaultPropertiesForPropertyBag;
	}
}
