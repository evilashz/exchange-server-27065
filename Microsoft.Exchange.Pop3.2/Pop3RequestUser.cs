using System;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.PopImap.Core;

namespace Microsoft.Exchange.Pop3
{
	// Token: 0x02000017 RID: 23
	internal class Pop3RequestUser : Pop3Request
	{
		// Token: 0x06000087 RID: 135 RVA: 0x00003F48 File Offset: 0x00002148
		public Pop3RequestUser(Pop3ResponseFactory factory, string input) : base(factory, input)
		{
			this.PerfCounterTotal = base.Pop3CountersInstance.PerfCounter_USER_Total;
			this.PerfCounterFailures = base.Pop3CountersInstance.PerfCounter_USER_Failures;
			base.AllowedStates = (Pop3State.Nonauthenticated | Pop3State.AuthenticatedButFailed);
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00003F7C File Offset: 0x0000217C
		public override bool VerifyState()
		{
			return base.VerifyState() && (base.Factory.Session.IsTls || base.Factory.Session.Server.LoginType < LoginOptions.PlainTextAuthentication);
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00003FB4 File Offset: 0x000021B4
		public override void ParseArguments()
		{
			if (!string.IsNullOrEmpty(base.Arguments))
			{
				this.ParseResult = ParseResult.success;
				return;
			}
			this.ParseResult = ParseResult.invalidNumberOfArguments;
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00003FD2 File Offset: 0x000021D2
		public override ProtocolResponse Process()
		{
			base.Factory.UserName = base.Arguments;
			base.Factory.SessionState = Pop3State.User;
			return new Pop3Response(Pop3Response.Type.ok);
		}
	}
}
