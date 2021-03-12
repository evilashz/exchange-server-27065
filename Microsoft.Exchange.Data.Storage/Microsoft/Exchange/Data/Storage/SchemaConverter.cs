using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000CC1 RID: 3265
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal abstract class SchemaConverter
	{
		// Token: 0x06007178 RID: 29048 RVA: 0x001F7490 File Offset: 0x001F5690
		internal SchemaConverter.Getter GetGetter(PropertyDefinition source)
		{
			KeyValuePair<SchemaConverter.Getter, SchemaConverter.Setter> keyValuePair;
			if (!this.mapping.TryGetValue(source, out keyValuePair))
			{
				return null;
			}
			return keyValuePair.Key;
		}

		// Token: 0x06007179 RID: 29049 RVA: 0x001F74B8 File Offset: 0x001F56B8
		internal SchemaConverter.Setter GetSetter(PropertyDefinition source)
		{
			KeyValuePair<SchemaConverter.Getter, SchemaConverter.Setter> keyValuePair;
			if (!this.mapping.TryGetValue(source, out keyValuePair))
			{
				return null;
			}
			return keyValuePair.Value;
		}

		// Token: 0x0600717A RID: 29050 RVA: 0x001F7540 File Offset: 0x001F5740
		protected void Add(PropertyDefinition source, PropertyDefinition destination)
		{
			this.Add(source, (IReadOnlyPropertyBag propertyBag) => propertyBag.GetProperties(new PropertyDefinition[]
			{
				destination
			})[0], delegate(IPropertyBag propertyBag, object value)
			{
				propertyBag.SetProperties(new PropertyDefinition[]
				{
					destination
				}, new object[]
				{
					value
				});
			});
		}

		// Token: 0x0600717B RID: 29051 RVA: 0x001F7579 File Offset: 0x001F5779
		protected void Add(PropertyDefinition source, SchemaConverter.Getter getter, SchemaConverter.Setter setter)
		{
			this.mapping.Add(source, new KeyValuePair<SchemaConverter.Getter, SchemaConverter.Setter>(getter, setter));
		}

		// Token: 0x04004EB9 RID: 20153
		private readonly Dictionary<PropertyDefinition, KeyValuePair<SchemaConverter.Getter, SchemaConverter.Setter>> mapping = new Dictionary<PropertyDefinition, KeyValuePair<SchemaConverter.Getter, SchemaConverter.Setter>>();

		// Token: 0x02000CC2 RID: 3266
		// (Invoke) Token: 0x0600717E RID: 29054
		public delegate object Getter(IReadOnlyPropertyBag propertyBag);

		// Token: 0x02000CC3 RID: 3267
		// (Invoke) Token: 0x06007182 RID: 29058
		public delegate void Setter(IPropertyBag propertyBag, object value);
	}
}
