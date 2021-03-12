using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000C7A RID: 3194
	[ClassAccessLevel(AccessLevel.Implementation)]
	[Serializable]
	internal class LikeCountProperty : SmartPropertyDefinition
	{
		// Token: 0x06007022 RID: 28706 RVA: 0x001F0880 File Offset: 0x001EEA80
		internal LikeCountProperty() : base("LikersCount", typeof(int), PropertyFlags.None, PropertyDefinitionConstraint.None, new PropertyDependency[]
		{
			new PropertyDependency(InternalSchema.MapiLikeCount, PropertyDependencyType.NeedForRead)
		})
		{
		}

		// Token: 0x06007023 RID: 28707 RVA: 0x001F08C0 File Offset: 0x001EEAC0
		protected override object InternalTryGetValue(PropertyBag.BasicPropertyStore propertyBag)
		{
			MessageItem messageItem = propertyBag.Context.StoreObject as MessageItem;
			if (messageItem == null)
			{
				return propertyBag.GetValue(InternalSchema.MapiLikeCount);
			}
			int count = ((Likers)messageItem.Likers).Count;
			if (count == 0)
			{
				return new PropertyError(this, PropertyErrorCode.NotFound);
			}
			return count;
		}

		// Token: 0x06007024 RID: 28708 RVA: 0x001F0911 File Offset: 0x001EEB11
		protected override void InternalSetValue(PropertyBag.BasicPropertyStore propertyBag, object value)
		{
			propertyBag.SetValueWithFixup(InternalSchema.MapiLikeCount, value);
		}
	}
}
