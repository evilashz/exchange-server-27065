using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Configuration.MonadDataProvider
{
	// Token: 0x020001AB RID: 427
	internal class UserCreator : ConfigurableObjectCreator
	{
		// Token: 0x06000F8D RID: 3981 RVA: 0x0002E6E4 File Offset: 0x0002C8E4
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
				"Manager",
				"UserPrincipalName",
				"SamAccountName",
				"ResetPasswordOnNextLogon"
			};
		}
	}
}
