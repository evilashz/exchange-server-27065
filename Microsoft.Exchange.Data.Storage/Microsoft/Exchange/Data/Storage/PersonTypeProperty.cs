using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000CA1 RID: 3233
	[ClassAccessLevel(AccessLevel.Implementation)]
	[Serializable]
	internal class PersonTypeProperty : SmartPropertyDefinition
	{
		// Token: 0x060070C8 RID: 28872 RVA: 0x001F45C9 File Offset: 0x001F27C9
		internal PersonTypeProperty() : base("PersonType", typeof(PersonType), PropertyFlags.None, PropertyDefinitionConstraint.None, PersonTypeProperty.PropertyDependencies)
		{
		}

		// Token: 0x17001E3E RID: 7742
		// (get) Token: 0x060070C9 RID: 28873 RVA: 0x001F45EB File Offset: 0x001F27EB
		public override StorePropertyCapabilities Capabilities
		{
			get
			{
				return StorePropertyCapabilities.None;
			}
		}

		// Token: 0x060070CA RID: 28874 RVA: 0x001F45F0 File Offset: 0x001F27F0
		protected override object InternalTryGetValue(PropertyBag.BasicPropertyStore propertyBag)
		{
			object obj = PersonTypeProperty.ExceptionHandlingGetValueOrDefault<object>(propertyBag, InternalSchema.InternalPersonType, null);
			if (obj is int)
			{
				return (PersonType)obj;
			}
			bool flag = PersonTypeProperty.ExceptionHandlingGetValueOrDefault<bool>(propertyBag, InternalSchema.IsDistributionListContact, false);
			if (flag)
			{
				return PersonType.DistributionList;
			}
			string itemClass = PersonTypeProperty.ExceptionHandlingGetValueOrDefault<string>(propertyBag, InternalSchema.ItemClass, string.Empty);
			if (ObjectClass.IsDistributionList(itemClass))
			{
				return PersonType.DistributionList;
			}
			return new PropertyError(this, PropertyErrorCode.NotFound);
		}

		// Token: 0x060070CB RID: 28875 RVA: 0x001F465C File Offset: 0x001F285C
		protected override void InternalSetValue(PropertyBag.BasicPropertyStore propertyBag, object value)
		{
			if (!(value is PersonType))
			{
				throw new ArgumentException("value");
			}
			propertyBag.SetValue(InternalSchema.InternalPersonType, (int)value);
		}

		// Token: 0x060070CC RID: 28876 RVA: 0x001F4688 File Offset: 0x001F2888
		private static T ExceptionHandlingGetValueOrDefault<T>(PropertyBag.BasicPropertyStore propertyBag, AtomicStorePropertyDefinition propertyDefinition, T defaultValue)
		{
			T result;
			try
			{
				result = propertyBag.GetValueOrDefault<T>(propertyDefinition, defaultValue);
			}
			catch (NotInBagPropertyErrorException)
			{
				result = defaultValue;
			}
			return result;
		}

		// Token: 0x04004E67 RID: 20071
		private static readonly PropertyDependency[] PropertyDependencies = new PropertyDependency[]
		{
			new PropertyDependency(InternalSchema.InternalPersonType, PropertyDependencyType.AllRead),
			new PropertyDependency(InternalSchema.IsDistributionListContact, PropertyDependencyType.NeedForRead),
			new PropertyDependency(InternalSchema.ItemClass, PropertyDependencyType.NeedForRead)
		};
	}
}
