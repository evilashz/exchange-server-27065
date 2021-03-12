using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x0200029C RID: 668
	internal class AddNewImContactToGroupCommand : SingleStepServiceCommand<AddNewImContactToGroupRequest, Persona>
	{
		// Token: 0x060011B9 RID: 4537 RVA: 0x00055E81 File Offset: 0x00054081
		public AddNewImContactToGroupCommand(CallContext callContext, AddNewImContactToGroupRequest request) : base(callContext, request)
		{
		}

		// Token: 0x060011BA RID: 4538 RVA: 0x00055E8B File Offset: 0x0005408B
		internal override IExchangeWebMethodResponse GetResponse()
		{
			return new AddNewImContactToGroupResponseMessage(base.Result.Code, base.Result.Error, base.Result.Value);
		}

		// Token: 0x060011BB RID: 4539 RVA: 0x00055EB4 File Offset: 0x000540B4
		internal override ServiceResult<Persona> Execute()
		{
			string imAddress = base.Request.ImAddress;
			string displayName = base.Request.DisplayName;
			StoreId groupId = null;
			if (base.Request.GroupId != null)
			{
				IdAndSession idAndSession = base.IdConverter.ConvertItemIdToIdAndSessionReadOnly(base.Request.GroupId);
				groupId = idAndSession.Id;
			}
			MailboxSession mailboxIdentityMailboxSession = base.CallContext.SessionCache.GetMailboxIdentityMailboxSession();
			PersonId personId = new AddNewImContactToGroup(mailboxIdentityMailboxSession, imAddress, displayName, groupId, new XSOFactory(), Global.UnifiedContactStoreConfiguration).Execute();
			ItemId personaId = IdConverter.PersonaIdFromPersonId(mailboxIdentityMailboxSession.MailboxOwner.MailboxInfo.MailboxGuid, personId);
			Persona value = Persona.LoadFromPersonaId(mailboxIdentityMailboxSession, base.CallContext.ADRecipientSessionContext.GetGALScopedADRecipientSession(base.CallContext.EffectiveCaller.ClientSecurityContext), personaId, Persona.FullPersonaShape, null, null);
			return new ServiceResult<Persona>(value);
		}
	}
}
