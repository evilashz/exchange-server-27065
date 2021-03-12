using System;
using System.CodeDom.Compiler;
using System.Collections.ObjectModel;

namespace Microsoft.WindowsAzure.ActiveDirectory
{
	// Token: 0x020005AF RID: 1455
	public class ServicePrincipalAuthenticationPolicy
	{
		// Token: 0x06001581 RID: 5505 RVA: 0x0002D3EC File Offset: 0x0002B5EC
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

		// Token: 0x170004E7 RID: 1255
		// (get) Token: 0x06001582 RID: 5506 RVA: 0x0002D415 File Offset: 0x0002B615
		// (set) Token: 0x06001583 RID: 5507 RVA: 0x0002D41D File Offset: 0x0002B61D
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

		// Token: 0x170004E8 RID: 1256
		// (get) Token: 0x06001584 RID: 5508 RVA: 0x0002D426 File Offset: 0x0002B626
		// (set) Token: 0x06001585 RID: 5509 RVA: 0x0002D42E File Offset: 0x0002B62E
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

		// Token: 0x040019BC RID: 6588
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _defaultPolicy;

		// Token: 0x040019BD RID: 6589
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<string> _allowedPolicies = new Collection<string>();
	}
}
