using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Entities.DataModel.PropertyBags;
using Microsoft.Exchange.Entities.DataProviders;
using Microsoft.Exchange.Entities.TypeConversion.PropertyAccessors;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Entities.TypeConversion.PropertyTranslationRules
{
	// Token: 0x0200007A RID: 122
	internal class StorageTimeZoneSensitiveTimeTranslationRule<TStoreObject, TEntity> : IStorageTranslationRule<!0, !1>, IPropertyValueCollectionTranslationRule<TStoreObject, Microsoft.Exchange.Data.PropertyDefinition, TEntity>, ITranslationRule<TStoreObject, TEntity> where TStoreObject : IStoreObject where TEntity : IPropertyChangeTracker<Microsoft.Exchange.Entities.DataModel.PropertyBags.PropertyDefinition>
	{
		// Token: 0x060002A2 RID: 674 RVA: 0x00008C80 File Offset: 0x00006E80
		public StorageTimeZoneSensitiveTimeTranslationRule(IStoragePropertyAccessor<TStoreObject, ExDateTime> storageTimeAccessor, IStoragePropertyAccessor<TStoreObject, ExTimeZone> storageTimeZoneAccessor, EntityPropertyAccessor<TEntity, ExDateTime> entityTimeAccessor, EntityPropertyAccessor<TEntity, string> entityIntendedTimeZoneIdAccessor, DateTimeHelper dateTimeHelper)
		{
			this.Helper = dateTimeHelper;
			this.storageTimeAccessor = storageTimeAccessor;
			this.storageTimeZoneAccessor = storageTimeZoneAccessor;
			this.entityTimeAccessor = entityTimeAccessor;
			this.entityIntendedTimeZoneIdAccessor = entityIntendedTimeZoneIdAccessor;
			this.StorageDependencies = this.storageTimeAccessor.Dependencies.Union(this.storageTimeZoneAccessor.Dependencies);
			this.StoragePropertyGroup = this.storageTimeAccessor.PropertyChangeMetadataGroup;
			this.EntityProperties = new Microsoft.Exchange.Entities.DataModel.PropertyBags.PropertyDefinition[]
			{
				this.entityTimeAccessor.PropertyDefinition,
				this.entityIntendedTimeZoneIdAccessor.PropertyDefinition
			};
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x060002A3 RID: 675 RVA: 0x00008D14 File Offset: 0x00006F14
		// (set) Token: 0x060002A4 RID: 676 RVA: 0x00008D1C File Offset: 0x00006F1C
		public IEnumerable<Microsoft.Exchange.Entities.DataModel.PropertyBags.PropertyDefinition> EntityProperties { get; private set; }

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x060002A5 RID: 677 RVA: 0x00008D25 File Offset: 0x00006F25
		// (set) Token: 0x060002A6 RID: 678 RVA: 0x00008D2D File Offset: 0x00006F2D
		public IEnumerable<Microsoft.Exchange.Data.PropertyDefinition> StorageDependencies { get; private set; }

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x060002A7 RID: 679 RVA: 0x00008D36 File Offset: 0x00006F36
		// (set) Token: 0x060002A8 RID: 680 RVA: 0x00008D3E File Offset: 0x00006F3E
		public PropertyChangeMetadata.PropertyGroup StoragePropertyGroup { get; private set; }

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x060002A9 RID: 681 RVA: 0x00008D47 File Offset: 0x00006F47
		// (set) Token: 0x060002AA RID: 682 RVA: 0x00008D4F File Offset: 0x00006F4F
		private protected DateTimeHelper Helper { protected get; private set; }

		// Token: 0x060002AB RID: 683 RVA: 0x00008D94 File Offset: 0x00006F94
		public void FromLeftToRightType(TStoreObject left, TEntity right)
		{
			this.FromLeftToRight(right, delegate(out ExDateTime value)
			{
				return this.storageTimeAccessor.TryGetValue(left, out value);
			}, delegate(out ExTimeZone value)
			{
				return this.storageTimeZoneAccessor.TryGetValue(left, out value);
			});
		}

		// Token: 0x060002AC RID: 684 RVA: 0x00008E58 File Offset: 0x00007058
		public void FromPropertyValues(IDictionary<Microsoft.Exchange.Data.PropertyDefinition, int> propertyIndices, IList values, TEntity right)
		{
			this.FromLeftToRight(right, delegate(out ExDateTime value)
			{
				IPropertyValueCollectionAccessor<TStoreObject, Microsoft.Exchange.Data.PropertyDefinition, ExDateTime> propertyValueCollectionAccessor = this.storageTimeAccessor as IPropertyValueCollectionAccessor<TStoreObject, Microsoft.Exchange.Data.PropertyDefinition, ExDateTime>;
				if (propertyValueCollectionAccessor == null)
				{
					value = default(ExDateTime);
					return false;
				}
				return propertyValueCollectionAccessor.TryGetValue(propertyIndices, values, out value);
			}, delegate(out ExTimeZone value)
			{
				IPropertyValueCollectionAccessor<TStoreObject, Microsoft.Exchange.Data.PropertyDefinition, ExTimeZone> propertyValueCollectionAccessor = this.storageTimeZoneAccessor as IPropertyValueCollectionAccessor<TStoreObject, Microsoft.Exchange.Data.PropertyDefinition, ExTimeZone>;
				if (propertyValueCollectionAccessor == null)
				{
					value = null;
					return false;
				}
				return propertyValueCollectionAccessor.TryGetValue(propertyIndices, values, out value);
			});
		}

		// Token: 0x060002AD RID: 685 RVA: 0x00008EA0 File Offset: 0x000070A0
		public void FromRightToLeftType(TStoreObject left, TEntity right)
		{
			ExDateTime exDateTime;
			if (this.storageTimeAccessor.Readonly)
			{
				ExTimeZone value;
				if (!this.storageTimeZoneAccessor.Readonly && this.GetTimeZone(right, this.entityIntendedTimeZoneIdAccessor, out value))
				{
					this.storageTimeZoneAccessor.Set(left, value);
					return;
				}
			}
			else if (this.entityTimeAccessor.TryGetValue(right, out exDateTime))
			{
				ExTimeZone exTimeZone;
				if (this.GetTimeZone(right, this.entityIntendedTimeZoneIdAccessor, out exTimeZone))
				{
					if (!this.storageTimeZoneAccessor.Readonly)
					{
						this.storageTimeZoneAccessor.Set(left, exTimeZone);
					}
					ExDateTime value2 = exTimeZone.ConvertDateTime(exDateTime);
					this.storageTimeAccessor.Set(left, value2);
					return;
				}
				this.storageTimeAccessor.Set(left, exDateTime);
			}
		}

		// Token: 0x060002AE RID: 686 RVA: 0x00008F44 File Offset: 0x00007144
		private bool GetTimeZone(TEntity entity, IPropertyAccessor<TEntity, string> accessor, out ExTimeZone timeZone)
		{
			timeZone = null;
			string id;
			if (accessor.TryGetValue(entity, out id))
			{
				this.Helper.TryParseTimeZoneId(id, out timeZone);
				if (timeZone == null)
				{
					throw new InvalidRequestException(Strings.ErrorInvalidTimeZoneId(id));
				}
			}
			return timeZone != null;
		}

		// Token: 0x060002AF RID: 687 RVA: 0x00008F88 File Offset: 0x00007188
		private void FromLeftToRight(TEntity entity, StorageTimeZoneSensitiveTimeTranslationRule<TStoreObject, TEntity>.TryGetValueFunc<ExDateTime> timeGetter, StorageTimeZoneSensitiveTimeTranslationRule<TStoreObject, TEntity>.TryGetValueFunc<ExTimeZone> timeZoneGetter)
		{
			ExDateTime value;
			if (timeGetter != null && timeGetter(out value))
			{
				this.entityTimeAccessor.Set(entity, value);
			}
			ExTimeZone exTimeZone;
			if (timeZoneGetter != null && timeZoneGetter(out exTimeZone))
			{
				this.entityIntendedTimeZoneIdAccessor.Set(entity, exTimeZone.Id);
			}
		}

		// Token: 0x040000FA RID: 250
		private readonly EntityPropertyAccessor<TEntity, string> entityIntendedTimeZoneIdAccessor;

		// Token: 0x040000FB RID: 251
		private readonly EntityPropertyAccessor<TEntity, ExDateTime> entityTimeAccessor;

		// Token: 0x040000FC RID: 252
		private readonly IStoragePropertyAccessor<TStoreObject, ExDateTime> storageTimeAccessor;

		// Token: 0x040000FD RID: 253
		private readonly IStoragePropertyAccessor<TStoreObject, ExTimeZone> storageTimeZoneAccessor;

		// Token: 0x0200007B RID: 123
		// (Invoke) Token: 0x060002B1 RID: 689
		public delegate bool TryGetValueFunc<TValue>(out TValue value);
	}
}
