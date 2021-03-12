using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000C91 RID: 3217
	[ClassAccessLevel(AccessLevel.Implementation)]
	[Serializable]
	internal class OutlookBlockStatusProperty : SmartPropertyDefinition
	{
		// Token: 0x0600707D RID: 28797 RVA: 0x001F261C File Offset: 0x001F081C
		internal OutlookBlockStatusProperty() : base("OutlookBlockStatusProperty", typeof(BlockStatus), PropertyFlags.None, PropertyDefinitionConstraint.None, new PropertyDependency[]
		{
			new PropertyDependency(InternalSchema.NativeBlockStatus, PropertyDependencyType.AllRead),
			new PropertyDependency(InternalSchema.ReceivedTime, PropertyDependencyType.AllRead)
		})
		{
		}

		// Token: 0x0600707E RID: 28798 RVA: 0x001F2668 File Offset: 0x001F0868
		protected override object InternalTryGetValue(PropertyBag.BasicPropertyStore propertyBag)
		{
			int valueOrDefault = propertyBag.GetValueOrDefault<int>(InternalSchema.NativeBlockStatus);
			if (valueOrDefault < 0)
			{
				return new PropertyError(this, PropertyErrorCode.GetCalculatedPropertyError);
			}
			if (valueOrDefault <= 3)
			{
				return (BlockStatus)valueOrDefault;
			}
			ExDateTime valueOrDefault2 = propertyBag.GetValueOrDefault<ExDateTime>(InternalSchema.ReceivedTime, ExDateTime.MinValue);
			if (!(valueOrDefault2 != ExDateTime.MinValue))
			{
				return BlockStatus.DontKnow;
			}
			double floatDate = ExTimeZone.UtcTimeZone.ConvertDateTime(valueOrDefault2).ToOADate();
			int num = OutlookBlockStatusProperty.ComputeBlockStatus(floatDate);
			int num2 = (num >= valueOrDefault) ? (num - valueOrDefault) : (valueOrDefault - num);
			if (num2 < 1)
			{
				return BlockStatus.NoNeverAgain;
			}
			return BlockStatus.DontKnow;
		}

		// Token: 0x0600707F RID: 28799 RVA: 0x001F26FC File Offset: 0x001F08FC
		protected override void InternalSetValue(PropertyBag.BasicPropertyStore propertyBag, object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("OutlookBlockStatus");
			}
			EnumValidator.ThrowIfInvalid<BlockStatus>((BlockStatus)value, "value");
			int num = (int)value;
			if (num < 3)
			{
				propertyBag.SetValueWithFixup(InternalSchema.NativeBlockStatus, (BlockStatus)num);
				return;
			}
			ExDateTime valueOrDefault = propertyBag.GetValueOrDefault<ExDateTime>(InternalSchema.ReceivedTime, ExDateTime.MinValue);
			if (valueOrDefault != ExDateTime.MinValue)
			{
				double floatDate = ExTimeZone.UtcTimeZone.ConvertDateTime(valueOrDefault).ToOADate();
				int num2 = OutlookBlockStatusProperty.ComputeBlockStatus(floatDate);
				propertyBag.SetValueWithFixup(InternalSchema.NativeBlockStatus, (BlockStatus)num2);
				return;
			}
			propertyBag.SetValueWithFixup(InternalSchema.NativeBlockStatus, BlockStatus.DontKnow);
		}

		// Token: 0x06007080 RID: 28800 RVA: 0x001F27A4 File Offset: 0x001F09A4
		internal override QueryFilter SmartFilterToNativeFilter(SinglePropertyFilter filter)
		{
			return base.SinglePropertySmartFilterToNativeFilter(filter, InternalSchema.NativeBlockStatus);
		}

		// Token: 0x06007081 RID: 28801 RVA: 0x001F27B2 File Offset: 0x001F09B2
		internal override QueryFilter NativeFilterToSmartFilter(QueryFilter filter)
		{
			return base.SinglePropertyNativeFilterToSmartFilter(filter, InternalSchema.NativeBlockStatus);
		}

		// Token: 0x06007082 RID: 28802 RVA: 0x001F27C0 File Offset: 0x001F09C0
		internal override void RegisterFilterTranslation()
		{
			FilterRestrictionConverter.RegisterFilterTranslation(this, typeof(ComparisonFilter));
			FilterRestrictionConverter.RegisterFilterTranslation(this, typeof(ExistsFilter));
		}

		// Token: 0x17001E35 RID: 7733
		// (get) Token: 0x06007083 RID: 28803 RVA: 0x001F27E2 File Offset: 0x001F09E2
		public override StorePropertyCapabilities Capabilities
		{
			get
			{
				return StorePropertyCapabilities.All;
			}
		}

		// Token: 0x06007084 RID: 28804 RVA: 0x001F27E5 File Offset: 0x001F09E5
		protected override NativeStorePropertyDefinition GetSortProperty()
		{
			return InternalSchema.NativeBlockStatus;
		}

		// Token: 0x06007085 RID: 28805 RVA: 0x001F27EC File Offset: 0x001F09EC
		private static int ComputeBlockStatus(double floatDate)
		{
			return (int)((floatDate - Math.Floor(floatDate)) * 100000000.0) + 3;
		}

		// Token: 0x04004DB0 RID: 19888
		private const int BlockStatusErr = 1;
	}
}
