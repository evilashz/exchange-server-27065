using System;
using System.Net;
using System.Web;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Services.Core;
using Microsoft.Exchange.Services.Core.Search;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Wcf;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core.ServiceCommands.Anonymous
{
	// Token: 0x020002DD RID: 733
	internal class FindItemAnonymous : BaseAnonymousCommand<FindItemJsonRequest, FindItemJsonResponse>
	{
		// Token: 0x060018BD RID: 6333 RVA: 0x0005589E File Offset: 0x00053A9E
		public FindItemAnonymous(FindItemJsonRequest request) : base(request)
		{
		}

		// Token: 0x060018BE RID: 6334 RVA: 0x000558A7 File Offset: 0x00053AA7
		protected override void ValidateRequestBody()
		{
			base.Request.Body.Validate();
		}

		// Token: 0x060018BF RID: 6335 RVA: 0x000558BC File Offset: 0x00053ABC
		protected override void UpdateRequestBody(PublishedCalendar publishedFolder)
		{
			ItemResponseShape itemResponseShape = new ItemResponseShape();
			itemResponseShape.BaseShape = ShapeEnum.IdOnly;
			itemResponseShape.AdditionalProperties = new PropertyPath[]
			{
				new PropertyUri(PropertyUriEnum.ItemId),
				new PropertyUri(PropertyUriEnum.ItemParentId),
				new PropertyUri(PropertyUriEnum.Sensitivity),
				new PropertyUri(PropertyUriEnum.IsCancelled),
				new PropertyUri(PropertyUriEnum.IsMeeting),
				new PropertyUri(PropertyUriEnum.LegacyFreeBusyStatus),
				new PropertyUri(PropertyUriEnum.CalendarItemType),
				new PropertyUri(PropertyUriEnum.Start),
				new PropertyUri(PropertyUriEnum.End),
				new PropertyUri(PropertyUriEnum.IsAllDayEvent),
				new PropertyUri(PropertyUriEnum.Location),
				new PropertyUri(PropertyUriEnum.Subject)
			};
			base.Request.Body.ItemShape = itemResponseShape;
			CalendarPageView calendarPageView = (CalendarPageView)base.Request.Body.Paging;
			CalendarPageView calendarPageView2 = new CalendarPageView();
			calendarPageView2.StartDate = calendarPageView.StartDate;
			calendarPageView2.EndDate = calendarPageView.EndDate;
			base.Request.Body.Paging = calendarPageView2;
		}

		// Token: 0x060018C0 RID: 6336 RVA: 0x000559BC File Offset: 0x00053BBC
		protected override FindItemJsonResponse InternalExecute(PublishedCalendar publishedFolder)
		{
			object[][] data;
			try
			{
				data = this.GetData(publishedFolder);
			}
			catch (ArgumentException exception)
			{
				return this.CreateErrorResponse(exception, ResponseCodeType.ErrorInvalidRequest);
			}
			ServiceResult<FindItemParentWrapper> result = this.CreateServiceResponse(new FindItemAnonymous.AnonymousQueryView(data, int.MaxValue), publishedFolder);
			FindItemResponse findItemResponse = new FindItemResponse();
			findItemResponse.ProcessServiceResult(result);
			return new FindItemJsonResponse
			{
				Body = findItemResponse
			};
		}

		// Token: 0x060018C1 RID: 6337 RVA: 0x00055A28 File Offset: 0x00053C28
		protected override FindItemJsonResponse CreateErrorResponse(Exception exception, ResponseCodeType codeType)
		{
			base.TraceError("FindItemAnonymous:CreateErrorResponse. Exception:{0}", new object[]
			{
				exception
			});
			FindItemResponse findItemResponse = new FindItemResponse();
			ServiceError error = new ServiceError(base.GetExceptionMessage(exception), codeType, 0, ExchangeVersion.Latest);
			findItemResponse.AddResponse(new ResponseMessage(ServiceResultCode.Error, error));
			return new FindItemJsonResponse
			{
				Body = findItemResponse
			};
		}

		// Token: 0x060018C2 RID: 6338 RVA: 0x00055A84 File Offset: 0x00053C84
		private object[][] GetData(PublishedCalendar publishedFolder)
		{
			CalendarPageView calendarPageView = (CalendarPageView)base.Request.Body.Paging;
			return publishedFolder.GetCalendarView(calendarPageView.StartDateEx, calendarPageView.EndDateEx, FindItemAnonymous.propertiesToFetch);
		}

		// Token: 0x060018C3 RID: 6339 RVA: 0x00055AC0 File Offset: 0x00053CC0
		private ServiceResult<FindItemParentWrapper> CreateServiceResponse(FindItemAnonymous.AnonymousQueryView anonymousView, PublishedCalendar publishedFolder)
		{
			ServiceResult<FindItemParentWrapper> result;
			using (Folder calendarFolder = publishedFolder.GetCalendarFolder())
			{
				PropertyListForViewRowDeterminer classDeterminer = PropertyListForViewRowDeterminer.BuildForItems(base.Request.Body.ItemShape, calendarFolder);
				IdAndSession idAndSession = new IdAndSession(calendarFolder.Id, calendarFolder.Session);
				ItemType[] items = anonymousView.ConvertToItems(FindItemAnonymous.propertiesToFetch, classDeterminer, idAndSession);
				BasePageResult paging = new BasePageResult(anonymousView);
				FindItemParentWrapper value = new FindItemParentWrapper(items, paging);
				result = new ServiceResult<FindItemParentWrapper>(value);
			}
			return result;
		}

		// Token: 0x04000D42 RID: 3394
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
			CalendarItemBaseSchema.CalendarItemType
		};

		// Token: 0x020002DE RID: 734
		internal class AnonymousQueryView : NormalQueryView
		{
			// Token: 0x060018C5 RID: 6341 RVA: 0x00055BB0 File Offset: 0x00053DB0
			public AnonymousQueryView(object[][] view, int rowsToGet) : base(view, rowsToGet)
			{
			}

			// Token: 0x060018C6 RID: 6342 RVA: 0x00055BBA File Offset: 0x00053DBA
			protected override void ThrowIfFindCountLimitExceeded(uint viewLength)
			{
			}

			// Token: 0x060018C7 RID: 6343 RVA: 0x00055BBC File Offset: 0x00053DBC
			protected override void CheckClientConnection()
			{
				if (!HttpContext.Current.Response.IsClientConnected)
				{
					BailOut.SetHTTPStatusAndClose(HttpStatusCode.NoContent);
				}
			}
		}
	}
}
