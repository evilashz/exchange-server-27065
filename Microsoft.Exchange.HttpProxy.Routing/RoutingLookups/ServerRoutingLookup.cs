using System;
using Microsoft.Exchange.HttpProxy.Common;
using Microsoft.Exchange.HttpProxy.Routing.RoutingDestinations;
using Microsoft.Exchange.HttpProxy.Routing.RoutingEntries;
using Microsoft.Exchange.HttpProxy.Routing.RoutingKeys;

namespace Microsoft.Exchange.HttpProxy.Routing.RoutingLookups
{
	// Token: 0x0200003E RID: 62
	internal class ServerRoutingLookup : IRoutingLookup, IServerVersionLookup
	{
		// Token: 0x060000F8 RID: 248 RVA: 0x00004217 File Offset: 0x00002417
		public ServerRoutingLookup()
		{
			this.versionLookup = this;
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x00004226 File Offset: 0x00002426
		public ServerRoutingLookup(IServerVersionLookup versionLookup)
		{
			this.versionLookup = versionLookup;
		}

		// Token: 0x060000FA RID: 250 RVA: 0x00004238 File Offset: 0x00002438
		IRoutingEntry IRoutingLookup.GetRoutingEntry(IRoutingKey routingKey, IRoutingDiagnostics diagnostics)
		{
			if (routingKey == null)
			{
				throw new ArgumentNullException("routingKey");
			}
			if (diagnostics == null)
			{
				throw new ArgumentNullException("diagnostics");
			}
			ServerRoutingKey serverRoutingKey = routingKey as ServerRoutingKey;
			if (serverRoutingKey == null)
			{
				string message = string.Format("Routing key type {0} is not supported", routingKey.GetType());
				throw new ArgumentException(message, "routingKey");
			}
			if (!string.IsNullOrEmpty(serverRoutingKey.Server))
			{
				int? version = null;
				if (serverRoutingKey.Version != null)
				{
					version = serverRoutingKey.Version;
				}
				else
				{
					version = this.versionLookup.LookupVersion(serverRoutingKey.Server);
				}
				return new SuccessfulServerRoutingEntry(serverRoutingKey, new ServerRoutingDestination(serverRoutingKey.Server, version), DateTime.UtcNow.ToFileTimeUtc());
			}
			ErrorRoutingDestination destination = new ErrorRoutingDestination("Could not extract server from ServerRoutingKey");
			return new FailedServerRoutingEntry(serverRoutingKey, destination, DateTime.UtcNow.ToFileTimeUtc());
		}

		// Token: 0x060000FB RID: 251 RVA: 0x00004310 File Offset: 0x00002510
		int? IServerVersionLookup.LookupVersion(string server)
		{
			return ServerLookup.LookupVersion(server);
		}

		// Token: 0x04000061 RID: 97
		private IServerVersionLookup versionLookup;
	}
}
