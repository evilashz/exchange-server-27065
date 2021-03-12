using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Xml;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage.RightsManagement;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Rpc;
using Microsoft.Exchange.Rpc.OfflineRms;
using Microsoft.Exchange.Security.RightsManagement;
using Microsoft.RightsManagementServices.Core;

namespace Microsoft.Exchange.Data.Storage.OfflineRms
{
	// Token: 0x02000ACE RID: 2766
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class RpcClientWrapper
	{
		// Token: 0x0600648F RID: 25743 RVA: 0x001AA68C File Offset: 0x001A888C
		private RpcClientWrapper()
		{
		}

		// Token: 0x06006490 RID: 25744 RVA: 0x001AA6F0 File Offset: 0x001A88F0
		private static void InitializeIfNeeded()
		{
			if (!RpcClientWrapper.instance.initialized)
			{
				lock (RpcClientWrapper.instance.initializeLockObject)
				{
					if (!RpcClientWrapper.instance.initialized)
					{
						RpcClientWrapper.instance.Initialize();
						RpcClientWrapper.instance.initialized = true;
					}
				}
			}
			bool flag2 = DateTime.UtcNow - RpcClientWrapper.instance.topologyLastUpdated > RpcClientWrapper.topologyExpiryTimeSpan;
			if (flag2)
			{
				Exception ex = null;
				RpcClientWrapper.instance.TryLoadTopologies(out ex);
			}
		}

		// Token: 0x06006491 RID: 25745 RVA: 0x001AA78C File Offset: 0x001A898C
		public static ActiveCryptoModeRpcResult[] GetTenantActiveCryptoMode(RmsClientManagerContext clientContext)
		{
			if (clientContext == null)
			{
				throw new ArgumentNullException("clientContext");
			}
			RpcClientWrapper.InitializeIfNeeded();
			string randomRpcTargetServerName = RpcClientWrapper.instance.GetRandomRpcTargetServerName();
			byte[] data = null;
			ServerManagerLog.LogEvent(ServerManagerLog.Subcomponent.RpcClientWrapper, ServerManagerLog.EventType.Entry, clientContext, string.Format("OfflineRmsRpcClient.GetTenantActiveCryptoMode against RPC server {0}", randomRpcTargetServerName));
			try
			{
				using (OfflineRmsRpcClient offlineRmsRpcClient = new OfflineRmsRpcClient(randomRpcTargetServerName))
				{
					data = offlineRmsRpcClient.GetTenantActiveCryptoMode(1, new GetTenantActiveCryptoModeRpcParameters(clientContext).Serialize());
				}
			}
			catch (RpcException ex)
			{
				ServerManagerLog.LogEvent(ServerManagerLog.Subcomponent.RpcClientWrapper, ServerManagerLog.EventType.Error, clientContext, string.Format("OfflineRmsRpcClient.GetTenantActiveCryptoMode against RPC server {0} failed with RPC Exception {1}", randomRpcTargetServerName, ServerManagerLog.GetExceptionLogString(ex, ServerManagerLog.ExceptionLogOption.IncludeStack | ServerManagerLog.ExceptionLogOption.IncludeInnerException)));
				throw new RightsManagementServerException(ServerStrings.RpcClientException("GetTenantActiveCryptoMode", randomRpcTargetServerName), ex, false);
			}
			GetTenantActiveCryptoModeRpcResults getTenantActiveCryptoModeRpcResults = new GetTenantActiveCryptoModeRpcResults(data);
			if (getTenantActiveCryptoModeRpcResults.OverallRpcResult.Status == OverallRpcStatus.Success)
			{
				return getTenantActiveCryptoModeRpcResults.ActiveCryptoModeRpcResults;
			}
			string serializedString = ErrorResult.GetSerializedString(getTenantActiveCryptoModeRpcResults.OverallRpcResult.ErrorResults);
			ServerManagerLog.LogEvent(ServerManagerLog.Subcomponent.RpcClientWrapper, ServerManagerLog.EventType.Error, clientContext, string.Format("OfflineRmsRpcClient.GetTenantActiveCryptoMode against RPC server {0} failed with WellKnownErrorCode {1}, and with Exception {2}", randomRpcTargetServerName, getTenantActiveCryptoModeRpcResults.OverallRpcResult.WellKnownErrorCode, serializedString));
			throw new RightsManagementServerException(ServerStrings.FailedToRpcAcquireUseLicenses(clientContext.OrgId.ToString(), serializedString, randomRpcTargetServerName), getTenantActiveCryptoModeRpcResults.OverallRpcResult.WellKnownErrorCode, getTenantActiveCryptoModeRpcResults.OverallRpcResult.Status == OverallRpcStatus.PermanentFailure);
		}

		// Token: 0x06006492 RID: 25746 RVA: 0x001AA8C8 File Offset: 0x001A8AC8
		public static UseLicenseRpcResult[] AcquireUseLicenses(RmsClientManagerContext clientContext, XmlNode[] rightsAccountCertificate, XmlNode[] issuanceLicense, LicenseeIdentity[] licenseeIdentities)
		{
			if (clientContext == null)
			{
				throw new ArgumentNullException("clientContext");
			}
			if (rightsAccountCertificate == null)
			{
				throw new ArgumentNullException("rightsAccountCertificate");
			}
			if (issuanceLicense == null || issuanceLicense.Length < 1)
			{
				throw new ArgumentNullException("issuanceLicense");
			}
			if (licenseeIdentities == null || licenseeIdentities.Length < 1)
			{
				throw new ArgumentNullException("licenseeIdentities");
			}
			RpcClientWrapper.InitializeIfNeeded();
			string randomRpcTargetServerName = RpcClientWrapper.instance.GetRandomRpcTargetServerName();
			byte[] data = null;
			ServerManagerLog.LogEvent(ServerManagerLog.Subcomponent.RpcClientWrapper, ServerManagerLog.EventType.Entry, clientContext, string.Format("OfflineRmsRpcClient.AcquireUseLicenses against RPC server {0}", randomRpcTargetServerName));
			try
			{
				using (OfflineRmsRpcClient offlineRmsRpcClient = new OfflineRmsRpcClient(randomRpcTargetServerName))
				{
					data = offlineRmsRpcClient.AcquireUseLicenses(1, new AcquireUseLicensesRpcParameters(clientContext, rightsAccountCertificate, issuanceLicense, licenseeIdentities).Serialize());
				}
			}
			catch (RpcException ex)
			{
				ServerManagerLog.LogEvent(ServerManagerLog.Subcomponent.RpcClientWrapper, ServerManagerLog.EventType.Error, clientContext, string.Format("OfflineRmsRpcClient.AcquireUseLicenses against RPC server {0} failed with RPC Exception {1}", randomRpcTargetServerName, ServerManagerLog.GetExceptionLogString(ex, ServerManagerLog.ExceptionLogOption.IncludeStack | ServerManagerLog.ExceptionLogOption.IncludeInnerException)));
				throw new RightsManagementServerException(ServerStrings.RpcClientException("AcquireUseLicenses", randomRpcTargetServerName), ex, false);
			}
			AcquireUseLicensesRpcResults acquireUseLicensesRpcResults = new AcquireUseLicensesRpcResults(data);
			if (acquireUseLicensesRpcResults.OverallRpcResult.Status == OverallRpcStatus.Success)
			{
				return acquireUseLicensesRpcResults.UseLicenseRpcResults;
			}
			string serializedString = ErrorResult.GetSerializedString(acquireUseLicensesRpcResults.OverallRpcResult.ErrorResults);
			ServerManagerLog.LogEvent(ServerManagerLog.Subcomponent.RpcClientWrapper, ServerManagerLog.EventType.Error, clientContext, string.Format("OfflineRmsRpcClient.AcquireUseLicenses against RPC server {0} failed with WellKnownErrorCode {1}, and with Exception {2}", randomRpcTargetServerName, acquireUseLicensesRpcResults.OverallRpcResult.WellKnownErrorCode, serializedString));
			throw new RightsManagementServerException(ServerStrings.FailedToRpcAcquireUseLicenses(clientContext.OrgId.ToString(), serializedString, randomRpcTargetServerName), acquireUseLicensesRpcResults.OverallRpcResult.WellKnownErrorCode, acquireUseLicensesRpcResults.OverallRpcResult.Status == OverallRpcStatus.PermanentFailure);
		}

		// Token: 0x06006493 RID: 25747 RVA: 0x001AAA40 File Offset: 0x001A8C40
		public static void AcquireTenantLicenses(RmsClientManagerContext clientContext, XmlNode[] machineCertificateChain, string identity, out XmlNode[] racXml, out XmlNode[] clcXml)
		{
			if (clientContext == null)
			{
				throw new ArgumentNullException("clientContext");
			}
			if (machineCertificateChain == null || machineCertificateChain.Length < 1)
			{
				throw new ArgumentNullException("machineCertificateChain");
			}
			if (string.IsNullOrEmpty(identity))
			{
				throw new ArgumentNullException("identity");
			}
			racXml = null;
			clcXml = null;
			RpcClientWrapper.InitializeIfNeeded();
			string randomRpcTargetServerName = RpcClientWrapper.instance.GetRandomRpcTargetServerName();
			byte[] data = null;
			ServerManagerLog.LogEvent(ServerManagerLog.Subcomponent.RpcClientWrapper, ServerManagerLog.EventType.Entry, clientContext, string.Format("OfflineRmsRpcClient.AcquireTenantLicenses against RPC server {0}", randomRpcTargetServerName));
			try
			{
				using (OfflineRmsRpcClient offlineRmsRpcClient = new OfflineRmsRpcClient(randomRpcTargetServerName))
				{
					data = offlineRmsRpcClient.AcquireTenantLicenses(1, new AcquireTenantLicensesRpcParameters(clientContext, identity, machineCertificateChain).Serialize());
				}
			}
			catch (RpcException ex)
			{
				ServerManagerLog.LogEvent(ServerManagerLog.Subcomponent.RpcClientWrapper, ServerManagerLog.EventType.Error, clientContext, string.Format("OfflineRmsRpcClient.AcquireTenantLicenses against RPC server {0} failed with RPC Exception {1}", randomRpcTargetServerName, ServerManagerLog.GetExceptionLogString(ex, ServerManagerLog.ExceptionLogOption.IncludeStack | ServerManagerLog.ExceptionLogOption.IncludeInnerException)));
				throw new RightsManagementServerException(ServerStrings.RpcClientException("AcquireTenantLicenses", randomRpcTargetServerName), ex, false);
			}
			AcquireTenantLicensesRpcResults acquireTenantLicensesRpcResults = new AcquireTenantLicensesRpcResults(data);
			if (acquireTenantLicensesRpcResults.OverallRpcResult.Status == OverallRpcStatus.Success)
			{
				racXml = acquireTenantLicensesRpcResults.RacXml;
				clcXml = acquireTenantLicensesRpcResults.ClcXml;
				return;
			}
			string serializedString = ErrorResult.GetSerializedString(acquireTenantLicensesRpcResults.OverallRpcResult.ErrorResults);
			ServerManagerLog.LogEvent(ServerManagerLog.Subcomponent.RpcClientWrapper, ServerManagerLog.EventType.Error, clientContext, string.Format("OfflineRmsRpcClient.AcquireTenantLicenses against RPC server {0} failed with WellKnownErrorCode {1} and with Exception {2}", randomRpcTargetServerName, acquireTenantLicensesRpcResults.OverallRpcResult.WellKnownErrorCode, serializedString));
			throw new RightsManagementServerException(ServerStrings.FailedToRpcAcquireRacAndClc(clientContext.OrgId.ToString(), serializedString, randomRpcTargetServerName), acquireTenantLicensesRpcResults.OverallRpcResult.WellKnownErrorCode, acquireTenantLicensesRpcResults.OverallRpcResult.Status == OverallRpcStatus.PermanentFailure);
		}

		// Token: 0x06006494 RID: 25748 RVA: 0x001AABB8 File Offset: 0x001A8DB8
		private void Initialize()
		{
			Exception innerException = null;
			if (!this.TryLoadTopologies(out innerException))
			{
				throw new RightsManagementServerException(ServerStrings.RpcClientWrapperFailedToLoadTopology, innerException, false);
			}
		}

		// Token: 0x06006495 RID: 25749 RVA: 0x001AABE0 File Offset: 0x001A8DE0
		private string GetRandomRpcTargetServerName()
		{
			if ((this.localServer.CurrentServerRole & ServerRole.HubTransport) == ServerRole.HubTransport)
			{
				return this.localServer.Name;
			}
			if (this.localSiteBridgeheadServers == null || this.localSiteBridgeheadServers.Count == 0)
			{
				throw new RightsManagementServerException(ServerStrings.FailedToFindAvailableHubs, false);
			}
			int index = this.random.Next(0, this.localSiteBridgeheadServers.Count - 1);
			return this.localSiteBridgeheadServers[index].Name;
		}

		// Token: 0x06006496 RID: 25750 RVA: 0x001AACC4 File Offset: 0x001A8EC4
		private bool TryLoadTopologies(out Exception e)
		{
			bool result;
			try
			{
				e = null;
				if (Interlocked.Increment(ref this.loadTopologyCount) == 1)
				{
					ADOperationResult adoperationResult;
					if (this.localServer == null)
					{
						adoperationResult = ADNotificationAdapter.TryRunADOperation(delegate()
						{
							this.localServer = this.rootOrgConfigSession.FindLocalServer();
						}, 3);
						if (!adoperationResult.Succeeded)
						{
							e = adoperationResult.Exception;
							ServerManagerLog.LogEvent(ServerManagerLog.Subcomponent.RpcClientWrapper, ServerManagerLog.EventType.Error, null, string.Format("Failed find local server with Exception {0}", ServerManagerLog.GetExceptionLogString(e, ServerManagerLog.ExceptionLogOption.IncludeStack | ServerManagerLog.ExceptionLogOption.IncludeInnerException)));
							return false;
						}
						if (this.localServer.ServerSite == null)
						{
							ServerManagerLog.LogEvent(ServerManagerLog.Subcomponent.RpcClientWrapper, ServerManagerLog.EventType.Error, null, "Local server doesn't have AD site");
							return false;
						}
						this.localSiteHubsFilter = new AndFilter(new QueryFilter[]
						{
							new BitMaskAndFilter(ServerSchema.CurrentServerRole, 32UL),
							new ComparisonFilter(ComparisonOperator.Equal, ServerSchema.ServerSite, this.localServer.ServerSite)
						});
					}
					List<Server> bridgeheadServers = new List<Server>();
					if (!ADNotificationAdapter.TryReadConfigurationPaged<Server>(() => this.rootOrgConfigSession.FindPaged<Server>(null, QueryScope.SubTree, this.localSiteHubsFilter, null, 0), delegate(Server server)
					{
						ServerVersion a = new ServerVersion(server.VersionNumber);
						if (ServerVersion.Compare(a, RpcClientWrapper.minRequiredRpcServerVersion) >= 0)
						{
							bridgeheadServers.Add(server);
						}
					}, out adoperationResult))
					{
						e = adoperationResult.Exception;
						ServerManagerLog.LogEvent(ServerManagerLog.Subcomponent.RpcClientWrapper, ServerManagerLog.EventType.Error, null, string.Format("Failed to load topology with exception {0}", ServerManagerLog.GetExceptionLogString(e, ServerManagerLog.ExceptionLogOption.IncludeStack | ServerManagerLog.ExceptionLogOption.IncludeInnerException)));
						result = false;
					}
					else
					{
						Interlocked.Exchange<List<Server>>(ref this.localSiteBridgeheadServers, bridgeheadServers);
						RpcClientWrapper.instance.topologyLastUpdated = DateTime.UtcNow;
						StringBuilder stringBuilder = new StringBuilder();
						foreach (Server server2 in bridgeheadServers)
						{
							stringBuilder.Append(server2.Name);
							stringBuilder.Append(",");
						}
						ServerManagerLog.LogEvent(ServerManagerLog.Subcomponent.RpcClientWrapper, ServerManagerLog.EventType.Success, null, string.Format("Sucessfully load topology with servers {0}", stringBuilder.ToString()));
						result = true;
					}
				}
				else
				{
					result = true;
				}
			}
			finally
			{
				Interlocked.Decrement(ref this.loadTopologyCount);
			}
			return result;
		}

		// Token: 0x0400393A RID: 14650
		private static readonly RpcClientWrapper instance = new RpcClientWrapper();

		// Token: 0x0400393B RID: 14651
		private static readonly TimeSpan topologyExpiryTimeSpan = TimeSpan.FromHours(1.0);

		// Token: 0x0400393C RID: 14652
		private static readonly ServerVersion minRequiredRpcServerVersion = new ServerVersion(14, 1, 114, 0);

		// Token: 0x0400393D RID: 14653
		private readonly object initializeLockObject = new object();

		// Token: 0x0400393E RID: 14654
		private readonly Random random = new Random();

		// Token: 0x0400393F RID: 14655
		private readonly ITopologyConfigurationSession rootOrgConfigSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 65, "rootOrgConfigSession", "f:\\15.00.1497\\sources\\dev\\data\\src\\storage\\OfflineRms\\RpcClientWrapper.cs");

		// Token: 0x04003940 RID: 14656
		private bool initialized;

		// Token: 0x04003941 RID: 14657
		private DateTime topologyLastUpdated = DateTime.MinValue;

		// Token: 0x04003942 RID: 14658
		private List<Server> localSiteBridgeheadServers = new List<Server>();

		// Token: 0x04003943 RID: 14659
		private volatile Server localServer;

		// Token: 0x04003944 RID: 14660
		private int loadTopologyCount;

		// Token: 0x04003945 RID: 14661
		private QueryFilter localSiteHubsFilter;
	}
}
