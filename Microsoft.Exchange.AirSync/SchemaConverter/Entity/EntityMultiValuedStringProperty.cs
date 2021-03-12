using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Exchange.AirSync.SchemaConverter.Common;
using Microsoft.Exchange.Entities.DataModel.Items;

namespace Microsoft.Exchange.AirSync.SchemaConverter.Entity
{
	// Token: 0x020001A4 RID: 420
	[Serializable]
	internal class EntityMultiValuedStringProperty : EntityProperty, IMultivaluedProperty<string>, IProperty, IEnumerable<string>, IEnumerable
	{
		// Token: 0x06001201 RID: 4609 RVA: 0x00061FA3 File Offset: 0x000601A3
		public EntityMultiValuedStringProperty(EntityPropertyDefinition propertyDefinition) : base(propertyDefinition, PropertyType.ReadWrite, false)
		{
		}

		// Token: 0x170006C6 RID: 1734
		// (get) Token: 0x06001202 RID: 4610 RVA: 0x00061FAE File Offset: 0x000601AE
		public int Count
		{
			get
			{
				if (this.values == null)
				{
					return 0;
				}
				return this.values.Count;
			}
		}

		// Token: 0x06001203 RID: 4611 RVA: 0x00061FC8 File Offset: 0x000601C8
		public override void Bind(IItem item)
		{
			base.Bind(item);
			if (base.EntityPropertyDefinition.Getter == null)
			{
				throw new ConversionException("Unable to retrieve data of type " + base.EntityPropertyDefinition.Type.FullName);
			}
			this.values = (List<string>)base.EntityPropertyDefinition.Getter(base.Item);
		}

		// Token: 0x06001204 RID: 4612 RVA: 0x0006202C File Offset: 0x0006022C
		public override void Unbind()
		{
			try
			{
				this.values = null;
			}
			finally
			{
				base.Unbind();
			}
		}

		// Token: 0x06001205 RID: 4613 RVA: 0x0006205C File Offset: 0x0006025C
		public IEnumerator<string> GetEnumerator()
		{
			if (this.values == null)
			{
				return null;
			}
			return this.values.GetEnumerator();
		}

		// Token: 0x06001206 RID: 4614 RVA: 0x00062073 File Offset: 0x00060273
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06001207 RID: 4615 RVA: 0x0006207C File Offset: 0x0006027C
		public override void CopyFrom(IProperty srcProperty)
		{
			if (base.EntityPropertyDefinition.Setter == null)
			{
				throw new ConversionException("Unable to set data of type " + base.EntityPropertyDefinition.Type.FullName);
			}
			IMultivaluedProperty<string> multivaluedProperty = srcProperty as IMultivaluedProperty<string>;
			if (multivaluedProperty != null)
			{
				List<string> list = new List<string>(multivaluedProperty.Count);
				list.AddRange(multivaluedProperty);
				base.EntityPropertyDefinition.Setter(base.Item, list);
			}
		}

		// Token: 0x04000B48 RID: 2888
		private IList<string> values;
	}
}
