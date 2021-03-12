using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Notifications.Broker
{
	// Token: 0x0200001B RID: 27
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	public sealed class PeopleIKnowNotification : RowNotification
	{
		// Token: 0x06000091 RID: 145 RVA: 0x000033C4 File Offset: 0x000015C4
		public PeopleIKnowNotification() : base(NotificationType.PeopleIKnow)
		{
		}
	}
}
