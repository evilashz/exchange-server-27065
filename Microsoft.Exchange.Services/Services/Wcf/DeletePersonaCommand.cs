using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000910 RID: 2320
	internal sealed class DeletePersonaCommand : ServiceCommand<ServiceResultNone>
	{
		// Token: 0x06004341 RID: 17217 RVA: 0x000E18CF File Offset: 0x000DFACF
		public DeletePersonaCommand(CallContext callContext, ItemId personaId, BaseFolderId deleteInFolder) : base(callContext)
		{
			WcfServiceCommandBase.ThrowIfNull(personaId, "personaId", "DeletePersonaCommand::ctor");
			this.personaId = personaId;
			this.deleteInFolder = deleteInFolder;
		}

		// Token: 0x06004342 RID: 17218 RVA: 0x000E18F8 File Offset: 0x000DFAF8
		protected override ServiceResultNone InternalExecute()
		{
			StoreSession storeSession = base.CallContext.SessionCache.GetMailboxIdentityMailboxSession();
			StoreId storeId = null;
			if (IdConverter.EwsIdIsActiveDirectoryObject(this.personaId.GetId()))
			{
				throw new ServiceInvalidOperationException((CoreResources.IDs)3142918589U);
			}
			PersonId personId = IdConverter.EwsIdToPersonId(this.personaId.Id);
			if (this.deleteInFolder != null)
			{
				IdAndSession idAndSession = base.GetIdAndSession(this.deleteInFolder);
				if (idAndSession.Session.IsPublicFolderSession)
				{
					storeSession = idAndSession.Session;
					storeId = idAndSession.Id;
				}
			}
			Persona.DeletePersona(storeSession, personId, this.personaId, storeId);
			return new ServiceResultNone();
		}

		// Token: 0x0400272B RID: 10027
		private ItemId personaId;

		// Token: 0x0400272C RID: 10028
		private BaseFolderId deleteInFolder;
	}
}
