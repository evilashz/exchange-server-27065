using System;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x02000193 RID: 403
	[Serializable]
	public class OrganizationRelationshipIdParameter : ADIdParameter
	{
		// Token: 0x06000E9C RID: 3740 RVA: 0x0002B12A File Offset: 0x0002932A
		public OrganizationRelationshipIdParameter()
		{
		}

		// Token: 0x06000E9D RID: 3741 RVA: 0x0002B132 File Offset: 0x00029332
		public OrganizationRelationshipIdParameter(ADObjectId adobjectid) : base(adobjectid)
		{
		}

		// Token: 0x06000E9E RID: 3742 RVA: 0x0002B13B File Offset: 0x0002933B
		public OrganizationRelationshipIdParameter(INamedIdentity namedIdentity) : base(namedIdentity)
		{
		}

		// Token: 0x06000E9F RID: 3743 RVA: 0x0002B144 File Offset: 0x00029344
		protected OrganizationRelationshipIdParameter(string identity) : base(identity)
		{
		}

		// Token: 0x06000EA0 RID: 3744 RVA: 0x0002B14D File Offset: 0x0002934D
		public static OrganizationRelationshipIdParameter Parse(string identity)
		{
			return new OrganizationRelationshipIdParameter(identity);
		}
	}
}
