using System;
using System.Security;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.PopImap.Core;
using Microsoft.Exchange.Security.Authentication;

namespace Microsoft.Exchange.Imap4
{
	// Token: 0x0200001F RID: 31
	internal sealed class Imap4RequestAuthenticate : Imap4RequestWithStringParameters, IProxyLogin
	{
		// Token: 0x06000182 RID: 386 RVA: 0x0000A8DC File Offset: 0x00008ADC
		public Imap4RequestAuthenticate(Imap4ResponseFactory factory, string tag, string data) : base(factory, tag, data, 1, 1)
		{
			this.PerfCounterTotal = base.Imap4CountersInstance.PerfCounter_AUTHENTICATE_Total;
			this.PerfCounterFailures = base.Imap4CountersInstance.PerfCounter_AUTHENTICATE_Failures;
			base.AllowedStates = (Imap4State.Nonauthenticated | Imap4State.AuthenticatedButFailed);
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x06000183 RID: 387 RVA: 0x0000A913 File Offset: 0x00008B13
		// (set) Token: 0x06000184 RID: 388 RVA: 0x0000A91B File Offset: 0x00008B1B
		public string UserName { get; set; }

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x06000185 RID: 389 RVA: 0x0000A924 File Offset: 0x00008B24
		// (set) Token: 0x06000186 RID: 390 RVA: 0x0000A92C File Offset: 0x00008B2C
		public SecureString Password { get; set; }

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x06000187 RID: 391 RVA: 0x0000A935 File Offset: 0x00008B35
		// (set) Token: 0x06000188 RID: 392 RVA: 0x0000A93D File Offset: 0x00008B3D
		public string ClientIp { get; set; }

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x06000189 RID: 393 RVA: 0x0000A946 File Offset: 0x00008B46
		// (set) Token: 0x0600018A RID: 394 RVA: 0x0000A94E File Offset: 0x00008B4E
		public string ClientPort { get; set; }

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x0600018B RID: 395 RVA: 0x0000A957 File Offset: 0x00008B57
		// (set) Token: 0x0600018C RID: 396 RVA: 0x0000A95F File Offset: 0x00008B5F
		public string AuthenticationType { get; set; }

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x0600018D RID: 397 RVA: 0x0000A968 File Offset: 0x00008B68
		// (set) Token: 0x0600018E RID: 398 RVA: 0x0000A970 File Offset: 0x00008B70
		public string AuthenticationError { get; set; }

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x0600018F RID: 399 RVA: 0x0000A979 File Offset: 0x00008B79
		// (set) Token: 0x06000190 RID: 400 RVA: 0x0000A981 File Offset: 0x00008B81
		public string ProxyDestination { get; set; }

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x06000191 RID: 401 RVA: 0x0000A98A File Offset: 0x00008B8A
		// (set) Token: 0x06000192 RID: 402 RVA: 0x0000A992 File Offset: 0x00008B92
		public ILiveIdBasicAuthentication LiveIdBasicAuth { get; set; }

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x06000193 RID: 403 RVA: 0x0000A99B File Offset: 0x00008B9B
		// (set) Token: 0x06000194 RID: 404 RVA: 0x0000A9A3 File Offset: 0x00008BA3
		public ADUser AdUser { get; set; }

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x06000195 RID: 405 RVA: 0x0000A9AC File Offset: 0x00008BAC
		public override bool NeedsStoreConnection
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000196 RID: 406 RVA: 0x0000A9B0 File Offset: 0x00008BB0
		public override bool VerifyState()
		{
			if (base.Factory.Session.Server.GSSAPIAndNTLMAuthDisabled)
			{
				return base.VerifyState() && (base.Factory.Session.IsTls || base.Factory.Session.Server.LoginType == LoginOptions.PlainTextLogin);
			}
			return base.VerifyState() && (base.Factory.Session.IsTls || base.Factory.Session.Server.LoginType < LoginOptions.SecureLogin);
		}

		// Token: 0x06000197 RID: 407 RVA: 0x0000AA44 File Offset: 0x00008C44
		public override ProtocolResponse Process()
		{
			bool isTls = base.Factory.Session.IsTls;
			AuthenticationMechanism authenticationMechanism = ResponseFactory.GetAuthenticationMechanism(base.ArrayOfArguments[0]);
			ProtocolBaseServices.SessionTracer.TraceDebug<string, AuthenticationMechanism>(base.Session.SessionId, "Auth mechanism argument {0} became AuthenticationMechanism {1}", base.ArrayOfArguments[0], authenticationMechanism);
			if (authenticationMechanism == AuthenticationMechanism.None)
			{
				return new Imap4Response(this, Imap4Response.Type.bad, "Command Argument Error. 11");
			}
			if (authenticationMechanism == AuthenticationMechanism.Plain && !isTls && base.Factory.Session.Server.LoginType >= LoginOptions.PlainTextAuthentication)
			{
				return new Imap4Response(this, Imap4Response.Type.bad, "Command received in Invalid state.");
			}
			return base.Factory.DoAuthenticate(this, authenticationMechanism);
		}

		// Token: 0x06000198 RID: 408 RVA: 0x0000AAE4 File Offset: 0x00008CE4
		public ProtocolResponse AuthenticationDone(uint loginAttempts, ResponseFactory.AuthenticationResult authenticationResult)
		{
			Imap4Response imap4Response = null;
			switch (authenticationResult)
			{
			case ResponseFactory.AuthenticationResult.success:
			case ResponseFactory.AuthenticationResult.authenticatedButFailed:
			case ResponseFactory.AuthenticationResult.authenticatedAsCafe:
				imap4Response = new Imap4Response(this, Imap4Response.Type.ok, "AUTHENTICATE completed.");
				break;
			case ResponseFactory.AuthenticationResult.failure:
				if ((ulong)loginAttempts < (ulong)((long)base.Session.Server.MaxFailedLoginAttempts))
				{
					imap4Response = new Imap4Response(this, Imap4Response.Type.no, "AUTHENTICATE failed.");
				}
				else
				{
					imap4Response = new Imap4Response(this, Imap4Response.Type.no, "AUTHENTICATE failed.\r\n* BYE Connection is closed. 14");
					imap4Response.IsDisconnectResponse = true;
				}
				break;
			case ResponseFactory.AuthenticationResult.cancel:
				if ((ulong)loginAttempts < (ulong)((long)base.Session.Server.MaxFailedLoginAttempts))
				{
					imap4Response = new Imap4Response(this, Imap4Response.Type.no, "The AUTH protocol exchange was canceled by the client.");
				}
				else
				{
					imap4Response = new Imap4Response(this, Imap4Response.Type.no, "The AUTH protocol exchange was canceled by the client.\r\n* BYE Connection is closed. 14");
					imap4Response.IsDisconnectResponse = true;
				}
				break;
			}
			return imap4Response;
		}

		// Token: 0x04000115 RID: 277
		public const string AUTHENTICATEResponseFailed = "AUTHENTICATE failed.";

		// Token: 0x04000116 RID: 278
		private const string AUTHENTICATEResponseCompleted = "AUTHENTICATE completed.";

		// Token: 0x04000117 RID: 279
		private const string AUTHENTICATEResponseFailedLastTime = "AUTHENTICATE failed.\r\n* BYE Connection is closed. 14";

		// Token: 0x04000118 RID: 280
		private const string AUTHENTICATEResponseTerminated = "The AUTH protocol exchange was canceled by the client.";

		// Token: 0x04000119 RID: 281
		private const string AUTHENTICATEResponseTerminatedLastTime = "The AUTH protocol exchange was canceled by the client.\r\n* BYE Connection is closed. 14";
	}
}
