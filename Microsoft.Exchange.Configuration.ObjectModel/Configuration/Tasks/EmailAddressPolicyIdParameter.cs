using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x0200010C RID: 268
	[Serializable]
	public class EmailAddressPolicyIdParameter : ADIdParameter
	{
		// Token: 0x060009AB RID: 2475 RVA: 0x00021010 File Offset: 0x0001F210
		public EmailAddressPolicyIdParameter()
		{
		}

		// Token: 0x060009AC RID: 2476 RVA: 0x00021018 File Offset: 0x0001F218
		public EmailAddressPolicyIdParameter(ADObjectId adObjectId) : base(adObjectId)
		{
		}

		// Token: 0x060009AD RID: 2477 RVA: 0x00021021 File Offset: 0x0001F221
		public EmailAddressPolicyIdParameter(EmailAddressPolicy policy) : base(policy.Id)
		{
		}

		// Token: 0x060009AE RID: 2478 RVA: 0x0002102F File Offset: 0x0001F22F
		public EmailAddressPolicyIdParameter(INamedIdentity namedIdentity) : base(namedIdentity)
		{
		}

		// Token: 0x060009AF RID: 2479 RVA: 0x00021038 File Offset: 0x0001F238
		protected EmailAddressPolicyIdParameter(string identity) : base(identity)
		{
		}

		// Token: 0x060009B0 RID: 2480 RVA: 0x00021041 File Offset: 0x0001F241
		public static EmailAddressPolicyIdParameter Parse(string identity)
		{
			return new EmailAddressPolicyIdParameter(identity);
		}

		// Token: 0x060009B1 RID: 2481 RVA: 0x0002104C File Offset: 0x0001F24C
		internal static ADObjectId GetRootContainerId(IConfigurationSession scSession, OrganizationId organizationId)
		{
			ADObjectId adobjectId;
			if (organizationId != null && organizationId.ConfigurationUnit != null)
			{
				adobjectId = organizationId.ConfigurationUnit;
			}
			else
			{
				adobjectId = scSession.GetOrgContainerId();
			}
			return adobjectId.GetDescendantId(EmailAddressPolicy.RdnEapContainerToOrganization);
		}
	}
}
