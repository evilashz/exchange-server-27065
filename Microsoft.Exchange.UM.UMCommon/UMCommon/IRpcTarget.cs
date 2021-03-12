using System;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x02000033 RID: 51
	internal interface IRpcTarget
	{
		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x060002B5 RID: 693
		string Name { get; }

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x060002B6 RID: 694
		ADConfigurationObject ConfigObject { get; }
	}
}
