using System;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Wcf.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x020009D8 RID: 2520
	internal class GetTaskFoldersCommand : ServiceCommand<GetTaskFoldersResponse>
	{
		// Token: 0x06004744 RID: 18244 RVA: 0x000FED8B File Offset: 0x000FCF8B
		public GetTaskFoldersCommand(CallContext context) : base(context)
		{
		}

		// Token: 0x06004745 RID: 18245 RVA: 0x000FED94 File Offset: 0x000FCF94
		protected override GetTaskFoldersResponse InternalExecute()
		{
			return new GetTaskFolders(base.MailboxIdentityMailboxSession).Execute();
		}
	}
}
