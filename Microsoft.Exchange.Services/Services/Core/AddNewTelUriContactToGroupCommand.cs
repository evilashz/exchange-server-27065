using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x0200029E RID: 670
	internal class AddNewTelUriContactToGroupCommand : SingleStepServiceCommand<AddNewTelUriContactToGroupRequest, Persona>
	{
		// Token: 0x060011BE RID: 4542 RVA: 0x000560A3 File Offset: 0x000542A3
		public AddNewTelUriContactToGroupCommand(CallContext callContext, AddNewTelUriContactToGroupRequest request) : base(callContext, request)
		{
		}

		// Token: 0x060011BF RID: 4543 RVA: 0x000560AD File Offset: 0x000542AD
		internal override IExchangeWebMethodResponse GetResponse()
		{
			return new AddNewTelUriContactToGroupResponseMessage(base.Result.Code, base.Result.Error, base.Result.Value);
		}

		// Token: 0x060011C0 RID: 4544 RVA: 0x000560D8 File Offset: 0x000542D8
		internal override ServiceResult<Persona> Execute()
		{
			string telUriAddress = base.Request.TelUriAddress;
			string imContactSipUriAddress = base.Request.ImContactSipUriAddress;
			string imTelephoneNumber = base.Request.ImTelephoneNumber;
			StoreId groupId = null;
			if (base.Request.GroupId != null)
			{
				IdAndSession idAndSession = base.IdConverter.ConvertItemIdToIdAndSessionReadOnly(base.Request.GroupId);
				groupId = idAndSession.Id;
			}
			MailboxSession mailboxIdentityMailboxSession = base.CallContext.SessionCache.GetMailboxIdentityMailboxSession();
			PersonId personId = new AddNewTelUriContactToGroup(mailboxIdentityMailboxSession, telUriAddress, imContactSipUriAddress, imTelephoneNumber, groupId, new XSOFactory(), Global.UnifiedContactStoreConfiguration).Execute();
			ItemId personaId = IdConverter.PersonaIdFromPersonId(mailboxIdentityMailboxSession.MailboxOwner.MailboxInfo.MailboxGuid, personId);
			Persona value = Persona.LoadFromPersonaId(mailboxIdentityMailboxSession, base.CallContext.ADRecipientSessionContext.GetGALScopedADRecipientSession(base.CallContext.EffectiveCaller.ClientSecurityContext), personaId, Persona.FullPersonaShape, null, null);
			return new ServiceResult<Persona>(value);
		}
	}
}
