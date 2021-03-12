using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x02000134 RID: 308
	[Serializable]
	public class OrganizationalUnitIdParameter : OrganizationalUnitIdParameterBase
	{
		// Token: 0x06000B07 RID: 2823 RVA: 0x00023AD3 File Offset: 0x00021CD3
		public OrganizationalUnitIdParameter(string identity) : base(identity)
		{
		}

		// Token: 0x06000B08 RID: 2824 RVA: 0x00023ADC File Offset: 0x00021CDC
		public OrganizationalUnitIdParameter()
		{
		}

		// Token: 0x06000B09 RID: 2825 RVA: 0x00023AE4 File Offset: 0x00021CE4
		public OrganizationalUnitIdParameter(ADObjectId adObjectId) : base(adObjectId)
		{
		}

		// Token: 0x06000B0A RID: 2826 RVA: 0x00023AED File Offset: 0x00021CED
		public OrganizationalUnitIdParameter(INamedIdentity namedIdentity) : base(namedIdentity)
		{
		}

		// Token: 0x06000B0B RID: 2827 RVA: 0x00023AF6 File Offset: 0x00021CF6
		public OrganizationalUnitIdParameter(ExtendedOrganizationalUnit organizationalUnit) : base(organizationalUnit.Id)
		{
		}

		// Token: 0x06000B0C RID: 2828 RVA: 0x00023B04 File Offset: 0x00021D04
		public static OrganizationalUnitIdParameter Parse(string identity)
		{
			return new OrganizationalUnitIdParameter(identity);
		}
	}
}
