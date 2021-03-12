using System;
using Microsoft.Exchange.AirSync.SchemaConverter.Common;

namespace Microsoft.Exchange.AirSync.SchemaConverter.Entity
{
	// Token: 0x020001A9 RID: 425
	[Serializable]
	internal class EntityStringProperty : EntityProperty, IStringProperty, IProperty
	{
		// Token: 0x0600121F RID: 4639 RVA: 0x00062747 File Offset: 0x00060947
		public EntityStringProperty(EntityPropertyDefinition propertyDefinition, bool syncForOccurenceItem = false) : base(propertyDefinition, PropertyType.ReadWrite, syncForOccurenceItem)
		{
		}

		// Token: 0x06001220 RID: 4640 RVA: 0x00062752 File Offset: 0x00060952
		public EntityStringProperty(EntityPropertyDefinition propertyDefinition, PropertyType type, bool syncForOccurenceItem = false) : base(propertyDefinition, type, syncForOccurenceItem)
		{
		}

		// Token: 0x170006CE RID: 1742
		// (get) Token: 0x06001221 RID: 4641 RVA: 0x00062760 File Offset: 0x00060960
		public virtual string StringData
		{
			get
			{
				if (base.EntityPropertyDefinition.Getter == null)
				{
					throw new ConversionException("Unable to retrieve data of type " + base.EntityPropertyDefinition.Type.FullName);
				}
				return (string)base.EntityPropertyDefinition.Getter(base.Item);
			}
		}

		// Token: 0x06001222 RID: 4642 RVA: 0x000627B8 File Offset: 0x000609B8
		public override void CopyFrom(IProperty srcProperty)
		{
			if (base.EntityPropertyDefinition.Setter == null)
			{
				throw new ConversionException("Unable to set data of type " + base.EntityPropertyDefinition.Type.FullName);
			}
			IStringProperty stringProperty = srcProperty as IStringProperty;
			if (stringProperty != null)
			{
				base.EntityPropertyDefinition.Setter(base.Item, stringProperty.StringData);
			}
		}
	}
}
