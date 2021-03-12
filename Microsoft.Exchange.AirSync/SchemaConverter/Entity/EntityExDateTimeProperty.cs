using System;
using Microsoft.Exchange.AirSync.SchemaConverter.Common;
using Microsoft.Exchange.Entities.DataModel.Items;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.AirSync.SchemaConverter.Entity
{
	// Token: 0x020001A2 RID: 418
	[Serializable]
	internal class EntityExDateTimeProperty : EntityProperty, IDateTimeProperty, IProperty
	{
		// Token: 0x060011F9 RID: 4601 RVA: 0x00061E4E File Offset: 0x0006004E
		public EntityExDateTimeProperty(EntityPropertyDefinition propertyDefinition, bool syncForOccurenceItem = false) : base(propertyDefinition, PropertyType.ReadWrite, syncForOccurenceItem)
		{
		}

		// Token: 0x060011FA RID: 4602 RVA: 0x00061E59 File Offset: 0x00060059
		public EntityExDateTimeProperty(EntityPropertyDefinition propertyDefinition, PropertyType type, bool syncForOccurenceItem = false) : base(propertyDefinition, type, syncForOccurenceItem)
		{
		}

		// Token: 0x170006C4 RID: 1732
		// (get) Token: 0x060011FB RID: 4603 RVA: 0x00061E64 File Offset: 0x00060064
		public virtual ExDateTime DateTime
		{
			get
			{
				if (base.EntityPropertyDefinition.Getter == null)
				{
					throw new ConversionException("Unable to retrieve data of type " + base.EntityPropertyDefinition.Type.FullName);
				}
				return (ExDateTime)base.EntityPropertyDefinition.Getter(base.Item);
			}
		}

		// Token: 0x060011FC RID: 4604 RVA: 0x00061EB9 File Offset: 0x000600B9
		public override void Bind(IItem item)
		{
			base.Bind(item);
			if (base.State == PropertyState.Modified && this.DateTime == ExDateTime.MinValue)
			{
				base.State = PropertyState.SetToDefault;
			}
		}

		// Token: 0x060011FD RID: 4605 RVA: 0x00061EE4 File Offset: 0x000600E4
		public override void CopyFrom(IProperty srcProperty)
		{
			if (base.EntityPropertyDefinition.Setter == null)
			{
				throw new ConversionException("Unable to set data of type " + base.EntityPropertyDefinition.Type.FullName);
			}
			IDateTimeProperty dateTimeProperty = srcProperty as IDateTimeProperty;
			if (dateTimeProperty != null)
			{
				base.EntityPropertyDefinition.Setter(base.Item, dateTimeProperty.DateTime);
			}
		}
	}
}
