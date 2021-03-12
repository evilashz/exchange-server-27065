using System;
using Microsoft.Exchange.Entities.DataModel.Items;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000E70 RID: 3696
	internal static class ItemBodyEwsConverter
	{
		// Token: 0x0600603D RID: 24637 RVA: 0x0012C88C File Offset: 0x0012AA8C
		public static BodyContentType ToBodyContentType(this ItemBody itemBody)
		{
			if (itemBody == null)
			{
				return null;
			}
			return new BodyContentType
			{
				BodyType = EnumConverter.CastEnumType<Microsoft.Exchange.Services.Core.Types.BodyType>(itemBody.ContentType),
				Value = itemBody.Content
			};
		}

		// Token: 0x0600603E RID: 24638 RVA: 0x0012C8C8 File Offset: 0x0012AAC8
		public static ItemBody ToItemBody(this BodyContentType bodyContentType)
		{
			if (bodyContentType == null)
			{
				return null;
			}
			return new ItemBody
			{
				ContentType = EnumConverter.CastEnumType<Microsoft.Exchange.Services.OData.Model.BodyType>(bodyContentType.BodyType),
				Content = bodyContentType.Value
			};
		}

		// Token: 0x0600603F RID: 24639 RVA: 0x0012C908 File Offset: 0x0012AB08
		public static ItemBody ToDataEntityItemBody(this ItemBody itemBody)
		{
			if (itemBody == null)
			{
				return null;
			}
			return new ItemBody
			{
				ContentType = EnumConverter.CastEnumType<Microsoft.Exchange.Entities.DataModel.Items.BodyType>(itemBody.ContentType),
				Content = itemBody.Content
			};
		}

		// Token: 0x06006040 RID: 24640 RVA: 0x0012C948 File Offset: 0x0012AB48
		public static ItemBody ToItemBody(this ItemBody dataEntityItemBody)
		{
			if (dataEntityItemBody == null)
			{
				return null;
			}
			return new ItemBody
			{
				ContentType = EnumConverter.CastEnumType<Microsoft.Exchange.Services.OData.Model.BodyType>(dataEntityItemBody.ContentType),
				Content = dataEntityItemBody.Content
			};
		}
	}
}
