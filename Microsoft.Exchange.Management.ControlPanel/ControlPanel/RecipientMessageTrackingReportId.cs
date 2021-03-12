using System;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020002E2 RID: 738
	internal class RecipientMessageTrackingReportId
	{
		// Token: 0x06002CF2 RID: 11506 RVA: 0x0008A03C File Offset: 0x0008823C
		public RecipientMessageTrackingReportId(string messageTractingReportId, string recipient)
		{
			this.MessageTrackingReportId = messageTractingReportId;
			this.Recipient = recipient;
			this.RawIdentity = messageTractingReportId + ((recipient != null) ? (",Recip=" + recipient) : string.Empty);
		}

		// Token: 0x06002CF3 RID: 11507 RVA: 0x0008A073 File Offset: 0x00088273
		private RecipientMessageTrackingReportId(string rawIdentity)
		{
			this.RawIdentity = rawIdentity;
		}

		// Token: 0x17001E12 RID: 7698
		// (get) Token: 0x06002CF4 RID: 11508 RVA: 0x0008A082 File Offset: 0x00088282
		// (set) Token: 0x06002CF5 RID: 11509 RVA: 0x0008A08A File Offset: 0x0008828A
		public string Recipient { get; private set; }

		// Token: 0x17001E13 RID: 7699
		// (get) Token: 0x06002CF6 RID: 11510 RVA: 0x0008A093 File Offset: 0x00088293
		// (set) Token: 0x06002CF7 RID: 11511 RVA: 0x0008A09B File Offset: 0x0008829B
		public string MessageTrackingReportId { get; private set; }

		// Token: 0x17001E14 RID: 7700
		// (get) Token: 0x06002CF8 RID: 11512 RVA: 0x0008A0A4 File Offset: 0x000882A4
		// (set) Token: 0x06002CF9 RID: 11513 RVA: 0x0008A0AC File Offset: 0x000882AC
		public string RawIdentity { get; private set; }

		// Token: 0x06002CFA RID: 11514 RVA: 0x0008A0B5 File Offset: 0x000882B5
		public static RecipientMessageTrackingReportId Parse(Identity identity)
		{
			return RecipientMessageTrackingReportId.Parse(identity.RawIdentity);
		}

		// Token: 0x06002CFB RID: 11515 RVA: 0x0008A0C4 File Offset: 0x000882C4
		public static RecipientMessageTrackingReportId Parse(string rawIdentity)
		{
			RecipientMessageTrackingReportId recipientMessageTrackingReportId = new RecipientMessageTrackingReportId(rawIdentity);
			recipientMessageTrackingReportId.ParseRawIdentity();
			return recipientMessageTrackingReportId;
		}

		// Token: 0x06002CFC RID: 11516 RVA: 0x0008A0E0 File Offset: 0x000882E0
		private void ParseRawIdentity()
		{
			int num = this.RawIdentity.LastIndexOf(",Recip=");
			if (num > 0)
			{
				this.MessageTrackingReportId = this.RawIdentity.Substring(0, num);
				this.Recipient = this.RawIdentity.Substring(num + ",Recip=".Length);
				return;
			}
			this.MessageTrackingReportId = this.RawIdentity;
		}

		// Token: 0x0400222D RID: 8749
		internal const string RecipientParameterPrefix = ",Recip=";
	}
}
