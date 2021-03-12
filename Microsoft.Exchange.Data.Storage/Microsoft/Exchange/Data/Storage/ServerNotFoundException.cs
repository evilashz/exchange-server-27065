using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000D5C RID: 3420
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal class ServerNotFoundException : ServiceDiscoveryPermanentException
	{
		// Token: 0x06007658 RID: 30296 RVA: 0x0020A778 File Offset: 0x00208978
		public ServerNotFoundException(string message, string serverName) : this(message, serverName, null)
		{
		}

		// Token: 0x06007659 RID: 30297 RVA: 0x0020A783 File Offset: 0x00208983
		public ServerNotFoundException(string message) : this(message, null, null)
		{
		}

		// Token: 0x0600765A RID: 30298 RVA: 0x0020A78E File Offset: 0x0020898E
		public ServerNotFoundException(string message, string serverName, Exception innerException) : base(message, innerException)
		{
			this.ServerName = serverName;
		}

		// Token: 0x0600765B RID: 30299 RVA: 0x0020A79F File Offset: 0x0020899F
		public ServerNotFoundException(string message, Exception innerException) : this(message, null, innerException)
		{
		}

		// Token: 0x040051EE RID: 20974
		public readonly string ServerName;
	}
}
