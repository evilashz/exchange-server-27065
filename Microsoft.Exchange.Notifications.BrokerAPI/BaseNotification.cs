using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Notifications.Broker
{
	// Token: 0x02000010 RID: 16
	[KnownType(typeof(SequenceIssueNotification))]
	[KnownType(typeof(ApplicationNotification))]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	public abstract class BaseNotification
	{
		// Token: 0x06000055 RID: 85 RVA: 0x0000318E File Offset: 0x0000138E
		public BaseNotification(NotificationType notificationType)
		{
			this.NotificationType = notificationType;
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000056 RID: 86 RVA: 0x0000319D File Offset: 0x0000139D
		// (set) Token: 0x06000057 RID: 87 RVA: 0x000031A5 File Offset: 0x000013A5
		[DataMember(EmitDefaultValue = false)]
		public NotificationType NotificationType { get; set; }

		// Token: 0x06000058 RID: 88 RVA: 0x000031AE File Offset: 0x000013AE
		public BaseNotification Clone()
		{
			return (BaseNotification)base.MemberwiseClone();
		}
	}
}
