using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x02000189 RID: 393
	internal class CompleteDateProperty : DateTimeProperty
	{
		// Token: 0x06000B23 RID: 2851 RVA: 0x00035135 File Offset: 0x00033335
		private CompleteDateProperty(CommandContext commandContext) : base(commandContext)
		{
		}

		// Token: 0x06000B24 RID: 2852 RVA: 0x0003513E File Offset: 0x0003333E
		public new static CompleteDateProperty CreateCommand(CommandContext commandContext)
		{
			return new CompleteDateProperty(commandContext);
		}

		// Token: 0x06000B25 RID: 2853 RVA: 0x00035148 File Offset: 0x00033348
		internal static void SetStatusCompleted(Task task, ExDateTime completeTime)
		{
			if (task.Session.ExTimeZone == ExTimeZone.UtcTimeZone)
			{
				ExDateTime exDateTime = ExTimeZone.UtcTimeZone.ConvertDateTime(completeTime);
				task.SetCompleteTimesForUtcSession(exDateTime, new ExDateTime?(exDateTime));
				return;
			}
			task.SetStatusCompleted(completeTime);
		}

		// Token: 0x06000B26 RID: 2854 RVA: 0x00035188 File Offset: 0x00033388
		protected override void SetStoreObjectProperty(StoreObject storeObject, PropertyDefinition propertyDefinition, object value)
		{
			Task task = storeObject as Task;
			ExDateTime exDateTime = (ExDateTime)value;
			if (ExDateTime.Compare(ExDateTime.Now, exDateTime) < 0 && !ExchangeVersion.Current.Supports(ExchangeVersion.Exchange2012))
			{
				throw new InvalidCompleteDateException((CoreResources.IDs)3098927940U);
			}
			try
			{
				CompleteDateProperty.SetStatusCompleted(task, exDateTime);
			}
			catch (ArgumentOutOfRangeException innerException)
			{
				throw new InvalidCompleteDateException((CoreResources.IDs)3371984686U, innerException);
			}
		}
	}
}
