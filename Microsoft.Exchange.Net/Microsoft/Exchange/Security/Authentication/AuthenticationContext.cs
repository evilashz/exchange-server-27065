using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Net;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Security.Cryptography;

namespace Microsoft.Exchange.Security.Authentication
{
	// Token: 0x02000BBF RID: 3007
	internal class AuthenticationContext : DisposeTrackableBase
	{
		// Token: 0x0600407B RID: 16507 RVA: 0x000AAC9C File Offset: 0x000A8E9C
		public AuthenticationContext()
		{
		}

		// Token: 0x0600407C RID: 16508 RVA: 0x000AACAF File Offset: 0x000A8EAF
		public AuthenticationContext(ChannelBindingToken channelBindingToken)
		{
			this.channelBindingToken = channelBindingToken;
		}

		// Token: 0x0600407D RID: 16509 RVA: 0x000AACC9 File Offset: 0x000A8EC9
		public AuthenticationContext(ExtendedProtectionConfig extendedProtectionConfig, ChannelBindingToken channelBindingToken)
		{
			ArgumentValidator.ThrowIfNull("extendedProtectionConfig", extendedProtectionConfig);
			this.extendedProtectionConfig = extendedProtectionConfig;
			this.channelBindingToken = channelBindingToken;
		}

		// Token: 0x0600407E RID: 16510 RVA: 0x000AACF5 File Offset: 0x000A8EF5
		public AuthenticationContext(ExternalLoginAuthentication externalAuthFunction)
		{
			this.externalAuthentication = externalAuthFunction;
		}

		// Token: 0x0600407F RID: 16511 RVA: 0x000AAD0F File Offset: 0x000A8F0F
		public AuthenticationContext(ExternalProxyAuthentication externalAuthFunction)
		{
			this.externalProxyAuthentication = externalAuthFunction;
		}

		// Token: 0x06004080 RID: 16512 RVA: 0x000AAD29 File Offset: 0x000A8F29
		public AuthenticationContext(ExternalLoginProcessing externalLoginProcessingFunction)
		{
			this.externalLoginProcessing = externalLoginProcessingFunction;
		}

		// Token: 0x17000FD2 RID: 4050
		// (get) Token: 0x06004081 RID: 16513 RVA: 0x000AAD44 File Offset: 0x000A8F44
		// (set) Token: 0x06004082 RID: 16514 RVA: 0x000AAD69 File Offset: 0x000A8F69
		public WindowsIdentity Identity
		{
			get
			{
				WindowsIdentity result;
				if ((result = this.windowsIdentity) == null)
				{
					result = (this.windowsIdentity = WindowsIdentity.GetAnonymous());
				}
				return result;
			}
			set
			{
				this.windowsIdentity = value;
			}
		}

		// Token: 0x17000FD3 RID: 4051
		// (get) Token: 0x06004083 RID: 16515 RVA: 0x000AAD72 File Offset: 0x000A8F72
		public string CommonAccessToken
		{
			get
			{
				return this.commonAccessToken;
			}
		}

		// Token: 0x17000FD4 RID: 4052
		// (get) Token: 0x06004084 RID: 16516 RVA: 0x000AAD7A File Offset: 0x000A8F7A
		public IAccountValidationContext AccountValidationContext
		{
			get
			{
				return this.accountValidationContext;
			}
		}

		// Token: 0x17000FD5 RID: 4053
		// (get) Token: 0x06004085 RID: 16517 RVA: 0x000AAD84 File Offset: 0x000A8F84
		public bool IsWellKnownAdministrator
		{
			get
			{
				return this.CommonAccessToken == null && !(this.windowsIdentity.User == null) && this.windowsIdentity != null && this.windowsIdentity.User.Value.EndsWith("-500", StringComparison.Ordinal);
			}
		}

		// Token: 0x17000FD6 RID: 4054
		// (get) Token: 0x06004086 RID: 16518 RVA: 0x000AADD3 File Offset: 0x000A8FD3
		public bool IsAnonymous
		{
			get
			{
				return this.CommonAccessToken == null && (this.windowsIdentity == null || this.windowsIdentity.IsAnonymous);
			}
		}

		// Token: 0x17000FD7 RID: 4055
		// (get) Token: 0x06004087 RID: 16519 RVA: 0x000AADF4 File Offset: 0x000A8FF4
		public bool IsAuthenticated
		{
			get
			{
				return this.CommonAccessToken != null || (this.windowsIdentity != null && this.windowsIdentity.IsAuthenticated);
			}
		}

		// Token: 0x17000FD8 RID: 4056
		// (get) Token: 0x06004088 RID: 16520 RVA: 0x000AAE18 File Offset: 0x000A9018
		public bool IsGuest
		{
			get
			{
				if (this.CommonAccessToken != null)
				{
					return false;
				}
				if (this.windowsIdentity == null || this.windowsIdentity.User == null)
				{
					return false;
				}
				SecurityIdentifier user = this.windowsIdentity.User;
				return user.Value.EndsWith("-501", StringComparison.Ordinal) || user.IsWellKnown(WellKnownSidType.BuiltinGuestsSid) || user.IsWellKnown(WellKnownSidType.AccountGuestSid);
			}
		}

		// Token: 0x17000FD9 RID: 4057
		// (get) Token: 0x06004089 RID: 16521 RVA: 0x000AAE80 File Offset: 0x000A9080
		public bool IsSystem
		{
			get
			{
				if (this.CommonAccessToken != null)
				{
					return false;
				}
				if (this.windowsIdentity == null || this.windowsIdentity.User == null)
				{
					return false;
				}
				SecurityIdentifier user = this.windowsIdentity.User;
				return user.IsWellKnown(WellKnownSidType.LocalSystemSid) || user.IsWellKnown(WellKnownSidType.LocalServiceSid) || user.IsWellKnown(WellKnownSidType.NetworkServiceSid);
			}
		}

		// Token: 0x17000FDA RID: 4058
		// (get) Token: 0x0600408A RID: 16522 RVA: 0x000AAEE0 File Offset: 0x000A90E0
		public int MaxTokenSize
		{
			get
			{
				switch (this.negotiateMechanism)
				{
				case AuthenticationContext.NegotiateMechanism.None:
					return -1;
				case AuthenticationContext.NegotiateMechanism.InboundLogon:
				case AuthenticationContext.NegotiateMechanism.OutboundLogon:
					return AuthenticationContext.Base64BytesRequired(512);
				case AuthenticationContext.NegotiateMechanism.InboundPlain:
				case AuthenticationContext.NegotiateMechanism.OutboundPlain:
					return AuthenticationContext.Base64BytesRequired(767);
				}
				if (this.sspiContext == null)
				{
					return -1;
				}
				return AuthenticationContext.Base64BytesRequired(this.sspiContext.MaxTokenSize);
			}
		}

		// Token: 0x17000FDB RID: 4059
		// (get) Token: 0x0600408B RID: 16523 RVA: 0x000AAF4E File Offset: 0x000A914E
		public string UserName
		{
			get
			{
				return this.userNameString;
			}
		}

		// Token: 0x17000FDC RID: 4060
		// (get) Token: 0x0600408C RID: 16524 RVA: 0x000AAF56 File Offset: 0x000A9156
		public byte[] UserNameBytes
		{
			get
			{
				return this.userName;
			}
		}

		// Token: 0x17000FDD RID: 4061
		// (get) Token: 0x0600408D RID: 16525 RVA: 0x000AAF5E File Offset: 0x000A915E
		public SecureString Password
		{
			get
			{
				return this.passwordData;
			}
		}

		// Token: 0x17000FDE RID: 4062
		// (get) Token: 0x0600408E RID: 16526 RVA: 0x000AAF66 File Offset: 0x000A9166
		// (set) Token: 0x0600408F RID: 16527 RVA: 0x000AAF6E File Offset: 0x000A916E
		public byte[] AuthorizationIdentity
		{
			get
			{
				return this.authzIdentity;
			}
			set
			{
				this.authzIdentity = value;
			}
		}

		// Token: 0x06004090 RID: 16528 RVA: 0x000AAF78 File Offset: 0x000A9178
		public static bool ParseDomainAndUsername(string inputString, out string username, out string domain)
		{
			int num = inputString.IndexOfAny(AuthenticationContext.domainDelimiters);
			if (num == 0 || num == inputString.Length - 1)
			{
				username = null;
				domain = null;
				return false;
			}
			if (num == -1)
			{
				username = inputString;
				domain = string.Empty;
			}
			else
			{
				username = inputString.Substring(num + 1);
				domain = inputString.Substring(0, num);
			}
			return true;
		}

		// Token: 0x06004091 RID: 16529 RVA: 0x000AAFD0 File Offset: 0x000A91D0
		public static byte[] ConvertFromBase64ByteArray(byte[] inputArray, int offset, int length)
		{
			int num = 0;
			int num2 = 0;
			for (int i = 0; i < length; i++)
			{
				byte b = inputArray[offset + i];
				int num3 = 64;
				if (b < 128)
				{
					num3 = AuthenticationContext.base64decoding[(int)b];
				}
				int num4 = num3;
				switch (num4)
				{
				case -2:
					num2++;
					break;
				case -1:
					break;
				default:
					if (num4 == 64)
					{
						return null;
					}
					num++;
					break;
				}
			}
			if (num2 > 2)
			{
				return null;
			}
			if ((num + num2 & 3) != 0)
			{
				return null;
			}
			int num5 = 3 * (num + num2) / 4 - num2;
			if (num5 == 0)
			{
				return AuthenticationContext.EmptyByteArray;
			}
			byte[] array = new byte[num5];
			int num6 = 0;
			int num7 = 4;
			int num8 = 0;
			for (int j = 0; j < length; j++)
			{
				byte b2 = inputArray[offset + j];
				int num9 = AuthenticationContext.base64decoding[(int)b2];
				if (num9 != -1)
				{
					if (num9 == -2)
					{
						if (num > 0)
						{
							return null;
						}
						if (num2 == 2)
						{
							array[num8++] = (byte)(num6 >> 4 & 255);
						}
						if (num2 == 1)
						{
							array[num8++] = (byte)(num6 >> 10 & 255);
							array[num8++] = (byte)(num6 >> 2 & 255);
							break;
						}
						break;
					}
					else
					{
						num6 = (num6 << 6 | num9);
						num--;
						num7--;
						if (num7 == 0)
						{
							array[num8++] = (byte)(num6 >> 16 & 255);
							array[num8++] = (byte)(num6 >> 8 & 255);
							array[num8++] = (byte)(num6 & 255);
							num7 = 4;
							num6 = 0;
						}
					}
				}
			}
			return array;
		}

		// Token: 0x06004092 RID: 16530 RVA: 0x000AB150 File Offset: 0x000A9350
		public static bool ParseExchangeAuthBlob(byte[] inputBuffer, int inputOffset, int inputLength, out byte[] kerberosToken, out byte[] challengeResponse)
		{
			ArgumentValidator.ThrowIfNull("inputBuffer", inputBuffer);
			ArgumentValidator.ThrowIfOutOfRange<int>("inputOffset", inputOffset, 0, inputBuffer.Length);
			ArgumentValidator.ThrowIfOutOfRange<int>("inputLength", inputLength, 0, inputBuffer.Length - inputOffset);
			if (!AuthenticationContext.ParseField(inputBuffer, ref inputOffset, ref inputLength, out kerberosToken))
			{
				challengeResponse = null;
				return false;
			}
			return AuthenticationContext.ParseField(inputBuffer, ref inputOffset, ref inputLength, out challengeResponse);
		}

		// Token: 0x06004093 RID: 16531 RVA: 0x000AB1A8 File Offset: 0x000A93A8
		public static bool TrySplitDomainAndUsername(byte[] input, out byte[] domain, out byte[] user)
		{
			int i = 0;
			while (i < input.Length)
			{
				if (input[i] == 47 || input[i] == 92)
				{
					if (i == 0 || i == input.Length - 1)
					{
						byte[] array;
						user = (array = null);
						domain = array;
						return false;
					}
					break;
				}
				else
				{
					i++;
				}
			}
			int srcOffset;
			int num;
			int num2;
			int num3;
			if (i < input.Length)
			{
				srcOffset = 0;
				num = i;
				num2 = i + 1;
				num3 = input.Length - num2;
			}
			else
			{
				num2 = 0;
				num3 = input.Length;
				srcOffset = 0;
				num = 0;
			}
			domain = new byte[num + 1];
			Buffer.BlockCopy(input, srcOffset, domain, 0, num);
			user = new byte[num3 + 1];
			Buffer.BlockCopy(input, num2, user, 0, num3);
			return true;
		}

		// Token: 0x06004094 RID: 16532 RVA: 0x000AB238 File Offset: 0x000A9438
		public SecurityStatus LogonUser(byte[] name, byte[] password)
		{
			byte[] nullTerminatedDomain;
			byte[] nullTerminatedUser;
			if (!AuthenticationContext.TrySplitDomainAndUsername(name, out nullTerminatedDomain, out nullTerminatedUser))
			{
				return SecurityStatus.IllegalMessage;
			}
			int num = password.Length + 1;
			int num2 = password.Length - 1;
			while (num2 >= 0 && password[num2] == 0)
			{
				num--;
				num2--;
			}
			byte[] array = new byte[num];
			Buffer.BlockCopy(password, 0, array, 0, num - 1);
			SecurityStatus result = this.InternalLogonUser(nullTerminatedUser, nullTerminatedDomain, array);
			Array.Clear(array, 0, array.Length);
			return result;
		}

		// Token: 0x06004095 RID: 16533 RVA: 0x000AB2A8 File Offset: 0x000A94A8
		public SecurityStatus LogonUser(string name, SecureString password)
		{
			if (this.externalAuthentication != null || this.externalProxyAuthentication != null)
			{
				throw new NotSupportedException();
			}
			int i = 0;
			while (i < name.Length)
			{
				if (name[i] == '/' || name[i] == '\\')
				{
					if (i == 0 || i == name.Length - 1)
					{
						return SecurityStatus.IllegalMessage;
					}
					break;
				}
				else
				{
					i++;
				}
			}
			string domain;
			string user;
			if (i < name.Length)
			{
				domain = name.Substring(0, i);
				user = name.Substring(i + 1, name.Length - (i + 1));
			}
			else
			{
				user = name;
				domain = string.Empty;
			}
			return this.LogonUser(user, domain, password);
		}

		// Token: 0x06004096 RID: 16534 RVA: 0x000AB340 File Offset: 0x000A9540
		public SecurityStatus LogonUser(string user, string domain, SecureString password)
		{
			if (this.externalAuthentication != null || this.externalProxyAuthentication != null)
			{
				throw new NotSupportedException();
			}
			IntPtr intPtr = IntPtr.Zero;
			SafeTokenHandle safeTokenHandle = new SafeTokenHandle();
			try
			{
				intPtr = Marshal.SecureStringToGlobalAllocUnicode(password);
				if (!SspiNativeMethods.LogonUser(user, domain, intPtr, LogonType.NetworkCleartext, LogonProvider.Default, ref safeTokenHandle))
				{
					int lastWin32Error = Marshal.GetLastWin32Error();
					ExTraceGlobals.AuthenticationTracer.TraceError<int>(0L, "LogonUser failed: {0}", lastWin32Error);
					return SecurityStatus.LogonDenied;
				}
				this.windowsIdentity = this.WindowsIdentityFromToken(safeTokenHandle, "Logon", WindowsAccountType.Normal);
				ExTraceGlobals.AuthenticationTracer.Information(0L, "LogonUser succeeded");
			}
			finally
			{
				safeTokenHandle.Close();
				if (intPtr != IntPtr.Zero)
				{
					Marshal.ZeroFreeGlobalAllocUnicode(intPtr);
				}
			}
			return SecurityStatus.OK;
		}

		// Token: 0x06004097 RID: 16535 RVA: 0x000AB3F8 File Offset: 0x000A95F8
		public SecurityStatus LogonUser(byte[] username, byte[] domain, byte[] password)
		{
			byte[] array = null;
			byte[] array2 = null;
			byte[] array3 = null;
			if (username != null)
			{
				array = new byte[username.Length + 1];
				Buffer.BlockCopy(username, 0, array, 0, username.Length);
			}
			if (domain != null)
			{
				array2 = new byte[domain.Length + 1];
				Buffer.BlockCopy(domain, 0, array2, 0, domain.Length);
			}
			if (password != null)
			{
				array3 = new byte[password.Length + 1];
				Buffer.BlockCopy(password, 0, array3, 0, password.Length);
			}
			SecurityStatus result = this.InternalLogonUser(array, array2, array3);
			if (array3 != null)
			{
				Array.Clear(array3, 0, array3.Length);
			}
			return result;
		}

		// Token: 0x06004098 RID: 16536 RVA: 0x000AB474 File Offset: 0x000A9674
		public SecurityStatus LogonAsMachineAccount()
		{
			SecurityStatus securityStatus;
			using (SspiContext sspiContext = new SspiContext())
			{
				using (SspiContext sspiContext2 = new SspiContext())
				{
					securityStatus = sspiContext2.InitializeForInboundAuthentication("Kerberos", ExtendedProtectionConfig.NoExtendedProtection, null);
					if (securityStatus != SecurityStatus.OK)
					{
						return securityStatus;
					}
					string target;
					securityStatus = sspiContext2.QueryCredentialsNames(out target);
					if (securityStatus != SecurityStatus.OK)
					{
						return securityStatus;
					}
					securityStatus = sspiContext.InitializeForOutboundAuthentication("Kerberos", target, AuthIdentity.LocalMachine, false, null);
					if (securityStatus != SecurityStatus.OK)
					{
						return securityStatus;
					}
					byte[] array = new byte[sspiContext.MaxTokenSize];
					byte[] array2 = new byte[sspiContext.MaxTokenSize];
					int num;
					int inputLength;
					securityStatus = sspiContext.NegotiateSecurityContext(null, 0, 0, array, 0, array.Length, out num, out inputLength);
					if (securityStatus != SecurityStatus.OK)
					{
						return securityStatus;
					}
					securityStatus = sspiContext2.NegotiateSecurityContext(array, 0, inputLength, array2, 0, array2.Length, out num, out inputLength);
					if (securityStatus != SecurityStatus.OK)
					{
						return securityStatus;
					}
					SafeTokenHandle safeTokenHandle;
					securityStatus = sspiContext2.QuerySecurityContextToken(out safeTokenHandle);
					if (securityStatus != SecurityStatus.OK)
					{
						return securityStatus;
					}
					this.windowsIdentity = this.WindowsIdentityFromToken(safeTokenHandle, this.packageName, WindowsAccountType.System);
					safeTokenHandle.Close();
				}
			}
			return securityStatus;
		}

		// Token: 0x06004099 RID: 16537 RVA: 0x000AB598 File Offset: 0x000A9798
		public SecurityStatus InitializeForInboundNegotiate(AuthenticationMechanism mechanism)
		{
			if (this.negotiateMechanism != AuthenticationContext.NegotiateMechanism.None)
			{
				throw new InvalidOperationException(NetException.AlreadyInitialized);
			}
			switch (mechanism)
			{
			case AuthenticationMechanism.Login:
				this.packageName = "Logon";
				this.negotiateMechanism = AuthenticationContext.NegotiateMechanism.InboundLogon;
				goto IL_BE;
			case AuthenticationMechanism.Negotiate:
				this.packageName = "Negotiate";
				this.negotiateMechanism = AuthenticationContext.NegotiateMechanism.SspiNegotiate;
				goto IL_BE;
			case AuthenticationMechanism.Ntlm:
				this.packageName = "NTLM";
				this.negotiateMechanism = AuthenticationContext.NegotiateMechanism.SspiNegotiate;
				goto IL_BE;
			case AuthenticationMechanism.Kerberos:
				this.packageName = "Kerberos";
				this.negotiateMechanism = AuthenticationContext.NegotiateMechanism.SspiNegotiate;
				goto IL_BE;
			case AuthenticationMechanism.Gssapi:
				this.packageName = "Negotiate";
				this.negotiateMechanism = AuthenticationContext.NegotiateMechanism.GssapiNegotiate;
				goto IL_BE;
			case AuthenticationMechanism.Plain:
				this.packageName = "Logon";
				this.negotiateMechanism = AuthenticationContext.NegotiateMechanism.InboundPlain;
				goto IL_BE;
			}
			return SecurityStatus.Unsupported;
			IL_BE:
			if (this.negotiateMechanism == AuthenticationContext.NegotiateMechanism.SspiNegotiate || this.negotiateMechanism == AuthenticationContext.NegotiateMechanism.GssapiNegotiate)
			{
				this.sspiContext = this.CreateSspiContext();
				bool flag = this.packageName.Equals("Kerberos", StringComparison.OrdinalIgnoreCase);
				return this.sspiContext.InitializeForInboundAuthentication(this.packageName, flag ? ExtendedProtectionConfig.NoExtendedProtection : this.extendedProtectionConfig, flag ? null : this.channelBindingToken);
			}
			return SecurityStatus.OK;
		}

		// Token: 0x0600409A RID: 16538 RVA: 0x000AB6C4 File Offset: 0x000A98C4
		public SecurityStatus InitializeForInboundExchangeAuth(string hashAlgorithm, string spn, byte[] publicKey, byte[] tlsEapKeyData)
		{
			ArgumentValidator.ThrowIfNullOrEmpty("hashAlgorithm", hashAlgorithm);
			ArgumentValidator.ThrowIfNullOrEmpty("spn", spn);
			ArgumentValidator.ThrowIfNull("publicKey", publicKey);
			ArgumentValidator.ThrowIfNull("tlsEapKeyData", tlsEapKeyData);
			if (this.negotiateMechanism != AuthenticationContext.NegotiateMechanism.None)
			{
				throw new InvalidOperationException(NetException.AlreadyInitialized);
			}
			if (!hashAlgorithm.Equals("SHA256", StringComparison.OrdinalIgnoreCase))
			{
				return SecurityStatus.Unsupported;
			}
			this.servicePrincipalName = spn;
			this.certificatePublicKey = publicKey;
			this.tlsEapKey = tlsEapKeyData;
			this.packageName = "Kerberos";
			this.negotiateMechanism = AuthenticationContext.NegotiateMechanism.ExchangeAuth;
			this.sspiContext = this.CreateSspiContext();
			return this.sspiContext.InitializeForInboundAuthentication("Kerberos", ExtendedProtectionConfig.NoExtendedProtection, null);
		}

		// Token: 0x0600409B RID: 16539 RVA: 0x000AB774 File Offset: 0x000A9974
		public SecurityStatus InitializeForOutboundNegotiate(AuthenticationMechanism mechanism, string spn, string inputUsername, SecureString password)
		{
			if (mechanism == AuthenticationMechanism.Login)
			{
				ArgumentValidator.ThrowIfNullOrEmpty("inputUsername", inputUsername);
			}
			string username = null;
			string domain = null;
			if (!string.IsNullOrEmpty(inputUsername))
			{
				if (mechanism == AuthenticationMechanism.Login)
				{
					username = inputUsername;
				}
				else if (!AuthenticationContext.ParseDomainAndUsername(inputUsername, out username, out domain))
				{
					return SecurityStatus.UnknownCredentials;
				}
			}
			return this.InitializeForOutboundNegotiate(mechanism, spn, username, domain, password);
		}

		// Token: 0x0600409C RID: 16540 RVA: 0x000AB7C4 File Offset: 0x000A99C4
		public SecurityStatus InitializeForOutboundNegotiate(AuthenticationMechanism mechanism, string spn, string username, string domain, SecureString password)
		{
			if (this.negotiateMechanism != AuthenticationContext.NegotiateMechanism.None)
			{
				throw new InvalidOperationException(NetException.AlreadyInitialized);
			}
			switch (mechanism)
			{
			case AuthenticationMechanism.Login:
				ArgumentValidator.ThrowIfNullOrEmpty("username", username);
				ArgumentValidator.ThrowIfNull("password", password);
				this.packageName = "Logon";
				this.negotiateMechanism = AuthenticationContext.NegotiateMechanism.OutboundLogon;
				goto IL_10B;
			case AuthenticationMechanism.Negotiate:
				this.packageName = "Negotiate";
				this.negotiateMechanism = AuthenticationContext.NegotiateMechanism.SspiNegotiate;
				goto IL_10B;
			case AuthenticationMechanism.Ntlm:
				ArgumentValidator.ThrowIfNullOrEmpty("spn", spn);
				this.packageName = "NTLM";
				this.negotiateMechanism = AuthenticationContext.NegotiateMechanism.SspiNegotiate;
				goto IL_10B;
			case AuthenticationMechanism.Kerberos:
				ArgumentValidator.ThrowIfNullOrEmpty("spn", spn);
				this.packageName = "Kerberos";
				this.negotiateMechanism = AuthenticationContext.NegotiateMechanism.SspiNegotiate;
				goto IL_10B;
			case AuthenticationMechanism.Gssapi:
				this.packageName = "Negotiate";
				this.negotiateMechanism = AuthenticationContext.NegotiateMechanism.GssapiNegotiate;
				goto IL_10B;
			case AuthenticationMechanism.Plain:
				ArgumentValidator.ThrowIfNullOrEmpty("username", username);
				ArgumentValidator.ThrowIfNull("password", password);
				this.packageName = "Logon";
				this.negotiateMechanism = AuthenticationContext.NegotiateMechanism.OutboundPlain;
				goto IL_10B;
			}
			return SecurityStatus.Unsupported;
			IL_10B:
			switch (this.negotiateMechanism)
			{
			case AuthenticationContext.NegotiateMechanism.OutboundLogon:
				if (string.IsNullOrEmpty(domain))
				{
					this.userName = AuthenticationContext.Base64Encode(username);
				}
				else
				{
					this.userName = AuthenticationContext.Base64Encode(domain + "\\" + username);
				}
				this.passwordData = password;
				return SecurityStatus.OK;
			case AuthenticationContext.NegotiateMechanism.SspiNegotiate:
			case AuthenticationContext.NegotiateMechanism.GssapiNegotiate:
			{
				this.sspiContext = this.CreateSspiContext();
				AuthIdentity @default;
				if (string.IsNullOrEmpty(username))
				{
					if (!string.IsNullOrEmpty(domain))
					{
						throw new InvalidOperationException("Domain specified when username is not.");
					}
					@default = AuthIdentity.Default;
				}
				else
				{
					@default = new AuthIdentity(username, password, domain);
				}
				try
				{
					this.sspiContext = this.CreateSspiContext();
					bool flag = this.packageName.Equals("Kerberos", StringComparison.OrdinalIgnoreCase);
					return this.sspiContext.InitializeForOutboundAuthentication(this.packageName, spn, @default, true, flag ? null : this.channelBindingToken);
				}
				finally
				{
					@default.Dispose();
				}
				break;
			}
			case AuthenticationContext.NegotiateMechanism.ExchangeAuth:
			case AuthenticationContext.NegotiateMechanism.InboundPlain:
				goto IL_231;
			case AuthenticationContext.NegotiateMechanism.OutboundPlain:
				break;
			default:
				goto IL_231;
			}
			this.userName = Encoding.ASCII.GetBytes(string.IsNullOrEmpty(domain) ? username : (domain + "\\" + username));
			this.passwordData = password;
			return SecurityStatus.OK;
			IL_231:
			return SecurityStatus.Unsupported;
		}

		// Token: 0x0600409D RID: 16541 RVA: 0x000ABA1C File Offset: 0x000A9C1C
		public SecurityStatus InitializeForOutboundExchangeAuth(string hashAlgorithm, string targetServicePrincipalName, byte[] certPublicKey, byte[] eapKey)
		{
			ArgumentValidator.ThrowIfNullOrEmpty("hashAlgorithm", hashAlgorithm);
			ArgumentValidator.ThrowIfNullOrEmpty("targetServicePrincipalName", targetServicePrincipalName);
			ArgumentValidator.ThrowIfNull("certPublicKey", certPublicKey);
			ArgumentValidator.ThrowIfNull("eapKey", eapKey);
			if (this.negotiateMechanism != AuthenticationContext.NegotiateMechanism.None)
			{
				throw new InvalidOperationException(NetException.AlreadyInitialized);
			}
			if (!hashAlgorithm.Equals("SHA256", StringComparison.OrdinalIgnoreCase))
			{
				return SecurityStatus.Unsupported;
			}
			this.certificatePublicKey = certPublicKey;
			this.tlsEapKey = eapKey;
			this.packageName = "Kerberos";
			this.negotiateMechanism = AuthenticationContext.NegotiateMechanism.ExchangeAuth;
			this.sspiContext = this.CreateSspiContext();
			return this.sspiContext.InitializeForOutboundAuthentication("Kerberos", targetServicePrincipalName, AuthIdentity.Default, false, null);
		}

		// Token: 0x0600409E RID: 16542 RVA: 0x000ABAC7 File Offset: 0x000A9CC7
		public SecurityStatus NegotiateSecurityContext(byte[] inputBuffer, out byte[] outputBuffer)
		{
			return this.NegotiateSecurityContext(inputBuffer, 0, (inputBuffer == null) ? 0 : inputBuffer.Length, out outputBuffer);
		}

		// Token: 0x0600409F RID: 16543 RVA: 0x000ABADC File Offset: 0x000A9CDC
		public SecurityStatus NegotiateSecurityContext(byte[] inputBuffer, int inputOffset, int inputLength, out byte[] outputBuffer)
		{
			switch (this.negotiateMechanism)
			{
			case AuthenticationContext.NegotiateMechanism.None:
				throw new InvalidOperationException(NetException.NotInitialized);
			case AuthenticationContext.NegotiateMechanism.InboundLogon:
				return this.NegotiateInboundLogon(inputBuffer, inputOffset, inputLength, out outputBuffer);
			case AuthenticationContext.NegotiateMechanism.OutboundLogon:
				return this.NegotiateOutboundLogon(out outputBuffer);
			case AuthenticationContext.NegotiateMechanism.SspiNegotiate:
			case AuthenticationContext.NegotiateMechanism.GssapiNegotiate:
				return this.NegotiateGssapi(inputBuffer, inputOffset, inputLength, out outputBuffer);
			case AuthenticationContext.NegotiateMechanism.ExchangeAuth:
				return this.NegotiateExchangeAuth(inputBuffer, inputOffset, inputLength, out outputBuffer);
			case AuthenticationContext.NegotiateMechanism.InboundPlain:
				outputBuffer = null;
				return this.NegotiatePlainLogon(inputBuffer, inputOffset, inputLength);
			case AuthenticationContext.NegotiateMechanism.OutboundPlain:
				return this.NegotiateOutboundPlain(out outputBuffer);
			default:
				throw new InvalidOperationException(NetException.UnknownAuthMechanism);
			}
		}

		// Token: 0x060040A0 RID: 16544 RVA: 0x000ABB7C File Offset: 0x000A9D7C
		public WindowsIdentity DetachIdentity()
		{
			WindowsIdentity result = this.windowsIdentity;
			this.windowsIdentity = null;
			return result;
		}

		// Token: 0x060040A1 RID: 16545 RVA: 0x000ABB98 File Offset: 0x000A9D98
		public bool TryGetAuthenticatedSidFromIdentity(out SecurityIdentifier authenticatedSid, out SystemException exception)
		{
			authenticatedSid = null;
			exception = null;
			try
			{
				authenticatedSid = this.Identity.User;
			}
			catch (SystemException ex)
			{
				exception = ex;
			}
			if (authenticatedSid == null)
			{
				if (exception == null)
				{
					exception = new SystemException("Identity.User returned null - likely anonymous identity");
				}
				return false;
			}
			return true;
		}

		// Token: 0x060040A2 RID: 16546 RVA: 0x000ABBF0 File Offset: 0x000A9DF0
		public bool TryGetAuthenticatedUsernameFromIdentity(out string username, out SystemException exception)
		{
			exception = null;
			try
			{
				username = this.Identity.Name;
			}
			catch (SystemException ex)
			{
				username = null;
				exception = ex;
				return false;
			}
			return true;
		}

		// Token: 0x060040A3 RID: 16547 RVA: 0x000ABC30 File Offset: 0x000A9E30
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<AuthenticationContext>(this);
		}

		// Token: 0x060040A4 RID: 16548 RVA: 0x000ABC38 File Offset: 0x000A9E38
		protected override void InternalDispose(bool disposing)
		{
			if (disposing)
			{
				if (this.sspiContext != null)
				{
					this.sspiContext.Dispose();
					this.sspiContext = null;
				}
				if (this.windowsIdentity != null)
				{
					this.windowsIdentity.Dispose();
					this.windowsIdentity = null;
				}
				this.commonAccessToken = null;
			}
		}

		// Token: 0x060040A5 RID: 16549 RVA: 0x000ABC78 File Offset: 0x000A9E78
		protected virtual SspiContext CreateSspiContext()
		{
			return new SspiContext();
		}

		// Token: 0x060040A6 RID: 16550 RVA: 0x000ABC7F File Offset: 0x000A9E7F
		protected virtual bool SspiLogonUser(byte[] username, byte[] domain, byte[] password, LogonType logonType, LogonProvider logonProvider, ref SafeTokenHandle token)
		{
			return SspiNativeMethods.LogonUser(username, domain, password, LogonType.NetworkCleartext, LogonProvider.Default, ref token);
		}

		// Token: 0x060040A7 RID: 16551 RVA: 0x000ABC8D File Offset: 0x000A9E8D
		protected virtual WindowsIdentity WindowsIdentityFromToken(SafeTokenHandle token, string package, WindowsAccountType accountType = WindowsAccountType.Normal)
		{
			return new WindowsIdentity(token.DangerousGetHandle(), package, accountType, true);
		}

		// Token: 0x060040A8 RID: 16552 RVA: 0x000ABCA0 File Offset: 0x000A9EA0
		private static byte[] FormatExchangeAuthBlob(BufferBuilder kerberosToken, byte[] challengeResponse)
		{
			BufferBuilder bufferBuilder = new BufferBuilder(8 + AuthenticationContext.Base64BytesRequired(kerberosToken.Length) + 8 + AuthenticationContext.Base64BytesRequired(challengeResponse));
			byte[] array = AuthenticationContext.Base64Encode(kerberosToken);
			bufferBuilder.Append(array.Length.ToString("X8", NumberFormatInfo.InvariantInfo));
			bufferBuilder.Append(array);
			array = AuthenticationContext.Base64Encode(challengeResponse);
			bufferBuilder.Append(array.Length.ToString("X8", NumberFormatInfo.InvariantInfo));
			bufferBuilder.Append(array);
			bufferBuilder.RemoveUnusedBufferSpace();
			return bufferBuilder.GetBuffer();
		}

		// Token: 0x060040A9 RID: 16553 RVA: 0x000ABD28 File Offset: 0x000A9F28
		private static bool ParseField(byte[] inputBuffer, ref int inputOffset, ref int inputLength, out byte[] outputBuffer)
		{
			outputBuffer = null;
			if (inputLength < 8 || inputLength > inputBuffer.Length || inputOffset < 0 || inputOffset > inputBuffer.Length - 8)
			{
				return false;
			}
			string @string = Encoding.ASCII.GetString(inputBuffer, inputOffset, 8);
			int num;
			if (!int.TryParse(@string, NumberStyles.HexNumber, null, out num))
			{
				return false;
			}
			inputOffset += 8;
			inputLength -= 8;
			if (num < 0 || num > inputBuffer.Length - inputOffset)
			{
				return false;
			}
			if (num > 0)
			{
				outputBuffer = AuthenticationContext.ConvertFromBase64ByteArray(inputBuffer, inputOffset, num);
				if (outputBuffer == null)
				{
					return false;
				}
				inputOffset += num;
				inputLength -= num;
			}
			else
			{
				outputBuffer = AuthenticationContext.EmptyByteArray;
			}
			return true;
		}

		// Token: 0x060040AA RID: 16554 RVA: 0x000ABDBC File Offset: 0x000A9FBC
		private static byte[] AsciiStringToBytes(string value)
		{
			byte[] array = new byte[value.Length];
			for (int i = 0; i < value.Length; i++)
			{
				array[i] = ((value[i] < '\u0080') ? ((byte)value[i]) : 63);
			}
			return array;
		}

		// Token: 0x060040AB RID: 16555 RVA: 0x000ABE04 File Offset: 0x000AA004
		private static byte[] Base64Encode(string inputString)
		{
			byte[] bytes = Encoding.ASCII.GetBytes(inputString);
			return AuthenticationContext.Base64Encode(bytes);
		}

		// Token: 0x060040AC RID: 16556 RVA: 0x000ABE24 File Offset: 0x000AA024
		private static byte[] Base64Encode(SecureString password)
		{
			byte[] password2 = AuthenticationContext.GetPassword(password);
			byte[] result;
			try
			{
				result = AuthenticationContext.Base64Encode(password2);
			}
			finally
			{
				Array.Clear(password2, 0, password2.Length);
			}
			return result;
		}

		// Token: 0x060040AD RID: 16557 RVA: 0x000ABE60 File Offset: 0x000AA060
		private static byte[] Base64Encode(byte[] inputBytes)
		{
			return AuthenticationContext.Base64Encode(inputBytes, 0, inputBytes.Length);
		}

		// Token: 0x060040AE RID: 16558 RVA: 0x000ABE6C File Offset: 0x000AA06C
		private static byte[] Base64Encode(BufferBuilder buffer)
		{
			return AuthenticationContext.Base64Encode(buffer.GetBuffer(), 0, buffer.Length);
		}

		// Token: 0x060040AF RID: 16559 RVA: 0x000ABE80 File Offset: 0x000AA080
		private static byte[] Base64Encode(byte[] inputBytes, int inputOffset, int inputLength)
		{
			char[] array = new char[AuthenticationContext.Base64BytesRequired(inputLength)];
			int num = Convert.ToBase64CharArray(inputBytes, inputOffset, inputLength, array, 0);
			byte[] array2 = new byte[num];
			for (int i = 0; i < num; i++)
			{
				array2[i] = (byte)array[i];
			}
			return array2;
		}

		// Token: 0x060040B0 RID: 16560 RVA: 0x000ABEC0 File Offset: 0x000AA0C0
		private static int Base64BytesRequired(byte[] inputBytes)
		{
			int length = (inputBytes == null) ? 0 : inputBytes.Length;
			return AuthenticationContext.Base64BytesRequired(length);
		}

		// Token: 0x060040B1 RID: 16561 RVA: 0x000ABEDD File Offset: 0x000AA0DD
		private static int Base64BytesRequired(int length)
		{
			return Math.Max(1, (length + 2) / 3 * 4);
		}

		// Token: 0x060040B2 RID: 16562 RVA: 0x000ABEEC File Offset: 0x000AA0EC
		private static byte[] GetPassword(SecureString password)
		{
			if (password == null || password.Length == 0)
			{
				return AuthenticationContext.EmptyByteArray;
			}
			IntPtr intPtr = IntPtr.Zero;
			char[] array = new char[password.Length];
			byte[] array2 = new byte[password.Length];
			try
			{
				intPtr = Marshal.SecureStringToGlobalAllocUnicode(password);
				Marshal.Copy(intPtr, array, 0, password.Length);
				for (int i = 0; i < password.Length; i++)
				{
					array2[i] = (byte)array[i];
				}
			}
			finally
			{
				if (intPtr != IntPtr.Zero)
				{
					Marshal.ZeroFreeGlobalAllocUnicode(intPtr);
				}
				Array.Clear(array, 0, array.Length);
			}
			return array2;
		}

		// Token: 0x060040B3 RID: 16563 RVA: 0x000ABF88 File Offset: 0x000AA188
		private static bool BlobsEqual(byte[] blob1, byte[] blob2)
		{
			if (blob1.Length != blob2.Length)
			{
				return false;
			}
			for (int i = 0; i < blob1.Length; i++)
			{
				if (blob1[i] != blob2[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060040B4 RID: 16564 RVA: 0x000ABFB8 File Offset: 0x000AA1B8
		private SecurityStatus InternalLogonUser(byte[] nullTerminatedUser, byte[] nullTerminatedDomain, byte[] nullTerminatedPassword)
		{
			SecurityStatus securityStatus = SecurityStatus.LogonDenied;
			SafeTokenHandle safeTokenHandle = new SafeTokenHandle();
			try
			{
				if (this.externalAuthentication != null)
				{
					securityStatus = this.externalAuthentication(nullTerminatedUser, nullTerminatedPassword, out this.windowsIdentity, out this.accountValidationContext);
					ExTraceGlobals.AuthenticationTracer.Information<string>(0L, "ExternalAuthentication {0}", (securityStatus == SecurityStatus.OK) ? "succeeded" : "failed");
				}
				else if (this.externalProxyAuthentication != null)
				{
					securityStatus = this.externalProxyAuthentication(nullTerminatedUser, nullTerminatedPassword, Guid.Empty, out this.commonAccessToken, out this.accountValidationContext);
					ExTraceGlobals.AuthenticationTracer.Information<string>(0L, "ExternalProxyAuthentication {0}", (securityStatus == SecurityStatus.OK) ? "succeeded" : "failed");
				}
				else
				{
					if (this.externalLoginProcessing != null)
					{
						this.externalLoginProcessing(nullTerminatedDomain, nullTerminatedUser, nullTerminatedPassword);
					}
					if (!this.SspiLogonUser(nullTerminatedUser, nullTerminatedDomain, nullTerminatedPassword, LogonType.NetworkCleartext, LogonProvider.Default, ref safeTokenHandle))
					{
						int lastWin32Error = Marshal.GetLastWin32Error();
						ExTraceGlobals.AuthenticationTracer.TraceError<int>(0L, "LogonUser failed: {0}", lastWin32Error);
						return securityStatus;
					}
					securityStatus = SecurityStatus.OK;
					this.windowsIdentity = this.WindowsIdentityFromToken(safeTokenHandle, "Logon", WindowsAccountType.Normal);
					ExTraceGlobals.AuthenticationTracer.Information(0L, "LogonUser succeeded");
				}
			}
			finally
			{
				safeTokenHandle.Close();
			}
			return securityStatus;
		}

		// Token: 0x060040B5 RID: 16565 RVA: 0x000AC0E4 File Offset: 0x000AA2E4
		private SecurityStatus NegotiateInboundLogon(byte[] inputBuffer, int inputOffset, int inputLength, out byte[] outputBuffer)
		{
			outputBuffer = null;
			if (this.userName == null)
			{
				if (inputLength == 0)
				{
					outputBuffer = AuthenticationContext.Base64Username;
					return SecurityStatus.ContinueNeeded;
				}
				this.userName = AuthenticationContext.ConvertFromBase64ByteArray(inputBuffer, inputOffset, inputLength);
				if (this.userName == null)
				{
					return SecurityStatus.IllegalMessage;
				}
				outputBuffer = AuthenticationContext.Base64Password;
				return SecurityStatus.ContinueNeeded;
			}
			else
			{
				byte[] array = AuthenticationContext.ConvertFromBase64ByteArray(inputBuffer, inputOffset, inputLength);
				if (array == null)
				{
					return SecurityStatus.IllegalMessage;
				}
				SecurityStatus result = this.LogonUser(this.userName, array);
				Array.Clear(array, 0, array.Length);
				return result;
			}
		}

		// Token: 0x060040B6 RID: 16566 RVA: 0x000AC164 File Offset: 0x000AA364
		private SecurityStatus NegotiateOutboundLogon(out byte[] outputBuffer)
		{
			if (this.userName != null)
			{
				outputBuffer = this.userName;
				this.userName = null;
				return SecurityStatus.ContinueNeeded;
			}
			if (this.passwordData != null)
			{
				outputBuffer = AuthenticationContext.Base64Encode(this.passwordData);
				this.passwordData = null;
				return SecurityStatus.OK;
			}
			outputBuffer = null;
			return SecurityStatus.OutOfSequence;
		}

		// Token: 0x060040B7 RID: 16567 RVA: 0x000AC1B4 File Offset: 0x000AA3B4
		private SecurityStatus NegotiateOutboundPlain(out byte[] outputBuffer)
		{
			if (this.userName != null && this.passwordData != null)
			{
				BufferBuilder bufferBuilder = new BufferBuilder(this.MaxTokenSize);
				try
				{
					if (this.authzIdentity != null)
					{
						bufferBuilder.Append(this.authzIdentity);
					}
					bufferBuilder.Append(0);
					bufferBuilder.Append(this.userName);
					bufferBuilder.Append(0);
					bufferBuilder.Append(this.passwordData);
					outputBuffer = AuthenticationContext.Base64Encode(bufferBuilder);
				}
				finally
				{
					bufferBuilder.Reset();
				}
				this.userName = null;
				this.passwordData = null;
				return SecurityStatus.OK;
			}
			outputBuffer = null;
			return SecurityStatus.OutOfSequence;
		}

		// Token: 0x060040B8 RID: 16568 RVA: 0x000AC250 File Offset: 0x000AA450
		private SecurityStatus NegotiateGssapi(byte[] inputBuffer, int inputOffset, int inputLength, out byte[] outputBuffer)
		{
			outputBuffer = null;
			byte[] array = AuthenticationContext.ConvertFromBase64ByteArray(inputBuffer, inputOffset, inputLength);
			if (array == null)
			{
				return SecurityStatus.IllegalMessage;
			}
			switch (this.state)
			{
			case AuthenticationContext.AuthenticationState.NegotiatingAuth:
			{
				BufferBuilder bufferBuilder = new BufferBuilder(this.sspiContext.MaxTokenSize);
				int num;
				int length;
				SecurityStatus securityStatus = this.sspiContext.NegotiateSecurityContext(array, 0, array.Length, bufferBuilder.GetBuffer(), 0, bufferBuilder.Capacity, out num, out length);
				if (securityStatus != SecurityStatus.OK && securityStatus != SecurityStatus.ContinueNeeded)
				{
					this.state = AuthenticationContext.AuthenticationState.Done;
					return securityStatus;
				}
				bufferBuilder.SetLength(length);
				outputBuffer = AuthenticationContext.Base64Encode(bufferBuilder);
				if (securityStatus == SecurityStatus.ContinueNeeded)
				{
					return securityStatus;
				}
				this.maxMessageSize = this.sspiContext.MaxTokenSize;
				if (this.sspiContext.CredentialUse == CredentialUse.Inbound)
				{
					SafeTokenHandle safeTokenHandle;
					securityStatus = this.sspiContext.QuerySecurityContextToken(out safeTokenHandle);
					if (securityStatus != SecurityStatus.OK)
					{
						this.state = AuthenticationContext.AuthenticationState.Done;
						return securityStatus;
					}
					this.windowsIdentity = this.WindowsIdentityFromToken(safeTokenHandle, this.packageName, WindowsAccountType.Normal);
					if (this.windowsIdentity != null)
					{
						this.userNameString = this.windowsIdentity.Name;
					}
					safeTokenHandle.Close();
					if (outputBuffer.Length != 0)
					{
						this.state = AuthenticationContext.AuthenticationState.WaitingForCrLf;
						return SecurityStatus.ContinueNeeded;
					}
					if (this.negotiateMechanism == AuthenticationContext.NegotiateMechanism.GssapiNegotiate || this.negotiateMechanism == AuthenticationContext.NegotiateMechanism.SspiNegotiate)
					{
						securityStatus = this.sspiContext.VerifyServiceBindings();
						if (securityStatus != SecurityStatus.OK)
						{
							this.state = AuthenticationContext.AuthenticationState.Done;
							return securityStatus;
						}
					}
					if (this.negotiateMechanism == AuthenticationContext.NegotiateMechanism.GssapiNegotiate)
					{
						securityStatus = this.FormatCapabilitiesBlob(out outputBuffer);
						if (securityStatus != SecurityStatus.OK)
						{
							this.state = AuthenticationContext.AuthenticationState.Done;
							return securityStatus;
						}
						outputBuffer = AuthenticationContext.Base64Encode(outputBuffer);
						this.state = AuthenticationContext.AuthenticationState.WaitingForCapabilities;
						securityStatus = SecurityStatus.ContinueNeeded;
					}
					else
					{
						this.state = AuthenticationContext.AuthenticationState.Done;
					}
				}
				else if (this.negotiateMechanism == AuthenticationContext.NegotiateMechanism.GssapiNegotiate)
				{
					securityStatus = SecurityStatus.ContinueNeeded;
					this.state = AuthenticationContext.AuthenticationState.WaitingForCapabilities;
				}
				else
				{
					this.state = AuthenticationContext.AuthenticationState.Done;
				}
				return securityStatus;
			}
			case AuthenticationContext.AuthenticationState.WaitingForCrLf:
			{
				if (array.Length != 0)
				{
					return SecurityStatus.IllegalMessage;
				}
				if (this.negotiateMechanism != AuthenticationContext.NegotiateMechanism.GssapiNegotiate)
				{
					this.state = AuthenticationContext.AuthenticationState.Done;
					return SecurityStatus.OK;
				}
				SecurityStatus securityStatus = this.FormatCapabilitiesBlob(out outputBuffer);
				if (securityStatus != SecurityStatus.OK)
				{
					this.state = AuthenticationContext.AuthenticationState.Done;
					return securityStatus;
				}
				outputBuffer = AuthenticationContext.Base64Encode(outputBuffer);
				this.state = AuthenticationContext.AuthenticationState.WaitingForCapabilities;
				return SecurityStatus.ContinueNeeded;
			}
			case AuthenticationContext.AuthenticationState.WaitingForCapabilities:
			{
				SecurityStatus securityStatus = this.ParseCapabilitiesBlob(array);
				if (securityStatus == SecurityStatus.OK && this.sspiContext.CredentialUse == CredentialUse.Outbound)
				{
					securityStatus = this.FormatCapabilitiesBlob(out outputBuffer);
					if (securityStatus == SecurityStatus.OK)
					{
						outputBuffer = AuthenticationContext.Base64Encode(outputBuffer);
					}
				}
				this.state = AuthenticationContext.AuthenticationState.Done;
				return securityStatus;
			}
			case AuthenticationContext.AuthenticationState.Done:
				return SecurityStatus.IllegalMessage;
			default:
				return SecurityStatus.Unsupported;
			}
		}

		// Token: 0x060040B9 RID: 16569 RVA: 0x000AC498 File Offset: 0x000AA698
		private SecurityStatus FormatCapabilitiesBlob(out byte[] outputBlob)
		{
			int num = 4;
			if (this.sspiContext.CredentialUse == CredentialUse.Outbound && this.authzIdentity != null)
			{
				num += this.authzIdentity.Length;
			}
			byte[] array = new byte[num];
			array[0] = 1;
			array[1] = (byte)(this.maxMessageSize >> 16);
			array[2] = (byte)(this.maxMessageSize >> 8);
			array[3] = (byte)this.maxMessageSize;
			if (num > 4 && this.authzIdentity != null)
			{
				Buffer.BlockCopy(this.authzIdentity, 0, array, 4, this.authzIdentity.Length);
			}
			return this.sspiContext.WrapMessage(array, false, out outputBlob);
		}

		// Token: 0x060040BA RID: 16570 RVA: 0x000AC528 File Offset: 0x000AA728
		protected virtual SecurityStatus ParseCapabilitiesBlob(byte[] inputBlob)
		{
			byte[] array;
			this.sspiContext.UnwrapMessage(inputBlob, out array);
			if (array == null || array.Length < 4)
			{
				return SecurityStatus.IllegalMessage;
			}
			if ((array[0] & 1) == 0)
			{
				return SecurityStatus.Unsupported;
			}
			int num = ((int)array[1] << 16) + ((int)array[2] << 8) + (int)array[3];
			if (num < this.maxMessageSize)
			{
				this.maxMessageSize = num;
			}
			if (array.Length > 4)
			{
				this.authzIdentity = new byte[array.Length - 4];
				Buffer.BlockCopy(array, 4, this.authzIdentity, 0, this.authzIdentity.Length);
			}
			return SecurityStatus.OK;
		}

		// Token: 0x060040BB RID: 16571 RVA: 0x000AC5AE File Offset: 0x000AA7AE
		private SecurityStatus NegotiateExchangeAuth(byte[] inputBuffer, int inputOffset, int inputLength, out byte[] outputBuffer)
		{
			if (this.sspiContext.CredentialUse != CredentialUse.Outbound)
			{
				return this.NegotiateInboundExchangeAuth(inputBuffer, inputOffset, inputLength, out outputBuffer);
			}
			if (inputLength == 0)
			{
				return this.NegotiateOutboundExchangeAuthLeg1(out outputBuffer);
			}
			return this.NegotiateOutboundExchangeAuthLeg2(inputBuffer, inputOffset, inputLength, out outputBuffer);
		}

		// Token: 0x060040BC RID: 16572 RVA: 0x000AC5E4 File Offset: 0x000AA7E4
		private SecurityStatus NegotiateInboundExchangeAuth(byte[] inputBuffer, int inputOffset, int inputLength, out byte[] outputBuffer)
		{
			outputBuffer = null;
			byte[] array;
			byte[] blob;
			if (!AuthenticationContext.ParseExchangeAuthBlob(inputBuffer, inputOffset, inputLength, out array, out blob))
			{
				return SecurityStatus.IllegalMessage;
			}
			BufferBuilder bufferBuilder = new BufferBuilder(this.sspiContext.MaxTokenSize);
			int num;
			int length;
			SecurityStatus securityStatus = this.sspiContext.NegotiateSecurityContext(array, 0, array.Length, bufferBuilder.GetBuffer(), 0, bufferBuilder.Capacity, out num, out length);
			if (securityStatus != SecurityStatus.OK)
			{
				if (securityStatus == SecurityStatus.ContinueNeeded)
				{
					securityStatus = SecurityStatus.UnexpectedExchangeAuthBlob;
				}
				ExTraceGlobals.AuthenticationTracer.TraceError<SecurityStatus>(0L, "NegotiateSecurityContext failed: {0}", securityStatus);
				return securityStatus;
			}
			bufferBuilder.SetLength(length);
			SessionKey sessionKey;
			securityStatus = this.sspiContext.QuerySessionKey(out sessionKey);
			if (securityStatus != SecurityStatus.OK)
			{
				ExTraceGlobals.AuthenticationTracer.TraceError<SecurityStatus>(0L, "QuerySessionKey failed: {0}", securityStatus);
				return securityStatus;
			}
			BufferBuilder bufferBuilder2 = new BufferBuilder(this.certificatePublicKey.Length + sessionKey.Key.Length * 2 + this.tlsEapKey.Length);
			bufferBuilder2.Append(this.certificatePublicKey);
			bufferBuilder2.Append(sessionKey.Key);
			bufferBuilder2.Append(this.tlsEapKey);
			using (HashAlgorithm hashAlgorithm = new Microsoft.Exchange.Security.Cryptography.SHA256CryptoServiceProvider())
			{
				hashAlgorithm.TransformFinalBlock(bufferBuilder2.GetBuffer(), 0, bufferBuilder2.Length);
				byte[] hash = hashAlgorithm.Hash;
				if (!AuthenticationContext.BlobsEqual(blob, hash))
				{
					ExTraceGlobals.AuthenticationTracer.TraceError(0L, "Hash does not match");
					return SecurityStatus.LogonDenied;
				}
			}
			SafeTokenHandle safeTokenHandle;
			if (this.sspiContext.QuerySecurityContextToken(out safeTokenHandle) == SecurityStatus.OK)
			{
				this.windowsIdentity = this.WindowsIdentityFromToken(safeTokenHandle, this.packageName, WindowsAccountType.Normal);
			}
			safeTokenHandle.Close();
			this.sspiContext.Dispose();
			this.sspiContext = this.CreateSspiContext();
			securityStatus = this.sspiContext.InitializeForOutboundAuthentication("Kerberos", this.servicePrincipalName, AuthIdentity.Default, false, null);
			if (securityStatus != SecurityStatus.OK)
			{
				ExTraceGlobals.AuthenticationTracer.TraceError<SecurityStatus>(0L, "InitializeForOutboundAuthentication failed: {0}", securityStatus);
				return securityStatus;
			}
			securityStatus = this.sspiContext.NegotiateSecurityContext(null, 0, 0, bufferBuilder.GetBuffer(), 0, bufferBuilder.Capacity, out num, out length);
			if (securityStatus == SecurityStatus.ContinueNeeded)
			{
				return SecurityStatus.InternalError;
			}
			if (securityStatus != SecurityStatus.OK)
			{
				ExTraceGlobals.AuthenticationTracer.TraceError<SecurityStatus>(0L, "NegotiateSecurityContext failed: {0}", securityStatus);
				return securityStatus;
			}
			bufferBuilder.SetLength(length);
			securityStatus = this.sspiContext.QuerySessionKey(out sessionKey);
			if (securityStatus != SecurityStatus.OK)
			{
				ExTraceGlobals.AuthenticationTracer.TraceError<SecurityStatus>(0L, "QuerySessionKey failed: {0}", securityStatus);
				return securityStatus;
			}
			bufferBuilder2.Append(sessionKey.Key);
			using (HashAlgorithm hashAlgorithm2 = new Microsoft.Exchange.Security.Cryptography.SHA256CryptoServiceProvider())
			{
				hashAlgorithm2.TransformFinalBlock(bufferBuilder2.GetBuffer(), 0, bufferBuilder2.Length);
				outputBuffer = AuthenticationContext.FormatExchangeAuthBlob(bufferBuilder, hashAlgorithm2.Hash);
			}
			return SecurityStatus.OK;
		}

		// Token: 0x060040BD RID: 16573 RVA: 0x000AC8A8 File Offset: 0x000AAAA8
		private SecurityStatus NegotiateOutboundExchangeAuthLeg1(out byte[] outputBuffer)
		{
			outputBuffer = null;
			BufferBuilder bufferBuilder = new BufferBuilder(this.sspiContext.MaxTokenSize);
			int num;
			int length;
			SecurityStatus securityStatus = this.sspiContext.NegotiateSecurityContext(null, 0, 0, bufferBuilder.GetBuffer(), 0, bufferBuilder.Capacity, out num, out length);
			if (securityStatus == SecurityStatus.ContinueNeeded)
			{
				return SecurityStatus.InternalError;
			}
			if (securityStatus != SecurityStatus.OK)
			{
				return securityStatus;
			}
			bufferBuilder.SetLength(length);
			SessionKey sessionKey;
			securityStatus = this.sspiContext.QuerySessionKey(out sessionKey);
			if (securityStatus != SecurityStatus.OK)
			{
				return securityStatus;
			}
			BufferBuilder bufferBuilder2 = new BufferBuilder(this.certificatePublicKey.Length + sessionKey.Key.Length * 2 + this.tlsEapKey.Length);
			bufferBuilder2.Append(this.certificatePublicKey);
			bufferBuilder2.Append(sessionKey.Key);
			bufferBuilder2.Append(this.tlsEapKey);
			using (HashAlgorithm hashAlgorithm = new Microsoft.Exchange.Security.Cryptography.SHA256CryptoServiceProvider())
			{
				hashAlgorithm.TransformFinalBlock(bufferBuilder2.GetBuffer(), 0, bufferBuilder2.Length);
				outputBuffer = AuthenticationContext.FormatExchangeAuthBlob(bufferBuilder, hashAlgorithm.Hash);
				this.sessionKeyData = bufferBuilder2;
			}
			return SecurityStatus.OK;
		}

		// Token: 0x060040BE RID: 16574 RVA: 0x000AC9B8 File Offset: 0x000AABB8
		private SecurityStatus NegotiateOutboundExchangeAuthLeg2(byte[] inputBuffer, int inputOffset, int inputLength, out byte[] outputBuffer)
		{
			outputBuffer = null;
			byte[] array;
			byte[] blob;
			if (!AuthenticationContext.ParseExchangeAuthBlob(inputBuffer, inputOffset, inputLength, out array, out blob))
			{
				return SecurityStatus.IllegalMessage;
			}
			this.sspiContext.Dispose();
			this.sspiContext = this.CreateSspiContext();
			SecurityStatus securityStatus = this.sspiContext.InitializeForInboundAuthentication(this.packageName, ExtendedProtectionConfig.NoExtendedProtection, null);
			if (securityStatus != SecurityStatus.OK)
			{
				return securityStatus;
			}
			BufferBuilder bufferBuilder = new BufferBuilder(this.sspiContext.MaxTokenSize);
			int num;
			int num2;
			securityStatus = this.sspiContext.NegotiateSecurityContext(array, 0, array.Length, bufferBuilder.GetBuffer(), 0, bufferBuilder.Capacity, out num, out num2);
			if (securityStatus != SecurityStatus.OK)
			{
				if (securityStatus == SecurityStatus.ContinueNeeded)
				{
					securityStatus = SecurityStatus.UnexpectedExchangeAuthBlob;
				}
				return securityStatus;
			}
			if (num2 != 0)
			{
				return SecurityStatus.IllegalMessage;
			}
			SessionKey sessionKey;
			securityStatus = this.sspiContext.QuerySessionKey(out sessionKey);
			if (securityStatus != SecurityStatus.OK)
			{
				return securityStatus;
			}
			this.sessionKeyData.Append(sessionKey.Key);
			using (HashAlgorithm hashAlgorithm = new Microsoft.Exchange.Security.Cryptography.SHA256CryptoServiceProvider())
			{
				hashAlgorithm.TransformFinalBlock(this.sessionKeyData.GetBuffer(), 0, this.sessionKeyData.Length);
				if (!AuthenticationContext.BlobsEqual(blob, hashAlgorithm.Hash))
				{
					return SecurityStatus.LogonDenied;
				}
			}
			SafeTokenHandle safeTokenHandle;
			securityStatus = this.sspiContext.QuerySecurityContextToken(out safeTokenHandle);
			if (securityStatus == SecurityStatus.OK)
			{
				this.windowsIdentity = this.WindowsIdentityFromToken(safeTokenHandle, this.packageName, WindowsAccountType.Normal);
			}
			safeTokenHandle.Close();
			return securityStatus;
		}

		// Token: 0x060040BF RID: 16575 RVA: 0x000ACB14 File Offset: 0x000AAD14
		private SecurityStatus NegotiatePlainLogon(byte[] inputBuffer, int inputOffset, int inputLength)
		{
			byte[] array = null;
			this.userNameString = null;
			byte[] array2 = null;
			GCHandle gchandle = default(GCHandle);
			GCHandle gchandle2 = default(GCHandle);
			SecurityStatus securityStatus;
			try
			{
				array2 = AuthenticationContext.ConvertFromBase64ByteArray(inputBuffer, inputOffset, inputLength);
				if (array2 == null)
				{
					return SecurityStatus.IllegalMessage;
				}
				gchandle = GCHandle.Alloc(array2, GCHandleType.Pinned);
				byte[] array3 = null;
				int num = 0;
				int num2 = 0;
				for (int i = 0; i < array2.Length; i++)
				{
					if (array2[i] == 0)
					{
						if (num == 0)
						{
							this.authzIdentity = new byte[i];
							Array.Copy(array2, this.authzIdentity, i);
							num2 = i;
						}
						else
						{
							if (num != 1)
							{
								return SecurityStatus.IllegalMessage;
							}
							array3 = new byte[i - num2 - 1];
							Array.Copy(array2, num2 + 1, array3, 0, i - num2 - 1);
							array = new byte[array2.Length - i - 1];
							Array.Copy(array2, i + 1, array, 0, array2.Length - i - 1);
							gchandle2 = GCHandle.Alloc(array, GCHandleType.Pinned);
						}
						num++;
					}
				}
				if (num != 2 || array3 == null || array == null)
				{
					return SecurityStatus.IllegalMessage;
				}
				this.userNameString = Encoding.ASCII.GetString(array3);
				securityStatus = this.LogonUser(array3, array);
				if (securityStatus == SecurityStatus.OK)
				{
					this.passwordData = new SecureString();
					foreach (byte c in array)
					{
						this.passwordData.AppendChar((char)c);
					}
				}
			}
			catch (EncoderFallbackException)
			{
				return SecurityStatus.IllegalMessage;
			}
			catch (FormatException)
			{
				return SecurityStatus.IllegalMessage;
			}
			finally
			{
				if (array2 != null)
				{
					Array.Clear(array2, 0, array2.Length);
				}
				if (array != null)
				{
					Array.Clear(array, 0, array.Length);
				}
				if (gchandle.IsAllocated)
				{
					gchandle.Free();
				}
				if (gchandle2.IsAllocated)
				{
					gchandle2.Free();
				}
				Array.Clear(inputBuffer, inputOffset, inputLength);
			}
			return securityStatus;
		}

		// Token: 0x040037F4 RID: 14324
		public const string ExchangeAuthHashAlgorithm = "SHA256";

		// Token: 0x040037F5 RID: 14325
		private const int LengthFieldSize = 8;

		// Token: 0x040037F6 RID: 14326
		private const int FixedCapabilitiesLength = 4;

		// Token: 0x040037F7 RID: 14327
		private static readonly int[] base64decoding = new int[]
		{
			64,
			64,
			64,
			64,
			64,
			64,
			64,
			64,
			64,
			-1,
			-1,
			64,
			64,
			-1,
			64,
			64,
			64,
			64,
			64,
			64,
			64,
			64,
			64,
			64,
			64,
			64,
			64,
			64,
			64,
			64,
			64,
			64,
			-1,
			64,
			64,
			64,
			64,
			64,
			64,
			64,
			64,
			64,
			64,
			62,
			64,
			64,
			64,
			63,
			52,
			53,
			54,
			55,
			56,
			57,
			58,
			59,
			60,
			61,
			64,
			64,
			64,
			-2,
			64,
			64,
			64,
			0,
			1,
			2,
			3,
			4,
			5,
			6,
			7,
			8,
			9,
			10,
			11,
			12,
			13,
			14,
			15,
			16,
			17,
			18,
			19,
			20,
			21,
			22,
			23,
			24,
			25,
			64,
			64,
			64,
			64,
			64,
			64,
			26,
			27,
			28,
			29,
			30,
			31,
			32,
			33,
			34,
			35,
			36,
			37,
			38,
			39,
			40,
			41,
			42,
			43,
			44,
			45,
			46,
			47,
			48,
			49,
			50,
			51,
			64,
			64,
			64,
			64,
			64
		};

		// Token: 0x040037F8 RID: 14328
		private static readonly byte[] Base64Username = AuthenticationContext.AsciiStringToBytes("VXNlcm5hbWU6");

		// Token: 0x040037F9 RID: 14329
		private static readonly byte[] Base64Password = AuthenticationContext.AsciiStringToBytes("UGFzc3dvcmQ6");

		// Token: 0x040037FA RID: 14330
		private static readonly char[] domainDelimiters = new char[]
		{
			'/',
			'\\'
		};

		// Token: 0x040037FB RID: 14331
		private static readonly byte[] EmptyByteArray = new byte[0];

		// Token: 0x040037FC RID: 14332
		private readonly ChannelBindingToken channelBindingToken;

		// Token: 0x040037FD RID: 14333
		private readonly ExtendedProtectionConfig extendedProtectionConfig = ExtendedProtectionConfig.NoExtendedProtection;

		// Token: 0x040037FE RID: 14334
		private SspiContext sspiContext;

		// Token: 0x040037FF RID: 14335
		private string packageName;

		// Token: 0x04003800 RID: 14336
		private WindowsIdentity windowsIdentity;

		// Token: 0x04003801 RID: 14337
		private string commonAccessToken;

		// Token: 0x04003802 RID: 14338
		private IAccountValidationContext accountValidationContext;

		// Token: 0x04003803 RID: 14339
		private string servicePrincipalName;

		// Token: 0x04003804 RID: 14340
		private byte[] certificatePublicKey;

		// Token: 0x04003805 RID: 14341
		private byte[] tlsEapKey;

		// Token: 0x04003806 RID: 14342
		private BufferBuilder sessionKeyData;

		// Token: 0x04003807 RID: 14343
		private AuthenticationContext.NegotiateMechanism negotiateMechanism;

		// Token: 0x04003808 RID: 14344
		private byte[] userName;

		// Token: 0x04003809 RID: 14345
		private string userNameString;

		// Token: 0x0400380A RID: 14346
		private SecureString passwordData;

		// Token: 0x0400380B RID: 14347
		private byte[] authzIdentity;

		// Token: 0x0400380C RID: 14348
		private int maxMessageSize;

		// Token: 0x0400380D RID: 14349
		private readonly ExternalLoginAuthentication externalAuthentication;

		// Token: 0x0400380E RID: 14350
		private readonly ExternalProxyAuthentication externalProxyAuthentication;

		// Token: 0x0400380F RID: 14351
		private readonly ExternalLoginProcessing externalLoginProcessing;

		// Token: 0x04003810 RID: 14352
		private AuthenticationContext.AuthenticationState state;

		// Token: 0x02000BC0 RID: 3008
		private enum NegotiateMechanism : byte
		{
			// Token: 0x04003812 RID: 14354
			None,
			// Token: 0x04003813 RID: 14355
			InboundLogon,
			// Token: 0x04003814 RID: 14356
			OutboundLogon,
			// Token: 0x04003815 RID: 14357
			SspiNegotiate,
			// Token: 0x04003816 RID: 14358
			ExchangeAuth,
			// Token: 0x04003817 RID: 14359
			GssapiNegotiate,
			// Token: 0x04003818 RID: 14360
			InboundPlain,
			// Token: 0x04003819 RID: 14361
			OutboundPlain
		}

		// Token: 0x02000BC1 RID: 3009
		private enum AuthenticationState : byte
		{
			// Token: 0x0400381B RID: 14363
			NegotiatingAuth,
			// Token: 0x0400381C RID: 14364
			WaitingForCrLf,
			// Token: 0x0400381D RID: 14365
			WaitingForCapabilities,
			// Token: 0x0400381E RID: 14366
			Done
		}
	}
}
