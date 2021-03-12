using System;
using System.IdentityModel.Claims;
using System.IdentityModel.Policy;
using System.ServiceModel;

namespace Microsoft.Exchange.PowerShell.RbacHostingTools
{
	// Token: 0x02000005 RID: 5
	public class RbacAuthorizationPolicy : IAuthorizationPolicy, IAuthorizationComponent
	{
		// Token: 0x06000011 RID: 17 RVA: 0x00002E37 File Offset: 0x00001037
		public virtual bool Evaluate(EvaluationContext evaluationContext, ref object state)
		{
			evaluationContext.Properties["Principal"] = OperationContext.Current.GetRbacPrincipal().GetWrapperPrincipal();
			return true;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000012 RID: 18 RVA: 0x00002E59 File Offset: 0x00001059
		public ClaimSet Issuer
		{
			get
			{
				return ClaimSet.System;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000013 RID: 19 RVA: 0x00002E60 File Offset: 0x00001060
		public string Id
		{
			get
			{
				return RbacAuthorizationPolicy.PolicyId;
			}
		}

		// Token: 0x04000006 RID: 6
		private static readonly string PolicyId = typeof(RbacAuthorizationPolicy).FullName;
	}
}
