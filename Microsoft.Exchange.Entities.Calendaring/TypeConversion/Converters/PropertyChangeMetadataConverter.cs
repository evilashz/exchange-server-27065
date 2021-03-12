using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Entities.DataModel.PropertyBags;
using Microsoft.Exchange.Entities.TypeConversion.Converters;

namespace Microsoft.Exchange.Entities.Calendaring.TypeConversion.Converters
{
	// Token: 0x02000081 RID: 129
	internal class PropertyChangeMetadataConverter : IConverter<PropertyChangeMetadata, IList<string>>
	{
		// Token: 0x0600032E RID: 814 RVA: 0x0000B8D5 File Offset: 0x00009AD5
		public PropertyChangeMetadataConverter(IConverter<PropertyChangeMetadata.PropertyGroup, IEnumerable<Microsoft.Exchange.Entities.DataModel.PropertyBags.PropertyDefinition>> propertyMapping)
		{
			this.propertyMapping = propertyMapping;
		}

		// Token: 0x0600032F RID: 815 RVA: 0x0000B908 File Offset: 0x00009B08
		public IList<string> Convert(PropertyChangeMetadata value)
		{
			if (value == null)
			{
				throw new ExArgumentNullException("value");
			}
			IEnumerable<string> source = from overriddenGroup in this.GetOverriddenGroups(value)
			select this.propertyMapping.Convert(overriddenGroup) into properties
			where properties != null
			from property in properties
			select property.Name;
			return source.ToList<string>();
		}

		// Token: 0x06000330 RID: 816 RVA: 0x0000B9A3 File Offset: 0x00009BA3
		protected virtual IEnumerable<PropertyChangeMetadata.PropertyGroup> GetOverriddenGroups(PropertyChangeMetadata value)
		{
			return value.GetOverriddenGroups();
		}

		// Token: 0x040000E3 RID: 227
		private readonly IConverter<PropertyChangeMetadata.PropertyGroup, IEnumerable<Microsoft.Exchange.Entities.DataModel.PropertyBags.PropertyDefinition>> propertyMapping;
	}
}
