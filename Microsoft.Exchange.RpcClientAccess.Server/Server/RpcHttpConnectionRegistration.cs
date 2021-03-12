using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Security.Authorization;

namespace Microsoft.Exchange.RpcClientAccess.Server
{
	// Token: 0x0200003D RID: 61
	internal class RpcHttpConnectionRegistration
	{
		// Token: 0x06000225 RID: 549 RVA: 0x0000C300 File Offset: 0x0000A500
		internal RpcHttpConnectionRegistration()
		{
			this.connectionRegistrationCache = new Dictionary<Guid, RpcHttpConnectionRegistrationCacheEntry>();
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x06000226 RID: 550 RVA: 0x0000C320 File Offset: 0x0000A520
		public int CacheSize
		{
			get
			{
				int count;
				lock (this.cacheLock)
				{
					count = this.connectionRegistrationCache.Count;
				}
				return count;
			}
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x06000227 RID: 551 RVA: 0x0000C368 File Offset: 0x0000A568
		internal static RpcHttpConnectionRegistration Instance
		{
			get
			{
				return RpcHttpConnectionRegistration.instance;
			}
		}

		// Token: 0x06000228 RID: 552 RVA: 0x0000C370 File Offset: 0x0000A570
		public void Register(Guid associationGroupId, ClientSecurityContext clientSecurityContext, string authIdentifier, string serverTarget, string sessionCookie, string clientIp, Guid requestId)
		{
			lock (this.cacheLock)
			{
				RpcHttpConnectionRegistrationCacheEntry rpcHttpConnectionRegistrationCacheEntry = null;
				if (!this.connectionRegistrationCache.TryGetValue(associationGroupId, out rpcHttpConnectionRegistrationCacheEntry))
				{
					rpcHttpConnectionRegistrationCacheEntry = new RpcHttpConnectionRegistrationCacheEntry(associationGroupId, clientSecurityContext, authIdentifier, serverTarget, sessionCookie, clientIp);
					this.connectionRegistrationCache.Add(associationGroupId, rpcHttpConnectionRegistrationCacheEntry);
				}
				rpcHttpConnectionRegistrationCacheEntry.AddRequest(requestId, clientSecurityContext);
			}
		}

		// Token: 0x06000229 RID: 553 RVA: 0x0000C3E4 File Offset: 0x0000A5E4
		public bool TryRegisterAdditionalConnection(Guid associationGroupId, string authIdentifier, Guid requestId)
		{
			bool flag = false;
			string arg = string.Empty;
			lock (this.cacheLock)
			{
				RpcHttpConnectionRegistrationCacheEntry rpcHttpConnectionRegistrationCacheEntry = null;
				if (this.connectionRegistrationCache.TryGetValue(associationGroupId, out rpcHttpConnectionRegistrationCacheEntry))
				{
					if (rpcHttpConnectionRegistrationCacheEntry.AuthIdentifier.Equals(authIdentifier, StringComparison.InvariantCultureIgnoreCase))
					{
						rpcHttpConnectionRegistrationCacheEntry.AddRequest(requestId);
						return true;
					}
					arg = rpcHttpConnectionRegistrationCacheEntry.AuthIdentifier;
					flag = true;
				}
			}
			if (flag)
			{
				throw new ConnectionRegistrationException(string.Format("Association GUID {0} cannot be shared between auth identifiers {1} and {2}", associationGroupId.ToString(), arg, authIdentifier));
			}
			return false;
		}

		// Token: 0x0600022A RID: 554 RVA: 0x0000C484 File Offset: 0x0000A684
		public void Unregister(Guid associationGroupId, Guid requestId)
		{
			DateTime utcNow = DateTime.UtcNow;
			lock (this.cacheLock)
			{
				RpcHttpConnectionRegistrationCacheEntry rpcHttpConnectionRegistrationCacheEntry = null;
				if (!this.connectionRegistrationCache.TryGetValue(associationGroupId, out rpcHttpConnectionRegistrationCacheEntry))
				{
					string message = string.Format("Association Group ID '{0}' does not exist in RpcHttpConnectionRegistration cache.", associationGroupId);
					throw new NotFoundException(message);
				}
				rpcHttpConnectionRegistrationCacheEntry.RemoveRequest(requestId);
				if (rpcHttpConnectionRegistrationCacheEntry.RequestIds.Count == 0)
				{
					this.connectionRegistrationCache.Remove(associationGroupId);
					rpcHttpConnectionRegistrationCacheEntry.Dispose();
				}
			}
		}

		// Token: 0x0600022B RID: 555 RVA: 0x0000C518 File Offset: 0x0000A718
		public void Clear()
		{
			Dictionary<Guid, RpcHttpConnectionRegistrationCacheEntry> dictionary = new Dictionary<Guid, RpcHttpConnectionRegistrationCacheEntry>();
			Dictionary<Guid, RpcHttpConnectionRegistrationCacheEntry> dictionary2;
			lock (this.cacheLock)
			{
				dictionary2 = this.connectionRegistrationCache;
				this.connectionRegistrationCache = dictionary;
			}
			foreach (RpcHttpConnectionRegistrationCacheEntry rpcHttpConnectionRegistrationCacheEntry in dictionary2.Values)
			{
				rpcHttpConnectionRegistrationCacheEntry.Dispose();
			}
			dictionary2.Clear();
		}

		// Token: 0x0600022C RID: 556 RVA: 0x0000C5C0 File Offset: 0x0000A7C0
		public bool TryGet(Guid associationGroupId, out ClientSecurityContext clientSecurityContext, out RpcHttpConnectionProperties connectionProperties)
		{
			DateTime utcNow = DateTime.UtcNow;
			clientSecurityContext = null;
			connectionProperties = null;
			bool result;
			lock (this.cacheLock)
			{
				RpcHttpConnectionRegistrationCacheEntry rpcHttpConnectionRegistrationCacheEntry = null;
				if (this.connectionRegistrationCache.TryGetValue(associationGroupId, out rpcHttpConnectionRegistrationCacheEntry))
				{
					string[] requestIds = (from requestId in rpcHttpConnectionRegistrationCacheEntry.RequestIds
					select requestId.ToString()).ToArray<string>();
					connectionProperties = new RpcHttpConnectionProperties(rpcHttpConnectionRegistrationCacheEntry.ClientIp, rpcHttpConnectionRegistrationCacheEntry.ServerTarget, rpcHttpConnectionRegistrationCacheEntry.SessionCookie, requestIds);
					clientSecurityContext = rpcHttpConnectionRegistrationCacheEntry.GetClientSecurityContext();
					result = true;
				}
				else
				{
					result = false;
				}
			}
			return result;
		}

		// Token: 0x04000117 RID: 279
		private static readonly RpcHttpConnectionRegistration instance = new RpcHttpConnectionRegistration();

		// Token: 0x04000118 RID: 280
		private readonly object cacheLock = new object();

		// Token: 0x04000119 RID: 281
		private Dictionary<Guid, RpcHttpConnectionRegistrationCacheEntry> connectionRegistrationCache;
	}
}
