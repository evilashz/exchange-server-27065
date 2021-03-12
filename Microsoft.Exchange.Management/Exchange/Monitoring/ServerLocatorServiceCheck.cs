﻿using System;
using Microsoft.Exchange.Data.Storage.ServerLocator;
using Microsoft.Exchange.Diagnostics.Components.Cluster.Replay;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x02000560 RID: 1376
	internal class ServerLocatorServiceCheck : ReplicationCheck
	{
		// Token: 0x060030E4 RID: 12516 RVA: 0x000C6308 File Offset: 0x000C4508
		public ServerLocatorServiceCheck(string serverName, IEventManager eventManager, string momeventsource) : base("ServerLocatorService", CheckId.ServerLocatorService, Strings.ServerLocatorServiceCheckDesc, CheckCategory.SystemHighPriority, eventManager, momeventsource, serverName)
		{
		}

		// Token: 0x060030E5 RID: 12517 RVA: 0x000C6324 File Offset: 0x000C4524
		public ServerLocatorServiceCheck(string serverName, IEventManager eventManager, string momeventsource, uint? ignoreTransientErrorsThreshold) : base("ServerLocatorService", CheckId.ServerLocatorService, Strings.ServerLocatorServiceCheckDesc, CheckCategory.SystemHighPriority, eventManager, momeventsource, serverName, ignoreTransientErrorsThreshold)
		{
		}

		// Token: 0x060030E6 RID: 12518 RVA: 0x000C634C File Offset: 0x000C454C
		protected override void InternalRun()
		{
			if (!ReplicationCheckGlobals.ServerLocatorServiceCheckHasRun)
			{
				ReplicationCheckGlobals.ServerLocatorServiceCheckHasRun = true;
			}
			else
			{
				ExTraceGlobals.HealthChecksTracer.TraceDebug((long)this.GetHashCode(), "ServerLocatorServiceCheck skipping because it has already been run once.");
				base.Skip();
			}
			if (!IgnoreTransientErrors.HasPassed(base.GetDefaultErrorKey(typeof(ReplayServiceCheck))))
			{
				ExTraceGlobals.HealthChecksTracer.TraceDebug<string>((long)this.GetHashCode(), "ReplayServiceCheck didn't pass! Skipping {0}.", base.Title);
				base.Skip();
			}
			if ((ReplicationCheckGlobals.ServerConfiguration & ServerConfig.Stopped) == ServerConfig.Stopped)
			{
				ExTraceGlobals.HealthChecksTracer.TraceDebug<string>((long)this.GetHashCode(), "Stopped server! Skipping {0}.", base.Title);
				base.Skip();
			}
			string text = null;
			bool flag = false;
			ServerLocatorServiceClient serverLocatorServiceClient = null;
			TimeSpan timeSpan = TimeSpan.FromSeconds(5.0);
			try
			{
				serverLocatorServiceClient = ServerLocatorServiceClient.Create(base.ServerName, timeSpan, timeSpan, timeSpan, timeSpan);
				serverLocatorServiceClient.GetVersion();
				flag = true;
			}
			catch (ServerLocatorClientException ex)
			{
				text = ex.Message;
			}
			catch (ServerLocatorClientTransientException ex2)
			{
				text = ex2.Message;
			}
			finally
			{
				if (serverLocatorServiceClient != null)
				{
					serverLocatorServiceClient.Dispose();
					serverLocatorServiceClient = null;
				}
			}
			ExTraceGlobals.HealthChecksTracer.TraceDebug<string, bool, string>((long)this.GetHashCode(), "ServerLocatorServiceCheck: TestHealth() for server '{0}' returned: healthy={1}, errMsg='{2}'", base.ServerName, flag, text);
			if (!flag)
			{
				base.Fail(Strings.ServerLocatorServiceRequestFailed(base.ServerName, text));
			}
		}
	}
}
