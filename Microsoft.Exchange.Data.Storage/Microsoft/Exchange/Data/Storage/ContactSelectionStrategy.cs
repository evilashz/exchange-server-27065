using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020004CF RID: 1231
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal abstract class ContactSelectionStrategy : SelectionStrategy
	{
		// Token: 0x060035EF RID: 13807 RVA: 0x000D98DA File Offset: 0x000D7ADA
		public override StorePropertyDefinition[] RequiredProperties()
		{
			return ContactSelectionStrategy.DefaultRequiredProperties;
		}

		// Token: 0x060035F0 RID: 13808 RVA: 0x000D98E1 File Offset: 0x000D7AE1
		public static SelectionStrategy CreatePersonNameProperty(StorePropertyDefinition propertyDefinition)
		{
			Util.ThrowOnNullArgument(propertyDefinition, "propertyDefinition");
			return new ContactSelectionStrategy.PersonNamePropertySelection(propertyDefinition);
		}

		// Token: 0x060035F1 RID: 13809 RVA: 0x000D98F4 File Offset: 0x000D7AF4
		public override bool HasPriority(IStorePropertyBag contact1, IStorePropertyBag contact2)
		{
			return ContactSelectionStrategy.HasDefaultPriority(contact1, contact2);
		}

		// Token: 0x060035F2 RID: 13810 RVA: 0x000D9900 File Offset: 0x000D7B00
		public static bool HasDefaultPriority(IStorePropertyBag contact1, IStorePropertyBag contact2)
		{
			Util.ThrowOnNullArgument(contact1, "contact1");
			Util.ThrowOnNullArgument(contact2, "contact2");
			string valueOrDefault = contact1.GetValueOrDefault<string>(InternalSchema.PartnerNetworkId, string.Empty);
			string valueOrDefault2 = contact2.GetValueOrDefault<string>(InternalSchema.PartnerNetworkId, string.Empty);
			int num = ContactSelectionStrategy.NumericalRankingFromPartnerNetworkId(valueOrDefault);
			int num2 = ContactSelectionStrategy.NumericalRankingFromPartnerNetworkId(valueOrDefault2);
			if (num < num2)
			{
				return true;
			}
			if (num == num2)
			{
				ExDateTime valueOrDefault3 = contact1.GetValueOrDefault<ExDateTime>(InternalSchema.CreationTime, ExDateTime.MinValue);
				ExDateTime valueOrDefault4 = contact2.GetValueOrDefault<ExDateTime>(InternalSchema.CreationTime, ExDateTime.MinValue);
				return valueOrDefault3 > valueOrDefault4;
			}
			return false;
		}

		// Token: 0x060035F3 RID: 13811 RVA: 0x000D998C File Offset: 0x000D7B8C
		private static int NumericalRankingFromPartnerNetworkId(string partnerNetworkId)
		{
			if (string.IsNullOrWhiteSpace(partnerNetworkId))
			{
				return 0;
			}
			if (string.Equals(partnerNetworkId, WellKnownNetworkNames.GAL))
			{
				return 1;
			}
			if (string.Equals(partnerNetworkId, WellKnownNetworkNames.LinkedIn))
			{
				return 4;
			}
			if (string.Equals(partnerNetworkId, WellKnownNetworkNames.Facebook))
			{
				return 5;
			}
			return 9;
		}

		// Token: 0x060035F4 RID: 13812 RVA: 0x000D99C7 File Offset: 0x000D7BC7
		public static SelectionStrategy CreateSingleSourceProperty(StorePropertyDefinition propertyDefinition)
		{
			Util.ThrowOnNullArgument(propertyDefinition, "propertyDefinition");
			return new ContactSelectionStrategy.ContactSingleSourcePropertySelection(propertyDefinition);
		}

		// Token: 0x060035F5 RID: 13813 RVA: 0x000D9A1A File Offset: 0x000D7C1A
		protected ContactSelectionStrategy() : base(new StorePropertyDefinition[0])
		{
		}

		// Token: 0x04001CF2 RID: 7410
		internal static readonly StorePropertyDefinition[] DefaultRequiredProperties = new StorePropertyDefinition[]
		{
			InternalSchema.CreationTime,
			InternalSchema.PartnerNetworkId
		};

		// Token: 0x04001CF3 RID: 7411
		public static readonly ContactSelectionStrategy PhotoContactIdProperty = new ContactSelectionStrategy.PhotoContactIdSelection();

		// Token: 0x04001CF4 RID: 7412
		public static readonly SelectionStrategy FileAsIdProperty = new ContactSelectionStrategy.FileAsIdPropertySelection();

		// Token: 0x020004D0 RID: 1232
		private sealed class PhotoContactIdSelection : ContactSelectionStrategy
		{
			// Token: 0x060035F7 RID: 13815 RVA: 0x000D9A28 File Offset: 0x000D7C28
			public override StorePropertyDefinition[] RequiredProperties()
			{
				return PropertyDefinitionCollection.Merge<StorePropertyDefinition>(base.RequiredProperties(), ContactSelectionStrategy.PhotoContactIdSelection.RequiredPropertiesArray);
			}

			// Token: 0x060035F8 RID: 13816 RVA: 0x000D9A3A File Offset: 0x000D7C3A
			public override bool IsSelectable(IStorePropertyBag source)
			{
				Util.ThrowOnNullArgument(source, "source");
				return source.GetValueOrDefault<bool>(InternalSchema.HasPicture, false);
			}

			// Token: 0x060035F9 RID: 13817 RVA: 0x000D9A54 File Offset: 0x000D7C54
			public override object GetValue(IStorePropertyBag contact)
			{
				Util.ThrowOnNullArgument(contact, "contact");
				byte[] valueOrDefault = contact.GetValueOrDefault<byte[]>(InternalSchema.EntryId, null);
				if (valueOrDefault == null)
				{
					return null;
				}
				return StoreObjectId.FromProviderSpecificId(valueOrDefault, StoreObjectType.Unknown);
			}

			// Token: 0x04001CF5 RID: 7413
			private static readonly StorePropertyDefinition[] RequiredPropertiesArray = new StorePropertyDefinition[]
			{
				InternalSchema.EntryId,
				InternalSchema.HasPicture
			};
		}

		// Token: 0x020004D1 RID: 1233
		private abstract class PersonDisplayNameBasedPropertySelection : SelectionStrategy.SingleSourcePropertySelection
		{
			// Token: 0x060035FC RID: 13820 RVA: 0x000D9ABA File Offset: 0x000D7CBA
			public override StorePropertyDefinition[] RequiredProperties()
			{
				return ContactSelectionStrategy.PersonDisplayNameBasedPropertySelection.RequiredPropertiesArray;
			}

			// Token: 0x060035FD RID: 13821 RVA: 0x000D9AC1 File Offset: 0x000D7CC1
			public PersonDisplayNameBasedPropertySelection(StorePropertyDefinition sourceProperty) : base(sourceProperty)
			{
			}

			// Token: 0x060035FE RID: 13822 RVA: 0x000D9ACC File Offset: 0x000D7CCC
			public override bool IsSelectable(IStorePropertyBag source)
			{
				Util.ThrowOnNullArgument(source, "source");
				string valueOrDefault = source.GetValueOrDefault<string>(ContactBaseSchema.DisplayNameFirstLast, null);
				return !string.IsNullOrWhiteSpace(valueOrDefault);
			}

			// Token: 0x060035FF RID: 13823 RVA: 0x000D9AFC File Offset: 0x000D7CFC
			public override object GetValue(IStorePropertyBag contact)
			{
				return contact.GetValueOrDefault<object>(base.SourceProperty, null);
			}

			// Token: 0x06003600 RID: 13824 RVA: 0x000D9B0C File Offset: 0x000D7D0C
			public override bool HasPriority(IStorePropertyBag contact1, IStorePropertyBag contact2)
			{
				Util.ThrowOnNullArgument(contact1, "contact1");
				Util.ThrowOnNullArgument(contact2, "contact2");
				int valueOrDefault = contact1.GetValueOrDefault<int>(ContactBaseSchema.DisplayNamePriority, int.MaxValue);
				int valueOrDefault2 = contact2.GetValueOrDefault<int>(ContactBaseSchema.DisplayNamePriority, int.MaxValue);
				return valueOrDefault < valueOrDefault2 || (valueOrDefault <= valueOrDefault2 && ContactSelectionStrategy.HasDefaultPriority(contact1, contact2));
			}

			// Token: 0x04001CF6 RID: 7414
			private static readonly StorePropertyDefinition[] RequiredPropertiesArray = new StorePropertyDefinition[]
			{
				InternalSchema.CreationTime,
				InternalSchema.DisplayNameFirstLast,
				InternalSchema.DisplayNamePriority
			};
		}

		// Token: 0x020004D2 RID: 1234
		private sealed class PersonNamePropertySelection : ContactSelectionStrategy.PersonDisplayNameBasedPropertySelection
		{
			// Token: 0x06003602 RID: 13826 RVA: 0x000D9B96 File Offset: 0x000D7D96
			public PersonNamePropertySelection(StorePropertyDefinition sourceProperty) : base(sourceProperty)
			{
			}
		}

		// Token: 0x020004D3 RID: 1235
		private sealed class FileAsIdPropertySelection : ContactSelectionStrategy.PersonDisplayNameBasedPropertySelection
		{
			// Token: 0x06003603 RID: 13827 RVA: 0x000D9B9F File Offset: 0x000D7D9F
			public FileAsIdPropertySelection() : base(InternalSchema.FileAsId)
			{
			}

			// Token: 0x06003604 RID: 13828 RVA: 0x000D9BAC File Offset: 0x000D7DAC
			public override object GetValue(IStorePropertyBag contact)
			{
				Util.ThrowOnNullArgument(contact, "contact");
				return contact.GetValueOrDefault<FileAsMapping>(InternalSchema.FileAsId, FileAsMapping.None).ToString();
			}
		}

		// Token: 0x020004D4 RID: 1236
		private class ContactSingleSourcePropertySelection : SelectionStrategy.SingleSourcePropertySelection
		{
			// Token: 0x06003605 RID: 13829 RVA: 0x000D9BCF File Offset: 0x000D7DCF
			public ContactSingleSourcePropertySelection(StorePropertyDefinition sourceProperty) : base(sourceProperty)
			{
			}

			// Token: 0x06003606 RID: 13830 RVA: 0x000D9BD8 File Offset: 0x000D7DD8
			protected ContactSingleSourcePropertySelection(StorePropertyDefinition sourceProperty, params StorePropertyDefinition[] additionalDependencies) : base(sourceProperty, additionalDependencies)
			{
			}

			// Token: 0x06003607 RID: 13831 RVA: 0x000D9BE2 File Offset: 0x000D7DE2
			public override StorePropertyDefinition[] RequiredProperties()
			{
				return ContactSelectionStrategy.DefaultRequiredProperties;
			}

			// Token: 0x06003608 RID: 13832 RVA: 0x000D9BE9 File Offset: 0x000D7DE9
			public override bool HasPriority(IStorePropertyBag contact1, IStorePropertyBag contact2)
			{
				return ContactSelectionStrategy.HasDefaultPriority(contact1, contact2);
			}
		}
	}
}
