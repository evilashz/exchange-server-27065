using System;

namespace Microsoft.Exchange.EdgeSync.Validation
{
	// Token: 0x0200004A RID: 74
	internal sealed class RemoteDomainValidator : ConfigValidator
	{
		// Token: 0x060001F3 RID: 499 RVA: 0x00009B0E File Offset: 0x00007D0E
		public RemoteDomainValidator(ReplicationTopology topology) : base(topology, "Remote Domain")
		{
			base.ConfigDirectoryPath = "CN=Internet Message Formats,CN=Global Settings";
			base.LdapQuery = Schema.Query.QueryAll;
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x060001F4 RID: 500 RVA: 0x00009B32 File Offset: 0x00007D32
		protected override string[] PayloadAttributes
		{
			get
			{
				return Schema.DomainConfig.PayloadAttributes;
			}
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x00009B39 File Offset: 0x00007D39
		protected override bool Filter(ExSearchResultEntry entry)
		{
			return !base.IsEntryContainer(entry);
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x00009B45 File Offset: 0x00007D45
		protected override bool FilterEdge(ExSearchResultEntry entry)
		{
			return !base.IsEntryContainer(entry);
		}
	}
}
