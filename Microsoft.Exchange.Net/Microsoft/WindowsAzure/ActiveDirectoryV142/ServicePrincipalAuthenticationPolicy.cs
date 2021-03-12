using System;
using System.CodeDom.Compiler;
using System.Collections.ObjectModel;

namespace Microsoft.WindowsAzure.ActiveDirectoryV142
{
	// Token: 0x020005FA RID: 1530
	public class ServicePrincipalAuthenticationPolicy
	{
		// Token: 0x06001AC6 RID: 6854 RVA: 0x00031664 File Offset: 0x0002F864
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

		// Token: 0x1700074C RID: 1868
		// (get) Token: 0x06001AC7 RID: 6855 RVA: 0x0003168D File Offset: 0x0002F88D
		// (set) Token: 0x06001AC8 RID: 6856 RVA: 0x00031695 File Offset: 0x0002F895
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

		// Token: 0x1700074D RID: 1869
		// (get) Token: 0x06001AC9 RID: 6857 RVA: 0x0003169E File Offset: 0x0002F89E
		// (set) Token: 0x06001ACA RID: 6858 RVA: 0x000316A6 File Offset: 0x0002F8A6
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

		// Token: 0x04001C2C RID: 7212
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _defaultPolicy;

		// Token: 0x04001C2D RID: 7213
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<string> _allowedPolicies = new Collection<string>();
	}
}
