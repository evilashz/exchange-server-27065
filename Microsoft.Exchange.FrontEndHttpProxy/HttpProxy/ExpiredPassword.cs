using System;
using System.Runtime.InteropServices;
using System.Security;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Extensions;
using Microsoft.Exchange.Security.Authentication;

namespace Microsoft.Exchange.HttpProxy
{
	// Token: 0x02000054 RID: 84
	public class ExpiredPassword : OwaPage
	{
		// Token: 0x17000099 RID: 153
		// (get) Token: 0x060002B2 RID: 690 RVA: 0x0000F024 File Offset: 0x0000D224
		protected ExpiredPassword.ExpiredPasswordReason Reason
		{
			get
			{
				return this.reason;
			}
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x060002B3 RID: 691 RVA: 0x0000F02C File Offset: 0x0000D22C
		protected string Destination
		{
			get
			{
				string text = base.Request.Form["url"];
				if (string.IsNullOrEmpty(text))
				{
					return "../";
				}
				return text;
			}
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x060002B4 RID: 692 RVA: 0x0000F060 File Offset: 0x0000D260
		protected string UserNameLabel
		{
			get
			{
				switch (OwaVdirConfiguration.Instance.LogonFormat)
				{
				case LogonFormats.PrincipalName:
					return LocalizedStrings.GetHtmlEncoded(1677919363);
				case LogonFormats.UserName:
					return LocalizedStrings.GetHtmlEncoded(537815319);
				}
				return LocalizedStrings.GetHtmlEncoded(78658498);
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x060002B5 RID: 693 RVA: 0x0000F0AC File Offset: 0x0000D2AC
		protected bool PasswordChanged
		{
			get
			{
				return this.passwordChanged;
			}
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x060002B6 RID: 694 RVA: 0x0000F0B4 File Offset: 0x0000D2B4
		protected bool ShouldClearAuthenticationCache
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x060002B7 RID: 695 RVA: 0x0000F0B7 File Offset: 0x0000D2B7
		protected override bool UseStrictMode
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060002B8 RID: 696
		[DllImport("netapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		public static extern uint NetUserChangePassword(string domainname, string username, IntPtr oldpassword, IntPtr newpassword);

		// Token: 0x060002B9 RID: 697 RVA: 0x0000F0BA File Offset: 0x0000D2BA
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			this.reason = ExpiredPassword.ExpiredPasswordReason.None;
			this.passwordChanged = false;
			this.ChangePassword();
			if (this.passwordChanged)
			{
				Utility.DeleteFbaAuthCookies(base.Request, base.Response);
			}
		}

		// Token: 0x060002BA RID: 698 RVA: 0x0000F0F0 File Offset: 0x0000D2F0
		private static ExpiredPassword.ChangePasswordResult ChangePasswordNUCP(string logonName, SecureString oldPassword, SecureString newPassword)
		{
			if (logonName == null || oldPassword == null || newPassword == null)
			{
				throw new ArgumentNullException();
			}
			string text = string.Empty;
			string text2 = string.Empty;
			switch (OwaVdirConfiguration.Instance.LogonFormat)
			{
			case LogonFormats.FullDomain:
				ExpiredPassword.GetDomainUser(logonName, ref text, ref text2);
				break;
			case LogonFormats.PrincipalName:
				text = NativeHelpers.GetDomainName();
				text2 = logonName;
				break;
			case LogonFormats.UserName:
				if (logonName.IndexOf("\\") == -1)
				{
					text2 = logonName;
					text = NativeHelpers.GetDomainName();
				}
				else
				{
					ExpiredPassword.GetDomainUser(logonName, ref text, ref text2);
				}
				break;
			}
			if (text == string.Empty || text2 == string.Empty)
			{
				return ExpiredPassword.ChangePasswordResult.OtherError;
			}
			IntPtr intPtr = IntPtr.Zero;
			IntPtr intPtr2 = IntPtr.Zero;
			try
			{
				intPtr = Marshal.SecureStringToGlobalAllocUnicode(oldPassword);
				intPtr2 = Marshal.SecureStringToGlobalAllocUnicode(newPassword);
				uint num = ExpiredPassword.NetUserChangePassword(text, text2, intPtr, intPtr2);
				if (num != 0U)
				{
					uint num2 = num;
					if (num2 == 5U)
					{
						return ExpiredPassword.ChangePasswordResult.LockedOut;
					}
					if (num2 == 86U)
					{
						return ExpiredPassword.ChangePasswordResult.InvalidCredentials;
					}
					if (num2 != 2245U)
					{
						return ExpiredPassword.ChangePasswordResult.OtherError;
					}
					return ExpiredPassword.ChangePasswordResult.BadNewPassword;
				}
			}
			finally
			{
				if (intPtr != IntPtr.Zero)
				{
					Marshal.ZeroFreeGlobalAllocUnicode(intPtr);
				}
				if (intPtr2 != IntPtr.Zero)
				{
					Marshal.ZeroFreeGlobalAllocUnicode(intPtr2);
				}
			}
			return ExpiredPassword.ChangePasswordResult.Success;
		}

		// Token: 0x060002BB RID: 699 RVA: 0x0000F224 File Offset: 0x0000D424
		private static void GetDomainUser(string logonName, ref string domain, ref string user)
		{
			string[] array = logonName.Split(new char[]
			{
				'\\'
			});
			if (array.Length == 2)
			{
				domain = array[0];
				user = array[1];
			}
		}

		// Token: 0x060002BC RID: 700 RVA: 0x0000F258 File Offset: 0x0000D458
		private static bool SecureStringEquals(SecureString secureStringA, SecureString secureStringB)
		{
			if (secureStringA == null || secureStringB == null || secureStringA.Length != secureStringB.Length)
			{
				return false;
			}
			using (SecureArray<char> secureArray = secureStringA.ConvertToSecureCharArray())
			{
				using (SecureArray<char> secureArray2 = secureStringB.ConvertToSecureCharArray())
				{
					for (int i = 0; i < secureStringA.Length; i++)
					{
						if (secureArray.ArrayValue[i] != secureArray2.ArrayValue[i])
						{
							return false;
						}
					}
				}
			}
			return true;
		}

		// Token: 0x060002BD RID: 701 RVA: 0x0000F2E8 File Offset: 0x0000D4E8
		private void ChangePassword()
		{
			SecureHtmlFormReader secureHtmlFormReader = new SecureHtmlFormReader(base.Request);
			secureHtmlFormReader.AddSensitiveInputName("oldPwd");
			secureHtmlFormReader.AddSensitiveInputName("newPwd1");
			secureHtmlFormReader.AddSensitiveInputName("newPwd2");
			SecureNameValueCollection secureNameValueCollection = null;
			try
			{
				if (secureHtmlFormReader.TryReadSecureFormData(out secureNameValueCollection))
				{
					string text = null;
					SecureString secureString = null;
					SecureString secureString2 = null;
					SecureString secureString3 = null;
					try
					{
						secureNameValueCollection.TryGetUnsecureValue("username", out text);
						secureNameValueCollection.TryGetSecureValue("oldPwd", out secureString);
						secureNameValueCollection.TryGetSecureValue("newPwd1", out secureString2);
						secureNameValueCollection.TryGetSecureValue("newPwd2", out secureString3);
						if (text != null && secureString != null && secureString2 != null && secureString3 != null)
						{
							if (!ExpiredPassword.SecureStringEquals(secureString2, secureString3))
							{
								this.reason = ExpiredPassword.ExpiredPasswordReason.PasswordConflict;
							}
							else
							{
								switch (ExpiredPassword.ChangePasswordNUCP(text, secureString, secureString2))
								{
								case ExpiredPassword.ChangePasswordResult.Success:
									this.reason = ExpiredPassword.ExpiredPasswordReason.None;
									this.passwordChanged = true;
									break;
								case ExpiredPassword.ChangePasswordResult.InvalidCredentials:
									this.reason = ExpiredPassword.ExpiredPasswordReason.InvalidCredentials;
									break;
								case ExpiredPassword.ChangePasswordResult.LockedOut:
									this.reason = ExpiredPassword.ExpiredPasswordReason.LockedOut;
									break;
								case ExpiredPassword.ChangePasswordResult.BadNewPassword:
									this.reason = ExpiredPassword.ExpiredPasswordReason.InvalidNewPassword;
									break;
								case ExpiredPassword.ChangePasswordResult.OtherError:
									this.reason = ExpiredPassword.ExpiredPasswordReason.InvalidCredentials;
									break;
								}
							}
						}
					}
					finally
					{
						secureString.Dispose();
						secureString2.Dispose();
						secureString3.Dispose();
					}
				}
			}
			finally
			{
				if (secureNameValueCollection != null)
				{
					secureNameValueCollection.Dispose();
				}
			}
		}

		// Token: 0x04000179 RID: 377
		private const string DestinationParameter = "url";

		// Token: 0x0400017A RID: 378
		private const string DefaultDestination = "../";

		// Token: 0x0400017B RID: 379
		private const string UsernameParameter = "username";

		// Token: 0x0400017C RID: 380
		private const string OldPasswordParameter = "oldPwd";

		// Token: 0x0400017D RID: 381
		private const string NewPassword1Parameter = "newPwd1";

		// Token: 0x0400017E RID: 382
		private const string NewPassword2Parameter = "newPwd2";

		// Token: 0x0400017F RID: 383
		private const int NetUserChangePasswordSuccess = 0;

		// Token: 0x04000180 RID: 384
		private const int NetUserChangePasswordAccessDenied = 5;

		// Token: 0x04000181 RID: 385
		private const int NetUserChangePasswordInvalidOldPassword = 86;

		// Token: 0x04000182 RID: 386
		private const int NetUserChangePasswordDoesNotMeetPolicyRequirement = 2245;

		// Token: 0x04000183 RID: 387
		private ExpiredPassword.ExpiredPasswordReason reason;

		// Token: 0x04000184 RID: 388
		private bool passwordChanged;

		// Token: 0x02000055 RID: 85
		protected enum ChangePasswordResult
		{
			// Token: 0x04000186 RID: 390
			Success,
			// Token: 0x04000187 RID: 391
			InvalidCredentials,
			// Token: 0x04000188 RID: 392
			LockedOut,
			// Token: 0x04000189 RID: 393
			BadNewPassword,
			// Token: 0x0400018A RID: 394
			OtherError
		}

		// Token: 0x02000056 RID: 86
		protected enum ExpiredPasswordReason
		{
			// Token: 0x0400018C RID: 396
			None,
			// Token: 0x0400018D RID: 397
			InvalidCredentials,
			// Token: 0x0400018E RID: 398
			InvalidNewPassword,
			// Token: 0x0400018F RID: 399
			PasswordConflict,
			// Token: 0x04000190 RID: 400
			LockedOut
		}
	}
}
