using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Configuration.MonadDataProvider
{
	// Token: 0x020001A9 RID: 425
	internal class MailboxAcePresentationObjectCreator : ConfigurableObjectCreator
	{
		// Token: 0x06000F86 RID: 3974 RVA: 0x0002E4CC File Offset: 0x0002C6CC
		internal override IList<string> GetProperties(string fullName)
		{
			return MailboxAcePresentationObjectCreator.mbxAcePresentationObjProperties;
		}

		// Token: 0x06000F87 RID: 3975 RVA: 0x0002E4F0 File Offset: 0x0002C6F0
		protected override void FillProperty(Type type, PSObject psObject, ConfigurableObject configObject, string propertyName)
		{
			if (propertyName == "Identity")
			{
				IEnumerable<PropertyDefinition> source = from c in configObject.ObjectSchema.AllProperties
				where c.Name == propertyName
				select c;
				if (source.Count<PropertyDefinition>() == 1)
				{
					PropertyDefinition propertyDefinition = source.First<PropertyDefinition>();
					object value = psObject.Members[propertyName].Value;
					configObject.propertyBag[propertyDefinition] = MockObjectCreator.GetPropertyBasedOnDefinition(propertyDefinition, value);
					return;
				}
			}
			else
			{
				base.FillProperty(type, psObject, configObject, propertyName);
			}
		}

		// Token: 0x0400034C RID: 844
		private static readonly string[] mbxAcePresentationObjProperties = new string[]
		{
			"AccessRights",
			"Deny",
			"Identity",
			"InheritanceType",
			"IsInherited",
			"RealAce",
			"User"
		};
	}
}
