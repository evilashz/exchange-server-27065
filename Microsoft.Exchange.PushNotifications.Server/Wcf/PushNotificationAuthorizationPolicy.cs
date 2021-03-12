using System;
using System.Collections.Generic;
using System.IdentityModel.Claims;
using System.IdentityModel.Policy;
using System.Security.Principal;
using System.ServiceModel;
using System.ServiceModel.Security;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.PushNotifications;
using Microsoft.Exchange.Security;

namespace Microsoft.Exchange.PushNotifications.Server.Wcf
{
	// Token: 0x0200002C RID: 44
	public class PushNotificationAuthorizationPolicy : IAuthorizationPolicy, IAuthorizationComponent
	{
		// Token: 0x17000034 RID: 52
		// (get) Token: 0x0600010C RID: 268 RVA: 0x00004763 File Offset: 0x00002963
		public string Id
		{
			get
			{
				return PushNotificationAuthorizationPolicy.PolicyId;
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x0600010D RID: 269 RVA: 0x0000476A File Offset: 0x0000296A
		public ClaimSet Issuer
		{
			get
			{
				return ClaimSet.System;
			}
		}

		// Token: 0x0600010E RID: 270 RVA: 0x00004774 File Offset: 0x00002974
		public bool Evaluate(EvaluationContext evaluationContext, ref object state)
		{
			IIdentity clientIdentity = this.GetClientIdentity(evaluationContext);
			IPrincipal principal;
			if (clientIdentity != null)
			{
				principal = new ServicePrincipal(clientIdentity, ExTraceGlobals.PushNotificationServiceTracer);
			}
			else
			{
				principal = OperationContext.Current.GetPrincipal();
			}
			if (principal == null)
			{
				throw new SecurityAccessDeniedException();
			}
			evaluationContext.Properties["Principal"] = principal;
			return true;
		}

		// Token: 0x0600010F RID: 271 RVA: 0x000047C4 File Offset: 0x000029C4
		private IIdentity GetClientIdentity(EvaluationContext evaluationContext)
		{
			object obj;
			if (!evaluationContext.Properties.TryGetValue("Identities", out obj))
			{
				return null;
			}
			IList<IIdentity> list = obj as IList<IIdentity>;
			if (list == null || list.Count <= 0)
			{
				return null;
			}
			if (list.Count > 1 && ExTraceGlobals.PushNotificationServiceTracer.IsTraceEnabled(TraceType.WarningTrace))
			{
				ExTraceGlobals.PushNotificationServiceTracer.TraceWarning<string, string>((long)this.GetHashCode(), "Request has multiple identities. Identity {0} will be used. Other identities {1}", list[0].Name, string.Join<IIdentity>(",", list));
			}
			return list[0];
		}

		// Token: 0x04000063 RID: 99
		private static readonly string PolicyId = typeof(PushNotificationAuthorizationPolicy).FullName;
	}
}
