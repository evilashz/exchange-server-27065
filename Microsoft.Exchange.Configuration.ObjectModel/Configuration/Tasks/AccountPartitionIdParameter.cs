using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x020000D6 RID: 214
	[Serializable]
	public class AccountPartitionIdParameter : ADIdParameter
	{
		// Token: 0x060007E3 RID: 2019 RVA: 0x0001D071 File Offset: 0x0001B271
		public AccountPartitionIdParameter()
		{
		}

		// Token: 0x060007E4 RID: 2020 RVA: 0x0001D079 File Offset: 0x0001B279
		public AccountPartitionIdParameter(ADObjectId adobjectid) : base(adobjectid)
		{
			this.fqdn = new Fqdn(adobjectid.GetPartitionId().ForestFQDN);
		}

		// Token: 0x060007E5 RID: 2021 RVA: 0x0001D098 File Offset: 0x0001B298
		public AccountPartitionIdParameter(Fqdn fqdn) : base((fqdn == null) ? null : fqdn.ToString())
		{
			if (fqdn == null)
			{
				throw new ArgumentNullException("fqdn");
			}
			this.fqdn = fqdn;
		}

		// Token: 0x060007E6 RID: 2022 RVA: 0x0001D0C1 File Offset: 0x0001B2C1
		public AccountPartitionIdParameter(AccountPartition partition)
		{
			if (partition == null)
			{
				throw new ArgumentNullException("partition");
			}
			this.Initialize(partition.Id);
		}

		// Token: 0x060007E7 RID: 2023 RVA: 0x0001D0E3 File Offset: 0x0001B2E3
		public AccountPartitionIdParameter(INamedIdentity namedIdentity) : base(namedIdentity)
		{
		}

		// Token: 0x060007E8 RID: 2024 RVA: 0x0001D0EC File Offset: 0x0001B2EC
		protected AccountPartitionIdParameter(string identity) : base(identity)
		{
			Fqdn.TryParse(identity, out this.fqdn);
		}

		// Token: 0x060007E9 RID: 2025 RVA: 0x0001D104 File Offset: 0x0001B304
		internal override IEnumerable<T> GetObjects<T>(ADObjectId rootId, IDirectorySession session, IDirectorySession subTreeSession, OptionalIdentityData optionalData, out LocalizedString? notFoundReason)
		{
			if (session == null)
			{
				throw new ArgumentNullException("session");
			}
			if (subTreeSession == null)
			{
				throw new ArgumentNullException("subTreeSession");
			}
			EnumerableWrapper<T> enumerableWrapper = null;
			enumerableWrapper = base.GetEnumerableWrapper<T>(enumerableWrapper, base.GetObjects<T>(rootId, session, subTreeSession, optionalData, out notFoundReason));
			if (enumerableWrapper.HasElements())
			{
				return enumerableWrapper;
			}
			if (!typeof(T).IsAssignableFrom(typeof(AccountPartition)))
			{
				return enumerableWrapper;
			}
			if (this.fqdn != null)
			{
				ADObjectId adobjectId = ADSession.GetDomainNamingContextForLocalForest();
				adobjectId = adobjectId.GetChildId("System").GetChildId(this.fqdn.ToString());
				ADPagedReader<T> collection = session.FindPaged<T>(rootId, QueryScope.SubTree, new ComparisonFilter(ComparisonOperator.Equal, AccountPartitionSchema.TrustedDomainLink, adobjectId.DistinguishedName), null, 0, null);
				enumerableWrapper = base.GetEnumerableWrapper<T>(enumerableWrapper, collection);
				if (enumerableWrapper.HasElements())
				{
					return enumerableWrapper;
				}
				Guid g;
				Guid.TryParse(this.fqdn, out g);
				if (TopologyProvider.LocalForestFqdn.Equals(this.fqdn.ToString(), StringComparison.OrdinalIgnoreCase) || ADObjectId.ResourcePartitionGuid.Equals(g))
				{
					collection = session.FindPaged<T>(rootId, QueryScope.SubTree, new ComparisonFilter(ComparisonOperator.Equal, AccountPartitionSchema.IsLocalForest, true), null, 0, null);
					enumerableWrapper = base.GetEnumerableWrapper<T>(enumerableWrapper, collection);
				}
				if (enumerableWrapper.HasElements())
				{
					return enumerableWrapper;
				}
				PartitionId partitionId;
				if (ADAccountPartitionLocator.IsSingleForestTopology(out partitionId) && this.fqdn.ToString().Equals(partitionId.ForestFQDN, StringComparison.OrdinalIgnoreCase) && partitionId.PartitionObjectId != null)
				{
					base.UpdateInternalADObjectId(new ADObjectId(partitionId.PartitionObjectId.Value));
					enumerableWrapper = base.GetEnumerableWrapper<T>(enumerableWrapper, base.GetExactMatchObjects<T>(rootId, session, optionalData));
				}
			}
			return enumerableWrapper;
		}

		// Token: 0x060007EA RID: 2026 RVA: 0x0001D293 File Offset: 0x0001B493
		public static AccountPartitionIdParameter Parse(string identity)
		{
			return new AccountPartitionIdParameter(identity);
		}

		// Token: 0x04000249 RID: 585
		private Fqdn fqdn;
	}
}
