using System;
using System.Net;
using System.Threading;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Net;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring
{
	// Token: 0x02000087 RID: 135
	public class ExternalDnsProbe : ProbeWorkItem
	{
		// Token: 0x060003A5 RID: 933 RVA: 0x00015880 File Offset: 0x00013A80
		protected override void DoWork(CancellationToken cancellationToken)
		{
			ExternalDnsProbe.RefreshExternalDnsServerList();
			string text = base.Definition.Attributes["Text"];
			CancellationTokenRegistration cancellationTokenRegistration = cancellationToken.Register(delegate()
			{
				this.resolveFinished.Set();
			});
			using (cancellationTokenRegistration)
			{
				IAsyncResult ar2 = ExternalDnsProbe.dnsResolver.BeginResolveToMailServers(text, DnsQueryOptions.BypassCache, delegate(IAsyncResult ar)
				{
					this.resolveFinished.Set();
				}, text);
				this.resolveFinished.WaitOne();
				if (cancellationToken.IsCancellationRequested)
				{
					throw new OperationCanceledException(cancellationToken);
				}
				this.ResolveComplete(ar2);
			}
		}

		// Token: 0x060003A6 RID: 934 RVA: 0x00015924 File Offset: 0x00013B24
		private static void RefreshExternalDnsServerList()
		{
			bool flag = false;
			DateTime dateTime = ExternalDnsProbe.lastRefreshTS;
			if ((DateTime.UtcNow - ExternalDnsProbe.lastRefreshTS).TotalSeconds >= 3600.0)
			{
				flag = true;
			}
			if (flag)
			{
				ITopologyConfigurationSession topologyConfigurationSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 98, "RefreshExternalDnsServerList", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ForefrontActiveMonitoring\\Components\\DNS\\Probes\\ExternalDnsProbe.cs");
				Server server = topologyConfigurationSession.FindLocalServer();
				if (server == null)
				{
					throw new Exception("FindLocalServer() failed");
				}
				ADObjectId childId = server.Id.GetChildId("Transport Configuration");
				ADObjectId childId2 = childId.GetChildId("Frontend");
				FrontendTransportServer frontendTransportServer = topologyConfigurationSession.Read<FrontendTransportServer>(childId2);
				if (frontendTransportServer == null)
				{
					throw new Exception("this is not a Frontend server");
				}
				if (frontendTransportServer.ExternalDNSAdapterEnabled || MultiValuedPropertyBase.IsNullOrEmpty(frontendTransportServer.ExternalDNSServers))
				{
					ExternalDnsProbe.dnsResolver.AdapterServerList(frontendTransportServer.ExternalDNSAdapterGuid);
				}
				else if (frontendTransportServer.ExternalDNSServers != null && frontendTransportServer.ExternalDNSServers.Count > 0)
				{
					IPAddress[] array = new IPAddress[frontendTransportServer.ExternalDNSServers.Count];
					frontendTransportServer.ExternalDNSServers.CopyTo(array, 0);
					ExternalDnsProbe.dnsResolver.InitializeServerList(array);
				}
				ExternalDnsProbe.lastRefreshTS = DateTime.UtcNow;
			}
		}

		// Token: 0x060003A7 RID: 935 RVA: 0x00015A4C File Offset: 0x00013C4C
		private void ResolveComplete(IAsyncResult ar)
		{
			TargetHost[] array;
			DnsStatus dnsStatus = Dns.EndResolveToMailServers(ar, out array);
			if (dnsStatus != DnsStatus.Success)
			{
				string arg = (string)ar.AsyncState;
				throw new Exception(string.Format("External DNS MX query for {0} failed with status = {1}", arg, dnsStatus));
			}
		}

		// Token: 0x04000210 RID: 528
		private static Dns dnsResolver = new Dns();

		// Token: 0x04000211 RID: 529
		private static DateTime lastRefreshTS;

		// Token: 0x04000212 RID: 530
		private ManualResetEvent resolveFinished = new ManualResetEvent(false);
	}
}
