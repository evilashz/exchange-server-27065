using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x020001A7 RID: 423
	internal sealed class MeetingMessageShape : Shape
	{
		// Token: 0x06000BA2 RID: 2978 RVA: 0x00039864 File Offset: 0x00037A64
		static MeetingMessageShape()
		{
			MeetingMessageShape.defaultProperties.Add(ItemSchema.ItemId);
			MeetingMessageShape.defaultProperties.Add(ItemSchema.Attachments);
			MeetingMessageShape.defaultProperties.Add(ItemSchema.Subject);
			MeetingMessageShape.defaultProperties.Add(ItemSchema.Sensitivity);
			MeetingMessageShape.defaultProperties.Add(ItemSchema.ResponseObjects);
			MeetingMessageShape.defaultProperties.Add(ItemSchema.HasAttachments);
			MeetingMessageShape.defaultProperties.Add(ItemSchema.IsAssociated);
			MeetingMessageShape.defaultProperties.Add(MessageSchema.ToRecipients);
			MeetingMessageShape.defaultProperties.Add(MessageSchema.CcRecipients);
			MeetingMessageShape.defaultProperties.Add(MessageSchema.BccRecipients);
			MeetingMessageShape.defaultProperties.Add(MessageSchema.IsRead);
			MeetingMessageShape.defaultProperties.Add(MeetingMessageSchema.AssociatedCalendarItemId);
			MeetingMessageShape.defaultProperties.Add(MeetingMessageSchema.IsDelegated);
			MeetingMessageShape.defaultProperties.Add(MeetingMessageSchema.IsOutOfDate);
			MeetingMessageShape.defaultProperties.Add(MeetingMessageSchema.HasBeenProcessed);
			MeetingMessageShape.defaultProperties.Add(MeetingMessageSchema.ResponseType);
		}

		// Token: 0x06000BA3 RID: 2979 RVA: 0x0003996B File Offset: 0x00037B6B
		private MeetingMessageShape() : base(Schema.MeetingMessage, MeetingMessageSchema.GetSchema(), MessageShape.CreateShape(), MeetingMessageShape.defaultProperties)
		{
		}

		// Token: 0x06000BA4 RID: 2980 RVA: 0x00039987 File Offset: 0x00037B87
		internal static MeetingMessageShape CreateShape()
		{
			return new MeetingMessageShape();
		}

		// Token: 0x040008E3 RID: 2275
		private static List<PropertyInformation> defaultProperties = new List<PropertyInformation>();
	}
}
