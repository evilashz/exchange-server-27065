using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x020001A4 RID: 420
	internal sealed class MeetingCancellationShape : Shape
	{
		// Token: 0x06000B99 RID: 2969 RVA: 0x0003946C File Offset: 0x0003766C
		static MeetingCancellationShape()
		{
			MeetingCancellationShape.defaultProperties.Add(ItemSchema.ItemId);
			MeetingCancellationShape.defaultProperties.Add(ItemSchema.Attachments);
			MeetingCancellationShape.defaultProperties.Add(ItemSchema.Subject);
			MeetingCancellationShape.defaultProperties.Add(ItemSchema.Sensitivity);
			MeetingCancellationShape.defaultProperties.Add(ItemSchema.ResponseObjects);
			MeetingCancellationShape.defaultProperties.Add(ItemSchema.HasAttachments);
			MeetingCancellationShape.defaultProperties.Add(ItemSchema.IsAssociated);
			MeetingCancellationShape.defaultProperties.Add(MessageSchema.ToRecipients);
			MeetingCancellationShape.defaultProperties.Add(MessageSchema.CcRecipients);
			MeetingCancellationShape.defaultProperties.Add(MessageSchema.BccRecipients);
			MeetingCancellationShape.defaultProperties.Add(MessageSchema.IsRead);
			MeetingCancellationShape.defaultProperties.Add(MeetingMessageSchema.AssociatedCalendarItemId);
			MeetingCancellationShape.defaultProperties.Add(MeetingMessageSchema.IsDelegated);
			MeetingCancellationShape.defaultProperties.Add(MeetingMessageSchema.IsOutOfDate);
			MeetingCancellationShape.defaultProperties.Add(MeetingMessageSchema.HasBeenProcessed);
			MeetingCancellationShape.defaultProperties.Add(MeetingMessageSchema.ResponseType);
		}

		// Token: 0x06000B9A RID: 2970 RVA: 0x00039573 File Offset: 0x00037773
		private MeetingCancellationShape() : base(Schema.MeetingCancellation, MeetingCancellationSchema.GetSchema(), MeetingMessageShape.CreateShape(), MeetingCancellationShape.defaultProperties)
		{
		}

		// Token: 0x06000B9B RID: 2971 RVA: 0x0003958F File Offset: 0x0003778F
		internal static MeetingCancellationShape CreateShape()
		{
			return new MeetingCancellationShape();
		}

		// Token: 0x040008D4 RID: 2260
		private static List<PropertyInformation> defaultProperties = new List<PropertyInformation>();
	}
}
