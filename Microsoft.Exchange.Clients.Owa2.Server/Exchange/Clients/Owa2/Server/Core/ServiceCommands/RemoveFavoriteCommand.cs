using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Services;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Diagnostics;
using Microsoft.Exchange.Services.Wcf;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core.ServiceCommands
{
	// Token: 0x02000349 RID: 841
	internal class RemoveFavoriteCommand : InstantMessageCommandBase<bool>
	{
		// Token: 0x06001B8C RID: 7052 RVA: 0x00069B9A File Offset: 0x00067D9A
		static RemoveFavoriteCommand()
		{
			OwsLogRegistry.Register(OwaApplication.GetRequestDetailsLogger.Get(ExtensibleLoggerMetadata.EventId), typeof(RemoveFavoriteMetadata), new Type[0]);
		}

		// Token: 0x06001B8D RID: 7053 RVA: 0x00069BC1 File Offset: 0x00067DC1
		public RemoveFavoriteCommand(CallContext callContext, ItemId personaId) : base(callContext)
		{
			WcfServiceCommandBase.ThrowIfNull(personaId, "personaId", "RemoveFavoriteCommand");
			this.personaId = personaId;
			this.logger = callContext.ProtocolLog;
		}

		// Token: 0x06001B8E RID: 7054 RVA: 0x00069BED File Offset: 0x00067DED
		protected override bool InternalExecute()
		{
			return new RemoveFavorite(new XSOFactory(), base.MailboxIdentityMailboxSession, this.logger, this.personaId).Execute();
		}

		// Token: 0x04000F90 RID: 3984
		private readonly ItemId personaId;

		// Token: 0x04000F91 RID: 3985
		private readonly RequestDetailsLogger logger;
	}
}
