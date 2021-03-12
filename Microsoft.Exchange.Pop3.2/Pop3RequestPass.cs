using System;
using System.Security;
using System.Text;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.PopImap.Core;
using Microsoft.Exchange.Security.Authentication;

namespace Microsoft.Exchange.Pop3
{
	// Token: 0x0200000F RID: 15
	internal class Pop3RequestPass : Pop3Request, IProxyLogin
	{
		// Token: 0x06000056 RID: 86 RVA: 0x000036E8 File Offset: 0x000018E8
		public Pop3RequestPass(Pop3ResponseFactory factory, byte[] buf, int offset, int size) : base(factory, string.Empty)
		{
			this.PerfCounterTotal = base.Pop3CountersInstance.PerfCounter_PASS_Total;
			this.PerfCounterFailures = base.Pop3CountersInstance.PerfCounter_PASS_Failures;
			base.AllowedStates = Pop3State.User;
			this.userName = factory.UserName;
			int passwordCodePage = base.Session.Server.PasswordCodePage;
			Decoder passwordDecoder = base.GetPasswordDecoder(passwordCodePage);
			this.securePassword = Pop3RequestPass.GetSecureString(buf, offset, size, passwordDecoder);
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000057 RID: 87 RVA: 0x0000375F File Offset: 0x0000195F
		// (set) Token: 0x06000058 RID: 88 RVA: 0x00003767 File Offset: 0x00001967
		public string UserName
		{
			get
			{
				return this.userName;
			}
			set
			{
				this.userName = value;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000059 RID: 89 RVA: 0x00003770 File Offset: 0x00001970
		// (set) Token: 0x0600005A RID: 90 RVA: 0x00003778 File Offset: 0x00001978
		public SecureString Password
		{
			get
			{
				return this.securePassword;
			}
			set
			{
				throw new InvalidOperationException("Password is not settable in Pop3RequestPass command!");
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600005B RID: 91 RVA: 0x00003784 File Offset: 0x00001984
		// (set) Token: 0x0600005C RID: 92 RVA: 0x0000378C File Offset: 0x0000198C
		public string ClientIp { get; set; }

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x0600005D RID: 93 RVA: 0x00003795 File Offset: 0x00001995
		// (set) Token: 0x0600005E RID: 94 RVA: 0x0000379D File Offset: 0x0000199D
		public string ClientPort { get; set; }

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x0600005F RID: 95 RVA: 0x000037A6 File Offset: 0x000019A6
		// (set) Token: 0x06000060 RID: 96 RVA: 0x000037AE File Offset: 0x000019AE
		public string AuthenticationType { get; set; }

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000061 RID: 97 RVA: 0x000037B7 File Offset: 0x000019B7
		// (set) Token: 0x06000062 RID: 98 RVA: 0x000037BF File Offset: 0x000019BF
		public string AuthenticationError { get; set; }

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000063 RID: 99 RVA: 0x000037C8 File Offset: 0x000019C8
		// (set) Token: 0x06000064 RID: 100 RVA: 0x000037D0 File Offset: 0x000019D0
		public string ProxyDestination { get; set; }

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000065 RID: 101 RVA: 0x000037D9 File Offset: 0x000019D9
		// (set) Token: 0x06000066 RID: 102 RVA: 0x000037E1 File Offset: 0x000019E1
		public ILiveIdBasicAuthentication LiveIdBasicAuth { get; set; }

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000067 RID: 103 RVA: 0x000037EA File Offset: 0x000019EA
		// (set) Token: 0x06000068 RID: 104 RVA: 0x000037F2 File Offset: 0x000019F2
		public ADUser AdUser { get; set; }

		// Token: 0x06000069 RID: 105 RVA: 0x000037FB File Offset: 0x000019FB
		public override void ParseArguments()
		{
			this.ParseResult = ParseResult.success;
			if (base.Session.LightLogSession != null)
			{
				base.Session.LightLogSession.Parameters = "*****";
			}
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00003826 File Offset: 0x00001A26
		public override ProtocolResponse Process()
		{
			return base.Factory.DoConnect(this, this.securePassword);
		}

		// Token: 0x0600006B RID: 107 RVA: 0x0000383C File Offset: 0x00001A3C
		private static SecureString GetSecureString(byte[] buf, int offset, int size, Decoder passwordDecoder)
		{
			SecureString secureString = new SecureString();
			if (size > 0)
			{
				if (passwordDecoder != null)
				{
					char[] array = new char[passwordDecoder.GetCharCount(buf, offset, size)];
					passwordDecoder.GetChars(buf, offset, size, array, 0);
					for (int i = 0; i < array.Length; i++)
					{
						secureString.AppendChar(array[i]);
					}
				}
				else
				{
					for (int j = 0; j < size; j++)
					{
						secureString.AppendChar((char)buf[offset + j]);
					}
				}
				Array.Clear(buf, offset, size);
			}
			return secureString;
		}

		// Token: 0x0400003F RID: 63
		internal const string PASSResponseSucceeded = "User successfully logged on.";

		// Token: 0x04000040 RID: 64
		internal const string PASSResponseFailed = "Logon failure: unknown user name or bad password.";

		// Token: 0x04000041 RID: 65
		internal const string PASSResponseFailedLastTime = "Logon failure: unknown user name or bad password.\r\nConnection is closed. 21";

		// Token: 0x04000042 RID: 66
		private string userName;

		// Token: 0x04000043 RID: 67
		private SecureString securePassword;
	}
}
