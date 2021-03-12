using System;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x02000156 RID: 342
	public class Logoff : OwaPage
	{
		// Token: 0x06000BC0 RID: 3008 RVA: 0x00051EAA File Offset: 0x000500AA
		protected override void OnLoad(EventArgs e)
		{
			if (Utilities.IsChangePasswordLogoff(base.Request))
			{
				this.reason = Logoff.LogoffReason.ChangePassword;
			}
			base.OnLoad(e);
		}

		// Token: 0x17000329 RID: 809
		// (get) Token: 0x06000BC1 RID: 3009 RVA: 0x00051EC7 File Offset: 0x000500C7
		protected Logoff.LogoffReason Reason
		{
			get
			{
				return this.reason;
			}
		}

		// Token: 0x1700032A RID: 810
		// (get) Token: 0x06000BC2 RID: 3010 RVA: 0x00051ECF File Offset: 0x000500CF
		protected override bool UseStrictMode
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700032B RID: 811
		// (get) Token: 0x06000BC3 RID: 3011 RVA: 0x00051ED4 File Offset: 0x000500D4
		protected string Message
		{
			get
			{
				if (this.Reason != Logoff.LogoffReason.ChangePassword)
				{
					return LocalizedStrings.GetHtmlEncoded(1735477837);
				}
				if (Utilities.GetBrowserType(base.Request.UserAgent) == BrowserType.IE && !base.IsDownLevelClient)
				{
					return LocalizedStrings.GetHtmlEncoded(575439440);
				}
				return LocalizedStrings.GetHtmlEncoded(252488134);
			}
		}

		// Token: 0x1700032C RID: 812
		// (get) Token: 0x06000BC4 RID: 3012 RVA: 0x00051F25 File Offset: 0x00050125
		protected static bool ShouldClearAuthenticationCache
		{
			get
			{
				return OwaConfigurationManager.Configuration.ClientAuthCleanupLevel == ClientAuthCleanupLevels.High;
			}
		}

		// Token: 0x0400086E RID: 2158
		private Logoff.LogoffReason reason;

		// Token: 0x02000157 RID: 343
		protected enum LogoffReason
		{
			// Token: 0x04000870 RID: 2160
			UserInitiated,
			// Token: 0x04000871 RID: 2161
			ChangePassword
		}
	}
}
