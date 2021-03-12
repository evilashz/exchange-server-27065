using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport.Sync.Manager.Throttling
{
	// Token: 0x0200004E RID: 78
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ConnectSubscriptionPolicySettings
	{
		// Token: 0x1700010D RID: 269
		// (get) Token: 0x060003BB RID: 955 RVA: 0x00017FCA File Offset: 0x000161CA
		internal bool IsFacebookDisabled
		{
			get
			{
				return !this.facebookEnabled;
			}
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x060003BC RID: 956 RVA: 0x00017FD5 File Offset: 0x000161D5
		internal bool IsLinkedInDisabled
		{
			get
			{
				return !this.linkedInEnabled;
			}
		}

		// Token: 0x060003BD RID: 957 RVA: 0x00017FE0 File Offset: 0x000161E0
		internal static ConnectSubscriptionPolicySettings GetFallbackInstance()
		{
			return new ConnectSubscriptionPolicySettings();
		}

		// Token: 0x060003BE RID: 958 RVA: 0x00017FE7 File Offset: 0x000161E7
		private ConnectSubscriptionPolicySettings()
		{
			this.facebookEnabled = true;
			this.linkedInEnabled = true;
		}

		// Token: 0x060003BF RID: 959 RVA: 0x00017FFD File Offset: 0x000161FD
		internal ConnectSubscriptionPolicySettings(OwaSegmentationSettings owaSegmentationSettings)
		{
			this.facebookEnabled = owaSegmentationSettings[OwaMailboxPolicySchema.FacebookEnabled];
			this.linkedInEnabled = owaSegmentationSettings[OwaMailboxPolicySchema.LinkedInEnabled];
		}

		// Token: 0x04000222 RID: 546
		private readonly bool facebookEnabled;

		// Token: 0x04000223 RID: 547
		private readonly bool linkedInEnabled;
	}
}
