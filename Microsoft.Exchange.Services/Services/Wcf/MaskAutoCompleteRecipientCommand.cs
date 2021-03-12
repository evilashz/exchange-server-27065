﻿using System;
using System.Linq;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.Inference.PeopleRelevance;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Wcf.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x0200092B RID: 2347
	internal sealed class MaskAutoCompleteRecipientCommand : ServiceCommand<MaskAutoCompleteRecipientResponse>
	{
		// Token: 0x060043FB RID: 17403 RVA: 0x000E7F4C File Offset: 0x000E614C
		public MaskAutoCompleteRecipientCommand(CallContext callContext, MaskAutoCompleteRecipientRequest request) : base(callContext)
		{
			WcfServiceCommandBase.ThrowIfNull(request, "request", "MaskAutoCompleteRecipientCommand::ctor");
			if (string.IsNullOrEmpty(request.EmailAddress))
			{
				throw new ArgumentException("request.EmailAddress is null or empty");
			}
			this.emailAddress = request.EmailAddress;
		}

		// Token: 0x060043FC RID: 17404 RVA: 0x000E7F9C File Offset: 0x000E619C
		protected override MaskAutoCompleteRecipientResponse InternalExecute()
		{
			MailboxSession mailboxIdentityMailboxSession = base.CallContext.SessionCache.GetMailboxIdentityMailboxSession();
			MaskAutoCompleteRecipientCommand.Tracer.TraceDebug<string>((long)this.emailAddress.GetHashCode(), "PeopleRelevanceActions.MaskAutoCompleteRecipient: Masking email address = {0}.", this.emailAddress);
			MdbMaskedPeopleModelDataBinder mdbMaskedPeopleModelDataBinder = MdbMaskedPeopleModelDataBinderFactory.Current.CreateInstance(mailboxIdentityMailboxSession);
			WcfServiceCommandBase.ThrowIfNull(mdbMaskedPeopleModelDataBinder, "dataBinder", "MaskAutoCompleteRecipient.InternalExecute");
			MaskedPeopleModelItem modelData = mdbMaskedPeopleModelDataBinder.GetModelData();
			WcfServiceCommandBase.ThrowIfNull(modelData, "modelItem", "MaskAutoCompleteRecipient.InternalExecute");
			if (!modelData.ContactList.Any((MaskedRecipient r) => r.EmailAddress == this.emailAddress))
			{
				modelData.ContactList.Add(new MaskedRecipient(this.emailAddress));
				MdbModelUtils.WriteModelItem<MaskedPeopleModelItem, MdbMaskedPeopleModelDataBinder>(mdbMaskedPeopleModelDataBinder, modelData);
				MaskAutoCompleteRecipientCommand.Tracer.TraceDebug((long)this.emailAddress.GetHashCode(), "PeopleRelevanceActions.MaskAutoCompleteRecipient: Email address added to MaskedContacts.");
			}
			else
			{
				MaskAutoCompleteRecipientCommand.Tracer.TraceDebug((long)this.emailAddress.GetHashCode(), "PeopleRelevanceActions.MaskAutoCompleteRecipient: Email address already present in MaskedContacts.");
			}
			StoreObjectId defaultFolderId = mailboxIdentityMailboxSession.GetDefaultFolderId(DefaultFolderType.RecipientCache);
			WcfServiceCommandBase.ThrowIfNull(defaultFolderId, "recipientCacheId", "MaskAutoCompleteRecipient.InternalExecute");
			PersonId personId = Person.FindPersonIdByEmailAddress(mailboxIdentityMailboxSession, XSOFactory.Default, defaultFolderId, this.emailAddress);
			WcfServiceCommandBase.ThrowIfNull(personId, "personId", "MaskAutoCompleteRecipient.InternalExecute");
			Person person = Person.Load(mailboxIdentityMailboxSession, personId, null);
			WcfServiceCommandBase.ThrowIfNull(person, "xsoPersonItem", "MaskAutoCompleteRecipient.InternalExecute");
			person.Delete(mailboxIdentityMailboxSession, DeleteItemFlags.HardDelete, defaultFolderId);
			MaskAutoCompleteRecipientCommand.Tracer.TraceDebug((long)this.emailAddress.GetHashCode(), "PeopleRelevanceActions.MaskAutoCompleteRecipient: Person deleted from RecipientCache.");
			return new MaskAutoCompleteRecipientResponse();
		}

		// Token: 0x040027AC RID: 10156
		private static readonly Trace Tracer = ExTraceGlobals.GetPersonaCallTracer;

		// Token: 0x040027AD RID: 10157
		private string emailAddress;
	}
}
