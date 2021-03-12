using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x0200018C RID: 396
	internal class TaskDueDateProperty : DateTimeProperty
	{
		// Token: 0x06000B33 RID: 2867 RVA: 0x000353E2 File Offset: 0x000335E2
		private TaskDueDateProperty(CommandContext commandContext) : base(commandContext)
		{
		}

		// Token: 0x06000B34 RID: 2868 RVA: 0x000353EB File Offset: 0x000335EB
		public new static TaskDueDateProperty CreateCommand(CommandContext commandContext)
		{
			return new TaskDueDateProperty(commandContext);
		}

		// Token: 0x06000B35 RID: 2869 RVA: 0x000353F4 File Offset: 0x000335F4
		protected override void SetStoreObjectProperty(StoreObject storeObject, PropertyDefinition propertyDefinition, object value)
		{
			Task task = storeObject as Task;
			ExDateTime exDateTime = (ExDateTime)value;
			if (task.Session.ExTimeZone == ExTimeZone.UtcTimeZone)
			{
				ExDateTime value2 = ExTimeZone.UtcTimeZone.ConvertDateTime(exDateTime);
				task.SetDueDatesForUtcSession(new ExDateTime?(value2), new ExDateTime?(value2));
				return;
			}
			task.DueDate = new ExDateTime?(exDateTime);
		}
	}
}
