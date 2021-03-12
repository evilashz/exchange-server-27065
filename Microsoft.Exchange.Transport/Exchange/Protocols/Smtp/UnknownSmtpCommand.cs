using System;
using Microsoft.Exchange.Data.Transport.Smtp;
using Microsoft.Exchange.Transport;

namespace Microsoft.Exchange.Protocols.Smtp
{
	// Token: 0x02000469 RID: 1129
	internal class UnknownSmtpCommand : SmtpCommand
	{
		// Token: 0x06003433 RID: 13363 RVA: 0x000D2F9F File Offset: 0x000D119F
		public UnknownSmtpCommand(ISmtpSession session, string commandEventString, bool notSupported) : base(session, commandEventString, null, LatencyComponent.None)
		{
			this.notSupported = notSupported;
		}

		// Token: 0x06003434 RID: 13364 RVA: 0x000D2FB4 File Offset: 0x000D11B4
		internal override void InboundParseCommand()
		{
			if (!this.notSupported)
			{
				base.SmtpResponse = SmtpResponse.UnrecognizedCommand;
				base.ParsingStatus = ParsingStatus.ProtocolError;
				return;
			}
			if (string.Equals(base.ProtocolCommandKeyword, "vrfy", StringComparison.OrdinalIgnoreCase))
			{
				base.SmtpResponse = SmtpResponse.UnableToVrfyUser;
				base.ParsingStatus = ParsingStatus.Complete;
				return;
			}
			base.SmtpResponse = SmtpResponse.CommandNotImplemented;
			base.ParsingStatus = ParsingStatus.Complete;
		}

		// Token: 0x06003435 RID: 13365 RVA: 0x000D3014 File Offset: 0x000D1214
		internal override void InboundProcessCommand()
		{
			base.LowAuthenticationLevelTarpitOverride = TarpitAction.DoTarpit;
		}

		// Token: 0x06003436 RID: 13366 RVA: 0x000D301D File Offset: 0x000D121D
		internal override void OutboundCreateCommand()
		{
		}

		// Token: 0x06003437 RID: 13367 RVA: 0x000D301F File Offset: 0x000D121F
		internal override void OutboundFormatCommand()
		{
		}

		// Token: 0x06003438 RID: 13368 RVA: 0x000D3021 File Offset: 0x000D1221
		internal override void OutboundProcessResponse()
		{
		}

		// Token: 0x04001A84 RID: 6788
		private bool notSupported;
	}
}
