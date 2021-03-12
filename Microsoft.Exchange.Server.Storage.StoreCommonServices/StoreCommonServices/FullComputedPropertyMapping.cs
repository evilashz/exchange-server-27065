using System;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;
using Microsoft.Exchange.Server.Storage.PropTags;

namespace Microsoft.Exchange.Server.Storage.StoreCommonServices
{
	// Token: 0x020000F7 RID: 247
	internal sealed class FullComputedPropertyMapping : ComputedPropertyMapping
	{
		// Token: 0x060009B9 RID: 2489 RVA: 0x0002C890 File Offset: 0x0002AA90
		internal FullComputedPropertyMapping(StorePropTag propertyTag, Column column, Func<Context, ISimpleReadOnlyPropertyBag, object> valueGetter, StorePropTag[] dependentPropertyTags, PropertyMapping[] dependentPropertyMappings, Func<Context, ISimplePropertyBag, object, ErrorCode> valueSetter, StreamGetterDelegate readStreamGetter, StreamGetterDelegate writeStreamGetter, bool primary, bool reservedPropId, bool list) : base(propertyTag, column, valueGetter, dependentPropertyTags, dependentPropertyMappings, valueSetter, readStreamGetter, writeStreamGetter, true, primary, reservedPropId, list)
		{
		}

		// Token: 0x17000282 RID: 642
		// (get) Token: 0x060009BA RID: 2490 RVA: 0x0002C8B7 File Offset: 0x0002AAB7
		public override bool CanBeSet
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060009BB RID: 2491 RVA: 0x0002C8BA File Offset: 0x0002AABA
		public override object GetPropertyValue(Context context, ISimpleReadOnlyPropertyBag bag)
		{
			return base.ValueGetter(context, bag);
		}

		// Token: 0x060009BC RID: 2492 RVA: 0x0002C8C9 File Offset: 0x0002AAC9
		public override ErrorCode SetPropertyValue(Context context, ISimplePropertyBag bag, object value)
		{
			return base.ValueSetter(context, bag, value);
		}
	}
}
