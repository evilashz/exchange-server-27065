using System;

namespace Microsoft.Exchange.EdgeSync.Validation
{
	// Token: 0x02000046 RID: 70
	internal class MessageClassificationValidator : ConfigValidator
	{
		// Token: 0x060001DC RID: 476 RVA: 0x0000917A File Offset: 0x0000737A
		public MessageClassificationValidator(ReplicationTopology topology) : base(topology, "Message Classification")
		{
			base.ConfigDirectoryPath = "CN=Message Classifications,CN=Transport Settings";
			base.LdapQuery = Schema.Query.QueryAll;
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x060001DD RID: 477 RVA: 0x0000919E File Offset: 0x0000739E
		protected override string[] PayloadAttributes
		{
			get
			{
				return Schema.MessageClassification.PayloadAttributes;
			}
		}

		// Token: 0x060001DE RID: 478 RVA: 0x000091A5 File Offset: 0x000073A5
		protected override bool Filter(ExSearchResultEntry entry)
		{
			return !base.IsEntryContainer(entry);
		}

		// Token: 0x060001DF RID: 479 RVA: 0x000091B1 File Offset: 0x000073B1
		protected override bool FilterEdge(ExSearchResultEntry entry)
		{
			return !base.IsEntryContainer(entry);
		}
	}
}
