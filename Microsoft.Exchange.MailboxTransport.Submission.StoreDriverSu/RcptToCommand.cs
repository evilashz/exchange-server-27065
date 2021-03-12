using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Protocols.Smtp;

namespace Microsoft.Exchange.MailboxTransport.Submission.StoreDriverSubmission
{
	// Token: 0x02000039 RID: 57
	internal class RcptToCommand : DavCommand
	{
		// Token: 0x06000238 RID: 568 RVA: 0x0000CD90 File Offset: 0x0000AF90
		public RcptToCommand(byte[] commandBytes) : base(commandBytes)
		{
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x06000239 RID: 569 RVA: 0x0000CD99 File Offset: 0x0000AF99
		public DsnRequestedFlags Notify
		{
			get
			{
				return this.notify;
			}
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x0600023A RID: 570 RVA: 0x0000CDA1 File Offset: 0x0000AFA1
		public string ORcpt
		{
			get
			{
				return this.orcpt;
			}
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x0600023B RID: 571 RVA: 0x0000CDA9 File Offset: 0x0000AFA9
		protected override byte[] FirstToken
		{
			get
			{
				return RcptToCommand.Rcpt;
			}
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x0600023C RID: 572 RVA: 0x0000CDB0 File Offset: 0x0000AFB0
		protected override byte[] SecondToken
		{
			get
			{
				return RcptToCommand.To;
			}
		}

		// Token: 0x0600023D RID: 573 RVA: 0x0000CDB8 File Offset: 0x0000AFB8
		protected override void ParseArguments()
		{
			if (base.Address == RoutingAddress.NullReversePath)
			{
				throw new FormatException("Recipient can't be empty");
			}
			RoutingAddress routingAddress;
			string text;
			byte[] array;
			ParseResult parseResult = RcptSmtpCommandParser.ParseOptionalArguments(CommandContext.FromByteArrayLegacyCodeOnly(base.CommandBytes, base.CurrentOffset), true, false, out this.notify, out this.orcpt, out routingAddress, out text, out array, null);
			if (parseResult.IsFailed)
			{
				throw new FormatException(parseResult.SmtpResponse.ToString());
			}
		}

		// Token: 0x04000131 RID: 305
		private static readonly byte[] Rcpt = Util.AsciiStringToBytes("rcpt");

		// Token: 0x04000132 RID: 306
		private static readonly byte[] To = Util.AsciiStringToBytes("to");

		// Token: 0x04000133 RID: 307
		private DsnRequestedFlags notify;

		// Token: 0x04000134 RID: 308
		private string orcpt;
	}
}
