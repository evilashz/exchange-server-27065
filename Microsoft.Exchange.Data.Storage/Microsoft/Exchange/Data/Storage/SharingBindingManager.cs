using System;
using Microsoft.Exchange.Data.Storage.Principal;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000DC2 RID: 3522
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class SharingBindingManager : SharingItemManagerBase<SharingBindingData>
	{
		// Token: 0x060078FF RID: 30975 RVA: 0x00216381 File Offset: 0x00214581
		public SharingBindingManager(MailboxSession mailboxSession)
		{
			this.mailboxSession = mailboxSession;
		}

		// Token: 0x06007900 RID: 30976 RVA: 0x00216390 File Offset: 0x00214590
		public SharingBindingData GetSharingBindingDataInFolder(StoreId folderId)
		{
			Util.ThrowOnNullArgument(folderId, "folderId");
			object[] rawBindingQueryInFolder = this.GetRawBindingQueryInFolder(folderId);
			if (rawBindingQueryInFolder != null)
			{
				return this.CreateDataObjectFromItem(rawBindingQueryInFolder);
			}
			return null;
		}

		// Token: 0x06007901 RID: 30977 RVA: 0x002163BC File Offset: 0x002145BC
		public void CreateOrUpdateSharingBinding(SharingBindingData bindingData)
		{
			Util.ThrowOnNullArgument(bindingData, "bindingData");
			Util.ThrowOnNullArgument(bindingData.LocalFolderId, "bindingData.LocalFolderId");
			object[] rawBindingQueryInFolder = this.GetRawBindingQueryInFolder(bindingData.LocalFolderId);
			if (rawBindingQueryInFolder != null)
			{
				ExTraceGlobals.SharingTracer.TraceDebug<IExchangePrincipal, SharingBindingData>((long)this.GetHashCode(), "{0}: updating binding message {1}", this.mailboxSession.MailboxOwner, bindingData);
				SharingBindingData sharingBindingData = this.CreateDataObjectFromItem(rawBindingQueryInFolder);
				if (SharingBindingData.EqualContent(sharingBindingData, bindingData))
				{
					return;
				}
				using (Item item = MessageItem.Bind(this.mailboxSession, sharingBindingData.Id, SharingBindingManager.QueryBindingColumns))
				{
					this.SaveBindingMessage(item, bindingData);
					return;
				}
			}
			ExTraceGlobals.SharingTracer.TraceDebug<IExchangePrincipal, SharingBindingData>((long)this.GetHashCode(), "{0}: creating binding message {1}", this.mailboxSession.MailboxOwner, bindingData);
			using (Item item2 = MessageItem.CreateAssociated(this.mailboxSession, bindingData.LocalFolderId))
			{
				item2[BindingItemSchema.SharingInstanceGuid] = Guid.NewGuid();
				this.SaveBindingMessage(item2, bindingData);
			}
		}

		// Token: 0x06007902 RID: 30978 RVA: 0x002164D0 File Offset: 0x002146D0
		protected override void StampItemFromDataObject(Item item, SharingBindingData bindingData)
		{
			item[BindingItemSchema.SharingInitiatorName] = bindingData.InitiatorName;
			item[BindingItemSchema.SharingInitiatorSmtp] = bindingData.InitiatorSmtpAddress;
			item[BindingItemSchema.SharingRemoteName] = bindingData.RemoteFolderName;
			item[BindingItemSchema.SharingRemoteFolderId] = bindingData.RemoteFolderId;
			item[BindingItemSchema.SharingLocalName] = bindingData.LocalFolderName;
			if (bindingData.LastSyncTimeUtc != null)
			{
				item[BindingItemSchema.SharingLastSync] = new ExDateTime(ExTimeZone.UtcTimeZone, bindingData.LastSyncTimeUtc.Value.ToUniversalTime());
			}
			item[BindingItemSchema.SharingRemoteType] = (item[BindingItemSchema.SharingLocalType] = bindingData.DataType.ContainerClass);
			StoreObjectId storeObjectId = StoreId.GetStoreObjectId(bindingData.LocalFolderId);
			bindingData.LocalFolderId = storeObjectId;
			item[BindingItemSchema.SharingLocalUid] = storeObjectId.ToHexEntryId();
			item[BindingItemSchema.SharingLocalFolderEwsId] = StoreId.StoreIdToEwsId(this.mailboxSession.MailboxOwner.MailboxInfo.MailboxGuid, storeObjectId);
			int num = 11;
			if (bindingData.IsDefaultFolderShared)
			{
				num |= 131072;
			}
			item[BindingItemSchema.SharingFlavor] = num;
			item[BindingItemSchema.SharingRoamLog] = SharingContextRoamLog.UnroamedBinding;
			item[BindingItemSchema.SharingStatus] = SharingContextStatus.Configured;
			item[BindingItemSchema.SharingProviderGuid] = SharingBindingManager.ExternalSharingProviderGuid;
			item[BindingItemSchema.SharingProviderName] = "Microsoft Exchange";
			item[BindingItemSchema.SharingProviderUrl] = "http://www.microsoft.com/exchange/";
			item[StoreObjectSchema.ItemClass] = "IPM.Sharing.Binding.In";
		}

		// Token: 0x06007903 RID: 30979 RVA: 0x00216670 File Offset: 0x00214870
		protected override SharingBindingData CreateDataObjectFromItem(object[] properties)
		{
			VersionedId versionedId = SharingItemManagerBase<SharingBindingData>.TryGetPropertyRef<VersionedId>(properties, 1);
			if (versionedId == null)
			{
				ExTraceGlobals.SharingTracer.TraceError<IExchangePrincipal>((long)this.GetHashCode(), "{0}: Binding is missing ID", this.mailboxSession.MailboxOwner);
				return null;
			}
			string text = SharingItemManagerBase<SharingBindingData>.TryGetPropertyRef<string>(properties, 12);
			if (text == null)
			{
				ExTraceGlobals.SharingTracer.TraceError<IExchangePrincipal, VersionedId>((long)this.GetHashCode(), "{0}: Binding {1} is missing sharingLocalType", this.mailboxSession.MailboxOwner, versionedId);
				return null;
			}
			SharingDataType sharingDataType = SharingDataType.FromContainerClass(text);
			if (sharingDataType == null)
			{
				ExTraceGlobals.SharingTracer.TraceError<IExchangePrincipal, VersionedId, string>((long)this.GetHashCode(), "{0}: Binding {1} has invalid sharingLocalType: {2}", this.mailboxSession.MailboxOwner, versionedId, text);
				return null;
			}
			string text2 = SharingItemManagerBase<SharingBindingData>.TryGetPropertyRef<string>(properties, 8);
			if (text2 == null)
			{
				ExTraceGlobals.SharingTracer.TraceError<IExchangePrincipal, VersionedId>((long)this.GetHashCode(), "{0}: Binding {1} is missing initiatorName", this.mailboxSession.MailboxOwner, versionedId);
				return null;
			}
			string text3 = SharingItemManagerBase<SharingBindingData>.TryGetPropertyRef<string>(properties, 9);
			if (text3 == null)
			{
				ExTraceGlobals.SharingTracer.TraceError<IExchangePrincipal, VersionedId>((long)this.GetHashCode(), "{0}: Binding {1} is missing initiatorSmtpAddress", this.mailboxSession.MailboxOwner, versionedId);
				return null;
			}
			string text4 = SharingItemManagerBase<SharingBindingData>.TryGetPropertyRef<string>(properties, 7);
			if (text4 == null)
			{
				ExTraceGlobals.SharingTracer.TraceError<IExchangePrincipal, VersionedId>((long)this.GetHashCode(), "{0}: Binding {1} is missing remoteFolderName", this.mailboxSession.MailboxOwner, versionedId);
				return null;
			}
			string text5 = SharingItemManagerBase<SharingBindingData>.TryGetPropertyRef<string>(properties, 18);
			if (text5 == null)
			{
				ExTraceGlobals.SharingTracer.TraceError<IExchangePrincipal, VersionedId>((long)this.GetHashCode(), "{0}: Binding {1} is missing remoteFolderId", this.mailboxSession.MailboxOwner, versionedId);
				return null;
			}
			string text6 = SharingItemManagerBase<SharingBindingData>.TryGetPropertyRef<string>(properties, 10);
			if (text6 == null)
			{
				ExTraceGlobals.SharingTracer.TraceError<IExchangePrincipal, VersionedId>((long)this.GetHashCode(), "{0}: Binding {1} is missing localFolderName", this.mailboxSession.MailboxOwner, versionedId);
				return null;
			}
			string text7 = SharingItemManagerBase<SharingBindingData>.TryGetPropertyRef<string>(properties, 11);
			if (text7 == null)
			{
				ExTraceGlobals.SharingTracer.TraceError<IExchangePrincipal, VersionedId>((long)this.GetHashCode(), "{0}: Binding {1} is missing localFolderUid", this.mailboxSession.MailboxOwner, versionedId);
				return null;
			}
			StoreObjectId localFolderId;
			try
			{
				localFolderId = StoreObjectId.FromHexEntryId(text7, sharingDataType.StoreObjectType);
			}
			catch (CorruptDataException)
			{
				ExTraceGlobals.SharingTracer.TraceError<IExchangePrincipal, VersionedId>((long)this.GetHashCode(), "{0}: Binding {1} has invalid localFolderUid", this.mailboxSession.MailboxOwner, versionedId);
				return null;
			}
			int? num = SharingItemManagerBase<SharingBindingData>.TryGetPropertyVal<int>(properties, 3);
			if (num == null)
			{
				ExTraceGlobals.SharingTracer.TraceError<IExchangePrincipal, VersionedId>((long)this.GetHashCode(), "{0}: Binding {1} is missing flavor", this.mailboxSession.MailboxOwner, versionedId);
				return null;
			}
			bool isDefaultFolderShared = 0 != (num.Value & 131072);
			DateTime? lastSyncTimeUtc = null;
			ExDateTime? exDateTime = SharingItemManagerBase<SharingBindingData>.TryGetPropertyVal<ExDateTime>(properties, 15);
			if (exDateTime == null)
			{
				ExTraceGlobals.SharingTracer.TraceDebug<IExchangePrincipal, VersionedId>((long)this.GetHashCode(), "{0}: Binding {1} is missing lastSyncTime", this.mailboxSession.MailboxOwner, versionedId);
			}
			else
			{
				lastSyncTimeUtc = new DateTime?((DateTime)exDateTime.Value.ToUtc());
			}
			return new SharingBindingData(versionedId, sharingDataType, text2, text3, text4, text5, text6, localFolderId, isDefaultFolderShared, lastSyncTimeUtc);
		}

		// Token: 0x06007904 RID: 30980 RVA: 0x00216940 File Offset: 0x00214B40
		private void SaveBindingMessage(Item item, SharingBindingData bindingData)
		{
			this.StampItemFromDataObject(item, bindingData);
			item.Save(SaveMode.NoConflictResolution);
			ExTraceGlobals.SharingTracer.TraceDebug<IExchangePrincipal, SharingBindingData>((long)this.GetHashCode(), "{0}: Saved binding message: {1}", this.mailboxSession.MailboxOwner, bindingData);
		}

		// Token: 0x06007905 RID: 30981 RVA: 0x00216974 File Offset: 0x00214B74
		private object[] GetRawBindingQueryInFolder(StoreId folderId)
		{
			using (Folder folder = Folder.Bind(this.mailboxSession, folderId))
			{
				using (QueryResult queryResult = folder.ItemQuery(ItemQueryType.Associated, null, null, SharingBindingManager.QueryBindingColumns))
				{
					if (queryResult.SeekToCondition(SeekReference.OriginBeginning, SharingBindingManager.SharingProviderGuidFilter))
					{
						object[][] rows = queryResult.GetRows(2);
						if (rows.Length > 0)
						{
							if (rows.Length > 1)
							{
								ExTraceGlobals.SharingTracer.TraceError<IExchangePrincipal, StoreId>((long)this.GetHashCode(), "{0}: There should be only one external sharing binding message associated with a folder, but more than one were found in folder: {1}", this.mailboxSession.MailboxOwner, folderId);
							}
							else
							{
								object[] array = rows[0];
								string x = SharingItemManagerBase<SharingBindingData>.TryGetPropertyRef<string>(array, 2);
								if (StringComparer.OrdinalIgnoreCase.Equals(x, "IPM.Sharing.Binding.In"))
								{
									return array;
								}
							}
						}
						else
						{
							ExTraceGlobals.SharingTracer.TraceDebug((long)this.GetHashCode(), "The total Items length retrieved from the Query is not greater than zero");
						}
					}
					else
					{
						ExTraceGlobals.SharingTracer.TraceDebug<SeekReference, ComparisonFilter>((long)this.GetHashCode(), "Query.SeekToCondition returned false for SeekReference.ForwardFromBeginning {0} and SharingProviderGuidFilter {1}", SeekReference.OriginBeginning, SharingBindingManager.SharingProviderGuidFilter);
					}
				}
			}
			return null;
		}

		// Token: 0x0400538D RID: 21389
		private const string ExternalSharingProviderName = "Microsoft Exchange";

		// Token: 0x0400538E RID: 21390
		private const string ExternalSharingProviderUrl = "http://www.microsoft.com/exchange/";

		// Token: 0x0400538F RID: 21391
		public static readonly Guid ExternalSharingProviderGuid = new Guid("{0006F0C0-0000-0000-C000-000000000046}");

		// Token: 0x04005390 RID: 21392
		private static readonly PropertyDefinition[] QueryBindingColumns = new PropertyDefinition[]
		{
			BindingItemSchema.SharingProviderGuid,
			ItemSchema.Id,
			StoreObjectSchema.ItemClass,
			BindingItemSchema.SharingFlavor,
			BindingItemSchema.SharingStatus,
			BindingItemSchema.SharingProviderName,
			BindingItemSchema.SharingProviderUrl,
			BindingItemSchema.SharingRemoteName,
			BindingItemSchema.SharingInitiatorName,
			BindingItemSchema.SharingInitiatorSmtp,
			BindingItemSchema.SharingLocalName,
			BindingItemSchema.SharingLocalUid,
			BindingItemSchema.SharingLocalType,
			BindingItemSchema.SharingInstanceGuid,
			BindingItemSchema.SharingRemoteType,
			BindingItemSchema.SharingLastSync,
			BindingItemSchema.SharingRoamLog,
			BindingItemSchema.SharingBindingEid,
			BindingItemSchema.SharingRemoteFolderId,
			BindingItemSchema.SharingLocalFolderEwsId
		};

		// Token: 0x04005391 RID: 21393
		private static readonly ComparisonFilter SharingProviderGuidFilter = new ComparisonFilter(ComparisonOperator.Equal, BindingItemSchema.SharingProviderGuid, SharingBindingManager.ExternalSharingProviderGuid);

		// Token: 0x04005392 RID: 21394
		private MailboxSession mailboxSession;

		// Token: 0x02000DC3 RID: 3523
		private enum QueryBindingColumnsIndex
		{
			// Token: 0x04005394 RID: 21396
			SharingProviderGuid,
			// Token: 0x04005395 RID: 21397
			Id,
			// Token: 0x04005396 RID: 21398
			ItemClass,
			// Token: 0x04005397 RID: 21399
			SharingFlavor,
			// Token: 0x04005398 RID: 21400
			SharingStatus,
			// Token: 0x04005399 RID: 21401
			SharingProviderName,
			// Token: 0x0400539A RID: 21402
			SharingProviderUrl,
			// Token: 0x0400539B RID: 21403
			SharingRemoteName,
			// Token: 0x0400539C RID: 21404
			SharingInitiatorName,
			// Token: 0x0400539D RID: 21405
			SharingInitiatorSmtp,
			// Token: 0x0400539E RID: 21406
			SharingLocalName,
			// Token: 0x0400539F RID: 21407
			SharingLocalUid,
			// Token: 0x040053A0 RID: 21408
			SharingLocalType,
			// Token: 0x040053A1 RID: 21409
			SharingInstanceGuid,
			// Token: 0x040053A2 RID: 21410
			SharingRemoteType,
			// Token: 0x040053A3 RID: 21411
			SharingLastSync,
			// Token: 0x040053A4 RID: 21412
			SharingRoamLog,
			// Token: 0x040053A5 RID: 21413
			SharingBindingEid,
			// Token: 0x040053A6 RID: 21414
			SharingRemoteFolderId,
			// Token: 0x040053A7 RID: 21415
			SharingLocalFolderEwsId,
			// Token: 0x040053A8 RID: 21416
			Count
		}
	}
}
