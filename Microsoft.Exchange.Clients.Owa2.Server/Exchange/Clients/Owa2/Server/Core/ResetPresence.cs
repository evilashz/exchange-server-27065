using System;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x0200034B RID: 843
	internal class ResetPresence : InstantMessageCommandBase<int>
	{
		// Token: 0x06001B91 RID: 7057 RVA: 0x00069C32 File Offset: 0x00067E32
		public ResetPresence(CallContext callContext) : base(callContext)
		{
		}

		// Token: 0x06001B92 RID: 7058 RVA: 0x00069C3C File Offset: 0x00067E3C
		protected override int InternalExecute()
		{
			UserContext userContext = UserContextManager.GetUserContext(CallContext.Current.HttpContext, CallContext.Current.EffectiveCaller, true);
			if (userContext != null)
			{
				userContext.UpdateLastUserRequestTime();
				return 0;
			}
			return -10;
		}
	}
}
