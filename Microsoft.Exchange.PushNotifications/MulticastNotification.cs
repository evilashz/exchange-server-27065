using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.PushNotifications
{
	// Token: 0x02000004 RID: 4
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Exchange.PushNotifications.Wcf")]
	internal abstract class MulticastNotification : BasicNotification
	{
		// Token: 0x06000011 RID: 17
		public abstract IEnumerable<Notification> GetFragments();

		// Token: 0x06000012 RID: 18 RVA: 0x000022BC File Offset: 0x000004BC
		protected MulticastNotification() : base(null)
		{
		}
	}
}
