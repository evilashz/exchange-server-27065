using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000854 RID: 2132
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class Likers : ParticipantList
	{
		// Token: 0x06004FF4 RID: 20468 RVA: 0x0014D1A7 File Offset: 0x0014B3A7
		internal Likers(PropertyBag propertyBag) : this(propertyBag, true)
		{
		}

		// Token: 0x06004FF5 RID: 20469 RVA: 0x0014D1B1 File Offset: 0x0014B3B1
		internal Likers(PropertyBag propertyBag, bool suppressCorruptDataException) : base(propertyBag, InternalSchema.MapiLikersBlob, null, InternalSchema.MapiLikeCount, suppressCorruptDataException)
		{
		}

		// Token: 0x06004FF6 RID: 20470 RVA: 0x0014D1C8 File Offset: 0x0014B3C8
		internal static Likers CreateInstance(IDictionary<PropertyDefinition, object> propertyBag)
		{
			object obj;
			if (propertyBag.TryGetValue(InternalSchema.LikersBlob, out obj))
			{
				byte[] array = obj as byte[];
				if (array != null)
				{
					PropertyBag propertyBag2 = new MemoryPropertyBag();
					propertyBag2[InternalSchema.MapiLikersBlob] = array;
					return new Likers(propertyBag2);
				}
			}
			return null;
		}

		// Token: 0x04002B65 RID: 11109
		public static readonly PropertyDefinition[] RequiredProperties = new PropertyDefinition[]
		{
			MessageItemSchema.LikeCount,
			MessageItemSchema.LikersBlob
		};
	}
}
