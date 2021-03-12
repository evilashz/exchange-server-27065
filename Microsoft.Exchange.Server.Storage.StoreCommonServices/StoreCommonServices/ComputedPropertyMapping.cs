using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;
using Microsoft.Exchange.Server.Storage.PropTags;

namespace Microsoft.Exchange.Server.Storage.StoreCommonServices
{
	// Token: 0x020000F6 RID: 246
	internal class ComputedPropertyMapping : PropertyMapping
	{
		// Token: 0x060009B0 RID: 2480 RVA: 0x0002C754 File Offset: 0x0002A954
		internal ComputedPropertyMapping(StorePropTag propertyTag, Column column, Func<Context, ISimpleReadOnlyPropertyBag, object> valueGetter, StorePropTag[] dependentPropertyTags, PropertyMapping[] dependentPropertyMappings, Func<Context, ISimplePropertyBag, object, ErrorCode> valueSetter, StreamGetterDelegate readStreamGetter, StreamGetterDelegate writeStreamGetter, bool canBeOverriden, bool primary, bool reservedPropId, bool list) : base(PropertyMappingKind.Compute, propertyTag, column, valueSetter, readStreamGetter, writeStreamGetter, primary, reservedPropId, list)
		{
			this.valueGetter = valueGetter;
			this.dependentPropertyTags = dependentPropertyTags;
			this.dependentPropertyMappings = dependentPropertyMappings;
			this.canBeOverriden = canBeOverriden;
		}

		// Token: 0x1700027D RID: 637
		// (get) Token: 0x060009B1 RID: 2481 RVA: 0x0002C795 File Offset: 0x0002A995
		public Func<Context, ISimpleReadOnlyPropertyBag, object> ValueGetter
		{
			get
			{
				return this.valueGetter;
			}
		}

		// Token: 0x1700027E RID: 638
		// (get) Token: 0x060009B2 RID: 2482 RVA: 0x0002C79D File Offset: 0x0002A99D
		public bool CanBeOverridden
		{
			get
			{
				return this.canBeOverriden;
			}
		}

		// Token: 0x1700027F RID: 639
		// (get) Token: 0x060009B3 RID: 2483 RVA: 0x0002C7A5 File Offset: 0x0002A9A5
		public override bool CanBeSet
		{
			get
			{
				return base.ValueSetter != null || this.canBeOverriden;
			}
		}

		// Token: 0x17000280 RID: 640
		// (get) Token: 0x060009B4 RID: 2484 RVA: 0x0002C7B7 File Offset: 0x0002A9B7
		public StorePropTag[] DependentPropertyTags
		{
			get
			{
				return this.dependentPropertyTags;
			}
		}

		// Token: 0x17000281 RID: 641
		// (get) Token: 0x060009B5 RID: 2485 RVA: 0x0002C7BF File Offset: 0x0002A9BF
		public PropertyMapping[] DependentPropertyMappings
		{
			get
			{
				return this.dependentPropertyMappings;
			}
		}

		// Token: 0x060009B6 RID: 2486 RVA: 0x0002C7C8 File Offset: 0x0002A9C8
		public override object GetPropertyValue(Context context, ISimpleReadOnlyPropertyBag bag)
		{
			object obj = null;
			if (this.CanBeOverridden)
			{
				obj = bag.GetBlobPropertyValue(context, base.PropertyTag);
			}
			if (obj == null)
			{
				obj = this.ValueGetter(context, bag);
			}
			return obj;
		}

		// Token: 0x060009B7 RID: 2487 RVA: 0x0002C800 File Offset: 0x0002AA00
		public override ErrorCode SetPropertyValue(Context context, ISimplePropertyBag bag, object value)
		{
			if (!this.CanBeSet)
			{
				return ErrorCode.CreateNoAccess((LID)35608U, base.PropertyTag.PropTag);
			}
			if (base.ValueSetter != null)
			{
				return base.ValueSetter(context, bag, value);
			}
			bag.SetBlobProperty(context, base.PropertyTag, value);
			return ErrorCode.NoError;
		}

		// Token: 0x060009B8 RID: 2488 RVA: 0x0002C860 File Offset: 0x0002AA60
		public override bool IsPropertyChanged(Context context, ISimplePropertyBagWithChangeTracking bag)
		{
			object propertyValue = this.GetPropertyValue(context, bag.OriginalBag);
			object propertyValue2 = this.GetPropertyValue(context, bag);
			return !ValueHelper.ValuesEqual(propertyValue, propertyValue2);
		}

		// Token: 0x0400056B RID: 1387
		private readonly StorePropTag[] dependentPropertyTags;

		// Token: 0x0400056C RID: 1388
		private readonly PropertyMapping[] dependentPropertyMappings;

		// Token: 0x0400056D RID: 1389
		private readonly Func<Context, ISimpleReadOnlyPropertyBag, object> valueGetter;

		// Token: 0x0400056E RID: 1390
		private readonly bool canBeOverriden;
	}
}
