using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Clients;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Diagnostics;
using Microsoft.Exchange.Services.Wcf;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core.ServiceCommands
{
	// Token: 0x02000347 RID: 839
	internal class RemoveBuddy : InstantMessageCommandBase<bool>
	{
		// Token: 0x06001B84 RID: 7044 RVA: 0x00069718 File Offset: 0x00067918
		static RemoveBuddy()
		{
			OwsLogRegistry.Register(OwaApplication.GetRequestDetailsLogger.Get(ExtensibleLoggerMetadata.EventId), typeof(InstantMessagingBuddyMetadata), new Type[0]);
		}

		// Token: 0x06001B85 RID: 7045 RVA: 0x00069768 File Offset: 0x00067968
		public RemoveBuddy(CallContext callContext, InstantMessageBuddy instantMessageBuddy, ItemId personId) : base(callContext)
		{
			WcfServiceCommandBase.ThrowIfNull(instantMessageBuddy, "instantMessageBuddy", "RemoveBuddy");
			WcfServiceCommandBase.ThrowIfNull(personId, "personId", "RemoveBuddy");
			this.instantMessageBuddy = instantMessageBuddy;
			this.personId = IdConverter.EwsIdToPersonId(personId.Id);
		}

		// Token: 0x06001B86 RID: 7046 RVA: 0x000697B4 File Offset: 0x000679B4
		protected override bool InternalExecute()
		{
			InstantMessageProvider provider = base.Provider;
			StoreId storeId = null;
			if (provider == null)
			{
				ExTraceGlobals.InstantMessagingTracer.TraceError((long)this.GetHashCode(), "instant message provider is null");
				return false;
			}
			using (Folder folder = Folder.Bind(base.MailboxIdentityMailboxSession, DefaultFolderType.QuickContacts))
			{
				ExTraceGlobals.InstantMessagingTracer.TraceDebug<string, string>((long)this.GetHashCode(), "Removing instant message buddy:{0},{1}", this.instantMessageBuddy.DisplayName, this.instantMessageBuddy.SipUri);
				ComparisonFilter queryFilter = new ComparisonFilter(ComparisonOperator.Equal, ContactSchema.PersonId, this.personId);
				using (QueryResult queryResult = folder.ItemQuery(ItemQueryType.None, queryFilter, null, RemoveBuddy.contactProperties))
				{
					IStorePropertyBag[] propertyBags = queryResult.GetPropertyBags(1);
					if (propertyBags == null || propertyBags.Length == 0)
					{
						ExTraceGlobals.InstantMessagingTracer.TraceDebug<string, string>((long)this.GetHashCode(), "Lync contact cannot be found in store: {0},{1}", this.instantMessageBuddy.DisplayName, this.instantMessageBuddy.SipUri);
						return false;
					}
					storeId = propertyBags[0].GetValueOrDefault<VersionedId>(ItemSchema.Id, null).ObjectId;
				}
			}
			ExTraceGlobals.InstantMessagingTracer.TraceDebug<string, string>((long)this.GetHashCode(), "Removing instant message buddy after retrieving store id of the contact:{0},{1}", this.instantMessageBuddy.DisplayName, storeId.ToBase64String());
			provider.RemoveBuddy(base.MailboxIdentityMailboxSession, this.instantMessageBuddy, storeId);
			return true;
		}

		// Token: 0x04000F86 RID: 3974
		private static readonly PropertyDefinition[] contactProperties = new PropertyDefinition[]
		{
			ContactSchema.PersonId,
			ItemSchema.Id
		};

		// Token: 0x04000F87 RID: 3975
		private InstantMessageBuddy instantMessageBuddy;

		// Token: 0x04000F88 RID: 3976
		private PersonId personId;
	}
}
