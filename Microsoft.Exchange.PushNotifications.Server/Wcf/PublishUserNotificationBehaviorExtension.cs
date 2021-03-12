using System;
using System.ServiceModel.Configuration;

namespace Microsoft.Exchange.PushNotifications.Server.Wcf
{
	// Token: 0x0200002A RID: 42
	internal class PublishUserNotificationBehaviorExtension : BehaviorExtensionElement
	{
		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000105 RID: 261 RVA: 0x0000454D File Offset: 0x0000274D
		public override Type BehaviorType
		{
			get
			{
				return typeof(PublishUserNotificationBehavior);
			}
		}

		// Token: 0x06000106 RID: 262 RVA: 0x00004559 File Offset: 0x00002759
		protected override object CreateBehavior()
		{
			return new PublishUserNotificationBehavior();
		}
	}
}
