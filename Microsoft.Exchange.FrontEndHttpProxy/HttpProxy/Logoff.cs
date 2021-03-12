using System;
using Microsoft.Exchange.Clients.Owa.Core;

namespace Microsoft.Exchange.HttpProxy
{
	// Token: 0x0200005B RID: 91
	public class Logoff : OwaPage
	{
		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x060002F0 RID: 752 RVA: 0x000126F0 File Offset: 0x000108F0
		protected Logoff.LogoffReason Reason
		{
			get
			{
				return this.reason;
			}
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x060002F1 RID: 753 RVA: 0x000126F8 File Offset: 0x000108F8
		protected override bool UseStrictMode
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x060002F2 RID: 754 RVA: 0x000126FB File Offset: 0x000108FB
		protected string Message
		{
			get
			{
				if (this.Reason != Logoff.LogoffReason.ChangePassword)
				{
					return LocalizedStrings.GetHtmlEncoded(1735477837);
				}
				if (base.IsDownLevelClient)
				{
					return LocalizedStrings.GetHtmlEncoded(252488134);
				}
				return LocalizedStrings.GetHtmlEncoded(575439440);
			}
		}

		// Token: 0x060002F3 RID: 755 RVA: 0x0001272E File Offset: 0x0001092E
		protected override void OnLoad(EventArgs e)
		{
			if (base.Request.IsChangePasswordLogoff())
			{
				this.reason = Logoff.LogoffReason.ChangePassword;
			}
			base.OnLoad(e);
		}

		// Token: 0x040001D8 RID: 472
		private Logoff.LogoffReason reason;

		// Token: 0x0200005C RID: 92
		protected enum LogoffReason
		{
			// Token: 0x040001DA RID: 474
			UserInitiated,
			// Token: 0x040001DB RID: 475
			ChangePassword
		}
	}
}
