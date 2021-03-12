using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x0200018B RID: 395
	internal sealed class ModernRemindersTaskProperty : ModernRemindersPropertyBase, ISetCommand, IUpdateCommand, IPropertyCommand
	{
		// Token: 0x06000B2E RID: 2862 RVA: 0x00035341 File Offset: 0x00033541
		private ModernRemindersTaskProperty(CommandContext commandContext) : base(commandContext)
		{
		}

		// Token: 0x06000B2F RID: 2863 RVA: 0x0003534A File Offset: 0x0003354A
		public static ModernRemindersTaskProperty CreateCommand(CommandContext commandContext)
		{
			return new ModernRemindersTaskProperty(commandContext);
		}

		// Token: 0x06000B30 RID: 2864 RVA: 0x00035354 File Offset: 0x00033554
		void ISetCommand.Set()
		{
			SetCommandSettings commandSettings = base.GetCommandSettings<SetCommandSettings>();
			Item messageItem = commandSettings.StoreObject as Item;
			this.SetProperty(commandSettings.ServiceObject, messageItem);
		}

		// Token: 0x06000B31 RID: 2865 RVA: 0x00035384 File Offset: 0x00033584
		internal static void SetModernRemindersTaskProperty(ServiceObject serviceObject, Item item)
		{
			ModernReminderType[] array = (ModernReminderType[])serviceObject.PropertyBag[TaskSchema.ModernReminders];
			if (array != null)
			{
				item.Load(new PropertyDefinition[]
				{
					TaskSchema.QuickCaptureReminders
				});
				Reminders<ModernReminder> reminders = new Reminders<ModernReminder>();
				ModernRemindersPropertyBase.GetModernReminderSettings(array, reminders);
				((IToDoItem)item).ModernReminders = reminders;
			}
		}

		// Token: 0x06000B32 RID: 2866 RVA: 0x000353D9 File Offset: 0x000335D9
		internal override void SetProperty(ServiceObject serviceObject, Item item)
		{
			ModernRemindersTaskProperty.SetModernRemindersTaskProperty(serviceObject, item);
		}
	}
}
