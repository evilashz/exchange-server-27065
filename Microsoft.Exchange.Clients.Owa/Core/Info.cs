using System;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x0200012B RID: 299
	public class Info : OwaPage
	{
		// Token: 0x060009EB RID: 2539 RVA: 0x00045470 File Offset: 0x00043670
		protected override void OnLoad(EventArgs e)
		{
			string queryStringParameter = Utilities.GetQueryStringParameter(base.Request, "Msg", false);
			if (!string.IsNullOrEmpty(queryStringParameter))
			{
				if (string.Compare(queryStringParameter, "1", StringComparison.OrdinalIgnoreCase) == 0)
				{
					this.message = Info.InfoMessage.FailedToSaveUserCulture;
					return;
				}
			}
			else
			{
				this.message = Info.InfoMessage.Unknown;
			}
		}

		// Token: 0x170002DC RID: 732
		// (get) Token: 0x060009EC RID: 2540 RVA: 0x000454B4 File Offset: 0x000436B4
		protected Info.InfoMessage Message
		{
			get
			{
				return this.message;
			}
		}

		// Token: 0x170002DD RID: 733
		// (get) Token: 0x060009ED RID: 2541 RVA: 0x000454BC File Offset: 0x000436BC
		protected string UrlTitle
		{
			get
			{
				string arg = Utilities.HtmlEncode(base.OwaContext.MailboxIdentity.GetOWAMiniRecipient().DisplayName);
				return string.Format(LocalizedStrings.GetHtmlEncoded(-456269480), arg);
			}
		}

		// Token: 0x0400075D RID: 1885
		private Info.InfoMessage message;

		// Token: 0x0200012C RID: 300
		protected enum InfoMessage
		{
			// Token: 0x0400075F RID: 1887
			Unknown,
			// Token: 0x04000760 RID: 1888
			FailedToSaveUserCulture
		}
	}
}
