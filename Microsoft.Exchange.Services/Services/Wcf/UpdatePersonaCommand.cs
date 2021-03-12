using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000941 RID: 2369
	internal class UpdatePersonaCommand : UpdateCreatePersonaCommandBase
	{
		// Token: 0x0600448D RID: 17549 RVA: 0x000EC4E0 File Offset: 0x000EA6E0
		public UpdatePersonaCommand(CallContext callContext, UpdatePersonaRequest request) : base(callContext, request)
		{
			WcfServiceCommandBase.ThrowIfNull(request, "UpdatePersonaRequest", "UpdatePersonaCommand::UpdatePersonaCommand");
			WcfServiceCommandBase.ThrowIfNull(request.PersonaId, "UpdatePersonaRequest.PersonaId", "UpdatePersonaCommand::UpdatePersonaCommand");
			this.personaId = request.PersonaId;
			if (request.ParentFolderId != null)
			{
				this.idAndSession = base.GetIdAndSession(request.ParentFolderId.BaseFolderId);
			}
		}

		// Token: 0x0600448E RID: 17550 RVA: 0x000EC548 File Offset: 0x000EA748
		protected override Persona InternalExecute()
		{
			StoreSession storeSession = base.CallContext.SessionCache.GetMailboxIdentityMailboxSession();
			StoreId folderId = null;
			if (this.idAndSession != null && this.idAndSession.Session.IsPublicFolderSession)
			{
				storeSession = this.idAndSession.Session;
				folderId = this.idAndSession.Id;
			}
			PersonId personId = IdConverter.EwsIdToPersonId(this.personaId.GetId());
			return Persona.UpdatePersona(storeSession, personId, this.personaId, this.propertyChanges, this.personType, folderId);
		}

		// Token: 0x040027F4 RID: 10228
		protected ItemId personaId;

		// Token: 0x040027F5 RID: 10229
		private IdAndSession idAndSession;
	}
}
