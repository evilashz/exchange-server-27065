using System;
using Microsoft.Exchange.Data.Storage.Principal;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000DDE RID: 3550
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class SharingSubscriptionManager : SharingSubscriptionManagerBase<SharingSubscriptionKey, SharingSubscriptionData>
	{
		// Token: 0x06007A3A RID: 31290 RVA: 0x0021C891 File Offset: 0x0021AA91
		public SharingSubscriptionManager(MailboxSession mailboxSession) : base(mailboxSession, "IPM.ExternalSharingSubscription", SharingSubscriptionManager.ItemProperties)
		{
		}

		// Token: 0x06007A3B RID: 31291 RVA: 0x0021C8A4 File Offset: 0x0021AAA4
		public SharingSubscriptionData GetPrimary(string dataType, string sharerIdentity)
		{
			base.CheckDisposed("GetPrimary");
			ExTraceGlobals.SharingTracer.TraceDebug<IExchangePrincipal, string, string>((long)this.GetHashCode(), "{0}: looking for subscription of {1} of {2}", base.MailboxSession.MailboxOwner, dataType, sharerIdentity);
			object[] array = this.FindPrimaryByDataType(dataType, sharerIdentity);
			if (array != null)
			{
				SharingSubscriptionData sharingSubscriptionData = this.CreateDataObjectFromItem(array);
				ExTraceGlobals.SharingTracer.TraceDebug<IExchangePrincipal, SharingSubscriptionData>((long)this.GetHashCode(), "{0}: found subscription {1}", base.MailboxSession.MailboxOwner, sharingSubscriptionData);
				return sharingSubscriptionData;
			}
			ExTraceGlobals.SharingTracer.TraceDebug<IExchangePrincipal, string, string>((long)this.GetHashCode(), "{0}: No subscription was found: {1} of {2}.", base.MailboxSession.MailboxOwner, dataType, sharerIdentity);
			return null;
		}

		// Token: 0x06007A3C RID: 31292 RVA: 0x0021C9A0 File Offset: 0x0021ABA0
		private object[] FindPrimaryByDataType(string dataType, string sharerIdentity)
		{
			return base.FindFirst((object[] properties) => StringComparer.OrdinalIgnoreCase.Equals(dataType, SharingItemManagerBase<SharingSubscriptionData>.TryGetPropertyRef<string>(properties, 4)) && StringComparer.OrdinalIgnoreCase.Equals(sharerIdentity, SharingItemManagerBase<SharingSubscriptionData>.TryGetPropertyRef<string>(properties, 5)) && SharingItemManagerBase<SharingSubscriptionData>.TryGetPropertyVal<bool>(properties, 9) == true);
		}

		// Token: 0x06007A3D RID: 31293 RVA: 0x0021CA10 File Offset: 0x0021AC10
		protected override object[] FindFirstByKey(SharingSubscriptionKey subscriptionKey)
		{
			string sharerIdentity = subscriptionKey.SharerIdentity;
			string remoteFolderId = subscriptionKey.RemoteFolderId;
			return base.FindFirst((object[] properties) => StringComparer.OrdinalIgnoreCase.Equals(sharerIdentity, SharingItemManagerBase<SharingSubscriptionData>.TryGetPropertyRef<string>(properties, 5)) && StringComparer.OrdinalIgnoreCase.Equals(remoteFolderId, SharingItemManagerBase<SharingSubscriptionData>.TryGetPropertyRef<string>(properties, 7)));
		}

		// Token: 0x06007A3E RID: 31294 RVA: 0x0021CA50 File Offset: 0x0021AC50
		protected override void StampItemFromDataObject(Item item, SharingSubscriptionData subscriptionData)
		{
			item[SharingSchema.ExternalSharingDataType] = subscriptionData.DataType.ExternalName;
			item[SharingSchema.ExternalSharingSharerIdentity] = subscriptionData.SharerIdentity;
			item[SharingSchema.ExternalSharingSharerName] = subscriptionData.SharerName;
			item[SharingSchema.ExternalSharingRemoteFolderId] = subscriptionData.RemoteFolderId;
			item[SharingSchema.ExternalSharingRemoteFolderName] = subscriptionData.RemoteFolderName;
			item[SharingSchema.ExternalSharingIsPrimary] = subscriptionData.IsPrimary;
			item[SharingSchema.ExternalSharingSharerIdentityFederationUri] = subscriptionData.SharerIdentityFederationUri.ToString();
			item[SharingSchema.ExternalSharingUrl] = subscriptionData.SharingUrl.ToString();
			item[SharingSchema.ExternalSharingLocalFolderId] = subscriptionData.LocalFolderId.GetBytes();
			item[SharingSchema.ExternalSharingSharingKey] = subscriptionData.SharingKey;
			item[SharingSchema.ExternalSharingSubscriberIdentity] = subscriptionData.SubscriberIdentity;
		}

		// Token: 0x06007A3F RID: 31295 RVA: 0x0021CB34 File Offset: 0x0021AD34
		protected override SharingSubscriptionData CreateDataObjectFromItem(object[] properties)
		{
			VersionedId versionedId = SharingItemManagerBase<SharingSubscriptionData>.TryGetPropertyRef<VersionedId>(properties, 1);
			if (versionedId == null)
			{
				ExTraceGlobals.SharingTracer.TraceError<IExchangePrincipal>((long)this.GetHashCode(), "{0}: subscription is missing ID", base.MailboxSession.MailboxOwner);
				return null;
			}
			string text = SharingItemManagerBase<SharingSubscriptionData>.TryGetPropertyRef<string>(properties, 4);
			if (string.IsNullOrEmpty(text))
			{
				ExTraceGlobals.SharingTracer.TraceError<IExchangePrincipal, VersionedId>((long)this.GetHashCode(), "{0}: subscription {1} is missing ExternalSharingDataType", base.MailboxSession.MailboxOwner, versionedId);
				return null;
			}
			string text2 = SharingItemManagerBase<SharingSubscriptionData>.TryGetPropertyRef<string>(properties, 5);
			if (string.IsNullOrEmpty(text2))
			{
				ExTraceGlobals.SharingTracer.TraceError<IExchangePrincipal, VersionedId>((long)this.GetHashCode(), "{0}: subscription {1} is missing ExternalSharingSharerIdentity", base.MailboxSession.MailboxOwner, versionedId);
				return null;
			}
			string text3 = SharingItemManagerBase<SharingSubscriptionData>.TryGetPropertyRef<string>(properties, 6);
			if (string.IsNullOrEmpty(text3))
			{
				ExTraceGlobals.SharingTracer.TraceError<IExchangePrincipal, VersionedId>((long)this.GetHashCode(), "{0}: subscription {1} is missing ExternalSharingSharerName", base.MailboxSession.MailboxOwner, versionedId);
				return null;
			}
			string text4 = SharingItemManagerBase<SharingSubscriptionData>.TryGetPropertyRef<string>(properties, 7);
			if (string.IsNullOrEmpty(text4))
			{
				ExTraceGlobals.SharingTracer.TraceError<IExchangePrincipal, VersionedId>((long)this.GetHashCode(), "{0}: subscription {1} is missing ExternalSharingRemoteFolderId", base.MailboxSession.MailboxOwner, versionedId);
				return null;
			}
			string text5 = SharingItemManagerBase<SharingSubscriptionData>.TryGetPropertyRef<string>(properties, 8);
			if (string.IsNullOrEmpty(text5))
			{
				ExTraceGlobals.SharingTracer.TraceError<IExchangePrincipal, VersionedId>((long)this.GetHashCode(), "{0}: subscription {1} is missing ExternalSharingRemoteFolderName", base.MailboxSession.MailboxOwner, versionedId);
				return null;
			}
			bool? flag = SharingItemManagerBase<SharingSubscriptionData>.TryGetPropertyVal<bool>(properties, 9);
			if (flag == null)
			{
				ExTraceGlobals.SharingTracer.TraceError<IExchangePrincipal, VersionedId>((long)this.GetHashCode(), "{0}: subscription {1} is missing ExternalSharingIsPrimary", base.MailboxSession.MailboxOwner, versionedId);
				return null;
			}
			bool value = flag.Value;
			string text6 = SharingItemManagerBase<SharingSubscriptionData>.TryGetPropertyRef<string>(properties, 10);
			if (string.IsNullOrEmpty(text6))
			{
				ExTraceGlobals.SharingTracer.TraceError<IExchangePrincipal, VersionedId>((long)this.GetHashCode(), "{0}: subscription {1} is missing ExternalSharingSharerIdentityFederationUri", base.MailboxSession.MailboxOwner, versionedId);
				return null;
			}
			if (!Uri.IsWellFormedUriString(text6, UriKind.Absolute))
			{
				ExTraceGlobals.SharingTracer.TraceError<IExchangePrincipal, VersionedId, string>((long)this.GetHashCode(), "{0}: subscription {1} has invalid ExternalSharingSharerIdentityFederationUri: {2}", base.MailboxSession.MailboxOwner, versionedId, text6);
				return null;
			}
			string text7 = SharingItemManagerBase<SharingSubscriptionData>.TryGetPropertyRef<string>(properties, 11);
			if (string.IsNullOrEmpty(text7))
			{
				ExTraceGlobals.SharingTracer.TraceError<IExchangePrincipal, VersionedId>((long)this.GetHashCode(), "{0}: subscription {1} is missing ExternalSharingUrl", base.MailboxSession.MailboxOwner, versionedId);
				return null;
			}
			if (!Uri.IsWellFormedUriString(text7, UriKind.Absolute))
			{
				ExTraceGlobals.SharingTracer.TraceError<IExchangePrincipal, VersionedId, string>((long)this.GetHashCode(), "{0}: subscription {1} has invalid ExternalSharingUrl: {2}", base.MailboxSession.MailboxOwner, versionedId, text7);
				return null;
			}
			byte[] array = SharingItemManagerBase<SharingSubscriptionData>.TryGetPropertyRef<byte[]>(properties, 2);
			if (array == null || array.Length == 0)
			{
				ExTraceGlobals.SharingTracer.TraceError<IExchangePrincipal, VersionedId>((long)this.GetHashCode(), "{0}: subscription {1} is missing ExternalSharingLocalFolderId", base.MailboxSession.MailboxOwner, versionedId);
				return null;
			}
			string text8 = SharingItemManagerBase<SharingSubscriptionData>.TryGetPropertyRef<string>(properties, 12);
			if (string.IsNullOrEmpty(text8))
			{
				ExTraceGlobals.SharingTracer.TraceError<IExchangePrincipal, VersionedId>((long)this.GetHashCode(), "{0}: subscription {1} is missing ExternalSharingSharingKey", base.MailboxSession.MailboxOwner, versionedId);
				return null;
			}
			string text9 = SharingItemManagerBase<SharingSubscriptionData>.TryGetPropertyRef<string>(properties, 13);
			if (string.IsNullOrEmpty(text9))
			{
				ExTraceGlobals.SharingTracer.TraceError<IExchangePrincipal, VersionedId>((long)this.GetHashCode(), "{0}: subscription {1} is missing ExternalSharingSubscriberIdentity", base.MailboxSession.MailboxOwner, versionedId);
				return null;
			}
			return new SharingSubscriptionData(versionedId, text, text2, text3, text4, text5, value, new Uri(text6), new Uri(text7), StoreObjectId.Deserialize(array), text8, text9);
		}

		// Token: 0x0400542A RID: 21546
		private static readonly PropertyDefinition[] ItemProperties = new PropertyDefinition[]
		{
			StoreObjectSchema.LastModifiedTime,
			SharingSchema.ExternalSharingDataType,
			SharingSchema.ExternalSharingSharerIdentity,
			SharingSchema.ExternalSharingSharerName,
			SharingSchema.ExternalSharingRemoteFolderId,
			SharingSchema.ExternalSharingRemoteFolderName,
			SharingSchema.ExternalSharingIsPrimary,
			SharingSchema.ExternalSharingSharerIdentityFederationUri,
			SharingSchema.ExternalSharingUrl,
			SharingSchema.ExternalSharingSharingKey,
			SharingSchema.ExternalSharingSubscriberIdentity
		};

		// Token: 0x02000DDF RID: 3551
		private enum ItemPropertiesIndex
		{
			// Token: 0x0400542C RID: 21548
			LastModifiedTime = 3,
			// Token: 0x0400542D RID: 21549
			ExternalSharingDataType,
			// Token: 0x0400542E RID: 21550
			ExternalSharingSharerIdentity,
			// Token: 0x0400542F RID: 21551
			ExternalSharingSharerName,
			// Token: 0x04005430 RID: 21552
			ExternalSharingRemoteFolderId,
			// Token: 0x04005431 RID: 21553
			ExternalSharingRemoteFolderName,
			// Token: 0x04005432 RID: 21554
			ExternalSharingIsPrimary,
			// Token: 0x04005433 RID: 21555
			ExternalSharingSharerIdentityFederationUri,
			// Token: 0x04005434 RID: 21556
			ExternalSharingUrl,
			// Token: 0x04005435 RID: 21557
			ExternalSharingSharingKey,
			// Token: 0x04005436 RID: 21558
			ExternalSharingSubscriberIdentity
		}
	}
}
