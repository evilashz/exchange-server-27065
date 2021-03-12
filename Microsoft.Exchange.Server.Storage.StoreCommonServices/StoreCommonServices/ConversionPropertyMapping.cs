using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;
using Microsoft.Exchange.Server.Storage.PropTags;

namespace Microsoft.Exchange.Server.Storage.StoreCommonServices
{
	// Token: 0x020000F5 RID: 245
	internal sealed class ConversionPropertyMapping : PropertyMapping
	{
		// Token: 0x060009A8 RID: 2472 RVA: 0x0002C654 File Offset: 0x0002A854
		internal ConversionPropertyMapping(StorePropTag propertyTag, Column column, Func<object, object> conversionFunction, StorePropTag argumentPropertyTag, PropertyMapping argumentPropertyMapping, Func<Context, ISimplePropertyBag, object, ErrorCode> valueSetter, StreamGetterDelegate readStreamGetter, StreamGetterDelegate writeStreamGetter, bool primary, bool reservedPropId, bool list) : base(PropertyMappingKind.Convert, propertyTag, column, valueSetter, readStreamGetter, writeStreamGetter, primary, reservedPropId, list)
		{
			this.conversionFunction = conversionFunction;
			this.argumentPropertyTag = argumentPropertyTag;
			this.argumentPropertyMapping = argumentPropertyMapping;
		}

		// Token: 0x17000279 RID: 633
		// (get) Token: 0x060009A9 RID: 2473 RVA: 0x0002C68D File Offset: 0x0002A88D
		public StorePropTag ArgumentPropertyTag
		{
			get
			{
				return this.argumentPropertyTag;
			}
		}

		// Token: 0x1700027A RID: 634
		// (get) Token: 0x060009AA RID: 2474 RVA: 0x0002C695 File Offset: 0x0002A895
		public PropertyMapping ArgumentPropertyMapping
		{
			get
			{
				return this.argumentPropertyMapping;
			}
		}

		// Token: 0x1700027B RID: 635
		// (get) Token: 0x060009AB RID: 2475 RVA: 0x0002C69D File Offset: 0x0002A89D
		public Func<object, object> ConversionFunction
		{
			get
			{
				return this.conversionFunction;
			}
		}

		// Token: 0x1700027C RID: 636
		// (get) Token: 0x060009AC RID: 2476 RVA: 0x0002C6A5 File Offset: 0x0002A8A5
		public override bool CanBeSet
		{
			get
			{
				return base.ValueSetter != null;
			}
		}

		// Token: 0x060009AD RID: 2477 RVA: 0x0002C6B4 File Offset: 0x0002A8B4
		public override object GetPropertyValue(Context context, ISimpleReadOnlyPropertyBag bag)
		{
			object obj = this.ArgumentPropertyMapping.GetPropertyValue(context, bag);
			if (obj != null)
			{
				obj = this.ConversionFunction(obj);
			}
			return obj;
		}

		// Token: 0x060009AE RID: 2478 RVA: 0x0002C6E0 File Offset: 0x0002A8E0
		public override ErrorCode SetPropertyValue(Context context, ISimplePropertyBag bag, object value)
		{
			if (!this.CanBeSet)
			{
				return ErrorCode.CreateNoAccess((LID)47676U, base.PropertyTag.PropTag);
			}
			return base.ValueSetter(context, bag, value);
		}

		// Token: 0x060009AF RID: 2479 RVA: 0x0002C724 File Offset: 0x0002A924
		public override bool IsPropertyChanged(Context context, ISimplePropertyBagWithChangeTracking bag)
		{
			object propertyValue = this.GetPropertyValue(context, bag.OriginalBag);
			object propertyValue2 = this.GetPropertyValue(context, bag);
			return !ValueHelper.ValuesEqual(propertyValue, propertyValue2);
		}

		// Token: 0x04000568 RID: 1384
		private readonly StorePropTag argumentPropertyTag;

		// Token: 0x04000569 RID: 1385
		private readonly PropertyMapping argumentPropertyMapping;

		// Token: 0x0400056A RID: 1386
		private readonly Func<object, object> conversionFunction;
	}
}
