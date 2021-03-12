using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x02000190 RID: 400
	internal class TaskStatusProperty : SimpleProperty
	{
		// Token: 0x06000B45 RID: 2885 RVA: 0x00035652 File Offset: 0x00033852
		private TaskStatusProperty(CommandContext commandContext, BaseConverter converter) : base(commandContext, converter)
		{
		}

		// Token: 0x06000B46 RID: 2886 RVA: 0x0003565C File Offset: 0x0003385C
		public new static TaskStatusProperty CreateCommand(CommandContext commandContext)
		{
			return new TaskStatusProperty(commandContext, new TaskStatusConverter());
		}

		// Token: 0x06000B47 RID: 2887 RVA: 0x0003566C File Offset: 0x0003386C
		protected override void SetStoreObjectProperty(StoreObject storeObject, PropertyDefinition propertyDefinition, object value)
		{
			Task task = storeObject as Task;
			switch ((TaskStatus)value)
			{
			case TaskStatus.NotStarted:
				task.SetStatusNotStarted();
				return;
			case TaskStatus.InProgress:
				task.SetStatusInProgress();
				return;
			case TaskStatus.Completed:
				CompleteDateProperty.SetStatusCompleted(task, ExDateTime.Now);
				return;
			case TaskStatus.WaitingOnOthers:
				task.SetStatusWaitingOnOthers();
				return;
			case TaskStatus.Deferred:
				task.SetStatusDeferred();
				return;
			default:
				return;
			}
		}
	}
}
