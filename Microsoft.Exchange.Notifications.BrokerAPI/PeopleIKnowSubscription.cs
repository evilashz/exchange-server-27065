using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Notifications.Broker
{
	// Token: 0x0200002A RID: 42
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[ClassAccessLevel(AccessLevel.MSInternal)]
	public sealed class PeopleIKnowSubscription : RowSubscription
	{
		// Token: 0x060000F2 RID: 242 RVA: 0x00003A68 File Offset: 0x00001C68
		public PeopleIKnowSubscription() : base(NotificationType.PeopleIKnow)
		{
		}
	}
}
