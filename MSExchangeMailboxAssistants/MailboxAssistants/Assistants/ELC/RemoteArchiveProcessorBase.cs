using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Assistants;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.InfoWorker.Common;
using Microsoft.Exchange.EDiscovery.Export.EwsProxy;
using Microsoft.Exchange.InfoWorker.Common.ELC;
using Microsoft.Exchange.InfoWorker.EventLog;
using Microsoft.Exchange.MailboxAssistants.Assistants.ELC.Logging;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.ELC
{
	// Token: 0x0200008A RID: 138
	internal class RemoteArchiveProcessorBase : IArchiveProcessor
	{
		// Token: 0x1700013F RID: 319
		// (get) Token: 0x06000529 RID: 1321 RVA: 0x00027390 File Offset: 0x00025590
		public virtual int MaxMessageSizeInArchive
		{
			get
			{
				return 36700160;
			}
		}

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x0600052A RID: 1322 RVA: 0x00027397 File Offset: 0x00025597
		public bool IsCrossPremise
		{
			get
			{
				return this.isCrossPremise;
			}
		}

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x0600052B RID: 1323 RVA: 0x0002739F File Offset: 0x0002559F
		// (set) Token: 0x0600052C RID: 1324 RVA: 0x000273A7 File Offset: 0x000255A7
		public IElcEwsClient PrimaryEwsClient { get; set; }

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x0600052D RID: 1325 RVA: 0x000273B0 File Offset: 0x000255B0
		// (set) Token: 0x0600052E RID: 1326 RVA: 0x000273B8 File Offset: 0x000255B8
		public IElcEwsClient ArchiveEwsClient { get; set; }

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x0600052F RID: 1327 RVA: 0x000273C1 File Offset: 0x000255C1
		public int MoveToArchiveTotalCountLimit
		{
			get
			{
				return this.moveToArchiveTotalCountLimit;
			}
		}

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x06000530 RID: 1328 RVA: 0x000273C9 File Offset: 0x000255C9
		public int MoveToArchiveBatchCountLimit
		{
			get
			{
				return this.moveToArchiveBatchCountLimit;
			}
		}

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x06000531 RID: 1329 RVA: 0x000273D1 File Offset: 0x000255D1
		public int MoveToArchiveBatchSizeLimit
		{
			get
			{
				return this.moveToArchiveBatchSizeLimit;
			}
		}

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x06000532 RID: 1330 RVA: 0x000273D9 File Offset: 0x000255D9
		private Dictionary<PropertyDefinition, PathToExtendedFieldType> EwsStorePropertyMapping
		{
			get
			{
				if (this.ewsStorePropertyMapping == null)
				{
					this.InitializeRetentionProperties();
				}
				return this.ewsStorePropertyMapping;
			}
		}

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x06000533 RID: 1331 RVA: 0x000273EF File Offset: 0x000255EF
		private List<PathToExtendedFieldType> FolderExtendedRetentionProperties
		{
			get
			{
				if (this.folderExtendedRetentionProperties == null)
				{
					this.InitializeRetentionProperties();
				}
				return this.folderExtendedRetentionProperties;
			}
		}

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x06000534 RID: 1332 RVA: 0x00027408 File Offset: 0x00025608
		private List<BasePathToElementType> FolderAllProperties
		{
			get
			{
				if (this.folderAllProperties == null)
				{
					this.folderAllProperties = new List<BasePathToElementType>();
					PathToUnindexedFieldType pathToUnindexedFieldType = new PathToUnindexedFieldType();
					pathToUnindexedFieldType.FieldURI = UnindexedFieldURIType.folderDisplayName;
					PathToUnindexedFieldType pathToUnindexedFieldType2 = new PathToUnindexedFieldType();
					pathToUnindexedFieldType2.FieldURI = UnindexedFieldURIType.folderParentFolderId;
					this.folderAllProperties.Add(pathToUnindexedFieldType);
					this.folderAllProperties.Add(pathToUnindexedFieldType2);
					this.folderAllProperties.AddRange(this.FolderExtendedRetentionProperties.ToArray());
				}
				return this.folderAllProperties;
			}
		}

		// Token: 0x06000535 RID: 1333 RVA: 0x00027478 File Offset: 0x00025678
		public RemoteArchiveProcessorBase(MailboxSession mailboxSession, ADUser user, StatisticsLogEntry statisticsLogEntry, bool isCrossPremise, bool isTestMode)
		{
			this.statisticsLogEntry = statisticsLogEntry;
			this.isCrossPremise = isCrossPremise;
			this.LoadRegistryConfigurations();
			this.primaryMailboxSession = mailboxSession;
			this.currentBatch = new List<ElcEwsItem>(this.moveToArchiveBatchCountLimit);
			this.currentBatchSize = 0U;
			if (isTestMode)
			{
				return;
			}
			Uri uri = ElcEwsClientHelper.DiscoverEwsUrl(mailboxSession.MailboxOwner.MailboxInfo);
			if (uri != null)
			{
				this.PrimaryEwsClient = new ElcEwsClient(mailboxSession.MailboxOwner, uri, new ElcEwsCallingContext(user, false), RemoteArchiveProcessorBase.TotalRetryTimeWindowForPrimary, RemoteArchiveProcessorBase.RetryScheduleForPrimary);
			}
			Uri uri2 = isCrossPremise ? ElcEwsClientHelper.DiscoverCloudArchiveEwsUrl(user) : ElcEwsClientHelper.DiscoverEwsUrl(mailboxSession.MailboxOwner.GetArchiveMailbox());
			if (uri2 != null)
			{
				this.ArchiveEwsClient = new ElcEwsClient(mailboxSession.MailboxOwner, uri2, new ElcEwsCallingContext(user, isCrossPremise), RemoteArchiveProcessorBase.TotalRetryTimeWindowForArchive, RemoteArchiveProcessorBase.RetryScheduleForArchive);
				return;
			}
			throw new ElcEwsException(ElcEwsErrorType.ArchiveExchangeWebServiceNotAvailable, "Archive EWS url is unknown.");
		}

		// Token: 0x06000536 RID: 1334 RVA: 0x00027578 File Offset: 0x00025778
		public bool SaveConfigItemInArchive(byte[] xmlData)
		{
			try
			{
				DistinguishedFolderIdType folderId = new DistinguishedFolderIdType
				{
					Id = DistinguishedFolderIdNameType.archiveinbox
				};
				if (this.ArchiveEwsClient.GetMrmConfiguration(folderId) == null)
				{
					this.ArchiveEwsClient.CreateMrmConfiguration(folderId, xmlData);
				}
				else
				{
					this.ArchiveEwsClient.UpdateMrmConfiguration(folderId, xmlData);
				}
			}
			catch (ElcEwsException arg)
			{
				RemoteArchiveProcessorBase.Tracer.TraceError<MailboxSession, ElcEwsException>((long)this.GetHashCode(), "The MRM FAI message could not be saved in archive for {0}, Exception: {1}", this.primaryMailboxSession, arg);
				throw;
			}
			return true;
		}

		// Token: 0x06000537 RID: 1335 RVA: 0x000275F4 File Offset: 0x000257F4
		public void DeleteConfigItemInArchive()
		{
			try
			{
				this.ArchiveEwsClient.DeleteMrmConfiguration(new DistinguishedFolderIdType
				{
					Id = DistinguishedFolderIdNameType.archiveinbox
				});
			}
			catch (ElcEwsException arg)
			{
				RemoteArchiveProcessorBase.Tracer.TraceError<MailboxSession, ElcEwsException>((long)this.GetHashCode(), "The MRM FAI message could not be deleted in archive for {0}, Exception: {1}", this.primaryMailboxSession, arg);
				throw;
			}
		}

		// Token: 0x06000538 RID: 1336 RVA: 0x00027650 File Offset: 0x00025850
		public void MoveToArchive(TagExpirationExecutor.ItemSet itemSet, ElcSubAssistant assistant, FolderArchiver folderArchiver, int totalFailuresSoFar, ref List<Exception> allExceptionsSoFar, out List<string> foldersWithErrors, out int newMoveErrorsTotal)
		{
			if (this.PrimaryEwsClient == null)
			{
				throw new ElcEwsException(ElcEwsErrorType.PrimaryExchangeWebServiceNotAvailable, "Primary EWS url is unknown.");
			}
			foldersWithErrors = new List<string>();
			newMoveErrorsTotal = 0;
			if (this.IsCrossPremise && this.moveToArchiveTotalCount > this.MoveToArchiveTotalCountLimit)
			{
				RemoteArchiveProcessorBase.Tracer.TraceDebug<RemoteArchiveProcessorBase>((long)this.GetHashCode(), "{0}: Move to archive total count limit reached.  No more item is moved to the archive mailbox during this run.", this);
				return;
			}
			using (Folder folder = Folder.Bind(this.primaryMailboxSession, itemSet.FolderId))
			{
				FolderTupleRemoteArchive folderTupleRemoteArchive = folderArchiver.GetArchiveFolderTuple(itemSet.FolderId) as FolderTupleRemoteArchive;
				if (folderTupleRemoteArchive != null)
				{
					RemoteArchiveProcessorBase.Tracer.TraceDebug<RemoteArchiveProcessorBase, string, string>((long)this.GetHashCode(), "{0}: Was able to open target folder {1} in the archive, corresponding to source folder {2}. Will proceed to move in batches.", this, folderTupleRemoteArchive.DisplayName, folder.DisplayName);
					this.MoveItemsInBatches(itemSet.Items, folder, folderTupleRemoteArchive.Folder, ExpirationExecutor.Action.MoveToArchive, totalFailuresSoFar, ref allExceptionsSoFar, out foldersWithErrors, out newMoveErrorsTotal);
				}
				else
				{
					RemoteArchiveProcessorBase.Tracer.TraceWarning<RemoteArchiveProcessorBase, string>((long)this.GetHashCode(), "{0}: Unable to get target folder in the archive corresponding to source folder {1}. Will not move anything to it.", this, folder.DisplayName);
				}
			}
		}

		// Token: 0x06000539 RID: 1337 RVA: 0x0002774C File Offset: 0x0002594C
		public void MoveToArchiveDumpster(DefaultFolderType folderType, List<ItemData> itemsToMove, ElcSubAssistant assistant, FolderArchiver folderArchiver, int totalFailuresSoFar, ref List<Exception> allExceptionsSoFar, out List<string> foldersWithErrors, out int newMoveErrorsTotal)
		{
			if (this.PrimaryEwsClient == null)
			{
				throw new ElcEwsException(ElcEwsErrorType.PrimaryExchangeWebServiceNotAvailable, "Primary EWS url is unknown.");
			}
			foldersWithErrors = new List<string>();
			newMoveErrorsTotal = 0;
			if (this.IsCrossPremise && this.moveToArchiveTotalCount > this.MoveToArchiveTotalCountLimit)
			{
				RemoteArchiveProcessorBase.Tracer.TraceDebug<RemoteArchiveProcessorBase>((long)this.GetHashCode(), "{0}: Move to archive total count limit reached.  No more item is moved to the archive mailbox during this run.", this);
				return;
			}
			using (Folder folder = Folder.Bind(this.primaryMailboxSession, folderType))
			{
				DistinguishedFolderIdNameType id;
				if (RemoteArchiveProcessorBase.ArchiveEwsDumpsterFolderMapping.TryGetValue(folderType, out id))
				{
					BaseFolderType baseFolderType = null;
					Exception ex = null;
					try
					{
						baseFolderType = this.ArchiveEwsClient.GetFolderById(new DistinguishedFolderIdType
						{
							Id = id
						}, this.FolderAllProperties.ToArray());
					}
					catch (ElcEwsException ex2)
					{
						ex = ex2;
					}
					if (baseFolderType != null && ex == null)
					{
						RemoteArchiveProcessorBase.Tracer.TraceDebug<RemoteArchiveProcessorBase, string, string>((long)this.GetHashCode(), "{0}: Was able to open target folder {1} in the archive, corresponding to source folder {2}. Will proceed to move in batches.", this, baseFolderType.DisplayName, folder.DisplayName);
						this.MoveItemsInBatches(itemsToMove, folder, baseFolderType, ExpirationExecutor.Action.MoveToArchiveDumpster, totalFailuresSoFar, ref allExceptionsSoFar, out foldersWithErrors, out newMoveErrorsTotal);
					}
					else
					{
						RemoteArchiveProcessorBase.Tracer.TraceWarning<RemoteArchiveProcessorBase, string, string>((long)this.GetHashCode(), "{0}: Unable to get target folder in the archive corresponding to source folder {1}. Will not move anything to it. Exception : {2}", this, folder.DisplayName, (ex == null) ? string.Empty : ex.ToString());
					}
				}
				else
				{
					RemoteArchiveProcessorBase.Tracer.TraceDebug<RemoteArchiveProcessorBase, DefaultFolderType>((long)this.GetHashCode(), "{0}: Unable to find the corresponding archive dumpster folder for foler type {1}. Will not move anything to it.", this, folderType);
				}
			}
		}

		// Token: 0x0600053A RID: 1338 RVA: 0x000278A8 File Offset: 0x00025AA8
		public Dictionary<StoreObjectId, FolderTuple> GetFolderHierarchyInArchive()
		{
			Dictionary<StoreObjectId, FolderTuple> dictionary = new Dictionary<StoreObjectId, FolderTuple>();
			try
			{
				BaseFolderType folderById = this.ArchiveEwsClient.GetFolderById(new DistinguishedFolderIdType
				{
					Id = DistinguishedFolderIdNameType.archivemsgfolderroot
				}, this.FolderAllProperties.ToArray());
				FolderTupleRemoteArchive folderTupleRemoteArchive = new FolderTupleRemoteArchive(folderById, folderById.FolderId, folderById.ParentFolderId, folderById.DisplayName, RemoteArchiveProcessorBase.ConvertToFolderProperties(folderById.ExtendedProperty), true);
				dictionary.Add(folderTupleRemoteArchive.FolderId, folderTupleRemoteArchive);
				IEnumerable<BaseFolderType> folderHierarchy = this.ArchiveEwsClient.GetFolderHierarchy(new DistinguishedFolderIdType
				{
					Id = DistinguishedFolderIdNameType.archivemsgfolderroot
				}, true, this.folderAllProperties.ToArray());
				foreach (BaseFolderType baseFolderType in folderHierarchy)
				{
					FolderTupleRemoteArchive folderTupleRemoteArchive2 = new FolderTupleRemoteArchive(baseFolderType, baseFolderType.FolderId, baseFolderType.ParentFolderId, baseFolderType.DisplayName, RemoteArchiveProcessorBase.ConvertToFolderProperties(baseFolderType.ExtendedProperty));
					dictionary.Add(folderTupleRemoteArchive2.FolderId, folderTupleRemoteArchive2);
				}
			}
			catch (ElcEwsException arg)
			{
				RemoteArchiveProcessorBase.Tracer.TraceError<MailboxSession, ElcEwsException>((long)this.GetHashCode(), "Get folder hierarchy from archive for {0} failed, Exception: {1}", this.primaryMailboxSession, arg);
				throw;
			}
			return dictionary;
		}

		// Token: 0x0600053B RID: 1339 RVA: 0x000279E4 File Offset: 0x00025BE4
		public void UpdatePropertiesOnFolderInArchive(FolderTuple sourceInPrimary, FolderTuple targetInArchive)
		{
			FolderTupleRemoteArchive folderTupleRemoteArchive = targetInArchive as FolderTupleRemoteArchive;
			if (folderTupleRemoteArchive == null)
			{
				throw new ArgumentException("Archive folder tuple must be a FolderTupleRemoteArchive.", "targetInArchive");
			}
			List<FolderChangeDescriptionType> list = new List<FolderChangeDescriptionType>();
			for (FolderHelper.DataColumnIndex dataColumnIndex = FolderHelper.DataColumnIndex.startOfTagPropsIndex; dataColumnIndex <= FolderHelper.DataColumnIndex.containerClassIndex; dataColumnIndex++)
			{
				PropertyDefinition key = FolderHelper.DataColumns[(int)dataColumnIndex];
				PathToExtendedFieldType pathToExtendedFieldType = this.EwsStorePropertyMapping[key];
				object obj = sourceInPrimary.FolderProps[key];
				object obj2 = folderTupleRemoteArchive.FolderProps[key];
				if (obj != null && !(obj is PropertyError))
				{
					string text = ElcEwsClientHelper.ConvertRetentionPropertyForService(obj, pathToExtendedFieldType.PropertyType);
					string b = ElcEwsClientHelper.ConvertRetentionPropertyForService(obj2, pathToExtendedFieldType.PropertyType);
					if (text != b)
					{
						SetFolderFieldType setFolderFieldType = new SetFolderFieldType();
						FolderType folderType = new FolderType();
						ExtendedPropertyType extendedPropertyType = new ExtendedPropertyType
						{
							ExtendedFieldURI = pathToExtendedFieldType,
							Item = text
						};
						folderType.ExtendedProperty = new ExtendedPropertyType[]
						{
							extendedPropertyType
						};
						setFolderFieldType.Item = pathToExtendedFieldType;
						setFolderFieldType.Item1 = folderType;
						list.Add(setFolderFieldType);
					}
				}
				else if (obj2 != null)
				{
					list.Add(new DeleteFolderFieldType
					{
						Item = pathToExtendedFieldType
					});
				}
			}
			if (list.Count > 0)
			{
				RemoteArchiveProcessorBase.Tracer.TraceDebug<RemoteArchiveProcessorBase, int, string>((long)this.GetHashCode(), "{0}: {1} properties to be updated for folder {2}.", this, list.Count, folderTupleRemoteArchive.DisplayName);
				FolderChangeType folderChangeType = new FolderChangeType();
				folderChangeType.Item = folderTupleRemoteArchive.EwsFolderId;
				folderChangeType.Updates = list.ToArray();
				try
				{
					this.ArchiveEwsClient.UpdateFolder(folderChangeType);
				}
				catch (ElcEwsException arg)
				{
					RemoteArchiveProcessorBase.Tracer.TraceError<MailboxSession, string, ElcEwsException>((long)this.GetHashCode(), "Update folder {1} in archive for {0} failed, Exception: {2}", this.primaryMailboxSession, folderTupleRemoteArchive.DisplayName, arg);
					throw;
				}
			}
		}

		// Token: 0x0600053C RID: 1340 RVA: 0x00027BA4 File Offset: 0x00025DA4
		public FolderTuple CreateAndUpdateFolderInArchive(FolderTuple parentInArchive, FolderTuple sourceInPrimary)
		{
			FolderTupleRemoteArchive folderTupleRemoteArchive = (FolderTupleRemoteArchive)parentInArchive;
			FolderType folderType = new FolderType();
			BaseFolderType baseFolderType = null;
			folderType.DisplayName = sourceInPrimary.DisplayName;
			List<ExtendedPropertyType> list = new List<ExtendedPropertyType>();
			for (FolderHelper.DataColumnIndex dataColumnIndex = FolderHelper.DataColumnIndex.startOfTagPropsIndex; dataColumnIndex <= FolderHelper.DataColumnIndex.containerClassIndex; dataColumnIndex++)
			{
				PropertyDefinition key = FolderHelper.DataColumns[(int)dataColumnIndex];
				PathToExtendedFieldType pathToExtendedFieldType = this.EwsStorePropertyMapping[key];
				object obj = sourceInPrimary.FolderProps[key];
				if (obj != null && !(obj is PropertyError))
				{
					obj = ElcEwsClientHelper.ConvertRetentionPropertyForService(obj, pathToExtendedFieldType.PropertyType);
					ExtendedPropertyType item = new ExtendedPropertyType
					{
						ExtendedFieldURI = pathToExtendedFieldType,
						Item = obj
					};
					list.Add(item);
				}
			}
			if (list.Count > 0)
			{
				folderType.ExtendedProperty = list.ToArray();
			}
			try
			{
				baseFolderType = this.ArchiveEwsClient.CreateFolder(folderTupleRemoteArchive.EwsFolderId, folderType);
			}
			catch (ElcEwsException arg)
			{
				RemoteArchiveProcessorBase.Tracer.TraceError<string, MailboxSession, ElcEwsException>((long)this.GetHashCode(), "Create folder {0} in archive for {1} failed, Exception: {2}", sourceInPrimary.DisplayName, this.primaryMailboxSession, arg);
				throw;
			}
			if (baseFolderType != null)
			{
				try
				{
					baseFolderType = this.ArchiveEwsClient.GetFolderById(baseFolderType.FolderId, this.FolderAllProperties.ToArray());
					goto IL_16D;
				}
				catch (ElcEwsException arg2)
				{
					RemoteArchiveProcessorBase.Tracer.TraceError<string, MailboxSession, ElcEwsException>((long)this.GetHashCode(), "Get newly created folder {0} in archive for {1} failed, Exception: {2}", sourceInPrimary.DisplayName, this.primaryMailboxSession, arg2);
					throw;
				}
				goto IL_13E;
				IL_16D:
				if (baseFolderType != null)
				{
					return new FolderTupleRemoteArchive(baseFolderType, baseFolderType.FolderId, folderTupleRemoteArchive.EwsFolderId, baseFolderType.DisplayName, RemoteArchiveProcessorBase.ConvertToFolderProperties(baseFolderType.ExtendedProperty));
				}
				RemoteArchiveProcessorBase.Tracer.TraceError<string, MailboxSession>((long)this.GetHashCode(), "Get newly created folder {0} in archive for {1} returned no folder", sourceInPrimary.DisplayName, this.primaryMailboxSession);
				throw new ElcEwsException(ElcEwsErrorType.FailedToGetFolderById, "Get newly created folder {0} returned no folder");
			}
			IL_13E:
			RemoteArchiveProcessorBase.Tracer.TraceError<string, MailboxSession>((long)this.GetHashCode(), "Create folder {0} in archive for {1} returned no folder", sourceInPrimary.DisplayName, this.primaryMailboxSession);
			throw new ElcEwsException(ElcEwsErrorType.FailedToCreateFolder, "Creating folder {0} returned no folder");
		}

		// Token: 0x0600053D RID: 1341 RVA: 0x00027D94 File Offset: 0x00025F94
		internal static object[] ConvertToFolderProperties(IEnumerable<ExtendedPropertyType> propertyList)
		{
			object[] array = new object[FolderHelper.DataColumns.Length];
			if (propertyList != null)
			{
				foreach (ExtendedPropertyType extendedPropertyType in propertyList)
				{
					if (string.Compare("RetentionTagEntryId", extendedPropertyType.ExtendedFieldURI.PropertyName, true) == 0)
					{
						array[8] = ElcEwsClientHelper.ConvertRetentionPropertyFromService(extendedPropertyType.Item, extendedPropertyType.ExtendedFieldURI.PropertyType);
					}
					else
					{
						int num = (int)RemoteArchiveProcessorBase.RetentionPropertyTagsMapping[extendedPropertyType.ExtendedFieldURI.PropertyTag];
						array[num] = ElcEwsClientHelper.ConvertRetentionPropertyFromService(extendedPropertyType.Item, extendedPropertyType.ExtendedFieldURI.PropertyType);
					}
				}
			}
			return array;
		}

		// Token: 0x0600053E RID: 1342 RVA: 0x00027E4C File Offset: 0x0002604C
		private void LoadRegistryConfigurations()
		{
			object obj = Globals.ReadRegKey(ElcGlobals.ParameterRegistryKeyPath, ElcGlobals.MoveToArchiveTotalCountLimit);
			if (obj is int)
			{
				this.moveToArchiveTotalCountLimit = (int)obj;
				if (this.moveToArchiveTotalCountLimit > 10000)
				{
					this.moveToArchiveTotalCountLimit = 10000;
				}
			}
			obj = Globals.ReadRegKey(ElcGlobals.ParameterRegistryKeyPath, ElcGlobals.MoveToArchiveBatchCountLimit);
			if (obj is int)
			{
				this.moveToArchiveBatchCountLimit = (int)obj;
				if (this.moveToArchiveBatchCountLimit > 250)
				{
					this.moveToArchiveBatchCountLimit = 250;
				}
			}
			obj = Globals.ReadRegKey(ElcGlobals.ParameterRegistryKeyPath, ElcGlobals.MoveToArchiveBatchSizeLimit);
			if (obj is int)
			{
				this.moveToArchiveBatchSizeLimit = (int)obj;
				if (this.moveToArchiveBatchSizeLimit > 26214400)
				{
					this.moveToArchiveBatchSizeLimit = 26214400;
				}
			}
		}

		// Token: 0x0600053F RID: 1343 RVA: 0x00027F10 File Offset: 0x00026110
		private void InitializeRetentionProperties()
		{
			this.folderExtendedRetentionProperties = new List<PathToExtendedFieldType>();
			this.ewsStorePropertyMapping = new Dictionary<PropertyDefinition, PathToExtendedFieldType>();
			foreach (KeyValuePair<string, MapiPropertyTypeType> keyValuePair in RemoteArchiveProcessorBase.ExtendedRetentionPropertyTags)
			{
				string key = keyValuePair.Key;
				MapiPropertyTypeType value = keyValuePair.Value;
				PathToExtendedFieldType pathToExtendedFieldType = new PathToExtendedFieldType();
				pathToExtendedFieldType.PropertyType = value;
				if (key == "-1")
				{
					pathToExtendedFieldType.PropertySetId = RemoteArchiveProcessorBase.RetentionTagEntryId.ToString();
					pathToExtendedFieldType.PropertyName = "RetentionTagEntryId";
				}
				else
				{
					pathToExtendedFieldType.PropertyTag = key;
				}
				this.folderExtendedRetentionProperties.Add(pathToExtendedFieldType);
				FolderHelper.DataColumnIndex dataColumnIndex = RemoteArchiveProcessorBase.RetentionPropertyTagsMapping[key];
				PropertyDefinition key2 = FolderHelper.DataColumns[(int)dataColumnIndex];
				this.ewsStorePropertyMapping.Add(key2, pathToExtendedFieldType);
			}
		}

		// Token: 0x06000540 RID: 1344 RVA: 0x00028004 File Offset: 0x00026204
		private void MoveItemsInBatches(List<ItemData> listToSend, Folder sourceFolder, BaseFolderType targetFolder, ExpirationExecutor.Action retentionActionType, int totalFailuresSoFar, ref List<Exception> allExceptionsSoFar, out List<string> foldersWithErrors, out int newMoveErrorsTotal)
		{
			foldersWithErrors = new List<string>();
			newMoveErrorsTotal = 0;
			Exception exception = null;
			int num = 0;
			long num2 = 0L;
			foreach (ItemData itemData in listToSend)
			{
				if (itemData.MessageSize > this.MaxMessageSizeInArchive)
				{
					if (foldersWithErrors.Count == 0)
					{
						foldersWithErrors.Add(sourceFolder.DisplayName);
						RemoteArchiveProcessorBase.Tracer.TraceDebug<RemoteArchiveProcessorBase, string>((long)this.GetHashCode(), "{0}: Added folder {1} to the list of bad folders to be event logged because an item too large was skipped.", this, sourceFolder.DisplayName);
					}
					if (this.statisticsLogEntry != null)
					{
						this.statisticsLogEntry.NumberOfItemsSkippedDueToSizeRestrictionInArchiveProcessor += 1L;
					}
				}
				else
				{
					bool flag = this.ProcessItem(itemData);
					num++;
					this.moveToArchiveTotalCount++;
					num2 += (long)itemData.MessageSize;
					if (this.IsCrossPremise && this.moveToArchiveTotalCount >= this.MoveToArchiveTotalCountLimit)
					{
						if (this.statisticsLogEntry != null)
						{
							this.statisticsLogEntry.MoveToArchiveLimitReached = true;
							break;
						}
						break;
					}
					else if (flag && !this.ProcessBatch(targetFolder.FolderId, out exception))
					{
						newMoveErrorsTotal++;
						this.HandleFailure(retentionActionType, sourceFolder, targetFolder, exception, newMoveErrorsTotal + totalFailuresSoFar, ref allExceptionsSoFar);
					}
				}
			}
			if (!this.ProcessBatch(targetFolder.FolderId, out exception))
			{
				newMoveErrorsTotal++;
				this.HandleFailure(retentionActionType, sourceFolder, targetFolder, exception, newMoveErrorsTotal + totalFailuresSoFar, ref allExceptionsSoFar);
			}
			ELCPerfmon.TotalItemsExpired.IncrementBy((long)num);
			ELCPerfmon.TotalSizeItemsExpired.IncrementBy(num2);
			ELCPerfmon.TotalItemsMoved.IncrementBy((long)num);
			ELCPerfmon.TotalSizeItemsMoved.IncrementBy(num2);
		}

		// Token: 0x06000541 RID: 1345 RVA: 0x000281C0 File Offset: 0x000263C0
		private bool ProcessItem(ItemData itemData)
		{
			string id = StoreId.StoreIdToEwsId(this.primaryMailboxSession.MailboxGuid, itemData.Id);
			ElcEwsItem item = new ElcEwsItem
			{
				Id = id,
				Data = null,
				Error = null,
				StorageItemData = itemData
			};
			this.currentBatch.Add(item);
			this.currentBatchSize += Convert.ToUInt32(itemData.MessageSize);
			return (ulong)this.currentBatchSize >= (ulong)((long)this.moveToArchiveBatchSizeLimit) || this.currentBatch.Count >= this.moveToArchiveBatchCountLimit;
		}

		// Token: 0x06000542 RID: 1346 RVA: 0x00028264 File Offset: 0x00026464
		private bool ProcessBatch(FolderIdType targetFolderId, out Exception batchException)
		{
			RemoteArchiveProcessorBase.Tracer.TraceDebug<MailboxSession, int, uint>((long)this.GetHashCode(), "RemoteArchiveProcessorBase.ProcessBatch: Process items batch for {0}.  Count = {1}, Size = {2}.", this.primaryMailboxSession, this.currentBatch.Count, this.currentBatchSize);
			batchException = null;
			if (this.currentBatch.Count == 0 || this.currentBatchSize == 0U)
			{
				return true;
			}
			bool flag = true;
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			int num4 = 0;
			int num5 = 0;
			OperationResult operationResult = OperationResult.Succeeded;
			List<Exception> list = new List<Exception>();
			try
			{
				Exception ex;
				List<ElcEwsItem> list2 = this.ExportItemsFromPrimaryMailbox(this.currentBatch, out ex);
				num = list2.Count;
				if (ex != null)
				{
					list.Add(ex);
				}
				List<ElcEwsItem> list3 = this.UploadItemsToArchiveMailbox(list2, targetFolderId, out ex);
				num2 = list3.Count;
				if (ex != null)
				{
					list.Add(ex);
				}
				List<ElcEwsItem> list4 = null;
				list4 = this.VerifyItemsUploadedToArchiveMailbox(list3, out ex);
				num3 = list4.Count;
				if (ex != null)
				{
					list.Add(ex);
				}
				ex = null;
				VersionedId[] array = (from elcEwsItem in list4
				select elcEwsItem.StorageItemData.Id).ToArray<VersionedId>();
				if (array.Length > 0)
				{
					try
					{
						this.primaryMailboxSession.COWSettings.TemporaryDisableHold = true;
						AggregateOperationResult aggregateOperationResult = this.primaryMailboxSession.Delete(DeleteItemFlags.HardDelete | DeleteItemFlags.SuppressReadReceipt, array);
						operationResult = aggregateOperationResult.OperationResult;
						ex = ElcExceptionHelper.ExtractExceptionsFromAggregateOperationResult(aggregateOperationResult);
					}
					finally
					{
						this.primaryMailboxSession.COWSettings.TemporaryDisableHold = false;
					}
					flag = (operationResult != OperationResult.Failed && operationResult != OperationResult.PartiallySucceeded);
					if (!flag)
					{
						if (ex != null)
						{
							list.Add(ex);
						}
					}
					else
					{
						foreach (ElcEwsItem elcEwsItem2 in list4)
						{
							if (elcEwsItem2.StorageItemData != null && elcEwsItem2.StorageItemData.Enforcer == ItemData.EnforcerType.DumpsterExpirationEnforcer)
							{
								num4++;
							}
							else if (elcEwsItem2.StorageItemData != null && elcEwsItem2.StorageItemData.Enforcer == ItemData.EnforcerType.ExpirationTagEnforcer)
							{
								num5++;
							}
						}
					}
				}
				flag = (flag && num3 == this.currentBatch.Count);
			}
			finally
			{
				batchException = new AggregateException(list);
				RemoteArchiveProcessorBase.Tracer.TraceDebug((long)this.GetHashCode(), "{0}: Batch processing summary.  Batch count = {1}, Batch size = {2}, Successfully exported = {3}, Successfully uploaded = {4}, Successfully verified = {5}, Delete status = {6}, Failure = {7}", new object[]
				{
					this,
					this.currentBatch.Count,
					this.currentBatchSize,
					num,
					num2,
					num3,
					operationResult,
					batchException
				});
				this.currentBatch.Clear();
				this.currentBatchSize = 0U;
				if (this.statisticsLogEntry != null)
				{
					this.statisticsLogEntry.NumberOfItemsActuallyArchivedByDumpsterExpirationEnforcer += (long)num4;
					this.statisticsLogEntry.NumberOfItemsActuallyArchivedByTag += (long)num5;
				}
			}
			return flag;
		}

		// Token: 0x06000543 RID: 1347 RVA: 0x00028610 File Offset: 0x00026810
		private List<ElcEwsItem> ExportItemsFromPrimaryMailbox(List<ElcEwsItem> itemsToExport, out Exception exception)
		{
			return this.OperateOnItems(itemsToExport, () => this.PrimaryEwsClient.ExportItems(itemsToExport), "RemoteArchiveProcessorBase.ExportItemsFromPrimaryMailbox: Export items from primary mailbox of {0} failed, Exception: {1}", delegate(ElcEwsItem exportedItem)
			{
				if (exportedItem.Error == null && exportedItem.Data != null)
				{
					return true;
				}
				if (exportedItem.Error != null)
				{
					RemoteArchiveProcessorBase.Tracer.TraceError<MailboxSession, string, ElcEwsException>((long)this.GetHashCode(), "RemoteArchiveProcessorBase.ExportItemsFromPrimaryMailbox: Export item {1} for {0} failed.  Error = {2}", this.primaryMailboxSession, exportedItem.Id, exportedItem.Error);
				}
				else
				{
					RemoteArchiveProcessorBase.Tracer.TraceError<MailboxSession, string>((long)this.GetHashCode(), "RemoteArchiveProcessorBase.ExportItemsFromPrimaryMailbox: Export items {1} for {0} had no data.", this.primaryMailboxSession, exportedItem.Id);
				}
				return false;
			}, out exception);
		}

		// Token: 0x06000544 RID: 1348 RVA: 0x000286B8 File Offset: 0x000268B8
		private List<ElcEwsItem> UploadItemsToArchiveMailbox(List<ElcEwsItem> itemsToUpload, FolderIdType targetFolderId, out Exception exception)
		{
			return this.OperateOnItems(itemsToUpload, () => this.ArchiveEwsClient.UploadItems(targetFolderId, itemsToUpload, true), "RemoteArchiveProcessorBase.UploadItemsToArchiveMailbox: Upload items to archive mailbox of {0} failed, Exception: {1}", delegate(ElcEwsItem uploadedItem)
			{
				if (uploadedItem.Error == null)
				{
					return true;
				}
				RemoteArchiveProcessorBase.Tracer.TraceError<MailboxSession, string, ElcEwsException>((long)this.GetHashCode(), "RemoteArchiveProcessorBase.UploadItemsToArchiveMailbox: Upload items {1} for {0} failed.  Error = {2}", this.primaryMailboxSession, uploadedItem.Id, uploadedItem.Error);
				return false;
			}, out exception);
		}

		// Token: 0x06000545 RID: 1349 RVA: 0x00028760 File Offset: 0x00026960
		private List<ElcEwsItem> VerifyItemsUploadedToArchiveMailbox(List<ElcEwsItem> itemsToVerify, out Exception exception)
		{
			return this.OperateOnItems(itemsToVerify, () => this.ArchiveEwsClient.GetItems(itemsToVerify), "RemoteArchiveProcessorBase.VerifyItemsUploadedToArchiveMailbox: Verify items uploaded to achive mailbox of {0} failed, Exception: {1}", delegate(ElcEwsItem verifiedItem)
			{
				if (verifiedItem.Error == null)
				{
					return true;
				}
				RemoteArchiveProcessorBase.Tracer.TraceError<MailboxSession, string, ElcEwsException>((long)this.GetHashCode(), "RemoteArchiveProcessorBase.VerifyItemsUploadedToArchiveMailbox: Verify items {1} for {0} failed.  Error = {2}", this.primaryMailboxSession, verifiedItem.Id, verifiedItem.Error);
				return false;
			}, out exception);
		}

		// Token: 0x06000546 RID: 1350 RVA: 0x000287AC File Offset: 0x000269AC
		private List<ElcEwsItem> OperateOnItems(List<ElcEwsItem> itemsToOperate, Func<List<ElcEwsItem>> itemsOperations, string operationFailureMessage, Func<ElcEwsItem, bool> resultHandler, out Exception exception)
		{
			List<ElcEwsItem> list = new List<ElcEwsItem>();
			exception = null;
			if (itemsToOperate.Count > 0)
			{
				List<ElcEwsItem> list2 = null;
				try
				{
					list2 = itemsOperations();
				}
				catch (ElcEwsException ex)
				{
					RemoteArchiveProcessorBase.Tracer.TraceError<MailboxSession, ElcEwsException>((long)this.GetHashCode(), operationFailureMessage, this.primaryMailboxSession, ex);
					list2 = null;
					exception = ex;
				}
				if (list2 != null && itemsToOperate.Count > 0)
				{
					foreach (ElcEwsItem elcEwsItem in list2)
					{
						if (resultHandler(elcEwsItem))
						{
							list.Add(elcEwsItem);
						}
					}
				}
			}
			return list;
		}

		// Token: 0x06000547 RID: 1351 RVA: 0x00028860 File Offset: 0x00026A60
		private void HandleFailure(ExpirationExecutor.Action retentionActionType, Folder sourceFolder, BaseFolderType targetFolder, Exception exception, int failureCount, ref List<Exception> allExceptionsSoFar)
		{
			Globals.Logger.LogEvent(InfoWorkerEventLogConstants.Tuple_ExpirationOfCurrentBatchFailed, null, new object[]
			{
				this.primaryMailboxSession.MailboxOwner,
				retentionActionType.ToString(),
				(sourceFolder == null) ? string.Empty : sourceFolder.DisplayName,
				(targetFolder == null) ? string.Empty : targetFolder.DisplayName,
				(sourceFolder == null) ? string.Empty : sourceFolder.Id.ObjectId.ToHexEntryId(),
				(targetFolder == null) ? string.Empty : targetFolder.FolderId.Id,
				(exception == null) ? string.Empty : exception.ToString()
			});
			if (this.statisticsLogEntry != null)
			{
				this.statisticsLogEntry.NumberOfBatchesFailedToMoveInArchiveProcessor += 1L;
			}
			if (exception != null)
			{
				allExceptionsSoFar.Add(exception);
			}
			if (failureCount > MailboxData.MaxErrorsAllowed)
			{
				throw new TransientMailboxException(Strings.descELCEnforcerTooManyErrors(this.primaryMailboxSession.MailboxOwner.MailboxInfo.PrimarySmtpAddress.ToString(), MailboxData.MaxErrorsAllowed), new AggregateException(allExceptionsSoFar), null);
			}
		}

		// Token: 0x040003DD RID: 989
		internal const string RetentionTagEntryIdName = "RetentionTagEntryId";

		// Token: 0x040003DE RID: 990
		internal const int MaxMessageMoveSize = 36700160;

		// Token: 0x040003DF RID: 991
		private const int DefaultMoveToArchiveTotalCountLimit = 2000;

		// Token: 0x040003E0 RID: 992
		private const int MaxMoveToArchiveTotalCountLimit = 10000;

		// Token: 0x040003E1 RID: 993
		private const int DefaultMoveToArchiveBatchCountLimit = 100;

		// Token: 0x040003E2 RID: 994
		private const int MaxMoveToArchiveBatchCountLimit = 250;

		// Token: 0x040003E3 RID: 995
		private const int DefaultMoveToArchiveBatchSizeLimit = 5242880;

		// Token: 0x040003E4 RID: 996
		private const int MaxMoveToArchiveBatchSizeLimit = 26214400;

		// Token: 0x040003E5 RID: 997
		private const DeleteItemFlags DeleteItemFlags = DeleteItemFlags.HardDelete | DeleteItemFlags.SuppressReadReceipt;

		// Token: 0x040003E6 RID: 998
		public static readonly Dictionary<string, MapiPropertyTypeType> ExtendedRetentionPropertyTags = new Dictionary<string, MapiPropertyTypeType>
		{
			{
				"0x3019",
				MapiPropertyTypeType.Binary
			},
			{
				"0x301a",
				MapiPropertyTypeType.Integer
			},
			{
				"0x301d",
				MapiPropertyTypeType.Integer
			},
			{
				"0x3018",
				MapiPropertyTypeType.Binary
			},
			{
				"0x301e",
				MapiPropertyTypeType.Integer
			},
			{
				"-1",
				MapiPropertyTypeType.Binary
			},
			{
				"0x3613",
				MapiPropertyTypeType.String
			}
		};

		// Token: 0x040003E7 RID: 999
		public static readonly TimeSpan TotalRetryTimeWindowForPrimary = TimeSpan.FromMinutes(15.0);

		// Token: 0x040003E8 RID: 1000
		public static readonly TimeSpan TotalRetryTimeWindowForArchive = TimeSpan.FromMinutes(15.0);

		// Token: 0x040003E9 RID: 1001
		public static readonly TimeSpan[] RetryScheduleForPrimary = new TimeSpan[]
		{
			TimeSpan.FromSeconds(30.0)
		};

		// Token: 0x040003EA RID: 1002
		public static readonly TimeSpan[] RetryScheduleForArchive = new TimeSpan[]
		{
			TimeSpan.FromSeconds(30.0),
			TimeSpan.FromMinutes(2.0),
			TimeSpan.FromMinutes(6.0)
		};

		// Token: 0x040003EB RID: 1003
		protected static readonly Trace Tracer = ExTraceGlobals.ELCTracer;

		// Token: 0x040003EC RID: 1004
		private static readonly Guid RetentionTagEntryId = new Guid("C7A4569B-F7AE-4DC2-9279-A8FE2F3CAF89");

		// Token: 0x040003ED RID: 1005
		private static readonly Dictionary<DefaultFolderType, DistinguishedFolderIdNameType> ArchiveEwsDumpsterFolderMapping = new Dictionary<DefaultFolderType, DistinguishedFolderIdNameType>
		{
			{
				DefaultFolderType.RecoverableItemsDeletions,
				DistinguishedFolderIdNameType.archiverecoverableitemsdeletions
			},
			{
				DefaultFolderType.RecoverableItemsPurges,
				DistinguishedFolderIdNameType.archiverecoverableitemspurges
			},
			{
				DefaultFolderType.RecoverableItemsVersions,
				DistinguishedFolderIdNameType.archiverecoverableitemsversions
			}
		};

		// Token: 0x040003EE RID: 1006
		private static readonly Dictionary<string, FolderHelper.DataColumnIndex> RetentionPropertyTagsMapping = new Dictionary<string, FolderHelper.DataColumnIndex>
		{
			{
				"0x3019",
				FolderHelper.DataColumnIndex.startOfTagPropsIndex
			},
			{
				"0x301a",
				FolderHelper.DataColumnIndex.retentionPeriodIndex
			},
			{
				"0x301d",
				FolderHelper.DataColumnIndex.retentionFlagsIndex
			},
			{
				"0x3018",
				FolderHelper.DataColumnIndex.archiveTagIndex
			},
			{
				"0x301e",
				FolderHelper.DataColumnIndex.archivePeriodIndex
			},
			{
				"-1",
				FolderHelper.DataColumnIndex.retentionTagEntryId
			},
			{
				"0x3613",
				FolderHelper.DataColumnIndex.containerClassIndex
			}
		};

		// Token: 0x040003EF RID: 1007
		protected readonly MailboxSession primaryMailboxSession;

		// Token: 0x040003F0 RID: 1008
		private readonly List<ElcEwsItem> currentBatch;

		// Token: 0x040003F1 RID: 1009
		private readonly bool isCrossPremise;

		// Token: 0x040003F2 RID: 1010
		private uint currentBatchSize;

		// Token: 0x040003F3 RID: 1011
		private int moveToArchiveTotalCount;

		// Token: 0x040003F4 RID: 1012
		private int moveToArchiveTotalCountLimit = 2000;

		// Token: 0x040003F5 RID: 1013
		private int moveToArchiveBatchCountLimit = 100;

		// Token: 0x040003F6 RID: 1014
		private int moveToArchiveBatchSizeLimit = 5242880;

		// Token: 0x040003F7 RID: 1015
		private StatisticsLogEntry statisticsLogEntry;

		// Token: 0x040003F8 RID: 1016
		private Dictionary<PropertyDefinition, PathToExtendedFieldType> ewsStorePropertyMapping;

		// Token: 0x040003F9 RID: 1017
		private List<PathToExtendedFieldType> folderExtendedRetentionProperties;

		// Token: 0x040003FA RID: 1018
		private List<BasePathToElementType> folderAllProperties;
	}
}
