using System;
using System.Collections.Generic;
using System.Management.Automation;
using System.Reflection;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Configuration.MonadDataProvider
{
	// Token: 0x020001BE RID: 446
	internal class AcceptedDomainCreator : ConfigurableObjectCreator
	{
		// Token: 0x06000FBF RID: 4031 RVA: 0x0002FF0C File Offset: 0x0002E10C
		internal override IList<string> GetProperties(string fullName)
		{
			return new string[]
			{
				"Identity",
				"Name",
				"WhenChanged",
				"DomainType",
				"DomainName",
				"Default"
			};
		}

		// Token: 0x06000FC0 RID: 4032 RVA: 0x0002FF54 File Offset: 0x0002E154
		protected override void FillProperty(Type type, PSObject psObject, ConfigurableObject configObject, string propertyName)
		{
			if (propertyName == "DomainType")
			{
				PropertyInfo property = configObject.GetType().GetProperty(propertyName);
				property.SetValue(configObject, MockObjectCreator.GetSingleProperty(psObject.Members[propertyName].Value, property.PropertyType), null);
				return;
			}
			base.FillProperty(type, psObject, configObject, propertyName);
		}
	}
}
