using System;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x020000F2 RID: 242
	[Serializable]
	public class AvailabilityConfigIdParameter : ADIdParameter
	{
		// Token: 0x060008B5 RID: 2229 RVA: 0x0001EB90 File Offset: 0x0001CD90
		public AvailabilityConfigIdParameter()
		{
		}

		// Token: 0x060008B6 RID: 2230 RVA: 0x0001EB98 File Offset: 0x0001CD98
		public AvailabilityConfigIdParameter(string identity) : base(identity)
		{
		}

		// Token: 0x060008B7 RID: 2231 RVA: 0x0001EBA1 File Offset: 0x0001CDA1
		public AvailabilityConfigIdParameter(ADObjectId adObjectId) : base(adObjectId)
		{
		}

		// Token: 0x060008B8 RID: 2232 RVA: 0x0001EBAA File Offset: 0x0001CDAA
		public AvailabilityConfigIdParameter(INamedIdentity namedIdentity) : base(namedIdentity)
		{
		}

		// Token: 0x060008B9 RID: 2233 RVA: 0x0001EBB3 File Offset: 0x0001CDB3
		public static AvailabilityConfigIdParameter Parse(string identity)
		{
			return new AvailabilityConfigIdParameter(identity);
		}
	}
}
