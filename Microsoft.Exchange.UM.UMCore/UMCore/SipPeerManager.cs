using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.UM.TroubleshootingTool.Shared;
using Microsoft.Exchange.UM.UMCommon;
using Microsoft.Exchange.UM.UMCore.Exceptions;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x0200010B RID: 267
	internal abstract class SipPeerManager
	{
		// Token: 0x170001ED RID: 493
		// (get) Token: 0x0600075C RID: 1884 RVA: 0x0001DE24 File Offset: 0x0001C024
		// (set) Token: 0x0600075D RID: 1885 RVA: 0x0001DE2B File Offset: 0x0001C02B
		protected static SipPeerManager ManagerInstance { get; set; }

		// Token: 0x0600075E RID: 1886 RVA: 0x0001DE34 File Offset: 0x0001C034
		protected SipPeerManager(UMADSettings adSettings)
		{
			ExAssert.RetailAssert(adSettings != null, "UMADSettings is null");
			using (Process currentProcess = Process.GetCurrentProcess())
			{
				this.ProcessName = currentProcess.ProcessName.ToLower();
			}
			this.ServiceADSettings = adSettings;
			this.ServiceADSettings.SubscribeToADNotifications(new ADNotificationEvent(this.ADNotification_ServerConfigChanged));
		}

		// Token: 0x14000014 RID: 20
		// (add) Token: 0x0600075F RID: 1887 RVA: 0x0001DEAC File Offset: 0x0001C0AC
		// (remove) Token: 0x06000760 RID: 1888 RVA: 0x0001DEE4 File Offset: 0x0001C0E4
		internal event EventHandler SipPeerListChanged;

		// Token: 0x170001EE RID: 494
		// (get) Token: 0x06000761 RID: 1889 RVA: 0x0001DF19 File Offset: 0x0001C119
		internal static SipPeerManager Instance
		{
			get
			{
				ExAssert.RetailAssert(SipPeerManager.ManagerInstance != null, "SipPeerManager was not initialized");
				return SipPeerManager.ManagerInstance;
			}
		}

		// Token: 0x170001EF RID: 495
		// (get) Token: 0x06000762 RID: 1890 RVA: 0x0001DF35 File Offset: 0x0001C135
		protected virtual bool SkipCertPHeaderCheckforMonitoring
		{
			get
			{
				return AppConfig.Instance.Service.SkipCertPHeaderCheckforActiveMonitoring;
			}
		}

		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x06000763 RID: 1891
		internal abstract UMSipPeer AVAuthenticationService { get; }

		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x06000764 RID: 1892
		internal abstract UMSipPeer SIPAccessService { get; }

		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x06000765 RID: 1893
		internal abstract bool IsHeuristicBasedSIPAccessService { get; }

		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x06000766 RID: 1894
		internal abstract UMSipPeer SBCService { get; }

		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x06000767 RID: 1895 RVA: 0x0001DF46 File Offset: 0x0001C146
		// (set) Token: 0x06000768 RID: 1896 RVA: 0x0001DF4E File Offset: 0x0001C14E
		private protected UMADSettings ServiceADSettings { protected get; private set; }

		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x06000769 RID: 1897 RVA: 0x0001DF57 File Offset: 0x0001C157
		// (set) Token: 0x0600076A RID: 1898 RVA: 0x0001DF5F File Offset: 0x0001C15F
		private protected string ProcessName { protected get; private set; }

		// Token: 0x0600076B RID: 1899 RVA: 0x0001DF68 File Offset: 0x0001C168
		internal static void Initialize(bool generateNoSipPeerWarning, UMADSettings adSettings)
		{
			if (SipPeerManager.ManagerInstance == null)
			{
				lock (SipPeerManager.syncLock)
				{
					if (SipPeerManager.ManagerInstance == null)
					{
						if (CommonConstants.UseDataCenterCallRouting)
						{
							SipPeerManager.ManagerInstance = new DatacenterSipPeerManager(adSettings);
						}
						else
						{
							SipPeerManager.ManagerInstance = new EnterpriseSipPeerManager(generateNoSipPeerWarning, adSettings);
						}
					}
				}
			}
		}

		// Token: 0x0600076C RID: 1900
		internal abstract List<UMSipPeer> GetSecuredSipPeers();

		// Token: 0x0600076D RID: 1901 RVA: 0x0001DFD0 File Offset: 0x0001C1D0
		internal UMSipPeer GetSipPeerFromUMCertificateSubjectName()
		{
			UMSipPeer result = null;
			if (!string.IsNullOrEmpty(this.ServiceADSettings.UMCertificateThumbprint))
			{
				X509Certificate2 x509Certificate = CertificateUtils.FindCertByThumbprint(this.ServiceADSettings.UMCertificateThumbprint);
				if (x509Certificate != null)
				{
					string subjectFqdn = CertificateUtils.GetSubjectFqdn(x509Certificate);
					try
					{
						result = new UMSipPeer(new UMSmartHost(subjectFqdn), 10000, false, true, IPAddressFamily.Any);
					}
					catch (ArgumentException ex)
					{
						string text = CommonUtil.ToEventLogString(ex);
						UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_SipPeerCertificateSubjectName, null, new object[]
						{
							subjectFqdn,
							text
						});
						this.DebugTrace("SipPeerManager::GetSipPeerFromUMCertificateSubjectName UM Local Sip Option Probe will fail {0}", new object[]
						{
							ex
						});
					}
				}
			}
			return result;
		}

		// Token: 0x0600076E RID: 1902 RVA: 0x0001E080 File Offset: 0x0001C280
		internal virtual UMIPGateway GetUMIPGateway(string fqdn, Guid tenantGuid)
		{
			ValidateArgument.NotNullOrEmpty(fqdn, "fqdn");
			return this.GetUMIPGateway(new string[]
			{
				fqdn
			}, tenantGuid, false);
		}

		// Token: 0x0600076F RID: 1903
		internal abstract bool IsAccessProxy(string matchedFqdn);

		// Token: 0x06000770 RID: 1904
		internal abstract bool IsAccessProxyWithOrgTestHook(string matchedFqdn, string orgParameter);

		// Token: 0x06000771 RID: 1905
		internal abstract bool IsSIPSBC(string matchedFqdn);

		// Token: 0x06000772 RID: 1906 RVA: 0x0001E0AC File Offset: 0x0001C2AC
		internal bool IsAuthorized(X509Certificate certificate, IPAddress connectingGW, ReadOnlyCollection<PlatformSignalingHeader> sipHeaders, PlatformSipUri requestUri, Guid tenantGuid, ref string matchedFqdn, out UMIPGateway gateway)
		{
			ValidateArgument.NotNull(connectingGW, "connectingGW");
			bool flag = false;
			gateway = null;
			if (certificate == null)
			{
				flag = this.DoAuthorizationForTcpEndPoint(connectingGW, sipHeaders, tenantGuid, out gateway);
			}
			else
			{
				try
				{
					flag = this.DoAuthorizationForTlsEndPoint(new X509Certificate2(certificate), connectingGW, sipHeaders, requestUri, tenantGuid, ref matchedFqdn, out gateway);
				}
				catch (CryptographicException ex)
				{
					this.DebugTrace("SipPeerManager::IsAuthorized Got CryptographicException exception {0}", new object[]
					{
						ex.Message
					});
					string text = string.Empty;
					try
					{
						text = certificate.GetCertHashString();
					}
					catch
					{
					}
					UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_InvalidClientCertificate, null, new object[]
					{
						text,
						CommonUtil.ToEventLogString(ex.ToString())
					});
				}
			}
			if (gateway != null && gateway.Status != GatewayStatus.Enabled)
			{
				UmGlobals.ExEvent.LogEvent(gateway.OrganizationId, UMEventLogConstants.Tuple_CallRejectedSinceGatewayDisabled, gateway.Name, (CallId.Id == null) ? string.Empty : CallId.Id, gateway.Address);
				flag = false;
			}
			this.DebugTrace("SipPeerManager::IsAuthorized Call authorized = {0}", new object[]
			{
				flag
			});
			return flag;
		}

		// Token: 0x06000773 RID: 1907 RVA: 0x0001E1E0 File Offset: 0x0001C3E0
		internal bool IsLocalDiagnosticCall(IPAddress connectingGW, IEnumerable<PlatformSignalingHeader> headers)
		{
			ValidateArgument.NotNull(connectingGW, "connectingGW");
			bool flag = false;
			bool flag2 = false;
			bool result = false;
			if (headers != null)
			{
				this.DebugTrace("SipPeerManager::IsLocalDiagnosticCall: Looking for the diagnostic and user agent header", new object[0]);
				foreach (PlatformSignalingHeader platformSignalingHeader in headers)
				{
					if (string.Equals(platformSignalingHeader.Name, "msexum-connectivitytest", StringComparison.OrdinalIgnoreCase))
					{
						if (!string.Equals(platformSignalingHeader.Value, "local", StringComparison.OrdinalIgnoreCase))
						{
							this.DebugTrace("SipPeerManager:IsLocalDiagnosticCall:: Diagnostic header value '{0}' is not valid", new object[]
							{
								(platformSignalingHeader.Value == null) ? "<null>" : platformSignalingHeader.Value
							});
							break;
						}
						flag = true;
					}
					else if (string.Equals(platformSignalingHeader.Name, "user-agent", StringComparison.OrdinalIgnoreCase))
					{
						if (platformSignalingHeader.Value == null || platformSignalingHeader.Value.IndexOf("MSExchangeUM-Diagnostics") <= 0)
						{
							this.DebugTrace("SipPeerManager:IsLocalDiagnosticCall:: user header value '{0}' is not valid", new object[]
							{
								(platformSignalingHeader.Value == null) ? "<null>" : platformSignalingHeader.Value
							});
							break;
						}
						flag2 = true;
					}
				}
			}
			if (flag && flag2)
			{
				if (!this.IsLocalCall(connectingGW))
				{
					this.DebugTrace("SipPeerManager:IsLocalDiagnosticCall:: Local diagnostic call attempted from a remote machine {0}", new object[]
					{
						connectingGW.ToString()
					});
					throw CallRejectedException.Create(Strings.DiagnosticCallFromRemoteHost(connectingGW.ToString()), CallEndingReason.TransientError, null, new object[0]);
				}
				result = true;
				this.DebugTrace("SipPeerManager::IsLocalDiagnosticCall:: Local diagnostic call detected", new object[0]);
			}
			else
			{
				this.DebugTrace("SipPeerManager::IsLocalDiagnosticCall: Not a diagnositc call", new object[0]);
			}
			return result;
		}

		// Token: 0x06000774 RID: 1908 RVA: 0x0001E388 File Offset: 0x0001C588
		public bool IsActiveMonitoringCall(IEnumerable<PlatformSignalingHeader> diagnosticsHeaders)
		{
			bool flag = false;
			foreach (PlatformSignalingHeader platformSignalingHeader in diagnosticsHeaders)
			{
				if (string.Equals(platformSignalingHeader.Name, "user-agent", StringComparison.OrdinalIgnoreCase))
				{
					string value = platformSignalingHeader.Value;
					if (value != null && value.IndexOf("ActiveMonitoringClient", StringComparison.OrdinalIgnoreCase) > 0)
					{
						flag = true;
						break;
					}
					break;
				}
			}
			this.DebugTrace("SipPeerManager::IsActiveMonitoringCall returning = {0}", new object[]
			{
				flag
			});
			return flag;
		}

		// Token: 0x06000775 RID: 1909 RVA: 0x0001E41C File Offset: 0x0001C61C
		protected void RaisePeerListChangeEvent()
		{
			if (this.SipPeerListChanged != null)
			{
				this.DebugTrace("SipPeerManager::RaisePeerListChangeEvent", new object[0]);
				this.SipPeerListChanged(this, EventArgs.Empty);
			}
		}

		// Token: 0x06000776 RID: 1910 RVA: 0x0001E448 File Offset: 0x0001C648
		protected void DebugTrace(string message, params object[] args)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.SipPeerManagerTracer, this.GetHashCode(), message, args);
		}

		// Token: 0x06000777 RID: 1911 RVA: 0x0001E464 File Offset: 0x0001C664
		protected bool HandleADException(Exception ex)
		{
			bool result = true;
			if (ex is ADTransientException)
			{
				UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_ADTransientError, null, new object[]
				{
					(CallId.Id == null) ? "<null>" : CallId.Id,
					CommonUtil.ToEventLogString(ex.ToString())
				});
			}
			else if (ex is DataSourceOperationException)
			{
				UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_ADPermanentError, null, new object[]
				{
					(CallId.Id == null) ? "<null>" : CallId.Id,
					CommonUtil.ToEventLogString(ex.ToString())
				});
			}
			else if (ex is DataValidationException)
			{
				UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_ADDataError, null, new object[]
				{
					(CallId.Id == null) ? "<null>" : CallId.Id,
					CommonUtil.ToEventLogString(ex.ToString())
				});
			}
			else
			{
				result = false;
			}
			return result;
		}

		// Token: 0x06000778 RID: 1912 RVA: 0x0001E550 File Offset: 0x0001C750
		protected UMIPGateway GetUMIPGateway(IList<string> fqdns, Guid tenantGuid, bool isMonitoring)
		{
			UMIPGateway result = null;
			try
			{
				UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_FindUMIPGatewayInAD, null, new object[]
				{
					fqdns.ToString(Environment.NewLine)
				});
				IADSystemConfigurationLookup adlookup = this.GetADLookup(tenantGuid);
				IEnumerable<UMIPGateway> allIPGateways = adlookup.GetAllIPGateways();
				if (isMonitoring && this.SkipCertPHeaderCheckforMonitoring)
				{
					result = this.GetMonitoringGateway(allIPGateways);
				}
				else
				{
					result = this.GetIPGatewayFromAddress(allIPGateways, fqdns);
				}
			}
			catch (Exception ex)
			{
				this.DebugTrace("SipPeerManager::GetUMIPGateway An exception occured {0}", new object[]
				{
					ex.Message
				});
				if (!this.HandleADException(ex))
				{
					throw;
				}
			}
			return result;
		}

		// Token: 0x06000779 RID: 1913 RVA: 0x0001E5F8 File Offset: 0x0001C7F8
		protected virtual IADSystemConfigurationLookup GetADLookup(Guid tenantGuid)
		{
			return ADSystemConfigurationLookupFactory.CreateFromTenantGuid(tenantGuid);
		}

		// Token: 0x0600077A RID: 1914 RVA: 0x0001E618 File Offset: 0x0001C818
		protected UMIPGateway GetMonitoringGateway(IEnumerable<UMIPGateway> gateways)
		{
			IEnumerable<UMIPGateway> source = from gw in gateways
			where gw.Address.ToString().EndsWith("o365.exchangemon.net", StringComparison.InvariantCultureIgnoreCase)
			select gw;
			return source.FirstOrDefault<UMIPGateway>();
		}

		// Token: 0x0600077B RID: 1915 RVA: 0x0001E674 File Offset: 0x0001C874
		protected UMIPGateway GetIPGatewayFromAddress(IEnumerable<UMIPGateway> gateways, IList<string> fqdns)
		{
			if (fqdns == null || fqdns.Count == 0)
			{
				throw new ArgumentException("Null or empty", "fqdns");
			}
			IEnumerable<UMIPGateway> source = from gw in gateways
			where fqdns.Contains(gw.Address.ToString(), StringComparer.InvariantCultureIgnoreCase)
			select gw;
			return source.FirstOrDefault<UMIPGateway>();
		}

		// Token: 0x0600077C RID: 1916
		protected abstract UMIPGateway FindGatewayForTlsEndPoint(X509Certificate2 certificate, ReadOnlyCollection<PlatformSignalingHeader> sipHeaders, PlatformSipUri requestUri, Guid tenantGuid, ref string matchedFqdn);

		// Token: 0x0600077D RID: 1917
		protected abstract void LogUnknownTcpGateway(string remoteEndPoint);

		// Token: 0x0600077E RID: 1918
		protected abstract void LogUnknownTlsGateway(X509Certificate2 certificate, string matchedFqdn, string remoteEndPoint);

		// Token: 0x0600077F RID: 1919
		protected abstract void OnLocalServerUpdated();

		// Token: 0x06000780 RID: 1920 RVA: 0x0001E6CC File Offset: 0x0001C8CC
		private bool DoAuthorizationForTlsEndPoint(X509Certificate2 certificate, IPAddress connectingGW, ReadOnlyCollection<PlatformSignalingHeader> sipHeaders, PlatformSipUri requestUri, Guid tenantGuid, ref string matchedFqdn, out UMIPGateway gateway)
		{
			ValidateArgument.NotNull(certificate, "certificate");
			this.DebugTrace("SipPeerManager::DoAuthorizationForTlsEndPoint.", new object[0]);
			bool result = false;
			gateway = null;
			if (string.IsNullOrEmpty(matchedFqdn))
			{
				this.DebugTrace("MatchedFqdn is null or emtpy for a Tls connection. The inbound call is being rejected", new object[0]);
			}
			else
			{
				gateway = this.FindGatewayForTlsEndPoint(certificate, sipHeaders, requestUri, tenantGuid, ref matchedFqdn);
				if (gateway != null)
				{
					this.DebugTrace("SipPeerManager::DoAuthorizationForTlsEndPoint. Found the gateway {0}", new object[]
					{
						gateway.Id
					});
					result = true;
				}
				else
				{
					this.LogUnknownTlsGateway(certificate, matchedFqdn, connectingGW.ToString());
				}
			}
			return result;
		}

		// Token: 0x06000781 RID: 1921 RVA: 0x0001E764 File Offset: 0x0001C964
		private bool DoAuthorizationForTcpEndPoint(IPAddress connectingGW, ReadOnlyCollection<PlatformSignalingHeader> sipHeaders, Guid tenantGuid, out UMIPGateway gateway)
		{
			this.DebugTrace("SipPeerManager::DoAuthorizationForTcpEndPoint", new object[0]);
			bool result = false;
			gateway = this.GetUMIPGateway(connectingGW.ToString(), tenantGuid);
			if (gateway != null)
			{
				this.DebugTrace("SipPeerManager::DoAuthorizationForTcpEndPoint. Found the gateway {0}", new object[]
				{
					gateway.Id
				});
				result = true;
			}
			else
			{
				this.LogUnknownTcpGateway(connectingGW.ToString());
			}
			return result;
		}

		// Token: 0x06000782 RID: 1922 RVA: 0x0001E7E0 File Offset: 0x0001C9E0
		private bool IsLocalCall(IPAddress connectingGateway)
		{
			return ComputerInformation.GetLocalIPAddresses().Exists((IPAddress ip) => ip.Equals(connectingGateway));
		}

		// Token: 0x06000783 RID: 1923 RVA: 0x0001E810 File Offset: 0x0001CA10
		private void ADNotification_ServerConfigChanged(ADNotificationEventArgs args)
		{
			if (args != null && args.Id != null && args.ChangeType == ADNotificationChangeType.ModifyOrAdd && this.ServiceADSettings.Id.Equals(args.Id))
			{
				this.ServiceADSettings = this.ServiceADSettings.RefreshFromAD();
				this.OnLocalServerUpdated();
			}
		}

		// Token: 0x04000830 RID: 2096
		private static object syncLock = new object();
	}
}
