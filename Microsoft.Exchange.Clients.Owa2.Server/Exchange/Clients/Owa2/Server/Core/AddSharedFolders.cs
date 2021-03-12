using System;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Wcf;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000246 RID: 582
	internal class AddSharedFolders : ServiceCommand<bool>
	{
		// Token: 0x060015CD RID: 5581 RVA: 0x0004E392 File Offset: 0x0004C592
		public AddSharedFolders(CallContext callContext, string displayName, string primarySMTPAddress) : base(callContext)
		{
			this.displayName = displayName;
			this.primarySMTPAddress = primarySMTPAddress;
		}

		// Token: 0x060015CE RID: 5582 RVA: 0x0004E3A9 File Offset: 0x0004C5A9
		protected override bool InternalExecute()
		{
			return OwaOtherMailboxConfiguration.AddOtherMailboxConfig(CallContext.Current, this.displayName, this.primarySMTPAddress);
		}

		// Token: 0x04000C18 RID: 3096
		private readonly string displayName;

		// Token: 0x04000C19 RID: 3097
		private readonly string primarySMTPAddress;
	}
}
