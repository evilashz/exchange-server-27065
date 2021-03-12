using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Configuration.MonadDataProvider
{
	// Token: 0x020001A7 RID: 423
	internal abstract class ConfigurableObjectCreator : MockObjectCreator
	{
		// Token: 0x06000F80 RID: 3968 RVA: 0x0002DF3C File Offset: 0x0002C13C
		protected override void FillProperties(Type type, PSObject psObject, object dummyObject, IList<string> properties)
		{
			ConfigurableObject configurableObject = dummyObject as ConfigurableObject;
			if (configurableObject == null)
			{
				throw new ArgumentException("This method only support ConfigurableObject; please override this method if not this type!");
			}
			if (psObject.Members["ExchangeVersion"] != null)
			{
				PropertyBag propertyBag = configurableObject.propertyBag;
				ExchangeObjectVersion exchangeObjectVersion = MockObjectCreator.GetPropertyBasedOnDefinition(propertyBag.ObjectVersionPropertyDefinition, psObject.Members["ExchangeVersion"].Value) as ExchangeObjectVersion;
				if (exchangeObjectVersion != null)
				{
					configurableObject.SetExchangeVersion(exchangeObjectVersion);
				}
			}
			foreach (PSMemberInfo psmemberInfo in psObject.Members)
			{
				if (properties.Contains(psmemberInfo.Name))
				{
					this.FillProperty(type, psObject, configurableObject, psmemberInfo.Name);
				}
			}
			configurableObject.ResetChangeTracking();
		}

		// Token: 0x06000F81 RID: 3969 RVA: 0x0002E02C File Offset: 0x0002C22C
		protected virtual void FillProperty(Type type, PSObject psObject, ConfigurableObject configObject, string propertyName)
		{
			if (propertyName == "Identity")
			{
				configObject.propertyBag[ADObjectSchema.Id] = MockObjectCreator.GetSingleProperty(psObject.Members[propertyName].Value, ADObjectSchema.Id.Type);
				return;
			}
			if (propertyName == "WhenChanged")
			{
				if (psObject.Members[propertyName].Value is DateTime)
				{
					configObject.propertyBag.DangerousSetValue(ADObjectSchema.WhenChangedRaw, ((DateTime)psObject.Members[propertyName].Value).ToString("yyyyMMddHHmmss'.0Z'", CultureInfo.InvariantCulture));
					return;
				}
			}
			else
			{
				IEnumerable<PropertyDefinition> source = from c in configObject.ObjectSchema.AllProperties
				where c.Name == propertyName
				select c;
				if (source.Count<PropertyDefinition>() == 1)
				{
					PropertyDefinition propertyDefinition = source.First<PropertyDefinition>();
					object value = psObject.Members[propertyName].Value;
					if (value != null)
					{
						object propertyBasedOnDefinition = MockObjectCreator.GetPropertyBasedOnDefinition(propertyDefinition, value);
						configObject.propertyBag[propertyDefinition] = propertyBasedOnDefinition;
					}
				}
			}
		}
	}
}
