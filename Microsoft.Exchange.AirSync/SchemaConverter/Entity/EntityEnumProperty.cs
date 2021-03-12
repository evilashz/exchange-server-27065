using System;
using Microsoft.Exchange.AirSync.SchemaConverter.Common;

namespace Microsoft.Exchange.AirSync.SchemaConverter.Entity
{
	// Token: 0x020001A1 RID: 417
	[Serializable]
	internal class EntityEnumProperty : EntityProperty, IIntegerProperty, IProperty
	{
		// Token: 0x060011F5 RID: 4597 RVA: 0x00061D39 File Offset: 0x0005FF39
		public EntityEnumProperty(EntityPropertyDefinition propertyDefinition, bool syncForOccurenceItem = false) : base(propertyDefinition, PropertyType.ReadWrite, syncForOccurenceItem)
		{
		}

		// Token: 0x060011F6 RID: 4598 RVA: 0x00061D44 File Offset: 0x0005FF44
		public EntityEnumProperty(EntityPropertyDefinition propertyDefinition, PropertyType type, bool syncForOccurenceItem = false) : base(propertyDefinition, type, syncForOccurenceItem)
		{
		}

		// Token: 0x170006C3 RID: 1731
		// (get) Token: 0x060011F7 RID: 4599 RVA: 0x00061D50 File Offset: 0x0005FF50
		public virtual int IntegerData
		{
			get
			{
				if (base.EntityPropertyDefinition.Getter == null)
				{
					throw new ConversionException("Unable to retrieve data of type " + base.EntityPropertyDefinition.Type.FullName);
				}
				return (int)base.EntityPropertyDefinition.Getter(base.Item);
			}
		}

		// Token: 0x060011F8 RID: 4600 RVA: 0x00061DA8 File Offset: 0x0005FFA8
		public override void CopyFrom(IProperty srcProperty)
		{
			if (base.EntityPropertyDefinition.Setter == null)
			{
				throw new ConversionException("Unable to set data of type " + base.EntityPropertyDefinition.Type.FullName);
			}
			IIntegerProperty integerProperty = srcProperty as IIntegerProperty;
			if (integerProperty != null)
			{
				int integerData = integerProperty.IntegerData;
				if (!Enum.IsDefined(base.EntityPropertyDefinition.Type, integerData))
				{
					throw new ConversionException(string.Format("EntityEnumProperty.CopyFrom Type {0} does not have value {1} defined.", base.EntityPropertyDefinition.Type.FullName, integerData));
				}
				base.EntityPropertyDefinition.Setter(base.Item, integerData);
			}
		}
	}
}
