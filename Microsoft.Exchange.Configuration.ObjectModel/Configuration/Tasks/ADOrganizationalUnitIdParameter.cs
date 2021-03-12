using System;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x020000E3 RID: 227
	[Serializable]
	public sealed class ADOrganizationalUnitIdParameter : OrganizationalUnitIdParameterBase
	{
		// Token: 0x0600084A RID: 2122 RVA: 0x0001E1FF File Offset: 0x0001C3FF
		public ADOrganizationalUnitIdParameter()
		{
		}

		// Token: 0x0600084B RID: 2123 RVA: 0x0001E207 File Offset: 0x0001C407
		public ADOrganizationalUnitIdParameter(string identity) : base(identity)
		{
		}

		// Token: 0x0600084C RID: 2124 RVA: 0x0001E210 File Offset: 0x0001C410
		public ADOrganizationalUnitIdParameter(ADObjectId adObjectId) : base(adObjectId)
		{
		}

		// Token: 0x0600084D RID: 2125 RVA: 0x0001E219 File Offset: 0x0001C419
		public ADOrganizationalUnitIdParameter(INamedIdentity namedIdentity) : base(namedIdentity)
		{
		}

		// Token: 0x0600084E RID: 2126 RVA: 0x0001E222 File Offset: 0x0001C422
		public static ADOrganizationalUnitIdParameter Parse(string identity)
		{
			return new ADOrganizationalUnitIdParameter(identity);
		}
	}
}
