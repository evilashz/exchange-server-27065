using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;
using Microsoft.Exchange.Server.Storage.PropTags;

namespace Microsoft.Exchange.Server.Storage.StoreCommonServices
{
	// Token: 0x020000F8 RID: 248
	internal sealed class ConstantPropertyMapping : PropertyMapping
	{
		// Token: 0x060009BD RID: 2493 RVA: 0x0002C8DC File Offset: 0x0002AADC
		internal ConstantPropertyMapping(StorePropTag propertyTag, Column column, Func<Context, ISimplePropertyBag, object, ErrorCode> valueSetter, object propertyValue, bool primary, bool reservedPropId, bool list) : base(PropertyMappingKind.Constant, propertyTag, column, valueSetter, null, null, primary, reservedPropId, list)
		{
			this.propertyValue = propertyValue;
		}

		// Token: 0x17000283 RID: 643
		// (get) Token: 0x060009BE RID: 2494 RVA: 0x0002C903 File Offset: 0x0002AB03
		public override bool CanBeSet
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060009BF RID: 2495 RVA: 0x0002C906 File Offset: 0x0002AB06
		public override object GetPropertyValue(Context context, ISimpleReadOnlyPropertyBag bag)
		{
			return this.propertyValue;
		}

		// Token: 0x060009C0 RID: 2496 RVA: 0x0002C910 File Offset: 0x0002AB10
		public override ErrorCode SetPropertyValue(Context context, ISimplePropertyBag bag, object value)
		{
			if (base.ValueSetter == null)
			{
				return ErrorCode.CreateNoAccess((LID)37084U, base.PropertyTag.PropTag);
			}
			return base.ValueSetter(context, bag, value);
		}

		// Token: 0x060009C1 RID: 2497 RVA: 0x0002C954 File Offset: 0x0002AB54
		public override bool IsPropertyChanged(Context context, ISimplePropertyBagWithChangeTracking bag)
		{
			if (!this.CanBeSet)
			{
				return false;
			}
			object x = this.GetPropertyValue(context, bag.OriginalBag);
			object y = this.GetPropertyValue(context, bag);
			return !ValueHelper.ValuesEqual(x, y);
		}

		// Token: 0x0400056F RID: 1391
		private readonly object propertyValue;
	}
}
