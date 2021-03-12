using System;
using System.Collections;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Clients.Owa.Premium.Controls;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Clients;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Clients.Owa.Premium
{
	// Token: 0x02000457 RID: 1111
	public class EditTask : EditItemForm, IRegistryOnlyForm
	{
		// Token: 0x060028D7 RID: 10455 RVA: 0x000E7C78 File Offset: 0x000E5E78
		protected override void OnLoad(EventArgs e)
		{
			ExTraceGlobals.TasksCallTracer.TraceDebug((long)this.GetHashCode(), "EditTask.OnLoad");
			base.OnLoad(e);
			this.task = base.Initialize<Task>(false, TaskUtilities.TaskPrefetchProperties);
			if (this.task != null)
			{
				if (this.task.StartDate != null)
				{
					this.startDate = this.task.StartDate.Value;
				}
				if (this.task.DueDate != null)
				{
					this.dueDate = this.task.DueDate.Value;
				}
				if (this.task.CompleteDate != null)
				{
					this.completeDate = this.task.CompleteDate.Value;
				}
				object obj = this.task.TryGetProperty(ItemSchema.ReminderDueBy);
				if (obj is ExDateTime)
				{
					this.reminderDate = (ExDateTime)obj;
				}
				this.SetWorkTime();
				TaskUtilities.RenderInfobarMessages(this.task, this.infobar);
				this.recurrenceUtilities = new RecurrenceUtilities(this.task.Recurrence, base.Response.Output);
			}
			else
			{
				int workDayStartTimeInWorkingHoursTimeZone = base.UserContext.WorkingHours.WorkDayStartTimeInWorkingHoursTimeZone;
				this.recurrenceUtilities = new RecurrenceUtilities(null, base.Response.Output);
			}
			this.toolbar = new EditTaskToolbar(base.IsEmbeddedItem, base.UserCanDeleteItem);
		}

		// Token: 0x060028D8 RID: 10456 RVA: 0x000E7DEC File Offset: 0x000E5FEC
		private string GetPropertyValue(PropertyDefinition property)
		{
			string result = string.Empty;
			if (base.Item == null)
			{
				return result;
			}
			string text = base.Item.TryGetProperty(property) as string;
			if (text != null)
			{
				result = text;
			}
			return result;
		}

		// Token: 0x060028D9 RID: 10457 RVA: 0x000E7E21 File Offset: 0x000E6021
		protected void LoadMessageBodyIntoStream()
		{
			BodyConversionUtilities.RenderMeetingPlainTextBody(base.Response.Output, base.Item, base.UserContext, false);
		}

		// Token: 0x17000BB8 RID: 3000
		// (get) Token: 0x060028DA RID: 10458 RVA: 0x000E7E41 File Offset: 0x000E6041
		protected RecurrenceUtilities RecurrenceUtilities
		{
			get
			{
				return this.recurrenceUtilities;
			}
		}

		// Token: 0x060028DB RID: 10459 RVA: 0x000E7E49 File Offset: 0x000E6049
		protected void RenderAttachments()
		{
			AttachmentWell.RenderAttachmentWell(base.Response.Output, AttachmentWellType.ReadWrite, this.AttachmentWellRenderObjects, base.UserContext);
		}

		// Token: 0x060028DC RID: 10460 RVA: 0x000E7E68 File Offset: 0x000E6068
		protected void CreateAttachmentHelpers()
		{
			if (base.Item != null)
			{
				this.attachmentWellRenderObjects = AttachmentWell.GetAttachmentInformation(base.Item, base.AttachmentLinks, base.UserContext.IsPublicLogon, base.IsEmbeddedItem);
				InfobarRenderingHelper infobarRenderingHelper = new InfobarRenderingHelper(this.attachmentWellRenderObjects);
				if (infobarRenderingHelper.HasLevelOne)
				{
					this.infobar.AddMessage(SanitizedHtmlString.FromStringId(-2118248931), InfobarMessageType.Informational, AttachmentWell.AttachmentInfobarHtmlTag);
				}
			}
		}

		// Token: 0x060028DD RID: 10461 RVA: 0x000E7ED4 File Offset: 0x000E60D4
		protected void SetWorkTime()
		{
			object obj = this.task.TryGetProperty(TaskSchema.TotalWork);
			if (obj is int && (int)obj > 0)
			{
				this.totalWork = TaskUtilities.MinutesToWork((int)obj);
			}
			object obj2 = this.task.TryGetProperty(TaskSchema.ActualWork);
			if (obj2 is int && (int)obj2 > 0)
			{
				this.actualWork = TaskUtilities.MinutesToWork((int)obj2);
			}
		}

		// Token: 0x060028DE RID: 10462 RVA: 0x000E7F47 File Offset: 0x000E6147
		protected void RenderStartDateScriptObject()
		{
			RenderingUtilities.RenderDateTimeScriptObject(base.Response.Output, this.startDate);
		}

		// Token: 0x060028DF RID: 10463 RVA: 0x000E7F5F File Offset: 0x000E615F
		protected void RenderDueDateScriptObject()
		{
			RenderingUtilities.RenderDateTimeScriptObject(base.Response.Output, this.dueDate);
		}

		// Token: 0x060028E0 RID: 10464 RVA: 0x000E7F77 File Offset: 0x000E6177
		protected void RenderCompleteDateScriptObject()
		{
			RenderingUtilities.RenderDateTimeScriptObject(base.Response.Output, this.completeDate);
		}

		// Token: 0x060028E1 RID: 10465 RVA: 0x000E7F8F File Offset: 0x000E618F
		protected void RenderReminderDateScriptObject()
		{
			RenderingUtilities.RenderDateTimeScriptObject(base.Response.Output, this.reminderDate);
		}

		// Token: 0x060028E2 RID: 10466 RVA: 0x000E7FA7 File Offset: 0x000E61A7
		protected void RenderCurrentDateScriptObject()
		{
			RenderingUtilities.RenderDateTimeScriptObject(base.Response.Output, DateTimeUtilities.GetLocalTime());
		}

		// Token: 0x060028E3 RID: 10467 RVA: 0x000E7FBE File Offset: 0x000E61BE
		protected void RenderMinimumDateScriptObject()
		{
			RenderingUtilities.RenderDateTimeScriptObject(base.Response.Output, ExDateTime.MinValue);
		}

		// Token: 0x060028E4 RID: 10468 RVA: 0x000E7FD5 File Offset: 0x000E61D5
		protected void RenderStartDate()
		{
			DatePickerDropDownCombo.RenderDatePicker(base.Response.Output, "divSDate", this.startDate, DatePicker.Features.TodayButton | DatePicker.Features.NoneButton, true);
		}

		// Token: 0x060028E5 RID: 10469 RVA: 0x000E7FF5 File Offset: 0x000E61F5
		protected void RenderDueDate()
		{
			DatePickerDropDownCombo.RenderDatePicker(base.Response.Output, "divDateDue", this.dueDate, DatePicker.Features.TodayButton | DatePicker.Features.NoneButton, true);
		}

		// Token: 0x060028E6 RID: 10470 RVA: 0x000E8015 File Offset: 0x000E6215
		protected void RenderCompleteDate()
		{
			DatePickerDropDownCombo.RenderDatePicker(base.Response.Output, "divDateCmplt", this.completeDate, DatePicker.Features.TodayButton | DatePicker.Features.NoneButton, true);
		}

		// Token: 0x060028E7 RID: 10471 RVA: 0x000E8038 File Offset: 0x000E6238
		protected void RenderStatusDropDownList()
		{
			TaskStatus statusMapping = TaskStatus.NotStarted;
			if (this.task != null && TaskUtilities.IsValidTaskStatus(this.task.Status))
			{
				statusMapping = this.task.Status;
			}
			StatusDropDownList statusDropDownList = new StatusDropDownList("divStatus", statusMapping);
			statusDropDownList.Render(base.Response.Output);
		}

		// Token: 0x060028E8 RID: 10472 RVA: 0x000E808C File Offset: 0x000E628C
		protected void RenderPriorityDropDownList()
		{
			Importance priority = Importance.Normal;
			if (this.task != null)
			{
				object obj = this.task.TryGetProperty(ItemSchema.Importance);
				Importance importance = (Importance)obj;
				if (TaskUtilities.IsValidTaskPriority(importance))
				{
					priority = importance;
				}
			}
			PriorityDropDownList priorityDropDownList = new PriorityDropDownList("divPriority", priority);
			priorityDropDownList.Render(base.Response.Output);
		}

		// Token: 0x060028E9 RID: 10473 RVA: 0x000E80E4 File Offset: 0x000E62E4
		protected void RenderTotalWorkDurationDropDownList()
		{
			WorkDurationDropDownList workDurationDropDownList = new WorkDurationDropDownList("divTtlWrkT", this.totalWork.WorkUnit);
			workDurationDropDownList.Render(base.Response.Output);
		}

		// Token: 0x060028EA RID: 10474 RVA: 0x000E8118 File Offset: 0x000E6318
		protected void RenderActualWorkDurationDropDownList()
		{
			WorkDurationDropDownList workDurationDropDownList = new WorkDurationDropDownList("divActWrkT", this.actualWork.WorkUnit);
			workDurationDropDownList.Render(base.Response.Output);
		}

		// Token: 0x060028EB RID: 10475 RVA: 0x000E814C File Offset: 0x000E634C
		protected void RenderTaskOwner()
		{
			if (this.task != null)
			{
				string text = this.task.TryGetProperty(TaskSchema.TaskOwner) as string;
				if (!string.IsNullOrEmpty(text))
				{
					base.Response.Output.Write("<div class=\"w100\"><div class=\"hdrHr\">&nbsp;</div></div>");
					base.Response.Output.Write("<div class=\"hdrRow\">");
					base.Response.Output.Write("<div id=\"divOwner\" class=\"hdrLabel1\">");
					base.Response.Output.Write(LocalizedStrings.GetHtmlEncoded(1425891972));
					base.Response.Output.Write("</div>");
					base.Response.Output.Write("<div class=\"hdrField\">");
					Utilities.HtmlEncode(text, base.Response.Output);
					base.Response.Output.Write("</div></div>");
				}
			}
		}

		// Token: 0x060028EC RID: 10476 RVA: 0x000E8230 File Offset: 0x000E6430
		protected void RenderPercentCompleteDropDownList()
		{
			PercentCompleteDropDownList percentCompleteDropDownList = new PercentCompleteDropDownList("divPerCmplt", this.PercentComplete.ToString());
			percentCompleteDropDownList.Render(base.Response.Output);
		}

		// Token: 0x060028ED RID: 10477 RVA: 0x000E8267 File Offset: 0x000E6467
		protected void RenderTotalWorkValue()
		{
			if (this.task != null && this.totalWork.WorkAmount > 0f)
			{
				base.Response.Output.Write(this.totalWork.WorkAmount);
			}
		}

		// Token: 0x060028EE RID: 10478 RVA: 0x000E829E File Offset: 0x000E649E
		protected void RenderActualWorkValue()
		{
			if (this.task != null && this.actualWork.WorkAmount > 0f)
			{
				base.Response.Output.Write(this.actualWork.WorkAmount);
			}
		}

		// Token: 0x060028EF RID: 10479 RVA: 0x000E82D8 File Offset: 0x000E64D8
		protected void RenderMileageValue()
		{
			if (this.task != null)
			{
				string text = this.task.TryGetProperty(TaskSchema.Mileage) as string;
				if (text != null)
				{
					Utilities.HtmlEncode(text, base.Response.Output);
				}
			}
		}

		// Token: 0x060028F0 RID: 10480 RVA: 0x000E8318 File Offset: 0x000E6518
		protected void RenderBillingValue()
		{
			if (this.task != null)
			{
				string text = this.task.TryGetProperty(TaskSchema.BillingInformation) as string;
				if (text != null)
				{
					Utilities.HtmlEncode(text, base.Response.Output);
				}
			}
		}

		// Token: 0x060028F1 RID: 10481 RVA: 0x000E8358 File Offset: 0x000E6558
		protected void RenderCompaniesValue()
		{
			if (this.task != null)
			{
				string[] array = this.task.TryGetProperty(TaskSchema.Companies) as string[];
				if (array != null)
				{
					foreach (string str in array)
					{
						Utilities.HtmlEncode(str + ((array.Length > 1) ? " " : string.Empty), base.Response.Output);
					}
				}
			}
		}

		// Token: 0x060028F2 RID: 10482 RVA: 0x000E83C2 File Offset: 0x000E65C2
		protected void RenderCategoriesJavascriptArray()
		{
			CategorySwatch.RenderCategoriesJavascriptArray(base.SanitizingResponse, base.Item);
		}

		// Token: 0x060028F3 RID: 10483 RVA: 0x000E83D5 File Offset: 0x000E65D5
		protected void RenderCategories()
		{
			if (base.Item != null)
			{
				CategorySwatch.RenderCategories(base.OwaContext, base.SanitizingResponse, base.Item);
			}
		}

		// Token: 0x060028F4 RID: 10484 RVA: 0x000E83F8 File Offset: 0x000E65F8
		protected void RenderShowHideDetailIcon()
		{
			string text;
			if (this.IsDetailVisible)
			{
				text = "_d=\"1\"";
			}
			else
			{
				text = "_d=\"0\"";
			}
			base.UserContext.RenderThemeImage(base.Response.Output, this.IsDetailVisible ? ThemeFileId.Collapse : ThemeFileId.Expand, string.Empty, new object[]
			{
				"id=\"imgTsk\"",
				text
			});
		}

		// Token: 0x17000BB9 RID: 3001
		// (get) Token: 0x060028F5 RID: 10485 RVA: 0x000E8458 File Offset: 0x000E6658
		protected bool IsReminderSet
		{
			get
			{
				if (this.task != null)
				{
					object obj = this.task.TryGetProperty(ItemSchema.ReminderIsSet);
					return obj is bool && (bool)obj;
				}
				return false;
			}
		}

		// Token: 0x17000BBA RID: 3002
		// (get) Token: 0x060028F6 RID: 10486 RVA: 0x000E8490 File Offset: 0x000E6690
		protected bool IsPrivate
		{
			get
			{
				if (this.task != null)
				{
					object obj = this.task.TryGetProperty(ItemSchema.Sensitivity);
					return obj is Sensitivity && (Sensitivity)obj == Sensitivity.Private;
				}
				return false;
			}
		}

		// Token: 0x17000BBB RID: 3003
		// (get) Token: 0x060028F7 RID: 10487 RVA: 0x000E84CB File Offset: 0x000E66CB
		protected bool IsRecurring
		{
			get
			{
				return this.task != null && this.task.IsRecurring;
			}
		}

		// Token: 0x17000BBC RID: 3004
		// (get) Token: 0x060028F8 RID: 10488 RVA: 0x000E84E2 File Offset: 0x000E66E2
		protected bool IsDetailVisible
		{
			get
			{
				return base.UserContext.UserOptions.IsTaskDetailsVisible;
			}
		}

		// Token: 0x17000BBD RID: 3005
		// (get) Token: 0x060028F9 RID: 10489 RVA: 0x000E84F4 File Offset: 0x000E66F4
		protected int PercentComplete
		{
			get
			{
				if (this.task != null)
				{
					return (int)(this.task.PercentComplete * 100.0);
				}
				return 0;
			}
		}

		// Token: 0x17000BBE RID: 3006
		// (get) Token: 0x060028FA RID: 10490 RVA: 0x000E8516 File Offset: 0x000E6716
		protected bool HasAttachments
		{
			get
			{
				return this.task != null && this.task.AttachmentCollection.Count > 0;
			}
		}

		// Token: 0x17000BBF RID: 3007
		// (get) Token: 0x060028FB RID: 10491 RVA: 0x000E8535 File Offset: 0x000E6735
		protected string GetWhen
		{
			get
			{
				if (this.task != null)
				{
					return Utilities.HtmlEncode(TaskUtilities.GetWhen(this.task));
				}
				return string.Empty;
			}
		}

		// Token: 0x17000BC0 RID: 3008
		// (get) Token: 0x060028FC RID: 10492 RVA: 0x000E8555 File Offset: 0x000E6755
		protected ArrayList AttachmentWellRenderObjects
		{
			get
			{
				return this.attachmentWellRenderObjects;
			}
		}

		// Token: 0x17000BC1 RID: 3009
		// (get) Token: 0x060028FD RID: 10493 RVA: 0x000E855D File Offset: 0x000E675D
		protected Infobar Infobar
		{
			get
			{
				return this.infobar;
			}
		}

		// Token: 0x17000BC2 RID: 3010
		// (get) Token: 0x060028FE RID: 10494 RVA: 0x000E8565 File Offset: 0x000E6765
		protected EditTaskToolbar Toolbar
		{
			get
			{
				return this.toolbar;
			}
		}

		// Token: 0x17000BC3 RID: 3011
		// (get) Token: 0x060028FF RID: 10495 RVA: 0x000E856D File Offset: 0x000E676D
		protected static int WorkMinutesInDay
		{
			get
			{
				return 480;
			}
		}

		// Token: 0x17000BC4 RID: 3012
		// (get) Token: 0x06002900 RID: 10496 RVA: 0x000E8574 File Offset: 0x000E6774
		protected static int WorkMinutesInWeek
		{
			get
			{
				return 2400;
			}
		}

		// Token: 0x17000BC5 RID: 3013
		// (get) Token: 0x06002901 RID: 10497 RVA: 0x000E857B File Offset: 0x000E677B
		protected static int MaxWorkMinutes
		{
			get
			{
				return 1525252319;
			}
		}

		// Token: 0x17000BC6 RID: 3014
		// (get) Token: 0x06002902 RID: 10498 RVA: 0x000E8582 File Offset: 0x000E6782
		protected static int ImportanceLow
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000BC7 RID: 3015
		// (get) Token: 0x06002903 RID: 10499 RVA: 0x000E8585 File Offset: 0x000E6785
		protected static int ImportanceNormal
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000BC8 RID: 3016
		// (get) Token: 0x06002904 RID: 10500 RVA: 0x000E8588 File Offset: 0x000E6788
		protected static int ImportanceHigh
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x17000BC9 RID: 3017
		// (get) Token: 0x06002905 RID: 10501 RVA: 0x000E858B File Offset: 0x000E678B
		protected static int TaskStatusNotStarted
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000BCA RID: 3018
		// (get) Token: 0x06002906 RID: 10502 RVA: 0x000E858E File Offset: 0x000E678E
		protected static int TaskStatusInProgress
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000BCB RID: 3019
		// (get) Token: 0x06002907 RID: 10503 RVA: 0x000E8591 File Offset: 0x000E6791
		protected static int TaskStatusCompleted
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x17000BCC RID: 3020
		// (get) Token: 0x06002908 RID: 10504 RVA: 0x000E8594 File Offset: 0x000E6794
		protected static int TaskStatusWaitingOnOthers
		{
			get
			{
				return 3;
			}
		}

		// Token: 0x17000BCD RID: 3021
		// (get) Token: 0x06002909 RID: 10505 RVA: 0x000E8597 File Offset: 0x000E6797
		protected static int TaskStatusDeferred
		{
			get
			{
				return 4;
			}
		}

		// Token: 0x17000BCE RID: 3022
		// (get) Token: 0x0600290A RID: 10506 RVA: 0x000E859A File Offset: 0x000E679A
		protected static int StoreObjectTypeTask
		{
			get
			{
				return 19;
			}
		}

		// Token: 0x17000BCF RID: 3023
		// (get) Token: 0x0600290B RID: 10507 RVA: 0x000E859E File Offset: 0x000E679E
		protected bool IsItemNullOrEmbeddedInNonSMimeItem
		{
			get
			{
				return base.Item == null || base.IsEmbeddedItemInNonSMimeItem;
			}
		}

		// Token: 0x0600290C RID: 10508 RVA: 0x000E85B0 File Offset: 0x000E67B0
		protected void RenderTitle()
		{
			if (base.Item == null)
			{
				base.Response.Write(LocalizedStrings.GetHtmlEncoded(151903378));
				return;
			}
			string propertyValue = this.GetPropertyValue(ItemSchema.Subject);
			if (string.IsNullOrEmpty(propertyValue))
			{
				base.Response.Write(LocalizedStrings.GetHtmlEncoded(151903378));
				return;
			}
			Utilities.HtmlEncode(propertyValue, base.Response.Output);
		}

		// Token: 0x0600290D RID: 10509 RVA: 0x000E8616 File Offset: 0x000E6816
		protected void RenderReminderDate()
		{
			TaskUtilities.RenderReminderDate(base.Response.Output, base.Item, this.IsReminderSet && !this.IsPublicItem);
		}

		// Token: 0x0600290E RID: 10510 RVA: 0x000E8642 File Offset: 0x000E6842
		protected void RenderReminderTimeDropDownList()
		{
			TaskUtilities.RenderReminderTimeDropDownList(base.UserContext, base.Response.Output, base.Item, this.IsReminderSet && !this.IsPublicItem);
		}

		// Token: 0x0600290F RID: 10511 RVA: 0x000E8674 File Offset: 0x000E6874
		protected void RenderSubject()
		{
			RenderingUtilities.RenderSubject(base.Response.Output, base.Item);
		}

		// Token: 0x04001C17 RID: 7191
		private Infobar infobar = new Infobar();

		// Token: 0x04001C18 RID: 7192
		private EditTaskToolbar toolbar;

		// Token: 0x04001C19 RID: 7193
		private ArrayList attachmentWellRenderObjects;

		// Token: 0x04001C1A RID: 7194
		private Task task;

		// Token: 0x04001C1B RID: 7195
		private ExDateTime startDate = ExDateTime.MinValue;

		// Token: 0x04001C1C RID: 7196
		private ExDateTime dueDate = ExDateTime.MinValue;

		// Token: 0x04001C1D RID: 7197
		private ExDateTime completeDate = ExDateTime.MinValue;

		// Token: 0x04001C1E RID: 7198
		private ExDateTime reminderDate = ExDateTime.MinValue;

		// Token: 0x04001C1F RID: 7199
		private Work actualWork = new Work(0f, DurationUnit.Hours);

		// Token: 0x04001C20 RID: 7200
		private Work totalWork = new Work(0f, DurationUnit.Hours);

		// Token: 0x04001C21 RID: 7201
		private RecurrenceUtilities recurrenceUtilities;
	}
}
