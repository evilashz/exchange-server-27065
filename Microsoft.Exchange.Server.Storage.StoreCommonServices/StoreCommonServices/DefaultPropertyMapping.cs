using System;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;
using Microsoft.Exchange.Server.Storage.PropTags;

namespace Microsoft.Exchange.Server.Storage.StoreCommonServices
{
	// Token: 0x020000F3 RID: 243
	internal sealed class DefaultPropertyMapping : PropertyMapping
	{
		// Token: 0x0600099B RID: 2459 RVA: 0x0002C320 File Offset: 0x0002A520
		internal DefaultPropertyMapping(StorePropTag propertyTag, Column column, Func<Context, ISimplePropertyBag, object, ErrorCode> valueSetter, StreamGetterDelegate readStreamGetter, StreamGetterDelegate writeStreamGetter, bool primary, bool reservedPropId, bool list, bool tailSet) : base(PropertyMappingKind.Default, propertyTag, column, valueSetter, readStreamGetter, writeStreamGetter, primary, reservedPropId, list)
		{
			this.tailSet = tailSet;
		}

		// Token: 0x17000276 RID: 630
		// (get) Token: 0x0600099C RID: 2460 RVA: 0x0002C349 File Offset: 0x0002A549
		public override bool CanBeSet
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600099D RID: 2461 RVA: 0x0002C34C File Offset: 0x0002A54C
		public override object GetPropertyValue(Context context, ISimpleReadOnlyPropertyBag bag)
		{
			return bag.GetBlobPropertyValue(context, base.PropertyTag);
		}

		// Token: 0x0600099E RID: 2462 RVA: 0x0002C35C File Offset: 0x0002A55C
		public override ErrorCode SetPropertyValue(Context context, ISimplePropertyBag bag, object value)
		{
			if (base.ValueSetter != null)
			{
				ErrorCode errorCode = base.ValueSetter(context, bag, value);
				if (errorCode != ErrorCode.NoError || !this.tailSet)
				{
					return errorCode;
				}
			}
			bag.SetBlobProperty(context, base.PropertyTag, value);
			return ErrorCode.NoError;
		}

		// Token: 0x0600099F RID: 2463 RVA: 0x0002C3AA File Offset: 0x0002A5AA
		public override bool IsPropertyChanged(Context context, ISimplePropertyBagWithChangeTracking bag)
		{
			return bag.IsBlobPropertyChanged(context, base.PropertyTag);
		}

		// Token: 0x04000564 RID: 1380
		private readonly bool tailSet;
	}
}
