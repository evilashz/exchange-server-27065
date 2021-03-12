using System;
using System.Collections.Generic;
using System.DirectoryServices.Protocols;
using System.Net;
using System.Threading;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.TopologyDiscovery;
using Microsoft.Exchange.Diagnostics.Components.Data.Directory;
using Microsoft.Win32;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000025 RID: 37
	internal class AdamTopologyProvider : TopologyProvider
	{
		// Token: 0x06000277 RID: 631 RVA: 0x0000E9E3 File Offset: 0x0000CBE3
		internal AdamTopologyProvider(int adamPort)
		{
			ExTraceGlobals.TopologyProviderTracer.TraceDebug<int, int>((long)this.GetHashCode(), "Creating new ADAM topo provider instance {0}, port {1}", this.GetHashCode(), adamPort);
			this.adamPort = adamPort;
			this.topologyVersion = 1;
			ADProviderPerf.AddDCInstance(Environment.MachineName);
		}

		// Token: 0x06000278 RID: 632 RVA: 0x0000EA20 File Offset: 0x0000CC20
		internal static bool CheckIfAdamConfigured(out int portNumber)
		{
			bool flag = false;
			RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\ExchangeServer\\v15\\EdgeTransportRole\\AdamSettings\\MSExchange\\");
			if (registryKey != null)
			{
				try
				{
					portNumber = (int)registryKey.GetValue("LdapPort", 389, RegistryValueOptions.DoNotExpandEnvironmentNames);
					flag = true;
					goto IL_40;
				}
				finally
				{
					registryKey.Close();
				}
			}
			portNumber = 0;
			IL_40:
			ExTraceGlobals.TopologyProviderTracer.TraceDebug<bool, int>(0L, "CheckIfAdamIsConfigured returns {0}, port {1}", flag, portNumber);
			return flag;
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x06000279 RID: 633 RVA: 0x0000EA94 File Offset: 0x0000CC94
		internal override TopologyMode TopologyMode
		{
			get
			{
				return TopologyMode.Adam;
			}
		}

		// Token: 0x0600027A RID: 634 RVA: 0x0000EA97 File Offset: 0x0000CC97
		public override IList<TopologyVersion> GetAllTopologyVersions()
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600027B RID: 635 RVA: 0x0000EA9E File Offset: 0x0000CC9E
		public int GetTopologyVersion()
		{
			return this.topologyVersion;
		}

		// Token: 0x0600027C RID: 636 RVA: 0x0000EAA8 File Offset: 0x0000CCA8
		public override IList<TopologyVersion> GetTopologyVersions(IList<string> partitionFqdns)
		{
			if (partitionFqdns == null)
			{
				throw new ArgumentException("partitionFqdns");
			}
			TopologyProvider.EnforceLocalForestPartition(partitionFqdns[0]);
			return new List<TopologyVersion>
			{
				new TopologyVersion(partitionFqdns[0], this.topologyVersion)
			};
		}

		// Token: 0x0600027D RID: 637 RVA: 0x0000EAF0 File Offset: 0x0000CCF0
		protected override IList<ADServerInfo> InternalGetServersForRole(string partitionFqdn, IList<string> currentlyUsedServers, ADServerRole role, int serversRequested, bool forestWideAffinityRequested = false)
		{
			TopologyProvider.EnforceLocalForestPartition(partitionFqdn);
			return new List<ADServerInfo>
			{
				this.GetDefaultServerInfo(partitionFqdn)
			};
		}

		// Token: 0x0600027E RID: 638 RVA: 0x0000EB17 File Offset: 0x0000CD17
		public override ADServerInfo GetServerFromDomainDN(string distinguishedName, NetworkCredential credential)
		{
			throw new NotSupportedException("ADAM topology provider works only with local objects");
		}

		// Token: 0x0600027F RID: 639 RVA: 0x0000EB23 File Offset: 0x0000CD23
		public override ADServerInfo GetRemoteServerFromDomainFqdn(string domainFqdn, NetworkCredential credential)
		{
			throw new NotSupportedException("ADAM topology provider works only with local objects");
		}

		// Token: 0x06000280 RID: 640 RVA: 0x0000EB2F File Offset: 0x0000CD2F
		internal override ADServerInfo GetConfigDCInfo(string partitionFqdn, bool throwOnFailure)
		{
			return this.GetDefaultServerInfo(partitionFqdn);
		}

		// Token: 0x06000281 RID: 641 RVA: 0x0000EB38 File Offset: 0x0000CD38
		public override void ReportServerDown(string partitionFqdn, string serverName, ADServerRole role)
		{
			int arg = Interlocked.Increment(ref this.topologyVersion);
			ExTraceGlobals.TopologyProviderTracer.TraceDebug<int>((long)this.GetHashCode(), "New topo version is {0}", arg);
		}

		// Token: 0x06000282 RID: 642 RVA: 0x0000EB68 File Offset: 0x0000CD68
		protected override ADServerInfo GetDefaultServerInfo(string partitionFqdn)
		{
			LocalizedString empty = LocalizedString.Empty;
			try
			{
				if (!SuitabilityVerifier.IsAdamServerSuitable("localhost", this.adamPort, null, ref empty))
				{
					throw new NoSuitableServerFoundException(empty);
				}
			}
			catch (ADTransientException ex)
			{
				throw new NoSuitableServerFoundException(new LocalizedString(ex.Message), ex);
			}
			return new ADServerInfo("localhost", this.adamPort, "OU=MsExchangeGateway", 100, AuthType.Negotiate);
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06000283 RID: 643 RVA: 0x0000EBD4 File Offset: 0x0000CDD4
		internal override int DefaultDCPort
		{
			get
			{
				return this.adamPort;
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x06000284 RID: 644 RVA: 0x0000EBDC File Offset: 0x0000CDDC
		internal override int DefaultGCPort
		{
			get
			{
				return this.adamPort;
			}
		}

		// Token: 0x04000090 RID: 144
		public const string AdamNamingContext = "OU=MsExchangeGateway";

		// Token: 0x04000091 RID: 145
		private int adamPort;

		// Token: 0x04000092 RID: 146
		private int topologyVersion;
	}
}
