using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Services.Core.DataConverter;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Wcf;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core.ServiceCommands.Anonymous
{
	// Token: 0x020002DC RID: 732
	internal class GetItemAnonymous : BaseAnonymousCommand<GetItemJsonRequest, GetItemJsonResponse>
	{
		// Token: 0x060018B3 RID: 6323 RVA: 0x0005535A File Offset: 0x0005355A
		public GetItemAnonymous(GetItemJsonRequest request) : base(request)
		{
		}

		// Token: 0x060018B4 RID: 6324 RVA: 0x00055364 File Offset: 0x00053564
		protected override void UpdateRequestBody(PublishedCalendar publishedFolder)
		{
			ItemResponseShape itemResponseShape = new ItemResponseShape();
			itemResponseShape.BaseShape = ShapeEnum.IdOnly;
			List<PropertyPath> list = new List<PropertyPath>
			{
				new PropertyUri(PropertyUriEnum.ItemId),
				new PropertyUri(PropertyUriEnum.ItemParentId),
				new PropertyUri(PropertyUriEnum.Sensitivity),
				new PropertyUri(PropertyUriEnum.IsCancelled),
				new PropertyUri(PropertyUriEnum.LegacyFreeBusyStatus),
				new PropertyUri(PropertyUriEnum.CalendarItemType),
				new PropertyUri(PropertyUriEnum.Start),
				new PropertyUri(PropertyUriEnum.End),
				new PropertyUri(PropertyUriEnum.IsAllDayEvent),
				new PropertyUri(PropertyUriEnum.EnhancedLocation),
				new PropertyUri(PropertyUriEnum.Subject),
				new PropertyUri(PropertyUriEnum.Recurrence)
			};
			if (publishedFolder.DetailLevel == DetailLevelEnumType.FullDetails)
			{
				list.Add(new PropertyUri(PropertyUriEnum.Body));
			}
			itemResponseShape.AdditionalProperties = list.ToArray();
			itemResponseShape.BodyType = base.Request.Body.ItemShape.BodyType;
			WellKnownShapes.SetDefaultsOnItemResponseShape(itemResponseShape, base.Context.UserAgent.Layout, null);
			base.Request.Body.ItemShape = itemResponseShape;
		}

		// Token: 0x060018B5 RID: 6325 RVA: 0x0005548D File Offset: 0x0005368D
		protected override void ValidateRequestBody()
		{
			base.Request.Body.Validate();
		}

		// Token: 0x060018B6 RID: 6326 RVA: 0x000554A0 File Offset: 0x000536A0
		protected override GetItemJsonResponse InternalExecute(PublishedCalendar publishedFolder)
		{
			base.TraceDebug("GetItemAnonymous:InternalExecute", new object[0]);
			if (publishedFolder.DetailLevel == DetailLevelEnumType.AvailabilityOnly)
			{
				return this.CreateErrorResponse(new InvalidOperationException("Item details are not allowed"), ResponseCodeType.ErrorInvalidRequest);
			}
			StoreObjectId itemId = null;
			bool flag;
			try
			{
				this.GetStoreObjectId(out itemId, out flag);
			}
			catch (StoragePermanentException exception)
			{
				return this.CreateErrorResponse(exception, ResponseCodeType.ErrorInvalidRequest);
			}
			GetItemJsonResponse result;
			try
			{
				base.TraceDebug("Get item from published folder", new object[0]);
				using (DisposeGuard disposeGuard = default(DisposeGuard))
				{
					CalendarItemBase item = publishedFolder.GetItem(itemId, GetItemAnonymous.propertiesToFetch);
					disposeGuard.Add<CalendarItemBase>(item);
					if (flag)
					{
						base.TraceDebug("Request was for a Recurring Master", new object[0]);
						if (!(item is CalendarItemOccurrence))
						{
							return this.CreateErrorResponse(new Exception("Invalid RecurrenceMasterId"), ResponseCodeType.ErrorInvalidRequest);
						}
						itemId = ((CalendarItemOccurrence)item).MasterId.ObjectId;
						item = publishedFolder.GetItem(itemId, GetItemAnonymous.propertiesToFetch);
						disposeGuard.Add<CalendarItemBase>(item);
					}
					EwsCalendarItemType serviceObject = this.CreateServiceObject(item);
					if (item.Sensitivity == Sensitivity.Private)
					{
						base.TraceDebug("Clear sensitive information", new object[0]);
						this.ClearSensitiveInformation(serviceObject);
					}
					result = this.CreateSuccessResponse(serviceObject);
				}
			}
			catch (ObjectNotFoundException exception2)
			{
				result = this.CreateErrorResponse(exception2, ResponseCodeType.ErrorInvalidRequest);
			}
			return result;
		}

		// Token: 0x060018B7 RID: 6327 RVA: 0x00055618 File Offset: 0x00053818
		protected override GetItemJsonResponse CreateErrorResponse(Exception exception, ResponseCodeType codeType)
		{
			base.TraceError("GetItemAnonymous:CreateErrorResponse. Exception:{0}", new object[]
			{
				exception
			});
			GetItemResponse getItemResponse = new GetItemResponse();
			ServiceError error = new ServiceError(base.GetExceptionMessage(exception), codeType, 0, ExchangeVersion.Latest);
			getItemResponse.AddResponse(new ResponseMessage(ServiceResultCode.Error, error));
			return new GetItemJsonResponse
			{
				Body = getItemResponse
			};
		}

		// Token: 0x060018B8 RID: 6328 RVA: 0x00055674 File Offset: 0x00053874
		private void GetStoreObjectId(out StoreObjectId objectId, out bool isRecurringMaster)
		{
			objectId = ServiceIdConverter.ConvertFromConcatenatedId(base.Request.Body.Ids[0].GetId(), BasicTypes.Item, new List<AttachmentId>(0)).ToStoreObjectId();
			isRecurringMaster = (base.Request.Body.Ids[0] is RecurringMasterItemId);
		}

		// Token: 0x060018B9 RID: 6329 RVA: 0x000556C8 File Offset: 0x000538C8
		private EwsCalendarItemType CreateServiceObject(CalendarItemBase calendarItem)
		{
			EwsCalendarItemType ewsCalendarItemType = (EwsCalendarItemType)ItemType.CreateFromStoreObjectType(calendarItem.StoreObjectId.ObjectType);
			IdAndSession idAndSession = new IdAndSession(calendarItem.StoreObjectId, calendarItem.Session);
			ToServiceObjectPropertyList toServiceObjectPropertyList = XsoDataConverter.GetToServiceObjectPropertyList(idAndSession.Id, idAndSession.Session, base.Request.Body.ItemShape, StaticParticipantResolver.DefaultInstance);
			toServiceObjectPropertyList.CharBuffer = new char[32768];
			toServiceObjectPropertyList.ConvertStoreObjectPropertiesToServiceObject(idAndSession, calendarItem, ewsCalendarItemType);
			if (ewsCalendarItemType.Body == null)
			{
				ewsCalendarItemType.Body = new BodyContentType
				{
					BodyType = ((base.Request.Body.ItemShape.BodyType == BodyResponseType.Text) ? BodyType.Text : BodyType.HTML),
					Value = string.Empty
				};
			}
			return ewsCalendarItemType;
		}

		// Token: 0x060018BA RID: 6330 RVA: 0x00055784 File Offset: 0x00053984
		private void ClearSensitiveInformation(EwsCalendarItemType serviceObject)
		{
			serviceObject.Subject = ClientStrings.PrivateAppointmentSubject.ToString(base.Context.Culture);
			serviceObject.Location = string.Empty;
			serviceObject.EnhancedLocation = null;
			if (serviceObject.Body != null)
			{
				serviceObject.Body.Value = string.Empty;
			}
		}

		// Token: 0x060018BB RID: 6331 RVA: 0x000557DC File Offset: 0x000539DC
		private GetItemJsonResponse CreateSuccessResponse(EwsCalendarItemType serviceObject)
		{
			base.TraceDebug("create successful response", new object[0]);
			GetItemJsonResponse getItemJsonResponse = new GetItemJsonResponse();
			getItemJsonResponse.Body = new GetItemResponse();
			getItemJsonResponse.Body.AddResponse(new ItemInfoResponseMessage(ServiceResultCode.Success, null, serviceObject));
			return getItemJsonResponse;
		}

		// Token: 0x04000D41 RID: 3393
		private static readonly PropertyDefinition[] propertiesToFetch = new PropertyDefinition[]
		{
			ItemSchema.Id,
			StoreObjectSchema.ParentItemId,
			ItemSchema.Subject,
			ItemSchema.Sensitivity,
			CalendarItemInstanceSchema.StartTime,
			CalendarItemInstanceSchema.EndTime,
			CalendarItemBaseSchema.IsAllDayEvent,
			CalendarItemBaseSchema.FreeBusyStatus,
			CalendarItemBaseSchema.Location,
			CalendarItemBaseSchema.CalendarItemType,
			CalendarItemBaseSchema.RecurrenceType,
			CalendarItemBaseSchema.RecurrencePattern
		};
	}
}
