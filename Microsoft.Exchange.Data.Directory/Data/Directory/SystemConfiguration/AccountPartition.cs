using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020002A5 RID: 677
	[Serializable]
	public class AccountPartition : ADConfigurationObject
	{
		// Token: 0x06001F73 RID: 8051 RVA: 0x0008BE2D File Offset: 0x0008A02D
		internal static QueryFilter IsLocalForestFilterBuilder(SinglePropertyFilter filter)
		{
			return new BitMaskAndFilter(AccountPartitionSchema.ProvisioningFlags, 1UL);
		}

		// Token: 0x06001F74 RID: 8052 RVA: 0x0008BE3B File Offset: 0x0008A03B
		internal static QueryFilter IsSecondaryFilterBuilder(SinglePropertyFilter filter)
		{
			return new BitMaskAndFilter(AccountPartitionSchema.ProvisioningFlags, 4UL);
		}

		// Token: 0x06001F75 RID: 8053 RVA: 0x0008BE4C File Offset: 0x0008A04C
		internal static object PartitionIdGetter(IPropertyBag propertyBag)
		{
			bool flag = (bool)propertyBag[AccountPartitionSchema.IsLocalForest];
			if (flag)
			{
				return new PartitionId(PartitionId.LocalForest.ForestFQDN, ADObjectId.ResourcePartitionGuid);
			}
			ADObjectId adobjectId = (ADObjectId)propertyBag[AccountPartitionSchema.TrustedDomainLink];
			if (adobjectId == null)
			{
				return null;
			}
			string name = adobjectId.Name;
			if ((ObjectState)propertyBag[ADObjectSchema.ObjectState] == ObjectState.New)
			{
				return new PartitionId(name);
			}
			return new PartitionId(name, ((ADObjectId)propertyBag[ADObjectSchema.Id]).ObjectGuid);
		}

		// Token: 0x17000785 RID: 1925
		// (get) Token: 0x06001F76 RID: 8054 RVA: 0x0008BED3 File Offset: 0x0008A0D3
		// (set) Token: 0x06001F77 RID: 8055 RVA: 0x0008BEDB File Offset: 0x0008A0DB
		public new string Name
		{
			get
			{
				return base.Name;
			}
			internal set
			{
				base.Name = value;
			}
		}

		// Token: 0x17000786 RID: 1926
		// (get) Token: 0x06001F78 RID: 8056 RVA: 0x0008BEE4 File Offset: 0x0008A0E4
		// (set) Token: 0x06001F79 RID: 8057 RVA: 0x0008BEF6 File Offset: 0x0008A0F6
		public ADObjectId TrustedDomain
		{
			get
			{
				return (ADObjectId)this[AccountPartitionSchema.TrustedDomainLink];
			}
			internal set
			{
				this[AccountPartitionSchema.TrustedDomainLink] = value;
			}
		}

		// Token: 0x17000787 RID: 1927
		// (get) Token: 0x06001F7A RID: 8058 RVA: 0x0008BF04 File Offset: 0x0008A104
		internal PartitionId PartitionId
		{
			get
			{
				return (PartitionId)this[AccountPartitionSchema.PartitionId];
			}
		}

		// Token: 0x17000788 RID: 1928
		// (get) Token: 0x06001F7B RID: 8059 RVA: 0x0008BF16 File Offset: 0x0008A116
		// (set) Token: 0x06001F7C RID: 8060 RVA: 0x0008BF28 File Offset: 0x0008A128
		public bool IsLocalForest
		{
			get
			{
				return (bool)this[AccountPartitionSchema.IsLocalForest];
			}
			internal set
			{
				this[AccountPartitionSchema.IsLocalForest] = value;
			}
		}

		// Token: 0x17000789 RID: 1929
		// (get) Token: 0x06001F7D RID: 8061 RVA: 0x0008BF3B File Offset: 0x0008A13B
		// (set) Token: 0x06001F7E RID: 8062 RVA: 0x0008BF4D File Offset: 0x0008A14D
		public bool EnabledForProvisioning
		{
			get
			{
				return (bool)this[AccountPartitionSchema.EnabledForProvisioning];
			}
			internal set
			{
				this[AccountPartitionSchema.EnabledForProvisioning] = value;
			}
		}

		// Token: 0x1700078A RID: 1930
		// (get) Token: 0x06001F7F RID: 8063 RVA: 0x0008BF60 File Offset: 0x0008A160
		// (set) Token: 0x06001F80 RID: 8064 RVA: 0x0008BF72 File Offset: 0x0008A172
		public bool IsSecondary
		{
			get
			{
				return (bool)this[AccountPartitionSchema.IsSecondary];
			}
			internal set
			{
				this[AccountPartitionSchema.IsSecondary] = value;
			}
		}

		// Token: 0x1700078B RID: 1931
		// (get) Token: 0x06001F81 RID: 8065 RVA: 0x0008BF85 File Offset: 0x0008A185
		internal override ADObjectSchema Schema
		{
			get
			{
				return ObjectSchema.GetInstance<AccountPartitionSchema>();
			}
		}

		// Token: 0x1700078C RID: 1932
		// (get) Token: 0x06001F82 RID: 8066 RVA: 0x0008BF8C File Offset: 0x0008A18C
		internal override string MostDerivedObjectClass
		{
			get
			{
				return AccountPartition.MostDerivedClass;
			}
		}

		// Token: 0x06001F83 RID: 8067 RVA: 0x0008BF93 File Offset: 0x0008A193
		internal bool TryGetPartitionId(out PartitionId partitionId)
		{
			partitionId = this.PartitionId;
			return this.TrustedDomain != null || this.IsLocalForest;
		}

		// Token: 0x06001F84 RID: 8068 RVA: 0x0008BFB0 File Offset: 0x0008A1B0
		protected override void ValidateRead(List<ValidationError> errors)
		{
			base.ValidateRead(errors);
			if (this.IsLocalForest && this.TrustedDomain != null)
			{
				errors.Add(new ObjectValidationError(DirectoryStrings.ErrorAccountPartitionCantBeLocalAndHaveTrustAtTheSameTime(this.Identity.ToString()), this.Identity, string.Empty));
			}
			if (this.IsLocalForest && this.IsSecondary)
			{
				errors.Add(new ObjectValidationError(DirectoryStrings.ErrorAccountPartitionCantBeLocalAndSecondaryAtTheSameTime(this.Identity.ToString()), this.Identity, string.Empty));
			}
			if (!this.IsLocalForest && this.TrustedDomain == null)
			{
				errors.Add(new ObjectValidationError(DirectoryStrings.ErrorRemoteAccountPartitionMustHaveTrust(this.Identity.ToString()), this.Identity, string.Empty));
			}
			if (this.IsSecondary && this.EnabledForProvisioning)
			{
				errors.Add(new ObjectValidationError(DirectoryStrings.ErrorSecondaryAccountPartitionCantBeUsedForProvisioning(this.Identity.ToString()), this.Identity, string.Empty));
			}
		}

		// Token: 0x040012B9 RID: 4793
		public static readonly string AccountForestContainerName = "Account Forests";

		// Token: 0x040012BA RID: 4794
		public static readonly string ResourceForestContainerName = "Resource Forest";

		// Token: 0x040012BB RID: 4795
		internal static string MostDerivedClass = "msExchAccountForest";
	}
}
