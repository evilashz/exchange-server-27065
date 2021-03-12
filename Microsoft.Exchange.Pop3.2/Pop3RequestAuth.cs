using System;
using System.Security;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.PopImap.Core;
using Microsoft.Exchange.Security.Authentication;

namespace Microsoft.Exchange.Pop3
{
	// Token: 0x02000009 RID: 9
	internal class Pop3RequestAuth : Pop3Request, IProxyLogin
	{
		// Token: 0x06000032 RID: 50 RVA: 0x00002ED9 File Offset: 0x000010D9
		public Pop3RequestAuth(Pop3ResponseFactory factory, string input) : base(factory, input)
		{
			this.PerfCounterTotal = base.Pop3CountersInstance.PerfCounter_AUTH_Total;
			this.PerfCounterFailures = base.Pop3CountersInstance.PerfCounter_AUTH_Failures;
			base.AllowedStates = (Pop3State.Nonauthenticated | Pop3State.AuthenticatedButFailed);
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000033 RID: 51 RVA: 0x00002F0D File Offset: 0x0000110D
		// (set) Token: 0x06000034 RID: 52 RVA: 0x00002F15 File Offset: 0x00001115
		public string UserName { get; set; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000035 RID: 53 RVA: 0x00002F1E File Offset: 0x0000111E
		// (set) Token: 0x06000036 RID: 54 RVA: 0x00002F26 File Offset: 0x00001126
		public SecureString Password { get; set; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000037 RID: 55 RVA: 0x00002F2F File Offset: 0x0000112F
		// (set) Token: 0x06000038 RID: 56 RVA: 0x00002F37 File Offset: 0x00001137
		public string ClientIp { get; set; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000039 RID: 57 RVA: 0x00002F40 File Offset: 0x00001140
		// (set) Token: 0x0600003A RID: 58 RVA: 0x00002F48 File Offset: 0x00001148
		public string ClientPort { get; set; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600003B RID: 59 RVA: 0x00002F51 File Offset: 0x00001151
		// (set) Token: 0x0600003C RID: 60 RVA: 0x00002F59 File Offset: 0x00001159
		public string AuthenticationType { get; set; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600003D RID: 61 RVA: 0x00002F62 File Offset: 0x00001162
		// (set) Token: 0x0600003E RID: 62 RVA: 0x00002F6A File Offset: 0x0000116A
		public string AuthenticationError { get; set; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600003F RID: 63 RVA: 0x00002F73 File Offset: 0x00001173
		// (set) Token: 0x06000040 RID: 64 RVA: 0x00002F7B File Offset: 0x0000117B
		public string ProxyDestination { get; set; }

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000041 RID: 65 RVA: 0x00002F84 File Offset: 0x00001184
		// (set) Token: 0x06000042 RID: 66 RVA: 0x00002F8C File Offset: 0x0000118C
		public ILiveIdBasicAuthentication LiveIdBasicAuth { get; set; }

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000043 RID: 67 RVA: 0x00002F95 File Offset: 0x00001195
		// (set) Token: 0x06000044 RID: 68 RVA: 0x00002F9D File Offset: 0x0000119D
		public ADUser AdUser { get; set; }

		// Token: 0x06000045 RID: 69 RVA: 0x00002FA8 File Offset: 0x000011A8
		public override bool VerifyState()
		{
			if (base.Factory.Session.Server.GSSAPIAndNTLMAuthDisabled)
			{
				return base.VerifyState() && (base.Factory.Session.IsTls || base.Factory.Session.Server.LoginType == LoginOptions.PlainTextLogin);
			}
			return base.VerifyState() && (base.Factory.Session.IsTls || base.Factory.Session.Server.LoginType < LoginOptions.SecureLogin);
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00003039 File Offset: 0x00001239
		public override void ParseArguments()
		{
			if (base.Arguments == null)
			{
				this.ParseResult = ParseResult.success;
				return;
			}
			if (base.Arguments.IndexOfAny(ResponseFactory.WordDelimiter) != -1)
			{
				this.ParseResult = ParseResult.invalidNumberOfArguments;
				return;
			}
			this.ParseResult = ParseResult.success;
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00003070 File Offset: 0x00001270
		public ProtocolResponse AuthenticationDone(uint loginAttempts, ResponseFactory.AuthenticationResult authenticationResult)
		{
			ProtocolResponse protocolResponse = null;
			switch (authenticationResult)
			{
			case ResponseFactory.AuthenticationResult.success:
			case ResponseFactory.AuthenticationResult.authenticatedButFailed:
				protocolResponse = new Pop3Response(this, Pop3Response.Type.ok, "User successfully authenticated.");
				break;
			case ResponseFactory.AuthenticationResult.failure:
				if ((ulong)loginAttempts < (ulong)((long)base.Session.Server.MaxFailedLoginAttempts))
				{
					protocolResponse = new Pop3Response(this, Pop3Response.Type.err, "Authentication failure: unknown user name or bad password.");
				}
				else
				{
					protocolResponse = new Pop3Response(this, Pop3Response.Type.err, "Authentication failure: unknown user name or bad password.\r\nConnection is closed. 21");
					protocolResponse.IsDisconnectResponse = true;
				}
				break;
			case ResponseFactory.AuthenticationResult.cancel:
				if ((ulong)loginAttempts < (ulong)((long)base.Session.Server.MaxFailedLoginAttempts))
				{
					protocolResponse = new Pop3Response(this, Pop3Response.Type.err, "The AUTH protocol exchange was canceled by the client.");
				}
				else
				{
					protocolResponse = new Pop3Response(this, Pop3Response.Type.err, "The AUTH protocol exchange was canceled by the client.\r\nConnection is closed. 21");
					protocolResponse.IsDisconnectResponse = true;
				}
				break;
			}
			return protocolResponse;
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00003124 File Offset: 0x00001324
		public override ProtocolResponse Process()
		{
			bool isTls = base.Factory.Session.IsTls;
			bool gssapiandNTLMAuthDisabled = base.Factory.Session.Server.GSSAPIAndNTLMAuthDisabled;
			if (base.Arguments == null)
			{
				switch (base.Factory.Session.Server.LoginType)
				{
				case LoginOptions.PlainTextLogin:
					return new Pop3Response(this, Pop3Response.Type.unknown, gssapiandNTLMAuthDisabled ? "+OK\r\nPLAIN\r\n." : "+OK\r\nNTLM\r\nGSSAPI\r\nPLAIN\r\n.");
				case LoginOptions.PlainTextAuthentication:
					if (isTls)
					{
						return new Pop3Response(this, Pop3Response.Type.unknown, gssapiandNTLMAuthDisabled ? "+OK\r\nPLAIN\r\n." : "+OK\r\nNTLM\r\nGSSAPI\r\nPLAIN\r\n.");
					}
					if (!gssapiandNTLMAuthDisabled)
					{
						return new Pop3Response(this, Pop3Response.Type.unknown, "+OK\r\nNTLM\r\nGSSAPI\r\n.");
					}
					break;
				case LoginOptions.SecureLogin:
					return new Pop3Response(this, Pop3Response.Type.unknown, gssapiandNTLMAuthDisabled ? "+OK\r\nPLAIN\r\n." : "+OK\r\nNTLM\r\nGSSAPI\r\nPLAIN\r\n.");
				}
				throw new InvalidOperationException("The code should never be called!");
			}
			AuthenticationMechanism authenticationMechanism = ResponseFactory.GetAuthenticationMechanism(base.Arguments);
			if (authenticationMechanism == AuthenticationMechanism.None)
			{
				return new Pop3Response(this, Pop3Response.Type.err, "Protocol error. 14");
			}
			if (authenticationMechanism == AuthenticationMechanism.Plain && !isTls && base.Factory.Session.Server.LoginType >= LoginOptions.PlainTextAuthentication)
			{
				return new Pop3Response(this, Pop3Response.Type.err, "Command is not valid in this state.");
			}
			if (authenticationMechanism != AuthenticationMechanism.Plain && gssapiandNTLMAuthDisabled)
			{
				return new Pop3Response(this, Pop3Response.Type.err, "Protocol error. 14");
			}
			return base.Factory.DoAuthenticate(this, authenticationMechanism);
		}

		// Token: 0x04000026 RID: 38
		internal const string AUTHResponseSecureOnly = "+OK\r\nNTLM\r\nGSSAPI\r\n.";

		// Token: 0x04000027 RID: 39
		internal const string AUTHResponseLiveId = "+OK\r\nPLAIN\r\n.";

		// Token: 0x04000028 RID: 40
		internal const string AUTHResponseAll = "+OK\r\nNTLM\r\nGSSAPI\r\nPLAIN\r\n.";

		// Token: 0x04000029 RID: 41
		internal const string AUTHResponseCompleted = "User successfully authenticated.";

		// Token: 0x0400002A RID: 42
		internal const string AUTHResponseFailed = "Authentication failure: unknown user name or bad password.";

		// Token: 0x0400002B RID: 43
		internal const string AUTHResponseFailedLastTime = "Authentication failure: unknown user name or bad password.\r\nConnection is closed. 21";

		// Token: 0x0400002C RID: 44
		internal const string AUTHResponseTerminated = "The AUTH protocol exchange was canceled by the client.";

		// Token: 0x0400002D RID: 45
		internal const string AUTHResponseTerminatedLastTime = "The AUTH protocol exchange was canceled by the client.\r\nConnection is closed. 21";
	}
}
