using System;
using System.Linq;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Services.OData.Model;
using Microsoft.OData.Core;

namespace Microsoft.Exchange.Services.OData.Web
{
	// Token: 0x02000E07 RID: 3591
	internal static class ODataObjectModelConverter
	{
		// Token: 0x06005D20 RID: 23840 RVA: 0x00122968 File Offset: 0x00120B68
		public static ODataEntry ConvertToODataEntry(Entity element, Uri entryUri)
		{
			ArgumentValidator.ThrowIfNull("element", element);
			string etag = null;
			object arg = null;
			if (element.PropertyBag.TryGetValue(ItemSchema.ChangeKey, out arg))
			{
				etag = string.Format("W/\"{0}\"", arg);
			}
			ODataEntry odataEntry = new ODataEntry();
			odataEntry.EditLink = entryUri;
			odataEntry.ReadLink = entryUri;
			odataEntry.Id = entryUri;
			odataEntry.ETag = etag;
			odataEntry.TypeName = element.GetType().FullName;
			odataEntry.Properties = from p in element.PropertyBag.GetProperties()
			where !p.Flags.HasFlag(PropertyDefinitionFlags.Navigation)
			select ODataObjectModelConverter.ConvertToODataProperty(p, element[p]);
			return odataEntry;
		}

		// Token: 0x06005D21 RID: 23841 RVA: 0x00122A48 File Offset: 0x00120C48
		public static ODataProperty ConvertToODataProperty(PropertyDefinition propertyDefinition, object clrValue)
		{
			ArgumentValidator.ThrowIfNull("propertyDefinition", propertyDefinition);
			string name = propertyDefinition.Name;
			Type type = propertyDefinition.Type;
			object obj = clrValue;
			if (propertyDefinition.ODataPropertyValueConverter != null)
			{
				obj = propertyDefinition.ODataPropertyValueConverter.ToODataPropertyValue(clrValue);
			}
			if (type.IsEnum && obj != null)
			{
				obj = EnumConverter.ToODataEnumValue((Enum)obj);
			}
			return new ODataProperty
			{
				Name = name,
				Value = obj
			};
		}

		// Token: 0x06005D22 RID: 23842 RVA: 0x00122AB4 File Offset: 0x00120CB4
		public static object ConvertFromPropertyValue(PropertyDefinition entityProperty, object odataPropertyValue)
		{
			ArgumentValidator.ThrowIfNull("entityProperty", entityProperty);
			Type type = entityProperty.Type;
			object result = odataPropertyValue;
			if (entityProperty.ODataPropertyValueConverter != null)
			{
				result = entityProperty.ODataPropertyValueConverter.FromODataPropertyValue(odataPropertyValue);
			}
			if (type.IsEnum)
			{
				result = EnumConverter.FromODataEnumValue(type, (ODataEnumValue)odataPropertyValue);
			}
			return result;
		}
	}
}
