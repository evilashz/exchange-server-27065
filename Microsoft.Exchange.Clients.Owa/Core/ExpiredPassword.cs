using System;
using System.Security;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x0200010C RID: 268
	public class ExpiredPassword : OwaPage
	{
		// Token: 0x17000276 RID: 630
		// (get) Token: 0x060008FA RID: 2298 RVA: 0x00040C5F File Offset: 0x0003EE5F
		protected ExpiredPassword.ExpiredPasswordReason Reason
		{
			get
			{
				return this.reason;
			}
		}

		// Token: 0x17000277 RID: 631
		// (get) Token: 0x060008FB RID: 2299 RVA: 0x00040C68 File Offset: 0x0003EE68
		protected string Destination
		{
			get
			{
				string text = Utilities.GetQueryStringParameter(base.Request, "url", false) ?? Utilities.GetFormParameter(base.Request, "url", false);
				if (string.IsNullOrEmpty(text))
				{
					return "../owa14.aspx";
				}
				return text;
			}
		}

		// Token: 0x17000278 RID: 632
		// (get) Token: 0x060008FC RID: 2300 RVA: 0x00040CAC File Offset: 0x0003EEAC
		protected string UserNameLabel
		{
			get
			{
				switch (OwaConfigurationManager.Configuration.LogonFormat)
				{
				case LogonFormats.PrincipalName:
					return LocalizedStrings.GetHtmlEncoded(1677919363);
				case LogonFormats.UserName:
					return LocalizedStrings.GetHtmlEncoded(537815319);
				}
				return LocalizedStrings.GetHtmlEncoded(78658498);
			}
		}

		// Token: 0x17000279 RID: 633
		// (get) Token: 0x060008FD RID: 2301 RVA: 0x00040CF8 File Offset: 0x0003EEF8
		protected bool PasswordChanged
		{
			get
			{
				return this.passwordChanged;
			}
		}

		// Token: 0x1700027A RID: 634
		// (get) Token: 0x060008FE RID: 2302 RVA: 0x00040D00 File Offset: 0x0003EF00
		protected bool ShouldClearAuthenticationCache
		{
			get
			{
				return OwaConfigurationManager.Configuration.ClientAuthCleanupLevel == ClientAuthCleanupLevels.High;
			}
		}

		// Token: 0x1700027B RID: 635
		// (get) Token: 0x060008FF RID: 2303 RVA: 0x00040D0F File Offset: 0x0003EF0F
		protected override bool UseStrictMode
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000900 RID: 2304 RVA: 0x00040D12 File Offset: 0x0003EF12
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			this.reason = ExpiredPassword.ExpiredPasswordReason.None;
			this.passwordChanged = false;
			if (Globals.ChangeExpiredPasswordEnabled)
			{
				this.ChangePassword();
				if (this.passwordChanged)
				{
					Utilities.DeleteFBASessionCookies(base.Response);
				}
			}
		}

		// Token: 0x06000901 RID: 2305 RVA: 0x00040D4C File Offset: 0x0003EF4C
		private void ChangePassword()
		{
			string text = Utilities.GetFormParameter(base.Request, "username", false);
			using (SecureString secureFormParameter = Utilities.GetSecureFormParameter(base.Request, "oldPwd", false))
			{
				using (SecureString secureFormParameter2 = Utilities.GetSecureFormParameter(base.Request, "newPwd1", false))
				{
					using (SecureString secureFormParameter3 = Utilities.GetSecureFormParameter(base.Request, "newPwd2", false))
					{
						if (text != null && secureFormParameter != null && secureFormParameter2 != null && secureFormParameter3 != null)
						{
							if (!Utilities.SecureStringEquals(secureFormParameter2, secureFormParameter3))
							{
								this.reason = ExpiredPassword.ExpiredPasswordReason.PasswordConflict;
							}
							else
							{
								if (OwaConfigurationManager.Configuration.LogonFormat == LogonFormats.UserName && text.IndexOf("\\") == -1)
								{
									text = string.Format("{0}\\{1}", OwaConfigurationManager.Configuration.DefaultDomain, text);
								}
								switch (Utilities.ChangePassword(text, secureFormParameter, secureFormParameter2))
								{
								case Utilities.ChangePasswordResult.Success:
									this.reason = ExpiredPassword.ExpiredPasswordReason.None;
									this.passwordChanged = true;
									break;
								case Utilities.ChangePasswordResult.InvalidCredentials:
									this.reason = ExpiredPassword.ExpiredPasswordReason.InvalidCredentials;
									break;
								case Utilities.ChangePasswordResult.LockedOut:
									this.reason = ExpiredPassword.ExpiredPasswordReason.LockedOut;
									break;
								case Utilities.ChangePasswordResult.BadNewPassword:
									this.reason = ExpiredPassword.ExpiredPasswordReason.InvalidNewPassword;
									break;
								case Utilities.ChangePasswordResult.OtherError:
									this.reason = ExpiredPassword.ExpiredPasswordReason.InvalidCredentials;
									break;
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x0400064F RID: 1615
		private const string DestinationParameter = "url";

		// Token: 0x04000650 RID: 1616
		private const string DefaultDestination = "../owa14.aspx";

		// Token: 0x04000651 RID: 1617
		private const string UsernameParameter = "username";

		// Token: 0x04000652 RID: 1618
		private const string OldPasswordParameter = "oldPwd";

		// Token: 0x04000653 RID: 1619
		private const string NewPassword1Parameter = "newPwd1";

		// Token: 0x04000654 RID: 1620
		private const string NewPassword2Parameter = "newPwd2";

		// Token: 0x04000655 RID: 1621
		private ExpiredPassword.ExpiredPasswordReason reason;

		// Token: 0x04000656 RID: 1622
		private bool passwordChanged;

		// Token: 0x0200010D RID: 269
		protected enum ExpiredPasswordReason
		{
			// Token: 0x04000658 RID: 1624
			None,
			// Token: 0x04000659 RID: 1625
			InvalidCredentials,
			// Token: 0x0400065A RID: 1626
			InvalidNewPassword,
			// Token: 0x0400065B RID: 1627
			PasswordConflict,
			// Token: 0x0400065C RID: 1628
			LockedOut
		}
	}
}
