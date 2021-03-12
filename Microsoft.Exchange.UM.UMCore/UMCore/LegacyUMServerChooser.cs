using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x02000166 RID: 358
	internal class LegacyUMServerChooser : IRedirectTargetChooser
	{
		// Token: 0x06000A84 RID: 2692 RVA: 0x0002C9D1 File Offset: 0x0002ABD1
		internal LegacyUMServerChooser(CallContext callContext, UMRecipient userContext) : this(callContext.DialPlan, callContext.IsSecuredCall, userContext)
		{
		}

		// Token: 0x06000A85 RID: 2693 RVA: 0x0002C9E6 File Offset: 0x0002ABE6
		internal LegacyUMServerChooser(UMDialPlan dp, bool securedCall, UMRecipient userContext)
		{
			this.dialplan = dp;
			this.userContext = userContext;
			this.securedCall = securedCall;
			ExAssert.RetailAssert(userContext.VersionCompatibility != VersionEnum.Compatible, "LegacyUMServerChooser should only be used for Legacy users");
		}

		// Token: 0x170002A5 RID: 677
		// (get) Token: 0x06000A86 RID: 2694 RVA: 0x0002CA19 File Offset: 0x0002AC19
		public string SubscriberLogId
		{
			get
			{
				return this.userContext.MailAddress;
			}
		}

		// Token: 0x06000A87 RID: 2695 RVA: 0x0002CA28 File Offset: 0x0002AC28
		public bool GetTargetServer(out string fqdn, out int port)
		{
			fqdn = null;
			port = -1;
			CallIdTracer.TraceDebug(ExTraceGlobals.CallSessionTracer, this, "LegacyServerChooser::GetTargetServer()", new object[0]);
			LegacyUMServerPicker instance = LegacyUMServerPicker.GetInstance(this.userContext.VersionCompatibility);
			InternalExchangeServer internalExchangeServer = instance.PickNextServer(this.dialplan.Id);
			if (internalExchangeServer == null || internalExchangeServer.Server == null)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.CallSessionTracer, this, "LegacyServerChooser::GetTargetServer() Did not find a valid {0} server to redirect the call. The call will be rejected", new object[]
				{
					this.userContext.VersionCompatibility
				});
				return false;
			}
			fqdn = Utils.TryGetRedirectTargetFqdnForServer(internalExchangeServer.Server);
			UMServer umserver = new UMServer(internalExchangeServer.Server);
			if ((bool)Server.IsE14OrLaterGetter(umserver))
			{
				port = Utils.GetRedirectPort(umserver.SipTcpListeningPort, umserver.SipTlsListeningPort, this.securedCall);
			}
			else
			{
				port = Utils.GetRedirectPort(5060, 5061, this.securedCall);
			}
			CallIdTracer.TraceDebug(ExTraceGlobals.CallSessionTracer, this, "LegacyServerChooser::GetTargetServer() returning {0}:{1}", new object[]
			{
				fqdn,
				port
			});
			return true;
		}

		// Token: 0x06000A88 RID: 2696 RVA: 0x0002CB30 File Offset: 0x0002AD30
		public void HandleServerNotFound()
		{
			ADTopologyLookup adtopologyLookup = ADTopologyLookup.CreateLocalResourceForestLookup();
			IEnumerable<Server> enabledUMServersInDialPlan = adtopologyLookup.GetEnabledUMServersInDialPlan(this.userContext.VersionCompatibility, this.dialplan.Id);
			using (IEnumerator<Server> enumerator = enabledUMServersInDialPlan.GetEnumerator())
			{
				if (!enumerator.MoveNext())
				{
					UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_LegacyServerNotFoundInDialPlan, null, new object[]
					{
						this.SubscriberLogId,
						this.dialplan.DistinguishedName
					});
				}
				else
				{
					UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_LegacyServerNotRunningInDialPlan, null, new object[]
					{
						this.SubscriberLogId,
						this.dialplan.DistinguishedName
					});
				}
			}
		}

		// Token: 0x04000962 RID: 2402
		private readonly bool securedCall;

		// Token: 0x04000963 RID: 2403
		private UMDialPlan dialplan;

		// Token: 0x04000964 RID: 2404
		private UMRecipient userContext;
	}
}
