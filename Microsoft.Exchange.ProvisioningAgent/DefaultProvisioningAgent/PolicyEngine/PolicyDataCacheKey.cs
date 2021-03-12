using System;
using System.ComponentModel;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Provisioning;

namespace Microsoft.Exchange.DefaultProvisioningAgent.PolicyEngine
{
	// Token: 0x02000037 RID: 55
	[ImmutableObject(true)]
	internal class PolicyDataCacheKey : IEquatable<PolicyDataCacheKey>
	{
		// Token: 0x0600015C RID: 348 RVA: 0x0000888C File Offset: 0x00006A8C
		public PolicyDataCacheKey(OrganizationId organizationId, Type poType, ProvisioningPolicyType policyType)
		{
			if (null == organizationId)
			{
				throw new ArgumentNullException("organizationId");
			}
			if (null == poType)
			{
				throw new ArgumentNullException("poType");
			}
			if ((policyType & ~(ProvisioningPolicyType.Template | ProvisioningPolicyType.Enforcement)) != (ProvisioningPolicyType)0)
			{
				throw new ArgumentOutOfRangeException("policyType");
			}
			if (!PolicyConfiguration.ObjectType2PolicyEntryDictionary.ContainsKey(poType))
			{
				throw new ArgumentOutOfRangeException("poType");
			}
			this.organizationId = organizationId;
			this.poType = poType;
			this.policyType = policyType;
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x0600015D RID: 349 RVA: 0x00008905 File Offset: 0x00006B05
		public Type ObjectType
		{
			get
			{
				return this.poType;
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x0600015E RID: 350 RVA: 0x0000890D File Offset: 0x00006B0D
		public ProvisioningPolicyType PolicyType
		{
			get
			{
				return this.policyType;
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x0600015F RID: 351 RVA: 0x00008915 File Offset: 0x00006B15
		public OrganizationId OrganizationId
		{
			get
			{
				return this.organizationId;
			}
		}

		// Token: 0x06000160 RID: 352 RVA: 0x00008920 File Offset: 0x00006B20
		public bool Equals(PolicyDataCacheKey other)
		{
			return other != null && (this.ObjectType.Equals(other.ObjectType) && this.PolicyType.Equals(other.PolicyType)) && object.Equals(this.OrganizationId, other.OrganizationId);
		}

		// Token: 0x06000161 RID: 353 RVA: 0x00008975 File Offset: 0x00006B75
		public override bool Equals(object other)
		{
			return other is PolicyDataCacheKey && this.Equals((PolicyDataCacheKey)other);
		}

		// Token: 0x06000162 RID: 354 RVA: 0x0000898D File Offset: 0x00006B8D
		public override int GetHashCode()
		{
			return this.ObjectType.GetHashCode() ^ this.PolicyType.GetHashCode() ^ this.OrganizationId.GetHashCode();
		}

		// Token: 0x040000AB RID: 171
		private OrganizationId organizationId;

		// Token: 0x040000AC RID: 172
		private Type poType;

		// Token: 0x040000AD RID: 173
		private ProvisioningPolicyType policyType;
	}
}
