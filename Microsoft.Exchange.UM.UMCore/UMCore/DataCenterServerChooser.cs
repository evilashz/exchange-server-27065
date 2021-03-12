using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x0200010A RID: 266
	internal class DataCenterServerChooser : IRedirectTargetChooser
	{
		// Token: 0x06000758 RID: 1880 RVA: 0x0001DCC1 File Offset: 0x0001BEC1
		public DataCenterServerChooser(UMDialPlan dialPlan, bool isSecuredCall, UMRecipient userContext)
		{
			this.dialPlan = dialPlan;
			this.isSecuredCall = isSecuredCall;
			this.userContext = userContext;
		}

		// Token: 0x170001EC RID: 492
		// (get) Token: 0x06000759 RID: 1881 RVA: 0x0001DCDE File Offset: 0x0001BEDE
		public string SubscriberLogId
		{
			get
			{
				return this.userContext.MailAddress;
			}
		}

		// Token: 0x0600075A RID: 1882 RVA: 0x0001DCEC File Offset: 0x0001BEEC
		public bool GetTargetServer(out string fqdn, out int port)
		{
			fqdn = null;
			port = -1;
			CallIdTracer.TraceDebug(ExTraceGlobals.CallSessionTracer, this, "DataCenterServerChooser::GetTargetServer() Site = {0}", new object[]
			{
				this.userContext.MailboxServerSite.DistinguishedName
			});
			Server server = null;
			UMRecipient umrecipient = this.userContext;
			if (Utils.RunningInTestMode && umrecipient != null && !string.IsNullOrEmpty(umrecipient.ADRecipient.CustomAttribute2))
			{
				ITopologyConfigurationSession topologyConfigurationSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(ConsistencyMode.FullyConsistent, ADSessionSettings.FromRootOrgScopeSet(), 111, "GetTargetServer", "f:\\15.00.1497\\sources\\dev\\um\\src\\umcore\\DataCenterServerChooser.cs");
				server = topologyConfigurationSession.FindServerByFqdn(umrecipient.ADRecipient.CustomAttribute2);
			}
			else
			{
				InternalExchangeServer internalExchangeServer = DatacenterSiteBasedServerPicker.Instance.PickNextServer(this.userContext.MailboxServerSite);
				if (internalExchangeServer != null)
				{
					server = internalExchangeServer.Server;
				}
			}
			if (server == null)
			{
				return false;
			}
			fqdn = SipRoutingHelper.GetCrossSiteRedirectTargetFqdnAndPort(server, this.isSecuredCall, out port);
			return true;
		}

		// Token: 0x0600075B RID: 1883 RVA: 0x0001DDB8 File Offset: 0x0001BFB8
		public void HandleServerNotFound()
		{
			UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_UMServerNotFoundInSite, null, new object[]
			{
				this.SubscriberLogId,
				(this.dialPlan != null && this.dialPlan.Id != null) ? this.dialPlan.Id.ToString() : string.Empty,
				this.userContext.MailboxServerSite
			});
		}

		// Token: 0x0400082D RID: 2093
		private readonly bool isSecuredCall;

		// Token: 0x0400082E RID: 2094
		private UMDialPlan dialPlan;

		// Token: 0x0400082F RID: 2095
		private UMRecipient userContext;
	}
}
