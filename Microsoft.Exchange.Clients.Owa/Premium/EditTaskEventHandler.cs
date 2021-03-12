using System;
using Microsoft.Exchange.Clients.Common;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Clients;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Clients.Owa.Premium
{
	// Token: 0x020004B1 RID: 1201
	[OwaEventNamespace("EditTask")]
	[OwaEventSegmentation(Feature.Tasks)]
	internal sealed class EditTaskEventHandler : RecurringItemEventHandler
	{
		// Token: 0x06002E06 RID: 11782 RVA: 0x00105BD8 File Offset: 0x00103DD8
		public static void Register()
		{
			OwaEventRegistry.RegisterEnum(typeof(RecurrenceRangeType));
			OwaEventRegistry.RegisterHandler(typeof(EditTaskEventHandler));
		}

		// Token: 0x06002E07 RID: 11783 RVA: 0x00105BF8 File Offset: 0x00103DF8
		[OwaEventVerb(OwaEventVerb.Get)]
		[OwaEvent("LRD")]
		public void LoadRecurrenceDialog()
		{
			this.HttpContext.Server.Execute("forms/premium/taskrecurrence.aspx", this.Writer);
		}

		// Token: 0x06002E08 RID: 11784 RVA: 0x00105C18 File Offset: 0x00103E18
		[OwaEventParameter("RcrM", typeof(int), false, true)]
		[OwaEventParameter("rd", typeof(ExDateTime), false, true)]
		[OwaEventParameter("aw", typeof(int), false, true)]
		[OwaEventParameter("mi", typeof(string), false, true)]
		[OwaEventParameter("bl", typeof(string), false, true)]
		[OwaEventParameter("rs", typeof(bool), false, true)]
		[OwaEventParameter("nt", typeof(string), false, true)]
		[OwaEventParameter("RcrT", typeof(int), false, true)]
		[OwaEventParameter("RcrI", typeof(int), false, true)]
		[OwaEventParameter("RgrI", typeof(int), false, true)]
		[OwaEventParameter("RcrDys", typeof(int), false, true)]
		[OwaEventParameter("RcrDy", typeof(int), false, true)]
		[OwaEventParameter("Id", typeof(OwaStoreObjectId), false, true)]
		[OwaEventParameter("RcrO", typeof(int), false, true)]
		[OwaEventParameter("RcrRngT", typeof(RecurrenceRangeType), false, true)]
		[OwaEventParameter("RcrRngS", typeof(ExDateTime), false, true)]
		[OwaEventParameter("RcrRngO", typeof(int), false, true)]
		[OwaEventParameter("RcrRngE", typeof(ExDateTime), false, true)]
		[OwaEvent("Save")]
		[OwaEventParameter("co", typeof(string), false, true)]
		[OwaEventParameter("ps", typeof(bool), false, true)]
		[OwaEventParameter("tw", typeof(int), false, true)]
		[OwaEventParameter("CK", typeof(string), false, true)]
		[OwaEventParameter("fId", typeof(OwaStoreObjectId), false, true)]
		[OwaEventParameter("subj", typeof(string), false, true)]
		[OwaEventParameter("sd", typeof(ExDateTime), false, true)]
		[OwaEventParameter("dd", typeof(ExDateTime), false, true)]
		[OwaEventParameter("dc", typeof(ExDateTime), false, true)]
		[OwaEventParameter("st", typeof(TaskStatus), false, true)]
		[OwaEventParameter("pri", typeof(Importance), false, true)]
		[OwaEventParameter("pc", typeof(int), false, true)]
		public void Save()
		{
			ExTraceGlobals.TasksCallTracer.TraceDebug((long)this.GetHashCode(), "EditTaskEventHandler.Save");
			bool flag = base.IsParameterSet("Id");
			bool flag2 = false;
			bool flag3 = false;
			ExDateTime? exDateTime = null;
			Task task = this.GetTask(new PropertyDefinition[0]);
			try
			{
				if (!base.IsParameterSet("Id"))
				{
					OwaStoreObjectId owaStoreObjectId = (OwaStoreObjectId)base.GetParameter("fId");
					if (owaStoreObjectId != null && owaStoreObjectId.IsOtherMailbox)
					{
						ADSessionSettings adSettings = Utilities.CreateScopedADSessionSettings(base.UserContext.LogonIdentity.DomainName);
						ExchangePrincipal exchangePrincipal = ExchangePrincipal.FromLegacyDN(adSettings, owaStoreObjectId.MailboxOwnerLegacyDN);
						task[TaskSchema.TaskOwner] = exchangePrincipal.MailboxInfo.DisplayName;
					}
					else
					{
						task[TaskSchema.TaskOwner] = base.UserContext.ExchangePrincipal.MailboxInfo.DisplayName;
					}
				}
				if (base.IsParameterSet("subj"))
				{
					task.Subject = (string)base.GetParameter("subj");
				}
				if (base.IsParameterSet("sd"))
				{
					task.StartDate = this.GetDateValue("sd");
				}
				if (base.IsParameterSet("dd"))
				{
					task.DueDate = this.GetDateValue("dd");
				}
				if (base.IsParameterSet("dc"))
				{
					exDateTime = this.GetDateValue("dc");
					if (exDateTime != null)
					{
						flag2 = true;
					}
				}
				if (base.IsParameterSet("st"))
				{
					TaskStatus taskStatus = (TaskStatus)base.GetParameter("st");
					if (taskStatus == TaskStatus.Completed)
					{
						flag2 = true;
					}
					else
					{
						TaskUtilities.SetIncomplete(task, taskStatus);
					}
				}
				if (base.IsParameterSet("pri"))
				{
					task[ItemSchema.Importance] = (Importance)base.GetParameter("pri");
				}
				if (base.IsParameterSet("pc"))
				{
					double num = (double)((int)base.GetParameter("pc")) / 100.0;
					if (!flag2 || num != 1.0)
					{
						if (num >= 0.0 && num < 1.0)
						{
							task.PercentComplete = num;
						}
						else if (num == 1.0)
						{
							flag2 = true;
						}
					}
				}
				if (base.IsParameterSet("rs"))
				{
					bool flag4 = (bool)base.GetParameter("rs");
					task[ItemSchema.ReminderIsSet] = flag4;
					if (flag4 && base.IsParameterSet("rd"))
					{
						ExDateTime? dateValue = this.GetDateValue("rd");
						if (dateValue != null)
						{
							task[ItemSchema.ReminderDueBy] = dateValue.Value;
						}
					}
				}
				if (base.IsParameterSet("ps"))
				{
					task[ItemSchema.Sensitivity] = (((bool)base.GetParameter("ps")) ? Sensitivity.Private : Sensitivity.Normal);
				}
				if (base.IsParameterSet("tw"))
				{
					int num2 = (int)base.GetParameter("tw");
					if (num2 < 0 || num2 > 1525252319)
					{
						throw new OwaInvalidRequestException(LocalizedStrings.GetNonEncoded(-1310086288));
					}
					task[TaskSchema.TotalWork] = num2;
				}
				if (base.IsParameterSet("aw"))
				{
					int num3 = (int)base.GetParameter("aw");
					if (num3 < 0 || num3 > 1525252319)
					{
						throw new OwaInvalidRequestException(LocalizedStrings.GetNonEncoded(210380742));
					}
					task[TaskSchema.ActualWork] = num3;
				}
				if (base.IsParameterSet("mi"))
				{
					task[TaskSchema.Mileage] = (string)base.GetParameter("mi");
				}
				if (base.IsParameterSet("bl"))
				{
					task[TaskSchema.BillingInformation] = (string)base.GetParameter("bl");
				}
				if (base.IsParameterSet("co"))
				{
					string text = (string)base.GetParameter("co");
					string[] value;
					if (string.IsNullOrEmpty(text))
					{
						value = new string[0];
					}
					else
					{
						value = new string[]
						{
							text
						};
					}
					task[TaskSchema.Companies] = value;
				}
				if (base.IsParameterSet("nt"))
				{
					string text2 = (string)base.GetParameter("nt");
					if (text2 != null)
					{
						BodyConversionUtilities.SetBody(task, text2, Markup.PlainText, base.UserContext);
					}
				}
				if (base.IsParameterSet("RcrT"))
				{
					Recurrence recurrence = base.CreateRecurrenceFromRequest();
					if ((recurrence != null || task.Recurrence != null) && (recurrence == null || task.Recurrence == null || !recurrence.Equals(task.Recurrence)))
					{
						task.Recurrence = recurrence;
						flag3 = true;
					}
				}
				if (flag2 && exDateTime == null)
				{
					if (task.CompleteDate == null)
					{
						exDateTime = new ExDateTime?(DateTimeUtilities.GetLocalTime());
					}
					else
					{
						exDateTime = new ExDateTime?(task.CompleteDate.Value);
					}
				}
				if (!flag3 && flag2)
				{
					task.SetStatusCompleted(exDateTime.Value);
				}
				Utilities.SaveItem(task, flag);
				task.Load();
				if (flag3 && flag2)
				{
					OwaStoreObjectId owaStoreObjectId2 = OwaStoreObjectId.CreateFromStoreObject(task);
					string changeKey = task.Id.ChangeKeyAsBase64String();
					task.Dispose();
					task = Utilities.GetItem<Task>(base.UserContext, owaStoreObjectId2, changeKey, TaskUtilities.TaskPrefetchProperties);
					task.SetStatusCompleted(exDateTime.Value);
					Utilities.SaveItem(task);
					task.Load();
				}
				if (!flag)
				{
					OwaStoreObjectId owaStoreObjectId3 = OwaStoreObjectId.CreateFromStoreObject(task);
					if (ExTraceGlobals.TasksDataTracer.IsTraceEnabled(TraceType.DebugTrace))
					{
						ExTraceGlobals.TasksDataTracer.TraceDebug<string>((long)this.GetHashCode(), "New task item ID is '{0}'", owaStoreObjectId3.ToBase64String());
					}
					this.Writer.Write("<div id=itemId>");
					this.Writer.Write(owaStoreObjectId3.ToBase64String());
					this.Writer.Write("</div>");
				}
				this.Writer.Write("<div id=ck>");
				this.Writer.Write(task.Id.ChangeKeyAsBase64String());
				this.Writer.Write("</div>");
				base.MoveItemToDestinationFolderIfInScratchPad(task);
			}
			finally
			{
				task.Dispose();
			}
		}

		// Token: 0x06002E09 RID: 11785 RVA: 0x00106234 File Offset: 0x00104434
		[OwaEventParameter("dc", typeof(ExDateTime))]
		[OwaEventParameter("dd", typeof(ExDateTime))]
		[OwaEvent("GetDueBy")]
		public void GetDueBy()
		{
			if (this.GetDateValue("dc") == null)
			{
				SanitizedHtmlString dueByString = TaskUtilities.GetDueByString(this.GetDateValue("dd"));
				if (dueByString != null)
				{
					this.SanitizingWriter.Write(dueByString);
				}
			}
		}

		// Token: 0x06002E0A RID: 11786 RVA: 0x00106276 File Offset: 0x00104476
		[OwaEventParameter("RcrM", typeof(int), false, true)]
		[OwaEventParameter("RcrDys", typeof(int), false, true)]
		[OwaEventParameter("RcrO", typeof(int), false, true)]
		[OwaEvent("GenerateWhen")]
		[OwaEventParameter("RgrI", typeof(int), false, true)]
		[OwaEventParameter("RcrDy", typeof(int), false, true)]
		[OwaEventParameter("RcrT", typeof(int), false, true)]
		[OwaEventParameter("RcrI", typeof(int), false, true)]
		[OwaEventParameter("RcrRngT", typeof(RecurrenceRangeType), false, true)]
		[OwaEventParameter("RcrRngS", typeof(ExDateTime), false, true)]
		[OwaEventParameter("RcrRngO", typeof(int), false, true)]
		[OwaEventParameter("RcrRngE", typeof(ExDateTime), false, true)]
		public void GenerateWhen()
		{
			this.Writer.Write(TaskUtilities.GenerateWhen(base.UserContext, base.CreateRecurrenceFromRequest()));
		}

		// Token: 0x06002E0B RID: 11787 RVA: 0x00106294 File Offset: 0x00104494
		[OwaEvent("DetailVisible")]
		public void PersistDetailsState()
		{
			ExTraceGlobals.TasksCallTracer.TraceDebug((long)this.GetHashCode(), "EditTaskEventHandler.PersistDetailsState");
			if (!base.UserContext.IsWebPartRequest)
			{
				base.UserContext.UserOptions.IsTaskDetailsVisible = !base.UserContext.UserOptions.IsTaskDetailsVisible;
				base.UserContext.UserOptions.CommitChanges();
			}
		}

		// Token: 0x06002E0C RID: 11788 RVA: 0x001062F8 File Offset: 0x001044F8
		[OwaEventParameter("CK", typeof(string), false, true)]
		[OwaEventParameter("fId", typeof(OwaStoreObjectId), false, true)]
		[OwaEventParameter("rs", typeof(bool), false, true)]
		[OwaEventParameter("Id", typeof(OwaStoreObjectId))]
		[OwaEventParameter("rd", typeof(ExDateTime), false, true)]
		[OwaEventParameter("ps", typeof(bool), false, true)]
		[OwaEvent("SaveReadForm")]
		public void SaveReadForm()
		{
			ExTraceGlobals.TasksCallTracer.TraceDebug((long)this.GetHashCode(), "EditTaskEventHandler.SaveReadForm");
			using (Task task = this.GetTask(new PropertyDefinition[0]))
			{
				if (base.IsParameterSet("rs"))
				{
					bool flag = (bool)base.GetParameter("rs");
					task[ItemSchema.ReminderIsSet] = flag;
					if (flag && base.IsParameterSet("rd"))
					{
						task[ItemSchema.ReminderDueBy] = (ExDateTime)base.GetParameter("rd");
					}
				}
				if (base.IsParameterSet("ps"))
				{
					task[ItemSchema.Sensitivity] = (((bool)base.GetParameter("ps")) ? Sensitivity.Private : Sensitivity.Normal);
				}
				Utilities.SaveItem(task, true);
				task.Load();
				this.Writer.Write("<div id=ck>");
				this.Writer.Write(task.Id.ChangeKeyAsBase64String());
				this.Writer.Write("</div>");
			}
		}

		// Token: 0x06002E0D RID: 11789 RVA: 0x0010641C File Offset: 0x0010461C
		private Task GetTask(params PropertyDefinition[] prefetchProperties)
		{
			Task result;
			if (base.IsParameterSet("Id"))
			{
				result = base.GetRequestItem<Task>(prefetchProperties);
			}
			else
			{
				ExTraceGlobals.TasksTracer.TraceDebug((long)this.GetHashCode(), "ItemId is null. Creating new task item.");
				OwaStoreObjectId folderId = (OwaStoreObjectId)base.GetParameter("fId");
				result = Utilities.CreateItem<Task>(folderId);
				if (Globals.ArePerfCountersEnabled)
				{
					OwaSingleCounters.ItemsCreated.Increment();
				}
			}
			return result;
		}

		// Token: 0x06002E0E RID: 11790 RVA: 0x00106484 File Offset: 0x00104684
		private ExDateTime? GetDateValue(string dateId)
		{
			ExDateTime exDateTime = (ExDateTime)base.GetParameter(dateId);
			if (exDateTime != ExDateTime.MinValue && exDateTime.Year != 1901)
			{
				return new ExDateTime?(exDateTime);
			}
			return null;
		}

		// Token: 0x04001F46 RID: 8006
		public const string EventNamespace = "EditTask";

		// Token: 0x04001F47 RID: 8007
		public const string MethodDetailsState = "DetailVisible";

		// Token: 0x04001F48 RID: 8008
		public const string MethodGetDueBy = "GetDueBy";

		// Token: 0x04001F49 RID: 8009
		public const string MethodGenerateWhen = "GenerateWhen";

		// Token: 0x04001F4A RID: 8010
		public const string MethodSaveReadForm = "SaveReadForm";

		// Token: 0x04001F4B RID: 8011
		public const string MethodLoadRecurrenceDialog = "LRD";

		// Token: 0x04001F4C RID: 8012
		public const string SubjectId = "subj";

		// Token: 0x04001F4D RID: 8013
		public const string StartDateId = "sd";

		// Token: 0x04001F4E RID: 8014
		public const string DueDateId = "dd";

		// Token: 0x04001F4F RID: 8015
		public const string DateCompletedId = "dc";

		// Token: 0x04001F50 RID: 8016
		public const string StatusId = "st";

		// Token: 0x04001F51 RID: 8017
		public const string PriorityId = "pri";

		// Token: 0x04001F52 RID: 8018
		public const string PercentCompleteId = "pc";

		// Token: 0x04001F53 RID: 8019
		public const string ReminderSetId = "rs";

		// Token: 0x04001F54 RID: 8020
		public const string ReminderDateId = "rd";

		// Token: 0x04001F55 RID: 8021
		public const string PrivateSetId = "ps";

		// Token: 0x04001F56 RID: 8022
		public const string CategoriesId = "cat";

		// Token: 0x04001F57 RID: 8023
		public const string AttachmentsId = "att";

		// Token: 0x04001F58 RID: 8024
		public const string TotalWorkId = "tw";

		// Token: 0x04001F59 RID: 8025
		public const string ActualWorkId = "aw";

		// Token: 0x04001F5A RID: 8026
		public const string MileageId = "mi";

		// Token: 0x04001F5B RID: 8027
		public const string BillingId = "bl";

		// Token: 0x04001F5C RID: 8028
		public const string CompaniesId = "co";

		// Token: 0x04001F5D RID: 8029
		public const string NotesId = "nt";
	}
}
