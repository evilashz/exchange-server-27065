using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Wcf.Types;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000955 RID: 2389
	internal sealed class GetCalendarFoldersCommand : ServiceCommand<GetCalendarFoldersResponse>
	{
		// Token: 0x060044D7 RID: 17623 RVA: 0x000EE881 File Offset: 0x000ECA81
		public GetCalendarFoldersCommand(CallContext callContext) : base(callContext)
		{
		}

		// Token: 0x060044D8 RID: 17624 RVA: 0x000EE88C File Offset: 0x000ECA8C
		protected override GetCalendarFoldersResponse InternalExecute()
		{
			IConstraintProvider context = base.MailboxIdentityMailboxSession.MailboxOwner.GetContext(null);
			bool enabled = VariantConfiguration.GetSnapshot(context, null, null).OwaClientServer.OwaPublicFolders.Enabled;
			return new GetCalendarFolders(base.MailboxIdentityMailboxSession, base.CallContext.ADRecipientSessionContext.GetADRecipientSession(), enabled).Execute();
		}
	}
}
