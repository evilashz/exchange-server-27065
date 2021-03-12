using System;
using System.Collections;
using System.Text;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Clients.Owa.Premium.Controls;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Clients;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Clients.Owa.Premium
{
	// Token: 0x02000473 RID: 1139
	public class ReadTask : OwaForm, IRegistryOnlyForm
	{
		// Token: 0x06002B44 RID: 11076 RVA: 0x000F2F98 File Offset: 0x000F1198
		protected override void OnLoad(EventArgs e)
		{
			ExTraceGlobals.TasksCallTracer.TraceDebug((long)this.GetHashCode(), "ReadTask.OnLoad");
			base.OnLoad(e);
			this.task = base.Initialize<Task>(TaskUtilities.TaskPrefetchProperties);
			TaskUtilities.RenderInfobarMessages(this.task, this.infobar);
			if (!this.IsAssignedTask && !this.IsPreview)
			{
				this.infobar.AddMessage(2078257811, InfobarMessageType.Informational);
			}
		}

		// Token: 0x06002B45 RID: 11077 RVA: 0x000F3005 File Offset: 0x000F1205
		protected void LoadMessageBodyIntoStream()
		{
			BodyConversionUtilities.RenderMeetingPlainTextBody(base.Response.Output, base.Item, base.UserContext, false);
		}

		// Token: 0x06002B46 RID: 11078 RVA: 0x000F3025 File Offset: 0x000F1225
		protected void RenderAttachments()
		{
			AttachmentWell.RenderAttachmentWell(base.Response.Output, AttachmentWellType.ReadOnly, this.AttachmentWellRenderObjects, base.UserContext);
		}

		// Token: 0x06002B47 RID: 11079 RVA: 0x000F3044 File Offset: 0x000F1244
		protected void CreateAttachmentHelpers()
		{
			this.attachmentWellRenderObjects = AttachmentWell.GetAttachmentInformation(base.Item, base.AttachmentLinks, base.UserContext.IsPublicLogon, base.IsEmbeddedItem);
			this.shouldRenderAttachmentWell = RenderingUtilities.AddAttachmentInfobarMessages(base.Item, base.IsEmbeddedItem, false, this.infobar, this.attachmentWellRenderObjects);
		}

		// Token: 0x17000CBE RID: 3262
		// (get) Token: 0x06002B48 RID: 11080 RVA: 0x000F30A0 File Offset: 0x000F12A0
		private bool IsPreview
		{
			get
			{
				FormsRegistryContext formsRegistryContext = base.OwaContext.FormsRegistryContext;
				return formsRegistryContext != null && "Preview".Equals(formsRegistryContext.Action);
			}
		}

		// Token: 0x06002B49 RID: 11081 RVA: 0x000F30D0 File Offset: 0x000F12D0
		protected void RenderToolbar()
		{
			if (!this.IsPreview)
			{
				ReadTaskToolbar readTaskToolbar = new ReadTaskToolbar(this.IsAssignedTask, base.UserCanDeleteItem);
				readTaskToolbar.Render(base.Response.Output);
			}
		}

		// Token: 0x06002B4A RID: 11082 RVA: 0x000F3108 File Offset: 0x000F1308
		protected void RenderCurrentTimeScriptObject()
		{
			ExDateTime localTime = DateTimeUtilities.GetLocalTime();
			RenderingUtilities.RenderDateTimeScriptObject(base.Response.Output, localTime);
		}

		// Token: 0x06002B4B RID: 11083 RVA: 0x000F312C File Offset: 0x000F132C
		protected void RenderReminderTimeScriptObject()
		{
			object obj = this.task.TryGetProperty(ItemSchema.ReminderDueBy);
			if (obj is ExDateTime)
			{
				this.reminderDate = (ExDateTime)obj;
			}
			else
			{
				this.reminderDate = this.reminderDate.AddMinutes((double)base.UserContext.WorkingHours.WorkDayStartTimeInWorkingHoursTimeZone);
			}
			RenderingUtilities.RenderDateTimeScriptObject(base.Response.Output, this.reminderDate);
		}

		// Token: 0x06002B4C RID: 11084 RVA: 0x000F3198 File Offset: 0x000F1398
		protected void RenderTitle()
		{
			RenderingUtilities.RenderSubject(base.Response.Output, base.Item);
		}

		// Token: 0x06002B4D RID: 11085 RVA: 0x000F31B0 File Offset: 0x000F13B0
		protected void RenderSubject()
		{
			base.Response.Write("<div id=divSubj>");
			RenderingUtilities.RenderSubject(base.Response.Output, base.Item);
			base.Response.Write("</div>");
		}

		// Token: 0x06002B4E RID: 11086 RVA: 0x000F31E8 File Offset: 0x000F13E8
		private void RenderInformationRow(string titleDivId, string valueDivId, Strings.IDs titleStringId, params string[] nonEncodedValues)
		{
			base.SanitizingResponse.Write("<div class=\"roWellRow\"><div id=\"");
			base.SanitizingResponse.Write(titleDivId);
			base.SanitizingResponse.Write("\" class=\"roWellLabel pvwLabel\">");
			base.SanitizingResponse.Write(LocalizedStrings.GetNonEncoded(titleStringId));
			base.SanitizingResponse.Write("</div><div class=\"roWellWrap\">");
			base.SanitizingResponse.Write("<div id=\"");
			base.SanitizingResponse.Write(valueDivId);
			base.SanitizingResponse.Write("\" class=\"wellField\">");
			base.SanitizingResponse.Write(string.Join(" ", nonEncodedValues));
			base.SanitizingResponse.Write("</div></div></div>");
		}

		// Token: 0x06002B4F RID: 11087 RVA: 0x000F3298 File Offset: 0x000F1498
		protected void RenderDueDate()
		{
			if (this.task.DueDate != null)
			{
				this.RenderInformationRow("divToL", "divFieldTo", -828041243, new string[]
				{
					this.DueDate
				});
			}
		}

		// Token: 0x06002B50 RID: 11088 RVA: 0x000F32E0 File Offset: 0x000F14E0
		protected void RenderCompleteDate()
		{
			if (this.task.CompleteDate != null)
			{
				this.RenderInformationRow("divDateCompletedL", "divFieldDateCompleted", -969999070, new string[]
				{
					this.CompleteDate
				});
			}
		}

		// Token: 0x06002B51 RID: 11089 RVA: 0x000F3328 File Offset: 0x000F1528
		protected void RenderStatus()
		{
			this.RenderInformationRow("divStatusL", "divFieldStatus", -883489071, new string[]
			{
				this.Status
			});
		}

		// Token: 0x06002B52 RID: 11090 RVA: 0x000F335C File Offset: 0x000F155C
		protected void RenderPriority()
		{
			this.RenderInformationRow("divPriorityL", "divFieldPriority", 1501244451, new string[]
			{
				this.Priority
			});
		}

		// Token: 0x06002B53 RID: 11091 RVA: 0x000F3390 File Offset: 0x000F1590
		protected void RenderPercentComplete()
		{
			this.RenderInformationRow("divPercentCompleteL", "divFieldPercentComplete", 2043350763, new string[]
			{
				(this.task.PercentComplete * 100.0).ToString()
			});
		}

		// Token: 0x06002B54 RID: 11092 RVA: 0x000F33DC File Offset: 0x000F15DC
		protected void RenderTaskRecurrence()
		{
			if (this.task.IsRecurring)
			{
				this.RenderInformationRow("divRecurrenceL", "divFieldRecurrence", 998368285, new string[]
				{
					TaskUtilities.GetWhen(this.task)
				});
			}
		}

		// Token: 0x06002B55 RID: 11093 RVA: 0x000F3424 File Offset: 0x000F1624
		protected void RenderTaskOwner()
		{
			string text = this.task.TryGetProperty(TaskSchema.TaskOwner) as string;
			if (!string.IsNullOrEmpty(text))
			{
				this.RenderInformationRow("divOwnerL", "divFieldOwner", 1425891972, new string[]
				{
					text
				});
			}
		}

		// Token: 0x06002B56 RID: 11094 RVA: 0x000F3470 File Offset: 0x000F1670
		private string GetWorkAmountNonEncodedString(Work work)
		{
			StringBuilder stringBuilder = new StringBuilder(32);
			stringBuilder.Append(work.WorkAmount);
			stringBuilder.Append(" ");
			switch (work.WorkUnit)
			{
			case DurationUnit.Minutes:
				stringBuilder.Append(LocalizedStrings.GetNonEncoded(-178797907));
				break;
			case DurationUnit.Hours:
				stringBuilder.Append(LocalizedStrings.GetNonEncoded(-1483270941));
				break;
			case DurationUnit.Days:
				stringBuilder.Append(LocalizedStrings.GetNonEncoded(-1872639189));
				break;
			case DurationUnit.Weeks:
				stringBuilder.Append(LocalizedStrings.GetNonEncoded(-1893458757));
				break;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06002B57 RID: 11095 RVA: 0x000F3510 File Offset: 0x000F1710
		protected void RenderTotalWork()
		{
			object obj = this.task.TryGetProperty(TaskSchema.TotalWork);
			if (obj is int && (int)obj > 0)
			{
				Work work = TaskUtilities.MinutesToWork((int)obj);
				this.RenderInformationRow("divTotalWorkL", "divFieldTotalWork", -540606344, new string[]
				{
					this.GetWorkAmountNonEncodedString(work)
				});
			}
		}

		// Token: 0x06002B58 RID: 11096 RVA: 0x000F3574 File Offset: 0x000F1774
		protected void RenderActualWork()
		{
			object obj = this.task.TryGetProperty(TaskSchema.ActualWork);
			if (obj is int && (int)obj > 0)
			{
				Work work = TaskUtilities.MinutesToWork((int)obj);
				this.RenderInformationRow("divActualWorkL", "divFieldActualWork", -1521146692, new string[]
				{
					this.GetWorkAmountNonEncodedString(work)
				});
			}
		}

		// Token: 0x06002B59 RID: 11097 RVA: 0x000F35D8 File Offset: 0x000F17D8
		protected void RenderMileage()
		{
			string text = this.task.TryGetProperty(TaskSchema.Mileage) as string;
			if (!string.IsNullOrEmpty(text))
			{
				this.RenderInformationRow("divMileageL", "divFieldMileage", 631649291, new string[]
				{
					text
				});
			}
		}

		// Token: 0x06002B5A RID: 11098 RVA: 0x000F3624 File Offset: 0x000F1824
		protected void RenderBillingInformation()
		{
			string text = this.task.TryGetProperty(TaskSchema.BillingInformation) as string;
			if (!string.IsNullOrEmpty(text))
			{
				this.RenderInformationRow("divBillingL", "divFieldBilling", -914943280, new string[]
				{
					text
				});
			}
		}

		// Token: 0x06002B5B RID: 11099 RVA: 0x000F3670 File Offset: 0x000F1870
		protected void RenderCompanies()
		{
			string[] array = this.task.TryGetProperty(TaskSchema.Companies) as string[];
			if (array != null && array.Length > 0)
			{
				this.RenderInformationRow("divCompaniesL", "divFieldCompanies", -1940990688, array);
			}
		}

		// Token: 0x06002B5C RID: 11100 RVA: 0x000F36B2 File Offset: 0x000F18B2
		protected void RenderReminderDate()
		{
			TaskUtilities.RenderReminderDate(base.Response.Output, base.Item, this.ReminderIsSet && !this.IsPublicItem && base.UserCanEditItem);
		}

		// Token: 0x06002B5D RID: 11101 RVA: 0x000F36E3 File Offset: 0x000F18E3
		protected void RenderReminderTimeDropDownList()
		{
			TaskUtilities.RenderReminderTimeDropDownList(base.UserContext, base.Response.Output, base.Item, this.ReminderIsSet && !this.IsPublicItem && base.UserCanEditItem);
		}

		// Token: 0x06002B5E RID: 11102 RVA: 0x000F371A File Offset: 0x000F191A
		protected void RenderOwaPlainTextStyle()
		{
			OwaPlainTextStyle.WriteLocalizedStyleIntoHeadForPlainTextBody(base.Item, base.Response.Output, "DIV#divBdy");
		}

		// Token: 0x06002B5F RID: 11103 RVA: 0x000F3737 File Offset: 0x000F1937
		protected void RenderCategories()
		{
			CategorySwatch.RenderCategories(base.OwaContext, base.SanitizingResponse, base.Item);
		}

		// Token: 0x17000CBF RID: 3263
		// (get) Token: 0x06002B60 RID: 11104 RVA: 0x000F3750 File Offset: 0x000F1950
		protected ArrayList AttachmentWellRenderObjects
		{
			get
			{
				return this.attachmentWellRenderObjects;
			}
		}

		// Token: 0x17000CC0 RID: 3264
		// (get) Token: 0x06002B61 RID: 11105 RVA: 0x000F3758 File Offset: 0x000F1958
		protected bool ShouldRenderAttachmentWell
		{
			get
			{
				return this.shouldRenderAttachmentWell;
			}
		}

		// Token: 0x17000CC1 RID: 3265
		// (get) Token: 0x06002B62 RID: 11106 RVA: 0x000F3760 File Offset: 0x000F1960
		protected string DueDate
		{
			get
			{
				string result = LocalizedStrings.GetNonEncoded(1414246128);
				if (this.task.DueDate != null)
				{
					ExDateTime value = this.task.DueDate.Value;
					if (this.task.StartDate != null)
					{
						ExDateTime value2 = this.task.StartDate.Value;
						result = string.Format(LocalizedStrings.GetNonEncoded(1970384503), value2.ToString("d"), value.ToString("d"));
					}
					else
					{
						result = string.Format(LocalizedStrings.GetNonEncoded(-535552699), value.ToString("d"));
					}
				}
				return result;
			}
		}

		// Token: 0x17000CC2 RID: 3266
		// (get) Token: 0x06002B63 RID: 11107 RVA: 0x000F3818 File Offset: 0x000F1A18
		protected string CompleteDate
		{
			get
			{
				string result = LocalizedStrings.GetNonEncoded(1414246128);
				if (this.task.CompleteDate != null)
				{
					result = this.task.CompleteDate.Value.ToString("d");
				}
				return result;
			}
		}

		// Token: 0x17000CC3 RID: 3267
		// (get) Token: 0x06002B64 RID: 11108 RVA: 0x000F3868 File Offset: 0x000F1A68
		protected string Status
		{
			get
			{
				string nonEncoded = LocalizedStrings.GetNonEncoded(-27287708);
				switch (this.task.Status)
				{
				case TaskStatus.InProgress:
					nonEncoded = LocalizedStrings.GetNonEncoded(558434074);
					break;
				case TaskStatus.Completed:
					nonEncoded = LocalizedStrings.GetNonEncoded(604411353);
					break;
				case TaskStatus.WaitingOnOthers:
					nonEncoded = LocalizedStrings.GetNonEncoded(1796266637);
					break;
				case TaskStatus.Deferred:
					nonEncoded = LocalizedStrings.GetNonEncoded(-341200625);
					break;
				}
				return nonEncoded;
			}
		}

		// Token: 0x17000CC4 RID: 3268
		// (get) Token: 0x06002B65 RID: 11109 RVA: 0x000F38DC File Offset: 0x000F1ADC
		protected string Priority
		{
			get
			{
				string nonEncoded = LocalizedStrings.GetNonEncoded(1690472495);
				object obj = this.task.TryGetProperty(ItemSchema.Importance);
				if (obj is Importance)
				{
					switch ((Importance)obj)
					{
					case Importance.Low:
						nonEncoded = LocalizedStrings.GetNonEncoded(1502599728);
						break;
					case Importance.High:
						nonEncoded = LocalizedStrings.GetNonEncoded(-77932258);
						break;
					}
				}
				return nonEncoded;
			}
		}

		// Token: 0x17000CC5 RID: 3269
		// (get) Token: 0x06002B66 RID: 11110 RVA: 0x000F3944 File Offset: 0x000F1B44
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

		// Token: 0x17000CC6 RID: 3270
		// (get) Token: 0x06002B67 RID: 11111 RVA: 0x000F3980 File Offset: 0x000F1B80
		protected bool ReminderIsSet
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

		// Token: 0x17000CC7 RID: 3271
		// (get) Token: 0x06002B68 RID: 11112 RVA: 0x000F39B8 File Offset: 0x000F1BB8
		protected bool HasAttachments
		{
			get
			{
				return this.task.AttachmentCollection != null && this.task.AttachmentCollection.Count > 0;
			}
		}

		// Token: 0x17000CC8 RID: 3272
		// (get) Token: 0x06002B69 RID: 11113 RVA: 0x000F39DD File Offset: 0x000F1BDD
		protected Infobar Infobar
		{
			get
			{
				return this.infobar;
			}
		}

		// Token: 0x17000CC9 RID: 3273
		// (get) Token: 0x06002B6A RID: 11114 RVA: 0x000F39E5 File Offset: 0x000F1BE5
		protected bool IsAssignedTask
		{
			get
			{
				return TaskUtilities.IsAssignedTask(this.task);
			}
		}

		// Token: 0x17000CCA RID: 3274
		// (get) Token: 0x06002B6B RID: 11115 RVA: 0x000F39F2 File Offset: 0x000F1BF2
		protected static int StoreObjectTypeTask
		{
			get
			{
				return 19;
			}
		}

		// Token: 0x04001CE2 RID: 7394
		private const string StateAssigned = "Assigned";

		// Token: 0x04001CE3 RID: 7395
		private const string ActionOpen = "Open";

		// Token: 0x04001CE4 RID: 7396
		private const string ActionPreview = "Preview";

		// Token: 0x04001CE5 RID: 7397
		private Task task;

		// Token: 0x04001CE6 RID: 7398
		private Infobar infobar = new Infobar("divErr", "infoBarRO");

		// Token: 0x04001CE7 RID: 7399
		private ArrayList attachmentWellRenderObjects;

		// Token: 0x04001CE8 RID: 7400
		private bool shouldRenderAttachmentWell;

		// Token: 0x04001CE9 RID: 7401
		private ExDateTime reminderDate = ExDateTime.MinValue;
	}
}
