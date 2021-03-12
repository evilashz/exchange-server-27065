using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Configuration.MonadDataProvider
{
	// Token: 0x020001B1 RID: 433
	internal class WindowsGroupCreator : ConfigurableObjectCreator
	{
		// Token: 0x06000F9D RID: 3997 RVA: 0x0002EF60 File Offset: 0x0002D160
		internal override IList<string> GetProperties(string fullName)
		{
			return new string[]
			{
				"Identity",
				"Notes",
				"Members"
			};
		}
	}
}
