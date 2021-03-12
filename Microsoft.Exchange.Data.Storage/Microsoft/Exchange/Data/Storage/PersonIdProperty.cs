using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000C9E RID: 3230
	[ClassAccessLevel(AccessLevel.Implementation)]
	[Serializable]
	internal class PersonIdProperty : SmartPropertyDefinition
	{
		// Token: 0x060070BA RID: 28858 RVA: 0x001F32AF File Offset: 0x001F14AF
		internal PersonIdProperty() : base("PersonId", typeof(PersonId), PropertyFlags.None, PropertyDefinitionConstraint.None, PersonIdProperty.PropertyDependencies)
		{
		}

		// Token: 0x060070BB RID: 28859 RVA: 0x001F32D4 File Offset: 0x001F14D4
		protected override object InternalTryGetValue(PropertyBag.BasicPropertyStore propertyBag)
		{
			object value = propertyBag.GetValue(InternalSchema.ConversationIndex);
			byte[] array = value as byte[];
			if (array == null)
			{
				PropertyError propertyError = (PropertyError)value;
				if (propertyError.PropertyErrorCode == PropertyErrorCode.NotEnoughMemory)
				{
					return new PropertyError(this, PropertyErrorCode.CorruptedData);
				}
				return new PropertyError(this, propertyError.PropertyErrorCode);
			}
			else
			{
				if (array.Length < 22)
				{
					return new PropertyError(this, PropertyErrorCode.CorruptedData);
				}
				byte[] array2 = new byte[16];
				Array.Copy(array, 6, array2, 0, 16);
				object result;
				try
				{
					result = PersonId.Create(array2);
				}
				catch (CorruptDataException)
				{
					result = new PropertyError(this, PropertyErrorCode.CorruptedData);
				}
				return result;
			}
		}

		// Token: 0x060070BC RID: 28860 RVA: 0x001F336C File Offset: 0x001F156C
		protected override void InternalSetValue(PropertyBag.BasicPropertyStore propertyBag, object value)
		{
			PersonId personId = value as PersonId;
			if (personId == null)
			{
				throw new ArgumentException("value");
			}
			byte[] array = new byte[22];
			array[0] = 1;
			Array.Copy(personId.GetBytes(), 0, array, 6, 16);
			propertyBag.SetValue(InternalSchema.ConversationIndex, array);
			propertyBag.SetValue(InternalSchema.ConversationIndexTracking, true);
		}

		// Token: 0x060070BD RID: 28861 RVA: 0x001F33CC File Offset: 0x001F15CC
		internal override QueryFilter NativeFilterToSmartFilter(QueryFilter filter)
		{
			ComparisonFilter comparisonFilter = filter as ComparisonFilter;
			if (comparisonFilter == null || !comparisonFilter.Property.Equals(InternalSchema.MapiConversationId))
			{
				throw base.CreateInvalidFilterConversionException(filter);
			}
			return new ComparisonFilter(comparisonFilter.ComparisonOperator, this, PersonId.Create((byte[])comparisonFilter.PropertyValue));
		}

		// Token: 0x060070BE RID: 28862 RVA: 0x001F341C File Offset: 0x001F161C
		internal override QueryFilter SmartFilterToNativeFilter(SinglePropertyFilter filter)
		{
			ComparisonFilter comparisonFilter = filter as ComparisonFilter;
			if (comparisonFilter == null || !comparisonFilter.Property.Equals(this))
			{
				throw base.CreateInvalidFilterConversionException(filter);
			}
			PersonId personId = (PersonId)comparisonFilter.PropertyValue;
			return new ComparisonFilter(comparisonFilter.ComparisonOperator, InternalSchema.MapiConversationId, personId.GetBytes());
		}

		// Token: 0x060070BF RID: 28863 RVA: 0x001F346B File Offset: 0x001F166B
		protected override NativeStorePropertyDefinition GetSortProperty()
		{
			return InternalSchema.MapiConversationId;
		}

		// Token: 0x17001E3B RID: 7739
		// (get) Token: 0x060070C0 RID: 28864 RVA: 0x001F3472 File Offset: 0x001F1672
		public override StorePropertyCapabilities Capabilities
		{
			get
			{
				return StorePropertyCapabilities.All;
			}
		}

		// Token: 0x04004DF5 RID: 19957
		private const int MinimumConversationIndexLength = 22;

		// Token: 0x04004DF6 RID: 19958
		private const int PersonIdIndexIntoConversationIndexBlob = 6;

		// Token: 0x04004DF7 RID: 19959
		private const int PersonIdLengthInBytes = 16;

		// Token: 0x04004DF8 RID: 19960
		private static readonly PropertyDependency[] PropertyDependencies = new PropertyDependency[]
		{
			new PropertyDependency(InternalSchema.ConversationIndex, PropertyDependencyType.AllRead),
			new PropertyDependency(InternalSchema.ConversationIndexTracking, PropertyDependencyType.AllRead)
		};
	}
}
