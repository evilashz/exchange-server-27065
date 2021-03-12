using System;
using System.Web;
using Microsoft.Exchange.Data.Storage.Principal;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000DB7 RID: 3511
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class PublishingSubscriptionManager : SharingSubscriptionManagerBase<PublishingSubscriptionKey, PublishingSubscriptionData>
	{
		// Token: 0x060078A8 RID: 30888 RVA: 0x00214B5B File Offset: 0x00212D5B
		public PublishingSubscriptionManager(MailboxSession mailboxSession) : base(mailboxSession, "IPM.PublishingSubscription", PublishingSubscriptionManager.ItemProperties)
		{
		}

		// Token: 0x060078A9 RID: 30889 RVA: 0x00214BA0 File Offset: 0x00212DA0
		protected override object[] FindFirstByKey(PublishingSubscriptionKey subscriptionKey)
		{
			return base.FindFirst((object[] properties) => StringComparer.OrdinalIgnoreCase.Equals(SharingItemManagerBase<PublishingSubscriptionData>.TryGetPropertyRef<string>(properties, 5), HttpUtility.UrlPathEncode(subscriptionKey.PublishingUrl.ToString())));
		}

		// Token: 0x060078AA RID: 30890 RVA: 0x00214BCC File Offset: 0x00212DCC
		protected override void StampItemFromDataObject(Item item, PublishingSubscriptionData subscriptionData)
		{
			item[SharingSchema.ExternalSharingDataType] = subscriptionData.DataType.PublishName;
			item[SharingSchema.ExternalSharingRemoteFolderName] = subscriptionData.RemoteFolderName;
			item[SharingSchema.ExternalSharingLocalFolderId] = subscriptionData.LocalFolderId.GetBytes();
			item[SharingSchema.ExternalSharingUrl] = HttpUtility.UrlPathEncode(subscriptionData.PublishingUrl.ToString());
		}

		// Token: 0x060078AB RID: 30891 RVA: 0x00214C34 File Offset: 0x00212E34
		protected override PublishingSubscriptionData CreateDataObjectFromItem(object[] properties)
		{
			VersionedId versionedId = SharingItemManagerBase<PublishingSubscriptionData>.TryGetPropertyRef<VersionedId>(properties, 1);
			if (versionedId == null)
			{
				ExTraceGlobals.SharingTracer.TraceError<IExchangePrincipal>((long)this.GetHashCode(), "{0}: subscription is missing ID", base.MailboxSession.MailboxOwner);
				return null;
			}
			string text = SharingItemManagerBase<PublishingSubscriptionData>.TryGetPropertyRef<string>(properties, 4);
			if (string.IsNullOrEmpty(text))
			{
				ExTraceGlobals.SharingTracer.TraceError<IExchangePrincipal, VersionedId>((long)this.GetHashCode(), "{0}: subscription {1} is missing ExternalSharingDataType", base.MailboxSession.MailboxOwner, versionedId);
				return null;
			}
			string text2 = SharingItemManagerBase<PublishingSubscriptionData>.TryGetPropertyRef<string>(properties, 5);
			if (string.IsNullOrEmpty(text2))
			{
				ExTraceGlobals.SharingTracer.TraceError<IExchangePrincipal, VersionedId>((long)this.GetHashCode(), "{0}: subscription {1} is missing ExternalSharingUrl", base.MailboxSession.MailboxOwner, versionedId);
				return null;
			}
			if (!Uri.IsWellFormedUriString(text2, UriKind.Absolute))
			{
				ExTraceGlobals.SharingTracer.TraceError<IExchangePrincipal, VersionedId, string>((long)this.GetHashCode(), "{0}: subscription {1} has invalid ExternalSharingUrl: {2}", base.MailboxSession.MailboxOwner, versionedId, text2);
				return null;
			}
			string text3 = SharingItemManagerBase<PublishingSubscriptionData>.TryGetPropertyRef<string>(properties, 6);
			if (string.IsNullOrEmpty(text3))
			{
				ExTraceGlobals.SharingTracer.TraceError<IExchangePrincipal, VersionedId>((long)this.GetHashCode(), "{0}: subscription {1} is missing ExternalSharingRemoteFolderName", base.MailboxSession.MailboxOwner, versionedId);
				return null;
			}
			byte[] array = SharingItemManagerBase<PublishingSubscriptionData>.TryGetPropertyRef<byte[]>(properties, 2);
			if (array == null || array.Length == 0)
			{
				ExTraceGlobals.SharingTracer.TraceError<IExchangePrincipal, VersionedId>((long)this.GetHashCode(), "{0}: subscription {1} is missing ExternalSharingLocalFolderId", base.MailboxSession.MailboxOwner, versionedId);
				return null;
			}
			return new PublishingSubscriptionData(versionedId, text, new Uri(text2), text3, StoreObjectId.Deserialize(array));
		}

		// Token: 0x04005363 RID: 21347
		private static readonly PropertyDefinition[] ItemProperties = new PropertyDefinition[]
		{
			StoreObjectSchema.LastModifiedTime,
			SharingSchema.ExternalSharingDataType,
			SharingSchema.ExternalSharingUrl,
			SharingSchema.ExternalSharingRemoteFolderName
		};

		// Token: 0x02000DB8 RID: 3512
		private enum ItemPropertiesIndex
		{
			// Token: 0x04005365 RID: 21349
			LastModifiedTime = 3,
			// Token: 0x04005366 RID: 21350
			ExternalSharingDataType,
			// Token: 0x04005367 RID: 21351
			ExternalSharingUrl,
			// Token: 0x04005368 RID: 21352
			ExternalSharingRemoteFolderName
		}
	}
}
