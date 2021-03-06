using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Services.Core.Search;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.ExchangeService;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000EAA RID: 3754
	internal class FolderProvider : ExchangeServiceProvider
	{
		// Token: 0x060061BF RID: 25023 RVA: 0x0013104F File Offset: 0x0012F24F
		public FolderProvider(IExchangeService exchangeService) : base(exchangeService)
		{
		}

		// Token: 0x060061C0 RID: 25024 RVA: 0x00131058 File Offset: 0x0012F258
		public static Folder FolderTypeToEntity(FolderType folderType, IList<PropertyDefinition> properties)
		{
			ArgumentValidator.ThrowIfNull("folderType", folderType);
			ArgumentValidator.ThrowIfNull("properties", properties);
			Folder folder = EwsServiceObjectFactory.CreateEntity<Folder>(folderType);
			foreach (PropertyDefinition propertyDefinition in properties)
			{
				propertyDefinition.EwsPropertyProvider.GetPropertyFromDataSource(folder, propertyDefinition, folderType);
			}
			return folder;
		}

		// Token: 0x060061C1 RID: 25025 RVA: 0x001310C8 File Offset: 0x0012F2C8
		public Folder Create(string parentFolderId, Folder template)
		{
			ArgumentValidator.ThrowIfNullOrEmpty("parentFolderId", parentFolderId);
			ArgumentValidator.ThrowIfNull("template", template);
			CreateFolderRequest createFolderRequest = new CreateFolderRequest();
			createFolderRequest.ParentFolderId = new TargetFolderId(EwsIdConverter.CreateFolderIdFromEwsId(EwsIdConverter.ODataIdToEwsId(parentFolderId)));
			FolderType folderType = EwsServiceObjectFactory.CreateServiceObject<FolderType>(template);
			foreach (PropertyDefinition propertyDefinition in template.PropertyBag.GetProperties())
			{
				if (propertyDefinition.Flags.HasFlag(PropertyDefinitionFlags.CanCreate))
				{
					propertyDefinition.EwsPropertyProvider.SetPropertyToDataSource(template, propertyDefinition, folderType);
				}
			}
			createFolderRequest.Folders = new BaseFolderType[]
			{
				folderType
			};
			CreateFolderResponse createFolderResponse = base.ExchangeService.CreateFolder(createFolderRequest, null);
			FolderInfoResponseMessage folderInfoResponseMessage = createFolderResponse.ResponseMessages.Items[0] as FolderInfoResponseMessage;
			FolderType folderType2 = folderInfoResponseMessage.Folders[0] as FolderType;
			return this.InternalRead(folderType2.FolderId.Id, null);
		}

		// Token: 0x060061C2 RID: 25026 RVA: 0x001311B8 File Offset: 0x0012F3B8
		public Folder Read(string id, FolderQueryAdapter queryAdapter = null)
		{
			ArgumentValidator.ThrowIfNullOrEmpty("id", id);
			string ewsId = EwsIdConverter.ODataIdToEwsId(id);
			return this.InternalRead(ewsId, queryAdapter);
		}

		// Token: 0x060061C3 RID: 25027 RVA: 0x001311E0 File Offset: 0x0012F3E0
		public void Delete(string id)
		{
			ArgumentValidator.ThrowIfNullOrEmpty("id", id);
			string id2 = EwsIdConverter.ODataIdToEwsId(id);
			BaseFolderId baseFolderId = EwsIdConverter.CreateFolderIdFromEwsId(id2);
			DeleteFolderRequest deleteFolderRequest = new DeleteFolderRequest();
			deleteFolderRequest.DeleteType = DisposalType.SoftDelete;
			deleteFolderRequest.Ids = new BaseFolderId[]
			{
				baseFolderId
			};
			base.ExchangeService.DeleteFolder(deleteFolderRequest, null);
		}

		// Token: 0x060061C4 RID: 25028 RVA: 0x00131234 File Offset: 0x0012F434
		public Folder Update(string id, Folder changeEntity)
		{
			ArgumentValidator.ThrowIfNullOrEmpty("id", id);
			ArgumentValidator.ThrowIfNull("changeEntity", changeEntity);
			string id2 = EwsIdConverter.ODataIdToEwsId(id);
			UpdateFolderRequest updateFolderRequest = new UpdateFolderRequest();
			FolderChange folderChange = new FolderChange();
			folderChange.FolderId = EwsIdConverter.CreateFolderIdFromEwsId(id2);
			updateFolderRequest.FolderChanges = new FolderChange[]
			{
				folderChange
			};
			List<PropertyUpdate> list = new List<PropertyUpdate>();
			foreach (PropertyDefinition propertyDefinition in changeEntity.PropertyBag.GetProperties())
			{
				if (propertyDefinition.Flags.HasFlag(PropertyDefinitionFlags.CanUpdate))
				{
					EwsPropertyProvider ewsPropertyProvider = propertyDefinition.EwsPropertyProvider.GetEwsPropertyProvider(changeEntity.Schema);
					FolderType folderType = EwsServiceObjectFactory.CreateServiceObject<FolderType>(changeEntity);
					ewsPropertyProvider.SetPropertyToDataSource(changeEntity, propertyDefinition, folderType);
					PropertyUpdate propertyUpdate = ewsPropertyProvider.GetPropertyUpdate(folderType, changeEntity[propertyDefinition]);
					list.Add(propertyUpdate);
				}
			}
			folderChange.PropertyUpdates = list.ToArray();
			base.ExchangeService.UpdateFolder(updateFolderRequest, null);
			return this.Read(id, null);
		}

		// Token: 0x060061C5 RID: 25029 RVA: 0x00131338 File Offset: 0x0012F538
		public IFindEntitiesResult<Folder> Find(string parentFolderId, FolderQueryAdapter queryAdapter = null)
		{
			ArgumentValidator.ThrowIfNullOrEmpty("parentFolderId", parentFolderId);
			string id = EwsIdConverter.ODataIdToEwsId(parentFolderId);
			BaseFolderId baseFolderId = EwsIdConverter.CreateFolderIdFromEwsId(id);
			queryAdapter = (queryAdapter ?? FolderQueryAdapter.Default);
			FindFolderRequest findFolderRequest = new FindFolderRequest();
			findFolderRequest.ParentFolderIds = new BaseFolderId[]
			{
				baseFolderId
			};
			findFolderRequest.Traversal = FolderQueryTraversal.Shallow;
			findFolderRequest.Paging = queryAdapter.GetPaging();
			findFolderRequest.Restriction = queryAdapter.GetRestriction();
			findFolderRequest.FolderShape = queryAdapter.GetResponseShape();
			FindFolderResponse findFolderResponse = base.ExchangeService.FindFolder(findFolderRequest, null);
			FindFolderResponseMessage findFolderResponseMessage = findFolderResponse.ResponseMessages.Items[0] as FindFolderResponseMessage;
			List<Folder> list = new List<Folder>();
			for (int i = 0; i < findFolderResponseMessage.RootFolder.Folders.Length; i++)
			{
				FolderType folderType = findFolderResponseMessage.RootFolder.Folders[i] as FolderType;
				if (folderType != null)
				{
					Folder item = FolderProvider.FolderTypeToEntity(folderType, queryAdapter.RequestedProperties);
					list.Add(item);
				}
			}
			return new FindEntitiesResult<Folder>(list, findFolderResponseMessage.RootFolder.TotalItemsInView);
		}

		// Token: 0x060061C6 RID: 25030 RVA: 0x0013143C File Offset: 0x0012F63C
		public Folder Copy(string id, string destinationId)
		{
			ArgumentValidator.ThrowIfNullOrEmpty("id", id);
			ArgumentValidator.ThrowIfNullOrEmpty("destinationId", destinationId);
			string id2 = EwsIdConverter.ODataIdToEwsId(id);
			BaseFolderId baseFolderId = EwsIdConverter.CreateFolderIdFromEwsId(id2);
			string id3 = EwsIdConverter.ODataIdToEwsId(destinationId);
			BaseFolderId baseFolderId2 = EwsIdConverter.CreateFolderIdFromEwsId(id3);
			CopyFolderRequest copyFolderRequest = new CopyFolderRequest();
			copyFolderRequest.Ids = new BaseFolderId[]
			{
				baseFolderId
			};
			copyFolderRequest.ToFolderId = new TargetFolderId(baseFolderId2);
			CopyFolderResponse copyFolderResponse = base.ExchangeService.CopyFolder(copyFolderRequest, null);
			FolderInfoResponseMessage folderInfoResponseMessage = copyFolderResponse.ResponseMessages.Items[0] as FolderInfoResponseMessage;
			FolderType folderType = folderInfoResponseMessage.Folders[0] as FolderType;
			return this.InternalRead(folderType.FolderId.Id, null);
		}

		// Token: 0x060061C7 RID: 25031 RVA: 0x001314F4 File Offset: 0x0012F6F4
		public Folder Move(string id, string destinationId)
		{
			ArgumentValidator.ThrowIfNullOrEmpty("id", id);
			ArgumentValidator.ThrowIfNullOrEmpty("destinationId", destinationId);
			string id2 = EwsIdConverter.ODataIdToEwsId(id);
			BaseFolderId baseFolderId = EwsIdConverter.CreateFolderIdFromEwsId(id2);
			string id3 = EwsIdConverter.ODataIdToEwsId(destinationId);
			BaseFolderId baseFolderId2 = EwsIdConverter.CreateFolderIdFromEwsId(id3);
			MoveFolderRequest moveFolderRequest = new MoveFolderRequest();
			moveFolderRequest.Ids = new BaseFolderId[]
			{
				baseFolderId
			};
			moveFolderRequest.ToFolderId = new TargetFolderId(baseFolderId2);
			MoveFolderResponse moveFolderResponse = base.ExchangeService.MoveFolder(moveFolderRequest, null);
			FolderInfoResponseMessage folderInfoResponseMessage = moveFolderResponse.ResponseMessages.Items[0] as FolderInfoResponseMessage;
			FolderType folderType = folderInfoResponseMessage.Folders[0] as FolderType;
			return this.InternalRead(folderType.FolderId.Id, null);
		}

		// Token: 0x060061C8 RID: 25032 RVA: 0x001315AC File Offset: 0x0012F7AC
		private Folder InternalRead(string ewsId, FolderQueryAdapter queryAdapter = null)
		{
			ArgumentValidator.ThrowIfNullOrEmpty("ewsId", ewsId);
			BaseFolderId baseFolderId = EwsIdConverter.CreateFolderIdFromEwsId(ewsId);
			queryAdapter = (queryAdapter ?? FolderQueryAdapter.Default);
			GetFolderRequest request = new GetFolderRequest
			{
				Ids = new BaseFolderId[]
				{
					baseFolderId
				},
				FolderShape = queryAdapter.GetResponseShape()
			};
			GetFolderResponse folder = base.ExchangeService.GetFolder(request, null);
			FolderInfoResponseMessage folderInfoResponseMessage = folder.ResponseMessages.Items[0] as FolderInfoResponseMessage;
			FolderType folderType = folderInfoResponseMessage.Folders[0] as FolderType;
			return FolderProvider.FolderTypeToEntity(folderType, queryAdapter.RequestedProperties);
		}
	}
}
