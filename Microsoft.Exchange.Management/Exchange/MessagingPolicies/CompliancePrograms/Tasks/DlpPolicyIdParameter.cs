using System;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.MessagingPolicies.CompliancePrograms.Tasks
{
	// Token: 0x02000952 RID: 2386
	[Serializable]
	public class DlpPolicyIdParameter : ADIdParameter
	{
		// Token: 0x0600552A RID: 21802 RVA: 0x0015ECD6 File Offset: 0x0015CED6
		public DlpPolicyIdParameter()
		{
		}

		// Token: 0x0600552B RID: 21803 RVA: 0x0015ECDE File Offset: 0x0015CEDE
		public DlpPolicyIdParameter(string identity) : base(identity)
		{
		}

		// Token: 0x0600552C RID: 21804 RVA: 0x0015ECE7 File Offset: 0x0015CEE7
		public DlpPolicyIdParameter(ADObjectId adObjectId) : base(adObjectId)
		{
		}

		// Token: 0x0600552D RID: 21805 RVA: 0x0015ECF0 File Offset: 0x0015CEF0
		public DlpPolicyIdParameter(INamedIdentity namedIdentity) : base(namedIdentity)
		{
		}

		// Token: 0x0600552E RID: 21806 RVA: 0x0015ECF9 File Offset: 0x0015CEF9
		public static explicit operator string(DlpPolicyIdParameter dlpPolicyIdParameter)
		{
			return dlpPolicyIdParameter.ToString();
		}

		// Token: 0x0600552F RID: 21807 RVA: 0x0015ED01 File Offset: 0x0015CF01
		public static DlpPolicyIdParameter Parse(string identity)
		{
			return new DlpPolicyIdParameter(identity);
		}

		// Token: 0x17001989 RID: 6537
		// (get) Token: 0x06005530 RID: 21808 RVA: 0x0015ED0C File Offset: 0x0015CF0C
		internal override ADPropertyDefinition[] AdditionalMatchingProperties
		{
			get
			{
				return new ADPropertyDefinition[]
				{
					ADComplianceProgramSchema.ImmutableId
				};
			}
		}

		// Token: 0x06005531 RID: 21809 RVA: 0x0015ED2C File Offset: 0x0015CF2C
		public static ADObjectId GetDlpPolicyCollectionRdn()
		{
			ADObjectId adobjectId = new ADObjectId(new AdName("cn", "Transport Settings"));
			return adobjectId.GetChildId("Rules").GetChildId(DlpUtils.TenantDlpPoliciesCollectionName);
		}
	}
}
