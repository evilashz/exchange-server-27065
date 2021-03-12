using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.UM.UMCommon;
using Microsoft.Exchange.UM.UMCommon.Exceptions;
using Microsoft.Exchange.UM.UMCore.Exceptions;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x02000138 RID: 312
	internal class EnterpriseSipPeerManager : SipPeerManager
	{
		// Token: 0x1700021D RID: 541
		// (get) Token: 0x060008AC RID: 2220 RVA: 0x00025800 File Offset: 0x00023A00
		internal override UMSipPeer AVAuthenticationService
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700021E RID: 542
		// (get) Token: 0x060008AD RID: 2221 RVA: 0x00025803 File Offset: 0x00023A03
		internal override UMSipPeer SIPAccessService
		{
			get
			{
				return this.sipAccessService;
			}
		}

		// Token: 0x1700021F RID: 543
		// (get) Token: 0x060008AE RID: 2222 RVA: 0x0002580D File Offset: 0x00023A0D
		internal override bool IsHeuristicBasedSIPAccessService
		{
			get
			{
				return this.heuristicBasedSIPAccessService;
			}
		}

		// Token: 0x17000220 RID: 544
		// (get) Token: 0x060008AF RID: 2223 RVA: 0x00025817 File Offset: 0x00023A17
		internal override UMSipPeer SBCService
		{
			get
			{
				return null;
			}
		}

		// Token: 0x060008B0 RID: 2224 RVA: 0x0002581C File Offset: 0x00023A1C
		internal EnterpriseSipPeerManager(bool generateNoSipPeerWarning, UMADSettings adSettings) : base(adSettings)
		{
			this.authorizationCache = new Dictionary<string, List<UMIPGateway>>(StringComparer.OrdinalIgnoreCase);
			this.peerListCache = new List<UMSipPeer>();
			this.generateNoSipPeerWarning = generateNoSipPeerWarning;
			this.PopulateSipPeers(Strings.CacheRefreshInitialization);
			ADNotificationsManager.Instance.UMIPGateway.ConfigChanged += this.ADUpdateCallback;
			ADNotificationsManager.Instance.UMDialPlan.ConfigChanged += this.ADUpdateCallback;
			ADNotificationsManager.Instance.UMHuntGroup.ConfigChanged += this.ADUpdateCallback;
			ADNotificationsManager.Instance.CallRouterSettings.ConfigChanged += this.ADUpdateCallback;
		}

		// Token: 0x060008B1 RID: 2225 RVA: 0x000258EC File Offset: 0x00023AEC
		internal override List<UMSipPeer> GetSecuredSipPeers()
		{
			if (UmServiceGlobals.StartupMode != UMStartupMode.TCP)
			{
				List<UMSipPeer> list = this.peerListCache.FindAll((UMSipPeer p) => p.UseMutualTLS);
				ExAssert.RetailAssert(list.Count > 0, "UM Service Mode is {0} but there is no secured peer", new object[]
				{
					UmServiceGlobals.StartupMode
				});
				return list;
			}
			return new List<UMSipPeer>(0);
		}

		// Token: 0x060008B2 RID: 2226 RVA: 0x0002595B File Offset: 0x00023B5B
		internal override bool IsAccessProxy(string matchedFqdn)
		{
			return false;
		}

		// Token: 0x060008B3 RID: 2227 RVA: 0x0002595E File Offset: 0x00023B5E
		internal override bool IsAccessProxyWithOrgTestHook(string matchedFqdn, string orgParameter)
		{
			return false;
		}

		// Token: 0x060008B4 RID: 2228 RVA: 0x00025961 File Offset: 0x00023B61
		internal override bool IsSIPSBC(string matchedFqdn)
		{
			return false;
		}

		// Token: 0x060008B5 RID: 2229 RVA: 0x00025964 File Offset: 0x00023B64
		internal override UMIPGateway GetUMIPGateway(string address, Guid tenantGuid)
		{
			ValidateArgument.NotNullOrEmpty(address, "fqdn");
			UMIPGateway result = null;
			List<UMIPGateway> list;
			if (this.authorizationCache.TryGetValue(address, out list))
			{
				result = list[0];
				base.DebugTrace("GetUMIPGateway::Found the Gateway with address {0}", new object[]
				{
					address
				});
				if (list.Count > 1)
				{
					this.LogDuplicatePeersEvent(list);
				}
			}
			else
			{
				base.DebugTrace("GetUMIPGateway::Could not find the Gateway with address {0}", new object[]
				{
					address
				});
			}
			return result;
		}

		// Token: 0x060008B6 RID: 2230 RVA: 0x000259D9 File Offset: 0x00023BD9
		protected override UMIPGateway FindGatewayForTlsEndPoint(X509Certificate2 certificate, ReadOnlyCollection<PlatformSignalingHeader> sipHeaders, PlatformSipUri requestUri, Guid tenantGuid, ref string matchedFqdn)
		{
			base.DebugTrace("EnterpriseSipPeerManager::FindGatewayForTlsEndPoint.", new object[0]);
			return this.GetUMIPGateway(matchedFqdn, tenantGuid);
		}

		// Token: 0x060008B7 RID: 2231 RVA: 0x000259F8 File Offset: 0x00023BF8
		protected override void LogUnknownTcpGateway(string remoteEndPoint)
		{
			UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_CallRejected, null, new object[]
			{
				(CallId.Id == null) ? "<null>" : CallId.Id,
				Strings.CallFromInvalidGateway(remoteEndPoint).ToString()
			});
		}

		// Token: 0x060008B8 RID: 2232 RVA: 0x00025A4B File Offset: 0x00023C4B
		protected override void LogUnknownTlsGateway(X509Certificate2 certificate, string matchedFqdn, string remoteEndPoint)
		{
			this.LogUnknownTcpGateway(matchedFqdn);
		}

		// Token: 0x060008B9 RID: 2233 RVA: 0x00025A54 File Offset: 0x00023C54
		protected override void OnLocalServerUpdated()
		{
			base.DebugTrace("SipPeerManager::OnLocalServerUpdated", new object[0]);
			lock (this.syncLock)
			{
				this.PopulateSipPeers(Strings.CacheRefreshInitialization);
			}
		}

		// Token: 0x060008BA RID: 2234 RVA: 0x00025AAC File Offset: 0x00023CAC
		protected virtual void ReadUMIPGateways(out List<UMIPGateway> secureGateways, out List<UMIPGateway> unsecureGateways)
		{
			Utils.GetIPGatewayList(base.ServiceADSettings.Fqdn, false, false, out secureGateways, out unsecureGateways);
		}

		// Token: 0x060008BB RID: 2235 RVA: 0x00025AC2 File Offset: 0x00023CC2
		protected virtual UMSipPeer ReadUMPoolSipPeer()
		{
			return base.GetSipPeerFromUMCertificateSubjectName();
		}

		// Token: 0x060008BC RID: 2236 RVA: 0x00025ACC File Offset: 0x00023CCC
		private static void AddGatewayToAuthCache(Dictionary<string, List<UMIPGateway>> authCache, string key, UMIPGatewaySipPeer gateway)
		{
			List<UMIPGateway> list = null;
			if (!authCache.TryGetValue(key, out list))
			{
				list = new List<UMIPGateway>(1);
				authCache[key] = list;
			}
			list.Add(gateway.ToUMIPGateway(OrganizationId.ForestWideOrgId));
		}

		// Token: 0x060008BD RID: 2237 RVA: 0x00025B08 File Offset: 0x00023D08
		private void PopulateSipPeers(LocalizedString reason)
		{
			List<UMSipPeer> securePeers;
			List<UMSipPeer> list;
			this.GetAllowedPeers(out securePeers, out list);
			list = this.ValidateTcpSipPeerList(list);
			this.GenerateAndUpdateCache(securePeers, list);
			UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_SipPeerCacheRefreshed, null, new object[]
			{
				base.ProcessName,
				this.peerListCache.ToString(Environment.NewLine),
				reason
			});
			this.DetermineSIPAccessService(securePeers);
			base.RaisePeerListChangeEvent();
		}

		// Token: 0x060008BE RID: 2238 RVA: 0x00025B7C File Offset: 0x00023D7C
		private void GetAllowedPeers(out List<UMSipPeer> secureUMPeers, out List<UMSipPeer> unsecureUMPeers)
		{
			secureUMPeers = new List<UMSipPeer>();
			unsecureUMPeers = new List<UMSipPeer>();
			List<UMIPGateway> list;
			List<UMIPGateway> list2;
			this.ReadUMIPGateways(out list, out list2);
			Hashtable hashtable = new Hashtable();
			foreach (UMIPGateway gateway in list2)
			{
				this.AddSipPeerIfNotPresentAlready(hashtable, unsecureUMPeers, new UMIPGatewaySipPeer(gateway, false));
			}
			hashtable.Clear();
			foreach (UMIPGateway gateway2 in list)
			{
				this.AddSipPeerIfNotPresentAlready(hashtable, secureUMPeers, new UMIPGatewaySipPeer(gateway2, true));
			}
			UMSipPeer umsipPeer = this.ReadUMPoolSipPeer();
			if (umsipPeer != null)
			{
				this.AddSipPeerIfNotPresentAlready(hashtable, secureUMPeers, umsipPeer);
			}
			if (this.generateNoSipPeerWarning)
			{
				this.CheckIfNoPeers(secureUMPeers, unsecureUMPeers);
			}
			if (UmServiceGlobals.StartupMode != UMStartupMode.TCP)
			{
				this.TrustCafeServersIfNecessary(hashtable, secureUMPeers);
				string fqdn = base.ServiceADSettings.Fqdn.ToString().ToLowerInvariant();
				this.AddSipPeerIfNotPresentAlready(hashtable, secureUMPeers, UMSipPeer.CreateForTlsAuth(fqdn));
				if (UmServiceGlobals.StartupMode == UMStartupMode.TLS)
				{
					unsecureUMPeers.Clear();
					return;
				}
			}
			else
			{
				secureUMPeers.Clear();
			}
		}

		// Token: 0x060008BF RID: 2239 RVA: 0x00025CB8 File Offset: 0x00023EB8
		private void AddSipPeerIfNotPresentAlready(Hashtable duplicateDetector, List<UMSipPeer> list, UMSipPeer peer)
		{
			string key = peer.Address.ToString().ToLowerInvariant();
			if (!duplicateDetector.Contains(key))
			{
				list.Add(peer);
				duplicateDetector.Add(key, peer);
			}
		}

		// Token: 0x060008C0 RID: 2240 RVA: 0x00025CF0 File Offset: 0x00023EF0
		private void TrustCafeServersIfNecessary(Hashtable duplicateDetector, List<UMSipPeer> secureUMPeers)
		{
			if (base.ServiceADSettings is UMServiceADSettings)
			{
				ADTopologyLookup adtopologyLookup = ADTopologyLookup.CreateLocalResourceForestLookup();
				IEnumerable<Server> allCafeServers = adtopologyLookup.GetAllCafeServers();
				if (allCafeServers != null)
				{
					foreach (Server server in allCafeServers)
					{
						this.AddSipPeerIfNotPresentAlready(duplicateDetector, secureUMPeers, UMSipPeer.CreateForTlsAuth(server.Fqdn));
					}
				}
			}
		}

		// Token: 0x060008C1 RID: 2241 RVA: 0x00025D64 File Offset: 0x00023F64
		private void GenerateAndUpdateCache(List<UMSipPeer> securePeers, List<UMSipPeer> unsecurePeers)
		{
			Dictionary<string, List<UMIPGateway>> authCache = new Dictionary<string, List<UMIPGateway>>(StringComparer.OrdinalIgnoreCase);
			List<UMSipPeer> list = new List<UMSipPeer>(securePeers.Count + unsecurePeers.Count);
			foreach (UMSipPeer umsipPeer in securePeers)
			{
				UMIPGatewaySipPeer umipgatewaySipPeer = umsipPeer as UMIPGatewaySipPeer;
				if (umipgatewaySipPeer != null)
				{
					string text = umsipPeer.Address.ToString();
					EnterpriseSipPeerManager.AddGatewayToAuthCache(authCache, text, umipgatewaySipPeer);
					base.DebugTrace("EnterpriseSipPeerManager::GenerateAndUpdateCache: add {0} (TLS)", new object[]
					{
						text
					});
				}
			}
			foreach (UMSipPeer umsipPeer2 in unsecurePeers)
			{
				UMIPGatewaySipPeer umipgatewaySipPeer2 = umsipPeer2 as UMIPGatewaySipPeer;
				if (umipgatewaySipPeer2 != null)
				{
					foreach (IPAddress ipaddress in umsipPeer2.ResolvedIPAddress)
					{
						string text2 = ipaddress.ToString();
						EnterpriseSipPeerManager.AddGatewayToAuthCache(authCache, text2, umipgatewaySipPeer2);
						base.DebugTrace("EnterpriseSipPeerManager::GenerateAndUpdateCache: add {0} (TCP)", new object[]
						{
							text2
						});
					}
				}
			}
			this.LogAllDuplicatePeers(authCache);
			list.AddRange(securePeers);
			list.AddRange(unsecurePeers);
			this.peerListCache = list;
			this.authorizationCache = authCache;
		}

		// Token: 0x060008C2 RID: 2242 RVA: 0x00025EE0 File Offset: 0x000240E0
		private void LogAllDuplicatePeers(Dictionary<string, List<UMIPGateway>> authCache)
		{
			foreach (List<UMIPGateway> list in authCache.Values)
			{
				if (list.Count > 1)
				{
					this.LogDuplicatePeersEvent(list);
				}
			}
		}

		// Token: 0x060008C3 RID: 2243 RVA: 0x00025F3C File Offset: 0x0002413C
		private void LogDuplicatePeersEvent(List<UMIPGateway> gatewayList)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine();
			foreach (UMIPGateway umipgateway in gatewayList)
			{
				stringBuilder.AppendLine(umipgateway.Name);
			}
			string text = CommonUtil.ToEventLogString(stringBuilder);
			UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_DuplicatePeersFound, text, new object[]
			{
				text
			});
		}

		// Token: 0x060008C4 RID: 2244 RVA: 0x00025FC4 File Offset: 0x000241C4
		private void DetermineSIPAccessService(List<UMSipPeer> securePeers)
		{
			if (!base.ServiceADSettings.IsSIPAccessServiceNeeded)
			{
				return;
			}
			try
			{
				if (base.ServiceADSettings.SIPAccessService != null)
				{
					this.sipAccessService = new UMSipPeer(new UMSmartHost(base.ServiceADSettings.SIPAccessService.Hostname.HostnameString), base.ServiceADSettings.SIPAccessService.Port, true, true, IPAddressFamily.Any);
					this.heuristicBasedSIPAccessService = false;
				}
				else
				{
					this.HeuristicallyDetermineSIPAccessService(securePeers);
				}
			}
			catch (Exception ex)
			{
				base.DebugTrace("EnterpriseSipPeerManager::DetermineSIPAccessService: Exception: {0}", new object[]
				{
					ex
				});
				if (!base.HandleADException(ex))
				{
					throw;
				}
			}
		}

		// Token: 0x060008C5 RID: 2245 RVA: 0x00026070 File Offset: 0x00024270
		private void HeuristicallyDetermineSIPAccessService(List<UMSipPeer> securePeers)
		{
			List<UMSipPeer> list = new List<UMSipPeer>();
			List<UMSipPeer> list2 = new List<UMSipPeer>();
			foreach (UMSipPeer umsipPeer in securePeers)
			{
				if (umsipPeer.IsOcs && umsipPeer.ToUMIPGateway(OrganizationId.ForestWideOrgId).Status == GatewayStatus.Enabled)
				{
					list2.Add(umsipPeer);
					if (umsipPeer.AllowOutboundCalls)
					{
						list.Add(umsipPeer);
					}
				}
			}
			if (list.Count > 0)
			{
				this.sipAccessService = this.FindLongestSuffixMatchingPeer(list);
				return;
			}
			if (list2.Count > 0)
			{
				this.sipAccessService = this.FindLongestSuffixMatchingPeer(list2);
			}
		}

		// Token: 0x060008C6 RID: 2246 RVA: 0x00026124 File Offset: 0x00024324
		private UMSipPeer FindLongestSuffixMatchingPeer(List<UMSipPeer> listOfpeers)
		{
			int index = -1;
			int num = -1;
			for (int i = 0; i < listOfpeers.Count; i++)
			{
				int num2 = Utils.DetermineLongestSuffixMatch(listOfpeers[i].Address.ToString(), base.ServiceADSettings.Fqdn);
				if (num2 > num)
				{
					num = num2;
					index = i;
				}
			}
			this.heuristicBasedSIPAccessService = true;
			return listOfpeers[index];
		}

		// Token: 0x060008C7 RID: 2247 RVA: 0x00026180 File Offset: 0x00024380
		private List<UMSipPeer> ValidateTcpSipPeerList(List<UMSipPeer> peerList)
		{
			List<UMSipPeer> list = new List<UMSipPeer>(peerList.Count);
			foreach (UMSipPeer umsipPeer in peerList)
			{
				if (this.ValidateSIPPeer(umsipPeer))
				{
					list.Add(umsipPeer);
				}
			}
			return list;
		}

		// Token: 0x060008C8 RID: 2248 RVA: 0x000261E4 File Offset: 0x000243E4
		private bool ValidateSIPPeer(UMSipPeer peer)
		{
			bool flag = false;
			ExAssert.RetailAssert(!peer.UseMutualTLS, "ValidateSIPPeer can only be called for TCP peers");
			if (!peer.Address.IsIPAddress)
			{
				IPHostEntry iphostEntry = null;
				string text = peer.Address.ToString();
				if (Utils.HasValidDNSRecord(text, out iphostEntry))
				{
					flag = true;
					if (iphostEntry.AddressList != null && iphostEntry.AddressList.Length > 0)
					{
						List<IPAddress> list = new List<IPAddress>();
						foreach (IPAddress item in iphostEntry.AddressList)
						{
							list.Add(item);
						}
						peer.ResolvedIPAddress = list;
					}
				}
				else
				{
					UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_InvalidPeerDNSHostName, text, new object[]
					{
						text,
						string.Empty
					});
				}
			}
			else
			{
				flag = true;
			}
			base.DebugTrace("SipPeerManager::ValidateSIPPeers: {0} Is valid:{1}", new object[]
			{
				peer,
				flag
			});
			return flag;
		}

		// Token: 0x060008C9 RID: 2249 RVA: 0x000262D0 File Offset: 0x000244D0
		private bool HasDuplicateAddresses(Dictionary<IPAddress, UMSipPeer> addressTable, UMSipPeer peer, out UMSipPeer duplicate)
		{
			duplicate = null;
			if (peer.ResolvedIPAddress != null)
			{
				foreach (IPAddress key in peer.ResolvedIPAddress)
				{
					if (addressTable.ContainsKey(key))
					{
						duplicate = addressTable[key];
						return true;
					}
				}
				foreach (IPAddress key2 in peer.ResolvedIPAddress)
				{
					addressTable[key2] = peer;
				}
				return false;
			}
			return false;
		}

		// Token: 0x060008CA RID: 2250 RVA: 0x00026384 File Offset: 0x00024584
		private void CheckIfNoPeers(List<UMSipPeer> securePeerList, List<UMSipPeer> unsecurePeerList)
		{
			switch (UMRecyclerConfig.UMStartupType)
			{
			case UMStartupMode.TCP:
				if (unsecurePeerList.Count == 0)
				{
					UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_WNoPeersFound, null, new object[]
					{
						Strings.UnSecured
					});
				}
				if (securePeerList.Count != 0)
				{
					UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_IncorrectPeers, null, new object[]
					{
						Strings.UnSecured,
						Strings.Secured,
						securePeerList.ToString(Environment.NewLine)
					});
					return;
				}
				break;
			case UMStartupMode.TLS:
				if (securePeerList.Count == 0)
				{
					UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_WNoPeersFound, null, new object[]
					{
						Strings.Secured
					});
				}
				if (unsecurePeerList.Count != 0)
				{
					UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_IncorrectPeers, null, new object[]
					{
						Strings.Secured,
						Strings.UnSecured,
						unsecurePeerList.ToString(Environment.NewLine)
					});
					return;
				}
				break;
			default:
				if (unsecurePeerList.Count == 0)
				{
					UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_WNoPeersFound, null, new object[]
					{
						Strings.UnSecured
					});
				}
				if (securePeerList.Count == 0)
				{
					UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_WNoPeersFound, null, new object[]
					{
						Strings.Secured
					});
				}
				break;
			}
		}

		// Token: 0x060008CB RID: 2251 RVA: 0x00026507 File Offset: 0x00024707
		private void ADUpdateCallback(ADNotificationEventArgs args)
		{
			if (args != null && args.Id != null)
			{
				this.RepopulateSipPeerList(args.Id, args.ChangeType);
				return;
			}
			base.DebugTrace("ADUpdateCallback : Ignore the AD callback because args or args.id is null.", new object[0]);
		}

		// Token: 0x060008CC RID: 2252 RVA: 0x00026538 File Offset: 0x00024738
		private void RepopulateSipPeerList(ADObjectId updatedObjectId, ADNotificationChangeType changeType)
		{
			base.DebugTrace("SipPeerManager::RepopulateSipPeerList", new object[0]);
			lock (this.syncLock)
			{
				if (changeType == ADNotificationChangeType.Delete)
				{
					this.PopulateSipPeers(Strings.CacheRefreshADDeleteNotification(updatedObjectId.Name.Replace("\n", "\\n")));
				}
				else
				{
					this.PopulateSipPeers(Strings.CacheRefreshADUpdateNotification(updatedObjectId.Name));
				}
			}
		}

		// Token: 0x040008B3 RID: 2227
		private volatile Dictionary<string, List<UMIPGateway>> authorizationCache;

		// Token: 0x040008B4 RID: 2228
		private bool generateNoSipPeerWarning;

		// Token: 0x040008B5 RID: 2229
		private volatile UMSipPeer sipAccessService;

		// Token: 0x040008B6 RID: 2230
		private volatile bool heuristicBasedSIPAccessService = true;

		// Token: 0x040008B7 RID: 2231
		private volatile List<UMSipPeer> peerListCache;

		// Token: 0x040008B8 RID: 2232
		private object syncLock = new object();
	}
}
