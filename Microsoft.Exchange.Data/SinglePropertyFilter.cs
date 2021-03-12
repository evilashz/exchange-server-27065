using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Data
{
	// Token: 0x02000032 RID: 50
	[Serializable]
	internal abstract class SinglePropertyFilter : QueryFilter
	{
		// Token: 0x060001AA RID: 426 RVA: 0x00007396 File Offset: 0x00005596
		public SinglePropertyFilter(PropertyDefinition property)
		{
			if (property == null)
			{
				throw new ArgumentNullException("property");
			}
			this.property = property;
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x060001AB RID: 427 RVA: 0x000073B3 File Offset: 0x000055B3
		public PropertyDefinition Property
		{
			get
			{
				return this.property;
			}
		}

		// Token: 0x060001AC RID: 428 RVA: 0x000073BC File Offset: 0x000055BC
		internal override IEnumerable<PropertyDefinition> FilterProperties()
		{
			return new List<PropertyDefinition>(1)
			{
				this.Property
			};
		}

		// Token: 0x060001AD RID: 429 RVA: 0x000073E0 File Offset: 0x000055E0
		public override bool Equals(object obj)
		{
			SinglePropertyFilter singlePropertyFilter = obj as SinglePropertyFilter;
			return singlePropertyFilter != null && singlePropertyFilter.GetType() == base.GetType() && this.property.Equals(singlePropertyFilter.property);
		}

		// Token: 0x060001AE RID: 430
		public abstract SinglePropertyFilter CloneWithAnotherProperty(PropertyDefinition property);

		// Token: 0x060001AF RID: 431 RVA: 0x0000741D File Offset: 0x0000561D
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode() ^ this.property.GetHashCode();
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x00007436 File Offset: 0x00005636
		public override QueryFilter CloneWithPropertyReplacement(IDictionary<PropertyDefinition, PropertyDefinition> propertyMap)
		{
			return this.CloneWithAnotherProperty(propertyMap[this.Property]);
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x0000744C File Offset: 0x0000564C
		protected void CheckClonable(PropertyDefinition targetProperty)
		{
			if (targetProperty.Type == typeof(SmtpAddress))
			{
				return;
			}
			if (this.Property.Type == typeof(string))
			{
				targetProperty.Type == typeof(MultiValuedProperty<string>);
			}
		}

		// Token: 0x04000093 RID: 147
		private readonly PropertyDefinition property;
	}
}
