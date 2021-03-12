using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.Net.WSTrust;
using Microsoft.Exchange.Services.Core.DataConverter;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x0200030A RID: 778
	internal sealed class GetFolder : MultiStepServiceCommand<GetFolderRequest, BaseFolderType>
	{
		// Token: 0x170002BC RID: 700
		// (get) Token: 0x06001603 RID: 5635 RVA: 0x000722F4 File Offset: 0x000704F4
		internal override bool SupportsExternalUsers
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170002BD RID: 701
		// (get) Token: 0x06001604 RID: 5636 RVA: 0x000722F7 File Offset: 0x000704F7
		internal override Offer ExpectedOffer
		{
			get
			{
				return Offer.SharingRead;
			}
		}

		// Token: 0x06001605 RID: 5637 RVA: 0x00072300 File Offset: 0x00070500
		public GetFolder(CallContext callContext, GetFolderRequest request) : base(callContext, request)
		{
			this.folderIds = base.Request.Ids;
			this.responseShape = Global.ResponseShapeResolver.GetResponseShape<FolderResponseShape>(base.Request.ShapeName, base.Request.FolderShape, base.CallContext.FeaturesManager);
			ServiceCommandBase.ThrowIfNullOrEmpty<BaseFolderId>(this.folderIds, "this.folderIds", "GetFolder::PreExecuteCommand");
			ServiceCommandBase.ThrowIfNull(this.responseShape, "this.responseShape", "GetFolder::PreExecuteCommand");
		}

		// Token: 0x06001606 RID: 5638 RVA: 0x00072384 File Offset: 0x00070584
		internal override ServiceResult<BaseFolderType> Execute()
		{
			FaultInjection.GenerateFault((FaultInjection.LIDs)3116772669U);
			string text = null;
			int num = 0;
			ExTraceGlobals.FaultInjectionTracer.TraceTest<string>(4186320189U, ref text);
			if (!string.IsNullOrEmpty(text) && int.TryParse(text, out num) && num != 0)
			{
				throw new MapiExceptionMaxObjsExceeded("Fault injection MapiExceptionMaxObjsExceededInGetItem_ChangeValue", 0, num, null, null);
			}
			IdAndSession idAndSession = base.IdConverter.ConvertFolderIdToIdAndSession(this.folderIds[base.CurrentStep], IdConverter.ConvertOption.IgnoreChangeKey | IdConverter.ConvertOption.AllowKnownExternalUsers | (base.Request.IsHierarchicalOperation ? IdConverter.ConvertOption.IsHierarchicalOperation : IdConverter.ConvertOption.None));
			ServiceError serviceError;
			BaseFolderType folderObject = this.GetFolderObject(idAndSession, out serviceError);
			this.objectsChanged++;
			ServiceResult<BaseFolderType> result;
			if (serviceError == null)
			{
				result = new ServiceResult<BaseFolderType>(folderObject);
			}
			else
			{
				result = new ServiceResult<BaseFolderType>(folderObject, serviceError);
			}
			return result;
		}

		// Token: 0x06001607 RID: 5639 RVA: 0x00072438 File Offset: 0x00070638
		private BaseFolderType GetFolderObject(IdAndSession idAndSession, out ServiceError warning)
		{
			warning = null;
			FolderResponseShape folderResponseShape = this.responseShape;
			if (base.CallContext.IsExternalUser)
			{
				folderResponseShape = ExternalUserHandler.FilterFolderResponseShape(idAndSession as ExternalUserIdAndSession, this.responseShape, out warning);
			}
			ToServiceObjectPropertyList toServiceObjectPropertyList = XsoDataConverter.GetToServiceObjectPropertyList(idAndSession.Id, idAndSession.Session, folderResponseShape, base.ParticipantResolver);
			BaseFolderType result;
			using (Folder xsoFolder = ServiceCommandBase.GetXsoFolder(idAndSession.Session, idAndSession.Id, ref toServiceObjectPropertyList))
			{
				StoreObjectId storeObjectId = StoreId.GetStoreObjectId(idAndSession.Id);
				if (idAndSession.Session is PublicFolderSession && ClientInfo.OWA.IsMatch(idAndSession.Session.ClientInfoString))
				{
					toServiceObjectPropertyList.CommandOptions |= CommandOptions.ConvertFolderIdToPublicFolderId;
					toServiceObjectPropertyList.CommandOptions |= CommandOptions.ConvertParentFolderIdToPublicFolderId;
				}
				BaseFolderType baseFolderType = BaseFolderType.CreateFromStoreObjectType(storeObjectId.ObjectType);
				ServiceCommandBase.LoadServiceObject(baseFolderType, xsoFolder, idAndSession, this.responseShape, toServiceObjectPropertyList);
				result = baseFolderType;
			}
			return result;
		}

		// Token: 0x170002BE RID: 702
		// (get) Token: 0x06001608 RID: 5640 RVA: 0x00072528 File Offset: 0x00070728
		internal override int StepCount
		{
			get
			{
				return this.folderIds.Count;
			}
		}

		// Token: 0x06001609 RID: 5641 RVA: 0x00072538 File Offset: 0x00070738
		internal override IExchangeWebMethodResponse GetResponse()
		{
			GetFolderResponse getFolderResponse = new GetFolderResponse();
			getFolderResponse.BuildForResults<BaseFolderType>(base.Results);
			return getFolderResponse;
		}

		// Token: 0x04000ED0 RID: 3792
		private IList<BaseFolderId> folderIds;

		// Token: 0x04000ED1 RID: 3793
		private FolderResponseShape responseShape;
	}
}
