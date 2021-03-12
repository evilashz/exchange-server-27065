using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x0200010D RID: 269
	internal class DatacenterSiteBasedServerPicker : ServerPickerBase<InternalExchangeServer, ADObjectId>
	{
		// Token: 0x06000797 RID: 1943 RVA: 0x0001ED04 File Offset: 0x0001CF04
		public DatacenterSiteBasedServerPicker() : base(DatacenterSiteBasedServerPicker.UMDefaultRetryInterval, DatacenterSiteBasedServerPicker.UMDefaultRefreshInterval, DatacenterSiteBasedServerPicker.UMDefaultRefreshIntervalOnFailure, ExTraceGlobals.UtilTracer)
		{
		}

		// Token: 0x170001FA RID: 506
		// (get) Token: 0x06000798 RID: 1944 RVA: 0x0001ED20 File Offset: 0x0001CF20
		public static DatacenterSiteBasedServerPicker Instance
		{
			get
			{
				if (DatacenterSiteBasedServerPicker.staticInstance == null)
				{
					lock (DatacenterSiteBasedServerPicker.lockObj)
					{
						if (DatacenterSiteBasedServerPicker.staticInstance == null)
						{
							DatacenterSiteBasedServerPicker.staticInstance = new DatacenterSiteBasedServerPicker();
						}
					}
				}
				return DatacenterSiteBasedServerPicker.staticInstance;
			}
		}

		// Token: 0x06000799 RID: 1945 RVA: 0x0001ED78 File Offset: 0x0001CF78
		protected override List<InternalExchangeServer> LoadConfiguration()
		{
			List<InternalExchangeServer> list = new List<InternalExchangeServer>(10);
			CallIdTracer.TraceDebug(ExTraceGlobals.UtilTracer, this.GetHashCode(), "DatacenterSiteBasedServerPicker::LoadConfiguration()", new object[0]);
			ADTopologyLookup adtopologyLookup = ADTopologyLookup.CreateLocalResourceForestLookup();
			IEnumerable<Server> enabledExchangeServers = adtopologyLookup.GetEnabledExchangeServers(VersionEnum.E14Legacy, ServerRole.UnifiedMessaging);
			foreach (Server server in enabledExchangeServers)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.UtilTracer, this.GetHashCode(), "DatacenterSiteBasedServerPicker::LoadConfiguration() Adding Server={0} Version={1} UMRole={2} Status={3}", new object[]
				{
					server.Fqdn,
					server.VersionNumber,
					server.IsUnifiedMessagingServer,
					server.Status
				});
				list.Add(new InternalExchangeServer(server));
			}
			return list;
		}

		// Token: 0x0600079A RID: 1946 RVA: 0x0001EE60 File Offset: 0x0001D060
		protected override bool IsValid(ADObjectId siteId, InternalExchangeServer candidate)
		{
			Server server = candidate.Server;
			return server.ServerSite != null && server.ServerSite.ObjectGuid.Equals(siteId.ObjectGuid);
		}

		// Token: 0x0400083A RID: 2106
		internal static readonly TimeSpan UMDefaultRetryInterval = TimeSpan.FromMinutes(1.0);

		// Token: 0x0400083B RID: 2107
		internal static readonly TimeSpan UMDefaultRefreshInterval = TimeSpan.FromMinutes(2.0);

		// Token: 0x0400083C RID: 2108
		internal static readonly TimeSpan UMDefaultRefreshIntervalOnFailure = TimeSpan.FromMinutes(1.0);

		// Token: 0x0400083D RID: 2109
		private static DatacenterSiteBasedServerPicker staticInstance;

		// Token: 0x0400083E RID: 2110
		private static object lockObj = new object();
	}
}
