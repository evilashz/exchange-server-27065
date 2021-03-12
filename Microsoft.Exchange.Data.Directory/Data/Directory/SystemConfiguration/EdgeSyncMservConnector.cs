using System;
using System.Collections.Generic;
using System.Globalization;
using System.Management.Automation;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net.Mserve;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000435 RID: 1077
	[Serializable]
	public class EdgeSyncMservConnector : EdgeSyncConnector
	{
		// Token: 0x17000DD8 RID: 3544
		// (get) Token: 0x0600305C RID: 12380 RVA: 0x000C2AD6 File Offset: 0x000C0CD6
		// (set) Token: 0x0600305D RID: 12381 RVA: 0x000C2AE8 File Offset: 0x000C0CE8
		[Parameter(Mandatory = false)]
		public Uri ProvisionUrl
		{
			get
			{
				return (Uri)this[EdgeSyncMservConnectorSchema.ProvisionUrl];
			}
			set
			{
				this[EdgeSyncMservConnectorSchema.ProvisionUrl] = value;
			}
		}

		// Token: 0x17000DD9 RID: 3545
		// (get) Token: 0x0600305E RID: 12382 RVA: 0x000C2AF6 File Offset: 0x000C0CF6
		// (set) Token: 0x0600305F RID: 12383 RVA: 0x000C2B08 File Offset: 0x000C0D08
		[Parameter(Mandatory = false)]
		public Uri SettingUrl
		{
			get
			{
				return (Uri)this[EdgeSyncMservConnectorSchema.SettingUrl];
			}
			set
			{
				this[EdgeSyncMservConnectorSchema.SettingUrl] = value;
			}
		}

		// Token: 0x17000DDA RID: 3546
		// (get) Token: 0x06003060 RID: 12384 RVA: 0x000C2B16 File Offset: 0x000C0D16
		// (set) Token: 0x06003061 RID: 12385 RVA: 0x000C2B28 File Offset: 0x000C0D28
		[Parameter(Mandatory = false)]
		public string LocalCertificate
		{
			get
			{
				return (string)this[EdgeSyncMservConnectorSchema.LocalCertificate];
			}
			set
			{
				this[EdgeSyncMservConnectorSchema.LocalCertificate] = value;
			}
		}

		// Token: 0x17000DDB RID: 3547
		// (get) Token: 0x06003062 RID: 12386 RVA: 0x000C2B36 File Offset: 0x000C0D36
		// (set) Token: 0x06003063 RID: 12387 RVA: 0x000C2B48 File Offset: 0x000C0D48
		[Parameter(Mandatory = false)]
		public string RemoteCertificate
		{
			get
			{
				return (string)this[EdgeSyncMservConnectorSchema.RemoteCertificate];
			}
			set
			{
				this[EdgeSyncMservConnectorSchema.RemoteCertificate] = value;
			}
		}

		// Token: 0x17000DDC RID: 3548
		// (get) Token: 0x06003064 RID: 12388 RVA: 0x000C2B56 File Offset: 0x000C0D56
		// (set) Token: 0x06003065 RID: 12389 RVA: 0x000C2B68 File Offset: 0x000C0D68
		[Parameter(Mandatory = false)]
		public string PrimaryLeaseLocation
		{
			get
			{
				return (string)this[EdgeSyncMservConnectorSchema.PrimaryLeaseLocation];
			}
			set
			{
				this[EdgeSyncMservConnectorSchema.PrimaryLeaseLocation] = value;
			}
		}

		// Token: 0x17000DDD RID: 3549
		// (get) Token: 0x06003066 RID: 12390 RVA: 0x000C2B76 File Offset: 0x000C0D76
		// (set) Token: 0x06003067 RID: 12391 RVA: 0x000C2B88 File Offset: 0x000C0D88
		[Parameter(Mandatory = false)]
		public string BackupLeaseLocation
		{
			get
			{
				return (string)this[EdgeSyncMservConnectorSchema.BackupLeaseLocation];
			}
			set
			{
				this[EdgeSyncMservConnectorSchema.BackupLeaseLocation] = value;
			}
		}

		// Token: 0x17000DDE RID: 3550
		// (get) Token: 0x06003068 RID: 12392 RVA: 0x000C2B96 File Offset: 0x000C0D96
		internal override ADObjectSchema Schema
		{
			get
			{
				return EdgeSyncMservConnector.schema;
			}
		}

		// Token: 0x17000DDF RID: 3551
		// (get) Token: 0x06003069 RID: 12393 RVA: 0x000C2B9D File Offset: 0x000C0D9D
		internal override string MostDerivedObjectClass
		{
			get
			{
				return "msExchEdgeSyncMservConnector";
			}
		}

		// Token: 0x17000DE0 RID: 3552
		// (get) Token: 0x0600306A RID: 12394 RVA: 0x000C2BA4 File Offset: 0x000C0DA4
		internal override QueryFilter ImplicitFilter
		{
			get
			{
				return new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.ObjectCategory, this.MostDerivedObjectClass);
			}
		}

		// Token: 0x0600306B RID: 12395 RVA: 0x000C2BB8 File Offset: 0x000C0DB8
		internal static string GetMserveWebServiceClientTokenFromEndpointConfig(ITopologyConfigurationSession configSession)
		{
			if (configSession == null)
			{
				configSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(true, ConsistencyMode.PartiallyConsistent, ADSessionSettings.FromRootOrgScopeSet(), 153, "GetMserveWebServiceClientTokenFromEndpointConfig", "f:\\15.00.1497\\sources\\dev\\data\\src\\directory\\SystemConfiguration\\EdgeSyncMservConnector.cs");
			}
			ServiceEndpointContainer endpointContainer = configSession.GetEndpointContainer();
			ServiceEndpoint endpoint = endpointContainer.GetEndpoint(ServiceEndpointId.DeltaSyncPartnerProvision);
			return endpoint.Token;
		}

		// Token: 0x0600306C RID: 12396 RVA: 0x000C2C03 File Offset: 0x000C0E03
		internal static MserveWebService CreateDefaultMserveWebService(string domainController)
		{
			return EdgeSyncMservConnector.CreateDefaultMserveWebService(null, false);
		}

		// Token: 0x0600306D RID: 12397 RVA: 0x000C2C0C File Offset: 0x000C0E0C
		internal static MserveWebService CreateDefaultMserveWebService(string domainController, bool batchMode)
		{
			return EdgeSyncMservConnector.CreateDefaultMserveWebService(domainController, batchMode, 0);
		}

		// Token: 0x0600306E RID: 12398 RVA: 0x000C2CB4 File Offset: 0x000C0EB4
		internal static MserveWebService CreateDefaultMserveWebService(string domainController, bool batchMode, int initialChunkSize)
		{
			ITopologyConfigurationSession rootOrgSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(domainController, true, ConsistencyMode.PartiallyConsistent, ADSessionSettings.FromRootOrgScopeSet(), 200, "CreateDefaultMserveWebService", "f:\\15.00.1497\\sources\\dev\\data\\src\\directory\\SystemConfiguration\\EdgeSyncMservConnector.cs");
			EdgeSyncServiceConfig config = null;
			string clientToken = null;
			ADSite localSite = null;
			ADOperationResult adoperationResult = ADNotificationAdapter.TryRunADOperation(delegate()
			{
				localSite = rootOrgSession.GetLocalSite();
				if (localSite == null)
				{
					throw new TransientException(DirectoryStrings.CannotGetLocalSite);
				}
				config = rootOrgSession.Read<EdgeSyncServiceConfig>(localSite.Id.GetChildId("EdgeSyncService"));
				clientToken = EdgeSyncMservConnector.GetMserveWebServiceClientTokenFromEndpointConfig(rootOrgSession);
			}, 3);
			if (!adoperationResult.Succeeded)
			{
				throw adoperationResult.Exception;
			}
			if (config == null)
			{
				throw new MserveException(string.Format("No EdgeSync configuration found. Site {0}", localSite.DistinguishedName));
			}
			if (string.IsNullOrEmpty(clientToken))
			{
				throw new InvalidOperationException(string.Format("clientToken from Endpoint configuration is null or empty . Site {0}", localSite.DistinguishedName));
			}
			List<EdgeSyncMservConnector> connectors = new List<EdgeSyncMservConnector>();
			if (!ADNotificationAdapter.TryReadConfigurationPaged<EdgeSyncMservConnector>(() => rootOrgSession.FindPaged<EdgeSyncMservConnector>(config.Id, QueryScope.SubTree, null, null, 0), delegate(EdgeSyncMservConnector connector)
			{
				connectors.Add(connector);
			}, 3, out adoperationResult))
			{
				throw adoperationResult.Exception;
			}
			if (connectors.Count == 0)
			{
				throw new InvalidOperationException(string.Format("No MServ configuration found. Site {0}", localSite.DistinguishedName));
			}
			MserveWebService mserveWebService = new MserveWebService(connectors[0].ProvisionUrl.AbsoluteUri, connectors[0].SettingUrl.AbsoluteUri, connectors[0].RemoteCertificate, clientToken, batchMode);
			mserveWebService.Initialize(initialChunkSize);
			return mserveWebService;
		}

		// Token: 0x0600306F RID: 12399 RVA: 0x000C2E20 File Offset: 0x000C1020
		internal static int GetMserveEntryTenantNegoConfig(string domainName)
		{
			IGlobalDirectorySession globalSession = DirectorySessionFactory.GetGlobalSession(null);
			bool flag;
			if (!globalSession.TryGetDomainFlag(domainName, GlsDomainFlags.Nego2Enabled, out flag))
			{
				return -1;
			}
			if (!flag)
			{
				return 0;
			}
			return 1;
		}

		// Token: 0x06003070 RID: 12400 RVA: 0x000C2E48 File Offset: 0x000C1048
		internal static string GetRedirectServer(string redirectFormat, Guid orgId, int currentSiteId, int startRange, int endRange)
		{
			return EdgeSyncMservConnector.GetRedirectServer(redirectFormat, orgId, currentSiteId, startRange, endRange, false, false);
		}

		// Token: 0x06003071 RID: 12401 RVA: 0x000C2E58 File Offset: 0x000C1058
		internal static string GetRedirectServer(string redirectFormat, Guid orgId, int currentSiteId, int startRange, int endRange, bool overrideCurrentSiteCheck, bool throwExceptions)
		{
			string result;
			try
			{
				IGlobalDirectorySession globalSession = DirectorySessionFactory.GetGlobalSession(redirectFormat);
				result = globalSession.GetRedirectServer(orgId);
			}
			catch (MServTransientException)
			{
				if (throwExceptions)
				{
					throw;
				}
				result = string.Empty;
			}
			catch (MServPermanentException)
			{
				if (throwExceptions)
				{
					throw;
				}
				result = string.Empty;
			}
			catch (InvalidOperationException)
			{
				if (throwExceptions)
				{
					throw;
				}
				result = string.Empty;
			}
			catch (TransientException)
			{
				if (throwExceptions)
				{
					throw;
				}
				result = string.Empty;
			}
			return result;
		}

		// Token: 0x06003072 RID: 12402 RVA: 0x000C2EE8 File Offset: 0x000C10E8
		internal static string GetRedirectServer(string redirectFormat, string address, int currentSiteId, int startRange, int endRange)
		{
			return EdgeSyncMservConnector.GetRedirectServer(redirectFormat, address, currentSiteId, startRange, endRange, false, false);
		}

		// Token: 0x06003073 RID: 12403 RVA: 0x000C2EF8 File Offset: 0x000C10F8
		internal static string GetRedirectServer(string redirectFormat, string address, int currentSiteId, int startRange, int endRange, bool overrideCurrentSiteCheck, bool throwExceptions)
		{
			string result;
			try
			{
				IGlobalDirectorySession globalSession = DirectorySessionFactory.GetGlobalSession(redirectFormat);
				result = globalSession.GetRedirectServer(address);
			}
			catch (MServTransientException)
			{
				if (throwExceptions)
				{
					throw;
				}
				result = string.Empty;
			}
			catch (MServPermanentException)
			{
				if (throwExceptions)
				{
					throw;
				}
				result = string.Empty;
			}
			catch (InvalidOperationException)
			{
				if (throwExceptions)
				{
					throw;
				}
				result = string.Empty;
			}
			catch (TransientException)
			{
				if (throwExceptions)
				{
					throw;
				}
				result = string.Empty;
			}
			return result;
		}

		// Token: 0x06003074 RID: 12404 RVA: 0x000C2F88 File Offset: 0x000C1188
		internal static string GetRedirectServerFromPartnerId(string redirectFormat, int partnerId, int currentSiteId, int startRange, int endRange, Trace tracer)
		{
			return EdgeSyncMservConnector.GetRedirectServerFromPartnerId(redirectFormat, partnerId, currentSiteId, startRange, endRange, tracer, false, false);
		}

		// Token: 0x06003075 RID: 12405 RVA: 0x000C2F9C File Offset: 0x000C119C
		internal static string GetRedirectServerFromPartnerId(string redirectFormat, int partnerId, int currentSiteId, int startRange, int endRange, Trace tracer, bool overrideCurrentSiteCheck, bool throwExceptions)
		{
			if (startRange > endRange)
			{
				throw new InvalidOperationException(string.Format("startRange: {0} greater than endRange: {1}", startRange, endRange));
			}
			if (string.IsNullOrEmpty(redirectFormat))
			{
				return string.Empty;
			}
			if (partnerId == -1 || partnerId < startRange || partnerId > endRange)
			{
				if (partnerId == -1)
				{
					string message = string.Format("The partner id {0} is invalid", partnerId);
					EdgeSyncMservConnector.TraceError(tracer, message);
					if (throwExceptions)
					{
						throw new InvalidPartnerIdException(message);
					}
				}
				else
				{
					string message2 = string.Format("The partner id {0} is out of range", partnerId);
					EdgeSyncMservConnector.TraceError(tracer, message2);
					if (throwExceptions)
					{
						throw new InvalidOperationException(message2);
					}
				}
				return string.Empty;
			}
			if (partnerId == currentSiteId && !overrideCurrentSiteCheck)
			{
				EdgeSyncMservConnector.TraceDebug(tracer, string.Format("The partner id {0} is the same as the current site id", partnerId));
				return string.Empty;
			}
			return string.Format(CultureInfo.InvariantCulture, redirectFormat, new object[]
			{
				partnerId
			});
		}

		// Token: 0x06003076 RID: 12406 RVA: 0x000C3079 File Offset: 0x000C1279
		private static void TraceDebug(Trace tracer, string message)
		{
			if (tracer != null)
			{
				tracer.TraceDebug(0L, message);
			}
		}

		// Token: 0x06003077 RID: 12407 RVA: 0x000C3087 File Offset: 0x000C1287
		private static void TraceError(Trace tracer, string message)
		{
			if (tracer != null)
			{
				tracer.TraceError(0L, message);
			}
		}

		// Token: 0x0400208A RID: 8330
		internal const string MostDerivedClass = "msExchEdgeSyncMservConnector";

		// Token: 0x0400208B RID: 8331
		private const char CommonNameSeperatorChar = '/';

		// Token: 0x0400208C RID: 8332
		public const string ArchiveAddressDomainSuffix = "@archive.exchangelabs.com";

		// Token: 0x0400208D RID: 8333
		private static readonly EdgeSyncMservConnectorSchema schema = ObjectSchema.GetInstance<EdgeSyncMservConnectorSchema>();
	}
}
