using System;
using Microsoft.Exchange.PopImap.Core;

namespace Microsoft.Exchange.Pop3
{
	// Token: 0x0200000C RID: 12
	internal class Pop3RequestInvalid : Pop3Request
	{
		// Token: 0x0600004D RID: 77 RVA: 0x00003451 File Offset: 0x00001651
		public Pop3RequestInvalid(ResponseFactory factory, string arguments) : base(factory, arguments)
		{
			base.AllowedStates = (Pop3State.Nonauthenticated | Pop3State.User | Pop3State.Authenticated | Pop3State.AuthenticatedButFailed);
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00003463 File Offset: 0x00001663
		public override void ParseArguments()
		{
			this.ParseResult = ParseResult.success;
		}

		// Token: 0x0600004F RID: 79 RVA: 0x0000346C File Offset: 0x0000166C
		public override ProtocolResponse Process()
		{
			if ((base.Factory.InvalidCommands += 1U) > 2U)
			{
				return new Pop3Response(Pop3Response.Type.err, "Protocol error. 20")
				{
					IsDisconnectResponse = true
				};
			}
			return new Pop3Response(Pop3Response.Type.err, base.Arguments);
		}
	}
}
