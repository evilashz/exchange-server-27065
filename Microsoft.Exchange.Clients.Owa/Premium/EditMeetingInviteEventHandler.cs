using System;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Clients;

namespace Microsoft.Exchange.Clients.Owa.Premium
{
	// Token: 0x020004A5 RID: 1189
	[OwaEventSegmentation(Feature.Calendar)]
	[OwaEventNamespace("EditMeetingInvite")]
	internal sealed class EditMeetingInviteEventHandler : ItemEventHandler
	{
		// Token: 0x06002DB0 RID: 11696 RVA: 0x00102AB0 File Offset: 0x00100CB0
		[OwaEventParameter("RemS", typeof(bool), false, true)]
		[OwaEventParameter("idci", typeof(OwaStoreObjectId), false, true)]
		[OwaEventParameter("Prvt", typeof(bool), false, true)]
		[OwaEventParameter("sn", typeof(int), false, true)]
		[OwaEventParameter("RemT", typeof(int), false, true)]
		[OwaEventParameter("Fbs", typeof(BusyType), false, true)]
		[OwaEvent("SaveMeetingInvite")]
		[OwaEventParameter("Id", typeof(OwaStoreObjectId))]
		[OwaEventParameter("CK", typeof(string))]
		[OwaEventParameter("Subj", typeof(string))]
		public void SaveMeetingInvite()
		{
			ExTraceGlobals.CalendarCallTracer.TraceDebug((long)this.GetHashCode(), "EditMeetingInviteEventHandler.SaveMeetingInvite");
			MeetingRequest meetingRequest = null;
			try
			{
				meetingRequest = this.GetMeetingRequest(new PropertyDefinition[0]);
				this.UpdateItem(meetingRequest);
				Utilities.SaveItem(meetingRequest);
				meetingRequest.Load();
				this.Writer.Write("<div id=ck>");
				this.Writer.Write(meetingRequest.Id.ChangeKeyAsBase64String());
				this.Writer.Write("</div>");
			}
			finally
			{
				if (meetingRequest != null)
				{
					meetingRequest.Dispose();
					meetingRequest = null;
				}
			}
		}

		// Token: 0x06002DB1 RID: 11697 RVA: 0x00102B4C File Offset: 0x00100D4C
		[OwaEventParameter("Fbs", typeof(BusyType), false, true)]
		[OwaEventParameter("CK", typeof(string))]
		[OwaEvent("SaveCalendarItem")]
		[OwaEventParameter("RemT", typeof(int), false, true)]
		[OwaEventParameter("Id", typeof(OwaStoreObjectId))]
		[OwaEventParameter("Subj", typeof(string))]
		[OwaEventParameter("Prvt", typeof(bool), false, true)]
		[OwaEventParameter("RemS", typeof(bool), false, true)]
		public void SaveCalendarItem()
		{
			ExTraceGlobals.CalendarCallTracer.TraceDebug((long)this.GetHashCode(), "EditMeetingInviteEventHandler.SaveCalendarItem");
			CalendarItemBase calendarItemBase = null;
			try
			{
				calendarItemBase = base.GetRequestItem<CalendarItemBase>(new PropertyDefinition[0]);
				this.UpdateItem(calendarItemBase);
				Utilities.SaveItem(calendarItemBase);
				calendarItemBase.Load();
				this.Writer.Write("<div id=ck>");
				this.Writer.Write(calendarItemBase.Id.ChangeKeyAsBase64String());
				this.Writer.Write("</div>");
			}
			finally
			{
				if (calendarItemBase != null)
				{
					calendarItemBase.Dispose();
					calendarItemBase = null;
				}
			}
		}

		// Token: 0x06002DB2 RID: 11698 RVA: 0x00102BE8 File Offset: 0x00100DE8
		[OwaEventParameter("Id", typeof(OwaStoreObjectId))]
		[OwaEventParameter("Subj", typeof(string))]
		[OwaEventParameter("CK", typeof(string))]
		[OwaEvent("SaveMeetingCancel")]
		public void SaveMeetingCancel()
		{
			ExTraceGlobals.CalendarCallTracer.TraceDebug((long)this.GetHashCode(), "EditMeetingInviteEventHandler.SaveMeetingCancel");
			MeetingCancellation meetingCancellation = null;
			try
			{
				meetingCancellation = base.GetRequestItem<MeetingCancellation>(new PropertyDefinition[0]);
				meetingCancellation[ItemSchema.Subject] = (string)base.GetParameter("Subj");
				Utilities.SaveItem(meetingCancellation);
				meetingCancellation.Load();
				this.Writer.Write("<div id=ck>");
				this.Writer.Write(meetingCancellation.Id.ChangeKeyAsBase64String());
				this.Writer.Write("</div>");
			}
			finally
			{
				if (meetingCancellation != null)
				{
					meetingCancellation.Dispose();
					meetingCancellation = null;
				}
			}
		}

		// Token: 0x06002DB3 RID: 11699 RVA: 0x00102C98 File Offset: 0x00100E98
		[OwaEvent("SaveMeetingResponse")]
		[OwaEventParameter("Subj", typeof(string))]
		[OwaEventParameter("Id", typeof(OwaStoreObjectId))]
		[OwaEventParameter("CK", typeof(string))]
		public void SaveMeetingResponse()
		{
			ExTraceGlobals.CalendarCallTracer.TraceDebug((long)this.GetHashCode(), "EditMeetingInviteEventHandler.SaveMeetingResponse");
			MeetingResponse meetingResponse = null;
			try
			{
				meetingResponse = base.GetRequestItem<MeetingResponse>(new PropertyDefinition[0]);
				meetingResponse[ItemSchema.Subject] = (string)base.GetParameter("Subj");
				Utilities.SaveItem(meetingResponse);
				meetingResponse.Load();
				this.Writer.Write("<div id=ck>");
				this.Writer.Write(meetingResponse.Id.ChangeKeyAsBase64String());
				this.Writer.Write("</div>");
			}
			finally
			{
				if (meetingResponse != null)
				{
					meetingResponse.Dispose();
					meetingResponse = null;
				}
			}
		}

		// Token: 0x06002DB4 RID: 11700 RVA: 0x00102D48 File Offset: 0x00100F48
		[OwaEventParameter("idci", typeof(OwaStoreObjectId), false, true)]
		[OwaEventParameter("ItemType", typeof(StoreObjectType))]
		[OwaEventParameter("sn", typeof(int), false, true)]
		[OwaEventParameter("Id", typeof(OwaStoreObjectId))]
		[OwaEventParameter("CK", typeof(string))]
		[OwaEventParameter("Subj", typeof(string), false, true)]
		[OwaEventParameter("Prvt", typeof(bool), false, true)]
		[OwaEventParameter("RemT", typeof(int), false, true)]
		[OwaEvent("EditResponseInvite")]
		[OwaEventParameter("Rsp", typeof(ResponseType))]
		[OwaEventParameter("RemS", typeof(bool), false, true)]
		public void EditResponseInvite()
		{
			ExTraceGlobals.CalendarCallTracer.TraceDebug((long)this.GetHashCode(), "EditMeetingInviteEventHandler.EditResponseInvite");
			ResponseType responseType = (ResponseType)base.GetParameter("Rsp");
			this.properties = new PropertyDefinition[]
			{
				MeetingMessageSchema.CalendarProcessed,
				StoreObjectSchema.ParentItemId
			};
			CalendarItemBase calendarItemBase = null;
			MeetingRequest meetingRequest = null;
			try
			{
				meetingRequest = this.GetMeetingRequest(this.properties);
				calendarItemBase = MeetingUtilities.UpdateCalendarItem(meetingRequest);
				if (calendarItemBase == null)
				{
					throw new OwaInvalidRequestException(string.Format("calendarItemBase associated with meeting request with Id {0} is null.", base.GetParameter("Id")));
				}
				this.EditResponseInternal(responseType, calendarItemBase);
				this.UpdateItem(meetingRequest);
				Utilities.SaveItem(meetingRequest);
				meetingRequest.Load();
				this.Writer.Write("<div id=ck>");
				this.Writer.Write(meetingRequest.Id.ChangeKeyAsBase64String());
				this.Writer.Write("</div>");
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
		}

		// Token: 0x06002DB5 RID: 11701 RVA: 0x00102E4C File Offset: 0x0010104C
		[OwaEventParameter("RemT", typeof(int), false, true)]
		[OwaEventParameter("CK", typeof(string))]
		[OwaEventParameter("Id", typeof(OwaStoreObjectId))]
		[OwaEventParameter("Subj", typeof(string))]
		[OwaEventParameter("Prvt", typeof(bool), false, true)]
		[OwaEventParameter("RemS", typeof(bool), false, true)]
		[OwaEventParameter("ItemType", typeof(StoreObjectType))]
		[OwaEventParameter("Rsp", typeof(ResponseType))]
		[OwaEvent("EditResponseCalendarItem")]
		public void EditResponseCalendarItem()
		{
			ExTraceGlobals.CalendarCallTracer.TraceDebug((long)this.GetHashCode(), "EditMeetingInviteEventHandler.EditResponseCalendarItem");
			ResponseType responseType = (ResponseType)base.GetParameter("Rsp");
			CalendarItemBase calendarItemBase = null;
			try
			{
				calendarItemBase = base.GetRequestItem<CalendarItemBase>(new PropertyDefinition[0]);
				if (calendarItemBase != null)
				{
					this.EditResponseInternal(responseType, calendarItemBase);
					calendarItemBase.Load();
					this.Writer.Write("<div id=nid>");
					if (calendarItemBase.Id != null && calendarItemBase.Id.ObjectId != null)
					{
						this.Writer.Write(OwaStoreObjectId.CreateFromStoreObject(calendarItemBase).ToBase64String());
					}
					this.Writer.Write("</div>");
				}
			}
			finally
			{
				if (calendarItemBase != null)
				{
					calendarItemBase.Dispose();
					calendarItemBase = null;
				}
			}
		}

		// Token: 0x06002DB6 RID: 11702 RVA: 0x00102F0C File Offset: 0x0010110C
		[OwaEvent("EditDeclineResponseCalendarItem")]
		[OwaEventParameter("Id", typeof(OwaStoreObjectId))]
		[OwaEventParameter("CK", typeof(string))]
		public void EditDeclineResponseCalendarItem()
		{
			ExTraceGlobals.CalendarCallTracer.TraceDebug((long)this.GetHashCode(), "EditMeetingInviteEventHandler.EditDeclineResponseCalendarItem");
			using (CalendarItemBase requestItem = base.GetRequestItem<CalendarItemBase>(new PropertyDefinition[0]))
			{
				this.EditResponseInternal(ResponseType.Decline, requestItem, false);
			}
		}

		// Token: 0x06002DB7 RID: 11703 RVA: 0x00102F64 File Offset: 0x00101164
		[OwaEventParameter("ItemType", typeof(StoreObjectType))]
		[OwaEventParameter("Id", typeof(OwaStoreObjectId))]
		[OwaEventParameter("CK", typeof(string))]
		[OwaEventParameter("Subj", typeof(string), false, true)]
		[OwaEventParameter("Prvt", typeof(bool), false, true)]
		[OwaEventParameter("RemS", typeof(bool), false, true)]
		[OwaEventParameter("RemT", typeof(int), false, true)]
		[OwaEventParameter("Rsp", typeof(ResponseType))]
		[OwaEventParameter("sn", typeof(int), false, true)]
		[OwaEventParameter("idci", typeof(OwaStoreObjectId), false, true)]
		[OwaEvent("SendResponse")]
		public void SendResponse()
		{
			ExTraceGlobals.CalendarCallTracer.TraceDebug((long)this.GetHashCode(), "EditMeetingInviteEventHandler.SendResponse");
			this.NonEditResponseInternal(true);
		}

		// Token: 0x06002DB8 RID: 11704 RVA: 0x00102F83 File Offset: 0x00101183
		[OwaEventParameter("Subj", typeof(string), false, true)]
		[OwaEventParameter("Id", typeof(OwaStoreObjectId))]
		[OwaEventParameter("CK", typeof(string))]
		[OwaEventParameter("sn", typeof(int), false, true)]
		[OwaEventParameter("Prvt", typeof(bool), false, true)]
		[OwaEventParameter("RemS", typeof(bool), false, true)]
		[OwaEventParameter("RemT", typeof(int), false, true)]
		[OwaEventParameter("Rsp", typeof(ResponseType))]
		[OwaEventParameter("ItemType", typeof(StoreObjectType))]
		[OwaEventParameter("idci", typeof(OwaStoreObjectId), false, true)]
		[OwaEvent("NoResponse")]
		public void NoResponse()
		{
			ExTraceGlobals.CalendarCallTracer.TraceDebug((long)this.GetHashCode(), "EditMeetingInviteEventHandler.NoResponse");
			this.NonEditResponseInternal(false);
		}

		// Token: 0x06002DB9 RID: 11705 RVA: 0x00102FA4 File Offset: 0x001011A4
		private void NonEditResponseInternal(bool sendResponse)
		{
			ResponseType responseType = (ResponseType)base.GetParameter("Rsp");
			StoreObjectType storeObjectType = (StoreObjectType)base.GetParameter("ItemType");
			MeetingRequest meetingRequest = null;
			CalendarItemBase calendarItemBase = null;
			try
			{
				StoreObjectType storeObjectType2 = storeObjectType;
				if (storeObjectType2 != StoreObjectType.MeetingRequest)
				{
					if (storeObjectType2 == StoreObjectType.CalendarItem)
					{
						calendarItemBase = base.GetRequestItem<CalendarItemBase>(new PropertyDefinition[0]);
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
					meetingRequest = this.GetMeetingRequest(this.properties);
					calendarItemBase = MeetingUtilities.UpdateCalendarItem(meetingRequest);
					if (calendarItemBase == null)
					{
						throw new OwaInvalidRequestException(string.Format("calendarItem associated with meetingRequest with Id {0} is null.", base.GetParameter("Id")));
					}
				}
				this.UpdateItem(calendarItemBase);
				Utilities.SaveItem(calendarItemBase);
				calendarItemBase.Load();
				MeetingUtilities.NonEditResponse(responseType, calendarItemBase, sendResponse, null);
				calendarItemBase.Load();
				if (meetingRequest != null)
				{
					this.UpdateItem(meetingRequest);
					Utilities.SaveItem(meetingRequest);
					MeetingUtilities.DeleteMeetingRequestAfterResponse(meetingRequest);
				}
				if (storeObjectType == StoreObjectType.CalendarItem)
				{
					this.Writer.Write("<div id=nid>");
					this.Writer.Write(OwaStoreObjectId.CreateFromStoreObject(calendarItemBase).ToBase64String());
					this.Writer.Write("</div>");
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
		}

		// Token: 0x06002DBA RID: 11706 RVA: 0x001030EC File Offset: 0x001012EC
		private void EditResponseInternal(ResponseType responseType, CalendarItemBase calendarItemBase)
		{
			this.EditResponseInternal(responseType, calendarItemBase, true);
		}

		// Token: 0x06002DBB RID: 11707 RVA: 0x001030F8 File Offset: 0x001012F8
		private void EditResponseInternal(ResponseType responseType, CalendarItemBase calendarItemBase, bool doCalendarItemUpdate)
		{
			ExTraceGlobals.CalendarCallTracer.TraceDebug((long)this.GetHashCode(), "EditMeetingInviteEventHandler.EditResponseInternal");
			MeetingUtilities.ThrowIfMeetingResponseInvalid(calendarItemBase);
			if (doCalendarItemUpdate)
			{
				this.UpdateItem(calendarItemBase);
				Utilities.SaveItem(calendarItemBase);
				calendarItemBase.Load();
			}
			using (MeetingResponse meetingResponse = MeetingUtilities.EditResponse(responseType, calendarItemBase))
			{
				meetingResponse.Load();
				this.Writer.Write("<div id=divOp _sOp=mr>");
				this.Writer.Write(OwaStoreObjectId.CreateFromStoreObject(meetingResponse).ToBase64String());
				this.Writer.Write("</div>");
			}
		}

		// Token: 0x06002DBC RID: 11708 RVA: 0x00103198 File Offset: 0x00101398
		private void UpdateItem(Item item)
		{
			if (base.IsParameterSet("Subj"))
			{
				item[ItemSchema.Subject] = (string)base.GetParameter("Subj");
			}
			if (base.IsParameterSet("RemS"))
			{
				bool flag = (bool)base.GetParameter("RemS");
				item[ItemSchema.ReminderIsSet] = flag;
				if (flag && base.IsParameterSet("RemT"))
				{
					int num = (int)base.GetParameter("RemT");
					if (num < 0)
					{
						throw new OwaInvalidRequestException("Reminder minutes before start cannot be a negative value");
					}
					item[ItemSchema.ReminderMinutesBeforeStart] = num;
				}
			}
			CalendarItemBase calendarItemBase = item as CalendarItemBase;
			MeetingRequest meetingRequest = item as MeetingRequest;
			if (base.IsParameterSet("Prvt") && (meetingRequest != null || (calendarItemBase != null && (calendarItemBase.CalendarItemType == CalendarItemType.Single || calendarItemBase.CalendarItemType == CalendarItemType.RecurringMaster))))
			{
				bool flag2 = (bool)base.GetParameter("Prvt");
				if (flag2)
				{
					item[ItemSchema.Sensitivity] = Sensitivity.Private;
				}
				else
				{
					object obj = item.TryGetProperty(ItemSchema.Sensitivity);
					if (obj is Sensitivity && (Sensitivity)obj == Sensitivity.Private)
					{
						item[ItemSchema.Sensitivity] = Sensitivity.Normal;
					}
				}
			}
			if (base.IsParameterSet("Fbs"))
			{
				object parameter = base.GetParameter("Fbs");
				if (parameter != null)
				{
					item[CalendarItemBaseSchema.FreeBusyStatus] = (BusyType)parameter;
				}
			}
			if (base.IsParameterSet("Rsp"))
			{
				item[CalendarItemBaseSchema.ResponseType] = (ResponseType)base.GetParameter("Rsp");
			}
			if (meetingRequest != null)
			{
				((MessageItem)item).IsRead = true;
			}
		}

		// Token: 0x06002DBD RID: 11709 RVA: 0x0010333C File Offset: 0x0010153C
		[OwaEventParameter("Id", typeof(OwaStoreObjectId))]
		[OwaEvent("RemCal")]
		public void RemoveFromCalendar()
		{
			ExTraceGlobals.CalendarCallTracer.TraceDebug((long)this.GetHashCode(), "EditMeetingInviteEventHandler.RemoveFromCalendar");
			OwaStoreObjectId owaStoreObjectId = (OwaStoreObjectId)base.GetParameter("Id");
			MeetingUtilities.DeleteMeetingMessageCalendarItem(owaStoreObjectId.StoreObjectId);
			base.Delete();
		}

		// Token: 0x06002DBE RID: 11710 RVA: 0x00103384 File Offset: 0x00101584
		[OwaEventParameter("Id", typeof(OwaStoreObjectId))]
		[OwaEventParameter("CK", typeof(string))]
		[OwaEvent("DeleteAttendeeMeeting")]
		public void DeleteAttendeeMeeting()
		{
			ExTraceGlobals.CalendarCallTracer.TraceDebug((long)this.GetHashCode(), "EditMeetingInviteEventHandler.DeleteAttendeeMeeting");
			using (CalendarItemBase requestItem = base.GetRequestItem<CalendarItemBase>(new PropertyDefinition[0]))
			{
				if (!requestItem.IsOrganizer() && !MeetingUtilities.IsCalendarItemEndTimeInPast(requestItem))
				{
					base.ResponseContentType = OwaEventContentType.Html;
					this.Writer.Write("<div id=divOp _sOp=sr></div>");
				}
				else
				{
					base.Delete();
				}
			}
		}

		// Token: 0x06002DBF RID: 11711 RVA: 0x00103400 File Offset: 0x00101600
		[OwaEvent("Delete")]
		[OwaEventParameter("Id", typeof(OwaStoreObjectId))]
		[OwaEventParameter("ItemType", typeof(StoreObjectType))]
		public override void Delete()
		{
			ExTraceGlobals.CalendarCallTracer.TraceDebug((long)this.GetHashCode(), "EditMeetingInviteEventHandler.Delete");
			OwaStoreObjectId owaStoreObjectId = (OwaStoreObjectId)base.GetParameter("Id");
			StoreObjectType storeObjectType = (StoreObjectType)base.GetParameter("ItemType");
			if (storeObjectType == StoreObjectType.MeetingRequest || storeObjectType == StoreObjectType.MeetingCancellation)
			{
				MeetingUtilities.DeleteMeetingMessageCalendarItem(owaStoreObjectId.StoreObjectId);
			}
			else if (storeObjectType == StoreObjectType.CalendarItem || storeObjectType == StoreObjectType.CalendarItemOccurrence)
			{
				MeetingUtilities.DeleteCalendarItem(owaStoreObjectId.StoreObjectId, DeleteItemFlags.MoveToDeletedItems);
				return;
			}
			base.Delete();
		}

		// Token: 0x06002DC0 RID: 11712 RVA: 0x0010347C File Offset: 0x0010167C
		[OwaEvent("PermanentDelete")]
		[OwaEventParameter("Id", typeof(OwaStoreObjectId))]
		[OwaEventParameter("ItemType", typeof(StoreObjectType))]
		public override void PermanentDelete()
		{
			ExTraceGlobals.CalendarCallTracer.TraceDebug((long)this.GetHashCode(), "EditMeetingInviteEventHandler.PermanentDelete");
			OwaStoreObjectId owaStoreObjectId = (OwaStoreObjectId)base.GetParameter("Id");
			StoreObjectType storeObjectType = (StoreObjectType)base.GetParameter("ItemType");
			if (!owaStoreObjectId.IsPublic && (storeObjectType == StoreObjectType.MeetingRequest || storeObjectType == StoreObjectType.MeetingCancellation))
			{
				MeetingUtilities.DeleteMeetingMessageCalendarItem(owaStoreObjectId.StoreObjectId);
			}
			else if (storeObjectType == StoreObjectType.CalendarItem || storeObjectType == StoreObjectType.CalendarItemOccurrence)
			{
				MeetingUtilities.DeleteCalendarItem(owaStoreObjectId.StoreObjectId, DeleteItemFlags.SoftDelete);
				return;
			}
			base.PermanentDelete();
		}

		// Token: 0x06002DC1 RID: 11713 RVA: 0x00103500 File Offset: 0x00101700
		private MeetingRequest GetMeetingRequest(params PropertyDefinition[] prefetchProperties)
		{
			MeetingRequest result = null;
			try
			{
				result = base.GetRequestItem<MeetingRequest>(prefetchProperties);
			}
			catch (ObjectNotFoundException innerException)
			{
				if (!base.IsParameterSet("idci"))
				{
					throw;
				}
				OwaStoreObjectId itemId = (OwaStoreObjectId)base.GetParameter("idci");
				int num;
				int num2;
				using (CalendarItemBase readOnlyRequestItem = base.GetReadOnlyRequestItem<CalendarItemBase>(itemId, new PropertyDefinition[0]))
				{
					num = (int)base.GetParameter("sn");
					num2 = (int)readOnlyRequestItem[CalendarItemBaseSchema.AppointmentSequenceNumber];
				}
				if (num < num2)
				{
					throw new OwaEventHandlerException("Meeting request must be out of date", LocalizedStrings.GetNonEncoded(2031473992), innerException);
				}
				throw;
			}
			return result;
		}

		// Token: 0x04001EBB RID: 7867
		public const string EventNamespace = "EditMeetingInvite";

		// Token: 0x04001EBC RID: 7868
		public const string MethodSaveMeetingInvite = "SaveMeetingInvite";

		// Token: 0x04001EBD RID: 7869
		public const string MethodSaveCalendarItem = "SaveCalendarItem";

		// Token: 0x04001EBE RID: 7870
		public const string MethodSaveMeetingCancel = "SaveMeetingCancel";

		// Token: 0x04001EBF RID: 7871
		public const string MethodSaveMeetingResponse = "SaveMeetingResponse";

		// Token: 0x04001EC0 RID: 7872
		public const string MethodEditResponseInvite = "EditResponseInvite";

		// Token: 0x04001EC1 RID: 7873
		public const string MethodEditResponseCalendarItem = "EditResponseCalendarItem";

		// Token: 0x04001EC2 RID: 7874
		public const string MethodEditDeclineResponseCalendarItem = "EditDeclineResponseCalendarItem";

		// Token: 0x04001EC3 RID: 7875
		public const string MethodDeleteAttendeeMeeting = "DeleteAttendeeMeeting";

		// Token: 0x04001EC4 RID: 7876
		public const string MethodSendResponse = "SendResponse";

		// Token: 0x04001EC5 RID: 7877
		public const string MethodNoResponse = "NoResponse";

		// Token: 0x04001EC6 RID: 7878
		public const string MethodRemoveFromCalendar = "RemCal";

		// Token: 0x04001EC7 RID: 7879
		public const string Response = "Rsp";

		// Token: 0x04001EC8 RID: 7880
		public const string Subject = "Subj";

		// Token: 0x04001EC9 RID: 7881
		public const string Importance = "Imp";

		// Token: 0x04001ECA RID: 7882
		public const string Private = "Prvt";

		// Token: 0x04001ECB RID: 7883
		public const string ReminderSet = "RemS";

		// Token: 0x04001ECC RID: 7884
		public const string ReminderTime = "RemT";

		// Token: 0x04001ECD RID: 7885
		public const string FreeBusyStatus = "Fbs";

		// Token: 0x04001ECE RID: 7886
		public const string Type = "ItemType";

		// Token: 0x04001ECF RID: 7887
		public const string SequenceNumber = "sn";

		// Token: 0x04001ED0 RID: 7888
		public const string CalendarItemId = "idci";

		// Token: 0x04001ED1 RID: 7889
		private PropertyDefinition[] properties;

		// Token: 0x020004A6 RID: 1190
		private enum ResponseAction
		{
			// Token: 0x04001ED3 RID: 7891
			EditResponse,
			// Token: 0x04001ED4 RID: 7892
			SendResponse,
			// Token: 0x04001ED5 RID: 7893
			NoResponse
		}
	}
}
