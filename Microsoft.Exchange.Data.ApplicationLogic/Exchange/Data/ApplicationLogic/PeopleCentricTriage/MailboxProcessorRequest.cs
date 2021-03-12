using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.ApplicationLogic.PeopleCentricTriage
{
	// Token: 0x02000171 RID: 369
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class MailboxProcessorRequest
	{
		// Token: 0x1700036B RID: 875
		// (get) Token: 0x06000E66 RID: 3686 RVA: 0x0003BDAC File Offset: 0x00039FAC
		// (set) Token: 0x06000E67 RID: 3687 RVA: 0x0003BDB4 File Offset: 0x00039FB4
		public DateTime? LastLogonTime { get; set; }

		// Token: 0x1700036C RID: 876
		// (get) Token: 0x06000E68 RID: 3688 RVA: 0x0003BDBD File Offset: 0x00039FBD
		// (set) Token: 0x06000E69 RID: 3689 RVA: 0x0003BDC5 File Offset: 0x00039FC5
		public IStoreSession MailboxSession { get; set; }

		// Token: 0x1700036D RID: 877
		// (get) Token: 0x06000E6A RID: 3690 RVA: 0x0003BDCE File Offset: 0x00039FCE
		// (set) Token: 0x06000E6B RID: 3691 RVA: 0x0003BDD6 File Offset: 0x00039FD6
		public bool IsFlightEnabled { get; set; }

		// Token: 0x1700036E RID: 878
		// (get) Token: 0x06000E6C RID: 3692 RVA: 0x0003BDDF File Offset: 0x00039FDF
		// (set) Token: 0x06000E6D RID: 3693 RVA: 0x0003BDE7 File Offset: 0x00039FE7
		public bool IsPublicFolderMailbox { get; set; }

		// Token: 0x1700036F RID: 879
		// (get) Token: 0x06000E6E RID: 3694 RVA: 0x0003BDF0 File Offset: 0x00039FF0
		// (set) Token: 0x06000E6F RID: 3695 RVA: 0x0003BDF8 File Offset: 0x00039FF8
		public bool IsGroupMailbox { get; set; }

		// Token: 0x17000370 RID: 880
		// (get) Token: 0x06000E70 RID: 3696 RVA: 0x0003BE01 File Offset: 0x0003A001
		// (set) Token: 0x06000E71 RID: 3697 RVA: 0x0003BE09 File Offset: 0x0003A009
		public bool IsTeamSiteMailbox { get; set; }

		// Token: 0x17000371 RID: 881
		// (get) Token: 0x06000E72 RID: 3698 RVA: 0x0003BE12 File Offset: 0x0003A012
		// (set) Token: 0x06000E73 RID: 3699 RVA: 0x0003BE1A File Offset: 0x0003A01A
		public bool IsSharedMailbox { get; set; }

		// Token: 0x17000372 RID: 882
		// (get) Token: 0x06000E74 RID: 3700 RVA: 0x0003BE23 File Offset: 0x0003A023
		// (set) Token: 0x06000E75 RID: 3701 RVA: 0x0003BE2B File Offset: 0x0003A02B
		public string DiagnosticsText { get; set; }

		// Token: 0x06000E76 RID: 3702 RVA: 0x0003BE34 File Offset: 0x0003A034
		public override string ToString()
		{
			if (string.IsNullOrEmpty(this.DiagnosticsText))
			{
				return base.ToString();
			}
			return this.DiagnosticsText;
		}
	}
}
