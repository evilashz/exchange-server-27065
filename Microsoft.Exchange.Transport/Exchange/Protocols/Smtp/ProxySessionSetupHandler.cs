using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.ApplicationLogic.Cafe;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Smtp;
using Microsoft.Exchange.Diagnostics.Components.Transport;
using Microsoft.Exchange.Transport;
using Microsoft.Exchange.Transport.Categorizer;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Protocols.Smtp
{
	// Token: 0x020004FB RID: 1275
	internal sealed class ProxySessionSetupHandler
	{
		// Token: 0x06003AB4 RID: 15028 RVA: 0x000F3E60 File Offset: 0x000F2060
		public ProxySessionSetupHandler(ISmtpInSession inSession, IEnhancedDns enhancedDns, ITransportConfiguration transportConfiguration) : this(inSession, enhancedDns, transportConfiguration, null, null, null, RiskLevel.Normal, 0, null, null)
		{
		}

		// Token: 0x06003AB5 RID: 15029 RVA: 0x000F3E80 File Offset: 0x000F2080
		public ProxySessionSetupHandler(ISmtpInSession inSession, IEnhancedDns enhancedDns, ITransportConfiguration transportConfiguration, IEnumerable<INextHopServer> outboundProxyDestinations, SmtpSendConnectorConfig outboundProxySendConnector, TlsSendConfiguration outboundProxyTlsSendConfiguration, RiskLevel outboundProxyRiskLevel, int outboundProxyOutboundIPPool, string outboundProxyNextHopDomain, string outboundProxySessionId)
		{
			if (inSession == null)
			{
				throw new ArgumentNullException("inSession");
			}
			this.inSession = inSession;
			this.enhancedDns = enhancedDns;
			this.transportConfiguration = transportConfiguration;
			this.outboundProxyDestinations = outboundProxyDestinations;
			this.outboundProxySendConnector = outboundProxySendConnector;
			this.outboundProxyTlsSendConfiguration = outboundProxyTlsSendConfiguration;
			this.outboundProxyRiskLevel = outboundProxyRiskLevel;
			this.outboundProxyOutboundIPPool = outboundProxyOutboundIPPool;
			this.outboundProxySessionId = outboundProxySessionId;
			this.outboundProxyNextHopDomain = outboundProxyNextHopDomain;
		}

		// Token: 0x170011F2 RID: 4594
		// (get) Token: 0x06003AB6 RID: 15030 RVA: 0x000F3EEE File Offset: 0x000F20EE
		public ISmtpInSession InSession
		{
			get
			{
				return this.inSession;
			}
		}

		// Token: 0x170011F3 RID: 4595
		// (get) Token: 0x06003AB7 RID: 15031 RVA: 0x000F3EF6 File Offset: 0x000F20F6
		private bool IsClientProxy
		{
			get
			{
				return this.outboundProxySendConnector == null;
			}
		}

		// Token: 0x06003AB8 RID: 15032 RVA: 0x000F3F01 File Offset: 0x000F2101
		public void ReleaseReferences()
		{
			this.smtpOutConnection = null;
		}

		// Token: 0x06003AB9 RID: 15033 RVA: 0x000F3F0C File Offset: 0x000F210C
		public void BeginSettingUpProxySession()
		{
			if (this.IsClientProxy)
			{
				bool flag = false;
				string text = null;
				SessionSetupFailureReason failureReason = SessionSetupFailureReason.None;
				try
				{
					this.authenticatedUser = this.inSession.AuthUserRecipient;
					AsyncBackEndLocator asyncBackEndLocator = new AsyncBackEndLocator();
					asyncBackEndLocator.BeginGetBackEndServerList(this.authenticatedUser, this.transportConfiguration.AppConfig.SmtpProxyConfiguration.MaxProxySetupAttempts, new AsyncCallback(this.OnBackendServerLookupComplete), asyncBackEndLocator);
				}
				catch (StoragePermanentException ex)
				{
					flag = true;
					failureReason = SessionSetupFailureReason.UserLookupFailure;
					text = ex.Message;
				}
				catch (ADTransientException ex2)
				{
					flag = false;
					failureReason = SessionSetupFailureReason.UserLookupFailure;
					text = ex2.Message;
				}
				catch (DataSourceOperationException ex3)
				{
					flag = true;
					failureReason = SessionSetupFailureReason.UserLookupFailure;
					text = ex3.Message;
				}
				catch (DataValidationException ex4)
				{
					flag = true;
					failureReason = SessionSetupFailureReason.UserLookupFailure;
					text = ex4.Message;
				}
				catch (BackEndLocatorException ex5)
				{
					flag = false;
					failureReason = SessionSetupFailureReason.BackEndLocatorFailure;
					text = ex5.Message;
				}
				if (!string.IsNullOrEmpty(text))
				{
					this.LogError(failureReason, text);
					this.HandleProxySessionDisconnection(ProxySessionSetupHandler.SmtpResponseFromFailure(flag, text), flag, failureReason);
					return;
				}
			}
			else
			{
				NextHopSolutionKey key = new NextHopSolutionKey(NextHopType.Empty, string.IsNullOrEmpty(this.outboundProxyNextHopDomain) ? "Outbound Proxy" : this.outboundProxyNextHopDomain, Guid.Empty);
				BlindProxyNextHopConnection connection = new BlindProxyNextHopConnection(this, key);
				string connectionContextString = string.Format("Outbound proxy from {0} with session id {1}. Proxied session id {2}", this.inSession.HelloDomain, this.outboundProxySessionId, this.InSession.SessionId.ToString("X16", NumberFormatInfo.InvariantInfo));
				this.smtpOutConnection = this.inSession.SmtpInServer.SmtpOutConnectionHandler.NewBlindProxyConnection(connection, this.outboundProxyDestinations, false, this.outboundProxySendConnector, this.outboundProxyTlsSendConfiguration, this.outboundProxyRiskLevel, this.outboundProxyOutboundIPPool, 0, this.InSession, connectionContextString);
				this.smtpOutConnection.ConnectToBlindProxyDestinations(this.outboundProxyDestinations, false, this.outboundProxySendConnector);
			}
		}

		// Token: 0x06003ABA RID: 15034 RVA: 0x000F40F4 File Offset: 0x000F22F4
		public void HandleProxySessionDisconnection(SmtpResponse response, bool permanentFailure, SessionSetupFailureReason failureReason)
		{
			this.InSession.DropBreadcrumb(SmtpInSessionBreadcrumbs.HandleProxySessionDisconnection);
			ExTraceGlobals.SmtpReceiveTracer.TraceError<long>((long)this.GetHashCode(), "ISmtpInSession(id={0}). All attempts to set up a proxy session failed or service was shut down.", this.InSession.ConnectionId);
			this.InSession.SmtpProxyPerfCounters.UpdateOnProxyFailure();
			SmtpResponse response2;
			if (this.IsClientProxy)
			{
				if (this.InSession.ClientProxyFailedDueToIncompatibleBackend)
				{
					response2 = SmtpResponse.UnableToProxyIntegratedAuthResponse;
				}
				else
				{
					response2 = InboundProxyLayer.GetEncodedProxyFailureResponse(failureReason);
				}
			}
			else if (permanentFailure)
			{
				response2 = new SmtpResponse(SmtpResponse.ProxySessionProtocolSetupPermanentFailure.StatusCode, SmtpResponse.ProxySessionProtocolSetupPermanentFailure.EnhancedStatusCode, new string[]
				{
					string.Format("{0} '{1}'", "Proxy session setup failed on Frontend with ", response)
				});
			}
			else
			{
				response2 = new SmtpResponse(SmtpResponse.ProxySessionProtocolSetupTransientFailure.StatusCode, SmtpResponse.ProxySessionProtocolSetupTransientFailure.EnhancedStatusCode, new string[]
				{
					string.Format("{0} '{1}'", "Proxy session setup failed on Frontend with ", response)
				});
			}
			this.InSession.HandleBlindProxySetupFailure(response2, this.IsClientProxy);
		}

		// Token: 0x06003ABB RID: 15035 RVA: 0x000F420C File Offset: 0x000F240C
		public void LogError(SessionSetupFailureReason failureReason, string errorString)
		{
			ExTraceGlobals.SmtpReceiveTracer.TraceError<long, string>((long)this.GetHashCode(), "ISmtpInSession(id={0}). Error encountered setting up proxy session {1}.", this.InSession.ConnectionId, errorString);
			this.InSession.SmtpProxyPerfCounters.UpdateOnProxyStepFailure(failureReason);
			this.InSession.LogSession.LogInformation(ProtocolLoggingLevel.Verbose, null, "Setting up {0} proxy session failed with error: {1}", new object[]
			{
				this.IsClientProxy ? "client" : "outbound",
				errorString
			});
		}

		// Token: 0x06003ABC RID: 15036 RVA: 0x000F4288 File Offset: 0x000F2488
		private void OnBackendServerLookupComplete(IAsyncResult asyncResult)
		{
			IList<BackEndServer> list = null;
			string errorMessage = null;
			AsyncBackEndLocator asyncBackEndLocator = (AsyncBackEndLocator)asyncResult.AsyncState;
			SessionSetupFailureReason failureReason = SessionSetupFailureReason.None;
			try
			{
				list = asyncBackEndLocator.EndGetBackEndServerList(asyncResult);
			}
			catch (ADTransientException ex)
			{
				failureReason = SessionSetupFailureReason.UserLookupFailure;
				errorMessage = ex.Message;
			}
			catch (DataSourceOperationException ex2)
			{
				failureReason = SessionSetupFailureReason.UserLookupFailure;
				errorMessage = ex2.Message;
			}
			catch (DataValidationException ex3)
			{
				failureReason = SessionSetupFailureReason.UserLookupFailure;
				errorMessage = ex3.Message;
			}
			catch (BackEndLocatorException ex4)
			{
				failureReason = SessionSetupFailureReason.BackEndLocatorFailure;
				errorMessage = ex4.Message;
			}
			IList<INextHopServer> list2 = new List<INextHopServer>();
			bool flag = false;
			if (list != null)
			{
				this.ParseAndVerifyPrimaryBackendServers(list, list2, ref errorMessage, out flag);
				if (flag)
				{
					failureReason = SessionSetupFailureReason.ProtocolError;
				}
			}
			bool primaryBackendLookupFailed = !list2.Any<INextHopServer>() && !flag;
			this.FindBackupBackendServersIfRequired(list2);
			this.HandleFinalBackendServerList(list2, errorMessage, flag, primaryBackendLookupFailed, failureReason);
		}

		// Token: 0x06003ABD RID: 15037 RVA: 0x000F4368 File Offset: 0x000F2568
		private void ParseAndVerifyPrimaryBackendServers(IEnumerable<BackEndServer> backendServerList, IList<INextHopServer> backendServersFinalList, ref string errorMessage, out bool incompatibleBackendVersion)
		{
			incompatibleBackendVersion = false;
			foreach (BackEndServer backEndServer in backendServerList)
			{
				if (backEndServer.Version < Server.E15MinVersion)
				{
					incompatibleBackendVersion = true;
					break;
				}
				RoutingHost item;
				if (RoutingHost.TryParse(backEndServer.Fqdn, out item))
				{
					backendServersFinalList.Add(item);
				}
				else
				{
					ExTraceGlobals.SmtpReceiveTracer.TraceError<string>(0L, "{0} returned as fqdn of backend server but it could not be parsed.", backEndServer.Fqdn);
					errorMessage = backEndServer.Fqdn + "returned as fqdn of backend server but it could not be parsed.";
				}
			}
			if (!this.transportConfiguration.AppConfig.SmtpProxyConfiguration.PreferMailboxMountedServer)
			{
				RoutingUtils.ShuffleList<INextHopServer>(backendServersFinalList);
			}
		}

		// Token: 0x06003ABE RID: 15038 RVA: 0x000F4420 File Offset: 0x000F2620
		private void FindBackupBackendServersIfRequired(IList<INextHopServer> backendServersFinalList)
		{
			IEnumerable<INextHopServer> enumerable;
			if (VariantConfiguration.InvariantNoFlightingSnapshot.Transport.SelectHubServersForClientProxy.Enabled && backendServersFinalList.Count < this.transportConfiguration.AppConfig.SmtpProxyConfiguration.MaxProxySetupAttempts && this.InSession.SmtpInServer.ProxyHubSelector.TrySelectHubServersForClientProxy(this.authenticatedUser, out enumerable))
			{
				int num = this.transportConfiguration.AppConfig.SmtpProxyConfiguration.MaxProxySetupAttempts - backendServersFinalList.Count;
				HashSet<string> hashSet = new HashSet<string>(StringComparer.InvariantCultureIgnoreCase);
				foreach (INextHopServer nextHopServer in backendServersFinalList)
				{
					hashSet.Add(nextHopServer.Fqdn);
				}
				foreach (INextHopServer nextHopServer2 in enumerable)
				{
					if (!nextHopServer2.IsIPAddress && !hashSet.Contains(nextHopServer2.Fqdn))
					{
						backendServersFinalList.Add(nextHopServer2);
						num--;
					}
					if (num == 0)
					{
						break;
					}
				}
			}
		}

		// Token: 0x06003ABF RID: 15039 RVA: 0x000F4558 File Offset: 0x000F2758
		private void HandleFinalBackendServerList(ICollection<INextHopServer> backendServersFinalList, string errorMessage, bool incompatibleBackendVersion, bool primaryBackendLookupFailed, SessionSetupFailureReason failureReason)
		{
			if (backendServersFinalList == null || !backendServersFinalList.Any<INextHopServer>())
			{
				if (failureReason == SessionSetupFailureReason.None)
				{
					failureReason = SessionSetupFailureReason.BackEndLocatorFailure;
				}
				string text;
				if (incompatibleBackendVersion)
				{
					text = "the user mailbox is on an incompatible backend and no compatible backends were found";
				}
				else
				{
					text = (string.IsNullOrEmpty(errorMessage) ? "an unknown error was encountered looking up a backend for the user" : errorMessage);
				}
				this.LogError(failureReason, text);
				this.HandleProxySessionDisconnection(new SmtpResponse("450", "4.7.0", new string[]
				{
					"Proxy session setup failed on Frontend because" + text
				}), false, failureReason);
				return;
			}
			if (primaryBackendLookupFailed)
			{
				this.InSession.LogSession.LogInformation(ProtocolLoggingLevel.Verbose, null, "Client proxy will be attempted to a backup backend since primary backend lookup for user failed with {0}", new object[]
				{
					string.IsNullOrEmpty(errorMessage) ? "unknown error" : errorMessage
				});
			}
			BlindProxyNextHopConnection connection = new BlindProxyNextHopConnection(this, ProxySessionSetupHandler.ClientProxyNextHopSolutionKey);
			string connectionContextString = string.Format("Client proxy session for {0}. Proxied session id {1}", Util.RedactUserName(this.InSession.ProxyUserName), this.InSession.SessionId.ToString("X16", NumberFormatInfo.InvariantInfo));
			this.smtpOutConnection = this.inSession.SmtpInServer.SmtpOutConnectionHandler.NewBlindProxyConnection(connection, backendServersFinalList, true, this.enhancedDns.ClientProxyConnector, null, RiskLevel.Normal, 0, 0, this.InSession, connectionContextString);
			this.smtpOutConnection.ConnectToBlindProxyDestinations(backendServersFinalList, true, this.enhancedDns.ClientProxyConnector);
		}

		// Token: 0x06003AC0 RID: 15040 RVA: 0x000F4698 File Offset: 0x000F2898
		private static SmtpResponse SmtpResponseFromFailure(bool isPermanentFailure, string errorMessage)
		{
			string text = string.Format("Proxy session setup failed on Frontend due to backend lookup error: {0}", Util.EnsureAscii(errorMessage));
			if (!isPermanentFailure)
			{
				return new SmtpResponse("450", "4.7.0", new string[]
				{
					text
				});
			}
			return new SmtpResponse("550", "5.7.0", new string[]
			{
				text
			});
		}

		// Token: 0x04001D86 RID: 7558
		public static readonly NextHopSolutionKey ClientProxyNextHopSolutionKey = new NextHopSolutionKey(NextHopType.Empty, "Client Proxy", Guid.Empty);

		// Token: 0x04001D87 RID: 7559
		private readonly string outboundProxyNextHopDomain;

		// Token: 0x04001D88 RID: 7560
		private readonly string outboundProxySessionId;

		// Token: 0x04001D89 RID: 7561
		private readonly ISmtpInSession inSession;

		// Token: 0x04001D8A RID: 7562
		private readonly IEnhancedDns enhancedDns;

		// Token: 0x04001D8B RID: 7563
		private readonly ITransportConfiguration transportConfiguration;

		// Token: 0x04001D8C RID: 7564
		private readonly IEnumerable<INextHopServer> outboundProxyDestinations;

		// Token: 0x04001D8D RID: 7565
		private readonly SmtpSendConnectorConfig outboundProxySendConnector;

		// Token: 0x04001D8E RID: 7566
		private readonly TlsSendConfiguration outboundProxyTlsSendConfiguration;

		// Token: 0x04001D8F RID: 7567
		private readonly RiskLevel outboundProxyRiskLevel;

		// Token: 0x04001D90 RID: 7568
		private readonly int outboundProxyOutboundIPPool;

		// Token: 0x04001D91 RID: 7569
		private SmtpOutConnection smtpOutConnection;

		// Token: 0x04001D92 RID: 7570
		private MiniRecipient authenticatedUser;
	}
}
