using System;
using System.Globalization;
using System.Xml;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x0200008D RID: 141
	internal class EmptyFolderContentsProvider : IItemOperationsProvider, IReusable
	{
		// Token: 0x0600075C RID: 1884 RVA: 0x00028E82 File Offset: 0x00027082
		internal EmptyFolderContentsProvider(SyncStateStorage syncStateStorage, MailboxSession mailboxSession)
		{
			AirSyncCounters.NumberOfEmptyFolderContents.Increment();
			this.syncStateStorage = syncStateStorage;
			this.mailboxSession = mailboxSession;
		}

		// Token: 0x170002DC RID: 732
		// (get) Token: 0x0600075D RID: 1885 RVA: 0x00028EA3 File Offset: 0x000270A3
		public bool RightsManagementSupport
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600075E RID: 1886 RVA: 0x00028EA8 File Offset: 0x000270A8
		public void BuildErrorResponse(string statusCode, XmlNode responseNode, ProtocolLogger protocolLogger)
		{
			if (protocolLogger != null)
			{
				protocolLogger.IncrementValue(ProtocolLoggerData.IOEmptyFolderContentsErrors);
			}
			XmlNode xmlNode = responseNode.OwnerDocument.CreateElement("EmptyFolderContents", "ItemOperations:");
			XmlNode xmlNode2 = responseNode.OwnerDocument.CreateElement("Status", "ItemOperations:");
			xmlNode2.InnerText = statusCode;
			xmlNode.AppendChild(xmlNode2);
			if (!string.IsNullOrEmpty(this.folderId))
			{
				XmlNode xmlNode3 = responseNode.OwnerDocument.CreateElement("CollectionId", "AirSync:");
				xmlNode3.InnerText = this.folderId;
				xmlNode.AppendChild(xmlNode3);
			}
			responseNode.AppendChild(xmlNode);
		}

		// Token: 0x0600075F RID: 1887 RVA: 0x00028F3C File Offset: 0x0002713C
		public void BuildResponse(XmlNode responseNode)
		{
			XmlNode xmlNode = responseNode.OwnerDocument.CreateElement("EmptyFolderContents", "ItemOperations:");
			XmlNode xmlNode2 = responseNode.OwnerDocument.CreateElement("Status", "ItemOperations:");
			XmlNode xmlNode3 = responseNode.OwnerDocument.CreateElement("CollectionId", "AirSync:");
			xmlNode2.InnerText = 1.ToString(CultureInfo.InvariantCulture);
			xmlNode.AppendChild(xmlNode2);
			xmlNode3.InnerText = this.folderId;
			xmlNode.AppendChild(xmlNode3);
			responseNode.AppendChild(xmlNode);
		}

		// Token: 0x06000760 RID: 1888 RVA: 0x00028FC4 File Offset: 0x000271C4
		public void Execute()
		{
			StoreObjectId storeObjectId = this.GetStoreObjectId(this.folderId);
			Folder folder = null;
			try
			{
				folder = Folder.Bind(this.mailboxSession, storeObjectId, null);
				if (this.deleteSubFolders)
				{
					GroupOperationResult groupOperationResult = folder.DeleteAllObjects(DeleteItemFlags.SoftDelete);
					if (groupOperationResult.OperationResult == OperationResult.PartiallySucceeded)
					{
						throw new AirSyncPermanentException(StatusCode.ItemOperations_PartialSuccess, false)
						{
							ErrorStringForProtocolLogger = "PartialSuccessInEmptyFolder"
						};
					}
					if (groupOperationResult.OperationResult == OperationResult.Failed)
					{
						throw new AirSyncPermanentException(StatusCode.Sync_InvalidSyncKey, false)
						{
							ErrorStringForProtocolLogger = "FailureInEmptyFolder"
						};
					}
				}
				else
				{
					AggregateOperationResult aggregateOperationResult = folder.DeleteAllItems(DeleteItemFlags.SoftDelete);
					if (aggregateOperationResult.OperationResult == OperationResult.PartiallySucceeded)
					{
						throw new AirSyncPermanentException(StatusCode.ItemOperations_PartialSuccess, false)
						{
							ErrorStringForProtocolLogger = "PartialSuccessInEmptyFolder2"
						};
					}
					if (aggregateOperationResult.OperationResult == OperationResult.Failed)
					{
						throw new AirSyncPermanentException(StatusCode.Sync_InvalidSyncKey, false)
						{
							ErrorStringForProtocolLogger = "FailureInEmptyFolder2"
						};
					}
				}
			}
			catch (ObjectNotFoundException innerException)
			{
				throw new AirSyncPermanentException(StatusCode.Sync_ClientServerConversion, innerException, false)
				{
					ErrorStringForProtocolLogger = "NotFoundInEmptyFolder2"
				};
			}
			finally
			{
				if (folder != null)
				{
					folder.Dispose();
				}
			}
		}

		// Token: 0x06000761 RID: 1889 RVA: 0x000290D4 File Offset: 0x000272D4
		public void ParseRequest(XmlNode fetchNode)
		{
			foreach (object obj in fetchNode.ChildNodes)
			{
				XmlNode xmlNode = (XmlNode)obj;
				string name;
				if ((name = xmlNode.Name) != null)
				{
					if (name == "CollectionId")
					{
						this.folderId = xmlNode.InnerText;
						continue;
					}
					if (name == "Options")
					{
						this.ParseOptions(xmlNode);
						continue;
					}
				}
				throw new AirSyncPermanentException(StatusCode.Sync_ProtocolVersionMismatch, false)
				{
					ErrorStringForProtocolLogger = "BadNodeInEmptyFolder"
				};
			}
			if (this.folderId == null)
			{
				throw new AirSyncPermanentException(StatusCode.Sync_ProtocolVersionMismatch, false)
				{
					ErrorStringForProtocolLogger = "NoFolderIdInEmptyFolder"
				};
			}
		}

		// Token: 0x06000762 RID: 1890 RVA: 0x0002919C File Offset: 0x0002739C
		public void Reset()
		{
			this.folderId = null;
			this.deleteSubFolders = false;
		}

		// Token: 0x06000763 RID: 1891 RVA: 0x000291AC File Offset: 0x000273AC
		private StoreObjectId GetStoreObjectId(string folderId)
		{
			if (this.folderIdMapping == null)
			{
				using (CustomSyncState customSyncState = this.syncStateStorage.GetCustomSyncState(new FolderIdMappingSyncStateInfo(), new PropertyDefinition[0]))
				{
					if (customSyncState == null)
					{
						throw new AirSyncPermanentException(StatusCode.Sync_ClientServerConversion, false)
						{
							ErrorStringForProtocolLogger = "NoSyncStateInEmptyFolder"
						};
					}
					if (customSyncState[CustomStateDatumType.IdMapping] == null)
					{
						throw new AirSyncPermanentException(StatusCode.Sync_ClientServerConversion, false)
						{
							ErrorStringForProtocolLogger = "NoIdMappingInEmptyFolder"
						};
					}
					this.folderIdMapping = (FolderIdMapping)customSyncState[CustomStateDatumType.IdMapping];
				}
			}
			SyncCollection.CollectionTypes collectionType = AirSyncUtility.GetCollectionType(folderId);
			if (collectionType != SyncCollection.CollectionTypes.Mailbox && collectionType != SyncCollection.CollectionTypes.Unknown)
			{
				throw new AirSyncPermanentException(StatusCode.Sync_Retry, false)
				{
					ErrorStringForProtocolLogger = "SpecialFolderInEmptyFolder"
				};
			}
			MailboxSyncItemId mailboxSyncItemId = this.folderIdMapping[folderId] as MailboxSyncItemId;
			if (mailboxSyncItemId == null)
			{
				throw new AirSyncPermanentException(StatusCode.Sync_ClientServerConversion, false)
				{
					ErrorStringForProtocolLogger = "NoIdMappingInEmptyFolder2"
				};
			}
			return (StoreObjectId)mailboxSyncItemId.NativeId;
		}

		// Token: 0x06000764 RID: 1892 RVA: 0x000292A8 File Offset: 0x000274A8
		private void ParseOptions(XmlNode optionsNode)
		{
			if (optionsNode.ChildNodes.Count != 1 || optionsNode.ChildNodes[0].Name != "DeleteSubFolders" || optionsNode.ChildNodes[0].HasChildNodes)
			{
				throw new AirSyncPermanentException(StatusCode.Sync_ProtocolVersionMismatch, false)
				{
					ErrorStringForProtocolLogger = "BadOptionsInEmptyFolder"
				};
			}
			this.deleteSubFolders = true;
		}

		// Token: 0x04000509 RID: 1289
		private bool deleteSubFolders;

		// Token: 0x0400050A RID: 1290
		private string folderId;

		// Token: 0x0400050B RID: 1291
		private FolderIdMapping folderIdMapping;

		// Token: 0x0400050C RID: 1292
		private MailboxSession mailboxSession;

		// Token: 0x0400050D RID: 1293
		private SyncStateStorage syncStateStorage;
	}
}
