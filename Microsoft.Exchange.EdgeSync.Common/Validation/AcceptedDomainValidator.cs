using System;

namespace Microsoft.Exchange.EdgeSync.Validation
{
	// Token: 0x0200003A RID: 58
	internal class AcceptedDomainValidator : ConfigValidator
	{
		// Token: 0x06000178 RID: 376 RVA: 0x00008384 File Offset: 0x00006584
		public AcceptedDomainValidator(ReplicationTopology topology) : base(topology, "Accepted Domain")
		{
			base.ConfigDirectoryPath = "CN=Accepted Domains,CN=Transport Settings";
			base.LdapQuery = Schema.Query.QueryAll;
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x06000179 RID: 377 RVA: 0x000083A8 File Offset: 0x000065A8
		protected override string[] PayloadAttributes
		{
			get
			{
				return Schema.AcceptedDomain.PayloadAttributes;
			}
		}

		// Token: 0x0600017A RID: 378 RVA: 0x000083AF File Offset: 0x000065AF
		protected override bool Filter(ExSearchResultEntry entry)
		{
			return !base.IsEntryContainer(entry);
		}

		// Token: 0x0600017B RID: 379 RVA: 0x000083BB File Offset: 0x000065BB
		protected override bool FilterEdge(ExSearchResultEntry entry)
		{
			return !base.IsEntryContainer(entry);
		}
	}
}
