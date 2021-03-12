using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Configuration.MonadDataProvider
{
	// Token: 0x020001B5 RID: 437
	internal class ContactCreator : ConfigurableObjectCreator
	{
		// Token: 0x06000FA8 RID: 4008 RVA: 0x0002F538 File Offset: 0x0002D738
		internal override IList<string> GetProperties(string fullName)
		{
			return new string[]
			{
				"Identity",
				"FirstName",
				"LastName",
				"Initials",
				"Name",
				"SimpleDisplayName",
				"WebPage",
				"Notes",
				"StreetAddress",
				"City",
				"StateOrProvince",
				"PostalCode",
				"CountryOrRegion",
				"Phone",
				"Pager",
				"Fax",
				"HomePhone",
				"MobilePhone",
				"Title",
				"Company",
				"Department",
				"Office",
				"Manager"
			};
		}
	}
}
