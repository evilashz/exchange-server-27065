using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x02000107 RID: 263
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class PushNotificationConfigurationException : PushNotificationPermanentException
	{
		// Token: 0x06000898 RID: 2200 RVA: 0x00019DEA File Offset: 0x00017FEA
		public PushNotificationConfigurationException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06000899 RID: 2201 RVA: 0x00019DF3 File Offset: 0x00017FF3
		public PushNotificationConfigurationException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600089A RID: 2202 RVA: 0x00019DFD File Offset: 0x00017FFD
		protected PushNotificationConfigurationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600089B RID: 2203 RVA: 0x00019E07 File Offset: 0x00018007
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
