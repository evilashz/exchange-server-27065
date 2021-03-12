using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000109 RID: 265
	public static class ServerResolverExtensions
	{
		// Token: 0x06001F8E RID: 8078 RVA: 0x0005F13C File Offset: 0x0005D33C
		public static ServerResolverRow ResolveServer(ADObjectId server)
		{
			List<ADObjectId> list = new List<ADObjectId>();
			list.Add(server);
			return ServerResolver.Instance.ResolveObjects(list).FirstOrDefault<ServerResolverRow>();
		}
	}
}
