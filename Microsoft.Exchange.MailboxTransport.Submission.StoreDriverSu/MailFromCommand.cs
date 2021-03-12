using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Diagnostics.Components.Transport;
using Microsoft.Exchange.Protocols.Smtp;
using Microsoft.Exchange.Transport;

namespace Microsoft.Exchange.MailboxTransport.Submission.StoreDriverSubmission
{
	// Token: 0x02000038 RID: 56
	internal class MailFromCommand : DavCommand
	{
		// Token: 0x0600022F RID: 559 RVA: 0x0000CCA4 File Offset: 0x0000AEA4
		public MailFromCommand(byte[] commandBytes) : base(commandBytes)
		{
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x06000230 RID: 560 RVA: 0x0000CCAD File Offset: 0x0000AEAD
		public BodyType BodyType
		{
			get
			{
				return this.bodyType;
			}
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x06000231 RID: 561 RVA: 0x0000CCB5 File Offset: 0x0000AEB5
		public DsnFormat Ret
		{
			get
			{
				return this.ret;
			}
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x06000232 RID: 562 RVA: 0x0000CCBD File Offset: 0x0000AEBD
		public string EnvId
		{
			get
			{
				return this.envId;
			}
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x06000233 RID: 563 RVA: 0x0000CCC5 File Offset: 0x0000AEC5
		public string Auth
		{
			get
			{
				return this.auth;
			}
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x06000234 RID: 564 RVA: 0x0000CCCD File Offset: 0x0000AECD
		protected override byte[] FirstToken
		{
			get
			{
				return MailFromCommand.Mail;
			}
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x06000235 RID: 565 RVA: 0x0000CCD4 File Offset: 0x0000AED4
		protected override byte[] SecondToken
		{
			get
			{
				return MailFromCommand.From;
			}
		}

		// Token: 0x06000236 RID: 566 RVA: 0x0000CCDC File Offset: 0x0000AEDC
		protected override void ParseArguments()
		{
			MailParseOutput mailParseOutput;
			ParseResult parseResult = MailSmtpCommandParser.ParseOptionalArguments(new RoutingAddress("notused@contoso.com"), CommandContext.FromByteArrayLegacyCodeOnly(base.CommandBytes, base.CurrentOffset), ExTraceGlobals.SmtpReceiveTracer, Components.TransportAppConfig.SmtpMailCommandConfiguration, out mailParseOutput, null);
			if (parseResult.IsFailed)
			{
				throw new FormatException(parseResult.SmtpResponse.ToString());
			}
			this.bodyType = mailParseOutput.MailBodyType;
			this.ret = mailParseOutput.DsnFormat;
			this.envId = mailParseOutput.EnvelopeId;
			this.auth = mailParseOutput.Auth;
		}

		// Token: 0x0400012B RID: 299
		private static readonly byte[] Mail = Util.AsciiStringToBytes("mail");

		// Token: 0x0400012C RID: 300
		private static readonly byte[] From = Util.AsciiStringToBytes("from");

		// Token: 0x0400012D RID: 301
		private BodyType bodyType;

		// Token: 0x0400012E RID: 302
		private DsnFormat ret;

		// Token: 0x0400012F RID: 303
		private string envId;

		// Token: 0x04000130 RID: 304
		private string auth;
	}
}
