using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000E86 RID: 3718
	internal class PropertyBag
	{
		// Token: 0x060060CB RID: 24779 RVA: 0x0012E539 File Offset: 0x0012C739
		public bool Contains(PropertyDefinition propertyDefinition)
		{
			ArgumentValidator.ThrowIfNull("propertyDefinition", propertyDefinition);
			return this.properties.ContainsKey(propertyDefinition);
		}

		// Token: 0x1700164D RID: 5709
		public object this[PropertyDefinition propertyDefinition]
		{
			get
			{
				ArgumentValidator.ThrowIfNull("propertyDefinition", propertyDefinition);
				return this.properties[propertyDefinition];
			}
			set
			{
				ArgumentValidator.ThrowIfNull("propertyDefinition", propertyDefinition);
				this.properties[propertyDefinition] = value;
			}
		}

		// Token: 0x060060CE RID: 24782 RVA: 0x0012E585 File Offset: 0x0012C785
		public bool TryGetValue(PropertyDefinition propertyDefinition, out object value)
		{
			value = null;
			return this.properties.TryGetValue(propertyDefinition, out value);
		}

		// Token: 0x060060CF RID: 24783 RVA: 0x0012E597 File Offset: 0x0012C797
		public PropertyDefinition[] GetProperties()
		{
			return this.properties.Keys.ToArray<PropertyDefinition>();
		}

		// Token: 0x04003480 RID: 13440
		private Dictionary<PropertyDefinition, object> properties = new Dictionary<PropertyDefinition, object>();
	}
}
