using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x0200018D RID: 397
	internal sealed class TaskRecurrenceProperty : ComplexPropertyBase, IToXmlCommand, IToServiceObjectCommand, ISetCommand, ISetUpdateCommand, IDeleteUpdateCommand, IUpdateCommand, IPropertyCommand
	{
		// Token: 0x06000B36 RID: 2870 RVA: 0x0003544C File Offset: 0x0003364C
		public TaskRecurrenceProperty(CommandContext commandContext) : base(commandContext)
		{
		}

		// Token: 0x06000B37 RID: 2871 RVA: 0x00035455 File Offset: 0x00033655
		public static TaskRecurrenceProperty CreateCommand(CommandContext commandContext)
		{
			return new TaskRecurrenceProperty(commandContext);
		}

		// Token: 0x06000B38 RID: 2872 RVA: 0x00035460 File Offset: 0x00033660
		public void ToServiceObject()
		{
			ToServiceObjectCommandSettings commandSettings = base.GetCommandSettings<ToServiceObjectCommandSettings>();
			ServiceObject serviceObject = commandSettings.ServiceObject;
			Task task = commandSettings.StoreObject as Task;
			serviceObject.PropertyBag[TaskSchema.Recurrence] = RecurrenceHelper.Recurrence.RenderForTask(task.Recurrence);
		}

		// Token: 0x06000B39 RID: 2873 RVA: 0x000354A4 File Offset: 0x000336A4
		public void Set()
		{
			SetCommandSettings commandSettings = base.GetCommandSettings<SetCommandSettings>();
			Task task = commandSettings.StoreObject as Task;
			TaskRecurrenceProperty.SetProperty(task, (TaskRecurrenceType)commandSettings.ServiceObject[TaskSchema.Recurrence]);
		}

		// Token: 0x06000B3A RID: 2874 RVA: 0x000354E0 File Offset: 0x000336E0
		public override void SetUpdate(SetPropertyUpdate setPropertyUpdate, UpdateCommandSettings updateCommandSettings)
		{
			Task task = updateCommandSettings.StoreObject as Task;
			TaskRecurrenceProperty.SetProperty(task, (TaskRecurrenceType)setPropertyUpdate.ServiceObject[TaskSchema.Recurrence]);
		}

		// Token: 0x06000B3B RID: 2875 RVA: 0x00035514 File Offset: 0x00033714
		public override void DeleteUpdate(DeletePropertyUpdate deletePropertyUpdate, UpdateCommandSettings updateCommandSettings)
		{
			Task task = updateCommandSettings.StoreObject as Task;
			task.Recurrence = null;
		}

		// Token: 0x06000B3C RID: 2876 RVA: 0x00035534 File Offset: 0x00033734
		private static void SetProperty(Task task, TaskRecurrenceType recurrenceType)
		{
			ExTimeZone timezone;
			if (ExchangeVersion.Current.Supports(ExchangeVersion.Exchange2007SP1))
			{
				timezone = task.Session.ExTimeZone;
			}
			else
			{
				timezone = null;
			}
			Recurrence recurrence;
			if (RecurrenceHelper.Recurrence.Parse(timezone, recurrenceType, out recurrence))
			{
				try
				{
					task.Recurrence = recurrence;
				}
				catch (NotSupportedException)
				{
					throw new UnsupportedRecurrenceException();
				}
			}
		}

		// Token: 0x06000B3D RID: 2877 RVA: 0x00035590 File Offset: 0x00033790
		public void ToXml()
		{
			throw new InvalidOperationException("TaskRecurrenceProperty.ToXml should not be called.");
		}
	}
}
