using System;
using System.Collections;
using System.Net;
using System.Xml;
using Microsoft.Exchange.AirSync.SchemaConverter;
using Microsoft.Exchange.AirSync.SchemaConverter.AirSync;
using Microsoft.Exchange.AirSync.SchemaConverter.RecipientInfoCache;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.AirSync;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x02000115 RID: 277
	internal class RecipientInfoCacheSyncCollection : SyncCollection
	{
		// Token: 0x06000EB5 RID: 3765 RVA: 0x00053AFD File Offset: 0x00051CFD
		public RecipientInfoCacheSyncCollection(StoreSession storeSession, int protocolVersion) : base(storeSession, protocolVersion)
		{
			base.ClassType = "RecipientInfoCache";
			base.Permissions = SyncPermissions.Readonly;
		}

		// Token: 0x170005A8 RID: 1448
		// (get) Token: 0x06000EB6 RID: 3766 RVA: 0x00053B19 File Offset: 0x00051D19
		public override PropertyDefinition[] PropertiesToSaveForNullSync
		{
			get
			{
				return RecipientInfoCacheSyncCollection.propertiesToSaveForNullSync;
			}
		}

		// Token: 0x170005A9 RID: 1449
		// (get) Token: 0x06000EB7 RID: 3767 RVA: 0x00053B20 File Offset: 0x00051D20
		public override StoreObjectId NativeStoreObjectId
		{
			get
			{
				return ((RecipientInfoCacheSyncProviderFactory)base.SyncProviderFactory).NativeStoreObjectId;
			}
		}

		// Token: 0x06000EB8 RID: 3768 RVA: 0x00053B34 File Offset: 0x00051D34
		public override void ParseSyncOptionsNode()
		{
			using (XmlNodeList childNodes = base.OptionsNode.ChildNodes)
			{
				foreach (object obj in childNodes)
				{
					XmlNode xmlNode = (XmlNode)obj;
					string localName;
					if ((localName = xmlNode.LocalName) == null || !(localName == "MaxItems"))
					{
						base.Status = SyncBase.ErrorCodeStatus.ProtocolError;
						throw new AirSyncPermanentException(HttpStatusCode.BadRequest, StatusCode.InvalidXML, null, false)
						{
							ErrorStringForProtocolLogger = "InvalidNode(" + xmlNode.LocalName + ")InRICacheSync"
						};
					}
					int num;
					if (!int.TryParse(xmlNode.InnerText, out num) || num <= 0)
					{
						base.Status = SyncBase.ErrorCodeStatus.ProtocolError;
						throw new AirSyncPermanentException(HttpStatusCode.BadRequest, StatusCode.Sync_ProtocolError, null, false)
						{
							ErrorStringForProtocolLogger = "InvalidMaxItemsNodeInRICacheSync"
						};
					}
					base.MaxItems = num;
					((RecipientInfoCacheSyncProviderFactory)base.SyncProviderFactory).MaxEntries = base.MaxItems;
				}
			}
		}

		// Token: 0x06000EB9 RID: 3769 RVA: 0x00053C54 File Offset: 0x00051E54
		public override EventCondition CreateEventCondition()
		{
			if (this.NativeStoreObjectId == null)
			{
				return null;
			}
			return new EventCondition
			{
				ObjectType = EventObjectType.Item,
				EventType = (EventType.ObjectDeleted | EventType.ObjectModified),
				ObjectIds = 
				{
					this.NativeStoreObjectId
				}
			};
		}

		// Token: 0x06000EBA RID: 3770 RVA: 0x00053C92 File Offset: 0x00051E92
		public override void CreateSyncProvider()
		{
			base.SyncProviderFactory = new RecipientInfoCacheSyncProviderFactory((MailboxSession)base.StoreSession);
		}

		// Token: 0x06000EBB RID: 3771 RVA: 0x00053CAC File Offset: 0x00051EAC
		public override void ParseFilterType(XmlNode filterTypeNode)
		{
			throw new AirSyncPermanentException(HttpStatusCode.BadRequest, StatusCode.InvalidXML, null, false)
			{
				ErrorStringForProtocolLogger = "InvalidFilterNode(" + filterTypeNode.LocalName + ")InRICacheSync"
			};
		}

		// Token: 0x06000EBC RID: 3772 RVA: 0x00053CE4 File Offset: 0x00051EE4
		public override void ParseSupportedTags(XmlNode parentNode)
		{
			throw new AirSyncPermanentException(HttpStatusCode.BadRequest, StatusCode.InvalidXML, null, false)
			{
				ErrorStringForProtocolLogger = "InvalidSupportedTagInRICacheSync"
			};
		}

		// Token: 0x06000EBD RID: 3773 RVA: 0x00053D0C File Offset: 0x00051F0C
		public override bool HasSchemaPropertyChanged(ISyncItem syncItem, int?[] oldChangeTrackingInformation, XmlDocument xmlResponse, MailboxLogger mailboxLogger)
		{
			throw new AirSyncPermanentException(HttpStatusCode.NotImplemented, StatusCode.UnexpectedItemClass, null, false)
			{
				ErrorStringForProtocolLogger = "NoSchemaPropChangeInRICacheSync"
			};
		}

		// Token: 0x06000EBE RID: 3774 RVA: 0x00053D37 File Offset: 0x00051F37
		public override bool GenerateResponsesXmlNode(XmlDocument xmlResponse, IAirSyncVersionFactory versionFactory, string deviceType, GlobalInfo globalInfo, ProtocolLogger protocolLogger, MailboxLogger mailboxLogger)
		{
			return false;
		}

		// Token: 0x06000EBF RID: 3775 RVA: 0x00053D3C File Offset: 0x00051F3C
		public override bool CollectionRequiresSync(bool ignoreSyncKeyAndFilter, bool nullSyncAllowed)
		{
			if (!nullSyncAllowed)
			{
				return true;
			}
			UserSyncStateMetadata userSyncStateMetadata = UserSyncStateMetadataCache.Singleton.Get(base.StoreSession as MailboxSession, null);
			DeviceSyncStateMetadata device = userSyncStateMetadata.GetDevice(base.StoreSession as MailboxSession, base.Context.Request.DeviceIdentity, null);
			FolderSyncStateMetadata folderSyncStateMetadata = device.GetSyncState(base.StoreSession as MailboxSession, base.CollectionId, null) as FolderSyncStateMetadata;
			return folderSyncStateMetadata == null || folderSyncStateMetadata.AirSyncLocalCommitTime != ((RecipientInfoCacheSyncProviderFactory)base.SyncProviderFactory).LastModifiedTime.UtcTicks || base.SyncKey != (uint)folderSyncStateMetadata.AirSyncSyncKey || (base.HasMaxItemsNode && base.MaxItems != folderSyncStateMetadata.AirSyncMaxItems);
		}

		// Token: 0x06000EC0 RID: 3776 RVA: 0x00053DF8 File Offset: 0x00051FF8
		public override void UpdateSavedNullSyncPropertiesInCache(object[] values)
		{
			FolderSyncStateMetadata folderSyncStateMetadata = base.GetFolderSyncStateMetadata();
			if (folderSyncStateMetadata != null)
			{
				folderSyncStateMetadata.UpdateRecipientInfoCacheNullSyncValues((long)values[0], (int)values[1], (int)values[2]);
			}
		}

		// Token: 0x06000EC1 RID: 3777 RVA: 0x00053E30 File Offset: 0x00052030
		public override object[] GetNullSyncPropertiesToSave()
		{
			ExDateTime exDateTime;
			if (base.SyncKey != 0U && !base.MoreAvailable && base.GetChanges)
			{
				exDateTime = ((RecipientInfoCacheSyncProviderFactory)base.SyncProviderFactory).LastModifiedTime;
			}
			else
			{
				exDateTime = ExDateTime.MinValue;
			}
			return new object[]
			{
				exDateTime.UtcTicks,
				(int)base.ResponseSyncKey,
				base.MaxItems
			};
		}

		// Token: 0x06000EC2 RID: 3778 RVA: 0x00053EA4 File Offset: 0x000520A4
		public override void ConvertServerToClientObject(ISyncItem syncItem, XmlNode airSyncParentNode, SyncOperation changeObject, GlobalInfo globalInfo)
		{
			RecipientInfoCacheEntry entry = (RecipientInfoCacheEntry)syncItem.NativeItem;
			this.recipientInfoCacheDataObject.Bind(entry);
			base.AirSyncDataObject.Bind(airSyncParentNode);
			base.AirSyncDataObject.CopyFrom(this.recipientInfoCacheDataObject);
			base.AirSyncDataObject.Unbind();
			if (changeObject != null && (changeObject.ChangeType == ChangeType.Add || changeObject.ChangeType == ChangeType.Change))
			{
				changeObject.ChangeTrackingInformation = base.ChangeTrackFilter.Filter(airSyncParentNode, changeObject.ChangeTrackingInformation);
			}
			if (changeObject != null && (changeObject.ChangeType == ChangeType.Add || changeObject.ChangeType == ChangeType.Change))
			{
				base.HasAddsOrChangesToReturnToClientImmediately = true;
			}
			base.HasServerChanges = true;
		}

		// Token: 0x06000EC3 RID: 3779 RVA: 0x00053F41 File Offset: 0x00052141
		public override void SetFolderSyncOptions(IAirSyncVersionFactory versionFactory, bool isQuarantineMailAvailable, GlobalInfo globalInfo)
		{
			base.FilterType = AirSyncV25FilterTypes.NoFilter;
			base.SyncState[CustomStateDatumType.FilterType] = new Int32Data((int)base.FilterType);
			base.SyncState[CustomStateDatumType.MaxItems] = new Int32Data(base.MaxItems);
		}

		// Token: 0x06000EC4 RID: 3780 RVA: 0x00053F80 File Offset: 0x00052180
		public override void SetSchemaConverterOptions(IDictionary schemaConverterOptions, IAirSyncVersionFactory versionFactory)
		{
			AirSyncRecipientInfoCacheSchemaState airSyncRecipientInfoCacheSchemaState = (AirSyncRecipientInfoCacheSchemaState)base.SchemaState;
			base.ChangeTrackFilter = ChangeTrackingFilterFactory.CreateFilter(base.ClassType, base.ProtocolVersion);
			IAirSyncMissingPropertyStrategy missingPropertyStrategy = versionFactory.CreateMissingPropertyStrategy(null);
			base.AirSyncDataObject = airSyncRecipientInfoCacheSchemaState.GetAirSyncDataObject(schemaConverterOptions, missingPropertyStrategy);
			this.recipientInfoCacheDataObject = airSyncRecipientInfoCacheSchemaState.GetRecipientInfoCacheDataObject();
		}

		// Token: 0x06000EC5 RID: 3781 RVA: 0x00053FD4 File Offset: 0x000521D4
		public override void OpenSyncState(bool autoLoadFilterAndSyncKey, SyncStateStorage syncStateStorage)
		{
			if (!(base.StoreSession is MailboxSession))
			{
				throw new InvalidOperationException();
			}
			RecipientInfoCacheSyncProviderFactory recipientInfoCacheSyncProviderFactory = (RecipientInfoCacheSyncProviderFactory)base.SyncProviderFactory;
			if (base.SyncKey != 0U || autoLoadFilterAndSyncKey)
			{
				base.SyncState = syncStateStorage.GetFolderSyncState(base.SyncProviderFactory, base.CollectionId);
				if (base.SyncState == null)
				{
					if (autoLoadFilterAndSyncKey)
					{
						base.Status = SyncBase.ErrorCodeStatus.InvalidSyncKey;
						base.ResponseSyncKey = base.SyncKey;
						throw new AirSyncPermanentException(false)
						{
							ErrorStringForProtocolLogger = "NoSyncStateInRICacheSync"
						};
					}
					using (CustomSyncState customSyncState = syncStateStorage.GetCustomSyncState(new FolderIdMappingSyncStateInfo(), new PropertyDefinition[0]))
					{
						if (customSyncState == null || customSyncState[CustomStateDatumType.IdMapping] == null)
						{
							base.Status = SyncBase.ErrorCodeStatus.InvalidCollection;
							throw new AirSyncPermanentException(false)
							{
								ErrorStringForProtocolLogger = "BadFolderMapTreeInRICacheSync"
							};
						}
					}
					base.Status = SyncBase.ErrorCodeStatus.InvalidSyncKey;
					base.ResponseSyncKey = base.SyncKey;
					throw new AirSyncPermanentException(false)
					{
						ErrorStringForProtocolLogger = "Sk0ErrorInRICacheSync"
					};
				}
				else
				{
					base.CheckProtocolVersion();
					if (autoLoadFilterAndSyncKey)
					{
						if (!base.SyncState.Contains(CustomStateDatumType.SyncKey))
						{
							base.Status = SyncBase.ErrorCodeStatus.InvalidSyncKey;
							base.ResponseSyncKey = base.SyncKey;
							throw new AirSyncPermanentException(false)
							{
								ErrorStringForProtocolLogger = "NoSyncStateKeyInRICacheSync"
							};
						}
						base.SyncKey = ((UInt32Data)base.SyncState[CustomStateDatumType.SyncKey]).Data;
						if (base.SyncState.Contains(CustomStateDatumType.RecoverySyncKey))
						{
							base.RecoverySyncKey = ((UInt32Data)base.SyncState[CustomStateDatumType.RecoverySyncKey]).Data;
						}
						base.FilterType = (AirSyncV25FilterTypes)base.SyncState.GetData<Int32Data, int>(CustomStateDatumType.FilterType, 0);
						base.SyncState[CustomStateDatumType.MaxItems] = new Int32Data(base.MaxItems);
					}
				}
			}
			if (base.SyncKey == 0U)
			{
				SyncState syncState = null;
				try
				{
					syncState = syncStateStorage.GetCustomSyncState(new FolderIdMappingSyncStateInfo(), new PropertyDefinition[0]);
					if ((FolderIdMapping)syncState[CustomStateDatumType.IdMapping] == null)
					{
						base.Status = SyncBase.ErrorCodeStatus.ServerError;
						base.ResponseSyncKey = 0U;
						AirSyncDiagnostics.TraceError(ExTraceGlobals.RequestsTracer, this, "Id Mapping not created on SK0-returning ServerError on RI collection");
						throw new AirSyncPermanentException(false)
						{
							ErrorStringForProtocolLogger = "NoIdMappingInRICacheSync"
						};
					}
				}
				finally
				{
					if (syncState != null)
					{
						syncState.Dispose();
					}
				}
				syncStateStorage.DeleteFolderSyncState(base.CollectionId);
				base.SyncState = syncStateStorage.CreateFolderSyncState(base.SyncProviderFactory, base.CollectionId);
				base.SyncState.RegisterColdDataKey("IdMapping");
				base.SyncState.RegisterColdDataKey("CustomCalendarSyncFilter");
				base.SyncState[CustomStateDatumType.IdMapping] = new ItemIdMapping(base.CollectionId);
				base.ClassType = "RecipientInfoCache";
				base.SyncState[CustomStateDatumType.AirSyncClassType] = new ConstStringData(StaticStringPool.Instance.Intern(base.ClassType));
			}
			else
			{
				object obj = base.SyncState[CustomStateDatumType.AirSyncClassType];
				if (obj == null || string.IsNullOrEmpty(((ConstStringData)obj).Data))
				{
					AirSyncDiagnostics.TraceDebug(ExTraceGlobals.RequestsTracer, this, "ClassType for RI folder was null in sync state");
					base.ClassType = "RecipientInfoCache";
				}
				else
				{
					if (!string.Equals("RecipientInfoCache", ((ConstStringData)obj).Data, StringComparison.OrdinalIgnoreCase))
					{
						base.Status = SyncBase.ErrorCodeStatus.ObjectNotFound;
						string text = string.Format("Invalid Class Type in RI sync state: {0}", ((ConstStringData)obj).Data);
						AirSyncDiagnostics.TraceError(ExTraceGlobals.RequestsTracer, this, text);
						throw new AirSyncPermanentException(StatusCode.Sync_InvalidSyncKey, new LocalizedString(text), false)
						{
							ErrorStringForProtocolLogger = "BadClassStateInRICacheSync"
						};
					}
					base.ClassType = ((ConstStringData)obj).Data;
				}
			}
			if (base.SyncState.CustomVersion != null && base.SyncState.CustomVersion.Value > 9)
			{
				throw new AirSyncPermanentException(HttpStatusCode.InternalServerError, StatusCode.SyncStateVersionInvalid, EASServerStrings.MismatchSyncStateError, true)
				{
					ErrorStringForProtocolLogger = "MismatchedSyncStateInRICacheSync"
				};
			}
			base.SyncState[CustomStateDatumType.AirSyncProtocolVersion] = new Int32Data(base.ProtocolVersion);
		}

		// Token: 0x04000A1D RID: 2589
		private const int AirSyncLocalCommitTimeMaxIndex = 0;

		// Token: 0x04000A1E RID: 2590
		private const int AirSyncSyncKeyIndex = 1;

		// Token: 0x04000A1F RID: 2591
		private const int AirSyncMaxItemsIndex = 2;

		// Token: 0x04000A20 RID: 2592
		private static readonly PropertyDefinition[] propertiesToSaveForNullSync = new PropertyDefinition[]
		{
			AirSyncStateSchema.MetadataLocalCommitTimeMax,
			AirSyncStateSchema.MetadataSyncKey,
			AirSyncStateSchema.MetadataMaxItems
		};

		// Token: 0x04000A21 RID: 2593
		private RecipientInfoCacheDataObject recipientInfoCacheDataObject;
	}
}
