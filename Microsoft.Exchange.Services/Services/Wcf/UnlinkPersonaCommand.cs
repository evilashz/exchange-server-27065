using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x0200093C RID: 2364
	internal class UnlinkPersonaCommand : ServiceCommand<Persona>
	{
		// Token: 0x06004470 RID: 17520 RVA: 0x000EB584 File Offset: 0x000E9784
		public UnlinkPersonaCommand(CallContext callContext, ItemId personaId, ItemId contactId) : base(callContext)
		{
			WcfServiceCommandBase.ThrowIfNull(personaId, "personaId", "UnlinkPersona::UnlinkPersona");
			WcfServiceCommandBase.ThrowIfNull(contactId, "contactId", "UnlinkPersona::UnlinkPersona");
			this.session = callContext.SessionCache.GetMailboxIdentityMailboxSession();
			this.personaId = personaId;
			this.contactId = contactId;
		}

		// Token: 0x06004471 RID: 17521 RVA: 0x000EB5D8 File Offset: 0x000E97D8
		protected override Persona InternalExecute()
		{
			PersonId personId = IdConverter.EwsIdToPersonId(this.personaId.GetId());
			MailboxInfoForLinking mailboxInfo = MailboxInfoForLinking.CreateFromMailboxSession(this.session);
			ContactLinkingPerformanceTracker performanceTracker = new ContactLinkingPerformanceTracker(this.session);
			ContactLinkingLogger logger = new ContactLinkingLogger("UnlinkPersonaCommand", mailboxInfo);
			ManualLink manualLink = new ManualLink(mailboxInfo, logger, performanceTracker);
			if (IdConverter.EwsIdIsActiveDirectoryObject(this.contactId.Id))
			{
				ADObjectId adobjectId = IdConverter.EwsIdToADObjectId(this.contactId.Id);
				manualLink.Unlink(this.session, personId, adobjectId.ObjectGuid);
			}
			else
			{
				StoreId storeId = IdConverter.EwsIdToMessageStoreObjectId(this.contactId.Id);
				VersionedId versionedId = VersionedId.Deserialize(storeId.ToBase64String(), this.contactId.ChangeKey);
				manualLink.Unlink(this.session, personId, versionedId);
			}
			return Persona.LoadFromPersonIdWithGalAggregation(this.session, personId, Persona.FullPersonaShape, null);
		}

		// Token: 0x040027E6 RID: 10214
		private readonly MailboxSession session;

		// Token: 0x040027E7 RID: 10215
		private readonly ItemId personaId;

		// Token: 0x040027E8 RID: 10216
		private readonly ItemId contactId;
	}
}
