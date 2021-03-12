using System;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Clients.Owa.Premium
{
	// Token: 0x020004D6 RID: 1238
	[OwaEventNamespace("RemindersEventHandler")]
	internal sealed class RemindersEventHandler : ItemEventHandler
	{
		// Token: 0x06002F2A RID: 12074 RVA: 0x00110394 File Offset: 0x0010E594
		public static void Register()
		{
			OwaEventRegistry.RegisterHandler(typeof(RemindersEventHandler));
		}

		// Token: 0x06002F2B RID: 12075 RVA: 0x001103A8 File Offset: 0x0010E5A8
		[OwaEvent("Snooze")]
		[OwaEventParameter("LRT", typeof(ExDateTime), false, true)]
		[OwaEventParameter("Rm", typeof(ReminderInfo), true, false)]
		[OwaEventParameter("Snt", typeof(int))]
		public void Snooze()
		{
			Item item = null;
			ExDateTime actualizationTime = DateTimeUtilities.GetLocalTime();
			ReminderInfo[] array = (ReminderInfo[])base.GetParameter("Rm");
			int num = (int)base.GetParameter("Snt");
			TimeSpan t = TimeSpan.FromMinutes((double)num);
			if (base.IsParameterSet("LRT"))
			{
				actualizationTime = (ExDateTime)base.GetParameter("LRT");
			}
			bool flag = false;
			this.Writer.Write("new Array(");
			for (int i = 0; i < array.Length; i++)
			{
				StoreObjectId itemId = array[i].ItemId;
				VersionedId versionedId = Utilities.CreateItemId(base.UserContext.MailboxSession, itemId, array[i].ChangeKey);
				if (flag)
				{
					this.Writer.Write(",");
				}
				else
				{
					flag = true;
				}
				bool flag2 = true;
				try
				{
					try
					{
						item = base.GetRequestItem<Item>(versionedId, new PropertyDefinition[0]);
						object obj = item.TryGetProperty(ItemSchema.ReminderIsSet);
						if (obj is bool && !(bool)obj)
						{
							flag2 = false;
						}
					}
					catch (ObjectNotFoundException)
					{
						flag2 = false;
					}
					if (!flag2)
					{
						this.Writer.Write("new Rrsp(\"\",\"\",1)");
					}
					else
					{
						if (num <= 0)
						{
							item.Reminder.SnoozeBeforeDueBy(actualizationTime, -t);
						}
						else
						{
							item.Reminder.Snooze(actualizationTime, DateTimeUtilities.GetLocalTime() + t);
						}
						Utilities.SaveItem(item);
						item.Load();
						this.Writer.Write("new Rrsp(\"");
						Utilities.JavascriptEncode(item.Id.ChangeKeyAsBase64String(), this.Writer);
						this.Writer.Write("\",\"");
						this.Writer.Write(DateTimeUtilities.GetJavascriptDate(item.Reminder.ReminderNextTime.Value));
						this.Writer.Write("\")");
					}
				}
				finally
				{
					if (item != null)
					{
						item.Dispose();
						item = null;
					}
				}
			}
			this.Writer.Write(")");
		}

		// Token: 0x06002F2C RID: 12076 RVA: 0x001105A8 File Offset: 0x0010E7A8
		[OwaEvent("Dismiss")]
		[OwaEventParameter("LRT", typeof(ExDateTime), false, true)]
		[OwaEventParameter("Rm", typeof(ReminderInfo), true, false)]
		public void Dismiss()
		{
			ReminderInfo[] array = (ReminderInfo[])base.GetParameter("Rm");
			ExDateTime actualizationTime = DateTimeUtilities.GetLocalTime();
			Item item = null;
			if (base.IsParameterSet("LRT"))
			{
				actualizationTime = (ExDateTime)base.GetParameter("LRT");
			}
			for (int i = 0; i < array.Length; i++)
			{
				StoreObjectId itemId = array[i].ItemId;
				VersionedId versionedId = Utilities.CreateItemId(base.UserContext.MailboxSession, itemId, array[i].ChangeKey);
				try
				{
					try
					{
						item = base.GetRequestItem<Item>(versionedId, ItemBindOption.LoadRequiredPropertiesOnly, new PropertyDefinition[0]);
					}
					catch (ObjectNotFoundException)
					{
						goto IL_9D;
					}
					item.Reminder.Dismiss(actualizationTime);
					item.EnableFullValidation = false;
					Utilities.SaveItem(item);
				}
				finally
				{
					if (item != null)
					{
						item.Dispose();
						item = null;
					}
				}
				IL_9D:;
			}
		}

		// Token: 0x0400211B RID: 8475
		public const string EventNamespace = "RemindersEventHandler";

		// Token: 0x0400211C RID: 8476
		public const string MethodSnooze = "Snooze";

		// Token: 0x0400211D RID: 8477
		public const string MethodDismiss = "Dismiss";

		// Token: 0x0400211E RID: 8478
		public const string SnoozeTime = "Snt";

		// Token: 0x0400211F RID: 8479
		public const string Reminders = "Rm";

		// Token: 0x04002120 RID: 8480
		public const string LastReminderTime = "LRT";
	}
}
