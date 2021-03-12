using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Configuration.MonadDataProvider
{
	// Token: 0x020001B6 RID: 438
	internal class SharingPolicyCreator : ConfigurableObjectCreator
	{
		// Token: 0x06000FAA RID: 4010 RVA: 0x0002F61C File Offset: 0x0002D81C
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
