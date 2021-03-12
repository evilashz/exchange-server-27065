using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000AEE RID: 2798
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class ConversionPropertyBag : PropertyBag
	{
		// Token: 0x060065A1 RID: 26017 RVA: 0x001B0380 File Offset: 0x001AE580
		internal ConversionPropertyBag(IReadOnlyPropertyBag underlyingPropertyBag, SchemaConverter schemaConverter)
		{
			this.schemaConverter = schemaConverter;
			this.underlyingPropertyBag = underlyingPropertyBag;
			this.isReadOnly = !(this.underlyingPropertyBag is IPropertyBag);
		}

		// Token: 0x060065A2 RID: 26018 RVA: 0x001B03B8 File Offset: 0x001AE5B8
		protected override object TryGetStoreProperty(StorePropertyDefinition propertyDefinition)
		{
			SchemaConverter.Getter getter = this.schemaConverter.GetGetter(propertyDefinition);
			if (getter != null)
			{
				object obj = getter(this.underlyingPropertyBag);
				if (obj is PropertyErrorCode)
				{
					obj = new PropertyError(propertyDefinition, (PropertyErrorCode)obj);
				}
				return ExTimeZoneHelperForMigrationOnly.ToExDateTimeIfObjectIsDateTime(this.exTimeZone, obj);
			}
			return new PropertyError(propertyDefinition, PropertyErrorCode.NotSupported);
		}

		// Token: 0x060065A3 RID: 26019 RVA: 0x001B040C File Offset: 0x001AE60C
		protected override void SetValidatedStoreProperty(StorePropertyDefinition propertyDefinition, object propertyValue)
		{
			SchemaConverter.Setter setter = this.schemaConverter.GetSetter(propertyDefinition);
			if (!this.isReadOnly && setter != null)
			{
				setter((IPropertyBag)this.underlyingPropertyBag, ExTimeZoneHelperForMigrationOnly.ToUtcIfDateTime(propertyValue));
				return;
			}
			throw PropertyError.ToException(new PropertyError[]
			{
				new PropertyError(propertyDefinition, PropertyErrorCode.NotSupported)
			});
		}

		// Token: 0x060065A4 RID: 26020 RVA: 0x001B0460 File Offset: 0x001AE660
		protected override void DeleteStoreProperty(StorePropertyDefinition propertyDefinition)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060065A5 RID: 26021 RVA: 0x001B0467 File Offset: 0x001AE667
		protected override bool InternalIsPropertyDirty(AtomicStorePropertyDefinition propertyDefinition)
		{
			throw new NotSupportedException();
		}

		// Token: 0x17001C0E RID: 7182
		// (get) Token: 0x060065A6 RID: 26022 RVA: 0x001B046E File Offset: 0x001AE66E
		// (set) Token: 0x060065A7 RID: 26023 RVA: 0x001B0476 File Offset: 0x001AE676
		internal override ExTimeZone ExTimeZone
		{
			get
			{
				return this.exTimeZone;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("ExTimeZone");
				}
				this.exTimeZone = value;
			}
		}

		// Token: 0x060065A8 RID: 26024 RVA: 0x001B048D File Offset: 0x001AE68D
		protected override bool IsLoaded(NativeStorePropertyDefinition propertyDefinition)
		{
			throw new NotSupportedException();
		}

		// Token: 0x17001C0F RID: 7183
		// (get) Token: 0x060065A9 RID: 26025 RVA: 0x001B0494 File Offset: 0x001AE694
		public override bool IsDirty
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x060065AA RID: 26026 RVA: 0x001B049B File Offset: 0x001AE69B
		public override void Load(ICollection<PropertyDefinition> propsToLoad)
		{
			throw new NotSupportedException();
		}

		// Token: 0x040039D2 RID: 14802
		private readonly bool isReadOnly;

		// Token: 0x040039D3 RID: 14803
		private readonly SchemaConverter schemaConverter;

		// Token: 0x040039D4 RID: 14804
		private ExTimeZone exTimeZone = ExTimeZone.UtcTimeZone;

		// Token: 0x040039D5 RID: 14805
		private readonly IReadOnlyPropertyBag underlyingPropertyBag;
	}
}
