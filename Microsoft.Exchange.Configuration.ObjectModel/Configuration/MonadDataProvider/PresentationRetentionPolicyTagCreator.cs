using System;
using System.Collections.Generic;
using System.Management.Automation;
using System.Reflection;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Configuration.MonadDataProvider
{
	// Token: 0x020001C5 RID: 453
	internal class PresentationRetentionPolicyTagCreator : ConfigurableObjectCreator
	{
		// Token: 0x06000FD1 RID: 4049 RVA: 0x00030658 File Offset: 0x0002E858
		internal override IList<string> GetProperties(string fullName)
		{
			return new string[]
			{
				"Identity",
				"Name",
				"WhenChanged",
				"Type",
				"Comment",
				"AgeLimitForRetention",
				"RetentionEnabled",
				"RetentionAction"
			};
		}

		// Token: 0x06000FD2 RID: 4050 RVA: 0x000306B0 File Offset: 0x0002E8B0
		protected override void FillProperty(Type type, PSObject psObject, ConfigurableObject configObject, string propertyName)
		{
			if (propertyName == "RetentionEnabled" || propertyName == "AgeLimitForRetention" || propertyName == "RetentionAction")
			{
				PropertyInfo property = configObject.GetType().GetProperty(propertyName);
				property.SetValue(configObject, MockObjectCreator.GetSingleProperty(psObject.Members[propertyName].Value, property.PropertyType), null);
				return;
			}
			base.FillProperty(type, psObject, configObject, propertyName);
		}
	}
}
