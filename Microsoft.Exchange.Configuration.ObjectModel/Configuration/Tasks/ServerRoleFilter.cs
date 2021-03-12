using System;
using System.Collections;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x02000190 RID: 400
	public class ServerRoleFilter<T> : IEnumerableFilter<T> where T : IConfigurable, new()
	{
		// Token: 0x06000E8C RID: 3724 RVA: 0x0002AFE9 File Offset: 0x000291E9
		private ServerRoleFilter(ServerRole serverRole)
		{
			this.serverRole = serverRole;
		}

		// Token: 0x06000E8D RID: 3725 RVA: 0x0002AFF8 File Offset: 0x000291F8
		public static ServerRoleFilter<T> GetServerRoleFilter(ServerRole serverRole)
		{
			ServerRoleFilter<T> result;
			lock (ServerRoleFilter<T>.syncRoot)
			{
				if (ServerRoleFilter<T>.instances == null)
				{
					ServerRoleFilter<T>.instances = new Hashtable();
				}
				if (ServerRoleFilter<T>.instances.Contains(serverRole))
				{
					result = (ServerRoleFilter<T>)ServerRoleFilter<T>.instances[serverRole];
				}
				else
				{
					ServerRoleFilter<T> serverRoleFilter = new ServerRoleFilter<T>(serverRole);
					ServerRoleFilter<T>.instances.Add(serverRole, serverRoleFilter);
					result = serverRoleFilter;
				}
			}
			return result;
		}

		// Token: 0x06000E8E RID: 3726 RVA: 0x0002B088 File Offset: 0x00029288
		public bool AcceptElement(T element)
		{
			if (element == null)
			{
				return false;
			}
			Server server = element as Server;
			return server != null && (server.CurrentServerRole & this.serverRole) > ServerRole.None;
		}

		// Token: 0x04000317 RID: 791
		private static Hashtable instances;

		// Token: 0x04000318 RID: 792
		private static object syncRoot = new object();

		// Token: 0x04000319 RID: 793
		private ServerRole serverRole;
	}
}
