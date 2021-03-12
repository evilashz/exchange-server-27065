using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.Cluster
{
	// Token: 0x02000422 RID: 1058
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal class RemoveDagNetworkRequest
	{
		// Token: 0x17000F11 RID: 3857
		// (get) Token: 0x06002F7F RID: 12159 RVA: 0x000C37D8 File Offset: 0x000C19D8
		// (set) Token: 0x06002F80 RID: 12160 RVA: 0x000C37E0 File Offset: 0x000C19E0
		public string Name { get; set; }
	}
}
