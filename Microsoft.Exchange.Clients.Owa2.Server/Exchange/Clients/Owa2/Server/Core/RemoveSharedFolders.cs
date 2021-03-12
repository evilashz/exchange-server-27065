using System;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Wcf;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x0200034A RID: 842
	internal class RemoveSharedFolders : ServiceCommand<bool>
	{
		// Token: 0x06001B8F RID: 7055 RVA: 0x00069C10 File Offset: 0x00067E10
		public RemoveSharedFolders(CallContext callContext, string primarySMTPAddress) : base(callContext)
		{
			this.primarySMTPAddress = primarySMTPAddress;
		}

		// Token: 0x06001B90 RID: 7056 RVA: 0x00069C20 File Offset: 0x00067E20
		protected override bool InternalExecute()
		{
			return OwaOtherMailboxConfiguration.RemoveOtherMailboxConfig(CallContext.Current, this.primarySMTPAddress);
		}

		// Token: 0x04000F92 RID: 3986
		private readonly string primarySMTPAddress;
	}
}
