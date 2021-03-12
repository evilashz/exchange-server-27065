using System;
using System.Collections.Generic;
using Microsoft.Exchange.Extensibility.Internal;

namespace Microsoft.Exchange.Transport.Categorizer
{
	// Token: 0x02000257 RID: 599
	internal class RoutingContextCore
	{
		// Token: 0x060019E4 RID: 6628 RVA: 0x00069E2C File Offset: 0x0006802C
		public RoutingContextCore(ProcessTransportRole role, TransportAppConfig.RoutingConfig settings, RoutingDependencies dependencies, EdgeRoutingDependencies edgeDependencies, RoutingPerformanceCounters perfCounters)
		{
			RoutingUtils.ThrowIfNull(settings, "settings");
			RoutingUtils.ThrowIfNull(dependencies, "dependencies");
			RoutingUtils.ThrowIfNull(perfCounters, "perfCounters");
			if (role == ProcessTransportRole.Edge)
			{
				RoutingUtils.ThrowIfNull(edgeDependencies, "edgeDependencies");
			}
			this.role = role;
			this.settings = settings;
			this.dependencies = dependencies;
			this.edgeDependencies = edgeDependencies;
			this.perfCounters = perfCounters;
			this.proxyRoutingAllowedTargetVersions = new List<int>(0);
			if (this.ProxyRoutingXVersionSupported)
			{
				foreach (int item in this.settings.ProxyRoutingAllowedTargetVersions)
				{
					if (!this.proxyRoutingAllowedTargetVersions.Contains(item))
					{
						this.proxyRoutingAllowedTargetVersions.Add(item);
					}
				}
			}
		}

		// Token: 0x170006C6 RID: 1734
		// (get) Token: 0x060019E5 RID: 6629 RVA: 0x00069F04 File Offset: 0x00068104
		public RoutingDependencies Dependencies
		{
			get
			{
				return this.dependencies;
			}
		}

		// Token: 0x170006C7 RID: 1735
		// (get) Token: 0x060019E6 RID: 6630 RVA: 0x00069F0C File Offset: 0x0006810C
		public EdgeRoutingDependencies EdgeDependencies
		{
			get
			{
				return this.edgeDependencies;
			}
		}

		// Token: 0x170006C8 RID: 1736
		// (get) Token: 0x060019E7 RID: 6631 RVA: 0x00069F14 File Offset: 0x00068114
		public RoutingPerformanceCounters PerfCounters
		{
			get
			{
				return this.perfCounters;
			}
		}

		// Token: 0x170006C9 RID: 1737
		// (get) Token: 0x060019E8 RID: 6632 RVA: 0x00069F1C File Offset: 0x0006811C
		public TransportAppConfig.RoutingConfig Settings
		{
			get
			{
				return this.settings;
			}
		}

		// Token: 0x170006CA RID: 1738
		// (get) Token: 0x060019E9 RID: 6633 RVA: 0x00069F24 File Offset: 0x00068124
		public bool IsEdgeMode
		{
			get
			{
				return this.role == ProcessTransportRole.Edge;
			}
		}

		// Token: 0x170006CB RID: 1739
		// (get) Token: 0x060019EA RID: 6634 RVA: 0x00069F2F File Offset: 0x0006812F
		public bool ConnectorRoutingSupported
		{
			get
			{
				return this.MessageQueuesSupported;
			}
		}

		// Token: 0x170006CC RID: 1740
		// (get) Token: 0x060019EB RID: 6635 RVA: 0x00069F37 File Offset: 0x00068137
		public bool DeliveryGroupMembershipSupported
		{
			get
			{
				return this.role == ProcessTransportRole.Hub || this.role == ProcessTransportRole.MailboxSubmission || this.role == ProcessTransportRole.MailboxDelivery;
			}
		}

		// Token: 0x170006CD RID: 1741
		// (get) Token: 0x060019EC RID: 6636 RVA: 0x00069F55 File Offset: 0x00068155
		public bool MailboxDeliveryQueuesSupported
		{
			get
			{
				return this.role == ProcessTransportRole.Hub;
			}
		}

		// Token: 0x170006CE RID: 1742
		// (get) Token: 0x060019ED RID: 6637 RVA: 0x00069F60 File Offset: 0x00068160
		public bool MessageQueuesSupported
		{
			get
			{
				return this.role == ProcessTransportRole.Hub || this.role == ProcessTransportRole.Edge;
			}
		}

		// Token: 0x170006CF RID: 1743
		// (get) Token: 0x060019EE RID: 6638 RVA: 0x00069F75 File Offset: 0x00068175
		public bool ProxyRoutingSupported
		{
			get
			{
				return !this.MessageQueuesSupported;
			}
		}

		// Token: 0x170006D0 RID: 1744
		// (get) Token: 0x060019EF RID: 6639 RVA: 0x00069F80 File Offset: 0x00068180
		public bool ProxyRoutingXVersionSupported
		{
			get
			{
				return this.role == ProcessTransportRole.FrontEnd;
			}
		}

		// Token: 0x170006D1 RID: 1745
		// (get) Token: 0x060019F0 RID: 6640 RVA: 0x00069F8B File Offset: 0x0006818B
		public IList<int> ProxyRoutingAllowedTargetVersions
		{
			get
			{
				return this.proxyRoutingAllowedTargetVersions;
			}
		}

		// Token: 0x170006D2 RID: 1746
		// (get) Token: 0x060019F1 RID: 6641 RVA: 0x00069F93 File Offset: 0x00068193
		public bool ServerRoutingSupported
		{
			get
			{
				return this.role == ProcessTransportRole.Hub || this.role == ProcessTransportRole.FrontEnd || this.role == ProcessTransportRole.MailboxSubmission || this.role == ProcessTransportRole.MailboxDelivery;
			}
		}

		// Token: 0x170006D3 RID: 1747
		// (get) Token: 0x060019F2 RID: 6642 RVA: 0x00069FBA File Offset: 0x000681BA
		public bool ShadowRoutingSupported
		{
			get
			{
				return this.role == ProcessTransportRole.Hub;
			}
		}

		// Token: 0x060019F3 RID: 6643 RVA: 0x00069FC5 File Offset: 0x000681C5
		public ProcessTransportRole GetProcessRoleForDiagnostics()
		{
			return this.role;
		}

		// Token: 0x060019F4 RID: 6644 RVA: 0x00069FCD File Offset: 0x000681CD
		public bool VerifyFrontendComponentStateRestriction(RoutingServerInfo serverInfo)
		{
			RoutingUtils.ThrowIfNull(serverInfo, "serverInfo");
			return this.settings.RoutingToNonActiveServersEnabled || serverInfo.IsFrontendTransportActive;
		}

		// Token: 0x060019F5 RID: 6645 RVA: 0x00069FEF File Offset: 0x000681EF
		public bool VerifyHubComponentStateRestriction(RoutingServerInfo serverInfo)
		{
			RoutingUtils.ThrowIfNull(serverInfo, "serverInfo");
			return this.settings.RoutingToNonActiveServersEnabled || serverInfo.IsHubTransportActive;
		}

		// Token: 0x04000C5D RID: 3165
		private readonly ProcessTransportRole role;

		// Token: 0x04000C5E RID: 3166
		private TransportAppConfig.RoutingConfig settings;

		// Token: 0x04000C5F RID: 3167
		private RoutingDependencies dependencies;

		// Token: 0x04000C60 RID: 3168
		private EdgeRoutingDependencies edgeDependencies;

		// Token: 0x04000C61 RID: 3169
		private RoutingPerformanceCounters perfCounters;

		// Token: 0x04000C62 RID: 3170
		private IList<int> proxyRoutingAllowedTargetVersions;
	}
}
