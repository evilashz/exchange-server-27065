using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000905 RID: 2309
	internal class AcceptPersonaLinkSuggestionCommand : ServiceCommand<Persona>
	{
		// Token: 0x06004305 RID: 17157 RVA: 0x000DF7F8 File Offset: 0x000DD9F8
		public AcceptPersonaLinkSuggestionCommand(CallContext callContext, ItemId personaId, ItemId suggestedPersonaId) : base(callContext)
		{
			WcfServiceCommandBase.ThrowIfNull(personaId, "personaId", "AcceptPersonaLinkSuggestionCommand::AcceptPersonaLinkSuggestionCommand");
			WcfServiceCommandBase.ThrowIfNull(suggestedPersonaId, "suggestedPersonaId", "AcceptPersonaLinkSuggestionCommand::AcceptPersonaLinkSuggestionCommand");
			this.session = callContext.SessionCache.GetMailboxIdentityMailboxSession();
			this.personaId = personaId;
			this.suggestedPersonaId = suggestedPersonaId;
		}

		// Token: 0x06004306 RID: 17158 RVA: 0x000DF84C File Offset: 0x000DDA4C
		protected override Persona InternalExecute()
		{
			if (IdConverter.EwsIdIsActiveDirectoryObject(this.personaId.GetId()))
			{
				throw new ServiceInvalidOperationException((CoreResources.IDs)3142918589U);
			}
			PersonId personId = IdConverter.EwsIdToPersonId(this.personaId.GetId());
			PersonId linkingPersonId = IdConverter.EwsIdToPersonId(this.suggestedPersonaId.GetId());
			MailboxInfoForLinking mailboxInfo = MailboxInfoForLinking.CreateFromMailboxSession(this.session);
			ContactLinkingPerformanceTracker performanceTracker = new ContactLinkingPerformanceTracker(this.session);
			ContactLinkingLogger logger = new ContactLinkingLogger("AcceptPersonaLinkSuggestionCommand", mailboxInfo);
			ManualLink manualLink = new ManualLink(mailboxInfo, logger, performanceTracker);
			manualLink.Link(this.session, linkingPersonId, personId);
			return Persona.LoadFromPersonIdWithGalAggregation(this.session, personId, Persona.FullPersonaShape, null);
		}

		// Token: 0x0400270F RID: 9999
		private readonly MailboxSession session;

		// Token: 0x04002710 RID: 10000
		private readonly ItemId personaId;

		// Token: 0x04002711 RID: 10001
		private readonly ItemId suggestedPersonaId;
	}
}
