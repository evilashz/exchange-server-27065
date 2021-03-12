using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Configuration.MonadDataProvider
{
	// Token: 0x020001B7 RID: 439
	internal class SharingPolicyDomainCreator : ConfigurableObjectCreator
	{
		// Token: 0x06000FAC RID: 4012 RVA: 0x0002F66C File Offset: 0x0002D86C
		internal override IList<string> GetProperties(string fullName)
		{
			return new string[]
			{
				"Identity",
				"Name",
				"WhenChanged",
				"Enabled",
				"Domains",
				"ObjectId"
			};
		}
	}
}
