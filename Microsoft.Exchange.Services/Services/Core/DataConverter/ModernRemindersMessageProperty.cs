using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x02000184 RID: 388
	internal sealed class ModernRemindersMessageProperty : ModernRemindersPropertyBase
	{
		// Token: 0x06000B0E RID: 2830 RVA: 0x00034E7A File Offset: 0x0003307A
		private ModernRemindersMessageProperty(CommandContext commandContext) : base(commandContext)
		{
		}

		// Token: 0x06000B0F RID: 2831 RVA: 0x00034E83 File Offset: 0x00033083
		public static ModernRemindersMessageProperty CreateCommand(CommandContext commandContext)
		{
			return new ModernRemindersMessageProperty(commandContext);
		}

		// Token: 0x06000B10 RID: 2832 RVA: 0x00034E8C File Offset: 0x0003308C
		internal static void SetModernRemindersMessageProperty(ServiceObject serviceObject, Item item)
		{
			ModernReminderType[] array = (ModernReminderType[])serviceObject.PropertyBag[MessageSchema.ModernReminders];
			if (array != null)
			{
				item.Load(new PropertyDefinition[]
				{
					MessageItemSchema.QuickCaptureReminders
				});
				Reminders<ModernReminder> reminders = new Reminders<ModernReminder>();
				ModernRemindersPropertyBase.GetModernReminderSettings(array, reminders);
				((IToDoItem)item).ModernReminders = reminders;
			}
		}

		// Token: 0x06000B11 RID: 2833 RVA: 0x00034EE1 File Offset: 0x000330E1
		internal override void SetProperty(ServiceObject serviceObject, Item item)
		{
			ModernRemindersMessageProperty.SetModernRemindersMessageProperty(serviceObject, item);
		}
	}
}
