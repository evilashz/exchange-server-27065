using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x020000F0 RID: 240
	[Serializable]
	public class AuthServerIdParameter : ADIdParameter
	{
		// Token: 0x060008A8 RID: 2216 RVA: 0x0001EA40 File Offset: 0x0001CC40
		public AuthServerIdParameter()
		{
		}

		// Token: 0x060008A9 RID: 2217 RVA: 0x0001EA48 File Offset: 0x0001CC48
		public AuthServerIdParameter(string identity) : base(identity)
		{
		}

		// Token: 0x060008AA RID: 2218 RVA: 0x0001EA51 File Offset: 0x0001CC51
		public AuthServerIdParameter(ADObjectId adObjectId) : base(adObjectId)
		{
		}

		// Token: 0x060008AB RID: 2219 RVA: 0x0001EA5A File Offset: 0x0001CC5A
		public AuthServerIdParameter(PartnerApplication app) : base(app.Id)
		{
		}

		// Token: 0x060008AC RID: 2220 RVA: 0x0001EA68 File Offset: 0x0001CC68
		public AuthServerIdParameter(INamedIdentity namedIdentity) : base(namedIdentity)
		{
		}

		// Token: 0x060008AD RID: 2221 RVA: 0x0001EA71 File Offset: 0x0001CC71
		public static AuthServerIdParameter Parse(string identity)
		{
			return new AuthServerIdParameter(identity);
		}

		// Token: 0x170001DB RID: 475
		// (get) Token: 0x060008AE RID: 2222 RVA: 0x0001EA7C File Offset: 0x0001CC7C
		internal override ADPropertyDefinition[] AdditionalMatchingProperties
		{
			get
			{
				return new ADPropertyDefinition[]
				{
					AuthServerSchema.IssuerIdentifier
				};
			}
		}
	}
}
