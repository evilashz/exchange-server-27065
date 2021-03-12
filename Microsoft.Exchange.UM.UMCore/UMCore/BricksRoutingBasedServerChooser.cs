using System;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.UM.TroubleshootingTool.Shared;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x02000247 RID: 583
	internal class BricksRoutingBasedServerChooser : IRedirectTargetChooser
	{
		// Token: 0x1700040C RID: 1036
		// (get) Token: 0x06001119 RID: 4377 RVA: 0x0004C9C3 File Offset: 0x0004ABC3
		// (set) Token: 0x0600111A RID: 4378 RVA: 0x0004C9CB File Offset: 0x0004ABCB
		public bool IsRedirectionNeeded { get; private set; }

		// Token: 0x0600111B RID: 4379 RVA: 0x0004C9D4 File Offset: 0x0004ABD4
		public BricksRoutingBasedServerChooser(IRoutingContext currentCallContext, UMRecipient userContext, CallType callType)
		{
			ExAssert.RetailAssert(callType == 3 || callType == 4, "Incorrect CallType");
			this.userContext = userContext;
			if (userContext.ADRecipient.RecipientType == RecipientType.UserMailbox)
			{
				this.InitializeRedirectionResults(currentCallContext, userContext, callType);
				Server server = LocalServer.GetServer();
				this.IsRedirectionNeeded = !string.Equals(Utils.TryGetRedirectTargetFqdnForServer(server), this.fqdn, StringComparison.OrdinalIgnoreCase);
			}
		}

		// Token: 0x1700040D RID: 1037
		// (get) Token: 0x0600111C RID: 4380 RVA: 0x0004CA42 File Offset: 0x0004AC42
		public string SubscriberLogId
		{
			get
			{
				return this.userContext.MailAddress;
			}
		}

		// Token: 0x0600111D RID: 4381 RVA: 0x0004CA4F File Offset: 0x0004AC4F
		public bool GetTargetServer(out string fqdn, out int port)
		{
			fqdn = this.fqdn;
			port = this.port;
			return true;
		}

		// Token: 0x0600111E RID: 4382 RVA: 0x0004CA62 File Offset: 0x0004AC62
		public void HandleServerNotFound()
		{
		}

		// Token: 0x0600111F RID: 4383 RVA: 0x0004CA64 File Offset: 0x0004AC64
		private void InitializeRedirectionResults(IRoutingContext currentCallContext, UMRecipient userContext, CallType callType)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.CallSessionTracer, this, "BricksRoutingBasedServerChooser::InitializeRedirectionResults", new object[0]);
			RedirectionTarget.ResultSet resultSet;
			if (callType == 4)
			{
				resultSet = RedirectionTarget.Instance.GetForCallAnsweringCall(userContext, currentCallContext);
			}
			else
			{
				resultSet = RedirectionTarget.Instance.GetForSubscriberAccessCall(userContext, currentCallContext);
			}
			this.fqdn = resultSet.Fqdn;
			this.port = resultSet.Port;
			CallIdTracer.TraceDebug(ExTraceGlobals.CallSessionTracer, this, "BricksRoutingBasedServerChooser::InitializeRedirectionResults() returning {0}:{1}", new object[]
			{
				this.fqdn,
				this.port
			});
		}

		// Token: 0x04000BB4 RID: 2996
		private UMRecipient userContext;

		// Token: 0x04000BB5 RID: 2997
		private string fqdn;

		// Token: 0x04000BB6 RID: 2998
		private int port = -1;
	}
}
