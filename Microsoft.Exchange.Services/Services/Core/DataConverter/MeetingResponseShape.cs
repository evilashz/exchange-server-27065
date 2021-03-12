using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x020001AB RID: 427
	internal sealed class MeetingResponseShape : Shape
	{
		// Token: 0x06000BAE RID: 2990 RVA: 0x0003A128 File Offset: 0x00038328
		static MeetingResponseShape()
		{
			MeetingResponseShape.defaultProperties.Add(ItemSchema.ItemId);
			MeetingResponseShape.defaultProperties.Add(ItemSchema.Attachments);
			MeetingResponseShape.defaultProperties.Add(ItemSchema.Subject);
			MeetingResponseShape.defaultProperties.Add(ItemSchema.Sensitivity);
			MeetingResponseShape.defaultProperties.Add(ItemSchema.ResponseObjects);
			MeetingResponseShape.defaultProperties.Add(ItemSchema.HasAttachments);
			MeetingResponseShape.defaultProperties.Add(ItemSchema.IsAssociated);
			MeetingResponseShape.defaultProperties.Add(MessageSchema.ToRecipients);
			MeetingResponseShape.defaultProperties.Add(MessageSchema.CcRecipients);
			MeetingResponseShape.defaultProperties.Add(MessageSchema.BccRecipients);
			MeetingResponseShape.defaultProperties.Add(MessageSchema.IsRead);
			MeetingResponseShape.defaultProperties.Add(MeetingMessageSchema.AssociatedCalendarItemId);
			MeetingResponseShape.defaultProperties.Add(MeetingMessageSchema.IsDelegated);
			MeetingResponseShape.defaultProperties.Add(MeetingMessageSchema.IsOutOfDate);
			MeetingResponseShape.defaultProperties.Add(MeetingMessageSchema.HasBeenProcessed);
			MeetingResponseShape.defaultProperties.Add(MeetingMessageSchema.ResponseType);
			MeetingResponseShape.defaultProperties.Add(MeetingResponseSchema.ProposedStart);
			MeetingResponseShape.defaultProperties.Add(MeetingResponseSchema.ProposedEnd);
		}

		// Token: 0x06000BAF RID: 2991 RVA: 0x0003A24D File Offset: 0x0003844D
		private MeetingResponseShape() : base(Schema.MeetingResponse, MeetingResponseSchema.GetSchema(), MeetingMessageShape.CreateShape(), MeetingResponseShape.defaultProperties)
		{
		}

		// Token: 0x06000BB0 RID: 2992 RVA: 0x0003A269 File Offset: 0x00038469
		internal static MeetingResponseShape CreateShape()
		{
			return new MeetingResponseShape();
		}

		// Token: 0x040008FA RID: 2298
		private static List<PropertyInformation> defaultProperties = new List<PropertyInformation>();
	}
}
