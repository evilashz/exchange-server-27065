using System;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x0200011A RID: 282
	[Serializable]
	public class IntraOrganizationConnectorIdParameter : ADIdParameter
	{
		// Token: 0x06000A0F RID: 2575 RVA: 0x000218F8 File Offset: 0x0001FAF8
		public IntraOrganizationConnectorIdParameter()
		{
		}

		// Token: 0x06000A10 RID: 2576 RVA: 0x00021900 File Offset: 0x0001FB00
		public IntraOrganizationConnectorIdParameter(ADObjectId adobjectid) : base(adobjectid)
		{
		}

		// Token: 0x06000A11 RID: 2577 RVA: 0x00021909 File Offset: 0x0001FB09
		public IntraOrganizationConnectorIdParameter(INamedIdentity namedIdentity) : base(namedIdentity)
		{
		}

		// Token: 0x06000A12 RID: 2578 RVA: 0x00021912 File Offset: 0x0001FB12
		protected IntraOrganizationConnectorIdParameter(string identity) : base(identity)
		{
		}

		// Token: 0x06000A13 RID: 2579 RVA: 0x0002191B File Offset: 0x0001FB1B
		public static IntraOrganizationConnectorIdParameter Parse(string identity)
		{
			return new IntraOrganizationConnectorIdParameter(identity);
		}
	}
}
