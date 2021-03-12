using System;
using Microsoft.Exchange.Data.ApplicationLogic.Cafe;
using Microsoft.Exchange.HttpProxy.Routing.Providers;
using Microsoft.Exchange.HttpProxy.Routing.RoutingDestinations;
using Microsoft.Exchange.HttpProxy.Routing.RoutingEntries;
using Microsoft.Exchange.HttpProxy.Routing.RoutingKeys;

namespace Microsoft.Exchange.HttpProxy.Routing.RoutingLookups
{
	// Token: 0x02000036 RID: 54
	internal class DatabaseGuidRoutingLookup : IRoutingLookup
	{
		// Token: 0x060000E4 RID: 228 RVA: 0x00003EA1 File Offset: 0x000020A1
		public DatabaseGuidRoutingLookup(IDatabaseLocationProvider databaseLocationProvider)
		{
			if (databaseLocationProvider == null)
			{
				throw new ArgumentNullException("databaseLocationProvider");
			}
			this.databaseLocationProvider = databaseLocationProvider;
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x00003EC0 File Offset: 0x000020C0
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
			DatabaseGuidRoutingKey databaseGuidRoutingKey = routingKey as DatabaseGuidRoutingKey;
			if (databaseGuidRoutingKey == null)
			{
				string message = string.Format("Routing key type {0} is not supported", routingKey.GetType());
				throw new ArgumentException(message, "routingKey");
			}
			return this.GetDatabaseGuidRoutingEntry(databaseGuidRoutingKey, diagnostics);
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x00003F20 File Offset: 0x00002120
		public DatabaseGuidRoutingEntry GetDatabaseGuidRoutingEntry(DatabaseGuidRoutingKey databaseGuidRoutingKey, IRoutingDiagnostics diagnostics)
		{
			if (databaseGuidRoutingKey == null)
			{
				throw new ArgumentNullException("databaseGuidRoutingKey");
			}
			DatabaseGuidRoutingEntry result;
			try
			{
				BackEndServer backEndServerForDatabase = this.databaseLocationProvider.GetBackEndServerForDatabase(databaseGuidRoutingKey.DatabaseGuid, databaseGuidRoutingKey.DomainName, databaseGuidRoutingKey.ResourceForest, diagnostics);
				if (backEndServerForDatabase == null)
				{
					result = this.CreateFailedEntry(databaseGuidRoutingKey, "Could not find database");
				}
				else
				{
					result = new SuccessfulDatabaseGuidRoutingEntry(databaseGuidRoutingKey, new ServerRoutingDestination(backEndServerForDatabase.Fqdn, new int?(backEndServerForDatabase.Version)), 0L);
				}
			}
			catch (DatabaseLocationProviderException ex)
			{
				result = this.CreateFailedEntry(databaseGuidRoutingKey, ex.Message);
			}
			return result;
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x00003FB8 File Offset: 0x000021B8
		private FailedDatabaseGuidRoutingEntry CreateFailedEntry(DatabaseGuidRoutingKey databaseGuidRoutingKey, string message)
		{
			ErrorRoutingDestination destination = new ErrorRoutingDestination(message);
			return new FailedDatabaseGuidRoutingEntry(databaseGuidRoutingKey, destination, DateTime.UtcNow.ToFileTimeUtc());
		}

		// Token: 0x0400005F RID: 95
		private readonly IDatabaseLocationProvider databaseLocationProvider;
	}
}
