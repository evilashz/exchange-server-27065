using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Net.WSTrust;
using Microsoft.Exchange.Services.Core.DataConverter;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x020002BE RID: 702
	internal sealed class CreateFolderPath : MultiStepServiceCommand<CreateFolderPathRequest, BaseFolderType>
	{
		// Token: 0x17000258 RID: 600
		// (get) Token: 0x06001304 RID: 4868 RVA: 0x0005CD58 File Offset: 0x0005AF58
		internal override bool SupportsExternalUsers
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000259 RID: 601
		// (get) Token: 0x06001305 RID: 4869 RVA: 0x0005CD5B File Offset: 0x0005AF5B
		internal override Offer ExpectedOffer
		{
			get
			{
				return Offer.SharingRead;
			}
		}

		// Token: 0x06001306 RID: 4870 RVA: 0x0005CD64 File Offset: 0x0005AF64
		internal override void PreExecuteCommand()
		{
			try
			{
				this.currentParentIdAndSession = base.IdConverter.ConvertTargetFolderIdToIdAndContentSession(this.parentFolderId.BaseFolderId, true);
			}
			catch (ObjectNotFoundException innerException)
			{
				throw new ParentFolderNotFoundException(innerException);
			}
		}

		// Token: 0x06001307 RID: 4871 RVA: 0x0005CDA8 File Offset: 0x0005AFA8
		public CreateFolderPath(CallContext callContext, CreateFolderPathRequest request) : base(callContext, request)
		{
			this.parentFolderId = base.Request.ParentFolderId;
			this.relativePath = base.Request.RelativeFolderPath;
			ServiceCommandBase.ThrowIfNull(this.parentFolderId, "this.parentFolderId", "CreateFolderPath::PreExecuteCommand");
			ServiceCommandBase.ThrowIfNullOrEmpty<BaseFolderType>(this.relativePath, "this.relativePath", "CreateFolderPath::PreExecuteCommand");
		}

		// Token: 0x06001308 RID: 4872 RVA: 0x0005CE18 File Offset: 0x0005B018
		internal override ServiceResult<BaseFolderType> Execute()
		{
			BaseFolderType baseFolderType = null;
			try
			{
				if (this.currentParentIdAndSession == null)
				{
					throw new ParentFolderNotFoundException();
				}
				baseFolderType = this.CreateFolderFromRequestFolder(this.currentParentIdAndSession, this.relativePath[base.CurrentStep]);
			}
			finally
			{
				if (baseFolderType != null)
				{
					this.currentParentIdAndSession = base.IdConverter.ConvertTargetFolderIdToIdAndContentSession(baseFolderType.FolderId, true);
				}
				else
				{
					this.currentParentIdAndSession = null;
				}
			}
			this.objectsChanged++;
			return new ServiceResult<BaseFolderType>(baseFolderType);
		}

		// Token: 0x06001309 RID: 4873 RVA: 0x0005CE9C File Offset: 0x0005B09C
		private static StoreObjectType GetStoreObjectType(object containerClassValue)
		{
			if (containerClassValue is PropertyError)
			{
				return StoreObjectType.Folder;
			}
			return ObjectClass.GetObjectType(containerClassValue as string);
		}

		// Token: 0x0600130A RID: 4874 RVA: 0x0005CEB4 File Offset: 0x0005B0B4
		private static Folder CreateOrGetFolderBasedOnStoreObjectType(IdAndSession parentIdAndSession, StoreObjectType storeObjectType, BaseFolderType folder)
		{
			Folder result;
			if (folder.DistinguishedFolderIdSpecified)
			{
				if (!IdConverter.IsDefaultFolderCreateSupported(folder.DistinguishedFolderId))
				{
					throw new ServiceInvalidOperationException(CoreResources.IDs.ErrorCreateDistinguishedFolder);
				}
				MailboxSession mailboxSession = parentIdAndSession.Session as MailboxSession;
				DefaultFolderType defaultFolderTypeFromDistinguishedFolderIdNameType = IdConverter.GetDefaultFolderTypeFromDistinguishedFolderIdNameType(folder.DistinguishedFolderId);
				StoreObjectId storeObjectId;
				if (DefaultFolderType.AdminAuditLogs == defaultFolderTypeFromDistinguishedFolderIdNameType)
				{
					storeObjectId = mailboxSession.GetAdminAuditLogsFolderId();
				}
				else
				{
					storeObjectId = mailboxSession.GetDefaultFolderId(defaultFolderTypeFromDistinguishedFolderIdNameType);
				}
				if (storeObjectId != null)
				{
					throw new ServiceInvalidOperationException(CoreResources.IDs.ErrorFolderExists);
				}
				StoreObjectId folderId = mailboxSession.CreateDefaultFolder(defaultFolderTypeFromDistinguishedFolderIdNameType);
				result = Folder.Bind(mailboxSession, folderId);
				folder.PropertyBag.Remove(BaseFolderSchema.DistinguishedFolderId);
				folder.PropertyBag.Remove(BaseFolderSchema.DisplayName);
			}
			else
			{
				VersionedId versionedId = null;
				using (Folder folder2 = Folder.Bind(parentIdAndSession.Session, parentIdAndSession.Id, null))
				{
					QueryFilter queryFilter = new ComparisonFilter(ComparisonOperator.Equal, FolderSchema.DisplayName, folder.DisplayName);
					using (QueryResult queryResult = folder2.FolderQuery(FolderQueryFlags.None, queryFilter, null, new PropertyDefinition[]
					{
						FolderSchema.DisplayName,
						FolderSchema.Id,
						StoreObjectSchema.ContainerClass
					}))
					{
						for (;;)
						{
							object[][] rows = queryResult.GetRows(10);
							if (rows.Length <= 0)
							{
								break;
							}
							for (int i = 0; i < rows.Length; i++)
							{
								if (CreateFolderPath.GetStoreObjectType(rows[i][2]) == storeObjectType)
								{
									versionedId = (VersionedId)rows[i][1];
									break;
								}
							}
						}
					}
				}
				if (versionedId != null)
				{
					result = Folder.Bind(parentIdAndSession.Session, versionedId.ObjectId);
				}
				else
				{
					result = Folder.Create(parentIdAndSession.Session, parentIdAndSession.Id, storeObjectType);
				}
			}
			return result;
		}

		// Token: 0x0600130B RID: 4875 RVA: 0x0005D06C File Offset: 0x0005B26C
		private BaseFolderType CreateFolderFromRequestFolder(IdAndSession parentIdAndSession, BaseFolderType folder)
		{
			BaseFolderType result = null;
			StoreObjectType storeObjectType = folder.StoreObjectType;
			this.ValidateCreate(storeObjectType, folder);
			using (Folder folder2 = CreateFolderPath.CreateOrGetFolderBasedOnStoreObjectType(parentIdAndSession, storeObjectType, folder))
			{
				result = this.UpdateNewFolder(parentIdAndSession, folder2, folder);
			}
			return result;
		}

		// Token: 0x0600130C RID: 4876 RVA: 0x0005D0BC File Offset: 0x0005B2BC
		private void ValidateCreate(StoreObjectType storeObjectType, BaseFolderType folder)
		{
			if (storeObjectType != StoreObjectType.Folder)
			{
				this.ConfirmNoFolderClassOverride(folder);
			}
		}

		// Token: 0x0600130D RID: 4877 RVA: 0x0005D0C9 File Offset: 0x0005B2C9
		private void ConfirmNoFolderClassOverride(BaseFolderType folder)
		{
			if (!string.IsNullOrEmpty(folder.FolderClass))
			{
				throw new NoFolderClassOverrideException();
			}
		}

		// Token: 0x1700025A RID: 602
		// (get) Token: 0x0600130E RID: 4878 RVA: 0x0005D0DE File Offset: 0x0005B2DE
		internal override int StepCount
		{
			get
			{
				return this.relativePath.Length;
			}
		}

		// Token: 0x0600130F RID: 4879 RVA: 0x0005D0E8 File Offset: 0x0005B2E8
		internal override IExchangeWebMethodResponse GetResponse()
		{
			CreateFolderPathResponse createFolderPathResponse = new CreateFolderPathResponse();
			createFolderPathResponse.BuildForResults<BaseFolderType>(base.Results);
			return createFolderPathResponse;
		}

		// Token: 0x06001310 RID: 4880 RVA: 0x0005D108 File Offset: 0x0005B308
		private BaseFolderType UpdateNewFolder(IdAndSession parentIdAndSession, Folder xsoFolder, BaseFolderType folder)
		{
			base.SetProperties(xsoFolder, folder);
			this.SaveXsoFolder(xsoFolder);
			folder.Clear();
			base.LoadServiceObject(folder, xsoFolder, parentIdAndSession, this.responseShape);
			return folder;
		}

		// Token: 0x04000D26 RID: 3366
		private TargetFolderId parentFolderId;

		// Token: 0x04000D27 RID: 3367
		private BaseFolderType[] relativePath;

		// Token: 0x04000D28 RID: 3368
		private IdAndSession currentParentIdAndSession;

		// Token: 0x04000D29 RID: 3369
		private ResponseShape responseShape = new ResponseShape(ShapeEnum.Default, null);
	}
}
