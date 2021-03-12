using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Mapi;

namespace Microsoft.Exchange.Configuration.MonadDataProvider
{
	// Token: 0x020001AA RID: 426
	internal class MailboxStatisticsCreator : ConfigurableObjectCreator
	{
		// Token: 0x06000F8A RID: 3978 RVA: 0x0002E5F0 File Offset: 0x0002C7F0
		internal override IList<string> GetProperties(string fullName)
		{
			return new string[]
			{
				"LastLoggedOnUserAccount",
				"ItemCount",
				"TotalItemSize"
			};
		}

		// Token: 0x06000F8B RID: 3979 RVA: 0x0002E620 File Offset: 0x0002C820
		protected override void FillProperty(Type type, PSObject psObject, ConfigurableObject configObject, string propertyName)
		{
			if (propertyName == "ItemCount")
			{
				configObject.propertyBag.DangerousSetValue(MapiPropertyDefinitions.ItemCount, psObject.Members[propertyName].Value);
				return;
			}
			if (propertyName == "TotalItemSize")
			{
				configObject.propertyBag.DangerousSetValue(MapiPropertyDefinitions.TotalItemSize, MockObjectCreator.GetSingleProperty(psObject.Members[propertyName].Value, MapiPropertyDefinitions.TotalItemSize.Type));
				return;
			}
			if (propertyName == "LastLoggedOnUserAccount")
			{
				configObject.propertyBag.DangerousSetValue(MapiPropertyDefinitions.LastLoggedOnUserAccount, psObject.Members[propertyName].Value);
				return;
			}
			base.FillProperty(type, psObject, configObject, propertyName);
		}
	}
}
