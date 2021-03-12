using System;
using System.Globalization;
using System.Net;
using System.Xml;
using Microsoft.Exchange.Compliance.Xml;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.AirSync;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x02000046 RID: 70
	internal abstract class CollectionCommand : Command
	{
		// Token: 0x0600048B RID: 1163 RVA: 0x0001C2A4 File Offset: 0x0001A4A4
		internal CollectionCommand()
		{
		}

		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x0600048C RID: 1164 RVA: 0x0001C2AC File Offset: 0x0001A4AC
		internal override int MaxVersion
		{
			get
			{
				return 121;
			}
		}

		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x0600048D RID: 1165 RVA: 0x0001C2B0 File Offset: 0x0001A4B0
		internal string CollectionId
		{
			get
			{
				string legacyUrlParameter = base.Request.GetLegacyUrlParameter("CollectionId");
				if (legacyUrlParameter != null && (legacyUrlParameter.Length < 1 || legacyUrlParameter.Length > 256))
				{
					base.ProtocolLogger.SetValue(ProtocolLoggerData.Error, "CollectionIdInvalid");
					throw new AirSyncPermanentException(HttpStatusCode.BadRequest, StatusCode.InvalidIDs, null, false);
				}
				return legacyUrlParameter;
			}
		}

		// Token: 0x170001BA RID: 442
		// (get) Token: 0x0600048E RID: 1166 RVA: 0x0001C30C File Offset: 0x0001A50C
		internal string ParentId
		{
			get
			{
				string legacyUrlParameter = base.Request.GetLegacyUrlParameter("ParentId");
				if (legacyUrlParameter != null && (legacyUrlParameter.Length < 1 || legacyUrlParameter.Length > 256))
				{
					base.ProtocolLogger.SetValue(ProtocolLoggerData.Error, "ParentIdInvalid");
					throw new AirSyncPermanentException(HttpStatusCode.BadRequest, StatusCode.InvalidIDs, null, false);
				}
				return legacyUrlParameter;
			}
		}

		// Token: 0x170001BB RID: 443
		// (get) Token: 0x0600048F RID: 1167 RVA: 0x0001C368 File Offset: 0x0001A568
		internal string CollectionName
		{
			get
			{
				string legacyUrlParameter = base.Request.GetLegacyUrlParameter("CollectionName");
				if (legacyUrlParameter != null && (legacyUrlParameter.Length < 1 || legacyUrlParameter.Length > 256))
				{
					base.ProtocolLogger.SetValue(ProtocolLoggerData.Error, "CollectionNameInvalid");
					throw new AirSyncPermanentException(HttpStatusCode.BadRequest, StatusCode.InvalidIDs, null, false);
				}
				return legacyUrlParameter;
			}
		}

		// Token: 0x170001BC RID: 444
		// (get) Token: 0x06000490 RID: 1168 RVA: 0x0001C3C1 File Offset: 0x0001A5C1
		protected sealed override string RootNodeName
		{
			get
			{
				return "Invalid";
			}
		}

		// Token: 0x170001BD RID: 445
		// (get) Token: 0x06000491 RID: 1169 RVA: 0x0001C3C8 File Offset: 0x0001A5C8
		protected CollectionCommand.CollectionRequestStruct CollectionRequest
		{
			get
			{
				return this.collectionRequest;
			}
		}

		// Token: 0x170001BE RID: 446
		// (get) Token: 0x06000492 RID: 1170 RVA: 0x0001C3D0 File Offset: 0x0001A5D0
		protected CustomSyncState SyncState
		{
			get
			{
				return this.syncState;
			}
		}

		// Token: 0x06000493 RID: 1171 RVA: 0x0001C3D8 File Offset: 0x0001A5D8
		internal static XmlDocument ConstructErrorXml(StatusCode statusCode)
		{
			XmlDocument xmlDocument = new SafeXmlDocument();
			XmlNode xmlNode = xmlDocument.CreateElement("Response", "FolderHierarchy:");
			XmlNode xmlNode2 = xmlDocument.CreateElement("Status", "FolderHierarchy:");
			XmlNode xmlNode3 = xmlNode2;
			int num = (int)statusCode;
			xmlNode3.InnerText = num.ToString(CultureInfo.InvariantCulture);
			xmlDocument.AppendChild(xmlNode);
			xmlNode.AppendChild(xmlNode2);
			return xmlDocument;
		}

		// Token: 0x06000494 RID: 1172 RVA: 0x0001C434 File Offset: 0x0001A634
		internal override Command.ExecutionState ExecuteCommand()
		{
			try
			{
				XmlDocument xmlDocument = new SafeXmlDocument();
				try
				{
					this.LoadSyncState();
					this.ParseRequest(base.MailboxSession);
					this.ProcessCommand(base.MailboxSession, xmlDocument);
					if (((FolderIdMapping)this.syncState[CustomStateDatumType.IdMapping]).IsDirty)
					{
						this.syncState.Commit();
					}
				}
				finally
				{
					if (this.syncState != null)
					{
						this.syncState.Dispose();
					}
					this.syncState = null;
				}
				base.XmlResponse = xmlDocument;
			}
			catch (QuotaExceededException)
			{
				throw;
			}
			catch (ObjectNotFoundException innerException)
			{
				throw new AirSyncPermanentException(StatusCode.Sync_ProtocolError, CollectionCommand.ConstructErrorXml(StatusCode.Sync_ProtocolError), innerException, false)
				{
					ErrorStringForProtocolLogger = "FolderNotFound"
				};
			}
			catch (StoragePermanentException ex)
			{
				throw new AirSyncPermanentException(StatusCode.Sync_ClientServerConversion, CollectionCommand.ConstructErrorXml(StatusCode.Sync_ClientServerConversion), ex, false)
				{
					ErrorStringForProtocolLogger = ex.GetType().FullName
				};
			}
			return Command.ExecutionState.Complete;
		}

		// Token: 0x06000495 RID: 1173
		protected abstract void ProcessCommand(MailboxSession mailboxSession, XmlDocument doc);

		// Token: 0x06000496 RID: 1174 RVA: 0x0001C530 File Offset: 0x0001A730
		protected override bool HandleQuarantinedState()
		{
			base.XmlResponse = CollectionCommand.ConstructErrorXml(StatusCode.Sync_ClientServerConversion);
			return false;
		}

		// Token: 0x06000497 RID: 1175 RVA: 0x0001C540 File Offset: 0x0001A740
		private void LoadSyncState()
		{
			this.syncState = base.SyncStateStorage.GetCustomSyncState(new FolderIdMappingSyncStateInfo(), new PropertyDefinition[0]);
			if (this.syncState == null)
			{
				throw new AirSyncPermanentException(HttpStatusCode.InternalServerError, StatusCode.ServerError, null, false)
				{
					ErrorStringForProtocolLogger = "CorruptSyncState"
				};
			}
		}

		// Token: 0x06000498 RID: 1176 RVA: 0x0001C590 File Offset: 0x0001A790
		private void ParseRequest(MailboxSession mailboxSession)
		{
			this.collectionRequest = default(CollectionCommand.CollectionRequestStruct);
			string collectionId = this.CollectionId;
			if (collectionId != null)
			{
				MailboxSyncItemId mailboxSyncItemId = ((FolderIdMapping)this.syncState[CustomStateDatumType.IdMapping])[collectionId] as MailboxSyncItemId;
				if (mailboxSyncItemId == null)
				{
					throw new AirSyncPermanentException(StatusCode.Sync_ProtocolError, CollectionCommand.ConstructErrorXml(StatusCode.Sync_ProtocolError), null, false)
					{
						ErrorStringForProtocolLogger = "FolderNotFound2"
					};
				}
				this.collectionRequest.CollectionId = (StoreObjectId)mailboxSyncItemId.NativeId;
				AirSyncDiagnostics.TraceDebug<string, StoreObjectId>(ExTraceGlobals.RequestsTracer, this, "Received request with syncCollectionId {0}, which maps to collection Id {1}.", collectionId, this.collectionRequest.CollectionId);
			}
			string parentId = this.ParentId;
			if (parentId == "0")
			{
				this.collectionRequest.ParentId = mailboxSession.GetDefaultFolderId(DefaultFolderType.Root);
			}
			else if (parentId != null)
			{
				MailboxSyncItemId mailboxSyncItemId2 = ((FolderIdMapping)this.syncState[CustomStateDatumType.IdMapping])[parentId] as MailboxSyncItemId;
				if (mailboxSyncItemId2 == null)
				{
					throw new AirSyncPermanentException(StatusCode.Sync_ServerError, CollectionCommand.ConstructErrorXml(StatusCode.Sync_ServerError), null, false)
					{
						ErrorStringForProtocolLogger = "NoIdMappingForParentId"
					};
				}
				this.collectionRequest.ParentId = (StoreObjectId)mailboxSyncItemId2.NativeId;
				AirSyncDiagnostics.TraceDebug<string, StoreObjectId>(ExTraceGlobals.RequestsTracer, this, "Received request with syncParentId {0}, which maps to parent Id {1}.", parentId, this.collectionRequest.ParentId);
			}
			this.collectionRequest.CollectionName = this.CollectionName;
		}

		// Token: 0x04000314 RID: 788
		internal const string RootFolderId = "0";

		// Token: 0x04000315 RID: 789
		private CollectionCommand.CollectionRequestStruct collectionRequest;

		// Token: 0x04000316 RID: 790
		private CustomSyncState syncState;

		// Token: 0x02000047 RID: 71
		protected struct CollectionRequestStruct
		{
			// Token: 0x04000317 RID: 791
			internal StoreObjectId CollectionId;

			// Token: 0x04000318 RID: 792
			internal string CollectionName;

			// Token: 0x04000319 RID: 793
			internal StoreObjectId ParentId;
		}
	}
}
