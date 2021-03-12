using System;
using System.Globalization;
using System.Net;
using System.Text;
using System.Threading;
using System.Xml;
using Microsoft.Exchange.Compliance.Xml;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.AirSync;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x02000096 RID: 150
	internal abstract class FolderCommand : Command
	{
		// Token: 0x06000877 RID: 2167 RVA: 0x00032187 File Offset: 0x00030387
		internal FolderCommand()
		{
		}

		// Token: 0x17000336 RID: 822
		// (get) Token: 0x06000878 RID: 2168 RVA: 0x00032196 File Offset: 0x00030396
		// (set) Token: 0x06000879 RID: 2169 RVA: 0x0003219E File Offset: 0x0003039E
		public bool SyncStateChanged
		{
			get
			{
				return this.syncStateChanged;
			}
			set
			{
				this.syncStateChanged = value;
			}
		}

		// Token: 0x17000337 RID: 823
		// (get) Token: 0x0600087A RID: 2170 RVA: 0x000321A7 File Offset: 0x000303A7
		// (set) Token: 0x0600087B RID: 2171 RVA: 0x000321AF File Offset: 0x000303AF
		public CustomSyncState FolderIdMappingSyncState
		{
			get
			{
				return this.folderIdMappingSyncState;
			}
			set
			{
				this.folderIdMappingSyncState = value;
			}
		}

		// Token: 0x17000338 RID: 824
		// (get) Token: 0x0600087C RID: 2172 RVA: 0x000321B8 File Offset: 0x000303B8
		// (set) Token: 0x0600087D RID: 2173 RVA: 0x000321C0 File Offset: 0x000303C0
		public FolderHierarchySyncState FolderHierarchySyncState
		{
			get
			{
				return this.folderHierarchySyncState;
			}
			set
			{
				this.folderHierarchySyncState = value;
			}
		}

		// Token: 0x17000339 RID: 825
		// (get) Token: 0x0600087E RID: 2174 RVA: 0x000321C9 File Offset: 0x000303C9
		// (set) Token: 0x0600087F RID: 2175 RVA: 0x000321D1 File Offset: 0x000303D1
		public FolderHierarchySync FolderHierarchySync
		{
			get
			{
				return this.folderHierarchySync;
			}
			set
			{
				this.folderHierarchySync = value;
			}
		}

		// Token: 0x1700033A RID: 826
		// (get) Token: 0x06000880 RID: 2176 RVA: 0x000321DA File Offset: 0x000303DA
		internal override bool ShouldSaveSyncStatus
		{
			get
			{
				return this.shouldSaveSyncStatus;
			}
		}

		// Token: 0x1700033B RID: 827
		// (get) Token: 0x06000881 RID: 2177
		protected abstract string CommandXmlTag { get; }

		// Token: 0x1700033C RID: 828
		// (get) Token: 0x06000882 RID: 2178 RVA: 0x000321E2 File Offset: 0x000303E2
		protected override string RootNodeNamespace
		{
			get
			{
				return "FolderHierarchy:";
			}
		}

		// Token: 0x06000883 RID: 2179 RVA: 0x000321EC File Offset: 0x000303EC
		public static int ComputeChangeTrackingHash(MailboxSession mbxsession, StoreObjectId folderId, IStorePropertyBag propertyBag)
		{
			StringBuilder stringBuilder = new StringBuilder(64 + FolderCommand.prefetchProperties.Length * 5);
			if (propertyBag != null)
			{
				AirSyncDiagnostics.TraceDebug(ExTraceGlobals.RequestsTracer, null, "[FolderCommand.ComputeChangeTrackingHash] Using passed folder view for change tracking.");
				string text = null;
				StoreObjectId storeObjectId = null;
				bool flag = false;
				stringBuilder.Append(AirSyncUtility.TryGetPropertyFromBag<string>(propertyBag, FolderSchema.DisplayName, out text) ? text : '§');
				stringBuilder.Append(AirSyncUtility.TryGetPropertyFromBag<StoreObjectId>(propertyBag, StoreObjectSchema.ParentItemId, out storeObjectId) ? storeObjectId : '§');
				stringBuilder.Append(AirSyncUtility.TryGetPropertyFromBag<bool>(propertyBag, FolderSchema.IsHidden, out flag) ? flag : '§');
				int num = 0;
				stringBuilder.Append(AirSyncUtility.TryGetPropertyFromBag<int>(propertyBag, FolderSchema.ExtendedFolderFlags, out num) && (num & 1073741824) != 0);
			}
			else
			{
				AirSyncDiagnostics.TraceDebug(ExTraceGlobals.RequestsTracer, null, "[FolderCommand.ComputeChangeTrackingHash] !!!!!WARNING!!!!! Using expensive full bind change tracking mechanism.  This is bad if it is done for entire hierarchy!!!");
				using (Folder folder = Folder.Bind(mbxsession, folderId, FolderCommand.prefetchProperties))
				{
					stringBuilder.Append(folder.DisplayName);
					stringBuilder.Append(folder.ParentId);
					foreach (PropertyDefinition propertyDefinition in FolderCommand.prefetchProperties)
					{
						object obj = folder.TryGetProperty(propertyDefinition);
						if (propertyDefinition == FolderSchema.ExtendedFolderFlags)
						{
							bool value = !(obj is PropertyError) && ((int)obj & 1073741824) != 0;
							stringBuilder.Append(value);
						}
						else if (obj is PropertyError)
						{
							stringBuilder.Append('§');
						}
						else
						{
							stringBuilder.Append(obj);
						}
					}
				}
			}
			return stringBuilder.ToString().GetHashCode();
		}

		// Token: 0x06000884 RID: 2180 RVA: 0x000323A8 File Offset: 0x000305A8
		public static bool FolderSyncRequired(SyncStateStorage syncStateStorage, HierarchySyncOperations folderHierarchyChanges, SyncState folderIdMappingSyncState = null)
		{
			bool flag = folderIdMappingSyncState == null;
			try
			{
				if (folderIdMappingSyncState == null)
				{
					folderIdMappingSyncState = syncStateStorage.GetCustomSyncState(new FolderIdMappingSyncStateInfo(), new PropertyDefinition[0]);
					if (folderIdMappingSyncState == null)
					{
						return true;
					}
				}
				FolderTree folderTree = (FolderTree)folderIdMappingSyncState[CustomStateDatumType.FullFolderTree];
				if (folderTree == null)
				{
					return true;
				}
				for (int i = 0; i < folderHierarchyChanges.Count; i++)
				{
					HierarchySyncOperation hierarchySyncOperation = folderHierarchyChanges[i];
					MailboxSyncItemId folderId = MailboxSyncItemId.CreateForNewItem(hierarchySyncOperation.ItemId);
					MailboxSyncItemId folderId2 = MailboxSyncItemId.CreateForNewItem(hierarchySyncOperation.ParentId);
					if (!folderTree.Contains(folderId2))
					{
						return true;
					}
					if (!folderTree.Contains(folderId))
					{
						if (!folderTree.IsHidden(folderId2))
						{
							return true;
						}
					}
					else if (!folderTree.IsHidden(folderId) || (folderTree.IsHiddenDueToParent(folderId) && !folderTree.IsHidden(folderId2)))
					{
						return true;
					}
				}
			}
			finally
			{
				if (flag && folderIdMappingSyncState != null)
				{
					folderIdMappingSyncState.Dispose();
				}
			}
			return false;
		}

		// Token: 0x06000885 RID: 2181 RVA: 0x00032498 File Offset: 0x00030698
		internal XmlDocument ConstructErrorXml(StatusCode statusCode)
		{
			base.ProtocolLogger.SetValue(ProtocolLoggerData.StatusCode, (int)statusCode);
			XmlDocument xmlDocument = new SafeXmlDocument();
			XmlNode xmlNode = xmlDocument.CreateElement(this.CommandXmlTag, "FolderHierarchy:");
			XmlNode xmlNode2 = xmlDocument.CreateElement("Status", "FolderHierarchy:");
			XmlNode xmlNode3 = xmlNode2;
			int num = (int)statusCode;
			xmlNode3.InnerText = num.ToString(CultureInfo.InvariantCulture);
			xmlDocument.AppendChild(xmlNode);
			xmlNode.AppendChild(xmlNode2);
			return xmlDocument;
		}

		// Token: 0x06000886 RID: 2182 RVA: 0x00032504 File Offset: 0x00030704
		internal override Command.ExecutionState ExecuteCommand()
		{
			try
			{
				FolderCommand.FolderRequest folderRequest = this.ParseRequest();
				XmlDocument xmlDocument = new SafeXmlDocument();
				XmlNode newChild = xmlDocument.CreateElement(base.XmlRequest.LocalName, base.XmlRequest.NamespaceURI);
				xmlDocument.AppendChild(newChild);
				try
				{
					base.ProtocolLogger.SetValue("F", PerFolderProtocolLoggerData.ClientSyncKey, folderRequest.SyncKey);
					this.LoadSyncState(folderRequest.SyncKey);
					this.ConvertSyncIdsToXsoIds(folderRequest);
					this.folderHierarchySync = this.folderHierarchySyncState.GetFolderHierarchySync(new ChangeTrackingDelegate(FolderCommand.ComputeChangeTrackingHash));
					if (folderRequest.SyncKey != 0)
					{
						if (!this.folderHierarchySyncState.Contains(CustomStateDatumType.SyncKey))
						{
							base.ProtocolLogger.SetValue("F", PerFolderProtocolLoggerData.SyncType, "I");
							base.ProtocolLogger.SetValue(ProtocolLoggerData.Error, "RecoveryNotAllowed");
							throw new AirSyncPermanentException(StatusCode.Sync_OutOfDisk, this.ConstructErrorXml(StatusCode.Sync_OutOfDisk), null, false);
						}
						int data = this.folderHierarchySyncState.GetData<Int32Data, int>(CustomStateDatumType.SyncKey, -1);
						int data2 = this.folderHierarchySyncState.GetData<Int32Data, int>(CustomStateDatumType.RecoverySyncKey, -1);
						if (folderRequest.SyncKey != data && (!this.allowRecovery || folderRequest.SyncKey != data2))
						{
							base.ProtocolLogger.SetValue("F", PerFolderProtocolLoggerData.SyncType, "I");
							base.ProtocolLogger.SetValue(ProtocolLoggerData.Error, "InvalidSyncKey");
							throw new AirSyncPermanentException(StatusCode.Sync_OutOfDisk, this.ConstructErrorXml(StatusCode.Sync_OutOfDisk), null, false);
						}
						FolderIdMapping folderIdMapping = (FolderIdMapping)this.folderIdMappingSyncState[CustomStateDatumType.IdMapping];
						if (folderRequest.SyncKey == data)
						{
							AirSyncDiagnostics.TraceDebug(ExTraceGlobals.RequestsTracer, this, "Committing folderIdMapping.");
							base.ProtocolLogger.SetValue("F", PerFolderProtocolLoggerData.SyncType, "S");
							folderIdMapping.CommitChanges();
							this.folderHierarchySync.AcknowledgeServerOperations();
						}
						else
						{
							AirSyncDiagnostics.TraceDebug(ExTraceGlobals.RequestsTracer, this, "Clearing changes on folderIdMapping.");
							base.ProtocolLogger.SetValue("F", PerFolderProtocolLoggerData.SyncType, "R");
							folderIdMapping.ClearChanges();
							this.folderIdMappingSyncState[CustomStateDatumType.FullFolderTree] = this.folderIdMappingSyncState[CustomStateDatumType.RecoveryFullFolderTree];
						}
					}
					else
					{
						base.ProtocolLogger.SetValue("F", PerFolderProtocolLoggerData.SyncType, "F");
						base.SendServerUpgradeHeader = true;
					}
					this.ProcessCommand(folderRequest, xmlDocument);
					if (this.folderHierarchySyncState != null)
					{
						int data3 = this.folderHierarchySyncState.GetData<Int32Data, int>(CustomStateDatumType.AirSyncProtocolVersion, -1);
						if (base.Version > data3)
						{
							AirSyncDiagnostics.TraceDebug<int, int>(ExTraceGlobals.RequestsTracer, this, "Changing sync state protocol version from {0} to {1}.", data3, base.Version);
							this.folderHierarchySyncState[CustomStateDatumType.AirSyncProtocolVersion] = new Int32Data(base.Version);
							this.syncStateChanged = true;
						}
					}
					if (this.syncStateChanged)
					{
						if (this.folderHierarchySyncState != null)
						{
							this.folderHierarchySyncState.CustomVersion = new int?(5);
							this.folderHierarchySyncState.Commit();
						}
						if (this.folderIdMappingSyncState != null)
						{
							FolderIdMapping folderIdMapping2 = this.folderIdMappingSyncState[CustomStateDatumType.IdMapping] as FolderIdMapping;
							FolderTree folderTree = this.folderIdMappingSyncState[CustomStateDatumType.FullFolderTree] as FolderTree;
							if (folderIdMapping2.IsDirty || folderTree.IsDirty)
							{
								this.folderIdMappingSyncState.Commit();
							}
						}
					}
				}
				finally
				{
					if (this.folderIdMappingSyncState != null)
					{
						this.folderIdMappingSyncState.Dispose();
					}
					if (this.folderHierarchySyncState != null)
					{
						this.folderHierarchySyncState.Dispose();
					}
				}
				base.XmlResponse = xmlDocument;
			}
			catch (ObjectNotFoundException innerException)
			{
				base.ProtocolLogger.SetValue(ProtocolLoggerData.Error, "FolderNotFound");
				throw new AirSyncPermanentException(StatusCode.Sync_ProtocolError, this.ConstructErrorXml(StatusCode.Sync_ProtocolError), innerException, false);
			}
			catch (CorruptDataException ex)
			{
				AirSyncUtility.ExceptionToStringHelper arg = new AirSyncUtility.ExceptionToStringHelper(ex);
				base.ProtocolLogger.SetValue(ProtocolLoggerData.Error, "CorruptData");
				AirSyncDiagnostics.TraceDebug<AirSyncUtility.ExceptionToStringHelper>(ExTraceGlobals.RequestsTracer, this, "Corrupted data found, replacing error with wrongsynckey error to force client to refresh.\r\n{0}", arg);
				throw new AirSyncPermanentException(StatusCode.Sync_OutOfDisk, this.ConstructErrorXml(StatusCode.Sync_OutOfDisk), ex, false);
			}
			catch (QuotaExceededException)
			{
				throw;
			}
			catch (StoragePermanentException ex2)
			{
				base.ProtocolLogger.SetValue(ProtocolLoggerData.Error, ex2.GetType().ToString());
				throw new AirSyncPermanentException(StatusCode.Sync_ClientServerConversion, this.ConstructErrorXml(StatusCode.Sync_ClientServerConversion), ex2, false);
			}
			catch (ArgumentException innerException2)
			{
				base.ProtocolLogger.SetValue(ProtocolLoggerData.Error, "ArgumentException");
				throw new AirSyncPermanentException(StatusCode.Sync_NotificationGUID, this.ConstructErrorXml(StatusCode.Sync_NotificationGUID), innerException2, false);
			}
			catch (FormatException innerException3)
			{
				base.ProtocolLogger.SetValue(ProtocolLoggerData.Error, "FormatException");
				throw new AirSyncPermanentException(StatusCode.Sync_NotificationGUID, this.ConstructErrorXml(StatusCode.Sync_NotificationGUID), innerException3, false);
			}
			return Command.ExecutionState.Complete;
		}

		// Token: 0x06000887 RID: 2183 RVA: 0x000329D0 File Offset: 0x00030BD0
		internal FolderCommand.FolderRequest ParseRequest()
		{
			FolderCommand.FolderRequest folderRequest = new FolderCommand.FolderRequest();
			foreach (object obj in base.XmlRequest.ChildNodes)
			{
				XmlNode xmlNode = (XmlNode)obj;
				string name = xmlNode.Name;
				if (name == "SyncKey")
				{
					string text = xmlNode.InnerText;
					int num = text.LastIndexOf('}');
					if (num > 0 && num < text.Length - 1)
					{
						text = text.Substring(num + 1);
						this.allowRecovery = false;
					}
					else
					{
						this.allowRecovery = true;
					}
					int num2;
					if (!int.TryParse(text, out num2))
					{
						AirSyncDiagnostics.TraceDebug<string>(ExTraceGlobals.RequestsTracer, this, "Cannot parse the sync key. syncKeyString = {0}.", text);
						base.ProtocolLogger.SetValue(ProtocolLoggerData.Error, "CannotParseSyncKey_" + text);
						throw new AirSyncPermanentException(StatusCode.Sync_OutOfDisk, this.ConstructErrorXml(StatusCode.Sync_OutOfDisk), null, false);
					}
					AirSyncDiagnostics.TraceDebug<int>(ExTraceGlobals.RequestsTracer, this, "Folder command sync key is {0}.", num2);
					folderRequest.SyncKey = num2;
					if (folderRequest.SyncKey == 0 && this.CommandXmlTag != "FolderSync")
					{
						base.ProtocolLogger.SetValue(ProtocolLoggerData.Error, "ZeroSyncKeyOnNonSync");
						throw new AirSyncPermanentException(StatusCode.Sync_OutOfDisk, this.ConstructErrorXml(StatusCode.Sync_OutOfDisk), null, false);
					}
				}
				else if (name == "ServerId")
				{
					folderRequest.SyncServerId = xmlNode.InnerText;
					AirSyncDiagnostics.TraceDebug<string>(ExTraceGlobals.RequestsTracer, this, "Folder command sync server Id is {0}.", folderRequest.SyncServerId);
				}
				else if (name == "ParentId")
				{
					folderRequest.SyncParentId = xmlNode.InnerText;
					AirSyncDiagnostics.TraceDebug<string>(ExTraceGlobals.RequestsTracer, this, "Folder command sync parent Id is {0}.", folderRequest.SyncParentId);
				}
				else if (name == "DisplayName")
				{
					folderRequest.DisplayName = xmlNode.InnerText;
					AirSyncDiagnostics.TraceDebug<string>(ExTraceGlobals.RequestsTracer, this, "Folder command folder name is {0}.", folderRequest.DisplayName);
					bool flag = true;
					if (string.IsNullOrEmpty(folderRequest.DisplayName))
					{
						base.ProtocolLogger.SetValue(ProtocolLoggerData.Error, "NoFolderName");
						throw new AirSyncPermanentException(StatusCode.Sync_NotificationGUID, this.ConstructErrorXml(StatusCode.Sync_NotificationGUID), null, false);
					}
					foreach (char c in folderRequest.DisplayName.ToCharArray())
					{
						if (c != '\\' && c != '/')
						{
							flag = false;
							break;
						}
					}
					if (flag)
					{
						base.ProtocolLogger.SetValue(ProtocolLoggerData.Error, "BadFolderName");
						throw new AirSyncPermanentException(StatusCode.Sync_ProtocolVersionMismatch, this.ConstructErrorXml(StatusCode.Sync_ProtocolVersionMismatch), null, false);
					}
				}
				else
				{
					if (!(name == "Type"))
					{
						throw new InvalidOperationException("Unkown Folder Command Content");
					}
					folderRequest.Type = xmlNode.InnerText;
					AirSyncDiagnostics.TraceDebug<string>(ExTraceGlobals.RequestsTracer, this, "Folder command folder type is {0}.", folderRequest.Type);
				}
			}
			return folderRequest;
		}

		// Token: 0x06000888 RID: 2184 RVA: 0x00032CA8 File Offset: 0x00030EA8
		protected static bool IsEmptyOrWhiteSpacesOnly(string folderDisplayName)
		{
			if (string.IsNullOrEmpty(folderDisplayName))
			{
				return true;
			}
			for (int i = 0; i < folderDisplayName.Length; i++)
			{
				if (!char.IsWhiteSpace(folderDisplayName[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000889 RID: 2185
		protected abstract void ConvertSyncIdsToXsoIds(FolderCommand.FolderRequest folderRequest);

		// Token: 0x0600088A RID: 2186 RVA: 0x00032CE4 File Offset: 0x00030EE4
		protected bool IsSharedFolder(string type)
		{
			return type.Equals("20", StringComparison.Ordinal) || type.Equals("21", StringComparison.Ordinal) || type.Equals("23", StringComparison.Ordinal) || type.Equals("24", StringComparison.Ordinal) || type.Equals("22", StringComparison.Ordinal);
		}

		// Token: 0x0600088B RID: 2187 RVA: 0x00032D38 File Offset: 0x00030F38
		protected StoreObjectId GetXsoFolderId(string syncFolderId, out SyncPermissions permissions)
		{
			StoreObjectId result = null;
			permissions = SyncPermissions.FullAccess;
			if (syncFolderId == "0")
			{
				result = base.MailboxSession.GetDefaultFolderId(DefaultFolderType.Root);
			}
			else
			{
				MailboxSyncItemId mailboxSyncItemId = ((FolderIdMapping)this.folderIdMappingSyncState[CustomStateDatumType.IdMapping])[syncFolderId] as MailboxSyncItemId;
				if (mailboxSyncItemId != null)
				{
					FolderTree folderTree = (FolderTree)this.FolderIdMappingSyncState[CustomStateDatumType.FullFolderTree];
					result = (StoreObjectId)mailboxSyncItemId.NativeId;
					permissions = folderTree.GetPermissions(mailboxSyncItemId);
				}
			}
			return result;
		}

		// Token: 0x0600088C RID: 2188
		protected abstract void ProcessCommand(FolderCommand.FolderRequest folderRequest, XmlDocument doc);

		// Token: 0x0600088D RID: 2189 RVA: 0x00032DB7 File Offset: 0x00030FB7
		protected override bool HandleQuarantinedState()
		{
			base.XmlResponse = this.ConstructErrorXml(StatusCode.Sync_ClientServerConversion);
			return false;
		}

		// Token: 0x0600088E RID: 2190 RVA: 0x00032DC8 File Offset: 0x00030FC8
		private void LoadSyncState(int syncKey)
		{
			FolderIdMappingSyncStateInfo syncStateInfo = new FolderIdMappingSyncStateInfo();
			if (syncKey == 0)
			{
				base.SendServerUpgradeHeader = true;
				this.folderIdMappingSyncState = base.SyncStateStorage.GetCustomSyncState(syncStateInfo, new PropertyDefinition[0]);
				if (this.folderIdMappingSyncState == null || this.folderIdMappingSyncState[CustomStateDatumType.IdMapping] == null)
				{
					CustomSyncState customSyncState = base.SyncStateStorage.GetCustomSyncState(new GlobalSyncStateInfo(), new PropertyDefinition[0]);
					if (customSyncState == null)
					{
						AirSyncDiagnostics.TraceDebug(ExTraceGlobals.RequestsTracer, this, "Could not find policy sync state.  Deleting all sync states.");
						base.SyncStateStorage.DeleteAllSyncStates();
					}
					else
					{
						customSyncState.Dispose();
						using (FolderHierarchySyncState folderHierarchySyncState = base.SyncStateStorage.GetFolderHierarchySyncState())
						{
							if (folderHierarchySyncState != null)
							{
								AirSyncDiagnostics.TraceDebug(ExTraceGlobals.RequestsTracer, this, "Deleting all sync states.");
								base.SyncStateStorage.DeleteAllSyncStates();
							}
						}
					}
					this.folderIdMappingSyncState = base.SyncStateStorage.CreateCustomSyncState(syncStateInfo);
					this.folderIdMappingSyncState[CustomStateDatumType.IdMapping] = new FolderIdMapping();
					this.folderHierarchySyncState = base.SyncStateStorage.CreateFolderHierarchySyncState();
				}
				else
				{
					AirSyncDiagnostics.TraceDebug(ExTraceGlobals.RequestsTracer, this, "Deleting folder hierarchy sync state.");
					base.SyncStateStorage.DeleteFolderHierarchySyncState();
					this.folderHierarchySyncState = base.SyncStateStorage.CreateFolderHierarchySyncState();
					((FolderIdMapping)this.folderIdMappingSyncState[CustomStateDatumType.IdMapping]).CommitChanges();
				}
				this.folderIdMappingSyncState[CustomStateDatumType.FullFolderTree] = new FolderTree();
				this.folderIdMappingSyncState[CustomStateDatumType.RecoveryFullFolderTree] = this.folderIdMappingSyncState[CustomStateDatumType.FullFolderTree];
				base.InitializeSyncStatusSyncState();
				base.SyncStatusSyncData.ClearClientCategoryHash();
				this.shouldSaveSyncStatus = true;
				Interlocked.Exchange(ref this.validToCommitSyncStatusSyncState, 1);
			}
			else
			{
				this.folderIdMappingSyncState = base.SyncStateStorage.GetCustomSyncState(syncStateInfo, new PropertyDefinition[0]);
				this.folderHierarchySyncState = base.SyncStateStorage.GetFolderHierarchySyncState();
				if (this.folderHierarchySyncState == null || this.folderIdMappingSyncState == null || this.folderIdMappingSyncState[CustomStateDatumType.IdMapping] == null)
				{
					base.ProtocolLogger.SetValue(ProtocolLoggerData.Error, "NoHierarchyState");
					throw new AirSyncPermanentException(StatusCode.Sync_OutOfDisk, this.ConstructErrorXml(StatusCode.Sync_OutOfDisk), null, false);
				}
				FolderIdMapping folderIdMapping = (FolderIdMapping)this.folderIdMappingSyncState[CustomStateDatumType.IdMapping];
				StoreObjectId defaultFolderId = base.MailboxSession.GetDefaultFolderId(DefaultFolderType.Inbox);
				MailboxSyncItemId mailboxId = MailboxSyncItemId.CreateForNewItem(defaultFolderId);
				if (!folderIdMapping.Contains(mailboxId))
				{
					base.SyncStateStorage.DeleteAllSyncStates();
					base.ProtocolLogger.SetValue(ProtocolLoggerData.Error, "InboxStoreObjectIdChanged");
					throw new AirSyncPermanentException(HttpStatusCode.InternalServerError, StatusCode.SyncStateCorrupt, new LocalizedString("The sync state is corrupt.  It is most likely due to a recent mailbox migration."), false);
				}
			}
			if (this.folderHierarchySyncState.CustomVersion != null && this.folderHierarchySyncState.CustomVersion.Value > 5)
			{
				base.ProtocolLogger.SetValue(ProtocolLoggerData.Error, "SyncStateVersionMismatch");
				throw new AirSyncPermanentException(HttpStatusCode.InternalServerError, StatusCode.SyncStateVersionInvalid, EASServerStrings.MismatchSyncStateError, true);
			}
		}

		// Token: 0x04000583 RID: 1411
		internal const string RootFolderId = "0";

		// Token: 0x04000584 RID: 1412
		internal const string StatusCodeSuccess = "1";

		// Token: 0x04000585 RID: 1413
		private const char ErrorPlaceHolder = '§';

		// Token: 0x04000586 RID: 1414
		private static PropertyDefinition[] prefetchProperties = new PropertyDefinition[]
		{
			FolderSchema.IsHidden,
			FolderSchema.ExtendedFolderFlags
		};

		// Token: 0x04000587 RID: 1415
		private FolderHierarchySync folderHierarchySync;

		// Token: 0x04000588 RID: 1416
		private FolderHierarchySyncState folderHierarchySyncState;

		// Token: 0x04000589 RID: 1417
		private CustomSyncState folderIdMappingSyncState;

		// Token: 0x0400058A RID: 1418
		private bool syncStateChanged;

		// Token: 0x0400058B RID: 1419
		private bool allowRecovery = true;

		// Token: 0x0400058C RID: 1420
		private bool shouldSaveSyncStatus;

		// Token: 0x02000097 RID: 151
		internal class FolderRequest
		{
			// Token: 0x1700033D RID: 829
			// (get) Token: 0x06000890 RID: 2192 RVA: 0x000330E2 File Offset: 0x000312E2
			// (set) Token: 0x06000891 RID: 2193 RVA: 0x000330EA File Offset: 0x000312EA
			public StoreObjectId ServerId
			{
				get
				{
					return this.serverId;
				}
				set
				{
					this.serverId = value;
				}
			}

			// Token: 0x1700033E RID: 830
			// (get) Token: 0x06000892 RID: 2194 RVA: 0x000330F3 File Offset: 0x000312F3
			// (set) Token: 0x06000893 RID: 2195 RVA: 0x000330FB File Offset: 0x000312FB
			public int RecoverySyncKey
			{
				get
				{
					return this.recoverySyncKey;
				}
				set
				{
					this.recoverySyncKey = value;
				}
			}

			// Token: 0x1700033F RID: 831
			// (get) Token: 0x06000894 RID: 2196 RVA: 0x00033104 File Offset: 0x00031304
			// (set) Token: 0x06000895 RID: 2197 RVA: 0x0003310C File Offset: 0x0003130C
			public string Type
			{
				get
				{
					return this.type;
				}
				set
				{
					this.type = value;
				}
			}

			// Token: 0x17000340 RID: 832
			// (get) Token: 0x06000896 RID: 2198 RVA: 0x00033115 File Offset: 0x00031315
			// (set) Token: 0x06000897 RID: 2199 RVA: 0x0003311D File Offset: 0x0003131D
			public string DisplayName
			{
				get
				{
					return this.displayName;
				}
				set
				{
					this.displayName = value;
				}
			}

			// Token: 0x17000341 RID: 833
			// (get) Token: 0x06000898 RID: 2200 RVA: 0x00033126 File Offset: 0x00031326
			// (set) Token: 0x06000899 RID: 2201 RVA: 0x0003312E File Offset: 0x0003132E
			public StoreObjectId ParentId
			{
				get
				{
					return this.parentId;
				}
				set
				{
					this.parentId = value;
				}
			}

			// Token: 0x17000342 RID: 834
			// (get) Token: 0x0600089A RID: 2202 RVA: 0x00033137 File Offset: 0x00031337
			// (set) Token: 0x0600089B RID: 2203 RVA: 0x0003313F File Offset: 0x0003133F
			public string SyncParentId
			{
				get
				{
					return this.syncParentId;
				}
				set
				{
					this.syncParentId = value;
				}
			}

			// Token: 0x17000343 RID: 835
			// (get) Token: 0x0600089C RID: 2204 RVA: 0x00033148 File Offset: 0x00031348
			// (set) Token: 0x0600089D RID: 2205 RVA: 0x00033150 File Offset: 0x00031350
			public string SyncServerId
			{
				get
				{
					return this.syncServerId;
				}
				set
				{
					this.syncServerId = value;
				}
			}

			// Token: 0x17000344 RID: 836
			// (get) Token: 0x0600089E RID: 2206 RVA: 0x00033159 File Offset: 0x00031359
			// (set) Token: 0x0600089F RID: 2207 RVA: 0x00033161 File Offset: 0x00031361
			public int SyncKey
			{
				get
				{
					return this.syncKey;
				}
				set
				{
					this.syncKey = value;
				}
			}

			// Token: 0x0400058D RID: 1421
			private string displayName;

			// Token: 0x0400058E RID: 1422
			private StoreObjectId parentId;

			// Token: 0x0400058F RID: 1423
			private int recoverySyncKey;

			// Token: 0x04000590 RID: 1424
			private StoreObjectId serverId;

			// Token: 0x04000591 RID: 1425
			private int syncKey;

			// Token: 0x04000592 RID: 1426
			private string syncParentId;

			// Token: 0x04000593 RID: 1427
			private string syncServerId;

			// Token: 0x04000594 RID: 1428
			private string type;
		}
	}
}
