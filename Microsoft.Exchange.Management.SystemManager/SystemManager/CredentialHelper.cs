using System;
using System.Globalization;
using System.Management.Automation;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using Microsoft.Exchange.Extensions;
using Microsoft.Exchange.Management.SystemManager.WinForms;
using Microsoft.Exchange.ManagementGUI.Resources;
using Microsoft.ManagementGUI.WinForms;

namespace Microsoft.Exchange.Management.SystemManager
{
	// Token: 0x020002A5 RID: 677
	public static class CredentialHelper
	{
		// Token: 0x06001C88 RID: 7304 RVA: 0x0007B7AC File Offset: 0x000799AC
		public static PSCredential PromptForCredentials(IntPtr parentWindow, string displayName, Uri targetUri, string errorMessage, ref bool pfSave)
		{
			string text = errorMessage;
			NativeMethods.CREDUI_INFO credUiInfo;
			credUiInfo..ctor(parentWindow);
			credUiInfo.pszCaptionText = Strings.CredentialInputDialogTitle(displayName);
			NativeMethods.CredUIReturnCodes credUIReturnCodes;
			for (;;)
			{
				credUiInfo.pszMessageText = (text ?? string.Empty);
				string userName;
				SecureString password;
				credUIReturnCodes = (WinformsHelper.IsWin7OrLater() ? CredentialHelper.SspiPromptForCredentials(credUiInfo, targetUri, ref pfSave, out userName, out password) : CredentialHelper.CredUIPromptForCredentials(credUiInfo, ref pfSave, out userName, out password));
				if (credUIReturnCodes == null)
				{
					try
					{
						return new PSCredential(userName, password);
					}
					catch (PSArgumentException)
					{
						text = Strings.InvalidUserNameOrPassword;
						continue;
					}
				}
				if (credUIReturnCodes == 1223)
				{
					break;
				}
				if (credUIReturnCodes != 1315)
				{
					goto IL_8F;
				}
				text = Strings.InvalidUserNameOrPassword;
			}
			return null;
			IL_8F:
			throw new InvalidOperationException(string.Format("PromptForCredentials failed with error {0}.", credUIReturnCodes.ToString()));
		}

		// Token: 0x06001C89 RID: 7305 RVA: 0x0007B878 File Offset: 0x00079A78
		public static PSCredential ReadCredential(string credentialKey)
		{
			NativeMethods.Credential credential;
			NativeMethods.CredRead(credentialKey, 1, 0U, ref credential);
			if (credential == null)
			{
				return null;
			}
			return new PSCredential(credential.UserName, credential.Password);
		}

		// Token: 0x06001C8A RID: 7306 RVA: 0x0007B8A8 File Offset: 0x00079AA8
		public static bool SaveCredential(string credentialKey, PSCredential credential)
		{
			NativeMethods.Credential credential2 = new NativeMethods.Credential(credentialKey, credential.UserName, credential.Password);
			return NativeMethods.CredWrite(credential2, 0U);
		}

		// Token: 0x06001C8B RID: 7307 RVA: 0x0007B8CF File Offset: 0x00079ACF
		public static void RemoveCredential(string credentialKey)
		{
			NativeMethods.CredDelete(credentialKey, 1, 0);
		}

		// Token: 0x06001C8C RID: 7308 RVA: 0x0007B8DC File Offset: 0x00079ADC
		private static NativeMethods.CredUIReturnCodes SspiPromptForCredentials(NativeMethods.CREDUI_INFO credUiInfo, Uri targetUri, ref bool pfSave, out string userName, out SecureString password)
		{
			IntPtr zero = IntPtr.Zero;
			NativeMethods.CredUIReturnCodes credUIReturnCodes = NativeMethods.SspiPromptForCredentials(targetUri.Host, ref credUiInfo, 0U, "Negotiate", IntPtr.Zero, ref zero, ref pfSave, 1);
			if (credUIReturnCodes == null)
			{
				try
				{
					CredentialHelper.AuthIdentityToCredential(zero, out userName, out password);
					return credUIReturnCodes;
				}
				finally
				{
					NativeMethods.SspiFreeAuthIdentity(zero);
				}
			}
			userName = null;
			password = null;
			return credUIReturnCodes;
		}

		// Token: 0x06001C8D RID: 7309 RVA: 0x0007B93C File Offset: 0x00079B3C
		private static NativeMethods.CredUIReturnCodes CredUIPromptForCredentials(NativeMethods.CREDUI_INFO credUiInfo, ref bool pfSave, out string userName, out SecureString password)
		{
			StringBuilder stringBuilder = new StringBuilder(CredentialHelper.MaxUserNameLength);
			StringBuilder stringBuilder2 = new StringBuilder(CredentialHelper.MaxPasswordLength);
			NativeMethods.CredUIReturnCodes credUIReturnCodes = NativeMethods.CredUIPromptForCredentials(ref credUiInfo, null, IntPtr.Zero, 0U, stringBuilder, stringBuilder.Capacity, stringBuilder2, stringBuilder2.Capacity, ref pfSave, 262338);
			userName = ((credUIReturnCodes == null) ? stringBuilder.ToString() : null);
			password = ((credUIReturnCodes == null) ? stringBuilder2.ToString().ConvertToSecureString() : null);
			return credUIReturnCodes;
		}

		// Token: 0x06001C8E RID: 7310 RVA: 0x0007B9A4 File Offset: 0x00079BA4
		private static void AuthIdentityToCredential(IntPtr authIdentity, out string userName, out SecureString password)
		{
			StringBuilder stringBuilder = new StringBuilder();
			StringBuilder stringBuilder2 = new StringBuilder();
			StringBuilder stringBuilder3 = new StringBuilder();
			NativeMethods.SspiEncodeReturnCode sspiEncodeReturnCode = NativeMethods.SspiEncodeAuthIdentityAsStrings(authIdentity, ref stringBuilder, ref stringBuilder2, ref stringBuilder3);
			if (sspiEncodeReturnCode != null)
			{
				throw new InvalidOperationException(string.Format("AuthIdentityToCredential failed with error {0}", sspiEncodeReturnCode.ToString()));
			}
			userName = null;
			if (!string.IsNullOrEmpty(stringBuilder2.ToString()))
			{
				userName = string.Format(CultureInfo.InvariantCulture, "{0}\\{1}", new object[]
				{
					stringBuilder2.ToString(),
					stringBuilder.ToString()
				});
			}
			else
			{
				userName = stringBuilder.ToString();
			}
			password = stringBuilder3.ToString().ConvertToSecureString();
		}

		// Token: 0x06001C8F RID: 7311 RVA: 0x0007BA44 File Offset: 0x00079C44
		public static bool ForceConnection(Uri uri)
		{
			CredentialHelper.IWinHttpRequest winHttpRequest = (CredentialHelper.IWinHttpRequest)new CredentialHelper.WinHttpRequestClass();
			bool result;
			try
			{
				winHttpRequest.Open("GET", uri.ToString(), null);
				winHttpRequest.SetAutoLogonPolicy(CredentialHelper.WinHttpRequestAutoLogonPolicy.AutoLogonPolicy_Never);
				winHttpRequest.Send(null);
				result = true;
			}
			catch (COMException)
			{
				result = false;
			}
			catch (InvalidComObjectException)
			{
				result = false;
			}
			catch (TargetException)
			{
				result = false;
			}
			return result;
		}

		// Token: 0x04000AAA RID: 2730
		private const string NEGOSSP_NAME_W = "Negotiate";

		// Token: 0x04000AAB RID: 2731
		private static readonly int MaxUserNameLength = 1024;

		// Token: 0x04000AAC RID: 2732
		private static readonly int MaxPasswordLength = 127;

		// Token: 0x020002A6 RID: 678
		[Guid("2087C2F4-2CEF-4953-A8AB-66779B670495")]
		[ComImport]
		private class WinHttpRequestClass
		{
			// Token: 0x06001C91 RID: 7313
			[MethodImpl(MethodImplOptions.InternalCall)]
			public extern WinHttpRequestClass();
		}

		// Token: 0x020002A7 RID: 679
		[Guid("9D8A6DF8-13DE-4B1F-A330-67C719D62514")]
		public enum WinHttpRequestAutoLogonPolicy
		{
			// Token: 0x04000AAE RID: 2734
			AutoLogonPolicy_Always,
			// Token: 0x04000AAF RID: 2735
			AutoLogonPolicy_OnlyIfBypassProxy,
			// Token: 0x04000AB0 RID: 2736
			AutoLogonPolicy_Never
		}

		// Token: 0x020002A8 RID: 680
		[InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
		[Guid("016FE2EC-B2C8-45F8-B23B-39E53A75396B")]
		[ComImport]
		public interface IWinHttpRequest
		{
			// Token: 0x06001C92 RID: 7314
			[DispId(1)]
			[MethodImpl(MethodImplOptions.InternalCall)]
			void Open([MarshalAs(UnmanagedType.BStr)] [In] string Method, [MarshalAs(UnmanagedType.BStr)] [In] string Url, [MarshalAs(UnmanagedType.Struct)] [In] [Optional] object Async);

			// Token: 0x06001C93 RID: 7315
			[DispId(5)]
			[MethodImpl(MethodImplOptions.InternalCall)]
			void Send([MarshalAs(UnmanagedType.Struct)] [In] [Optional] object Body);

			// Token: 0x06001C94 RID: 7316
			[DispId(18)]
			[MethodImpl(MethodImplOptions.InternalCall)]
			void SetAutoLogonPolicy([In] CredentialHelper.WinHttpRequestAutoLogonPolicy AutoLogonPolicy);
		}
	}
}
