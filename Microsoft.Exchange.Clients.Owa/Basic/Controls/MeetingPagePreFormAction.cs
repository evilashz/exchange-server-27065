using System;
using System.Collections.Generic;
using System.Web;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Clients;

namespace Microsoft.Exchange.Clients.Owa.Basic.Controls
{
	// Token: 0x02000060 RID: 96
	internal sealed class MeetingPagePreFormAction : IPreFormAction
	{
		// Token: 0x060002B7 RID: 695 RVA: 0x00017D44 File Offset: 0x00015F44
		public PreFormActionResponse Execute(OwaContext owaContext, out ApplicationElement applicationElement, out string type, out string state, out string action)
		{
			if (owaContext == null)
			{
				throw new ArgumentNullException("owaContext");
			}
			applicationElement = ApplicationElement.NotSet;
			type = string.Empty;
			state = string.Empty;
			action = string.Empty;
			HttpContext httpContext = owaContext.HttpContext;
			this.userContext = owaContext.UserContext;
			HttpRequest request = httpContext.Request;
			if (!Utilities.IsPostRequest(request) && owaContext.FormsRegistryContext.Action != "Prev" && owaContext.FormsRegistryContext.Action != "Next")
			{
				return this.userContext.LastClientViewState.ToPreFormActionResponse();
			}
			this.context = owaContext;
			StoreId storeId = null;
			this.itemType = this.context.FormsRegistryContext.Type;
			string text;
			string storeObjectId;
			if (Utilities.IsPostRequest(request))
			{
				text = Utilities.GetFormParameter(request, "hidid", false);
				if (text == null)
				{
					throw new OwaInvalidRequestException("MessageId is not set in the form");
				}
				string formParameter = Utilities.GetFormParameter(request, "hidchk", false);
				if (formParameter != null)
				{
					storeId = Utilities.CreateItemId(this.userContext.MailboxSession, text, formParameter);
				}
				storeObjectId = Utilities.GetFormParameter(request, "hidfldid", true);
				string formParameter2 = Utilities.GetFormParameter(request, "rdoRsp", false);
				int num;
				if (!string.IsNullOrEmpty(formParameter2) && int.TryParse(formParameter2, out num))
				{
					this.responseAction = (MeetingPagePreFormAction.ResponseAction)num;
				}
			}
			else
			{
				text = Utilities.GetQueryStringParameter(request, "id", false);
				storeObjectId = Utilities.GetQueryStringParameter(request, "fId", true);
			}
			ItemOperations.Result result = null;
			StoreObjectId storeObjectId2 = Utilities.CreateStoreObjectId(this.userContext.MailboxSession, text);
			StoreObjectId folderId = Utilities.CreateStoreObjectId(this.userContext.MailboxSession, storeObjectId);
			string action2;
			if ((action2 = owaContext.FormsRegistryContext.Action) != null)
			{
				if (<PrivateImplementationDetails>{30BFC313-DE03-42E1-890C-A081E6097637}.$$method0x60002a7-1 == null)
				{
					<PrivateImplementationDetails>{30BFC313-DE03-42E1-890C-A081E6097637}.$$method0x60002a7-1 = new Dictionary<string, int>(10)
					{
						{
							"Prev",
							0
						},
						{
							"Next",
							1
						},
						{
							"Del",
							2
						},
						{
							"RemoveFromCal",
							3
						},
						{
							"Junk",
							4
						},
						{
							"Close",
							5
						},
						{
							"Accept",
							6
						},
						{
							"Tentative",
							7
						},
						{
							"Decline",
							8
						},
						{
							"DeclineDelete",
							9
						}
					};
				}
				int num2;
				if (<PrivateImplementationDetails>{30BFC313-DE03-42E1-890C-A081E6097637}.$$method0x60002a7-1.TryGetValue(action2, out num2))
				{
					switch (num2)
					{
					case 0:
						result = ItemOperations.GetNextViewItem(this.userContext, ItemOperations.Action.Prev, storeObjectId2, folderId);
						break;
					case 1:
						result = ItemOperations.GetNextViewItem(this.userContext, ItemOperations.Action.Next, storeObjectId2, folderId);
						break;
					case 2:
						result = this.DeleteItem(storeObjectId2, folderId);
						this.userContext.ForceNewSearch = true;
						break;
					case 3:
						result = this.RemoveFromCalendar(storeObjectId2, folderId);
						this.userContext.ForceNewSearch = true;
						break;
					case 4:
						if (!this.userContext.IsJunkEmailEnabled)
						{
							throw new OwaInvalidRequestException(Strings.JunkMailDisabled);
						}
						owaContext[OwaContextProperty.InfobarMessage] = JunkEmailHelper.MarkAsJunk(this.userContext, new StoreObjectId[]
						{
							storeObjectId2
						});
						this.userContext.ForceNewSearch = true;
						break;
					case 5:
						break;
					case 6:
						this.responseType = ResponseType.Accept;
						this.userContext.ForceNewSearch = true;
						break;
					case 7:
						this.responseType = ResponseType.Tentative;
						this.userContext.ForceNewSearch = true;
						break;
					case 8:
						this.responseType = ResponseType.Decline;
						this.userContext.ForceNewSearch = true;
						break;
					case 9:
						this.responseType = ResponseType.Decline;
						this.responseAction = MeetingPagePreFormAction.ResponseAction.SendResponseAndDelete;
						this.userContext.ForceNewSearch = true;
						break;
					default:
						goto IL_370;
					}
					if (this.responseType != ResponseType.None)
					{
						return this.ProcessResponse(storeObjectId2, storeId, folderId);
					}
					return ItemOperations.GetPreFormActionResponse(this.userContext, result);
				}
			}
			IL_370:
			throw new OwaInvalidRequestException("Unknown action '" + owaContext.FormsRegistryContext.Action + "' in MeetingPage PreFormAction.");
		}

		// Token: 0x060002B8 RID: 696 RVA: 0x00018102 File Offset: 0x00016302
		private static void UpdateItem(Item item, ResponseType responseType)
		{
			item[CalendarItemBaseSchema.ResponseType] = responseType;
			if (item is MeetingRequest)
			{
				((MessageItem)item).IsRead = true;
			}
		}

		// Token: 0x060002B9 RID: 697 RVA: 0x0001812C File Offset: 0x0001632C
		private PreFormActionResponse ProcessResponse(StoreObjectId itemId, StoreId storeId, StoreObjectId folderId)
		{
			PreFormActionResponse result = null;
			switch (this.responseAction)
			{
			case MeetingPagePreFormAction.ResponseAction.SendResponse:
				result = this.ProcessNonEditResponse(MeetingPagePreFormAction.ResponseAction.SendResponse, storeId, itemId, folderId);
				break;
			case MeetingPagePreFormAction.ResponseAction.EditResponse:
			case MeetingPagePreFormAction.ResponseAction.SendResponseAndDelete:
				result = this.EditResponse(storeId);
				break;
			case MeetingPagePreFormAction.ResponseAction.NoResponse:
				result = this.ProcessNonEditResponse(MeetingPagePreFormAction.ResponseAction.NoResponse, storeId, itemId, folderId);
				break;
			}
			return result;
		}

		// Token: 0x060002BA RID: 698 RVA: 0x00018180 File Offset: 0x00016380
		private PreFormActionResponse EditResponse(StoreId storeId)
		{
			HttpRequest request = this.context.HttpContext.Request;
			MeetingRequest meetingRequest = null;
			CalendarItemBase calendarItemBase = null;
			this.properties = new PropertyDefinition[]
			{
				MeetingMessageSchema.CalendarProcessed,
				StoreObjectSchema.ParentItemId
			};
			PreFormActionResponse result;
			try
			{
				if (ObjectClass.IsCalendarItemCalendarItemOccurrenceOrRecurrenceException(this.itemType))
				{
					calendarItemBase = Utilities.GetItem<CalendarItemBase>(this.userContext, storeId, false, this.properties);
				}
				else if (ObjectClass.IsMeetingRequest(this.itemType))
				{
					meetingRequest = Utilities.GetItem<MeetingRequest>(this.userContext, storeId, false, this.properties);
					calendarItemBase = MeetingUtilities.UpdateCalendarItem(meetingRequest);
				}
				Utilities.ValidateCalendarItemBaseStoreObject(calendarItemBase);
				result = this.EditResponseInternal(this.responseType, calendarItemBase);
			}
			finally
			{
				if (calendarItemBase != null)
				{
					calendarItemBase.Dispose();
					calendarItemBase = null;
				}
				if (meetingRequest != null)
				{
					meetingRequest.Dispose();
					meetingRequest = null;
				}
			}
			return result;
		}

		// Token: 0x060002BB RID: 699 RVA: 0x0001824C File Offset: 0x0001644C
		private PreFormActionResponse EditResponseInternal(ResponseType responseType, CalendarItemBase calendarItemBase)
		{
			ExTraceGlobals.CalendarCallTracer.TraceDebug((long)this.GetHashCode(), "MeetingPagePreFormAction.EditResponseInternal");
			PreFormActionResponse preFormActionResponse = new PreFormActionResponse();
			using (MeetingResponse meetingResponse = MeetingUtilities.EditResponse(responseType, calendarItemBase))
			{
				meetingResponse.Load();
				preFormActionResponse.ApplicationElement = ApplicationElement.Item;
				preFormActionResponse.Type = meetingResponse.ClassName;
				preFormActionResponse.Action = "New";
				preFormActionResponse.AddParameter("id", meetingResponse.Id.ObjectId.ToBase64String());
			}
			return preFormActionResponse;
		}

		// Token: 0x060002BC RID: 700 RVA: 0x000182DC File Offset: 0x000164DC
		private PreFormActionResponse ProcessNonEditResponse(MeetingPagePreFormAction.ResponseAction responseAction, StoreId storeId, StoreObjectId itemId, StoreObjectId folderId)
		{
			ItemOperations.Result result = null;
			HttpRequest request = this.context.HttpContext.Request;
			StoreObjectType storeObjectType = StoreObjectType.Unknown;
			if (ObjectClass.IsCalendarItemCalendarItemOccurrenceOrRecurrenceException(this.itemType))
			{
				storeObjectType = StoreObjectType.CalendarItem;
			}
			else if (ObjectClass.IsMeetingRequest(this.itemType))
			{
				storeObjectType = StoreObjectType.MeetingRequest;
			}
			MeetingRequest meetingRequest = null;
			CalendarItemBase calendarItemBase = null;
			try
			{
				StoreObjectType storeObjectType2 = storeObjectType;
				if (storeObjectType2 != StoreObjectType.MeetingRequest)
				{
					if (storeObjectType2 == StoreObjectType.CalendarItem)
					{
						calendarItemBase = Utilities.GetItem<CalendarItemBase>(this.userContext, storeId, false, this.properties);
						MeetingUtilities.ThrowIfMeetingResponseInvalid(calendarItemBase);
					}
				}
				else
				{
					this.properties = new PropertyDefinition[]
					{
						MeetingMessageSchema.CalendarProcessed,
						StoreObjectSchema.ParentItemId
					};
					meetingRequest = Utilities.GetItem<MeetingRequest>(this.userContext, storeId, false, this.properties);
					calendarItemBase = MeetingUtilities.UpdateCalendarItem(meetingRequest);
				}
				if (meetingRequest != null && !Utilities.IsItemInDefaultFolder(meetingRequest, DefaultFolderType.DeletedItems))
				{
					MeetingPagePreFormAction.UpdateItem(meetingRequest, this.responseType);
					Utilities.SaveItem(meetingRequest);
				}
				MeetingPagePreFormAction.UpdateItem(calendarItemBase, this.responseType);
				Utilities.ValidateCalendarItemBaseStoreObject(calendarItemBase);
				if (meetingRequest != null)
				{
					result = this.CreateItemOperationsResultForDelete(itemId, folderId);
				}
				if (responseAction == MeetingPagePreFormAction.ResponseAction.SendResponse)
				{
					MeetingUtilities.NonEditResponse(this.responseType, calendarItemBase, true, meetingRequest);
				}
				else
				{
					if (responseAction != MeetingPagePreFormAction.ResponseAction.NoResponse)
					{
						throw new ArgumentException("Unhandled ResponseAction value.");
					}
					MeetingUtilities.NonEditResponse(this.responseType, calendarItemBase, false, meetingRequest);
				}
			}
			finally
			{
				if (meetingRequest != null)
				{
					meetingRequest.Dispose();
					meetingRequest = null;
				}
				if (calendarItemBase != null)
				{
					calendarItemBase.Dispose();
					calendarItemBase = null;
				}
			}
			this.userContext.ForceNewSearch = true;
			return ItemOperations.GetPreFormActionResponse(this.userContext, result);
		}

		// Token: 0x060002BD RID: 701 RVA: 0x0001843C File Offset: 0x0001663C
		private ItemOperations.Result DeleteItem(StoreObjectId itemId, StoreObjectId folderId)
		{
			if (ObjectClass.IsMeetingRequest(this.itemType) || ObjectClass.IsMeetingCancellation(this.itemType))
			{
				MeetingUtilities.DeleteMeetingMessageCalendarItem(itemId);
			}
			return ItemOperations.DeleteItem(this.userContext, itemId, folderId);
		}

		// Token: 0x060002BE RID: 702 RVA: 0x0001846C File Offset: 0x0001666C
		private ItemOperations.Result RemoveFromCalendar(StoreObjectId itemId, StoreObjectId folderId)
		{
			if (!Utilities.IsDefaultFolderId(this.userContext.MailboxSession, folderId, DefaultFolderType.DeletedItems))
			{
				return this.DeleteItem(itemId, folderId);
			}
			if (ObjectClass.IsMeetingCancellation(this.itemType))
			{
				MeetingUtilities.DeleteMeetingMessageCalendarItem(itemId);
			}
			else
			{
				if (!ObjectClass.IsCalendarItemCalendarItemOccurrenceOrRecurrenceException(this.itemType))
				{
					throw new OwaInvalidOperationException("RemoveFromCalendar can not handle this type of item");
				}
				MeetingUtilities.DeleteCalendarItem(itemId, DeleteItemFlags.MoveToDeletedItems);
			}
			return null;
		}

		// Token: 0x060002BF RID: 703 RVA: 0x000184CD File Offset: 0x000166CD
		private ItemOperations.Result CreateItemOperationsResultForDelete(StoreObjectId itemId, StoreObjectId folderId)
		{
			if (this.userContext.UserOptions.NextSelection != NextSelectionDirection.ReturnToView)
			{
				return ItemOperations.GetNextViewItem(this.userContext, ItemOperations.Action.Delete, itemId, folderId);
			}
			return null;
		}

		// Token: 0x040001EE RID: 494
		private const string FormMessageId = "hidid";

		// Token: 0x040001EF RID: 495
		private const string FormChangeKey = "hidchk";

		// Token: 0x040001F0 RID: 496
		private const string FormResponse = "rdoRsp";

		// Token: 0x040001F1 RID: 497
		private const string QueryStringFolderId = "fId";

		// Token: 0x040001F2 RID: 498
		private const string FormFolderId = "hidfldid";

		// Token: 0x040001F3 RID: 499
		private const string MessageId = "id";

		// Token: 0x040001F4 RID: 500
		private const string PreviousItemAction = "Prev";

		// Token: 0x040001F5 RID: 501
		private const string NextItemAction = "Next";

		// Token: 0x040001F6 RID: 502
		private const string DeleteAction = "Del";

		// Token: 0x040001F7 RID: 503
		private const string RemoveFromCalendarAction = "RemoveFromCal";

		// Token: 0x040001F8 RID: 504
		private const string JunkAction = "Junk";

		// Token: 0x040001F9 RID: 505
		private const string NotJunkAction = "NotJunk";

		// Token: 0x040001FA RID: 506
		private const string CloseAction = "Close";

		// Token: 0x040001FB RID: 507
		private const string AcceptAction = "Accept";

		// Token: 0x040001FC RID: 508
		private const string TentativeAction = "Tentative";

		// Token: 0x040001FD RID: 509
		private const string DeclineAction = "Decline";

		// Token: 0x040001FE RID: 510
		private const string DeclineDeleteAction = "DeclineDelete";

		// Token: 0x040001FF RID: 511
		private static readonly PropertyDefinition[] requestedProperties = new PropertyDefinition[]
		{
			ItemSchema.Id
		};

		// Token: 0x04000200 RID: 512
		private UserContext userContext;

		// Token: 0x04000201 RID: 513
		private OwaContext context;

		// Token: 0x04000202 RID: 514
		private PropertyDefinition[] properties;

		// Token: 0x04000203 RID: 515
		private ResponseType responseType;

		// Token: 0x04000204 RID: 516
		private string itemType;

		// Token: 0x04000205 RID: 517
		private MeetingPagePreFormAction.ResponseAction responseAction = MeetingPagePreFormAction.ResponseAction.NoResponse;

		// Token: 0x02000061 RID: 97
		public enum ResponseAction
		{
			// Token: 0x04000207 RID: 519
			SendResponse,
			// Token: 0x04000208 RID: 520
			EditResponse,
			// Token: 0x04000209 RID: 521
			NoResponse,
			// Token: 0x0400020A RID: 522
			SendResponseAndDelete
		}
	}
}
