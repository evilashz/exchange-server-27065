using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000926 RID: 2342
	internal class GetPersonaSuggestionsCommand : ServiceCommand<ItemId[]>
	{
		// Token: 0x060043DC RID: 17372 RVA: 0x000E6A96 File Offset: 0x000E4C96
		public GetPersonaSuggestionsCommand(CallContext callContext, ItemId personaId) : base(callContext)
		{
			WcfServiceCommandBase.ThrowIfNull(personaId, "personaId", "GetPersonaSuggestions::GetPersonaSuggestions");
			this.session = callContext.SessionCache.GetMailboxIdentityMailboxSession();
			this.personaId = personaId;
		}

		// Token: 0x060043DD RID: 17373 RVA: 0x000E6AC8 File Offset: 0x000E4CC8
		protected override ItemId[] InternalExecute()
		{
			if (this.personaId == null || string.IsNullOrEmpty(this.personaId.Id))
			{
				throw new ObjectNotFoundException(ServerStrings.ExItemNotFound);
			}
			ItemId[] result;
			if (IdConverter.EwsIdIsActiveDirectoryObject(this.personaId.GetId()))
			{
				result = Array<ItemId>.Empty;
			}
			else
			{
				PersonId personId = IdConverter.EwsIdToPersonId(this.personaId.GetId());
				Person person = Person.Load(this.session, personId, null, Person.SuggestionProperties, null);
				result = GetPersonaSuggestionsCommand.ConvertToItemIdArray(this.session, person.GetSuggestions());
			}
			return result;
		}

		// Token: 0x060043DE RID: 17374 RVA: 0x000E6B50 File Offset: 0x000E4D50
		private static ItemId[] ConvertToItemIdArray(MailboxSession mailboxSession, IEnumerable<PersonId> personIds)
		{
			if (personIds == null)
			{
				return GetPersonaSuggestionsCommand.NoSuggestionsResult;
			}
			List<ItemId> list = new List<ItemId>();
			foreach (PersonId personId in personIds)
			{
				string id = IdConverter.PersonIdToEwsId(mailboxSession.MailboxOwner.MailboxInfo.MailboxGuid, personId);
				ItemId item = new ItemId(id, null);
				list.Add(item);
			}
			if (list.Count > 0)
			{
				return list.ToArray();
			}
			return GetPersonaSuggestionsCommand.NoSuggestionsResult;
		}

		// Token: 0x04002791 RID: 10129
		private static readonly ItemId[] NoSuggestionsResult = new ItemId[0];

		// Token: 0x04002792 RID: 10130
		private readonly MailboxSession session;

		// Token: 0x04002793 RID: 10131
		private readonly ItemId personaId;
	}
}
