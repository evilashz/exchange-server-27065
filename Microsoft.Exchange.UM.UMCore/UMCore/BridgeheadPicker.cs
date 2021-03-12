using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x02000042 RID: 66
	internal class BridgeheadPicker : ServerPickerBase<InternalExchangeServer, object>
	{
		// Token: 0x17000094 RID: 148
		// (get) Token: 0x060002B1 RID: 689 RVA: 0x0000BF20 File Offset: 0x0000A120
		private InternalExchangeServer LocalServer
		{
			get
			{
				if (this.localServer == null)
				{
					lock (this.localServerLock)
					{
						if (this.localServer == null)
						{
							this.localServer = this.GetLocalServer();
						}
					}
				}
				return this.localServer;
			}
		}

		// Token: 0x060002B2 RID: 690 RVA: 0x0000BF7C File Offset: 0x0000A17C
		internal BridgeheadPicker() : base(ServerPickerBase<InternalExchangeServer, object>.DefaultRetryInterval, ServerPickerBase<InternalExchangeServer, object>.DefaultRefreshInterval, ServerPickerBase<InternalExchangeServer, object>.DefaultRefreshIntervalOnFailure, ExTraceGlobals.VoiceMailTracer)
		{
		}

		// Token: 0x060002B3 RID: 691 RVA: 0x0000BFA4 File Offset: 0x0000A1A4
		public override InternalExchangeServer PickNextServer(object context)
		{
			InternalExchangeServer internalExchangeServer = base.PickNextServer(context);
			if (!base.ServerInRetry(this.LocalServer))
			{
				CallIdTracer.TraceDebug(base.Tracer, 0, "BridgeheadServerPicker::PickNextServer() local server not in retry. Using it.", new object[0]);
				internalExchangeServer = this.LocalServer;
			}
			CallIdTracer.TraceDebug(base.Tracer, 0, "BridgeheadServerPicker::PickNextServer() returning  {0}", new object[]
			{
				(internalExchangeServer != null) ? internalExchangeServer.Fqdn : "<null>"
			});
			return internalExchangeServer;
		}

		// Token: 0x060002B4 RID: 692 RVA: 0x0000C01C File Offset: 0x0000A21C
		protected override List<InternalExchangeServer> LoadConfiguration()
		{
			CallIdTracer.TraceDebug(base.Tracer, 0, "BridgeheadServerPicker::LoadConfiguration() called", new object[0]);
			List<InternalExchangeServer> list = new List<InternalExchangeServer>(4);
			IEnumerable<Server> enumerable;
			if (this.LocalServer.Server.DatabaseAvailabilityGroup == null)
			{
				CallIdTracer.TraceDebug(base.Tracer, 0, "BridgeheadServerPicker: No DAG configured, adding servers in the site", new object[0]);
				enumerable = this.GetLocalSiteHubServers();
			}
			else
			{
				CallIdTracer.TraceDebug(base.Tracer, 0, "BridgeheadServerPicker: DAG configured, adding servers in the DAG", new object[0]);
				enumerable = this.GetDagHubServers();
			}
			foreach (Server s in enumerable)
			{
				InternalExchangeServer internalExchangeServer = new InternalExchangeServer(s);
				CallIdTracer.TraceDebug(base.Tracer, 0, "BridgeheadServerPicker: Adding Hub server {0} to active list", new object[]
				{
					internalExchangeServer.Fqdn
				});
				list.Add(internalExchangeServer);
			}
			CallIdTracer.TraceDebug(base.Tracer, 0, "BridgeheadServerPicker::LoadConfiguration() Finished refreshing hub servers", new object[0]);
			return list;
		}

		// Token: 0x060002B5 RID: 693 RVA: 0x0000C134 File Offset: 0x0000A334
		protected virtual InternalExchangeServer GetLocalServer()
		{
			return new InternalExchangeServer(ADTopologyLookup.CreateLocalResourceForestLookup().GetLocalServer());
		}

		// Token: 0x060002B6 RID: 694 RVA: 0x0000C145 File Offset: 0x0000A345
		protected virtual IEnumerable<Server> GetLocalSiteHubServers()
		{
			return ADTopologyLookup.CreateLocalResourceForestLookup().GetEnabledExchangeServers(VersionEnum.Compatible, ServerRole.HubTransport, this.LocalServer.ServerSite);
		}

		// Token: 0x060002B7 RID: 695 RVA: 0x0000C15F File Offset: 0x0000A35F
		protected virtual IEnumerable<Server> GetDagHubServers()
		{
			return ADTopologyLookup.CreateLocalResourceForestLookup().GetDatabaseAvailabilityGroupServers(VersionEnum.Compatible, ServerRole.HubTransport, this.LocalServer.Server.DatabaseAvailabilityGroup);
		}

		// Token: 0x040000DA RID: 218
		private InternalExchangeServer localServer;

		// Token: 0x040000DB RID: 219
		private object localServerLock = new object();
	}
}
