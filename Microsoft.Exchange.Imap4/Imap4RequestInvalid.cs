using System;
using Microsoft.Exchange.PopImap.Core;

namespace Microsoft.Exchange.Imap4
{
	// Token: 0x0200002A RID: 42
	internal sealed class Imap4RequestInvalid : Imap4Request
	{
		// Token: 0x060001B6 RID: 438 RVA: 0x0000B93F File Offset: 0x00009B3F
		public Imap4RequestInvalid(ResponseFactory factory, string tag, string arguments) : base(factory, tag, arguments)
		{
			base.AllowedStates = (Imap4State.Nonauthenticated | Imap4State.Authenticated | Imap4State.Selected | Imap4State.AuthenticatedButFailed);
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x0000B952 File Offset: 0x00009B52
		public override void ParseArguments()
		{
			this.ParseResult = ParseResult.success;
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x0000B95C File Offset: 0x00009B5C
		public override ProtocolResponse Process()
		{
			if ((base.Factory.InvalidCommands += 1U) > 2U)
			{
				return new Imap4Response(this, Imap4Response.Type.bad, base.Arguments + "\r\n* BYE Connection closed. 14")
				{
					IsDisconnectResponse = true
				};
			}
			return new Imap4Response(this, Imap4Response.Type.bad, base.Arguments);
		}
	}
}
