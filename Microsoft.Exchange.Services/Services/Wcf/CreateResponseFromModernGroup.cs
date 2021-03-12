﻿using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.Services.Core;
using Microsoft.Exchange.Services.Core.DataConverter;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Diagnostics;
using Microsoft.Exchange.Services.Wcf.Types;
using Microsoft.Exchange.SoapWebClient.EWS;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x0200090F RID: 2319
	internal sealed class CreateResponseFromModernGroup : MultiStepServiceCommand<CreateResponseFromModernGroupRequest, Microsoft.Exchange.Services.Core.Types.ItemType[]>
	{
		// Token: 0x06004332 RID: 17202 RVA: 0x000E12AF File Offset: 0x000DF4AF
		public CreateResponseFromModernGroup(CallContext callContext, CreateResponseFromModernGroupRequest request) : base(callContext, request)
		{
			OwsLogRegistry.Register(CreateResponseFromModernGroup.CreateResponseFromModernGroupActionName, typeof(CreateAndUpdateItemMetadata), new Type[0]);
		}

		// Token: 0x17000F73 RID: 3955
		// (get) Token: 0x06004333 RID: 17203 RVA: 0x000E12DF File Offset: 0x000DF4DF
		internal override int StepCount
		{
			get
			{
				return this.items.Length;
			}
		}

		// Token: 0x06004334 RID: 17204 RVA: 0x000E12EC File Offset: 0x000DF4EC
		internal override void PreExecuteCommand()
		{
			this.items = base.Request.Items.Items;
			if (!string.IsNullOrEmpty(base.Request.MessageDisposition))
			{
				this.messageDisposition = new Microsoft.Exchange.Services.Core.Types.MessageDispositionType?(MessageDisposition.ConvertToEnum(base.Request.MessageDisposition));
				RequestDetailsLoggerBase<RequestDetailsLogger>.SafeSetLogger(RequestDetailsLogger.Current, CreateAndUpdateItemMetadata.MessageDisposition, this.messageDisposition);
			}
			if (!string.IsNullOrEmpty(base.Request.ComposeOperation))
			{
				RequestDetailsLoggerBase<RequestDetailsLogger>.SafeSetLogger(RequestDetailsLogger.Current, CreateAndUpdateItemMetadata.ComposeOperation, base.Request.ComposeOperation);
			}
		}

		// Token: 0x06004335 RID: 17205 RVA: 0x000E1388 File Offset: 0x000DF588
		internal override ServiceResult<Microsoft.Exchange.Services.Core.Types.ItemType[]> Execute()
		{
			ServiceError serviceError;
			ServiceResult<Microsoft.Exchange.Services.Core.Types.ItemType[]> result = this.CreateResponseFromServiceObject(this.items[base.CurrentStep], out serviceError);
			this.objectsChanged++;
			if (serviceError == null)
			{
				return result;
			}
			return new ServiceResult<Microsoft.Exchange.Services.Core.Types.ItemType[]>(serviceError);
		}

		// Token: 0x06004336 RID: 17206 RVA: 0x000E13C4 File Offset: 0x000DF5C4
		internal override IExchangeWebMethodResponse GetResponse()
		{
			CreateResponseFromModernGroupResponse createResponseFromModernGroupResponse = new CreateResponseFromModernGroupResponse();
			createResponseFromModernGroupResponse.BuildForResults<Microsoft.Exchange.Services.Core.Types.ItemType[]>(base.Results);
			return createResponseFromModernGroupResponse;
		}

		// Token: 0x06004337 RID: 17207 RVA: 0x000E13E4 File Offset: 0x000DF5E4
		internal ServiceResult<Microsoft.Exchange.Services.Core.Types.ItemType[]> CreateResponseFromServiceObject(Microsoft.Exchange.Services.Core.Types.ItemType item, out ServiceError error)
		{
			error = null;
			if (base.Request.ItemShape != null || base.Request.ShapeName != null)
			{
				this.responseShape = Global.ResponseShapeResolver.GetResponseShape<ItemResponseShape>(base.Request.ShapeName, base.Request.ItemShape, base.CallContext.FeaturesManager);
			}
			else if (ExchangeVersion.Current.Supports(ExchangeVersion.Exchange2010SP2) && item.Attachments != null && item.Attachments.Length > 0)
			{
				this.responseShape = ServiceCommandBase.DefaultItemResponseShapeWithAttachments;
			}
			else
			{
				this.responseShape = ServiceCommandBase.DefaultItemResponseShape;
			}
			if (item is Microsoft.Exchange.Services.Core.Types.SmartResponseType)
			{
				Microsoft.Exchange.Services.Core.Types.SmartResponseType smartResponse = (Microsoft.Exchange.Services.Core.Types.SmartResponseType)item;
				return this.ExecuteForwardReplyFromModernGroup(smartResponse, out error);
			}
			throw new InvalidRequestException((CoreResources.IDs)4004906780U);
		}

		// Token: 0x06004338 RID: 17208 RVA: 0x000E14AC File Offset: 0x000DF6AC
		private ServiceResult<Microsoft.Exchange.Services.Core.Types.ItemType[]> ExecuteForwardReplyFromModernGroup(Microsoft.Exchange.Services.Core.Types.SmartResponseType smartResponse, out ServiceError serviceError)
		{
			serviceError = null;
			ServiceResult<Microsoft.Exchange.Services.Core.Types.ItemType[]> result = null;
			ServiceResult<Microsoft.Exchange.Services.Core.Types.MimeContentType> serviceResult = this.RemoteGetReferenceItem(smartResponse);
			if (serviceResult.Code != ServiceResultCode.Error)
			{
				Microsoft.Exchange.Services.Core.Types.ItemId referenceItemId = this.SaveReferenceItemFromMimeContent(serviceResult.Value);
				smartResponse.ReferenceItemId = referenceItemId;
				result = this.CreateResponseFromReferenceItem(smartResponse);
				this.DeleteReferenceItem(referenceItemId);
				return result;
			}
			serviceError = serviceResult.Error;
			return result;
		}

		// Token: 0x06004339 RID: 17209 RVA: 0x000E1500 File Offset: 0x000DF700
		private ExchangeServiceBinding GetServiceBinding(GetItemRequest getItemRequest)
		{
			BaseServerIdInfo proxyInfo = getItemRequest.GetProxyInfo(base.CallContext);
			string url = EwsClientHelper.DiscoverEwsUrl(proxyInfo);
			return EwsClientHelper.CreateBinding(base.CallContext.EffectiveCaller, url);
		}

		// Token: 0x0600433A RID: 17210 RVA: 0x000E1534 File Offset: 0x000DF734
		private Microsoft.Exchange.Services.Core.Types.ItemId SaveReferenceItemFromMimeContent(Microsoft.Exchange.Services.Core.Types.MimeContentType mimeContent)
		{
			IdAndSession idAndSession = new IdAndSession(base.MailboxIdentityMailboxSession.GetRefreshedDefaultFolderId(DefaultFolderType.DeletedItems), base.MailboxIdentityMailboxSession);
			MessageItem messageItem = MessageItem.Create(idAndSession.Session, idAndSession.Id);
			MimeContentProperty.ConvertMimeContentServiceObjectToMessageItem(mimeContent, messageItem, idAndSession);
			Microsoft.Exchange.Services.Core.Types.ItemType itemType = this.SaveReferenceItem(messageItem);
			return itemType.ItemId;
		}

		// Token: 0x0600433B RID: 17211 RVA: 0x000E1584 File Offset: 0x000DF784
		private Microsoft.Exchange.Services.Core.Types.ItemType SaveReferenceItem(MessageItem referenceItem)
		{
			RightsManagedMessageItem rightsManagedMessageItem = referenceItem as RightsManagedMessageItem;
			bool flag = rightsManagedMessageItem != null && rightsManagedMessageItem.IsDecoded;
			base.SaveXsoItem(referenceItem, Microsoft.Exchange.Services.Core.Types.ConflictResolutionType.AlwaysOverwrite);
			if (flag)
			{
				IrmUtils.DecodeIrmMessage(referenceItem.Session, referenceItem, false);
			}
			Microsoft.Exchange.Services.Core.Types.ItemType itemType = Microsoft.Exchange.Services.Core.Types.ItemType.CreateFromStoreObjectType(referenceItem.Id.ObjectId.ObjectType);
			ItemResponseShape defaultItemResponseShape = ServiceCommandBase.DefaultItemResponseShape;
			ToServiceObjectPropertyList toServiceObjectPropertyList = XsoDataConverter.GetToServiceObjectPropertyList(referenceItem, defaultItemResponseShape);
			toServiceObjectPropertyList.CharBuffer = new char[32768];
			ServiceCommandBase.LoadServiceObject(itemType, referenceItem, IdAndSession.CreateFromItem(referenceItem), defaultItemResponseShape, toServiceObjectPropertyList);
			return itemType;
		}

		// Token: 0x0600433C RID: 17212 RVA: 0x000E1608 File Offset: 0x000DF808
		private void DeleteReferenceItem(Microsoft.Exchange.Services.Core.Types.ItemId referenceItemId)
		{
			DeleteItemRequest deleteItemRequest = new DeleteItemRequest();
			deleteItemRequest.Ids = new BaseItemId[]
			{
				referenceItemId
			};
			deleteItemRequest.DeleteType = Microsoft.Exchange.Services.Core.Types.DisposalType.HardDelete;
			DeleteItem deleteItem = new DeleteItem(base.CallContext, deleteItemRequest);
			try
			{
				deleteItem.PreExecute();
				deleteItem.Execute();
			}
			catch (Exception ex)
			{
				ExTraceGlobals.DeleteItemCallTracer.TraceDebug<string, string>((long)this.GetHashCode(), "CreateResponseFromModernGroup.DeleteReferenceItem: Reference item {0} is not deleted, error message: {1}.", referenceItemId.Id, ex.Message);
			}
		}

		// Token: 0x0600433D RID: 17213 RVA: 0x000E1688 File Offset: 0x000DF888
		private ServiceResult<Microsoft.Exchange.Services.Core.Types.ItemType[]> CreateResponseFromReferenceItem(Microsoft.Exchange.Services.Core.Types.SmartResponseType smartResponse)
		{
			CreateItemRequest createItemRequest = new CreateItemRequest();
			Microsoft.Exchange.Services.Core.Types.NonEmptyArrayOfAllItemsType nonEmptyArrayOfAllItemsType = new Microsoft.Exchange.Services.Core.Types.NonEmptyArrayOfAllItemsType();
			nonEmptyArrayOfAllItemsType.Add(smartResponse);
			createItemRequest.Items = nonEmptyArrayOfAllItemsType;
			createItemRequest.MessageDisposition = base.Request.MessageDisposition;
			createItemRequest.ItemShape = base.Request.ItemShape;
			createItemRequest.ShapeName = base.Request.ShapeName;
			CreateItem createItem = new CreateItem(base.CallContext, createItemRequest);
			createItem.PreExecute();
			return createItem.Execute();
		}

		// Token: 0x0600433E RID: 17214 RVA: 0x000E1720 File Offset: 0x000DF920
		private ServiceResult<Microsoft.Exchange.Services.Core.Types.MimeContentType> RemoteGetReferenceItem(Microsoft.Exchange.Services.Core.Types.SmartResponseType item)
		{
			GetItemRequest getItemRequest = new GetItemRequest();
			getItemRequest.Ids = new BaseItemId[]
			{
				item.ReferenceItemId
			};
			getItemRequest.ItemShape = CreateResponseFromModernGroup.DefaultOWARemoteGetItemResponseShape;
			ExchangeServiceBinding serviceBinding = this.GetServiceBinding(getItemRequest);
			GetItemType getItemRequestType = EwsClientHelper.Convert<GetItemRequest, GetItemType>(getItemRequest);
			GetItemResponseType getItemResponseType = null;
			Exception ex;
			bool flag = EwsClientHelper.ExecuteEwsCall(delegate
			{
				getItemResponseType = serviceBinding.GetItem(getItemRequestType);
			}, out ex);
			if (flag)
			{
				return CreateResponseFromModernGroup.ParseItemInfoResponseMessageType(getItemResponseType);
			}
			throw ex;
		}

		// Token: 0x0600433F RID: 17215 RVA: 0x000E17A4 File Offset: 0x000DF9A4
		private static ServiceResult<Microsoft.Exchange.Services.Core.Types.MimeContentType> ParseItemInfoResponseMessageType(GetItemResponseType getItemResponseType)
		{
			if (getItemResponseType.ResponseMessages != null && getItemResponseType.ResponseMessages.Items != null && getItemResponseType.ResponseMessages.Items.Length > 0 && getItemResponseType.ResponseMessages.Items[0] is ItemInfoResponseMessageType)
			{
				ItemInfoResponseMessageType itemInfoResponseMessageType = (ItemInfoResponseMessageType)getItemResponseType.ResponseMessages.Items[0];
				if (itemInfoResponseMessageType.ResponseClass != ResponseClassType.Success)
				{
					ServiceError error = new ServiceError(itemInfoResponseMessageType.MessageText, EnumUtilities.Parse<Microsoft.Exchange.Services.Core.Types.ResponseCodeType>(itemInfoResponseMessageType.ResponseCode.ToString()), itemInfoResponseMessageType.DescriptiveLinkKey, ExchangeVersion.Exchange2012);
					return new ServiceResult<Microsoft.Exchange.Services.Core.Types.MimeContentType>(error);
				}
				if (itemInfoResponseMessageType.Items != null && itemInfoResponseMessageType.Items.Items.Length > 0 && itemInfoResponseMessageType.Items.Items[0] != null)
				{
					Microsoft.Exchange.Services.Core.Types.MimeContentType value = EwsClientHelper.Convert<Microsoft.Exchange.SoapWebClient.EWS.MimeContentType, Microsoft.Exchange.Services.Core.Types.MimeContentType>(itemInfoResponseMessageType.Items.Items[0].MimeContent);
					return new ServiceResult<Microsoft.Exchange.Services.Core.Types.MimeContentType>(value);
				}
			}
			ServiceError error2 = new ServiceError((CoreResources.IDs)4004906780U, Microsoft.Exchange.Services.Core.Types.ResponseCodeType.ErrorInternalServerError, 0, ExchangeVersion.Exchange2012);
			return new ServiceResult<Microsoft.Exchange.Services.Core.Types.MimeContentType>(error2);
		}

		// Token: 0x04002725 RID: 10021
		private const int BufferSize = 32768;

		// Token: 0x04002726 RID: 10022
		private static readonly string CreateResponseFromModernGroupActionName = typeof(CreateResponseFromModernGroup).Name;

		// Token: 0x04002727 RID: 10023
		private static readonly ItemResponseShape DefaultOWARemoteGetItemResponseShape = new ItemResponseShape(ShapeEnum.IdOnly, BodyResponseType.Best, true, null);

		// Token: 0x04002728 RID: 10024
		private Microsoft.Exchange.Services.Core.Types.ItemType[] items;

		// Token: 0x04002729 RID: 10025
		private ItemResponseShape responseShape;

		// Token: 0x0400272A RID: 10026
		private Microsoft.Exchange.Services.Core.Types.MessageDispositionType? messageDisposition = null;
	}
}
