using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Configuration.MonadDataProvider
{
	// Token: 0x020001BD RID: 445
	internal class DomainContentConfigCreator : ConfigurableObjectCreator
	{
		// Token: 0x06000FBC RID: 4028 RVA: 0x0002FE1C File Offset: 0x0002E01C
		internal override IList<string> GetProperties(string fullName)
		{
			return new string[]
			{
				"Identity",
				"Name",
				"WhenChanged",
				"AllowedOOFType",
				"DomainName",
				"AutoReplyEnabled",
				"AutoForwardEnabled",
				"DeliveryReportEnabled",
				"NDREnabled",
				"DisplaySenderName",
				"TNEFEnabled",
				"LineWrapSize",
				"CharacterSet",
				"NonMimeCharacterSet",
				"TargetDeliveryDomain"
			};
		}

		// Token: 0x06000FBD RID: 4029 RVA: 0x0002FEB0 File Offset: 0x0002E0B0
		protected override void FillProperty(Type type, PSObject psObject, ConfigurableObject configObject, string propertyName)
		{
			if (propertyName == "DisplaySenderName")
			{
				configObject.propertyBag[DomainContentConfigSchema.DisplaySenderName] = MockObjectCreator.GetPropertyBasedOnDefinition(DomainContentConfigSchema.DisplaySenderName, psObject.Members[propertyName].Value);
			}
			base.FillProperty(type, psObject, configObject, propertyName);
		}
	}
}
