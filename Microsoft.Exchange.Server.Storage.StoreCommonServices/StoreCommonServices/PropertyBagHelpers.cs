using System;
using Microsoft.Exchange.Server.Storage.PropTags;

namespace Microsoft.Exchange.Server.Storage.StoreCommonServices
{
	// Token: 0x0200008D RID: 141
	public static class PropertyBagHelpers
	{
		// Token: 0x060004FC RID: 1276 RVA: 0x0001D878 File Offset: 0x0001BA78
		public static bool TestPropertyFlags(Context context, ISimpleReadOnlyPropertyBag bag, StorePropTag propTag, short mask, short desiredValue)
		{
			object propertyValue = bag.GetPropertyValue(context, propTag);
			short num;
			if (propertyValue == null)
			{
				num = 0;
			}
			else
			{
				num = (short)propertyValue;
			}
			return (num & mask) == desiredValue;
		}

		// Token: 0x060004FD RID: 1277 RVA: 0x0001D8A4 File Offset: 0x0001BAA4
		public static bool TestPropertyFlags(Context context, ISimpleReadOnlyPropertyBag bag, StorePropTag propTag, int mask, int desiredValue)
		{
			object propertyValue = bag.GetPropertyValue(context, propTag);
			int num;
			if (propertyValue == null)
			{
				num = 0;
			}
			else
			{
				num = (int)propertyValue;
			}
			return (num & mask) == desiredValue;
		}

		// Token: 0x060004FE RID: 1278 RVA: 0x0001D8CF File Offset: 0x0001BACF
		public static bool SetPropertyFlags(Context context, ISimplePropertyBag bag, StorePropTag propTag, object value, short flags)
		{
			if (value != null && value is bool && (bool)value)
			{
				return PropertyBagHelpers.AdjustPropertyFlags(context, bag, propTag, flags, 0);
			}
			return PropertyBagHelpers.AdjustPropertyFlags(context, bag, propTag, 0, flags);
		}

		// Token: 0x060004FF RID: 1279 RVA: 0x0001D8FB File Offset: 0x0001BAFB
		public static bool SetPropertyFlags(Context context, ISimplePropertyBag bag, StorePropTag propTag, object value, int flags)
		{
			if (value != null && value is bool && (bool)value)
			{
				return PropertyBagHelpers.AdjustPropertyFlags(context, bag, propTag, flags, 0);
			}
			return PropertyBagHelpers.AdjustPropertyFlags(context, bag, propTag, 0, flags);
		}

		// Token: 0x06000500 RID: 1280 RVA: 0x0001D928 File Offset: 0x0001BB28
		public static bool AdjustPropertyFlags(Context context, ISimplePropertyBag bag, StorePropTag propTag, short flagsToSet, short flagsToClear)
		{
			object propertyValue = bag.GetPropertyValue(context, propTag);
			short num;
			if (propertyValue == null)
			{
				num = 0;
			}
			else
			{
				num = (short)propertyValue;
			}
			short num2 = (num | flagsToSet) & ~flagsToClear;
			if (num2 != num)
			{
				bag.SetProperty(context, propTag, num2);
				return true;
			}
			return false;
		}

		// Token: 0x06000501 RID: 1281 RVA: 0x0001D96C File Offset: 0x0001BB6C
		public static bool AdjustPropertyFlags(Context context, ISimplePropertyBag bag, StorePropTag propTag, int flagsToSet, int flagsToClear)
		{
			object propertyValue = bag.GetPropertyValue(context, propTag);
			int num;
			if (propertyValue == null)
			{
				num = 0;
			}
			else
			{
				num = (int)propertyValue;
			}
			int num2 = (num | flagsToSet) & ~flagsToClear;
			if (num2 != num)
			{
				bag.SetProperty(context, propTag, num2);
				return true;
			}
			return false;
		}
	}
}
