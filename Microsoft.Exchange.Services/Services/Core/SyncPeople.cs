using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Diagnostics;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x0200038E RID: 910
	internal sealed class SyncPeople : SyncPersonaContactsBase<SyncPeopleRequest, SyncPeopleResponseMessage>
	{
		// Token: 0x0600197C RID: 6524 RVA: 0x0009105A File Offset: 0x0008F25A
		public SyncPeople(CallContext callContext, SyncPeopleRequest request) : base(callContext, request, SyncPeople.syncStateInfo)
		{
		}

		// Token: 0x0600197D RID: 6525 RVA: 0x00091074 File Offset: 0x0008F274
		internal override IExchangeWebMethodResponse GetResponse()
		{
			if (base.Result.Value == null)
			{
				return new SyncPeopleResponseMessage(base.Result.Code, base.Result.Error);
			}
			return base.Result.Value;
		}

		// Token: 0x0600197E RID: 6526 RVA: 0x000910AC File Offset: 0x0008F2AC
		protected override SyncPeopleResponseMessage ExecuteAndGetResult(AllContactsCursor cursor)
		{
			SyncPeople.tracer.TraceDebug((long)this.GetHashCode(), "SyncPeople: Begin executing");
			base.CallContext.ProtocolLog.Set(SyncPersonaContactsMetadata.SyncPersonaContactsType, "SyncPeople");
			base.SplitSyncStates(base.Request.SyncState, null);
			base.PopulateFolderListAndCheckIfChanged();
			if (string.IsNullOrEmpty(base.Request.SyncState))
			{
				this.querySyncInProgress = true;
				base.GetIcsCatchUpStates();
			}
			if (this.querySyncInProgress)
			{
				base.CallContext.ProtocolLog.Set(SyncPersonaContactsMetadata.QuerySync, true);
				base.DoQuerySync(cursor);
			}
			if (!this.querySyncInProgress)
			{
				base.CallContext.ProtocolLog.Set(SyncPersonaContactsMetadata.IcsSync, true);
				base.DoIcsSync(cursor);
			}
			SyncPeopleResponseMessage syncPeopleResponseMessage = new SyncPeopleResponseMessage();
			syncPeopleResponseMessage.SyncState = base.JoinSyncStates(new KeyValuePair<int, string>[0]);
			syncPeopleResponseMessage.People = new Persona[this.people.Count];
			this.people.Values.CopyTo(syncPeopleResponseMessage.People, 0);
			syncPeopleResponseMessage.DeletedPeople = this.deletedPeople.ToArray();
			syncPeopleResponseMessage.JumpHeaderSortKeys = base.GetJumpHeaderSortKeys();
			syncPeopleResponseMessage.SortKeyVersion = PeopleStringUtils.ComputeSortVersion(this.mailboxSession.PreferedCulture);
			syncPeopleResponseMessage.IncludesLastItemInRange = this.includesLastItemInRange;
			SyncPeople.tracer.TraceDebug((long)this.GetHashCode(), "SyncPeople: Done executing");
			return syncPeopleResponseMessage;
		}

		// Token: 0x0600197F RID: 6527 RVA: 0x00091218 File Offset: 0x0008F418
		protected override void AddContacts(PersonId personId, List<IStorePropertyBag> contacts)
		{
			SyncPeople.tracer.TraceDebug<int, PersonId>((long)this.GetHashCode(), "SyncPeople: Adding {0} contacts for {1}", contacts.Count, personId);
			Person person = Person.LoadFromContacts(personId, contacts, this.mailboxSession, this.contactPropertyList, null);
			Dictionary<PropertyDefinition, object> properties = Persona.GetProperties(person.PropertyBag, this.personaPropertyDefinitions);
			Persona persona = Persona.LoadFromPropertyBag(this.mailboxSession, properties, this.personaPropertyList);
			this.people[persona.PersonaId] = persona;
			this.currentReturnSize++;
		}

		// Token: 0x0400111C RID: 4380
		private const string ClassName = "SyncPeople";

		// Token: 0x0400111D RID: 4381
		private static Trace tracer = ExTraceGlobals.SyncPeopleCallTracer;

		// Token: 0x0400111E RID: 4382
		private readonly Dictionary<ItemId, Persona> people = new Dictionary<ItemId, Persona>();

		// Token: 0x0400111F RID: 4383
		private static readonly SyncPersonaContactsBase<SyncPeopleRequest, SyncPeopleResponseMessage>.SyncStateInfo syncStateInfo = new SyncPersonaContactsBase<SyncPeopleRequest, SyncPeopleResponseMessage>.SyncStateInfo
		{
			VersionPrefix = "SR1",
			VersionIndex = 0,
			QuerySyncInProgressIndex = 1,
			LastPersonIdIndex = 2,
			MyContactsHashIndex = 3,
			SyncStatesStartIndex = 4
		};
	}
}
