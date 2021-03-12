using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x0200018E RID: 398
	internal class TaskStartDateProperty : DateTimeProperty
	{
		// Token: 0x06000B3E RID: 2878 RVA: 0x0003559C File Offset: 0x0003379C
		private TaskStartDateProperty(CommandContext commandContext) : base(commandContext)
		{
		}

		// Token: 0x06000B3F RID: 2879 RVA: 0x000355A5 File Offset: 0x000337A5
		public new static TaskStartDateProperty CreateCommand(CommandContext commandContext)
		{
			return new TaskStartDateProperty(commandContext);
		}

		// Token: 0x06000B40 RID: 2880 RVA: 0x000355B0 File Offset: 0x000337B0
		protected override void SetStoreObjectProperty(StoreObject storeObject, PropertyDefinition propertyDefinition, object value)
		{
			Task task = storeObject as Task;
			ExDateTime exDateTime = (ExDateTime)value;
			if (task.Session.ExTimeZone == ExTimeZone.UtcTimeZone)
			{
				ExDateTime value2 = ExTimeZone.UtcTimeZone.ConvertDateTime(exDateTime);
				task.SetStartDatesForUtcSession(new ExDateTime?(value2), new ExDateTime?(value2));
				return;
			}
			task.StartDate = new ExDateTime?(exDateTime);
		}
	}
}
