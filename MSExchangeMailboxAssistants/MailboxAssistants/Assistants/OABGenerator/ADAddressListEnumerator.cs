using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.ApplicationLogic.Performance;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics.Performance;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.OABGenerator
{
	// Token: 0x020001DB RID: 475
	internal sealed class ADAddressListEnumerator : ADPagedReader<ADRawEntry>
	{
		// Token: 0x0600123B RID: 4667 RVA: 0x00068564 File Offset: 0x00066764
		public static ADAddressListEnumerator Create(ADObjectId addressList, OrganizationId organizationId, IEnumerable<ADPropertyDefinition> properties, int pageSize, GenerationStats stats)
		{
			IRecipientSession tenantOrRootOrgRecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(true, ConsistencyMode.PartiallyConsistent, ADSessionSettings.FromOrganizationIdWithAddressListScopeServiceOnly(organizationId, addressList), 48, "Create", "f:\\15.00.1497\\sources\\dev\\MailboxAssistants\\src\\assistants\\OABGenerator\\ADAddressListEnumerator.cs");
			QueryFilter filter = new ComparisonFilter(ComparisonOperator.Equal, ADRecipientSchema.AddressListMembership, addressList);
			return new ADAddressListEnumerator(tenantOrRootOrgRecipientSession, null, QueryScope.SubTree, filter, null, pageSize, properties, tenantOrRootOrgRecipientSession.SessionSettings.SkipCheckVirtualIndex)
			{
				session = tenantOrRootOrgRecipientSession,
				stats = stats
			};
		}

		// Token: 0x0600123C RID: 4668 RVA: 0x000685C8 File Offset: 0x000667C8
		private ADAddressListEnumerator(IDirectorySession session, ADObjectId rootId, QueryScope scope, QueryFilter filter, SortBy sortBy, int pageSize, IEnumerable<PropertyDefinition> properties, bool skipCheckVirtualIndex) : base(session, rootId, scope, filter, sortBy, pageSize, properties, skipCheckVirtualIndex)
		{
		}

		// Token: 0x170004BF RID: 1215
		// (get) Token: 0x0600123D RID: 4669 RVA: 0x000685E8 File Offset: 0x000667E8
		public string LastUsedDc
		{
			get
			{
				return this.session.LastUsedDc;
			}
		}

		// Token: 0x170004C0 RID: 1216
		// (get) Token: 0x0600123E RID: 4670 RVA: 0x000685F5 File Offset: 0x000667F5
		public new bool? RetrievedAllData
		{
			get
			{
				return base.RetrievedAllData;
			}
		}

		// Token: 0x0600123F RID: 4671 RVA: 0x00068600 File Offset: 0x00066800
		public ADRawEntry[] GetNextPageSorted()
		{
			ADRawEntry[] nextPage;
			using (new StopwatchPerformanceTracker("ProcessOnePageOfADResults.ADQuery", this.stats))
			{
				using (new CpuPerformanceTracker("ProcessOnePageOfADResults.ADQuery", this.stats))
				{
					using (new ADPerformanceTracker("ProcessOnePageOfADResults.ADQuery", this.stats))
					{
						nextPage = this.GetNextPage();
					}
				}
			}
			if (nextPage == null || nextPage.Length == 0)
			{
				return nextPage;
			}
			using (new StopwatchPerformanceTracker("ProcessOnePageOfADResults.SortADResults", this.stats))
			{
				using (new CpuPerformanceTracker("ProcessOnePageOfADResults.SortADResults", this.stats))
				{
					Array.Sort<ADRawEntry>(nextPage, ADAddressListEnumerator.exchangeObjectIdComparer);
				}
			}
			return nextPage;
		}

		// Token: 0x04000B28 RID: 2856
		private static IComparer<ADRawEntry> exchangeObjectIdComparer = new ADAddressListEnumerator.ExchangeObjectIdComparer();

		// Token: 0x04000B29 RID: 2857
		private IRecipientSession session;

		// Token: 0x04000B2A RID: 2858
		private GenerationStats stats;

		// Token: 0x020001DC RID: 476
		private sealed class ExchangeObjectIdComparer : IComparer<ADRawEntry>
		{
			// Token: 0x06001241 RID: 4673 RVA: 0x00068720 File Offset: 0x00066920
			public int Compare(ADRawEntry x, ADRawEntry y)
			{
				return ((Guid)x[ADObjectSchema.ExchangeObjectId]).CompareTo((Guid)y[ADObjectSchema.ExchangeObjectId]);
			}
		}
	}
}
