using System;

namespace Microsoft.Exchange.Net.MonitoringWebClient
{
	// Token: 0x02000790 RID: 1936
	internal class LiveIdLogonErrorPage : LogonErrorPage
	{
		// Token: 0x06002673 RID: 9843 RVA: 0x00050ED4 File Offset: 0x0004F0D4
		private LiveIdLogonErrorPage(string errorMessage) : base(errorMessage)
		{
			if (errorMessage.IndexOf("user ID or password", StringComparison.OrdinalIgnoreCase) >= 0)
			{
				this.logonErrorType = LogonErrorType.BadUserNameOrPassword;
				return;
			}
			if (errorMessage.IndexOf("characters in the picture", StringComparison.OrdinalIgnoreCase) >= 0 || errorMessage.IndexOf("characters you entered didn't match", StringComparison.OrdinalIgnoreCase) >= 0)
			{
				this.logonErrorType = LogonErrorType.AccountLocked;
				return;
			}
			this.logonErrorType = LogonErrorType.Unknown;
		}

		// Token: 0x06002674 RID: 9844 RVA: 0x00050F2C File Offset: 0x0004F12C
		public static bool TryParse(HttpWebResponseWrapper response, out LiveIdLogonErrorPage liveIdLogonErrorPage)
		{
			string text;
			if (ParsingUtility.TryParseJavascriptStringVariable(response, "srf_sErr", out text))
			{
				liveIdLogonErrorPage = new LiveIdLogonErrorPage(text);
				return true;
			}
			text = ParsingUtility.ParseInnerHtml(response, "div", "cta_error_message_text");
			if (text != null)
			{
				liveIdLogonErrorPage = new LiveIdLogonErrorPage(ParsingUtility.RemoveHtmlTags(text));
				return true;
			}
			liveIdLogonErrorPage = null;
			return false;
		}
	}
}
