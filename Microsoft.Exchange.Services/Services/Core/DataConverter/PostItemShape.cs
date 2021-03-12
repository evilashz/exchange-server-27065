using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x020001B3 RID: 435
	internal sealed class PostItemShape : Shape
	{
		// Token: 0x06000BC7 RID: 3015 RVA: 0x0003C040 File Offset: 0x0003A240
		static PostItemShape()
		{
			PostItemShape.defaultProperties.Add(ItemSchema.ItemId);
			PostItemShape.defaultProperties.Add(ItemSchema.Subject);
			PostItemShape.defaultProperties.Add(ItemSchema.Attachments);
			PostItemShape.defaultProperties.Add(ItemSchema.HasAttachments);
			PostItemShape.defaultProperties.Add(PostItemSchema.ConversationIndex);
			PostItemShape.defaultProperties.Add(PostItemSchema.ConversationTopic);
			PostItemShape.defaultProperties.Add(PostItemSchema.From);
			PostItemShape.defaultProperties.Add(PostItemSchema.InternetMessageId);
			PostItemShape.defaultProperties.Add(PostItemSchema.PostedTime);
			PostItemShape.defaultProperties.Add(PostItemSchema.References);
			PostItemShape.defaultProperties.Add(PostItemSchema.Sender);
		}

		// Token: 0x06000BC8 RID: 3016 RVA: 0x0003C0FC File Offset: 0x0003A2FC
		private PostItemShape() : base(Schema.PostItem, PostItemSchema.GetSchema(), ItemShape.CreateShape(), PostItemShape.defaultProperties)
		{
		}

		// Token: 0x06000BC9 RID: 3017 RVA: 0x0003C118 File Offset: 0x0003A318
		internal static PostItemShape CreateShape()
		{
			return new PostItemShape();
		}

		// Token: 0x04000967 RID: 2407
		private static List<PropertyInformation> defaultProperties = new List<PropertyInformation>();
	}
}
