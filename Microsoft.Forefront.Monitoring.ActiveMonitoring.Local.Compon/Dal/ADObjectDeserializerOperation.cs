using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Hygiene.Data;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.Dal
{
	// Token: 0x02000059 RID: 89
	public class ADObjectDeserializerOperation : DeserializerOperation
	{
		// Token: 0x06000257 RID: 599 RVA: 0x0000F08C File Offset: 0x0000D28C
		protected override object DeserializedValue(Type parameterType, Type[] additionalTypes)
		{
			IConfigurable configurable = (IConfigurable)Activator.CreateInstance(parameterType);
			IEnumerable<PropertyDefinition> source = ADObjectDeserializerOperation.GetPropertyDefinitions(configurable).ToArray<PropertyDefinition>();
			using (IEnumerator<XElement> enumerator = base.DataObject.Elements().GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					XElement xElement = enumerator.Current;
					PropertyDefinition propertyDefinition = source.FirstOrDefault((PropertyDefinition pd) => string.Equals(pd.Name, xElement.Name.LocalName, StringComparison.OrdinalIgnoreCase));
					if (propertyDefinition != null)
					{
						DalHelper.SetPropertyValue(xElement.Value, propertyDefinition, configurable);
					}
				}
			}
			return configurable;
		}

		// Token: 0x06000258 RID: 600 RVA: 0x0000F134 File Offset: 0x0000D334
		private static IEnumerable<PropertyDefinition> GetPropertyDefinitions(IConfigurable iconfigObj)
		{
			ConfigurableObject configurableObject = iconfigObj as ConfigurableObject;
			if (configurableObject != null)
			{
				return configurableObject.ObjectSchema.AllProperties;
			}
			ConfigurablePropertyBag configurablePropertyBag = (ConfigurablePropertyBag)iconfigObj;
			return configurablePropertyBag.GetPropertyDefinitions(false);
		}
	}
}
