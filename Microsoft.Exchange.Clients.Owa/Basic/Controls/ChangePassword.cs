using System;
using System.IO;
using System.Security;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Clients.Owa.Basic.Controls
{
	// Token: 0x02000025 RID: 37
	public class ChangePassword : OptionsBase
	{
		// Token: 0x06000104 RID: 260 RVA: 0x00008D80 File Offset: 0x00006F80
		public ChangePassword(OwaContext owaContext, TextWriter writer) : base(owaContext, writer)
		{
			this.owaContext = owaContext;
			this.CommitAndLoad();
		}

		// Token: 0x06000105 RID: 261 RVA: 0x00008D98 File Offset: 0x00006F98
		private void CommitAndLoad()
		{
			if (Utilities.IsPostRequest(this.request) && !string.IsNullOrEmpty(base.Command))
			{
				using (SecureString secureFormParameter = Utilities.GetSecureFormParameter(this.request, "txtOldPwd", true))
				{
					using (SecureString secureFormParameter2 = Utilities.GetSecureFormParameter(this.request, "txtNewPwd", true))
					{
						using (SecureString secureFormParameter3 = Utilities.GetSecureFormParameter(this.request, "txtConfirmPwd", true))
						{
							if (!Utilities.SecureStringEquals(secureFormParameter2, secureFormParameter3))
							{
								base.SetInfobarMessage(LocalizedStrings.GetNonEncoded(51397275), InfobarMessageType.Error);
							}
							else
							{
								switch (Utilities.ChangePassword(this.owaContext.LogonIdentity, secureFormParameter, secureFormParameter2))
								{
								case Utilities.ChangePasswordResult.Success:
									this.owaContext.HttpContext.Response.Redirect(OwaUrl.LogoffChangePassword.GetExplicitUrl(this.request) + "&canary=" + Utilities.UrlEncode(Utilities.GetCurrentCanary(this.userContext)), false);
									this.owaContext.HttpContext.ApplicationInstance.CompleteRequest();
									break;
								case Utilities.ChangePasswordResult.InvalidCredentials:
									base.SetInfobarMessage(LocalizedStrings.GetNonEncoded(866665304), InfobarMessageType.Error);
									break;
								case Utilities.ChangePasswordResult.LockedOut:
									base.SetInfobarMessage(LocalizedStrings.GetNonEncoded(-1179631159), InfobarMessageType.Error);
									break;
								case Utilities.ChangePasswordResult.BadNewPassword:
									base.SetInfobarMessage(LocalizedStrings.GetNonEncoded(-782268049), InfobarMessageType.Error);
									break;
								case Utilities.ChangePasswordResult.OtherError:
									base.SetInfobarMessage(LocalizedStrings.GetNonEncoded(-1821890470), InfobarMessageType.Error);
									break;
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x06000106 RID: 262 RVA: 0x00008F60 File Offset: 0x00007160
		public override void Render()
		{
			this.RenderChangePassword();
		}

		// Token: 0x06000107 RID: 263 RVA: 0x00008F68 File Offset: 0x00007168
		public override void RenderScript()
		{
			base.RenderJSVariable("g_sbOptPg", "ChgPwdWndLd");
		}

		// Token: 0x06000108 RID: 264 RVA: 0x00008F7A File Offset: 0x0000717A
		private void RenderHelpText(string s)
		{
			this.writer.Write("<div class=spc>");
			this.writer.Write(s);
			this.writer.Write("</div>");
		}

		// Token: 0x06000109 RID: 265 RVA: 0x00008FA8 File Offset: 0x000071A8
		private void RenderDomainUsername(string domainUsername)
		{
			this.writer.Write("<tr><td>");
			if (VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).OwaDeployment.IsLogonFormatEmail.Enabled)
			{
				this.writer.Write(LocalizedStrings.GetHtmlEncoded(-1568335488));
			}
			else
			{
				this.writer.Write(LocalizedStrings.GetHtmlEncoded(50262124));
			}
			this.writer.Write("</td><td><span class=\"indnt1\">");
			this.writer.Write(domainUsername);
			this.writer.Write("</span></td></tr>");
		}

		// Token: 0x0600010A RID: 266 RVA: 0x00009040 File Offset: 0x00007240
		private void RenderPasswordTextBox(string id, string description)
		{
			this.writer.Write("<tr><td><label for=\"");
			this.writer.Write(id);
			this.writer.Write("\">");
			this.writer.Write(description);
			this.writer.Write("</label></td><td><span class=\"indnt1\"><input type=\"password\" name=\"");
			this.writer.Write(id);
			this.writer.Write("\" id=\"");
			this.writer.Write(id);
			this.writer.Write("\" class=\"edt\" autocomplete=\"off\"></span></td></tr>");
		}

		// Token: 0x0600010B RID: 267 RVA: 0x000090D0 File Offset: 0x000072D0
		private void RenderChangePassword()
		{
			string s;
			try
			{
				if (VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).OwaDeployment.IsLogonFormatEmail.Enabled)
				{
					s = Utilities.HtmlEncode(this.owaContext.LogonIdentity.PrimarySmtpAddress.ToString());
				}
				else
				{
					s = Utilities.HtmlEncode(this.owaContext.LogonIdentity.GetLogonName());
				}
			}
			catch (OwaIdentityException innerException)
			{
				throw new OwaChangePasswordTransientException(Strings.ChangePasswordFailedGetName, innerException);
			}
			base.RenderHeaderRow(ThemeFileId.ChangePassword, -392390655);
			this.writer.Write("<tr><td class=\"bd\">");
			this.RenderHelpText(LocalizedStrings.GetHtmlEncoded(-295337682));
			this.RenderHelpText(LocalizedStrings.GetHtmlEncoded(-255077324));
			this.writer.Write("<div class=\"indnt1\">");
			this.writer.Write("<table class=\"pwd\">");
			this.RenderDomainUsername(Utilities.HtmlEncode(s));
			this.RenderPasswordTextBox("txtOldPwd", LocalizedStrings.GetHtmlEncoded(328139404));
			this.RenderPasswordTextBox("txtNewPwd", LocalizedStrings.GetHtmlEncoded(-1594174847));
			this.RenderPasswordTextBox("txtConfirmPwd", LocalizedStrings.GetHtmlEncoded(1735206531));
			this.writer.Write("</table>");
			this.writer.Write("</div>");
			this.writer.Write("</td></tr>");
		}

		// Token: 0x040000AE RID: 174
		private const string FormOldPassword = "txtOldPwd";

		// Token: 0x040000AF RID: 175
		private const string FormNewPassword = "txtNewPwd";

		// Token: 0x040000B0 RID: 176
		private const string FormConfirmPassword = "txtConfirmPwd";

		// Token: 0x040000B1 RID: 177
		private OwaContext owaContext;
	}
}
