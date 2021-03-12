using System;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.CalendarNotification
{
	// Token: 0x020000EC RID: 236
	internal sealed class SettingsChangeListener
	{
		// Token: 0x060009DC RID: 2524 RVA: 0x000412D8 File Offset: 0x0003F4D8
		private SettingsChangeListener()
		{
		}

		// Token: 0x17000269 RID: 617
		// (get) Token: 0x060009DD RID: 2525 RVA: 0x000412E0 File Offset: 0x0003F4E0
		internal static SettingsChangeListener Instance
		{
			get
			{
				return SettingsChangeListener.instance;
			}
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x060009DE RID: 2526 RVA: 0x000412E8 File Offset: 0x0003F4E8
		// (remove) Token: 0x060009DF RID: 2527 RVA: 0x00041320 File Offset: 0x0003F520
		internal event SettingsChangeEventHandler UserSettingsChanged;

		// Token: 0x060009E0 RID: 2528 RVA: 0x00041355 File Offset: 0x0003F555
		internal void RaiseSettingsChangedEvent(UserSettings settings, InfoFromUserMailboxSession info)
		{
			if (this.UserSettingsChanged != null)
			{
				this.UserSettingsChanged(this, settings, info);
			}
		}

		// Token: 0x04000680 RID: 1664
		private static SettingsChangeListener instance = new SettingsChangeListener();
	}
}
