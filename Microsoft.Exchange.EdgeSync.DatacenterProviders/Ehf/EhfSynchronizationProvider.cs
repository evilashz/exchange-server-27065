using System;
using System.Collections.Generic;
using System.Globalization;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Ehf;
using Microsoft.Exchange.EdgeSync.Common;
using Microsoft.Exchange.EdgeSync.Logging;

namespace Microsoft.Exchange.EdgeSync.Ehf
{
	// Token: 0x0200002F RID: 47
	internal class EhfSynchronizationProvider : SynchronizationProvider
	{
		// Token: 0x17000069 RID: 105
		// (get) Token: 0x06000211 RID: 529 RVA: 0x0000E85D File Offset: 0x0000CA5D
		public override string Identity
		{
			get
			{
				if (this.identity == null)
				{
					throw new InvalidOperationException("EhfSynchronizationProvider.Identity property is accessed before EhfSynchronizationProvider.Initialize() method is invoked.");
				}
				return this.identity;
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x06000212 RID: 530 RVA: 0x0000E878 File Offset: 0x0000CA78
		public override int LeaseLockTryCount
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x06000213 RID: 531 RVA: 0x0000E87B File Offset: 0x0000CA7B
		public override List<TargetServerConfig> TargetServerConfigs
		{
			get
			{
				if (this.targetServerConfigs == null)
				{
					throw new InvalidOperationException("EhfSynchronizationProvider.TargetServerConfigs property is accessed before EhfSynchronizationProvider.Initialize() method is invoked.");
				}
				return this.targetServerConfigs;
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x06000214 RID: 532 RVA: 0x0000E896 File Offset: 0x0000CA96
		public override EnhancedTimeSpan RecipientSyncInterval
		{
			get
			{
				return this.adminSyncInterval;
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x06000215 RID: 533 RVA: 0x0000E8A3 File Offset: 0x0000CAA3
		public override EnhancedTimeSpan ConfigurationSyncInterval
		{
			get
			{
				return EdgeSyncSvc.EdgeSync.Config.ServiceConfig.ConfigurationSyncInterval;
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x06000216 RID: 534 RVA: 0x0000E8B9 File Offset: 0x0000CAB9
		public EhfSyncErrorTracker AdminSyncErrorTracker
		{
			get
			{
				return this.adminSyncErrorTracker;
			}
		}

		// Token: 0x06000217 RID: 535 RVA: 0x0000E8C4 File Offset: 0x0000CAC4
		public static void ValidateProvisioningUrl(Uri url, PSCredential authCredential, string connectorId)
		{
			string text = null;
			if (url == null)
			{
				text = "is not specified; an absolute URI must be specified";
			}
			else if (!url.IsAbsoluteUri)
			{
				text = "is not absolute; an absolute URI must be specified";
			}
			else
			{
				bool flag = authCredential != null && !string.IsNullOrEmpty(authCredential.UserName);
				string scheme = url.Scheme;
				if (scheme.Equals(Uri.UriSchemeHttp, StringComparison.OrdinalIgnoreCase))
				{
					if (flag)
					{
						text = "has the 'http' scheme that is not allowed when authentication credentials are provided; the 'https' scheme is expected";
					}
				}
				else if (scheme.Equals(Uri.UriSchemeHttps, StringComparison.OrdinalIgnoreCase))
				{
					if (!flag)
					{
						text = "has the 'https' scheme that is not allowed when authentication credentials are not provided; the 'http' scheme is expected";
					}
				}
				else
				{
					text = "has an unsupported scheme; only 'http' and 'https' schemes are allowed";
				}
			}
			if (text != null)
			{
				text = string.Format(CultureInfo.InvariantCulture, "EHF connector <{0}>: Provisioning URL <{1}> {2}", new object[]
				{
					connectorId,
					url ?? string.Empty,
					text
				});
				EhfSynchronizationProvider.tracer.TraceError<string>(0L, "{0}", text);
				throw new ExDirectoryException(new ArgumentException(text));
			}
		}

		// Token: 0x06000218 RID: 536 RVA: 0x0000E998 File Offset: 0x0000CB98
		public static int GetResellerId(EdgeSyncEhfConnector connector)
		{
			int result;
			if (!int.TryParse(connector.ResellerId, out result))
			{
				string text = string.Format(CultureInfo.InvariantCulture, "EHF connector <{0}> conatins invalid Reseller ID <{1}>; reseller ID must be an integer value", new object[]
				{
					connector.DistinguishedName,
					connector.ResellerId ?? "null"
				});
				EhfSynchronizationProvider.tracer.TraceError<string>((long)connector.GetHashCode(), "{0}", text);
				throw new ExDirectoryException(new FormatException(text));
			}
			return result;
		}

		// Token: 0x06000219 RID: 537 RVA: 0x0000EA0C File Offset: 0x0000CC0C
		public override void Initialize(EdgeSyncConnector connector)
		{
			EdgeSyncEhfConnector connector2 = (EdgeSyncEhfConnector)connector;
			this.InitializeIdentity(connector2);
			this.InitializeTargetServerConfig(connector2);
		}

		// Token: 0x0600021A RID: 538 RVA: 0x0000EA30 File Offset: 0x0000CC30
		public override List<TypeSynchronizer> CreateTypeSynchronizer(SyncTreeType type)
		{
			List<TypeSynchronizer> list = new List<TypeSynchronizer>();
			if (this.ehfWebServiceVersion != EhfWebServiceVersion.Version1 && this.ehfWebServiceVersion != EhfWebServiceVersion.Version2)
			{
				EdgeSyncEvents.Log.LogEvent(EdgeSyncEventLogConstants.Tuple_EhfWebServiceVersionIsNotSupported, null, new object[]
				{
					this.ehfWebServiceVersion,
					this.identity
				});
				return list;
			}
			if (type == SyncTreeType.Configuration)
			{
				list.Add(EhfCompanySynchronizer.CreateTypeSynchronizer());
				list.Add(EhfDomainSynchronizer.CreateTypeSynchronizer());
			}
			else if (type == SyncTreeType.Recipients)
			{
				list.Add(EhfAdminAccountSynchronizer.CreateTypeSynchronizer());
			}
			return list;
		}

		// Token: 0x0600021B RID: 539 RVA: 0x0000EAB4 File Offset: 0x0000CCB4
		public override TargetConnection CreateTargetConnection(TargetServerConfig targetServerConfig, SyncTreeType type, TestShutdownAndLeaseDelegate testShutdownAndLease, EdgeSyncLogSession logSession)
		{
			if (type == SyncTreeType.Configuration)
			{
				return new EhfConfigTargetConnection(EdgeSyncSvc.EdgeSync.Topology.LocalServer.VersionNumber, (EhfTargetServerConfig)targetServerConfig, this.ConfigurationSyncInterval, logSession);
			}
			if (type == SyncTreeType.Recipients)
			{
				return new EhfRecipientTargetConnection(EdgeSyncSvc.EdgeSync.Topology.LocalServer.VersionNumber, (EhfTargetServerConfig)targetServerConfig, this, logSession);
			}
			throw new NotSupportedException("Only config and recipient synchronization is supported by EHF sync provider");
		}

		// Token: 0x0600021C RID: 540 RVA: 0x0000EB20 File Offset: 0x0000CD20
		private void InitializeIdentity(EdgeSyncEhfConnector connector)
		{
			ADObjectId id = connector.Id;
			if (id.Depth > 2)
			{
				string name = id.AncestorDN(2).Name;
				this.identity = string.Format(CultureInfo.InvariantCulture, "{0}\\{1}", new object[]
				{
					name,
					connector.Name
				});
			}
			else
			{
				this.identity = id.DistinguishedName;
			}
			EhfSynchronizationProvider.tracer.TraceDebug<string, string>((long)this.GetHashCode(), "Initialized provider identity to <{0}> based on the connector DN <{1}>", this.identity, id.DistinguishedName);
		}

		// Token: 0x0600021D RID: 541 RVA: 0x0000EBA4 File Offset: 0x0000CDA4
		private void InitializeTargetServerConfig(EdgeSyncEhfConnector connector)
		{
			EhfTargetServerConfig ehfTargetServerConfig = new EhfTargetServerConfig(connector, EdgeSyncSvc.EdgeSync.Topology.LocalServer.InternetWebProxy);
			this.targetServerConfigs = new List<TargetServerConfig>(1);
			this.targetServerConfigs.Add(ehfTargetServerConfig);
			this.ehfWebServiceVersion = ehfTargetServerConfig.EhfWebServiceVersion;
			this.adminSyncInterval = ehfTargetServerConfig.EhfSyncAppConfig.EhfAdminSyncInterval;
			this.adminSyncErrorTracker.Initialize(ehfTargetServerConfig.EhfSyncAppConfig);
			EhfSynchronizationProvider.tracer.TraceDebug((long)this.GetHashCode(), "Initialized target server configuration for connector: <{0}> ProvisioningUrl: <{1}> PrimaryLeaseLocation: <{2}> BackupLeaseLocation: <{3}> Version: <{4}>", new object[]
			{
				ehfTargetServerConfig.Name,
				ehfTargetServerConfig.ProvisioningUrl,
				ehfTargetServerConfig.PrimaryLeaseLocation,
				ehfTargetServerConfig.BackupLeaseLocation,
				connector.Version
			});
		}

		// Token: 0x040000CE RID: 206
		private const int LeaseLockTryCountValue = 1;

		// Token: 0x040000CF RID: 207
		private static Trace tracer = ExTraceGlobals.ProviderTracer;

		// Token: 0x040000D0 RID: 208
		private string identity;

		// Token: 0x040000D1 RID: 209
		private TimeSpan adminSyncInterval;

		// Token: 0x040000D2 RID: 210
		private List<TargetServerConfig> targetServerConfigs;

		// Token: 0x040000D3 RID: 211
		private EhfWebServiceVersion ehfWebServiceVersion;

		// Token: 0x040000D4 RID: 212
		private EhfSyncErrorTracker adminSyncErrorTracker = new EhfSyncErrorTracker();
	}
}
