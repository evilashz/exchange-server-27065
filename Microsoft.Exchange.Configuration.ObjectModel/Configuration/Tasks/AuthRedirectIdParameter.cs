using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x020000EE RID: 238
	[Serializable]
	public class AuthRedirectIdParameter : ADIdParameter, IIdentityParameter
	{
		// Token: 0x06000893 RID: 2195 RVA: 0x0001E8D7 File Offset: 0x0001CAD7
		public AuthRedirectIdParameter()
		{
		}

		// Token: 0x06000894 RID: 2196 RVA: 0x0001E8DF File Offset: 0x0001CADF
		public AuthRedirectIdParameter(string identity) : base(identity)
		{
		}

		// Token: 0x06000895 RID: 2197 RVA: 0x0001E8E8 File Offset: 0x0001CAE8
		public AuthRedirectIdParameter(ADObjectId adObjectId) : base(adObjectId)
		{
		}

		// Token: 0x06000896 RID: 2198 RVA: 0x0001E8F1 File Offset: 0x0001CAF1
		public AuthRedirectIdParameter(AuthRedirect authRedirect) : base(authRedirect.Id)
		{
		}

		// Token: 0x06000897 RID: 2199 RVA: 0x0001E8FF File Offset: 0x0001CAFF
		public AuthRedirectIdParameter(INamedIdentity namedIdentity) : base(namedIdentity)
		{
		}

		// Token: 0x06000898 RID: 2200 RVA: 0x0001E908 File Offset: 0x0001CB08
		public static AuthRedirectIdParameter Parse(string identity)
		{
			return new AuthRedirectIdParameter(identity);
		}

		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x06000899 RID: 2201 RVA: 0x0001E910 File Offset: 0x0001CB10
		internal override ADPropertyDefinition[] AdditionalMatchingProperties
		{
			get
			{
				return new ADPropertyDefinition[]
				{
					AuthRedirectSchema.AuthScheme
				};
			}
		}
	}
}
