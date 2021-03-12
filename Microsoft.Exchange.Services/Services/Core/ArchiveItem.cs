using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Principal;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.SoapWebClient.EWS;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x020002A0 RID: 672
	internal sealed class ArchiveItem : MultiStepServiceCommand<ArchiveItemRequest, Microsoft.Exchange.Services.Core.Types.ItemType>
	{
		// Token: 0x060011D6 RID: 4566 RVA: 0x00056EBD File Offset: 0x000550BD
		public ArchiveItem(CallContext callContext, ArchiveItemRequest request) : base(callContext, request)
		{
			this.objectIds = request.Ids;
			this.sourceFolderId = request.SourceFolderId.BaseFolderId;
		}

		// Token: 0x17000236 RID: 566
		// (get) Token: 0x060011D7 RID: 4567 RVA: 0x00056EE4 File Offset: 0x000550E4
		internal override int StepCount
		{
			get
			{
				return this.objectIds.Length;
			}
		}

		// Token: 0x060011D8 RID: 4568 RVA: 0x00056EF0 File Offset: 0x000550F0
		internal override IExchangeWebMethodResponse GetResponse()
		{
			ArchiveItemResponse archiveItemResponse = new ArchiveItemResponse();
			archiveItemResponse.BuildForResults<Microsoft.Exchange.Services.Core.Types.ItemType>(base.Results);
			return archiveItemResponse;
		}

		// Token: 0x060011D9 RID: 4569 RVA: 0x00056F10 File Offset: 0x00055110
		internal override void PreExecuteCommand()
		{
			this.ValidateItems();
			this.destinationFolderId = this.GetArchiveDestinationFolder();
			ServiceCommandBase serviceCommand = new MoveItemRequest
			{
				ToFolderId = new TargetFolderId(this.destinationFolderId),
				Ids = this.objectIds,
				ReturnNewItemIds = true
			}.GetServiceCommand(base.CallContext);
			if (serviceCommand is MoveItem)
			{
				this.moveItemCommand = (serviceCommand as MoveItem);
				this.moveItemCommand.PreExecuteCommand();
				return;
			}
			this.moveItemBatchCommand = (serviceCommand as MoveItemBatch);
			this.moveItemBatchCommand.PreExecuteCommand();
		}

		// Token: 0x060011DA RID: 4570 RVA: 0x00056FA0 File Offset: 0x000551A0
		internal override ServiceResult<Microsoft.Exchange.Services.Core.Types.ItemType> Execute()
		{
			if (this.moveItemCommand != null)
			{
				this.moveItemCommand.CurrentStep = base.CurrentStep;
				return this.moveItemCommand.Execute();
			}
			this.moveItemBatchCommand.CurrentStep = base.CurrentStep;
			return this.moveItemBatchCommand.Execute();
		}

		// Token: 0x060011DB RID: 4571 RVA: 0x00056FEE File Offset: 0x000551EE
		internal override void PostExecuteCommand()
		{
			if (this.moveItemCommand != null)
			{
				this.moveItemCommand.PostExecuteCommand();
				return;
			}
			this.moveItemBatchCommand.PostExecuteCommand();
		}

		// Token: 0x060011DC RID: 4572 RVA: 0x00057010 File Offset: 0x00055210
		private static DistinguishedFolderId MapDefaultFolderTypeToDistinguishedFolderId(DefaultFolderType defaultFolderType)
		{
			ArchiveItem.ValidateDefaultFolderType(defaultFolderType);
			DistinguishedFolderId distinguishedFolderId = new DistinguishedFolderId();
			if (defaultFolderType == DefaultFolderType.Inbox)
			{
				distinguishedFolderId.Id = DistinguishedFolderIdName.inbox;
				return distinguishedFolderId;
			}
			if (defaultFolderType == DefaultFolderType.Root)
			{
				distinguishedFolderId.Id = DistinguishedFolderIdName.root;
				return distinguishedFolderId;
			}
			if (defaultFolderType == DefaultFolderType.Drafts)
			{
				distinguishedFolderId.Id = DistinguishedFolderIdName.drafts;
				return distinguishedFolderId;
			}
			if (defaultFolderType == DefaultFolderType.DeletedItems)
			{
				distinguishedFolderId.Id = DistinguishedFolderIdName.deleteditems;
				return distinguishedFolderId;
			}
			if (defaultFolderType == DefaultFolderType.SentItems)
			{
				distinguishedFolderId.Id = DistinguishedFolderIdName.sentitems;
				return distinguishedFolderId;
			}
			if (defaultFolderType == DefaultFolderType.CommunicatorHistory)
			{
				distinguishedFolderId.Id = DistinguishedFolderIdName.conversationhistory;
			}
			return null;
		}

		// Token: 0x060011DD RID: 4573 RVA: 0x0005707C File Offset: 0x0005527C
		private static void ValidateDefaultFolderType(DefaultFolderType defaultFolderType)
		{
			if (defaultFolderType != DefaultFolderType.None && defaultFolderType != DefaultFolderType.Inbox && defaultFolderType != DefaultFolderType.Root && defaultFolderType != DefaultFolderType.Drafts && defaultFolderType != DefaultFolderType.DeletedItems && defaultFolderType != DefaultFolderType.SentItems && defaultFolderType != DefaultFolderType.CommunicatorHistory)
			{
				throw new ServiceInvalidOperationException((CoreResources.IDs)2786380669U);
			}
		}

		// Token: 0x060011DE RID: 4574 RVA: 0x000570AC File Offset: 0x000552AC
		private static bool IsFolderDistinguished(IdConverter idConverter, BaseFolderId folderId, out DistinguishedFolderId archiveFolder)
		{
			archiveFolder = null;
			IdAndSession idAndSession = idConverter.ConvertFolderIdToIdAndSession(folderId, IdConverter.ConvertOption.IgnoreChangeKey);
			MailboxSession mailboxSession = idAndSession.Session as MailboxSession;
			DefaultFolderType defaultFolderType = mailboxSession.IsDefaultFolderType(idAndSession.Id);
			if (defaultFolderType == DefaultFolderType.Root)
			{
				archiveFolder = new DistinguishedFolderId();
				archiveFolder.Id = DistinguishedFolderIdName.archivemsgfolderroot;
				return true;
			}
			if (defaultFolderType == DefaultFolderType.DeletedItems)
			{
				archiveFolder = new DistinguishedFolderId();
				archiveFolder.Id = DistinguishedFolderIdName.deleteditems;
				return true;
			}
			ArchiveItem.ValidateDefaultFolderType(defaultFolderType);
			return false;
		}

		// Token: 0x060011DF RID: 4575 RVA: 0x00057114 File Offset: 0x00055314
		private static DistinguishedFolderId GetArchiveDistinguishedFolder(DistinguishedFolderId primaryFolderId)
		{
			DistinguishedFolderId distinguishedFolderId = new DistinguishedFolderId();
			if (primaryFolderId.Id == DistinguishedFolderIdName.root)
			{
				distinguishedFolderId.Id = DistinguishedFolderIdName.archivemsgfolderroot;
				return distinguishedFolderId;
			}
			if (primaryFolderId.Id == DistinguishedFolderIdName.deleteditems)
			{
				distinguishedFolderId.Id = DistinguishedFolderIdName.archivedeleteditems;
				return distinguishedFolderId;
			}
			return null;
		}

		// Token: 0x060011E0 RID: 4576 RVA: 0x0005714F File Offset: 0x0005534F
		private static StoreObjectType GetFolderStoreObjectType(object containerClassValue)
		{
			if (containerClassValue is PropertyError)
			{
				return StoreObjectType.Folder;
			}
			return ObjectClass.GetObjectType(containerClassValue as string);
		}

		// Token: 0x060011E1 RID: 4577 RVA: 0x00057168 File Offset: 0x00055368
		private CreateFolderPathRequest GetFolderPathRequest()
		{
			List<Microsoft.Exchange.Services.Core.Types.BaseFolderType> list = new List<Microsoft.Exchange.Services.Core.Types.BaseFolderType>();
			BaseFolderId baseFolderId = this.sourceFolderId;
			for (;;)
			{
				IdAndSession idAndSession = base.IdConverter.ConvertFolderIdToIdAndSession(baseFolderId, IdConverter.ConvertOption.IgnoreChangeKey);
				MailboxSession mailboxSession = idAndSession.Session as MailboxSession;
				using (Folder folder = Folder.Bind(mailboxSession, idAndSession.Id, new PropertyDefinition[]
				{
					StoreObjectSchema.ParentItemId,
					FolderSchema.DisplayName,
					StoreObjectSchema.ContainerClass
				}))
				{
					object obj = folder.TryGetProperty(StoreObjectSchema.ParentItemId);
					StoreId storeId = obj as StoreId;
					if (storeId == null)
					{
						throw new InvalidFolderIdException((CoreResources.IDs)2940401781U);
					}
					object containerClassValue = folder.TryGetProperty(StoreObjectSchema.ContainerClass);
					StoreObjectType folderStoreObjectType = ArchiveItem.GetFolderStoreObjectType(containerClassValue);
					if (folderStoreObjectType == StoreObjectType.SearchFolder || folderStoreObjectType == StoreObjectType.OutlookSearchFolder)
					{
						throw new InvalidFolderTypeForOperationException((CoreResources.IDs)3848937923U);
					}
					Microsoft.Exchange.Services.Core.Types.BaseFolderType baseFolderType = Microsoft.Exchange.Services.Core.Types.BaseFolderType.CreateFromStoreObjectType(folderStoreObjectType);
					baseFolderType.DisplayName = folder.DisplayName;
					list.Insert(0, baseFolderType);
					DefaultFolderType defaultFolderType = mailboxSession.IsDefaultFolderType(storeId);
					BaseFolderId baseFolderId2;
					if (defaultFolderType == DefaultFolderType.None)
					{
						baseFolderId2 = IdConverter.GetFolderIdFromStoreId(storeId, new MailboxId(mailboxSession));
					}
					else
					{
						baseFolderId2 = ArchiveItem.MapDefaultFolderTypeToDistinguishedFolderId(defaultFolderType);
					}
					baseFolderId = baseFolderId2;
					if (defaultFolderType != DefaultFolderType.Root && defaultFolderType != DefaultFolderType.DeletedItems)
					{
						continue;
					}
				}
				break;
			}
			return new CreateFolderPathRequest
			{
				ParentFolderId = new TargetFolderId(),
				ParentFolderId = 
				{
					BaseFolderId = ArchiveItem.GetArchiveDistinguishedFolder(baseFolderId as DistinguishedFolderId)
				},
				RelativeFolderPath = list.ToArray()
			};
		}

		// Token: 0x060011E2 RID: 4578 RVA: 0x000572E0 File Offset: 0x000554E0
		private BaseFolderId GetLocalArchiveDestinationFolder(CreateFolderPathRequest request)
		{
			CreateFolderPath createFolderPath = new CreateFolderPath(base.CallContext, request);
			List<Microsoft.Exchange.Services.Core.Types.BaseFolderType> list = new List<Microsoft.Exchange.Services.Core.Types.BaseFolderType>();
			createFolderPath.PreExecuteCommand();
			for (int i = 0; i < createFolderPath.StepCount; i++)
			{
				ServiceResult<Microsoft.Exchange.Services.Core.Types.BaseFolderType> serviceResult = createFolderPath.Execute();
				if (serviceResult.Error != null)
				{
					throw new ArchiveItemException((CoreResources.IDs)2565659540U);
				}
				list.Add(serviceResult.Value);
				createFolderPath.CurrentStep++;
			}
			createFolderPath.PostExecuteCommand();
			return list.Last<Microsoft.Exchange.Services.Core.Types.BaseFolderType>().FolderId;
		}

		// Token: 0x060011E3 RID: 4579 RVA: 0x00057364 File Offset: 0x00055564
		private void ValidateItems()
		{
			IdConverter idConverter = new IdConverter(base.CallContext);
			Guid? guid = null;
			foreach (BaseItemId itemId in this.objectIds)
			{
				StoreId storeId;
				Guid value;
				bool flag = idConverter.TryGetStoreIdAndMailboxGuidFromItemId(itemId, out storeId, out value);
				if (guid == null)
				{
					guid = new Guid?(value);
				}
				if (!flag || !value.Equals(guid.Value))
				{
					throw new InvalidItemForOperationException(typeof(ArchiveItem).Name);
				}
			}
			if (!idConverter.GetMailboxGuidFromFolderId(this.sourceFolderId).Equals(guid.Value))
			{
				throw new InvalidItemForOperationException(typeof(ArchiveItem).Name);
			}
			IdAndSession idAndSession = idConverter.ConvertFolderIdToIdAndSession(this.sourceFolderId, IdConverter.ConvertOption.IgnoreChangeKey);
			MailboxSession mailboxSession = idAndSession.Session as MailboxSession;
			if (mailboxSession.MailboxOwner.MailboxInfo.IsArchive)
			{
				throw new ServiceInvalidOperationException((CoreResources.IDs)2225772284U);
			}
		}

		// Token: 0x060011E4 RID: 4580 RVA: 0x00057484 File Offset: 0x00055684
		private BaseFolderId GetRemoteArchiveDestinationFolder(IMailboxInfo archiveMailbox, CreateFolderPathRequest request)
		{
			string text = EwsClientHelper.DiscoverEwsUrl(archiveMailbox);
			if (string.IsNullOrEmpty(text))
			{
				throw new ArchiveItemException((CoreResources.IDs)3156121664U);
			}
			ExchangeServiceBinding serviceBinding = EwsClientHelper.CreateBinding(base.CallContext.EffectiveCaller, text);
			CreateFolderPathType ewsClientRequest = EwsClientHelper.Convert<CreateFolderPathRequest, CreateFolderPathType>(request);
			Exception ex = null;
			CreateFolderPathResponseType ewsClientCreateFolderPathResponse = null;
			bool flag = EwsClientHelper.ExecuteEwsCall(delegate
			{
				ewsClientCreateFolderPathResponse = serviceBinding.CreateFolderPath(ewsClientRequest);
			}, out ex);
			if (!flag)
			{
				throw new ArchiveItemException((CoreResources.IDs)2565659540U);
			}
			CreateFolderPathResponse createFolderPathResponse = EwsClientHelper.Convert<CreateFolderPathResponseType, CreateFolderPathResponse>(ewsClientCreateFolderPathResponse);
			ResponseMessage[] items = createFolderPathResponse.ResponseMessages.Items;
			ResponseMessage responseMessage = items.Last<ResponseMessage>();
			if (responseMessage.ResponseClass == ResponseClass.Success)
			{
				return ((FolderInfoResponseMessage)responseMessage).Folders[0].FolderId;
			}
			throw new ArchiveItemException((CoreResources.IDs)2565659540U);
		}

		// Token: 0x060011E5 RID: 4581 RVA: 0x00057560 File Offset: 0x00055760
		private BaseFolderId GetArchiveDestinationFolder()
		{
			IdAndSession idAndSession = base.IdConverter.ConvertFolderIdToIdAndSession(this.sourceFolderId, IdConverter.ConvertOption.IgnoreChangeKey);
			MailboxSession mailboxSession = idAndSession.Session as MailboxSession;
			if (mailboxSession == null)
			{
				throw new ServiceInvalidOperationException((CoreResources.IDs)3558192788U);
			}
			DistinguishedFolderId result = null;
			if (ArchiveItem.IsFolderDistinguished(base.IdConverter, this.sourceFolderId, out result))
			{
				return result;
			}
			CreateFolderPathRequest folderPathRequest = this.GetFolderPathRequest();
			IMailboxInfo archiveMailbox = mailboxSession.MailboxOwner.GetArchiveMailbox();
			switch ((archiveMailbox != null) ? archiveMailbox.ArchiveState : ArchiveState.None)
			{
			case ArchiveState.None:
				throw new ServiceInvalidOperationException(CoreResources.IDs.ErrorArchiveMailboxNotEnabled);
			case ArchiveState.Local:
				if (archiveMailbox != null && mailboxSession.MailboxOwner.MailboxInfo.Location.ServerFqdn != archiveMailbox.Location.ServerFqdn)
				{
					return this.GetRemoteArchiveDestinationFolder(archiveMailbox, folderPathRequest);
				}
				return this.GetLocalArchiveDestinationFolder(folderPathRequest);
			default:
				throw new NotSupportedException();
			}
		}

		// Token: 0x04000CD4 RID: 3284
		private BaseItemId[] objectIds;

		// Token: 0x04000CD5 RID: 3285
		private BaseFolderId sourceFolderId;

		// Token: 0x04000CD6 RID: 3286
		private BaseFolderId destinationFolderId;

		// Token: 0x04000CD7 RID: 3287
		private MoveItemBatch moveItemBatchCommand;

		// Token: 0x04000CD8 RID: 3288
		private MoveItem moveItemCommand;
	}
}
