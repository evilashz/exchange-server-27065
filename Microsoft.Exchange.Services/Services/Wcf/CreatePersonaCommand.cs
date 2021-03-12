using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x0200090E RID: 2318
	internal class CreatePersonaCommand : UpdateCreatePersonaCommandBase
	{
		// Token: 0x06004330 RID: 17200 RVA: 0x000E119C File Offset: 0x000DF39C
		public CreatePersonaCommand(CallContext callContext, CreatePersonaRequest request) : base(callContext, request)
		{
			WcfServiceCommandBase.ThrowIfNull(request, "CreatePersonaRequest", "CreatePersonaCommand::CreatePersonaCommand");
			WcfServiceCommandBase.ThrowIfNull(request.ParentFolderId, "CreatePersonaRequest.parentFolderId", "CreatePersonaCommand::CreatePersonaCommand");
			this.personaId = request.PersonaId;
			this.parentFolderIdAndSession = base.GetIdAndSession(request.ParentFolderId.BaseFolderId);
		}

		// Token: 0x06004331 RID: 17201 RVA: 0x000E11FC File Offset: 0x000DF3FC
		protected override Persona InternalExecute()
		{
			MailboxSession mailboxIdentityMailboxSession = base.CallContext.SessionCache.GetMailboxIdentityMailboxSession();
			Persona result = Persona.CreatePersona(this.parentFolderIdAndSession.Session, this.propertyChanges, this.parentFolderIdAndSession.Id, this.personaId, this.personType);
			if (base.CallContext.AccessingPrincipal != null && base.CallContext.AccessingPrincipal.GetConfiguration().OwaClientServer.PeopleCentricTriage.Enabled)
			{
				StoreId defaultFolderId = mailboxIdentityMailboxSession.GetDefaultFolderId(DefaultFolderType.RecipientCache);
				if (defaultFolderId != null && defaultFolderId.Equals(this.parentFolderIdAndSession.Id))
				{
					new PeopleIKnowEmailAddressCollectionFolderProperty(XSOFactory.Default, NullTracer.Instance, 0).Publish(mailboxIdentityMailboxSession);
				}
			}
			return result;
		}

		// Token: 0x04002723 RID: 10019
		private IdAndSession parentFolderIdAndSession;

		// Token: 0x04002724 RID: 10020
		protected ItemId personaId;
	}
}
