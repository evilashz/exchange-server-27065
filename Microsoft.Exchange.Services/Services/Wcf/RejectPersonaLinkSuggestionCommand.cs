using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000932 RID: 2354
	internal class RejectPersonaLinkSuggestionCommand : ServiceCommand<Persona>
	{
		// Token: 0x0600444F RID: 17487 RVA: 0x000E9EA0 File Offset: 0x000E80A0
		public RejectPersonaLinkSuggestionCommand(CallContext callContext, ItemId personaId, ItemId suggestedPersonaId) : base(callContext)
		{
			WcfServiceCommandBase.ThrowIfNull(personaId, "personaId", "RejectPersonaLinkSuggestionCommand::RejectPersonaLinkSuggestionCommand");
			WcfServiceCommandBase.ThrowIfNull(suggestedPersonaId, "suggestedPersonaId", "RejectPersonaLinkSuggestionCommand::RejectPersonaLinkSuggestionCommand");
			this.session = callContext.SessionCache.GetMailboxIdentityMailboxSession();
			this.personaId = personaId;
			this.suggestedPersonaId = suggestedPersonaId;
		}

		// Token: 0x06004450 RID: 17488 RVA: 0x000E9EF4 File Offset: 0x000E80F4
		protected override Persona InternalExecute()
		{
			PersonId personId = IdConverter.EwsIdToPersonId(this.personaId.GetId());
			PersonId suggestionPersonId = IdConverter.EwsIdToPersonId(this.suggestedPersonaId.GetId());
			MailboxInfoForLinking mailboxInfo = MailboxInfoForLinking.CreateFromMailboxSession(this.session);
			ContactLinkingPerformanceTracker performanceTracker = new ContactLinkingPerformanceTracker(this.session);
			ContactLinkingLogger logger = new ContactLinkingLogger("RejectPersonaLinkSuggestionCommand", mailboxInfo);
			ManualLink manualLink = new ManualLink(mailboxInfo, logger, performanceTracker);
			manualLink.RejectSuggestion(this.session, personId, suggestionPersonId);
			return Persona.LoadFromPersonIdWithGalAggregation(this.session, personId, Persona.FullPersonaShape, null);
		}

		// Token: 0x040027C9 RID: 10185
		private readonly MailboxSession session;

		// Token: 0x040027CA RID: 10186
		private readonly ItemId personaId;

		// Token: 0x040027CB RID: 10187
		private readonly ItemId suggestedPersonaId;
	}
}
