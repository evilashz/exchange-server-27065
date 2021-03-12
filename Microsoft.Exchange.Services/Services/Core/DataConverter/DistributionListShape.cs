using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x020001A3 RID: 419
	internal sealed class DistributionListShape : Shape
	{
		// Token: 0x06000B96 RID: 2966 RVA: 0x000393D8 File Offset: 0x000375D8
		static DistributionListShape()
		{
			DistributionListShape.defaultProperties.Add(ItemSchema.ItemId);
			DistributionListShape.defaultProperties.Add(ItemSchema.Subject);
			DistributionListShape.defaultProperties.Add(ItemSchema.Attachments);
			DistributionListShape.defaultProperties.Add(DistributionListSchema.DisplayName);
			DistributionListShape.defaultProperties.Add(DistributionListSchema.FileAs);
			DistributionListShape.defaultProperties.Add(DistributionListSchema.Members);
		}

		// Token: 0x06000B97 RID: 2967 RVA: 0x00039449 File Offset: 0x00037649
		private DistributionListShape() : base(Schema.DistributionList, DistributionListSchema.GetSchema(), ItemShape.CreateShape(), DistributionListShape.defaultProperties)
		{
		}

		// Token: 0x06000B98 RID: 2968 RVA: 0x00039465 File Offset: 0x00037665
		internal static DistributionListShape CreateShape()
		{
			return new DistributionListShape();
		}

		// Token: 0x040008D3 RID: 2259
		private static List<PropertyInformation> defaultProperties = new List<PropertyInformation>();
	}
}
