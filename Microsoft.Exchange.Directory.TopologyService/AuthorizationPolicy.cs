using System;
using System.Collections.Generic;
using System.IdentityModel.Claims;
using System.IdentityModel.Policy;
using System.Security.Principal;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Directory.TopologyService;

namespace Microsoft.Exchange.Directory.TopologyService
{
	// Token: 0x02000009 RID: 9
	internal class AuthorizationPolicy : IAuthorizationPolicy, IAuthorizationComponent
	{
		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000050 RID: 80 RVA: 0x00003691 File Offset: 0x00001891
		public ClaimSet Issuer
		{
			get
			{
				return ClaimSet.System;
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000051 RID: 81 RVA: 0x00003698 File Offset: 0x00001898
		public string Id
		{
			get
			{
				return AuthorizationPolicy.PolicyId;
			}
		}

		// Token: 0x06000052 RID: 82 RVA: 0x000036A0 File Offset: 0x000018A0
		public bool Evaluate(EvaluationContext evaluationContext, ref object state)
		{
			IIdentity clientIdentity = this.GetClientIdentity(evaluationContext);
			if (clientIdentity == null)
			{
				throw new ServiceAccessDeniedException();
			}
			evaluationContext.Properties["Principal"] = new ServicePrincipal(clientIdentity);
			return true;
		}

		// Token: 0x06000053 RID: 83 RVA: 0x000036D8 File Offset: 0x000018D8
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
			if (list.Count > 1 && ExTraceGlobals.WCFServiceEndpointTracer.IsTraceEnabled(TraceType.WarningTrace))
			{
				ExTraceGlobals.WCFServiceEndpointTracer.TraceWarning<string, string>((long)this.GetHashCode(), "Request has multiple identities. Identity {0} will be used. Other identities {1}", list[0].Name, string.Join<IIdentity>(",", list));
			}
			return list[0];
		}

		// Token: 0x04000022 RID: 34
		private static readonly string PolicyId = typeof(AuthorizationPolicy).FullName;
	}
}
