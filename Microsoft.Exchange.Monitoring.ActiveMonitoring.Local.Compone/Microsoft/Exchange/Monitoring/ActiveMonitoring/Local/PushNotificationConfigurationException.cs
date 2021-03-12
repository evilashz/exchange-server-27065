using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Local
{
	// Token: 0x020005A9 RID: 1449
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class PushNotificationConfigurationException : LocalizedException
	{
		// Token: 0x060026E8 RID: 9960 RVA: 0x000DE2CD File Offset: 0x000DC4CD
		public PushNotificationConfigurationException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x060026E9 RID: 9961 RVA: 0x000DE2D6 File Offset: 0x000DC4D6
		public PushNotificationConfigurationException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x060026EA RID: 9962 RVA: 0x000DE2E0 File Offset: 0x000DC4E0
		protected PushNotificationConfigurationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060026EB RID: 9963 RVA: 0x000DE2EA File Offset: 0x000DC4EA
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
