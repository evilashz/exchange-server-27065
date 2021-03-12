using System;
using Microsoft.Exchange.AirSync.SchemaConverter.Common;

namespace Microsoft.Exchange.AirSync.SchemaConverter.Entity
{
	// Token: 0x0200019B RID: 411
	[Serializable]
	internal class EntityBooleanProperty : EntityProperty, IBooleanProperty, IProperty
	{
		// Token: 0x060011C5 RID: 4549 RVA: 0x0006140C File Offset: 0x0005F60C
		public EntityBooleanProperty(EntityPropertyDefinition propertyDefinition) : base(propertyDefinition, PropertyType.ReadWrite, false)
		{
		}

		// Token: 0x060011C6 RID: 4550 RVA: 0x00061417 File Offset: 0x0005F617
		public EntityBooleanProperty(EntityPropertyDefinition propertyDefinition, PropertyType type) : base(propertyDefinition, type, false)
		{
		}

		// Token: 0x170006B6 RID: 1718
		// (get) Token: 0x060011C7 RID: 4551 RVA: 0x00061424 File Offset: 0x0005F624
		public virtual bool BooleanData
		{
			get
			{
				if (base.EntityPropertyDefinition.Getter == null)
				{
					throw new ConversionException("Unable to retrieve data of type " + base.EntityPropertyDefinition.Type.FullName);
				}
				return (bool)base.EntityPropertyDefinition.Getter(base.Item);
			}
		}

		// Token: 0x060011C8 RID: 4552 RVA: 0x0006147C File Offset: 0x0005F67C
		public override void CopyFrom(IProperty srcProperty)
		{
			if (base.EntityPropertyDefinition.Setter == null)
			{
				throw new ConversionException("Unable to set data of type " + base.EntityPropertyDefinition.Type.FullName);
			}
			IBooleanProperty booleanProperty = srcProperty as IBooleanProperty;
			if (booleanProperty != null)
			{
				base.EntityPropertyDefinition.Setter(base.Item, booleanProperty.BooleanData);
			}
		}
	}
}
