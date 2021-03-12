using System;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x02000194 RID: 404
	[Serializable]
	public class SharingPolicyIdParameter : ADIdParameter
	{
		// Token: 0x170002A2 RID: 674
		// (get) Token: 0x06000EA1 RID: 3745 RVA: 0x0002B155 File Offset: 0x00029355
		protected override SharedTenantConfigurationMode SharedTenantConfigurationMode
		{
			get
			{
				return SharedTenantConfigurationMode.Dehydrateable;
			}
		}

		// Token: 0x06000EA2 RID: 3746 RVA: 0x0002B158 File Offset: 0x00029358
		public SharingPolicyIdParameter()
		{
		}

		// Token: 0x06000EA3 RID: 3747 RVA: 0x0002B160 File Offset: 0x00029360
		public SharingPolicyIdParameter(ADObjectId adobjectid) : base(adobjectid)
		{
		}

		// Token: 0x06000EA4 RID: 3748 RVA: 0x0002B169 File Offset: 0x00029369
		public SharingPolicyIdParameter(INamedIdentity namedIdentity) : base(namedIdentity)
		{
		}

		// Token: 0x06000EA5 RID: 3749 RVA: 0x0002B172 File Offset: 0x00029372
		public SharingPolicyIdParameter(string identity) : base(identity)
		{
		}

		// Token: 0x06000EA6 RID: 3750 RVA: 0x0002B17B File Offset: 0x0002937B
		public static SharingPolicyIdParameter Parse(string identity)
		{
			return new SharingPolicyIdParameter(identity);
		}
	}
}
