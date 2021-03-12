using System;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020002F3 RID: 755
	internal class ChangePresence : InstantMessageCommandBase<int>
	{
		// Token: 0x06001969 RID: 6505 RVA: 0x00058B41 File Offset: 0x00056D41
		public ChangePresence(CallContext callContext, InstantMessagePresenceType? presenceSetting) : base(callContext)
		{
			this.presenceSetting = presenceSetting;
		}

		// Token: 0x0600196A RID: 6506 RVA: 0x00058B54 File Offset: 0x00056D54
		protected override int InternalExecute()
		{
			InstantMessageProvider provider = base.Provider;
			if (provider == null)
			{
				return -11;
			}
			if (this.presenceSetting != null)
			{
				return provider.PublishSelfPresence(this.presenceSetting.Value);
			}
			provider.PublishResetStatus();
			return 0;
		}

		// Token: 0x04000DFC RID: 3580
		private readonly InstantMessagePresenceType? presenceSetting;
	}
}
