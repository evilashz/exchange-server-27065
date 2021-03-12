using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;
using Microsoft.Exchange.Server.Storage.PropTags;

namespace Microsoft.Exchange.Server.Storage.StoreCommonServices
{
	// Token: 0x020000F9 RID: 249
	internal sealed class FunctionPropertyMapping : PropertyMapping
	{
		// Token: 0x060009C2 RID: 2498 RVA: 0x0002C98C File Offset: 0x0002AB8C
		internal FunctionPropertyMapping(StorePropTag propertyTag, Column column, Func<Context, ISimplePropertyBag, object, ErrorCode> valueSetter, Func<object[], object> function, PropertyMapping[] argumentPropertyMappings, bool primary, bool reservedPropId, bool list) : base(PropertyMappingKind.Function, propertyTag, column, valueSetter, null, null, primary, reservedPropId, list)
		{
			this.function = function;
			this.argumentPropertyMappings = argumentPropertyMappings;
		}

		// Token: 0x17000284 RID: 644
		// (get) Token: 0x060009C3 RID: 2499 RVA: 0x0002C9BB File Offset: 0x0002ABBB
		public PropertyMapping[] ArgumentPropertyMappings
		{
			get
			{
				return this.argumentPropertyMappings;
			}
		}

		// Token: 0x17000285 RID: 645
		// (get) Token: 0x060009C4 RID: 2500 RVA: 0x0002C9C3 File Offset: 0x0002ABC3
		public Func<object[], object> Function
		{
			get
			{
				return this.function;
			}
		}

		// Token: 0x17000286 RID: 646
		// (get) Token: 0x060009C5 RID: 2501 RVA: 0x0002C9CB File Offset: 0x0002ABCB
		public override bool CanBeSet
		{
			get
			{
				return base.ValueSetter != null;
			}
		}

		// Token: 0x060009C6 RID: 2502 RVA: 0x0002C9DC File Offset: 0x0002ABDC
		public override object GetPropertyValue(Context context, ISimpleReadOnlyPropertyBag bag)
		{
			object[] array = new object[this.argumentPropertyMappings.Length];
			for (int i = 0; i < this.argumentPropertyMappings.Length; i++)
			{
				array[i] = this.argumentPropertyMappings[i].GetPropertyValue(context, bag);
			}
			return this.function(array);
		}

		// Token: 0x060009C7 RID: 2503 RVA: 0x0002CA2C File Offset: 0x0002AC2C
		public override ErrorCode SetPropertyValue(Context context, ISimplePropertyBag bag, object value)
		{
			if (base.ValueSetter == null)
			{
				return ErrorCode.CreateNoAccess((LID)53468U, base.PropertyTag.PropTag);
			}
			return base.ValueSetter(context, bag, value);
		}

		// Token: 0x060009C8 RID: 2504 RVA: 0x0002CA70 File Offset: 0x0002AC70
		public override bool IsPropertyChanged(Context context, ISimplePropertyBagWithChangeTracking bag)
		{
			object propertyValue = this.GetPropertyValue(context, bag.OriginalBag);
			object propertyValue2 = this.GetPropertyValue(context, bag);
			return !ValueHelper.ValuesEqual(propertyValue, propertyValue2);
		}

		// Token: 0x04000570 RID: 1392
		private readonly PropertyMapping[] argumentPropertyMappings;

		// Token: 0x04000571 RID: 1393
		private readonly Func<object[], object> function;
	}
}
