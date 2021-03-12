using System;
using System.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Diagnostics;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x020002CE RID: 718
	internal sealed class DeleteItem : DeleteCommandBase<DeleteItemRequest, DeleteItemResponseMessage>
	{
		// Token: 0x060013F2 RID: 5106 RVA: 0x00063B40 File Offset: 0x00061D40
		public DeleteItem(CallContext callContext, DeleteItemRequest request) : base(callContext, request)
		{
			OwsLogRegistry.Register(DeleteItem.DeleteItemActionName, typeof(DeleteItemMetadata), new Type[0]);
		}

		// Token: 0x060013F3 RID: 5107 RVA: 0x00063B64 File Offset: 0x00061D64
		internal override void PreExecuteCommand()
		{
			this.objectIds = base.Request.Ids.OfType<ServiceObjectId>().ToList<ServiceObjectId>();
			this.disposalType = base.Request.DeleteType;
			if (!string.IsNullOrEmpty(base.Request.SendMeetingCancellations))
			{
				this.sendMeetingCancellations = new CalendarItemOperationType.CreateOrDelete?(SendMeetingInvitations.ConvertToEnum(base.Request.SendMeetingCancellations));
			}
			if (!string.IsNullOrEmpty(base.Request.AffectedTaskOccurrences))
			{
				this.affectedTaskOccurrences = new AffectedTaskOccurrencesType?(AffectedTaskOccurrences.ConvertToEnum(base.Request.AffectedTaskOccurrences));
			}
			ServiceCommandBase.ThrowIfNullOrEmpty<ServiceObjectId>(this.objectIds, "objectIds", "DeleteItem::PreExecute");
		}

		// Token: 0x060013F4 RID: 5108 RVA: 0x00063C0C File Offset: 0x00061E0C
		internal override IExchangeWebMethodResponse GetResponse()
		{
			DeleteItemResponse deleteItemResponse = new DeleteItemResponse();
			deleteItemResponse.AddResponses(base.Results);
			return deleteItemResponse;
		}

		// Token: 0x060013F5 RID: 5109 RVA: 0x00063C2C File Offset: 0x00061E2C
		protected override IdAndSession GetIdAndSession(ServiceObjectId objectId)
		{
			BaseItemId baseItemId = objectId as BaseItemId;
			return base.IdConverter.ConvertItemIdToIdAndSessionReadOnly(baseItemId);
		}

		// Token: 0x060013F6 RID: 5110 RVA: 0x00063C4C File Offset: 0x00061E4C
		protected override StoreObject GetStoreObject(IdAndSession idAndSession)
		{
			return idAndSession.GetRootXsoItem(new PropertyDefinition[]
			{
				MessageItemSchema.Flags
			});
		}

		// Token: 0x060013F7 RID: 5111 RVA: 0x00063C70 File Offset: 0x00061E70
		protected override ServiceResult<DeleteItemResponseMessage> TryExecuteDelete(StoreObject storeObject, DeleteItemFlags deleteItemFlags, IdAndSession idAndSession, out LocalizedException localizedException, bool returnNewItemIds)
		{
			DeleteItemResponseMessage deleteItemResponseMessage = new DeleteItemResponseMessage();
			if (base.Request.SuppressReadReceipts)
			{
				deleteItemFlags |= DeleteItemFlags.SuppressReadReceipt;
			}
			Task task = storeObject as Task;
			bool flag;
			if (task != null && this.affectedTaskOccurrences.Value == AffectedTaskOccurrencesType.SpecifiedOccurrenceOnly)
			{
				task.OpenAsReadWrite();
				try
				{
					task.DeleteCurrentOccurrence();
				}
				catch (InvalidOperationException)
				{
					throw new CannotDeleteTaskOccurrenceException();
				}
				task.Save(SaveMode.ResolveConflicts);
				flag = true;
				localizedException = null;
			}
			else
			{
				CalendarItemBase calendarItemBase = storeObject as CalendarItemBase;
				if (calendarItemBase != null)
				{
					calendarItemBase.OpenAsReadWrite();
					DeleteItem.TrySendCancellations(calendarItemBase, this.sendMeetingCancellations.Value);
				}
				AggregateOperationResult aggregateOperationResult = base.ExecuteDelete(storeObject, deleteItemFlags, idAndSession, returnNewItemIds);
				ExTraceGlobals.DeleteItemCallTracer.TraceDebug<OperationResult>(0L, "Delete item operation result '{0}'", aggregateOperationResult.OperationResult);
				flag = (aggregateOperationResult.OperationResult == OperationResult.Succeeded);
				localizedException = (flag ? null : aggregateOperationResult.GroupOperationResults[0].Exception);
				if (returnNewItemIds)
				{
					ItemId movedItemId = null;
					if (aggregateOperationResult != null && aggregateOperationResult.GroupOperationResults != null && aggregateOperationResult.GroupOperationResults.Length > 0 && aggregateOperationResult.GroupOperationResults[0].OperationResult == OperationResult.Succeeded && aggregateOperationResult.GroupOperationResults[0].ResultObjectIds != null && aggregateOperationResult.GroupOperationResults[0].ResultObjectIds.Count > 0)
					{
						StoreId storeItemId = IdConverter.CombineStoreObjectIdWithChangeKey(aggregateOperationResult.GroupOperationResults[0].ResultObjectIds[base.CurrentStep], aggregateOperationResult.GroupOperationResults[0].ResultChangeKeys[base.CurrentStep]);
						movedItemId = IdConverter.ConvertStoreItemIdToItemId(storeItemId, storeObject.Session);
					}
					deleteItemResponseMessage.MovedItemId = movedItemId;
				}
			}
			if (!flag)
			{
				return null;
			}
			return new ServiceResult<DeleteItemResponseMessage>(deleteItemResponseMessage);
		}

		// Token: 0x060013F8 RID: 5112 RVA: 0x00063E0C File Offset: 0x0006200C
		protected override ServiceResult<DeleteItemResponseMessage> CreateEmptyResponseType()
		{
			return new ServiceResult<DeleteItemResponseMessage>(new DeleteItemResponseMessage());
		}

		// Token: 0x060013F9 RID: 5113 RVA: 0x00063E18 File Offset: 0x00062018
		protected override void ExecuteCommandValidations(StoreObject storeObject)
		{
			if (ServiceCommandBase.IsAssociated((Item)storeObject) && this.disposalType != DisposalType.HardDelete)
			{
				throw new ServiceInvalidOperationException(CoreResources.IDs.ErrorInvalidOperationDisposalTypeAssociatedItem);
			}
			if (storeObject is Task)
			{
				if (this.affectedTaskOccurrences == null)
				{
					throw new AffectedTaskOccurrencesRequiredException();
				}
			}
			else if (storeObject is CalendarItemBase)
			{
				RequestDetailsLoggerBase<RequestDetailsLogger>.SafeSetLogger(RequestDetailsLogger.Current, DeleteItemMetadata.ActionType, "DeleteCalendarItem");
				if (this.sendMeetingCancellations == null)
				{
					throw new SendMeetingCancellationsRequiredException();
				}
				if (this.sendMeetingCancellations.Value != CalendarItemOperationType.CreateOrDelete.SendToNone && storeObject.Session is PublicFolderSession)
				{
					throw new ServiceInvalidOperationException((CoreResources.IDs)2990730164U);
				}
			}
		}

		// Token: 0x060013FA RID: 5114 RVA: 0x00063EC0 File Offset: 0x000620C0
		protected override void LogDelegateSession(string principal)
		{
			RequestDetailsLoggerBase<RequestDetailsLogger>.SafeSetLogger(RequestDetailsLogger.Current, DeleteItemMetadata.SessionType, LogonType.Delegated);
			if (!string.IsNullOrEmpty(principal))
			{
				RequestDetailsLoggerBase<RequestDetailsLogger>.SafeSetLogger(RequestDetailsLogger.Current, DeleteItemMetadata.Principal, principal);
			}
		}

		// Token: 0x060013FB RID: 5115 RVA: 0x00063EF4 File Offset: 0x000620F4
		private static void TrySendCancellations(CalendarItemBase calendarItemBase, CalendarItemOperationType.CreateOrDelete sendMeetingCancellations)
		{
			switch (sendMeetingCancellations)
			{
			case CalendarItemOperationType.CreateOrDelete.SendToNone:
				return;
			case CalendarItemOperationType.CreateOrDelete.SendOnlyToAll:
				break;
			case CalendarItemOperationType.CreateOrDelete.SendToAllAndSaveCopy:
				if (!ServiceCommandBase.IsOrganizerMeeting(calendarItemBase))
				{
					return;
				}
				using (MeetingCancellation meetingCancellation = calendarItemBase.CancelMeeting(null, null))
				{
					if (meetingCancellation.Recipients.Count > 0)
					{
						meetingCancellation.Send();
					}
					return;
				}
				break;
			default:
				goto IL_89;
			}
			if (!ServiceCommandBase.IsOrganizerMeeting(calendarItemBase))
			{
				return;
			}
			using (MeetingCancellation meetingCancellation2 = calendarItemBase.CancelMeeting(null, null))
			{
				if (meetingCancellation2.Recipients.Count > 0)
				{
					meetingCancellation2.SendWithoutSavingMessage();
				}
				return;
			}
			IL_89:
			throw new CalendarExceptionInvalidAttributeValue(new PropertyUri(PropertyUriEnum.CalendarItemType));
		}

		// Token: 0x1700026F RID: 623
		// (get) Token: 0x060013FC RID: 5116 RVA: 0x00063FB8 File Offset: 0x000621B8
		// (set) Token: 0x060013FD RID: 5117 RVA: 0x00063FC0 File Offset: 0x000621C0
		internal override bool IgnoreObjectNotFoundError { get; set; }

		// Token: 0x04000D7A RID: 3450
		private static readonly string DeleteItemActionName = typeof(DeleteItem).Name;
	}
}
