using System;
using System.Globalization;
using System.Xml;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x0200009B RID: 155
	internal class FolderDeleteCommand : FolderCommand
	{
		// Token: 0x060008BB RID: 2235 RVA: 0x000346B8 File Offset: 0x000328B8
		internal FolderDeleteCommand()
		{
			base.PerfCounter = AirSyncCounters.NumberOfFolderDeletes;
		}

		// Token: 0x1700034E RID: 846
		// (get) Token: 0x060008BC RID: 2236 RVA: 0x000346CB File Offset: 0x000328CB
		protected override string CommandXmlTag
		{
			get
			{
				return "FolderDelete";
			}
		}

		// Token: 0x1700034F RID: 847
		// (get) Token: 0x060008BD RID: 2237 RVA: 0x000346D2 File Offset: 0x000328D2
		protected override string RootNodeName
		{
			get
			{
				return "FolderDelete";
			}
		}

		// Token: 0x17000350 RID: 848
		// (get) Token: 0x060008BE RID: 2238 RVA: 0x000346D9 File Offset: 0x000328D9
		protected override bool IsInteractiveCommand
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060008BF RID: 2239 RVA: 0x000346DC File Offset: 0x000328DC
		protected override void ConvertSyncIdsToXsoIds(FolderCommand.FolderRequest folderRequest)
		{
			SyncCollection.CollectionTypes collectionType = AirSyncUtility.GetCollectionType(folderRequest.SyncServerId);
			if (collectionType != SyncCollection.CollectionTypes.Mailbox && collectionType != SyncCollection.CollectionTypes.Unknown)
			{
				base.ProtocolLogger.SetValue(ProtocolLoggerData.Error, "DeleteOfVFolder");
				AirSyncPermanentException ex = new AirSyncPermanentException(StatusCode.Sync_InvalidSyncKey, base.ConstructErrorXml(StatusCode.Sync_InvalidSyncKey), null, false);
				throw ex;
			}
			SyncPermissions syncPermissions;
			folderRequest.ServerId = base.GetXsoFolderId(folderRequest.SyncServerId, out syncPermissions);
			if (folderRequest.ServerId == null)
			{
				base.ProtocolLogger.SetValue(ProtocolLoggerData.Error, "DeleteOfInvalidId");
				AirSyncPermanentException ex2 = new AirSyncPermanentException(StatusCode.Sync_ProtocolError, base.ConstructErrorXml(StatusCode.Sync_ProtocolError), null, false);
				throw ex2;
			}
			if (syncPermissions == SyncPermissions.FullAccess)
			{
				return;
			}
			if (base.Version < 140)
			{
				throw new InvalidOperationException("Pre-Version 14 device should not see a non-FullAccess folder! Folder Access Level: " + syncPermissions);
			}
			base.ProtocolLogger.SetValue(ProtocolLoggerData.Error, "DeleteOfNonFullAccessFolder");
			AirSyncPermanentException ex3 = new AirSyncPermanentException(StatusCode.Sync_InvalidSyncKey, base.ConstructErrorXml(StatusCode.Sync_InvalidSyncKey), null, false);
			throw ex3;
		}

		// Token: 0x060008C0 RID: 2240 RVA: 0x000347AC File Offset: 0x000329AC
		internal override XmlDocument GetValidationErrorXml()
		{
			if (FolderDeleteCommand.validationErrorXml == null)
			{
				XmlDocument commandXmlStub = base.GetCommandXmlStub();
				XmlElement xmlElement = commandXmlStub.CreateElement("Status", this.RootNodeNamespace);
				xmlElement.InnerText = XmlConvert.ToString(10);
				commandXmlStub[this.RootNodeName].AppendChild(xmlElement);
				FolderDeleteCommand.validationErrorXml = commandXmlStub;
			}
			return FolderDeleteCommand.validationErrorXml;
		}

		// Token: 0x060008C1 RID: 2241 RVA: 0x00034804 File Offset: 0x00032A04
		protected override void ProcessCommand(FolderCommand.FolderRequest folderRequest, XmlDocument doc)
		{
			string namespaceURI = doc.DocumentElement.NamespaceURI;
			DefaultFolderType defaultFolderType = base.MailboxSession.IsDefaultFolderType(folderRequest.ServerId);
			AirSyncPermanentException ex;
			if (defaultFolderType != DefaultFolderType.None)
			{
				base.ProtocolLogger.SetValue(ProtocolLoggerData.Error, "DeleteOfDefaultFolder");
				ex = new AirSyncPermanentException(StatusCode.Sync_InvalidSyncKey, base.ConstructErrorXml(StatusCode.Sync_InvalidSyncKey), null, false);
				throw ex;
			}
			AggregateOperationResult aggregateOperationResult = base.MailboxSession.Delete(DeleteItemFlags.SoftDelete, new StoreObjectId[]
			{
				folderRequest.ServerId
			});
			if (aggregateOperationResult.OperationResult != OperationResult.Failed)
			{
				base.FolderHierarchySync.RecordClientOperation(folderRequest.ServerId, ChangeType.Delete, null);
				folderRequest.RecoverySyncKey = folderRequest.SyncKey;
				folderRequest.SyncKey = base.GetNextNumber(folderRequest.SyncKey);
				base.SyncStateChanged = true;
				base.FolderHierarchySyncState[CustomStateDatumType.SyncKey] = new Int32Data(folderRequest.SyncKey);
				base.FolderHierarchySyncState[CustomStateDatumType.RecoverySyncKey] = new Int32Data(folderRequest.RecoverySyncKey);
				FolderIdMapping folderIdMapping = (FolderIdMapping)base.FolderIdMappingSyncState[CustomStateDatumType.IdMapping];
				FolderTree folderTree = (FolderTree)base.FolderIdMappingSyncState[CustomStateDatumType.FullFolderTree];
				MailboxSyncItemId folderId = MailboxSyncItemId.CreateForNewItem(folderRequest.ServerId);
				folderTree.RemoveFolderAndChildren(folderId, folderIdMapping);
				folderIdMapping.CommitChanges();
				base.ProtocolLogger.IncrementValue("F", PerFolderProtocolLoggerData.ClientDeletes);
				XmlNode xmlNode = doc.CreateElement("Status", namespaceURI);
				xmlNode.InnerText = "1";
				XmlNode xmlNode2 = doc.CreateElement("SyncKey", namespaceURI);
				xmlNode2.InnerText = folderRequest.SyncKey.ToString(CultureInfo.InvariantCulture);
				doc.DocumentElement.AppendChild(xmlNode);
				doc.DocumentElement.AppendChild(xmlNode2);
				return;
			}
			if (aggregateOperationResult.GroupOperationResults != null && aggregateOperationResult.GroupOperationResults.Length > 0 && aggregateOperationResult.GroupOperationResults[0] != null && aggregateOperationResult.GroupOperationResults[0].Exception is ObjectNotFoundException)
			{
				base.ProtocolLogger.SetValue(ProtocolLoggerData.Error, "DeleteOfNonExistentFolder");
				ex = new AirSyncPermanentException(StatusCode.Sync_ProtocolError, base.ConstructErrorXml(StatusCode.Sync_ProtocolError), null, false);
				throw ex;
			}
			base.ProtocolLogger.SetValue(ProtocolLoggerData.Error, "DeleteFailed");
			ex = new AirSyncPermanentException(StatusCode.Sync_ClientServerConversion, base.ConstructErrorXml(StatusCode.Sync_ClientServerConversion), null, false);
			throw ex;
		}

		// Token: 0x0400059C RID: 1436
		private static XmlDocument validationErrorXml;
	}
}
