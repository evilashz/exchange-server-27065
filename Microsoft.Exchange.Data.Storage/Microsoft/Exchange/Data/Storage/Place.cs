using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200054E RID: 1358
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class Place : Contact, IPlace, IContact, IContactBase, IItem, IStoreObject, IStorePropertyBag, IPropertyBag, IReadOnlyPropertyBag, IDisposable
	{
		// Token: 0x06003948 RID: 14664 RVA: 0x000EB48E File Offset: 0x000E968E
		internal Place(ICoreItem coreItem) : base(coreItem)
		{
			if (base.IsNew)
			{
				this.Initialize();
			}
		}

		// Token: 0x06003949 RID: 14665 RVA: 0x000EB4A5 File Offset: 0x000E96A5
		public new static Place Create(StoreSession session, StoreId contactFolderId)
		{
			return ItemBuilder.CreateNewItem<Place>(session, contactFolderId, ItemCreateInfo.PlaceInfo);
		}

		// Token: 0x0600394A RID: 14666 RVA: 0x000EB4B3 File Offset: 0x000E96B3
		public new static Place Bind(StoreSession session, StoreId storeId, params PropertyDefinition[] propsToReturn)
		{
			return Place.Bind(session, storeId, (ICollection<PropertyDefinition>)propsToReturn);
		}

		// Token: 0x0600394B RID: 14667 RVA: 0x000EB4C2 File Offset: 0x000E96C2
		public new static Place Bind(StoreSession session, StoreId storeId, ICollection<PropertyDefinition> propsToReturn)
		{
			return ItemBuilder.ItemBind<Place>(session, storeId, PlaceSchema.Instance, propsToReturn);
		}

		// Token: 0x170011CA RID: 4554
		// (get) Token: 0x0600394C RID: 14668 RVA: 0x000EB4D1 File Offset: 0x000E96D1
		public override Schema Schema
		{
			get
			{
				this.CheckDisposed("Schema::get");
				return PlaceSchema.Instance;
			}
		}

		// Token: 0x170011CB RID: 4555
		// (get) Token: 0x0600394D RID: 14669 RVA: 0x000EB4E3 File Offset: 0x000E96E3
		// (set) Token: 0x0600394E RID: 14670 RVA: 0x000EB4FC File Offset: 0x000E96FC
		public int RelevanceRank
		{
			get
			{
				this.CheckDisposed("RelevanceRank::get");
				return base.GetValueOrDefault<int>(PlaceSchema.LocationRelevanceRank, 0);
			}
			set
			{
				this.CheckDisposed("RelevanceRank::set");
				this[PlaceSchema.LocationRelevanceRank] = value;
			}
		}

		// Token: 0x0600394F RID: 14671 RVA: 0x000EB51A File Offset: 0x000E971A
		private void Initialize()
		{
			this[InternalSchema.ItemClass] = "IPM.Contact.Place";
		}
	}
}
