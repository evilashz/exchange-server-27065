using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.Hybrid
{
	// Token: 0x020008E5 RID: 2277
	internal interface IExchangeServer
	{
		// Token: 0x1700181D RID: 6173
		// (get) Token: 0x060050C9 RID: 20681
		ADObjectId Identity { get; }

		// Token: 0x1700181E RID: 6174
		// (get) Token: 0x060050CA RID: 20682
		string Name { get; }

		// Token: 0x1700181F RID: 6175
		// (get) Token: 0x060050CB RID: 20683
		ServerRole ServerRole { get; }

		// Token: 0x17001820 RID: 6176
		// (get) Token: 0x060050CC RID: 20684
		ServerVersion AdminDisplayVersion { get; }
	}
}
