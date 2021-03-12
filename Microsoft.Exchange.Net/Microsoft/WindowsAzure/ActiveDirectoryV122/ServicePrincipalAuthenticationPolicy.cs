using System;
using System.CodeDom.Compiler;
using System.Collections.ObjectModel;

namespace Microsoft.WindowsAzure.ActiveDirectoryV122
{
	// Token: 0x020005CE RID: 1486
	public class ServicePrincipalAuthenticationPolicy
	{
		// Token: 0x060017B5 RID: 6069 RVA: 0x0002EF48 File Offset: 0x0002D148
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public static ServicePrincipalAuthenticationPolicy CreateServicePrincipalAuthenticationPolicy(Collection<string> allowedPolicies)
		{
			ServicePrincipalAuthenticationPolicy servicePrincipalAuthenticationPolicy = new ServicePrincipalAuthenticationPolicy();
			if (allowedPolicies == null)
			{
				throw new ArgumentNullException("allowedPolicies");
			}
			servicePrincipalAuthenticationPolicy.allowedPolicies = allowedPolicies;
			return servicePrincipalAuthenticationPolicy;
		}

		// Token: 0x170005E7 RID: 1511
		// (get) Token: 0x060017B6 RID: 6070 RVA: 0x0002EF71 File Offset: 0x0002D171
		// (set) Token: 0x060017B7 RID: 6071 RVA: 0x0002EF79 File Offset: 0x0002D179
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public string defaultPolicy
		{
			get
			{
				return this._defaultPolicy;
			}
			set
			{
				this._defaultPolicy = value;
			}
		}

		// Token: 0x170005E8 RID: 1512
		// (get) Token: 0x060017B8 RID: 6072 RVA: 0x0002EF82 File Offset: 0x0002D182
		// (set) Token: 0x060017B9 RID: 6073 RVA: 0x0002EF8A File Offset: 0x0002D18A
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public Collection<string> allowedPolicies
		{
			get
			{
				return this._allowedPolicies;
			}
			set
			{
				this._allowedPolicies = value;
			}
		}

		// Token: 0x04001AC0 RID: 6848
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _defaultPolicy;

		// Token: 0x04001AC1 RID: 6849
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<string> _allowedPolicies = new Collection<string>();
	}
}
