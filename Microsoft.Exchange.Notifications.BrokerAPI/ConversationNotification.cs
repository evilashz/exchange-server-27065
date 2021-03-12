using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Notifications.Broker
{
	// Token: 0x02000015 RID: 21
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	public sealed class ConversationNotification : RowNotification
	{
		// Token: 0x0600007D RID: 125 RVA: 0x00003317 File Offset: 0x00001517
		public ConversationNotification() : base(NotificationType.Conversation)
		{
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x0600007E RID: 126 RVA: 0x00003320 File Offset: 0x00001520
		// (set) Token: 0x0600007F RID: 127 RVA: 0x00003328 File Offset: 0x00001528
		[DataMember(EmitDefaultValue = false)]
		public ConversationType Conversation { get; set; }
	}
}
