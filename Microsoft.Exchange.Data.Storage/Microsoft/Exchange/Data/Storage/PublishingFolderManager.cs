using System;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage.Principal;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000DB0 RID: 3504
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class PublishingFolderManager : SharingFolderManagerBase<PublishingSubscriptionData>
	{
		// Token: 0x06007878 RID: 30840 RVA: 0x00213FE0 File Offset: 0x002121E0
		public PublishingFolderManager(MailboxSession mailboxSession) : base(mailboxSession)
		{
		}

		// Token: 0x1700203A RID: 8250
		// (get) Token: 0x06007879 RID: 30841 RVA: 0x00213FE9 File Offset: 0x002121E9
		protected override ExtendedFolderFlags SharingFolderFlags
		{
			get
			{
				return ExtendedFolderFlags.ReadOnly | ExtendedFolderFlags.WebCalFolder | ExtendedFolderFlags.SharedIn | ExtendedFolderFlags.PersonalShare | ExtendedFolderFlags.ExclusivelyBound;
			}
		}

		// Token: 0x0600787A RID: 30842 RVA: 0x00213FF0 File Offset: 0x002121F0
		protected override LocalizedString CreateLocalFolderName(PublishingSubscriptionData subscriptionData)
		{
			return new LocalizedString(subscriptionData.RemoteFolderName);
		}

		// Token: 0x0600787B RID: 30843 RVA: 0x00213FFD File Offset: 0x002121FD
		protected override void CreateOrUpdateSharingBinding(PublishingSubscriptionData subscriptionData, string localFolderName, StoreObjectId folderId)
		{
			if (this.InternetCalendarBindingExists(folderId))
			{
				return;
			}
			this.CreateInternetCalendarBinding(subscriptionData, folderId);
		}

		// Token: 0x0600787C RID: 30844 RVA: 0x00214014 File Offset: 0x00212214
		private bool InternetCalendarBindingExists(StoreObjectId folderId)
		{
			bool result;
			using (Folder folder = Folder.Bind(base.MailboxSession, folderId))
			{
				using (QueryResult queryResult = folder.ItemQuery(ItemQueryType.Associated, null, null, PublishingFolderManager.QueryBindingColumns))
				{
					result = queryResult.SeekToCondition(SeekReference.OriginBeginning, PublishingFolderManager.SharingProviderGuidFilter);
				}
			}
			return result;
		}

		// Token: 0x0600787D RID: 30845 RVA: 0x00214080 File Offset: 0x00212280
		private void CreateInternetCalendarBinding(PublishingSubscriptionData subscriptionData, StoreObjectId folderId)
		{
			using (Item item = MessageItem.CreateAssociated(base.MailboxSession, folderId))
			{
				item[BindingItemSchema.SharingProviderGuid] = PublishingFolderManager.InternetCalendarProviderGuid;
				item[StoreObjectSchema.ItemClass] = "IPM.Sharing.Binding.In";
				item[BindingItemSchema.SharingLocalType] = subscriptionData.DataType.ContainerClass;
				item.Save(SaveMode.NoConflictResolution);
				ExTraceGlobals.SharingTracer.TraceDebug<IExchangePrincipal, StoreObjectId>((long)this.GetHashCode(), "{0}: PublishingFolderManager.CreateInternetCalendarBinding saved binding message for folder id: {1}", base.MailboxSession.MailboxOwner, subscriptionData.LocalFolderId);
			}
		}

		// Token: 0x04005348 RID: 21320
		private const ExtendedFolderFlags ICalSharingFolderFlags = ExtendedFolderFlags.ReadOnly | ExtendedFolderFlags.WebCalFolder | ExtendedFolderFlags.SharedIn | ExtendedFolderFlags.PersonalShare | ExtendedFolderFlags.ExclusivelyBound;

		// Token: 0x04005349 RID: 21321
		private static readonly PropertyDefinition[] QueryBindingColumns = new PropertyDefinition[]
		{
			BindingItemSchema.SharingProviderGuid,
			StoreObjectSchema.ItemClass
		};

		// Token: 0x0400534A RID: 21322
		public static readonly Guid InternetCalendarProviderGuid = new Guid("{FB98726D-69AE-491e-B7D8-8F0E026E0D5F}");

		// Token: 0x0400534B RID: 21323
		private static readonly ComparisonFilter SharingProviderGuidFilter = new ComparisonFilter(ComparisonOperator.Equal, BindingItemSchema.SharingProviderGuid, PublishingFolderManager.InternetCalendarProviderGuid);

		// Token: 0x02000DB1 RID: 3505
		private enum QueryBindingColumnsIndex
		{
			// Token: 0x0400534D RID: 21325
			SharingProviderGuid,
			// Token: 0x0400534E RID: 21326
			ItemClass
		}
	}
}
