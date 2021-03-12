using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020008E8 RID: 2280
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal abstract class ItemSelectionStrategy : SelectionStrategy
	{
		// Token: 0x0600559A RID: 21914 RVA: 0x00162981 File Offset: 0x00160B81
		public override StorePropertyDefinition[] RequiredProperties()
		{
			return ItemSelectionStrategy.DefaultRequiredProperties;
		}

		// Token: 0x0600559B RID: 21915 RVA: 0x00162988 File Offset: 0x00160B88
		public override bool HasPriority(IStorePropertyBag item1, IStorePropertyBag item2)
		{
			return ItemSelectionStrategy.HasDefaultPriority(item1, item2);
		}

		// Token: 0x0600559C RID: 21916 RVA: 0x00162994 File Offset: 0x00160B94
		public static bool HasDefaultPriority(IStorePropertyBag item1, IStorePropertyBag item2)
		{
			Util.ThrowOnNullArgument(item1, "item1");
			Util.ThrowOnNullArgument(item2, "item2");
			ExDateTime valueOrDefault = item1.GetValueOrDefault<ExDateTime>(InternalSchema.CreationTime, ExDateTime.MinValue);
			ExDateTime valueOrDefault2 = item2.GetValueOrDefault<ExDateTime>(InternalSchema.CreationTime, ExDateTime.MinValue);
			return valueOrDefault > valueOrDefault2;
		}

		// Token: 0x0600559D RID: 21917 RVA: 0x001629E0 File Offset: 0x00160BE0
		public static SelectionStrategy CreateSingleSourceProperty(StorePropertyDefinition propertyDefinition)
		{
			Util.ThrowOnNullArgument(propertyDefinition, "propertyDefinition");
			return new ItemSelectionStrategy.ItemSingleSourcePropertySelection(propertyDefinition);
		}

		// Token: 0x0600559E RID: 21918 RVA: 0x00162A16 File Offset: 0x00160C16
		protected ItemSelectionStrategy() : base(new StorePropertyDefinition[0])
		{
		}

		// Token: 0x04002DF5 RID: 11765
		internal static readonly StorePropertyDefinition[] DefaultRequiredProperties = new StorePropertyDefinition[]
		{
			InternalSchema.CreationTime
		};

		// Token: 0x020008E9 RID: 2281
		internal class ItemSingleSourcePropertySelection : SelectionStrategy.SingleSourcePropertySelection
		{
			// Token: 0x060055A0 RID: 21920 RVA: 0x00162A24 File Offset: 0x00160C24
			public ItemSingleSourcePropertySelection(StorePropertyDefinition sourceProperty) : base(sourceProperty)
			{
			}

			// Token: 0x060055A1 RID: 21921 RVA: 0x00162A2D File Offset: 0x00160C2D
			protected ItemSingleSourcePropertySelection(StorePropertyDefinition sourceProperty, params StorePropertyDefinition[] additionalDependencies) : base(sourceProperty, additionalDependencies)
			{
			}

			// Token: 0x060055A2 RID: 21922 RVA: 0x00162A37 File Offset: 0x00160C37
			public override StorePropertyDefinition[] RequiredProperties()
			{
				return ItemSelectionStrategy.DefaultRequiredProperties;
			}

			// Token: 0x060055A3 RID: 21923 RVA: 0x00162A3E File Offset: 0x00160C3E
			public override bool HasPriority(IStorePropertyBag contact1, IStorePropertyBag contact2)
			{
				return ItemSelectionStrategy.HasDefaultPriority(contact1, contact2);
			}
		}
	}
}
