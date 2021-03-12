using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Wcf;
using Microsoft.Exchange.Services.Wcf.Types;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000355 RID: 853
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class SetNotificationSettings : ServiceCommand<bool>
	{
		// Token: 0x06001BBE RID: 7102 RVA: 0x0006AD08 File Offset: 0x00068F08
		public SetNotificationSettings(CallContext callContext, NotificationSettingsRequest settings) : base(callContext)
		{
			this.settings = settings;
		}

		// Token: 0x06001BBF RID: 7103 RVA: 0x0006AD18 File Offset: 0x00068F18
		protected override bool InternalExecute()
		{
			UserConfigurationPropertyDefinition propertyDefinition = UserOptionPropertySchema.Instance.GetPropertyDefinition(UserConfigurationPropertyId.EnableReminders);
			UserConfigurationPropertyDefinition propertyDefinition2 = UserOptionPropertySchema.Instance.GetPropertyDefinition(UserConfigurationPropertyId.EnableReminderSound);
			UserConfigurationPropertyDefinition propertyDefinition3 = UserOptionPropertySchema.Instance.GetPropertyDefinition(UserConfigurationPropertyId.NewItemNotify);
			new UserOptionsType
			{
				EnableReminders = this.settings.EnableReminders,
				EnableReminderSound = this.settings.EnableReminderSound,
				NewItemNotify = (NewNotification)this.settings.NewItemNotify
			}.Commit(base.CallContext, new UserConfigurationPropertyDefinition[]
			{
				propertyDefinition,
				propertyDefinition2,
				propertyDefinition3
			});
			return true;
		}

		// Token: 0x04000FBE RID: 4030
		private NotificationSettingsRequest settings;
	}
}
