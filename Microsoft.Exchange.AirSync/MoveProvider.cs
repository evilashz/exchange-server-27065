using System;
using System.Globalization;
using System.IO;
using System.Xml;
using Microsoft.Exchange.AirSync.SchemaConverter.AirSync;
using Microsoft.Exchange.AirSync.Wbxml;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x020000EB RID: 235
	internal sealed class MoveProvider : IItemOperationsProvider, IReusable
	{
		// Token: 0x06000D0D RID: 3341 RVA: 0x00047228 File Offset: 0x00045428
		internal MoveProvider(SyncStateStorage syncStateStorage, MailboxSession mailboxSession)
		{
			this.syncStateStorage = syncStateStorage;
			this.mailboxSession = mailboxSession;
		}

		// Token: 0x17000517 RID: 1303
		// (get) Token: 0x06000D0E RID: 3342 RVA: 0x0004723E File Offset: 0x0004543E
		public bool RightsManagementSupport
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000D0F RID: 3343 RVA: 0x00047241 File Offset: 0x00045441
		public void BuildErrorResponse(string statusCode, XmlNode responseNode, ProtocolLogger protocolLogger)
		{
			if (protocolLogger != null)
			{
				protocolLogger.IncrementValue(ProtocolLoggerData.IOMoveErrors);
			}
			this.BuildResponseWithStatus(statusCode, responseNode);
		}

		// Token: 0x06000D10 RID: 3344 RVA: 0x00047258 File Offset: 0x00045458
		public void BuildResponse(XmlNode responseNode)
		{
			this.BuildResponseWithStatus(1.ToString(CultureInfo.InvariantCulture), responseNode);
		}

		// Token: 0x06000D11 RID: 3345 RVA: 0x0004727C File Offset: 0x0004547C
		public void Execute()
		{
			StoreObjectId storeObjectId = this.GetStoreObjectId(this.dstSyncFolderId);
			try
			{
				Conversation conversation = Conversation.Load(this.mailboxSession, this.conversationId, new PropertyDefinition[0]);
				AggregateOperationResult aggregateOperationResult;
				if (this.moveAlways)
				{
					aggregateOperationResult = conversation.AlwaysMove(storeObjectId, true);
				}
				else
				{
					aggregateOperationResult = conversation.Move(null, null, this.mailboxSession, storeObjectId);
				}
				if (aggregateOperationResult.OperationResult == OperationResult.PartiallySucceeded)
				{
					throw new AirSyncPermanentException(StatusCode.ItemOperations_PartialSuccess, false)
					{
						ErrorStringForProtocolLogger = "PartialSuccessInConversationMove"
					};
				}
				if (aggregateOperationResult.OperationResult == OperationResult.Failed)
				{
					throw new AirSyncPermanentException(StatusCode.Sync_InvalidSyncKey, false)
					{
						ErrorStringForProtocolLogger = "FailureInConversationMove"
					};
				}
			}
			catch (InvalidFolderTypeException innerException)
			{
				throw new AirSyncPermanentException(StatusCode.MoveCommandInvalidDestinationFolder, innerException, false)
				{
					ErrorStringForProtocolLogger = "BadFolderOnConversationMove"
				};
			}
			catch (StoragePermanentException ex)
			{
				throw new AirSyncPermanentException(StatusCode.Sync_InvalidSyncKey, ex, false)
				{
					ErrorStringForProtocolLogger = "ExceptionOnConversationMoveAlways:" + ex.GetType()
				};
			}
		}

		// Token: 0x06000D12 RID: 3346 RVA: 0x00047384 File Offset: 0x00045584
		public void ParseRequest(XmlNode node)
		{
			foreach (object obj in node.ChildNodes)
			{
				XmlNode xmlNode = (XmlNode)obj;
				string name;
				if ((name = xmlNode.Name) != null)
				{
					if (name == "ConversationId")
					{
						this.conversationIdNode = xmlNode;
						AirSyncByteArrayProperty airSyncByteArrayProperty = new AirSyncByteArrayProperty("ItemOperations:", "ConversationId", false);
						airSyncByteArrayProperty.Bind(xmlNode);
						if (airSyncByteArrayProperty.ByteArrayData != null)
						{
							try
							{
								this.conversationId = ConversationId.Create(airSyncByteArrayProperty.ByteArrayData);
								continue;
							}
							catch (CorruptDataException innerException)
							{
								throw new AirSyncPermanentException(StatusCode.Sync_ClientServerConversion, innerException, false)
								{
									ErrorStringForProtocolLogger = "BadConversationIdOnMove"
								};
							}
						}
						throw new AirSyncPermanentException(StatusCode.Sync_ProtocolVersionMismatch, false)
						{
							ErrorStringForProtocolLogger = "InvalidConversationId(" + xmlNode.Name + ")OnMove"
						};
					}
					if (name == "DstFldId")
					{
						this.dstSyncFolderId = xmlNode.InnerText;
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
					ErrorStringForProtocolLogger = "InvalidNode(" + xmlNode.Name + ")OnConversationMove"
				};
			}
			if (string.IsNullOrEmpty(this.dstSyncFolderId) || this.conversationId == null)
			{
				throw new AirSyncPermanentException(StatusCode.Sync_ProtocolVersionMismatch, false)
				{
					ErrorStringForProtocolLogger = "NoDstFldOrIdOnConversationMove"
				};
			}
		}

		// Token: 0x06000D13 RID: 3347 RVA: 0x0004752C File Offset: 0x0004572C
		public void Reset()
		{
			this.dstSyncFolderId = null;
			this.conversationId = null;
			this.moveAlways = false;
		}

		// Token: 0x06000D14 RID: 3348 RVA: 0x00047544 File Offset: 0x00045744
		private StoreObjectId GetStoreObjectId(string folderId)
		{
			SyncCollection.CollectionTypes collectionType = AirSyncUtility.GetCollectionType(folderId);
			if (collectionType != SyncCollection.CollectionTypes.Mailbox && collectionType != SyncCollection.CollectionTypes.Unknown)
			{
				throw new AirSyncPermanentException(StatusCode.InvalidCombinationOfIDs, false)
				{
					ErrorStringForProtocolLogger = "BadIdComboInConversationMove"
				};
			}
			if (this.folderIdMapping == null)
			{
				using (CustomSyncState customSyncState = this.syncStateStorage.GetCustomSyncState(new FolderIdMappingSyncStateInfo(), new PropertyDefinition[0]))
				{
					if (customSyncState == null)
					{
						throw new AirSyncPermanentException(StatusCode.Sync_ClientServerConversion, false)
						{
							ErrorStringForProtocolLogger = "NoSyncStateInConversationMove"
						};
					}
					if (customSyncState[CustomStateDatumType.IdMapping] == null)
					{
						throw new AirSyncPermanentException(StatusCode.Sync_ClientServerConversion, false)
						{
							ErrorStringForProtocolLogger = "NoIdMappingInConversationMove"
						};
					}
					this.folderIdMapping = (FolderIdMapping)customSyncState[CustomStateDatumType.IdMapping];
					this.fullFolderTree = (FolderTree)customSyncState[CustomStateDatumType.FullFolderTree];
				}
			}
			ISyncItemId syncItemId = this.folderIdMapping[folderId];
			if (syncItemId == null)
			{
				throw new AirSyncPermanentException(StatusCode.Sync_ClientServerConversion, false)
				{
					ErrorStringForProtocolLogger = "NoFldrIdInMappingInConversationMove"
				};
			}
			MailboxSyncItemId mailboxSyncItemId = syncItemId as MailboxSyncItemId;
			if (mailboxSyncItemId == null)
			{
				throw new AirSyncPermanentException(StatusCode.InvalidIDs, false)
				{
					ErrorStringForProtocolLogger = "BadIdInConversationMove"
				};
			}
			if (this.fullFolderTree.IsSharedFolder(mailboxSyncItemId) || this.fullFolderTree.GetPermissions(mailboxSyncItemId) != SyncPermissions.FullAccess)
			{
				throw new AirSyncPermanentException(StatusCode.Sync_Retry, false)
				{
					ErrorStringForProtocolLogger = "DeniedInConversationMove"
				};
			}
			return (StoreObjectId)mailboxSyncItemId.NativeId;
		}

		// Token: 0x06000D15 RID: 3349 RVA: 0x000476B0 File Offset: 0x000458B0
		private void ParseOptions(XmlNode optionsNode)
		{
			foreach (object obj in optionsNode.ChildNodes)
			{
				XmlNode xmlNode = (XmlNode)obj;
				string localName;
				if ((localName = xmlNode.LocalName) == null || !(localName == "MoveAlways"))
				{
					throw new AirSyncPermanentException(StatusCode.Sync_ProtocolVersionMismatch, false)
					{
						ErrorStringForProtocolLogger = "BadOptionsInConversationMove"
					};
				}
				if (!string.IsNullOrEmpty(xmlNode.InnerText))
				{
					if (xmlNode.InnerText.Equals("1"))
					{
						this.moveAlways = true;
					}
					else
					{
						if (!xmlNode.InnerText.Equals("0"))
						{
							throw new AirSyncPermanentException(StatusCode.Sync_ProtocolVersionMismatch, false)
							{
								ErrorStringForProtocolLogger = "BadOptionsInConversationMove"
							};
						}
						this.moveAlways = false;
					}
				}
				else
				{
					this.moveAlways = true;
				}
			}
		}

		// Token: 0x06000D16 RID: 3350 RVA: 0x00047798 File Offset: 0x00045998
		private void BuildResponseWithStatus(string statusCode, XmlNode responseNode)
		{
			XmlNode xmlNode = responseNode.OwnerDocument.CreateElement("Move", "ItemOperations:");
			XmlNode xmlNode2 = responseNode.OwnerDocument.CreateElement("Status", "ItemOperations:");
			xmlNode2.InnerText = statusCode;
			xmlNode.AppendChild(xmlNode2);
			AirSyncBlobXmlNode airSyncBlobXmlNode = new AirSyncBlobXmlNode(null, "ConversationId", "ItemOperations:", responseNode.OwnerDocument);
			if (this.conversationId != null)
			{
				airSyncBlobXmlNode.ByteArray = this.conversationId.GetBytes();
			}
			else
			{
				AirSyncBlobXmlNode airSyncBlobXmlNode2 = (AirSyncBlobXmlNode)this.conversationIdNode;
				if (airSyncBlobXmlNode2 != null && airSyncBlobXmlNode2.Stream != null && airSyncBlobXmlNode2.Stream.CanSeek && airSyncBlobXmlNode2.Stream.CanRead)
				{
					airSyncBlobXmlNode.ByteArray = new byte[airSyncBlobXmlNode2.Stream.Length];
					airSyncBlobXmlNode2.Stream.Seek(0L, SeekOrigin.Begin);
					airSyncBlobXmlNode2.Stream.Read(airSyncBlobXmlNode.ByteArray, 0, (int)airSyncBlobXmlNode2.Stream.Length);
				}
			}
			xmlNode.AppendChild(airSyncBlobXmlNode);
			responseNode.AppendChild(xmlNode);
		}

		// Token: 0x0400080C RID: 2060
		private bool moveAlways;

		// Token: 0x0400080D RID: 2061
		private string dstSyncFolderId;

		// Token: 0x0400080E RID: 2062
		private ConversationId conversationId;

		// Token: 0x0400080F RID: 2063
		private XmlNode conversationIdNode;

		// Token: 0x04000810 RID: 2064
		private FolderIdMapping folderIdMapping;

		// Token: 0x04000811 RID: 2065
		private FolderTree fullFolderTree;

		// Token: 0x04000812 RID: 2066
		private MailboxSession mailboxSession;

		// Token: 0x04000813 RID: 2067
		private SyncStateStorage syncStateStorage;
	}
}
