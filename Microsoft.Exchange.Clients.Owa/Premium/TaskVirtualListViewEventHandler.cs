using System;
using System.IO;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Clients.Owa.Core.Controls;
using Microsoft.Exchange.Clients.Owa.Premium.Controls;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Clients;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Clients.Owa.Premium
{
	// Token: 0x020004E5 RID: 1253
	[OwaEventNamespace("TskVLV")]
	[OwaEventObjectId(typeof(OwaStoreObjectId))]
	[OwaEventSegmentation(Feature.Tasks)]
	internal sealed class TaskVirtualListViewEventHandler : FolderVirtualListViewEventHandler2
	{
		// Token: 0x06002F8F RID: 12175 RVA: 0x00113A96 File Offset: 0x00111C96
		public new static void Register()
		{
			FolderVirtualListViewEventHandler2.Register();
			OwaEventRegistry.RegisterHandler(typeof(TaskVirtualListViewEventHandler));
		}

		// Token: 0x06002F90 RID: 12176 RVA: 0x00113AAC File Offset: 0x00111CAC
		protected override void PersistFilter()
		{
			if (!base.UserContext.IsWebPartRequest)
			{
				int num = (int)base.GetParameter("fltr");
				int folderProperty = Utilities.GetFolderProperty<int>(base.ContextFolder, ViewStateProperties.ViewFilter, 1);
				if (num != folderProperty)
				{
					base.ContextFolder[ViewStateProperties.ViewFilter] = num;
					base.ContextFolder.Save();
				}
			}
		}

		// Token: 0x06002F91 RID: 12177 RVA: 0x00113B10 File Offset: 0x00111D10
		[OwaEvent("MarkComplete")]
		[OwaEventParameter("Itms", typeof(ObjectId), true)]
		[OwaEventParameter("mkIncmp", typeof(bool), false, true)]
		public void MarkComplete()
		{
			ExTraceGlobals.TasksCallTracer.TraceDebug((long)this.GetHashCode(), "TaskVirtualListViewEventHandler.MarkComplete");
			bool flag = base.IsParameterSet("mkIncmp") && (bool)base.GetParameter("mkIncmp");
			OwaStoreObjectId[] array = (OwaStoreObjectId[])base.GetParameter("Itms");
			for (int i = 0; i < array.Length; i++)
			{
				using (Item item = Utilities.GetItem<Item>(base.UserContext, array[i], this.prefetchProperties))
				{
					TaskVirtualListViewEventHandler.ThrowIfAssignedTask(item);
					item.OpenAsReadWrite();
					if (!flag)
					{
						FlagEventHandler.FlagComplete(item);
					}
					else
					{
						FlagEventHandler.SetFlag(item, FlagAction.Default, null);
					}
					Utilities.SaveItem(item);
				}
			}
		}

		// Token: 0x06002F92 RID: 12178 RVA: 0x00113BD4 File Offset: 0x00111DD4
		[OwaEventParameter("imp", typeof(Importance))]
		[OwaEvent("SetImportance")]
		[OwaEventParameter("id", typeof(ObjectId))]
		public void SetImportance()
		{
			ExTraceGlobals.TasksCallTracer.TraceDebug((long)this.GetHashCode(), "TaskVirtualListViewEventHandler.SetImportance");
			OwaStoreObjectId owaStoreObjectId = (OwaStoreObjectId)base.GetParameter("id");
			Importance importance = (Importance)base.GetParameter("imp");
			using (Item item = Utilities.GetItem<Item>(base.UserContext, owaStoreObjectId, this.prefetchProperties))
			{
				TaskVirtualListViewEventHandler.ThrowIfAssignedTask(item);
				item.OpenAsReadWrite();
				item.Importance = importance;
				Utilities.SaveItem(item);
			}
		}

		// Token: 0x06002F93 RID: 12179 RVA: 0x00113C64 File Offset: 0x00111E64
		[OwaEvent("GetDatePicker")]
		public void GetDatePicker()
		{
			ExTraceGlobals.TasksCallTracer.TraceDebug((long)this.GetHashCode(), "TaskVirtualListViewEventHandler.GetDatePicker");
			ExDateTime localTime = DateTimeUtilities.GetLocalTime();
			DatePicker datePicker = new DatePicker("divDueDateDP", localTime, 12);
			this.Writer.Write("<div id=\"divDueDateDropDown\" class=\"pu\" style=\"display:none\">");
			datePicker.Render(this.Writer);
			this.Writer.Write("</div>");
		}

		// Token: 0x06002F94 RID: 12180 RVA: 0x00113CC8 File Offset: 0x00111EC8
		[OwaEvent("SetDueDate")]
		[OwaEventParameter("id", typeof(ObjectId))]
		[OwaEventParameter("ddt", typeof(ExDateTime), false, true)]
		public void SetDueDate()
		{
			ExTraceGlobals.TasksCallTracer.TraceDebug((long)this.GetHashCode(), "TaskVirtualListViewEventHandler.SetDueDate");
			ExDateTime? exDateTime = (ExDateTime?)base.GetParameter("ddt");
			OwaStoreObjectId owaStoreObjectId = (OwaStoreObjectId)base.GetParameter("id");
			using (Item item = Utilities.GetItem<Item>(base.UserContext, owaStoreObjectId, this.prefetchProperties))
			{
				TaskVirtualListViewEventHandler.ThrowIfAssignedTask(item);
				item.OpenAsReadWrite();
				Task task = item as Task;
				if (task != null)
				{
					task.DueDate = exDateTime;
					if (task.StartDate != null && exDateTime != null && task.StartDate.Value > exDateTime.Value)
					{
						task.StartDate = exDateTime;
					}
				}
				else
				{
					string property = ItemUtility.GetProperty<string>(item, ItemSchema.FlagRequest, LocalizedStrings.GetNonEncoded(-1950847676));
					ExDateTime? startDate = ItemUtility.GetProperty<ExDateTime?>(item, ItemSchema.UtcStartDate, null);
					if (exDateTime == null)
					{
						startDate = null;
					}
					else if (startDate != null && startDate.Value > exDateTime.Value)
					{
						startDate = exDateTime;
					}
					item.SetFlag(property, startDate, exDateTime);
				}
				Utilities.SaveItem(item);
				this.Writer.Write("<div id=data dtDD=\"");
				ExDateTime date = DateTimeUtilities.GetLocalTime().Date;
				ExDateTime date2 = (exDateTime != null) ? exDateTime.Value : date;
				this.Writer.Write(DateTimeUtilities.GetJavascriptDate(date2));
				this.Writer.Write("\"");
				if (exDateTime != null && exDateTime.Value.Date < date)
				{
					this.Writer.Write(" fOD=1");
				}
				this.Writer.Write(">");
				if (exDateTime != null)
				{
					this.Writer.Write(exDateTime.Value.ToString(base.UserContext.UserOptions.DateFormat));
				}
				else
				{
					this.Writer.Write("&nbsp;");
				}
				this.Writer.Write("</div>");
			}
		}

		// Token: 0x06002F95 RID: 12181 RVA: 0x00113F10 File Offset: 0x00112110
		[OwaEventParameter("id", typeof(OwaStoreObjectId), false, true)]
		[OwaEvent("CreateTask")]
		[OwaEventParameter("ddt", typeof(ExDateTime), false, true)]
		[OwaEventParameter("fId", typeof(ObjectId))]
		[OwaEventParameter("sbj", typeof(string), false, true)]
		public void CreateTask()
		{
			ExTraceGlobals.TasksCallTracer.TraceDebug((long)this.GetHashCode(), "TaskVirtualListViewEventHandler.CreateTask");
			if (!base.IsParameterSet("sbj") && !base.IsParameterSet("ddt"))
			{
				throw new OwaInvalidRequestException("Cannot create task without subject or due date.");
			}
			OwaStoreObjectId folderId = (OwaStoreObjectId)base.GetParameter("fId");
			if (Utilities.IsDefaultFolderId(base.UserContext, folderId, DefaultFolderType.ToDoSearch))
			{
				folderId = base.UserContext.TasksFolderOwaId;
			}
			using (Task task = Utilities.CreateItem<Task>(folderId))
			{
				if (!base.IsParameterSet("id"))
				{
					task[TaskSchema.TaskOwner] = base.UserContext.ExchangePrincipal.MailboxInfo.DisplayName;
				}
				if (base.IsParameterSet("sbj"))
				{
					string text = (string)base.GetParameter("sbj");
					if (text.Length > 255)
					{
						throw new OwaInvalidRequestException("Cannot create task with subject greater than 255 characters.");
					}
					task.Subject = text;
				}
				if (base.IsParameterSet("ddt"))
				{
					task.DueDate = new ExDateTime?((ExDateTime)base.GetParameter("ddt"));
				}
				Utilities.SaveItem(task);
				task.Load();
				this.Writer.Write(Utilities.GetIdAsString(task));
			}
		}

		// Token: 0x06002F96 RID: 12182 RVA: 0x00114058 File Offset: 0x00112258
		protected override VirtualListView2 GetListView()
		{
			base.BindToFolder();
			return new TaskVirtualListView(base.UserContext, "divVLV", this.ListViewState.SortedColumn, this.ListViewState.SortOrder, base.DataFolder, this.GetViewFilter(), base.SearchScope, Utilities.CanCreateItemInFolder(base.ContextFolder), base.IsFiltered);
		}

		// Token: 0x06002F97 RID: 12183 RVA: 0x001140B4 File Offset: 0x001122B4
		protected override QueryFilter GetViewFilter()
		{
			TaskFilterType filterType = TaskFilterType.All;
			if (base.IsParameterSet("fltr"))
			{
				filterType = (TaskFilterType)base.GetParameter("fltr");
			}
			base.FolderQueryFilter = TaskView.GetFilter(filterType);
			if (!Utilities.IsPublic(base.ContextFolder) && base.GetParameter("srchf") != null)
			{
				if (base.FolderQueryFilter == null)
				{
					base.FolderQueryFilter = TaskVirtualListViewEventHandler.taskItemFilter;
				}
				else
				{
					base.FolderQueryFilter = new AndFilter(new QueryFilter[]
					{
						base.FolderQueryFilter,
						TaskVirtualListViewEventHandler.taskItemFilter
					});
				}
			}
			return base.GetViewFilter();
		}

		// Token: 0x06002F98 RID: 12184 RVA: 0x00114148 File Offset: 0x00112348
		protected override void RenderSearchInPublicFolder(TextWriter writer)
		{
			AdvancedFindComponents advancedFindComponents = AdvancedFindComponents.Categories | AdvancedFindComponents.SearchTextInSubject | AdvancedFindComponents.SearchButton;
			base.RenderAdvancedFind(this.Writer, advancedFindComponents, null);
		}

		// Token: 0x06002F99 RID: 12185 RVA: 0x00114166 File Offset: 0x00112366
		protected override void RenderAdvancedFind(TextWriter writer, OwaStoreObjectId folderId)
		{
			base.RenderAdvancedFind(writer, AdvancedFindComponents.Categories, folderId);
		}

		// Token: 0x06002F9A RID: 12186 RVA: 0x00114174 File Offset: 0x00112374
		private static void ThrowIfAssignedTask(Item item)
		{
			TaskType property = (TaskType)ItemUtility.GetProperty<int>(item, TaskSchema.TaskType, 0);
			if (TaskUtilities.IsAssignedTaskType(property))
			{
				throw new OwaInvalidRequestException("Assigned tasks cannot be edited");
			}
		}

		// Token: 0x04002153 RID: 8531
		public const string EventNamespace = "TskVLV";

		// Token: 0x04002154 RID: 8532
		public const string MethodMarkComplete = "MarkComplete";

		// Token: 0x04002155 RID: 8533
		public const string MethodSetImportance = "SetImportance";

		// Token: 0x04002156 RID: 8534
		public const string MethodGetDatePicker = "GetDatePicker";

		// Token: 0x04002157 RID: 8535
		public const string MethodSetDueDate = "SetDueDate";

		// Token: 0x04002158 RID: 8536
		public const string MethodCreateTask = "CreateTask";

		// Token: 0x04002159 RID: 8537
		public const string MarkIncomplete = "mkIncmp";

		// Token: 0x0400215A RID: 8538
		public const string Importance = "imp";

		// Token: 0x0400215B RID: 8539
		public const string Subject = "sbj";

		// Token: 0x0400215C RID: 8540
		private static TextFilter taskFilter = new TextFilter(StoreObjectSchema.ItemClass, "IPM.Task", MatchOptions.FullString, MatchFlags.IgnoreCase);

		// Token: 0x0400215D RID: 8541
		private static TextFilter custonTaskFilter = new TextFilter(StoreObjectSchema.ItemClass, "IPM.Task.", MatchOptions.Prefix, MatchFlags.IgnoreCase);

		// Token: 0x0400215E RID: 8542
		private static TextFilter taskRequestFilter = new TextFilter(StoreObjectSchema.ItemClass, "IPM.TaskRequest", MatchOptions.FullString, MatchFlags.IgnoreCase);

		// Token: 0x0400215F RID: 8543
		private static TextFilter customTaskRequestFilter = new TextFilter(StoreObjectSchema.ItemClass, "IPM.TaskRequest.", MatchOptions.Prefix, MatchFlags.IgnoreCase);

		// Token: 0x04002160 RID: 8544
		private static ComparisonFilter flagCompleteFilter = new ComparisonFilter(ComparisonOperator.Equal, ItemSchema.FlagStatus, FlagStatus.Complete);

		// Token: 0x04002161 RID: 8545
		private static ComparisonFilter flaggedFilter = new ComparisonFilter(ComparisonOperator.Equal, ItemSchema.FlagStatus, FlagStatus.Flagged);

		// Token: 0x04002162 RID: 8546
		private static QueryFilter taskItemFilter = new OrFilter(new QueryFilter[]
		{
			TaskVirtualListViewEventHandler.taskFilter,
			TaskVirtualListViewEventHandler.custonTaskFilter,
			TaskVirtualListViewEventHandler.taskRequestFilter,
			TaskVirtualListViewEventHandler.customTaskRequestFilter,
			TaskVirtualListViewEventHandler.flagCompleteFilter,
			TaskVirtualListViewEventHandler.flaggedFilter
		});

		// Token: 0x04002163 RID: 8547
		private PropertyDefinition[] prefetchProperties = new PropertyDefinition[]
		{
			TaskSchema.TaskType
		};
	}
}
