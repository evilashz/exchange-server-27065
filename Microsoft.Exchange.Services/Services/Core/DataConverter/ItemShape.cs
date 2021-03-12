using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x020001AF RID: 431
	internal sealed class ItemShape : Shape
	{
		// Token: 0x06000BBA RID: 3002 RVA: 0x0003B3DC File Offset: 0x000395DC
		static ItemShape()
		{
			ItemShape.defaultProperties.Add(ItemSchema.ItemId);
			ItemShape.defaultProperties.Add(ItemSchema.Attachments);
			ItemShape.defaultProperties.Add(ItemSchema.ResponseObjects);
			ItemShape.defaultProperties.Add(ItemSchema.HasAttachments);
			ItemShape.defaultProperties.Add(ItemSchema.Culture);
			ItemShape.defaultProperties.Add(ItemSchema.IsAssociated);
		}

		// Token: 0x06000BBB RID: 3003 RVA: 0x0003B44D File Offset: 0x0003964D
		private ItemShape() : base(Schema.Item, ItemSchema.GetSchema(), null, ItemShape.defaultProperties)
		{
		}

		// Token: 0x06000BBC RID: 3004 RVA: 0x0003B465 File Offset: 0x00039665
		internal static ItemShape CreateShape()
		{
			return new ItemShape();
		}

		// Token: 0x0400093F RID: 2367
		private static List<PropertyInformation> defaultProperties = new List<PropertyInformation>();
	}
}
