using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x02000147 RID: 327
	[Serializable]
	internal class ServerInfo
	{
		// Token: 0x040002AC RID: 684
		public ADObjectId Identity;

		// Token: 0x040002AD RID: 685
		public ServerRole Role;
	}
}
