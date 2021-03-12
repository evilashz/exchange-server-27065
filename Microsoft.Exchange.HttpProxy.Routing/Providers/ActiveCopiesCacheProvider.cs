using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using Microsoft.Exchange.Data.ApplicationLogic.Cafe;
using Microsoft.Exchange.Data.Storage.ServerLocator;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.Net;
using www.outlook.com.highavailability.ServerLocator.v1;

namespace Microsoft.Exchange.HttpProxy.Routing.Providers
{
	// Token: 0x0200000B RID: 11
	internal class ActiveCopiesCacheProvider : IDatabaseLocationProvider, IDisposable
	{
		// Token: 0x0600001D RID: 29 RVA: 0x000020F4 File Offset: 0x000002F4
		public ActiveCopiesCacheProvider()
		{
			this.activeCopiesList = new List<DatabaseServerInformation>();
			this.serverLocator = ServerLocatorServiceClient.Create("localhost");
			this.backgroundRefresh = new Timer((double)ActiveCopiesCacheProvider.DataRefreshIntervalInMilliseconds.Value);
			this.backgroundRefresh.Elapsed += this.OnBackgroundRefresh;
			this.backgroundRefresh.Enabled = true;
			this.Synchronize();
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000021FC File Offset: 0x000003FC
		public void Synchronize()
		{
			ExWatson.SendReportOnUnhandledException(delegate()
			{
				lock (ActiveCopiesCacheProvider.synchronizeLock)
				{
					try
					{
						if (!this.serverLocator.IsUsable)
						{
							this.serverLocator.Dispose();
							this.serverLocator = ServerLocatorServiceClient.Create("localhost");
						}
						GetActiveCopiesForDatabaseAvailabilityGroupParameters getActiveCopiesForDatabaseAvailabilityGroupParameters = new GetActiveCopiesForDatabaseAvailabilityGroupParameters();
						getActiveCopiesForDatabaseAvailabilityGroupParameters.CachedData = false;
						this.activeCopiesList = new List<DatabaseServerInformation>(this.serverLocator.GetActiveCopiesForDatabaseAvailabilityGroupExtended(getActiveCopiesForDatabaseAvailabilityGroupParameters));
					}
					catch (ServerLocatorClientTransientException)
					{
					}
				}
			});
		}

		// Token: 0x0600001F RID: 31 RVA: 0x0000222C File Offset: 0x0000042C
		public BackEndServer GetBackEndServerForDatabase(Guid databaseGuid, string domainName, string resourceForest, IRoutingDiagnostics diagnostics)
		{
			DatabaseServerInformation databaseServerInformation = this.activeCopiesList.FirstOrDefault((DatabaseServerInformation info) => info.DatabaseGuid == databaseGuid);
			if (databaseServerInformation != null)
			{
				return new BackEndServer(databaseServerInformation.ServerFqdn, databaseServerInformation.ServerVersion);
			}
			return null;
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002274 File Offset: 0x00000474
		public void Dispose()
		{
			if (this.serverLocator != null)
			{
				this.serverLocator.Dispose();
			}
			if (this.backgroundRefresh != null)
			{
				this.backgroundRefresh.Dispose();
			}
		}

		// Token: 0x06000021 RID: 33 RVA: 0x0000229C File Offset: 0x0000049C
		private void OnBackgroundRefresh(object source, ElapsedEventArgs args)
		{
			this.Synchronize();
		}

		// Token: 0x04000002 RID: 2
		internal static readonly IntAppSettingsEntry DataRefreshIntervalInMilliseconds = new IntAppSettingsEntry("RoutingUpdateModule.DataRefreshIntervalInMilliseconds", 15000, ExTraceGlobals.CommonAlgorithmTracer);

		// Token: 0x04000003 RID: 3
		private static object synchronizeLock = new object();

		// Token: 0x04000004 RID: 4
		private Timer backgroundRefresh;

		// Token: 0x04000005 RID: 5
		private ServerLocatorServiceClient serverLocator;

		// Token: 0x04000006 RID: 6
		private List<DatabaseServerInformation> activeCopiesList;
	}
}
